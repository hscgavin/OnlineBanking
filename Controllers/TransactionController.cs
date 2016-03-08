using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TransactionController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /Transaction/

        public ActionResult Index(string searchStringCustName, string searchStringAcctType)
        {   
            /*
            var transactions = db.Transactions.Include(t => t.Account).Include(t => t.Account1).Include(t => t.TransactionType1);
            return View(transactions.ToList());
             */

            /*
            var transactions = from t in db.Transactions
                           select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(s => s.Account.Customer.CustomerName.Contains(searchString));
            }

            return View(transactions);
             */

            var AcctTypeLst = new List<string>();

            var AcctTypeQry = from a in db.Accounts
                           orderby a.AccountType
                           where (a.Customer.CustomerName.Contains(searchStringCustName))
                           select a.AccountType;
            AcctTypeLst.AddRange(AcctTypeQry.Distinct());
            ViewBag.acctType = new SelectList(AcctTypeLst);

            var transactions = from t in db.Transactions
                               select t;

            if (!String.IsNullOrEmpty(searchStringCustName))
            {
                transactions = transactions.Where(s => s.Account.Customer.CustomerName.Contains(searchStringCustName));
            }

            if (string.IsNullOrEmpty(searchStringAcctType))
                return View(transactions);
            else
            {
                return View(transactions.Where(x => x.Account.AccountType == searchStringAcctType));
            } 

        }

        //
        // GET: /Transaction/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}