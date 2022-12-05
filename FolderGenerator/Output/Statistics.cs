using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderGenerator.Output
{
    internal class Statistics
    {
        private readonly int _tobeFolderCount;
        private readonly int _foldersCreated;
        private readonly HashSet<string> _badCreationList;

        public Statistics(int tobeFolderCount, int foldersCreated, HashSet<string> badCreationList)
        {
            this._tobeFolderCount = tobeFolderCount;
            this._foldersCreated = foldersCreated;
            this._badCreationList = badCreationList;
        }

        public void PrintStats()
        {
            Console.WriteLine();
            Console.WriteLine($"--- Expected count : {_tobeFolderCount}");
            Console.WriteLine($"--- Folders created : {_foldersCreated}");
            Console.WriteLine($"--- Bad folder names : {_badCreationList.Count}");

            foreach(var notf in _badCreationList)
            {
                Console.WriteLine($"----- [ {notf} ]");
            }

            Console.WriteLine();
        }
    }
}
