using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.BusinessLogicLayer;
using WDTAssignment2NWBA.DataAccessLayer;
using WDTAssignment2NWBA.Models;

namespace WDTAssignment2NWBA.Controllers
{
    [Authorize(Roles = "User")]
    public class ATMController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /ATM/

        public ActionResult Index(TransactionMessageId? message)
        {
            ViewBag.StatusMessage =
                message == TransactionMessageId.ExecTransSuccess ? "Your request has been executed successfully!" : "";
            PopulateTransactionTypeDropDownList();
            PopulateSourceAcctDropDownList();
            PopulateDestAcctDropDownList();
          
            return View();
        }

        //
        // POST: /ATM/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ExecuteTransaction model)
        {
            bool execTransSucceeded = false;
            TransactionBO transactionBO = new TransactionBO();
            AccountBO accountBO=new AccountBO();
            decimal balance = transactionBO.getBalanceByaccountNumber(model.AccountNumber);
            Transaction transaction = new Transaction();
            if (model.TransactionTypeID != 1)
            {
                balance -= model.Amount;
            }
            else
            {
                balance += model.Amount;
            }
            
            string accountType = accountBO.getAccountTypeByNo(model.AccountNumber).ToUpper();
            
            //try
            //{
                transaction.TransactionTypeID = model.TransactionTypeID;
                /* Transaction Method Check:
                 * Copy's AccountNumber to DestinationAccount if the transaction is either
                 * deposit or withdrawal
                 */
                if (transaction.TransactionTypeID == 1 || transaction.TransactionTypeID == 2)
                {
                    transaction.AccountNumber = model.AccountNumber;
                    transaction.DestinationAccount = null;
                }
                else if (transaction.TransactionTypeID == 3) //IF THE TRANSACTION METHOD IS A TRANSFER METHOD
                {
                    transaction.AccountNumber = model.AccountNumber;
                    transaction.DestinationAccount = model.DestinationAccount;
                }
                transaction.Amount = model.Amount;
                transaction.Comment = model.Comment;
                transaction.ModifyDate = DateTime.Now;

                
                if (ModelState.IsValid)
                {
                    int count = transactionBO.getCountByAccountNo(model.AccountNumber);
                    if (count >= 4)
                    {
                        if ((accountType == "C" && balance - (decimal)0.20 >= 200) || (accountType == "S" && balance - (decimal)0.20 >= 0))
                        {
                            db.Transactions.Add(transaction);
                            execTransSucceeded = true;

                            Transaction fee = new Transaction();
                            fee.TransactionTypeID = 4;
                            fee.AccountNumber = model.AccountNumber;
                            fee.DestinationAccount = null;
                            fee.Amount = (decimal)0.20;
                            fee.Comment = "Deposit Fee";
                            fee.ModifyDate = DateTime.Now;
                            db.Transactions.Add(fee);
                            db.SaveChanges();
                            
                        }

                    }
                    else
                    {
                        if ((accountType == "C" && balance >= 200) || (accountType == "S" && balance >= 0))
                        {
                            db.Transactions.Add(transaction);
                            db.SaveChanges();
                            execTransSucceeded = true;
                        }
                    }

                    db.Accounts.FirstOrDefault(a => a.AccountNumber == model.AccountNumber).Balance = transactionBO.getBalanceByaccountNumber(model.AccountNumber);
                    db.SaveChanges();

                }
                
                

            //}
            //catch (Exception)
            //{
            //    execTransSucceeded = false;
            //}

            if (execTransSucceeded)
            {
                return RedirectToAction("Index", new { Message = TransactionMessageId.ExecTransSuccess });
            }
            else
            {
                ModelState.AddModelError("", "The request cannot be executed!");
            }

            PopulateTransactionTypeDropDownList();
            PopulateSourceAcctDropDownList();
            PopulateDestAcctDropDownList();

            return View(transaction);
        }

        private void PopulateTransactionTypeDropDownList(object selectedTransactionType = null) 
        { 
            var transTypeQuery = from t in db.TransactionTypes 
                                 where t.TransactionCategory.TransactionCategory1.Equals("A")
                                 orderby t.TransactionTypeID 
                                 select t;
            ViewBag.TransactionTypeID = new SelectList(transTypeQuery, "TransactionTypeID", "TransactionTypeDesc", selectedTransactionType); 
        }

        private void PopulateSourceAcctDropDownList(object selectedSourceAcct = null)
        {
            var SourceAcctQuery = from a in db.Accounts 
                                  join c in db.Customers 
                                  on a.CustomerID equals c.CustomerID
                                  join l in db.Logins
                                  on c.CustomerID equals l.CustomerID
                                  where l.UserName.Equals(HttpContext.User.Identity.Name)
                                  orderby a.AccountNumber
                                  select a;
            ViewBag.AccountNumber = new SelectList(SourceAcctQuery, "AccountNumber", "AccountTypeNum", selectedSourceAcct);
        }

        private void PopulateDestAcctDropDownList(object selectedDestAcct = null)
        {
            var DestAcctQuery = from a in db.Accounts
                                join c in db.Customers
                                on a.CustomerID equals c.CustomerID
                                join l in db.Logins
                                on c.CustomerID equals l.CustomerID
                                //where !(l.UserName.Equals(HttpContext.User.Identity.Name) && a.AccountNumber.Equals(ViewBag.AccountNumber))
                                orderby a.AccountNumber
                                select a;
            ViewBag.DestinationAccount = new SelectList(DestAcctQuery, "AccountNumber", "AccountTypeNum", selectedDestAcct);
        }

        public enum TransactionMessageId
        {
            ExecTransSuccess
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}