using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class AccountBO
    {
        internal AccountBO()
        {
        }

        public Account getById(int accountNo)
        {
           try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Accounts.SingleOrDefault(a => a.AccountNumber == accountNo);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public List<Account> getAll()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Accounts.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Account> getByCustomerId(int customerId)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Accounts.Where(a => a.CustomerID == customerId).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string getAccountTypeByNo(int accountNumber)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    string accountType = (from t in db.Accounts
                                         where t.AccountNumber == accountNumber
                                         select t.AccountType).FirstOrDefault();
                    return accountType;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        
    }
}