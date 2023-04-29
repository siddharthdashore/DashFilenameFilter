using ReimbursementBillFilter;
using System;

namespace LaunchPad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = "C:\\Users\\siddh\\OneDrive\\Desktop\\Scans\\final";
            ReimbursementBillFilterClass.WriteFolderFileDetails(ReimbursementBillFilterClass.FetchFolderFileDetails(folderPath));
            Console.ReadLine();
        }
    }
}
