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
    public class TEST_TRANSController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /TEST_TRANS/

        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Account).Include(t => t.Account1).Include(t => t.TransactionType1);
            return View(transactions.ToList());
        }

        //
        // GET: /TEST_TRANS/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /TEST_TRANS/Create

        public ActionResult Create()
        {
            ViewBag.AccountNumber = new SelectList(db.Accounts, "AccountNumber", "AccountType");
            ViewBag.DestinationAccount = new SelectList(db.Accounts, "AccountNumber", "AccountType");
            ViewBag.TransactionTypeID = new SelectList(db.TransactionTypes, "TransactionTypeID", "TransactionType1");
            return View();
        }

        //
        // POST: /TEST_TRANS/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountNumber = new SelectList(db.Accounts, "AccountNumber", "AccountType", transaction.AccountNumber);
            ViewBag.DestinationAccount = new SelectList(db.Accounts, "AccountNumber", "AccountType", transaction.DestinationAccount);
            ViewBag.TransactionTypeID = new SelectList(db.TransactionTypes, "TransactionTypeID", "TransactionType1", transaction.TransactionTypeID);
            return View(transaction);
        }

        //
        // GET: /TEST_TRANS/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountNumber = new SelectList(db.Accounts, "AccountNumber", "AccountType", transaction.AccountNumber);
            ViewBag.DestinationAccount = new SelectList(db.Accounts, "AccountNumber", "AccountType", transaction.DestinationAccount);
            ViewBag.TransactionTypeID = new SelectList(db.TransactionTypes, "TransactionTypeID", "TransactionType1", transaction.TransactionTypeID);
            return View(transaction);
        }

        //
        // POST: /TEST_TRANS/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountNumber = new SelectList(db.Accounts, "AccountNumber", "AccountType", transaction.AccountNumber);
            ViewBag.DestinationAccount = new SelectList(db.Accounts, "AccountNumber", "AccountType", transaction.DestinationAccount);
            ViewBag.TransactionTypeID = new SelectList(db.TransactionTypes, "TransactionTypeID", "TransactionType1", transaction.TransactionTypeID);
            return View(transaction);
        }

        //
        // GET: /TEST_TRANS/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /TEST_TRANS/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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