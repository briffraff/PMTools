using Global;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace GetAssets.Output
{
    internal class Collect
    {
        private readonly string _root;
        private readonly string _option;

        public Collect(string root, string option)
        {
            _root = root;
            _option = option;
        }

        private readonly EnumerationOptions _enumOptions = new EnumerationOptions()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = true
        };

        private readonly Constants _gc = new Constants();
        private readonly StringBuilder _notTransfered = new StringBuilder();
        // scan subfolders of the given "root" and get all files
        private IEnumerable<string> AllFiles => Directory.GetFiles(_root, "*", _enumOptions).ToHashSet();

        internal void Renders(string externalRenderList, string rendersCollectionFolder)
        {
            if (!File.Exists(externalRenderList))
            {
                Console.WriteLine("External render list is not exist!");
                Environment.Exit(0);
            }

            _notTransfered.Clear();
            //VARS
            var png = _gc.renderExtension;
            var regex = new Regex(_gc.correctFullPathAndRenderNameRegex);
            var renders = new Dictionary<string, string[]>();
            renders.Clear();

            // is png , trimmed path , match regex

            //var filter2 = _gc.NotAllowedFolders.Select(f => filter1.Any(p => !p.Contains(f))).ToHashSet();
            //var filter3 = filter1.Where(p => !_gc.NotAllowedFolders.Any(f => p.ToUpper().Contains(f.ToUpper()))).ToHashSet();
            //var filter4 = filter1.Where(p => !p.Contains("OLD")).ToHashSet();
            //var filter5 = filter1.Where(x => _gc.NotAllowedFolders.Any(isAllowed(x))).ToHashSet();

            //filter all files from 09_Renders folder which is correct
            var filter1 = AllFiles
                .Select(x => x.Trim())
                .Where(x => x.Contains(png))
                .Where(x => regex.IsMatch(x))
                .ToHashSet();

            var correctRenders = filter1
                .Where(x => !x.Contains(_gc.NotAllowedFolders[0]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[1]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[2]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[3]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[4]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[5]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[6]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[7]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[0].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[1].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[2].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[3].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[4].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[5].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[6].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[7].ToUpper()))
                .Where(x => x.Contains(_gc.renderFolder))
                .ToHashSet();

            // collect all renders with proper names in a dictionary string,string
            FillAssets(correctRenders, renders, false);

            // OPTION 1
            if (_option == "1")
            {
                foreach (var item in renders.Values)
                {
                    var filename = Path.GetFileName(item[0]);
                    var transferTo = Path.Combine(rendersCollectionFolder, filename);
                    File.Copy(item[0], transferTo, true);
                }
            }

            //  OPTION 2
            if (_option == "2")
            {
                // read "externalRenderList" that need to be collected
                string[] text = File.ReadAllLines(externalRenderList);

                // check if a row from "externalRenderList" is already catch in dictionary
                List<string> filenames = new List<string>();
                string[] correctViews = { "50", "55", "60", "65", "70", "90" };

                foreach (var row in text)
                {
                    var filename = "";
                    var split = row.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                    var sku = "";
                    var colorCode = "";
                    var variation = "";
                    var view = "";

                    if (split.Length == 2 || split.Length == 3)
                    {
                        sku = split[0];
                        colorCode = split[1];

                        if (split.Length == 2)
                        {
                            filenames.Add(sku + "_" + colorCode + "_" + "50");
                            filenames.Add(sku + "_" + colorCode + "_" + "60");
                        }
                        if (split.Length == 3)
                        {
                            filenames.Add(sku + "_" + colorCode + "_" + split[2]);
                        }
                    }
                    else if (split.Length > 3)
                    {
                        sku = split[0];
                        colorCode = split[1];
                        variation = split[2];

                        string[] viewCollection = split
                            .Skip(3)
                            .Take(split.Length - 1)
                            .Where(x => x != null)
                            .Where(c => correctViews.Contains(c))
                            .Distinct()
                            .ToArray();

                        foreach (var v in viewCollection)
                        {
                            filename = sku + "_" + colorCode + "_" + variation + "_" + v;
                            filenames.Add(filename);
                        }
                    }

                    foreach (var f in filenames)
                    {
                        //if exists then transfer to "rendersCollectionFolder"
                        if (renders.ContainsKey(f))
                        {
                            var dest = Path.Combine(rendersCollectionFolder, f + _gc.renderExtension);
                            Transfer(renders[f][0], dest);
                        }
                        else
                        {
                            _notTransfered.AppendLine($"{f} : Not transferred\n");
                        }
                    }

                    filenames.Clear();
                }
            }

            //OPTION 3
            if (_option == "3")
            {
                // Get renders from given date or range
                Console.WriteLine("---> date format: 20/01/2023 <---");

                Console.Write("Date from : ");
                var getDateFrom = Console.ReadLine().Split(new char[] { '/', '.', ',', '\\', '-' });
                var day = Int32.Parse(getDateFrom[0]);
                var month = Int32.Parse(getDateFrom[1]);
                var year = Int32.Parse(getDateFrom[2]);
                var date = new DateTime(year, month, day).ToString().Replace("12:00:00 AM", "01:00:00 AM");
                var dateFrom = DateTime.Parse(date);

                Console.Write("Date to : ");
                var getDateTo = Console.ReadLine().Split(new char[] { '/', '.', ',', '\\', '-' });
                var dayTo = Int32.Parse(getDateTo[0]);
                var monthTo = Int32.Parse(getDateTo[1]);
                var yearTo = Int32.Parse(getDateTo[2]);
                var dateToo = new DateTime(yearTo, monthTo, dayTo).ToString().Replace("12:00:00 AM", "23:59:59 PM");
                var dateTo = DateTime.Parse(dateToo);


                //foreach (var render in renders)
                //{
                //    if (dateFrom < DateTime.Parse(render.Value[1]) && dateTo > DateTime.Parse(render.Value[1]))
                //    {
                //        Console.WriteLine(render.Value[1]);
                //    }
                //}
                
                var rendersAfterGivenDate = renders
                    .Where(r => dateFrom < DateTime.Parse(r.Value[1]) && dateTo > DateTime.Parse(r.Value[1]))
                    .ToDictionary(r => r.Key, v => v.Value);

                foreach (var item in rendersAfterGivenDate.Values)
                {
                    var filename = Path.GetFileName(item[0]);
                    var transferTo = Path.Combine(rendersCollectionFolder, filename);
                    File.Copy(item[0], transferTo, true);
                }
            }

            // write "not transferred" to external file
            NotTransferredToFile(_notTransfered, _gc.NotTransferredFileList);
        }

        internal void Objs(string externalObjFile, string objCollectionFolder)
        {
            if (!File.Exists(externalObjFile))
            {
                Console.WriteLine("External obj list is not exist!");
                Environment.Exit(0);
            }

            _notTransfered.Clear();
            //VARS
            var obj = _gc.objExtension;

            var regex = new Regex(_gc.correctFullPathAndObjNameRegex);
            var objs = new Dictionary<string, string[]>();
            objs.Clear();

            //filter all files from 09_Renders folder which is correct
            var filter1 = AllFiles
                .Select(x => x.Trim())
                .Where(x => x.Contains(obj))
                .Where(x => regex.IsMatch(x))
                .ToHashSet();

            var correctAssets = filter1
                .Where(x => !x.Contains(_gc.NotAllowedFolders[0]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[1]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[2]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[3]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[4]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[5]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[6]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[7]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[0].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[1].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[2].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[3].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[4].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[5].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[6].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[7].ToUpper()))
                .Where(x => x.Contains(_gc.forRenderingFolder))
                .ToHashSet();

            // collect all renders with proper names in a dictionary string,string
            FillAssets(correctAssets, objs, true);

            // OPTION 4
            if (_option == "4")
            {
                string[] text = File.ReadAllLines(externalObjFile).Distinct().ToArray();

                foreach (var row in text)
                {
                    var found = objs.Keys.FirstOrDefault(o => o.StartsWith(row));
                    if (found != null)
                    {
                        var filename = Path.GetFileName(objs[found][0]);
                        var transferTo = Path.Combine(objCollectionFolder, filename);
                        Transfer(objs[found][0], transferTo);
                    }
                    else
                    {
                        _notTransfered.AppendLine($"{row} : Not transferred\n");
                    }
                }
            }

            NotTransferredToFile(_notTransfered, _gc.NotTransferredFileList);
        }

        //internal Func<string, bool> isAllowed(string path)
        //{
        //    bool answer = false;

        //    foreach (var folder in _gc.NotAllowedFolders)
        //    {
        //        if (path.Contains(folder))
        //        {
        //            answer = true;
        //            break;  
        //        }
        //    }

        //    return answer;
        //}

        internal void CloFiles(string externalCloFilesFile, string cloFilesCollectionFolder)
        {
            if (!File.Exists(externalCloFilesFile))
            {
                Console.WriteLine("External clo files list is not exist!");
                Environment.Exit(0);
            }

            _notTransfered.Clear();
            //VARS
            var cloFiles = _gc.cloExtension;

            var regex = new Regex(_gc.correctFullPathAndCloFileNameRegex);
            var clos = new Dictionary<string, string[]>();
            clos.Clear();

            //filter all files from 09_Renders folder which is correct

            var filter1 = AllFiles
                .Select(x => x.Trim())
                .Where(x => x.Contains(cloFiles))
                .Where(x => regex.IsMatch(x))
                .ToHashSet();

            var correctAssets = filter1
                .Where(x => !x.Contains(_gc.NotAllowedFolders[0]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[1]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[2]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[3]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[4]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[5]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[6]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[7]))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[0].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[1].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[2].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[3].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[4].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[5].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[6].ToUpper()))
                .Where(x => !x.Contains(_gc.NotAllowedFolders[7].ToUpper()))
                .Where(x => x.Contains(_gc.cloFilesFolder))
                .ToHashSet();

            // collect all renders with proper names in a dictionary string,string
            FillAssets(correctAssets, clos, true);

            // OPTION 5
            if (_option == "5")
            {
                string[] text = File.ReadAllLines(externalCloFilesFile).Distinct().ToArray();

                foreach (var row in text)
                {
                    var found = clos.Keys.FirstOrDefault(key => key.Contains(row));
                    if (found != null)
                    {
                        var filename = Path.GetFileName(clos[found][0]);
                        var transferTo = Path.Combine(cloFilesCollectionFolder, filename);
                        Transfer(clos[found][0], transferTo);
                    }
                    else
                    {
                        _notTransfered.AppendLine($"{row} : Not transferred\n");
                    }
                }
            }

            NotTransferredToFile(_notTransfered, _gc.NotTransferredFileList);
        }

        private static void FillAssets(HashSet<string> correctAssets, Dictionary<string, string[]> assets, bool dateComparable)
        {
            // collect all renders with proper names in a dictionary string,string
            foreach (var asset in correctAssets)
            {
                var fileInfo = File.GetCreationTime(asset);

                //123345_W_5_50
                var filename = Path.GetFileNameWithoutExtension(asset);

                var isProject = asset.Contains(".zprj");
                var isObj = asset.Contains(".obj");
                var isRender = asset.Contains(".png");

                if (dateComparable && isProject)
                {
                    filename = filename.Remove(filename.Length - 2, 2);
                }

                //C:\example\ad\asd\asd\
                var assetPath = Path.GetDirectoryName(asset);
                //123345
                if (filename.Contains("_"))
                {
                    var sku = filename.Split("_", StringSplitOptions.RemoveEmptyEntries)[0];

                    // if sku is element of the renderPath - that means that file is coming from the right folder
                    // && if filename is not contains in renders dict
                    if (assetPath != null && assetPath.Contains(sku) && !assets.ContainsKey(filename))
                    {
                        assets[filename] = new string[] { asset, fileInfo.ToString() };
                    }
                    else if (dateComparable)
                    {
                        // compare file dates of new and old files
                        var isNewest = DateTime.Parse(assets[filename][1]) < fileInfo;

                        // if date is newest (new file) overwrite file into the dictionary
                        if (isNewest)
                        {
                            assets[filename] = new string[] { asset, fileInfo.ToString() };
                        }
                    }
                }
            }
        }

        private static void Transfer(string sourceFile, string destFile)
        {
            File.Copy(sourceFile, destFile, true);
        }

        private static void NotTransferredToFile(StringBuilder notTransferredCollection, string notTransferredFile)
        {
            if (File.Exists(notTransferredFile))
            {
                File.Delete(notTransferredFile);
                StreamWriter sw = new StreamWriter(notTransferredFile);
                sw.WriteLine(notTransferredCollection.ToString());
                sw.Flush();
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(notTransferredFile);
                sw.WriteLine(notTransferredCollection.ToString());
                sw.Flush();
                sw.Close();
            }
        }
    }
}