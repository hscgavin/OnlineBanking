using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class LoginBO
    {
        internal LoginBO()
        {
        }

        public Login UpdateLogin(int userId, int CustomerId, string userName, string Password)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    Login login = db.Logins.SingleOrDefault(l => l.UserID == userId);
                    login.ModifyDate = DateTime.Now;
                    login.UserName = userName;
                    login.CustomerID = CustomerId;
                    db.SaveChanges();
                    return login;
                }
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public Login GetLoginById(int userId)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Logins.SingleOrDefault(l => l.UserID == userId);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public int GetCustomerIdByUserId(int userId)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Logins.SingleOrDefault(l => l.UserID == userId).CustomerID;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

    }
}