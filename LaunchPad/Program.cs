using ReimbursementBillFilter;
using ReimbursementBillFilterModels;
using System;

namespace LaunchPad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string folderPath = "C:\\Users\\siddh\\OneDrive\\Desktop\\Scans\\final";
            _ = ReimbursementBillFilterClass.WriteFolderFileDetails(ReimbursementBillFilterClass.FetchFolderFileDetails(folderPath, CurrencyType.INR));
            _ = Console.ReadLine();
        }
    }
}
