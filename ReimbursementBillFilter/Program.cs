using ReimbursementBillFilterModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ReimbursementBillFilter
{
    public class ReimbursementBillFilterClass
    {
        /// <summary>
        /// Filename input sample: 327_09Apr23_Medical_Bill.jpeg
        /// Filename output sample: Date: 09-04-2023, Item: Medical Bill, Amount: Rs 327/-, Sum: Rs 327/-
        /// </summary>
        /// <param name="data"></param>
        public static BaseException WriteFolderFileDetails(ReimbursementBillData data)
        {
            BaseException baseException = null;
            try
            {
                if (data.ExceptionObj != null)
                {
                    throw data.ExceptionObj;
                }

                data.FilesEntry = data.FilesEntry.OrderBy(x => x.Date).ToList();
                foreach (ReimbursementBillModel item in data.FilesEntry)
                {
                    Console.WriteLine($"Date: {item.Date:dd/MM/yyyy}, Item: {item.Item}, Amount: Rs {item.Amount:N0}/-");
                }

                Console.WriteLine("**********************************************");
                Console.WriteLine($"Total: Rs {data.SumStr}");
                Console.WriteLine("**********************************************");
            }
            catch (Exception ex)
            {
                baseException = new BaseException()
                {
                    ExceptionObj = ex
                };
                Console.WriteLine($"WriteFolderFileDetails failed with error: {ex.Message}");
            }
            return baseException;
        }

        /// <summary>
        /// Return information in ReimbursementBillData
        /// </summary>
        /// <param name="folderPath"></param>
        public static ReimbursementBillData FetchFolderFileDetails(string folderPath)
        {
            ReimbursementBillData reimbursementBillData = new ReimbursementBillData
            {
                FilesEntry = new List<ReimbursementBillModel>()
            };

            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                FileInfo[] files = di.GetFiles("*.*");

                foreach (FileInfo file in files)
                {
                    try
                    {
                        string filename = Path.GetFileNameWithoutExtension(file.Name);
                        string[] filenameSplit = filename.Split('_');
                        if (filenameSplit.Length < 3)
                        {
                            Console.WriteLine($"Bad filename format found. Skipping for '{file.Name}'");
                            continue;
                        }

                        int amount = Convert.ToInt32(filenameSplit[0]);
                        reimbursementBillData.FilesEntry.Add(new ReimbursementBillModel()
                        {
                            Amount = amount,
                            Date = Convert.ToDateTime(filenameSplit[1], CultureInfo.InvariantCulture),
                            Item = filename.Replace($"{filenameSplit[0]}_{filenameSplit[1]}_", "").Replace("_", " ")
                        });
                        reimbursementBillData.IncSum += amount;
                        reimbursementBillData.SumStr = $"Rs {reimbursementBillData.IncSum:N0}/-";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Bad filename format found. Skipping for '{file.Name}'. Error: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                reimbursementBillData.ExceptionObj = ex;
                Console.WriteLine($"FetchFolderFileDetails failed with error: {ex.Message}");
            }
            return reimbursementBillData;
        }
    }
}