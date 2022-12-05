using FolderGenerator;
using GetAssets;
using Global;
using OldButGold;
using PMToolsBase.Core.Interface;
using System;

namespace PMToolsBase.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            var _gc = new Constants();

            var baseService = new BaseService();
            baseService.INITConfigAndOutputFolder();

            var username = baseService.GetCurrentUser();

            var folderGenerator = new FolderGeneratorMain();
            var oldButFold = new OldButGoldMain();
            var getAssets = new GetAssetsMain();

            Console.WriteLine(baseService.PrintAppHeader());
            Console.Write(baseService.PrintAppOptions());

            while (true)
            {
                var option = Console.ReadLine();
                var isEnd = option == "end!" ? true : false;

                if (isEnd == true) { break; }

                switch (option)
                {
                    // F O L D E R   G E N E R A T O R
                    case "1":
                        folderGenerator.Power(_gc);
                        break;
                    // O L D   B U T  G O L D
                    case "2":
                        oldButFold.Power(_gc);
                        break;
                    // G E T   A S S E T S
                    case "3":
                        getAssets.Power(_gc);
                        break;
                }

                Console.WriteLine("\n---------------------------------------------------");
                Console.Write(baseService.PrintAppOptions());
            }
        }
    }
}
