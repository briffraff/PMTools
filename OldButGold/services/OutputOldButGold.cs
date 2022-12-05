using System;
using Global;
using System.Collections.Generic;
using System.Text;

namespace OldButGold.services
{
    public partial class OutputOldButGold
    {
        Constants _gc = new Constants();

        public void welcomeMessage()
        {
            Console.WriteLine($"\n████████████  {_gc.oldbutgold.ToUpper()}  ████████████");
            Console.WriteLine();
            Console.WriteLine(_gc.wip + "\n");
        }
    }
}
