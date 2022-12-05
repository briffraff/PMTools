using Global;
using System;
using System.Collections.Generic;

namespace FolderGenerator.Output
{
    public class Print
    {
        private readonly Constants _gc = new Constants();
        public void Schema(string brand, Dictionary<string, List<string>> structure)
        {
            Console.WriteLine($"--- You choose {brand.ToUpper()} folder structure");
            Console.WriteLine();
            Console.WriteLine($"               ■ F O L D E R   S C H E M A - [{brand.ToUpper()}]");
            Console.WriteLine("               ■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine($"              ");
            if (brand.ToUpper() == _gc.nike.ToUpper())
            {
                Console.WriteLine($"                   [GARMENT CODE_GARMENT NAME]".ToUpper());
            }
            if (brand.ToUpper() == _gc.agronsocks.ToUpper())
            {
                Console.WriteLine($"                   [SKU_GARMENT NAME]".ToUpper());
            }
            if (brand.ToUpper() == _gc.agronequipment.ToUpper())
            {
                Console.WriteLine($"                   [SKU_GARMENT NAME]".ToUpper());
            }
            Console.WriteLine($"                      ┬");

            foreach (var folder in structure.Keys)
            {
                //Console.WriteLine($"            │");
                Console.WriteLine($"                      ├────[{folder}]");

                if (structure[folder].Count != 0)
                {
                    //Console.WriteLine($"            │      │");

                    foreach (var subfolder in structure[folder])
                    {
                        Console.WriteLine($"                      │      ├──────────[{subfolder}]");
                    }

                    Console.WriteLine($"                      │      ┴");
                }
            }
            Console.WriteLine($"                      ┴");  
        }

        public void WelcomeMessage()
        {
            Console.WriteLine($"\n████████████  {_gc.folderGen.ToUpper()}  ████████████");
            Console.WriteLine();
        }

    }
}
