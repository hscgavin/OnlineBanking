using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WDTAssignment2NWBA.DataAccessLayer;

namespace WDTAssignment2NWBA.BusinessLogicLayer
{
    public class BPayBO
    {

        public BillPay GetById(int billPayId)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.BillPays.SingleOrDefault(c => c.BillPayID == billPayId);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BillPay> GetAll()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.BillPays.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BillPay> GetAllIncludePayee()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.BillPays.Include("Payee").ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BillPay> GetAllIncludePayeeById(int id)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    return db.BillPays.Include("Payee").Include("Account").Where(b => b.Account.CustomerID == id).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BillPay> GetActive()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    var query = from t in db.BillPays
                                where t.ScheduleDate > DateTime.Now && t.Status == "Y"
                                select t;
                    return query.ToList();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BillPay CreateBillPay(int accountNo, int payeeId, decimal amount, DateTime scheduleDate, string period)
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    BillPay billPay = new BillPay();
                    billPay.AccountNumber = accountNo;
                    billPay.PayeeID = payeeId;
                    billPay.Amount = amount;
                    billPay.ScheduleDate = scheduleDate;
                    billPay.Period = period;
                    billPay.Status = "Y";
                    billPay.ModifyDate = DateTime.Now;
                    db.BillPays.Add(billPay);
                    db.SaveChanges();
                    return billPay;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}