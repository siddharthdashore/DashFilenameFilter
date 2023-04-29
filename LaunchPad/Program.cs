using ReimbursementBillFilter;
using System;

namespace LaunchPad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string folderPath = "C:\\Users\\siddh\\OneDrive\\Desktop\\Scans\\final";
            ReimbursementBillFilterClass.WriteFolderFileDetails(ReimbursementBillFilterClass.FetchFolderFileDetails(folderPath));
            _ = Console.ReadLine();
        }
    }
}
