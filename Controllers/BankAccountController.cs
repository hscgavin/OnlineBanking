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
    public class BankAccountController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /BankAccount/
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchString)
        {
            /*
            var accounts = db.Accounts.Include(a => a.Customer);
            return View(accounts.ToList());
             */

            var accounts = from a in db.Accounts
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.Customer.CustomerName.Contains(searchString));
            }

            return View(accounts);
        }

        //
        // GET: /BankAccount/
        [Authorize(Roles = "Admin")]
        public ActionResult SearchIndex(string searchString)
        {
            var accounts = from a in db.Accounts
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.Customer.CustomerName.Contains(searchString));
            }

            return View(accounts);
        }


        //
        // GET: /BankAccount/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        //
        // GET: /BankAccount/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");

            /* drop down list for account types. */
            var AccountTypes = from AccountTypes data in Enum.GetValues(typeof(AccountTypes))
                               select new { id = data, AccountTypeDesc = data.ToString() };
            ViewData["AccountType"] = new SelectList(AccountTypes, "ID", "AccountTypeDesc", null);

            return View();
        }

        //
        // POST: /BankAccount/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Account account, Transaction transaction)
        {
            bool execAcctSucceeded = false;
            bool execTransSucceeded = false;

            if (ModelState.IsValid)
            {
                //db.Accounts.Add(account);
                try
                {
                    Account acct = new Account();

                    acct.AccountType = account.AccountType;
                    acct.Balance = account.Balance;
                    acct.CustomerID = account.CustomerID;
                    acct.ModifyDate = DateTime.Now;

                    if (ModelState.IsValid && acct != null)
                    {
                        db.Accounts.Add(acct);
                        db.SaveChanges();
                        execAcctSucceeded = true;
                    }

                    Transaction trans = new Transaction();

                    trans.TransactionTypeID = 1;
                    trans.TransactionType = null;

                    Account acct1 = db.Accounts.Single(r => r.CustomerID == account.CustomerID);
                    trans.AccountNumber = acct1.AccountNumber;

                    trans.DestinationAccount = null;
                    trans.Amount = account.Balance;
                    trans.Comment = "Opening Balanace";
                    trans.ModifyDate = DateTime.Now;

                    if (ModelState.IsValid && trans != null)
                    {
                        db.Transactions.Add(trans);
                        db.SaveChanges();
                        execTransSucceeded = true;
                    }
                    
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "The request cannot be executed!");
                    throw new Exception(e.Message);
                }

                if (execAcctSucceeded && execTransSucceeded)
                {
                    //return RedirectToAction("Index", new { Message = TransactionMessageId.ExecTransSuccess });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The request cannot be executed!");
                }
                
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        public enum AccountTypes
        {
            S = 1, C = 2
        }

       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}