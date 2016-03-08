using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class CustomerBO
    {
        internal CustomerBO()
        {
        }

        public Customer UpdateCustomer(int customerId, String customerName, string TFN, string address, string city, string state, string postCode, string phone)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    Customer customer = db.Customers.SingleOrDefault(c => c.CustomerID == customerId);
                    customer.CustomerName = customerName;
                    customer.TFN = TFN;
                    customer.Address = address;
                    customer.City = city;
                    customer.State = state;
                    customer.PostCode = postCode;
                    customer.Phone = phone;
                    customer.ModifyDate = DateTime.Now;
                    db.SaveChanges();
                    return customer;
                }
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public Customer GetById(int customerId)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Customers.SingleOrDefault(c => c.CustomerID == customerId);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public Customer GetByIdInclusive(int customerId)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.Customers.Include("Login").Include("Account").Include("BillPay").SingleOrDefault(c => c.CustomerID == customerId);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

   
}