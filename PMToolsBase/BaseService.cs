using Global;
using System;
using System.IO;
using System.Linq;

namespace PMToolsBase
{
    internal class BaseService
    {
        readonly Constants _gc = new Constants();

        internal string GetCurrentUser()
        {
            var username = string.Format(Environment.UserName);
            return username;
        }

        internal string PrintAppHeader()
        {
            var logo = "█████████████████████████████████████████████████████████████████████\n" +
            $"████████████  {_gc.appName.ToUpper()}  ████████████\n" +
            $"█████████████████████████████████████████████████████████████████████\n";

            return logo;
        }

        internal string PrintAppOptions()
        {
            return _gc.options;
        }

        internal void INITConfigAndOutputFolder()
        {
            // INIT 
            string[] outputFolder =
            {
                _gc.RendersCollectionFolder,
                _gc.ObjsCollectionFolder,
                _gc.CloFilesCollectionFolder
            };

            string[] configFolders =
            {
                _gc.ConfigFolder,
                _gc.FolderGeneratorFolder,
                _gc.GetAssetsFolder,
                _gc.OldButGoldFolder
            };
            var folders = outputFolder.Union(configFolders).ToArray();

            string[] configFiles =
            {
                _gc.ExternalToBeFolderList,
                _gc.ExternalRenderList,
                _gc.ExternalObjList,
                _gc.ExternalCloFilesList,
                _gc.NotTransferredFileList
            };

            // folders
            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }

            // files
            foreach (var file in configFiles)
            {
                if (!File.Exists(file))
                {
                    File.Create(file);
                };
            }

        }

    }
}
