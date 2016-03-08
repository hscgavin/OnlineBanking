using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.Models
{
    public class BPayModel
    {
        public BPayModel()
        {
            Periods = new List<SelectListItem> { new SelectListItem { Text = "Monthly", Value = "M" }, new SelectListItem { Text = "Quarterly", Value = "Q" }, new SelectListItem { Text = "Annually", Value = "Y" }, new SelectListItem { Text = "Once off", Value = "S" } };
        }

        public BillPay BillPay { get; set; }
        public List<BillPay> BillPays { get; set; }
        public List<Payee> PayeeList { get; set; }

        //public List<Account> Accounts { get; set; }

        //public List<Payee> Payees { get; set; }

        //public enum Period { Monthly= M, Quarterly=Q, Annually=Y, Once Off= S };

        public IEnumerable<SelectListItem> Accounts;
        public IEnumerable<SelectListItem> Payees;
        public IEnumerable<SelectListItem> Periods;
            
        
    }
}