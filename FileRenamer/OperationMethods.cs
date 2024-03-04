using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace FileRenamer
{
    internal class OperationMethods
    {
        public static void FilenameCleaner(string folderName) //Add logic to apply these cleanup only on filename [not on folder path]
        {
            DirectoryInfo directory = new DirectoryInfo(folderName);
            List<string> fileListByCreationTime = directory.GetFiles().OrderBy(x => x.FullName)
                                                  .Select(x => x.FullName)
                                                  .ToList();

            foreach (string oldFileName in fileListByCreationTime)
            {
                string newFileName = oldFileName.Replace(" ", "");
                newFileName = newFileName.Replace("ink", "Ink");
                newFileName = newFileName.Replace("oil", "Oil");
                newFileName = newFileName.Replace("water", "Water");
                newFileName = newFileName.Replace("color", "Color");
                newFileName = newFileName.Replace("colour", "Color");
                newFileName = newFileName.Replace("Colour", "Color");
                newFileName = newFileName.Replace("on", "On");
                newFileName = newFileName.Replace("drawing", "Drawing");
                newFileName = newFileName.Replace("sheet", "Sheet");
                newFileName = newFileName.Replace("paper", "Paper");
                newFileName = newFileName.Replace("canvas", "Canvas");
                newFileName = newFileName.Replace("with", "With");
                newFileName = newFileName.Replace("black", "Black");
                newFileName = newFileName.Replace("mount", "Mount");
                newFileName = newFileName.Replace("-Ink", "_Ink");
                newFileName = newFileName.Replace("-Water", "_Water");
                newFileName = newFileName.Replace("-Oil", "_Oil");
                newFileName = newFileName.Replace("Sheet-", "Sheet_");
                newFileName = newFileName.Replace("Canvas-", "Canvas_");
                newFileName = newFileName.Replace("Paper-", "Paper_");
                newFileName = newFileName.Replace("Mount-", "Mount_");
                //newFileName = newFileName.Replace("-", "_"); This cannot be used, as it will break at folder path
                newFileName = newFileName.Replace("×", "x");

                Console.WriteLine($"Old: {oldFileName}, New: {newFileName}");
                File.Move(oldFileName, newFileName);
            }
        }

        public static void FilenameNumberDigitZeros(string folderName, int length = 4)
        {
            DirectoryInfo directory = new DirectoryInfo(folderName);
            List<string> fileListByCreationTime = directory.GetFiles().OrderBy(x => int.Parse(Path.GetFileName(x.Name.Split('_')[0]).Replace("MA", ""))) //used this logic to override 1,10,100 like sorting because of string type
                                                  .Select(x => x.FullName)
                                                  .ToList();
            List<string> paintingType = new List<string>
            {
                "MA", "BWA", "BWC", "BW", "MD" //here BWC and BW might create issue
            };

            foreach (string oldFileName in fileListByCreationTime)
            {
                //string oldFileName = "E:\\\\Photos\\\\Paintings\\\\MA-2023\\\\MA1_OilColorOnDrawingSheet_15×25cm.jpg";

                //if(!paintingType.Any(x=> oldFileName.StartsWith(x)))
                //{
                //    Console.WriteLine($"Continue for {oldFileName}");
                //    continue;
                //}

                string paintingTypeOld = oldFileName.Split('_')[0];
                string paintingTypeNew = "";
                string filenameOld = Path.GetFileName(paintingTypeOld);

                foreach (string paintingTypeObj in paintingType)
                {
                    if (filenameOld.StartsWith(paintingTypeObj))
                    {
                        string paintingNum = filenameOld.Replace(paintingTypeObj, "");
                        paintingNum = paintingNum.ToString().PadLeft(length, '0');
                        paintingTypeNew = paintingTypeObj + paintingNum;
                        break;
                    }
                }
                string newFileName = oldFileName.Replace(filenameOld, paintingTypeNew);

                Console.WriteLine($"Old: {oldFileName}, New: {newFileName}");
                //Console.WriteLine($"paintingTypeOld: {paintingTypeOld}, paintingTypeNew: {paintingTypeNew}");
                File.Move(oldFileName, newFileName);
            }
        }

        public static void RenameFiles(string FolderName)
        {
            string[] files = Directory.GetFiles(FolderName);
            foreach (string oldFileName in files)
            {
                string newFileName = oldFileName.Replace("__", "_");
                //newFileName = newFileName.Replace(")", "");

                Console.WriteLine($"Old: {oldFileName}, New: {newFileName}");
                File.Move(oldFileName, newFileName);
                //string fileName = Path.GetFileNameWithoutExtension(FileName);
                /* here the function goes that will find numbers in filename using regular experssion and replace them */
            }
        }

        public static void ReadFiles(string FolderName)
        {
            string[] files = Directory.GetFiles(FolderName);
            foreach (string fileName in files)
            {
                string[] newFileName = fileName.Split('_');
                string output = "";
                foreach(string item in newFileName)
                {
                    output = output + " " + item;
                }

                Console.WriteLine($"{newFileName[0]} {newFileName[2]}");
                //string fileName = Path.GetFileNameWithoutExtension(FileName);
                /* here the function goes that will find numbers in filename using regular experssion and replace them */
            }
        }
    }

    internal class OperationMethodsExtras
    {
        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static Regex r = new Regex(":");

        //retrieves the datetime WITHOUT loading the whole image
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

        public static void ChangeDateTaken(string path)
        {
            Image theImage = new Bitmap(path);
            PropertyItem[] propItems = theImage.PropertyItems;
            Encoding _Encoding = Encoding.UTF8;
            var DataTakenProperty1 = propItems.Where(a => a.Id.ToString("x") == "9004").FirstOrDefault();
            var DataTakenProperty2 = propItems.Where(a => a.Id.ToString("x") == "9003").FirstOrDefault();
            string originalDateString = _Encoding.GetString(DataTakenProperty1.Value);
            originalDateString = originalDateString.Remove(originalDateString.Length - 1);
            //DateTime originalDate = DateTime.ParseExact(originalDateString, "yyyy:MM:dd HH:mm:ss", null);
            //originalDate = originalDate.AddHours(-3);

            DateTime originalDateNew = new DateTime(2023, 7, 21, 21, 45, 8);


            DataTakenProperty1.Value = _Encoding.GetBytes(originalDateNew.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
            DataTakenProperty2.Value = _Encoding.GetBytes(originalDateNew.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
            theImage.SetPropertyItem(DataTakenProperty1);
            theImage.SetPropertyItem(DataTakenProperty2);
            string new_path = Path.GetDirectoryName(path) + "\\x" + System.IO.Path.GetFileName(path);
            theImage.Save(new_path);
            theImage.Dispose();
        }

        public static void AutoRenameFiles(string folderName, int startCounth)
        {
            List<string> fileListByCreationTime = GetFilesByCreationTime(folderName);

            int cntNumber = startCounth;
            foreach (string oldFileName in fileListByCreationTime)
            {
                string newFileName = $"{folderName}\\BW{cntNumber++}_29x45cm_InkOnPaper.jpg"; //BW101_29x45cm_InkOnPaper
                Console.WriteLine($"Old: {oldFileName}, New: {newFileName}");
                File.Move(oldFileName, newFileName);
            }
        }

        public static List<string> GetFilesByCreationTime(string folderName)
        {
            DirectoryInfo directory = new DirectoryInfo(folderName);
            List<string> fileListByCreationTime = directory.GetFiles().OrderBy(x => x.CreationTime.Ticks)
                                                  .Select(x => x.FullName)
                                                  .ToList();
            Console.WriteLine("end");
            return fileListByCreationTime;
        }

        public static void RenameDrawingSheetFile(string oldFileName)
        {
            string newFileName = oldFileName.Replace("InkOnPaper", "InkOnDrawingSheet");

            Console.WriteLine($"Old: {oldFileName}, New: {newFileName}");
            File.Move(oldFileName, newFileName);
        }
    }
}
