using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using static System.Net.Mime.MediaTypeNames;

namespace FileRenamer
{
    internal class Program
    {
        /// <summary>
        /// Sample input: B23(23x44cm).png
        /// Sample output: B23_23x44cm_InkOnDrawingSheet.png
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Option option = Option.FilenameNumberDigitZeros;
            string folderPath = "E:\\Photos\\Paintings\\MA-2023";

            switch (option)
            {
                case Option.FilenameCleaner:
                    OperationMethods.FilenameCleaner(folderPath);
                    break;

                case Option.FilenameNumberDigitZeros:
                    OperationMethods.FilenameNumberDigitZeros(folderPath);
                    break;

                case Option.RenameFiles:
                    OperationMethods.RenameFiles(folderPath);
                    break;

                case Option.ReadFiles:
                    OperationMethods.ReadFiles(folderPath);
                    break;

                    //case Option.RecreateFilesWithIncrementalNumber:
                    //    RecreateFilesWithIncrementalNumber(folderPath);
                    //    break;
            }

            //string pathFolder = @"D:\PapaNewPicsMisc\20230723_190244.jpg";
            //StartRenaming(pathFolder);
            //string Paper = "InkOnPaper";
            //GetFilesByCreationTime(pathFolder);
            //SetCreateTimeInFile(@"D:\tempPapaPics\20230723_110145.jpg");

            #region Working

            bool isAutoRenameFiles = true;
            if (isAutoRenameFiles)
            {
                
                //AutoRenameFiles(@"D:\PapaNewPicsMisc\2", 173);
            }
            else
            {
                //string filename = @"D:\tempPapaPics\20230723_190244.jpg";
                //Console.WriteLine($"Old: {GetDateTakenFromImage(filename)}");
                //ChangeDateTaken(filename);
                //Console.WriteLine($"New: {GetDateTakenFromImage($@"D:\tempPapaPics\x{filename}.jpg")}");
            }

            #endregion Working

            //Console.WriteLine($"fileGetDateTakenFromImage: {fileGetDateTakenFromImage}");
            Console.ReadLine();
        }

        public static void RecreateFilesWithIncrementalNumber(string folderName)
        {
            DirectoryInfo directory = new DirectoryInfo(folderName);
            List<string> fileListByCreationTime = directory.GetFiles().OrderBy(x => x.FullName)
                                                  .Select(x => x.FullName)
                                                  .ToList();

            foreach (string oldFileName in fileListByCreationTime)
            {
                string newFileName = oldFileName.Replace(" ","");
                Console.WriteLine($"Old: {oldFileName}, New: {newFileName}");
                //File.Move(oldFileName, newFileName);
            }
        }
    }
}
