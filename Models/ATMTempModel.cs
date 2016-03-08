using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDTAssignment2NWBA.Models
{
    public class ATMTempModel
    {
        public int TransactionID { get; set; }
        public char  TransactionType { get; set; }
        public List<string> FromAcounts { get; set; }
        public List<string> ToAcounts { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime  ModifyDate { get; set; }
    }
}