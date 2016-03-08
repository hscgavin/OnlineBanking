using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class TransactionBO
    {
        internal TransactionBO()
        {
        }

        public const string Type_Saving = "S";
        public const string Type_Checking = "C";

        public List<Transaction> GetAll()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Transactions.ToList();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public int getCountByAccountNo(int accNo)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    var query = (from t in db.Transactions
                                 where t.AccountNumber == accNo //&& t.Comment != "View statement Fee"
                                 select t).ToList();
                    return query.Count;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public decimal getBalanceByaccountNumber(int accountNo)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    decimal balance = 0;
                    List<Transaction> transctions= db.Transactions.Where(t=>t.AccountNumber==accountNo).ToList();
                    foreach(var t in transctions)
                    {
                        if (t.TransactionTypeID == 1 || t.DestinationAccount!=null)
                        {
                            balance += t.Amount;
                        }
                        else
                            balance -= t.Amount;

                    }
                    return balance;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Transaction CreateTransaction(string transactionType, int accountNumber, decimal amount)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    Transaction transaction = new Transaction();
                    transaction.TransactionTypeID = 5;
                    transaction.TransactionType = transactionType;
                    transaction.AccountNumber=accountNumber;
                    transaction.Amount = amount;
                    transaction.ModifyDate = DateTime.Now;
                    transaction.Comment = "Bill Pay";
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return transaction;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
        


    }
}