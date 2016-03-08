using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.Models
{
    public class ATMModel :DbContext
    {
    }

    public class ExecuteTransaction
    {
        public int TransactionTypeID { get; set; }
        public int AccountNumber { get; set; }
        public Nullable<int> DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public string TransactionType { get; set; }
    }
}