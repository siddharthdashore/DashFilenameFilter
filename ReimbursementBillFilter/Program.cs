using System;
using System.IO;

namespace ReimenrsementBillFilter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = "C:\\Users\\siddh\\OneDrive\\Desktop\\Scans\\final";
            GetFolderFileDetails(folderPath);
        }

        /// <summary>
        /// Filename input sample: 327_09Apr23_Medical_Bill.jpeg
        /// Filename output sample: Date: 09-Apr-2023, Item: Medical Bill, Amount: Rs 327/-, Sum: Rs 327/-
        /// </summary>
        /// <param name="folderPath"></param>
        public static void GetFolderFileDetails(string folderPath)
        {

            DirectoryInfo di = new DirectoryInfo(folderPath);
            FileInfo[] files = di.GetFiles("*.*");
            int sumInt = 0;
            string sumStr = string.Empty;

            foreach (FileInfo file in files)
            {
                string filename = Path.GetFileNameWithoutExtension(file.Name);
                string[] filenameSplit = filename.Split('_');
                int amount = Convert.ToInt32(filenameSplit[0]);
                sumInt += amount;
                sumStr = $"Rs {sumInt.ToString("N0")}/-";
                string item = filename.Replace($"{filenameSplit[0]}_{filenameSplit[1]}_", "").Replace("_", " ");

                string data = filenameSplit[1];
                data = $"{data.Substring(0, 2)}-{data.Substring(2, 3)}-20{data.Substring(5, 2)}";
                Console.WriteLine($"Date: {data}, Item: {item}, Amount: Rs {amount.ToString("N0")}/-, Sum: {sumStr}");
            }
            Console.WriteLine("**********************************************");
            Console.WriteLine($"Total: Rs {sumStr}");
            Console.WriteLine("**********************************************");
            Console.ReadLine();
        }
    }
}