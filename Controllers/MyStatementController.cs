using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.DataAccessLayer;
using WDTAssignment2NWBA.Filters;
using WDTAssignment2NWBA.Models;
using WebMatrix.WebData;
using PagedList;
using WDTAssignment2NWBA.BusinessLogicLayer;

namespace WDTAssignment2NWBA.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "User")]
    public class MyStatementController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /MyStatements/
        public ActionResult Index(string currentFilter, string accountType, int? page, string sortOrder, string emp)
        {

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var accLst = new List<string>();

            var accQry = from t in db.Accounts
                         where t.CustomerID == WebSecurity.CurrentUserId
                         select t.AccountType;
            accLst.AddRange(accQry.Distinct());
            ViewBag.accountType = new SelectList(accLst);

            if (accountType != null)
            {
                page = 1;
            }
            else
            {
                accountType = currentFilter;
            }

            ViewBag.CurrentFilter = accountType;


            var accountInfo = from acc in db.Accounts
                              where acc.CustomerID == WebSecurity.CurrentUserId
                              select acc;

            accountInfo = accountInfo.Where(x => x.AccountType == accountType);


            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            var transInfo = from trans in db.Transactions
                            select trans;

            foreach (Account row in accountInfo)
            {
                transInfo = from trans in db.Transactions
                            where (trans.AccountNumber == row.AccountNumber)
                            || (trans.DestinationAccount == row.AccountNumber)
                            select trans;
            }

            switch (sortOrder)
            {
                //case "Name_desc":
                //    transInfo = transInfo.OrderByDescending(t => t.DestinationAccount);
                //    break;
                //case "Date":
                //    transInfo = transInfo.OrderBy(t => t.ModifyDate);
                //    break;
                //case "Date_desc":
                //    transInfo = transInfo.OrderByDescending(t => t.ModifyDate);
                //    break;
                default:
                    transInfo = transInfo.OrderBy(t => t.TransactionID);
                    break;
            }

            var model = new MyStatementModel
            {
                Accounts = accountInfo.ToList(),
                Transactions = (PagedList<Transaction>)transInfo.ToPagedList(pageNumber, pageSize)

            };


            if (string.IsNullOrEmpty(accountType))
                return View(model);
            else
            {
                return View("MyStatementView", model);
            } 
        }


        [HttpPost]       
        public ActionResult Index(string currentFilter, string accountType, int? page, string sortOrder)
        {
            TransactionBO transactionBO = new TransactionBO();
            int count;
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var accLst = new List<string>();

            var accQry = from t in db.Accounts
                         where t.CustomerID == WebSecurity.CurrentUserId
                         select t.AccountType;
            accLst.AddRange(accQry.Distinct());
            ViewBag.accountType = new SelectList(accLst);

            if (accountType != null)
            {
                page = 1;
            }
            else
            {
                accountType = currentFilter;
            }

            ViewBag.CurrentFilter = accountType;


            var accountInfo = from acc in db.Accounts
                              where acc.CustomerID == WebSecurity.CurrentUserId
                              select acc;

            accountInfo = accountInfo.Where(x => x.AccountType == accountType);


            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            var transInfo = from trans in db.Transactions
                            select trans;

            foreach (Account row in accountInfo)
            {
                transInfo = from trans in db.Transactions
                            where (trans.AccountNumber == row.AccountNumber)
                            || (trans.DestinationAccount == row.AccountNumber)
                            select trans;
            }

            switch (sortOrder)
            {
                //case "Name_desc":
                //    transInfo = transInfo.OrderByDescending(t => t.DestinationAccount);
                //    break;
                //case "Date":
                //    transInfo = transInfo.OrderBy(t => t.ModifyDate);
                //    break;
                //case "Date_desc":
                //    transInfo = transInfo.OrderByDescending(t => t.ModifyDate);
                //    break;
                default:
                    transInfo = transInfo.OrderBy(t => t.TransactionID);
                    break;
            }

            var model = new MyStatementModel
            {
                Accounts = accountInfo.ToList(),
                Transactions = (PagedList<Transaction>)transInfo.ToPagedList(pageNumber, pageSize)

            };

            count = transactionBO.getCountByAccountNo(model.Accounts.First().AccountNumber);


            if (string.IsNullOrEmpty(accountType)
                    || ((model.Accounts.First().AccountType == "C") && ((double)model.Accounts.First().Balance < 200.20))
                    || ((model.Accounts.First().AccountType == "S") && ((double)model.Accounts.First().Balance < 0.20)))
            {

                return View(model);
            }
            else
            {
                if (count > 3)
                {
                    Transaction fee = new Transaction();
                    fee.TransactionTypeID = 4;
                    fee.AccountNumber = model.Accounts.First().AccountNumber;
                    fee.DestinationAccount = null;
                    fee.Amount = (decimal)0.20;
                    fee.Comment = "View statement Fee";
                    fee.ModifyDate = DateTime.Now;
                    db.Transactions.Add(fee);
                    db.SaveChanges();
                }
                else
                {
                    Transaction fee = new Transaction();
                    fee.TransactionTypeID = 4;
                    fee.AccountNumber = model.Accounts.First().AccountNumber;
                    fee.DestinationAccount = null;
                    fee.Amount = (decimal)0.00;
                    fee.Comment = "Free View statement";
                    fee.ModifyDate = DateTime.Now;
                    db.Transactions.Add(fee);
                    db.SaveChanges();
                }


                transInfo = from trans in db.Transactions
                            select trans;

                foreach (Account row in accountInfo)
                {
                    transInfo = from trans in db.Transactions
                                where (trans.AccountNumber == row.AccountNumber)
                                || (trans.DestinationAccount == row.AccountNumber)
                                select trans;
                }

                switch (sortOrder)
                {

                    default:
                        transInfo = transInfo.OrderBy(t => t.TransactionID);
                        break;
                }

                model.Transactions = (PagedList<Transaction>)transInfo.ToPagedList(pageNumber, pageSize);

                model.Accounts.FirstOrDefault(a => a.AccountNumber == model.Accounts.First().AccountNumber).Balance = transactionBO.getBalanceByaccountNumber(model.Accounts.First().AccountNumber);
                db.SaveChanges();
                return View("MyStatementView", model);
            } 

        }

     


        //
        // GET: /MyStatements/Details/5

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
        // GET: /MyStatements/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName");
            return View();
        }

        //
        // POST: /MyStatements/Create

        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        //
        // GET: /MyStatements/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        //
        // POST: /MyStatements/Edit/5

        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        //
        // GET: /MyStatements/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        //
        // POST: /MyStatements/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}