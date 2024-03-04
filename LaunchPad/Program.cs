using ReimbursementBillFilter;
using ReimbursementBillFilterModels;
using System;

namespace LaunchPad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string folderPath = "D:\\Shrutika_Claim_10Apr23\\Bills+Invoices";
            _ = ReimbursementBillFilterClass.WriteFolderFileDetails(ReimbursementBillFilterClass.FetchFolderFileDetails(folderPath, CurrencyType.INR));
            _ = Console.ReadLine();
        }
    }
}
