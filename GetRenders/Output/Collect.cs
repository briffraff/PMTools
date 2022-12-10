﻿using Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var renders = new Dictionary<string, string>();
            renders.Clear();

            //filter all files from 09_Renders folder which is correct
            var correctRenders = AllFiles
                .Select(x => x.Trim())
                .Where(x => x.Contains(png))
                .Where(x => regex.IsMatch(x))
                .Where(x => x.Contains(_gc.renderFolder))
                .Where(x => !x.Contains(_gc.oldFolder) || !x.Contains(_gc.archiveFolder))
                .ToHashSet();

            // collect all renders with proper names in a dictionary string,string
            FillAssets(correctRenders, renders);

            // OPTION 1
            if (_option == "1")
            {
                foreach (var item in renders.Values)
                {
                    var filename = Path.GetFileName(item);
                    var transferTo = Path.Combine(rendersCollectionFolder, filename);
                    File.Copy(item, transferTo, true);
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
                    var split = row.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                    var sku = "";
                    var colorCode = "";
                    var variation = "";
                    var view = "";

                    if (split.Length == 2 || split.Length == 3)
                    {
                        sku = split[0];
                        colorCode = split[1];
                        view = "50";
                        if (split.Length == 3)
                        {
                            view = split[2];
                        }
                        filename = sku + "_" + colorCode + "_" + view;
                        filenames.Add(filename);
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
                            Transfer(renders[f], dest);
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
                //TODO GET by Date
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
            var objs = new Dictionary<string, string>();
            objs.Clear();

            //filter all files from 09_Renders folder which is correct
            var correctAssets = AllFiles
                .Select(x => x.Trim())
                .Where(x => x.Contains(obj))
                .Where(x => regex.IsMatch(x))
                .Where(x => x.Contains(_gc.forRenderingFolder))
                .Where(x => !x.Contains(_gc.oldFolder) || !x.Contains(_gc.archiveFolder))
                .ToHashSet();

            // collect all renders with proper names in a dictionary string,string
            FillAssets(correctAssets, objs);

            // OPTION 4
            if (_option == "4")
            {
                foreach (var item in objs.Values)
                {
                    var filename = Path.GetFileName(item);
                    var transferTo = Path.Combine(objCollectionFolder, filename);

                    //if exists then transfer to "objCollectionFolder"
                    if (objs.ContainsKey(Path.GetFileNameWithoutExtension(filename)))
                    {
                        Transfer(item, transferTo);
                    }
                    else
                    {
                        _notTransfered.AppendLine($"{filename} : Not transferred\n");
                    }
                }
            }

            NotTransferredToFile(_notTransfered, _gc.NotTransferredFileList);
        }

        internal void WorkingFiles()
        {
            throw new NotImplementedException();
        }

        private static void FillAssets(HashSet<string> correctRenders, Dictionary<string, string> renders)
        {
            // collect all renders with proper names in a dictionary string,string
            foreach (var render in correctRenders)
            {
                //123345_W_5_50
                var filename = Path.GetFileNameWithoutExtension(render);
                //C:\example\ad\asd\asd\
                var renderPath = Path.GetDirectoryName(render);
                //123345
                if (filename.Contains("_"))
                {
                    var sku = filename.Split("_", StringSplitOptions.RemoveEmptyEntries)[0];

                    // if sku is element of the renderPath - that means that file is coming from the right folder
                    // && if filename is not contains in renders dict
                    if (renderPath != null && renderPath.Contains(sku) && !renders.ContainsKey(filename))
                    {
                        renders[filename] = render;
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