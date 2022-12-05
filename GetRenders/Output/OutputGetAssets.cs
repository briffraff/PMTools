using System;
using System.IO;
using Global;

namespace GetAssets.Output
{
    public partial class OutputGetAssets
    {
        private Constants _gc;

        public OutputGetAssets(Constants gc)
        {
            this._gc = gc;
        }

        public void WelcomeMessage()
        {
            Console.WriteLine($"\n████████████  {_gc.getAssets.ToUpper()}  ████████████");
            Console.WriteLine();
            Console.WriteLine(_gc.wip.ToUpper() + "\n");
        }
    }
}
