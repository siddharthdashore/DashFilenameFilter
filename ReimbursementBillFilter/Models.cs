using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReimbursementBillFilterModels
{
    public class ReimbursementBillData : BaseException
    {
        public List<ReimbursementBillModel> FilesEntry { get; set; }

        public int IncSum { get; set; }

        public string SumStr { get; set; }

        public CurrencyType Currency { get; set; }
    }

    public class ReimbursementBillModel
    {
        public DateTime Date { get; set; }

        public string Item { get; set; }

        public int Amount { get; set; }
    }

    public enum CurrencyType
    {
        [Description("Rs ")]
        INR,

        [Description("$")]
        USD
    }

    public class BaseException
    {
        //public int ErrorCode { get; set; }

        //public string ExceptionMessage { get; set; }

        public Exception ExceptionObj { get; set; }
    }
}
