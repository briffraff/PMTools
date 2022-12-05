using FolderGenerator.Input;
using FolderGenerator.Output;
using Global;
using System;

namespace FolderGenerator
{
    public class FolderGeneratorMain
    {
        GetInput _input = new GetInput();
        Print _output = new Print();

        public void Power(Constants _gc)
        {
            // F O L D E R   G E N E R A T O R

            _output.WelcomeMessage();

            // MAIN PROJECT PATH INPUT
            Console.WriteLine();
            Console.Write(" ■■■ PROJECT : ");

            string projectPath = _input.ValidateRootDestination();
            string root = projectPath;
            //string root = _gc.debugFolder;

            // CHOOSE FOLDER STRUCTURE
            Console.WriteLine();
            Console.WriteLine(_gc.folderStructuresOptions);
            Console.Write(" ■■■ CHOOSE FOLDER STRUCTURE : ");
            var structureType = _input.FolderStructure();

            // CREATE
            Console.WriteLine();
            Console.Write(" ■■■ POWER WORD : ");
            var areYouReady = _input.IsApproved();

            // -- CONTINUE IF
            if (areYouReady)
            {
                var create = new Create(root, structureType);
                // CREATE FOLDERS
                create.Folders();

                //DONE
                Console.WriteLine("Folder generator - Done!");
            }

        }
    }
}
