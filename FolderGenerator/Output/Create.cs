using Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderGenerator.Output
{
    public class Create
    {
        private Dictionary<string, List<string>> _structure;
        private string _root;
        readonly Constants _gc = new Constants();

        public Create(string root, Dictionary<string, List<string>> structure)
        {
            this._structure = structure;
            this._root = root;
        }

        // CREATE FOLDERS
        public void Folders()
        {
            var tobeFolderCount = 0;
            var foldersCreated = 0;
            var badCreationList = new HashSet<string>();

            
            var externalFolderList = _gc.ExternalToBeFolderList;
            var isExternalFolderListExist = File.Exists(externalFolderList);

            if (isExternalFolderListExist)
            {
                //read externalFolderList file rows / avoid white spaces and null
                var tobeFolders = File.ReadAllLines(externalFolderList).ToHashSet();

                tobeFolderCount = tobeFolders.Count;

                if (tobeFolderCount == 0)
                {
                    Console.WriteLine(" -------> toBeCreated.txt file is empty");
                    Environment.Exit(0);
                }

                //parent folder 
                //if row contains ","
                foreach (var folder in tobeFolders)
                {
                    var splitRow = folder.Split(_gc.elementSeparator, StringSplitOptions.RemoveEmptyEntries);

                    // if one of the element is empty space or "" / if there is no "," / if elements are less the 2
                    if (splitRow.Length > 1 && splitRow[0].Trim() != "" && splitRow[1].Trim() != "")
                    {
                        var sku = splitRow[0].Trim();
                        var name = splitRow[1].Trim();

                        var parentFolder = Path.Combine(_root, $"{sku}_{name}");
                        var isParentFolderExist = Directory.Exists(parentFolder);
                        if (isParentFolderExist == false)
                        {
                            Directory.CreateDirectory(parentFolder);
                            foldersCreated++;
                        }

                        // internal folders
                        SubFolders(parentFolder);
                    }
                    else
                    {
                        badCreationList.Add(folder);
                    }
                }
            }

            if (isExternalFolderListExist == false)
            {
                Console.WriteLine("-------> toBeCreated.txt file is missing");
            }
            else
            {
                var stats = new Statistics(tobeFolderCount, foldersCreated, badCreationList);
                stats.PrintStats();
            }
        }

        // CREATE SUBFOLDERS
        internal void SubFolders(string parentFolder)
        {
            var folders = _structure.Keys;

            foreach (var folder in folders)
            {
                var folderPath = Path.Combine(parentFolder, folder);
                var isExist = Directory.Exists(folderPath);
                if (isExist == false)
                {
                    Directory.CreateDirectory(folderPath);
                }

                var subfolders = _structure[folder];

                if (subfolders.Count != 0)
                {
                    foreach (var subfolder in subfolders)
                    {
                        var subfolderPath = Path.Combine(folderPath, subfolder);
                        var isSubFolderExist = Directory.Exists(subfolderPath);
                        if (isSubFolderExist == false)
                        {
                            Directory.CreateDirectory(subfolderPath);
                        }
                    }
                }
            }
        }
    }
}
