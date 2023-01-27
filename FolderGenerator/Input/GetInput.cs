using FolderGenerator.Output;
using Global;
using System;
using System.Collections.Generic;

namespace FolderGenerator.Input
{
    public class GetInput
    {
        Print _print = new Print();
        Constants _gc = new Constants();

        public Dictionary<string, List<string>> FolderStructure()
        {
            var brands = new Dictionary<int, string>
            {
                [0] = _gc.nobrand,
                [1] = _gc.nike,
                [2] = _gc.agronsocks,
                [3] = _gc.agronequipment
            };

            var structures = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                [_gc.nike] = new Dictionary<string, List<string>>
                {
                    ["Archive"] = new List<string>(),
                    ["DXF"] = new List<string>(),
                    ["FBX"] = new List<string>(),
                    ["Garments"] = new List<string>(),
                    ["Maps"] = new List<string>(),
                    ["Obj"] = new List<string>() { "MD", "Simulatron" },
                    ["Ref"] = new List<string>(),
                    ["Renders"] = new List<string>(),
                },
                [_gc.agronsocks] = new Dictionary<string, List<string>>()
                {
                    ["Archive"] = new List<string>(),
                    ["01_References_LA"] = new List<string>(),
                    ["02_CloFiles"] = new List<string>(),
                    ["03_Export_For_Texturing"] = new List<string>(),
                    ["04_Assets"] = new List<string>(),
                    ["05_Blender_Scene"] = new List<string>(),
                    ["06_Maps"] = new List<string>(),
                    ["07_For_Rendering"] = new List<string>(),
                    ["09_Renders"] = new List<string>()
                },
                [_gc.agronequipment] = new Dictionary<string, List<string>>()
                {
                    ["Archive"] = new List<string>(),
                    ["DXF"] = new List<string>(),
                    ["Garment"] = new List<string>(),
                    ["GLB"] = new List<string>(),
                    ["Maps"] = new List<string>(),
                    ["Obj"] = new List<string>(),
                    ["Ref"] = new List<string>(),
                    ["Renders"] = new List<string>(),
                }
            };

            var input = Console.ReadLine();
            var toNum = int.Parse(input);

            while (input != null)
            {
                toNum = int.Parse(input);
                bool isNumber = char.IsNumber(input[0]);
                if (isNumber && brands.ContainsKey(toNum))
                {
                    break;
                }
                Console.WriteLine();
                Console.Write("-- Please choose a number! : ");
                input = Console.ReadLine();
            }


            var brand = brands.ContainsKey(toNum) ? brands[toNum] : brands[0];
            var structure = structures.ContainsKey(brand) ? structures[brand] : null;

            _print.Schema(brand, structure);

            return structure;
        }

        public string ValidateRootDestination()
        {
            var inputRoot = Console.ReadLine();

            while (inputRoot != null)
            {
                if (inputRoot.Length >= 3)
                {
                    var firstChar = char.IsLetter(inputRoot[0]);
                    var secondChar = inputRoot[1] == ':';
                    var thirdChar = inputRoot[2] == '\\';

                    if (firstChar && secondChar && thirdChar)
                    {
                        break;
                    }
                }

                Console.WriteLine($"-- Please check project path!");
                Console.WriteLine();
                inputRoot = Console.ReadLine();
            }

            return inputRoot;
        }

        public bool IsApproved()
        {
            const string keyword = "CREATE!";
            var yesOrNo = Console.ReadLine()?.ToUpper();
            var isApproved = yesOrNo == keyword ? true : false;

            while (yesOrNo != null)
            {
                if (isApproved)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("-- Wrong! Try again!");
                    yesOrNo = Console.ReadLine()?.ToUpper();
                    isApproved = yesOrNo == keyword ? true : false;
                }
            }

            return isApproved;
        }

    }


}
