using GetAssets.Input;
using GetAssets.Output;
using Global;
using System;
using System.IO;

namespace GetAssets
{
    public class GetAssetsMain
    {
        public void Power(Constants _gc)
        {
            var _output = new OutputGetAssets(_gc);
            var _input = new InputGetAssets(_gc);

            // GET ASSETS
            _output.WelcomeMessage();

            Console.WriteLine();
            Console.Write(" ■■■ GET ASSETS FROM : ");
            var root = _input.ValidateRootDestination();

            // list of renders to be collected

            Console.WriteLine(_gc.getAssetsOptions);
            string option = _input.Option();

            ////TODO if need set path where to transfer folder
            // renders will be transfered here
            //Console.WriteLine("\nDo you want to use default collection folder ? [ y , n ]");
            // зависимост от името на логнатия юзър
            // дефолтен път
            // ако не е дефолтен път ,да се добавя допълнително

            // GET!
            Console.WriteLine();
            Console.Write(" ■■■ POWER WORD : ");
            var areYouReady = _input.IsApproved();

            // -- CONTINUE
            if (areYouReady)
            {
                var collect = new Collect(root, option);

                if (option == "1" || option == "2" || option == "2") 
                {
                    // GET RENDERS
                    collect.Renders(_gc.ExternalRenderList, _gc.RendersCollectionFolder);
                }

                if(option == "4")
                {
                    // GET OBJS
                    collect.Objs(_gc.ExternalObjList,_gc.ObjsCollectionFolder);
                }

                if(option == "5")
                {
                    // GET WORKING FILES
                    collect.CloFiles(_gc.ExternalCloFilesList, _gc.CloFilesCollectionFolder);
                }
 
                //DONE
                Console.WriteLine("Get Assets - Done!");
            }
        }
    }
}
