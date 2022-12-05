﻿using System.IO;
using System.Runtime.CompilerServices;

namespace Global
{
    public class Constants
    {
        public string appName = "P r o j e c t   M a n a g e r   T o o l s";

        // global vars
        public string elementSeparator = ",";
        public string nobrand = "NoBrand";
        public string nike = "Nike";
        public string agronsocks = "Agron Socks";
        public string agronequipment = "Agron EQ";
        public string wip = "In progress";
        public string options = " " +
                      "[1] Folder Generator \n " +
                      "[2] 'Old but gold' - Check for already made products \n " +
                      "[3] Get Assets \n\n " +
                      "■■■ Your choise : ";

        // folders
        // Script/
        public string scriptFolder = @"C:\Users\riffraff\Desktop\Test";
        // Script/Config 
        public string ConfigFolder => scriptFolder + @"\PMTools\Config";


        //  "F o l d e r   G e n e r a t o r"
        public string folderGen = "F o l d e r   G e n e r a t o r";
        // Config/FolderGenerator
        public string FolderGeneratorFolder => ConfigFolder + @"\FolderGenerator";
        //// files
        public string ExternalToBeFolderList => FolderGeneratorFolder + @"\tobecreated.txt";
        public string folderStructuresOptions =
            "[ --- [1] NIKE --- ]\n" +
            "[ --- [2] Agron SOCKS --- ]\n" +
            "[ --- [3] Agron EQ --- ]\n";

        //  "G e t   A s s e t s"
        public string getAssets = "G e t   A s s e t s";
        // Config/GetAssets
        public string GetAssetsFolder => ConfigFolder + @"\GetAssets";
        //// files
        public string ExternalWorkFilesList => GetAssetsFolder + @"\getWorkFiles.txt";
        public string ExternalObjList => GetAssetsFolder + @"\getObjs.txt";
        public string ExternalRenderList => GetAssetsFolder + @"\getRenders.txt";
        public string NotTransferredFileList => GetAssetsFolder + @"\notTransfered.txt";

        // OUTPUT
        public string OutputCollectionsFolder => scriptFolder;
        public string RendersCollectionFolder => OutputCollectionsFolder + @"\Collections\renders";
        public string ObjsCollectionFolder => OutputCollectionsFolder + @"\Collections\objs";
        public string WorkingFilesCollectionFolder => OutputCollectionsFolder + @"\Collections\working files";

        // regex
        public string correctFullPathAndRenderNameRegex = @"^(?<path>.:\\.+\\)?(?<sku>[0-9A-Za-z]+)_{1}(?<colorCode>[A-Z]+)_{1}((?<variation>[0-9])_)?(?<view>50|60)(?<extension>.png)$";
        public string correctRenderName = @"(?<sku>[0-9A-Za-z]+)_{1}(?<colorCode>[A-Z]+)_{1}((?<variation>[0-9])_)?(?<view>50|60)(?<extension>.png)";

        public string pngPattern = "*.png";
        public string renderExtension = ".png";
        public string objExtension = ".obj";
        public string workingFiles = ".zprj";
        public string renderFolder = @"\09_Renders";
        public string oldFolder = @"\Old";
        public string archiveFolder = @"\Archive";

        public string getAssetsOptions = "\nOPTIONS: \n " +
                                         "[ --- [1] Collect ALL RENDERS --- ] \n " +
                                         "[ --- [2] Collect SPECIFIC RENDERS from external path --- ] \n " +
                                         "[ --- [3] Collect RENDERS from given DATE RANGE --- ] \n " +
                                         "[ --- [4] Collect OBJS --- ] \n " +
                                         "[ --- [5] Collect WORKING FILES --- ]";

        //  "O l d   b u t   g o l d"
        public string oldbutgold = "O l d   b u t   g o l d";
        public string OldButGoldFolder => ConfigFolder + @"\OldButGold";

    }
}