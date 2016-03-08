using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.Models
{
    public class MyStatementModel
    {
        public IEnumerable<Account> Accounts { get; set; }
        public PagedList<Transaction> Transactions { get; set; }
        
        
    }
}



