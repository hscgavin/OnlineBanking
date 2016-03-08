using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.Models
{
    public class StatementModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}