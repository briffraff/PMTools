using Global;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GetAssets.Input
{
    public partial class InputGetAssets
    {
        private Constants _gc;

        public InputGetAssets(Constants gc)
        {
            this._gc = gc;
        }

        public string Option()
        {
            string option;
            const string exitKey = "x";
            string[] options = { "1", "2", "3" , "4" , "5" };
            var optionsDesc = new Dictionary<string, string>
            {
                ["1"] = "Collect ALL RENDERS",
                ["2"] = "Collect SPECIFIC RENDERS from external path",
                ["3"] = "Collect RENDERS from given DATE RANGE",
                ["4"] = "Collect OBJS",
                ["5"] = "Collect WORKING FILES",
            };

            Console.Write("\n■■■ Chose an option : ");

            while ((option = Console.ReadLine()?.ToLower()) != exitKey)
            {
                if (!options.Any(x => x == option))
                {
                    Console.Write($"\n{option} is not an option\n");
                    Console.Write("\n■■■ Chose again : ");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine($"Thanks! You chose option {option} - {optionsDesc[option]}");
            return option;
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
            var keyword = "GET!";
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
