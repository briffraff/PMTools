using System;
using System.IO;
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
        public string scriptFolder = @"M:\Z_Software Assets\3ds Max\BorakaScriptPack_vol1\PMTools\Agron";
        //public string scriptFolder = @"C:\Users\riffraff\Desktop\New folder\Test\PMTools";
        public string testFolder = @$"C:\Users\{string.Format(Environment.UserName)}\Desktop\PMTools\Agron";
        // Script/Config 
        public string ConfigFolder => scriptFolder + @"\Config";


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
        public string ExternalCloFilesList => GetAssetsFolder + @"\getCloFiles.txt";
        public string ExternalObjList => GetAssetsFolder + @"\getObjs.txt";
        public string ExternalRenderList => GetAssetsFolder + @"\getRenders.txt";
        public string NotTransferredFileList => GetAssetsFolder + @"\notTransfered.txt";

        // OUTPUT
        public string OutputCollectionsFolder => testFolder;
        public string RendersCollectionFolder => OutputCollectionsFolder + @"\Collections\renders";
        public string ObjsCollectionFolder => OutputCollectionsFolder + @"\Collections\objs";
        public string CloFilesCollectionFolder => OutputCollectionsFolder + @"\Collections\clo files";

        // regex
        public string correctFullPathAndRenderNameRegex =
            @"^(?<path>.:\\.+\\)?(?<sku>[0-9A-Za-z]+)_{1}(?<colorCode>[A-Z]+)_{1}((?<variation>[0-9])_)?(?<view>50|60)(?<extension>.png)$";
        public string correctRenderName =
            @"(?<sku>[0-9A-Za-z]+)_{1}(?<colorCode>[A-Z]+)_{1}((?<variation>[0-9])_)?(?<view>50|60)(?<extension>.png)";
        public string correctFullPathAndObjNameRegex =
            @"^(?<path>.:\\.+\\)?(?<sku>[0-9A-Za-z]+)_{1}(?<cameraMarker>[S,M,L,D]{1,})(?<extension>.obj)$";
        public string correctFullPathAndCloFileNameRegex =
            @"^(?<path>.:\\.+\\)?(?<geometry>[A-Z0-9]+)_(?<sku>[0-9A-Za-z]+)_(?<garmentInfo>[A-Za-z0-9_\-\s]+)_(?<variation>[0-9]{1,2})(?<extension>.zprj)$";

        public string pngPattern = "*.png";
        public string renderExtension = ".png";
        public string objExtension = ".obj";
        public string cloExtension = ".zprj";

        public string renderFolder = @"\09_Renders";
        public string forRenderingFolder = @"\07_For_Rendering";
        public string cloFilesFolder = @"\02_CloFiles";

        //public string renderFolder = @"\Renders";
        //public string forRenderingFolder = @"\Obj";
        //public string cloFilesFolder = @"\Garment";

        public string oldFolder = @"\Old";
        public string archiveFolder = @"\Archive";

        public string[] NotAllowedFolders = { "\\Archive\\", "\\01_References_LA\\", "\\03_Export_For_Texturing\\", "\\04_Assets\\", "\\05_Blender_Scene\\", "\\06_Maps\\", "\\10_MailOut\\", "\\Old\\" };
        //public string[] NotAllowedFolders = { "\\Archive\\", "\\DXF\\", "\\GLB\\", "\\Maps\\", "\\Ref\\", "\\Blender_Scene\\", "\\MailOut\\", "\\Old\\" };


        public string getAssetsOptions = "\nOPTIONS: \n " +
                                         "[ --- [1] Collect ALL RENDERS --- ] \n " +
                                         "[ --- [2] Collect SPECIFIC RENDERS from external path --- ] \n " +
                                         "[ --- [3] Collect RENDERS by given DATE RANGE --- ] \n " +
                                         "[ --- [4] Collect OBJS --- ] \n " +
                                         "[ --- [5] Collect WORKING FILES --- ]";

        //  "O l d   b u t   g o l d"
        public string oldbutgold = "O l d   b u t   g o l d";
        public string OldButGoldFolder => ConfigFolder + @"\OldButGold";

    }
}
