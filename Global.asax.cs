using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WDTAssignment2NWBA.BusinessLogicLayer;
using WDTAssignment2NWBA.DataAccessLayer;
using WDTAssignment2NWBA.Controllers;

namespace WDTAssignment2NWBA
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static System.Timers.Timer aTimer;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // add code here schedule


            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 10 seconds
            aTimer.Interval = 10000;
            aTimer.Enabled = true;


        }

        // Specify what you want to happen when the Elapsed event is  
        // raised. 
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            BPayBO bPayBusinessObject = new BPayBO();
            List<BillPay> billPays = bPayBusinessObject.GetActive();

            foreach(var b in billPays)
            {
                DateTime ScheduleDate = b.ModifyDate;
                var scheduleHour=ScheduleDate.Hour;
                var scheduleMinitue=ScheduleDate.Minute;
                var date = DateTime.Now;
                //var currentHour = date.Hour;
                //var currentMinute = date.Minute;

                
                if (DateTime.Now.Minute==b.ModifyDate.AddMinutes(2).Minute)
                {
                    TransactionBO transactionBO = new TransactionBO();
                    AccountBO accountBO = new AccountBO();
                    decimal balance = transactionBO.getBalanceByaccountNumber(b.AccountNumber)-b.Amount;
                    string accountType = accountBO.getAccountTypeByNo(b.AccountNumber).ToUpper();
                    int count = transactionBO.getCountByAccountNo(b.AccountNumber);
                    
                    if (count >= 4)
                    {
                        if ((accountType == "C" && balance - (decimal)0.30-b.Amount >= 200) || (accountType == "S" && balance - (decimal)0.30 >= 0))
                        {
                            using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                            {
                                Transaction transaction = transactionBO.CreateTransaction("B", b.AccountNumber, b.Amount);
                                var billPay = db.BillPays.SingleOrDefault(bp => bp.BillPayID == b.BillPayID);
                                billPay.Status = "N";
                                billPay.ModifyDate = DateTime.Now;
                                b.Status = "N";
                                db.SaveChanges();
                                // create a new schedule for next month
                                if (b.Period == "M")
                                {
                                    bPayBusinessObject.CreateBillPay(b.AccountNumber, b.PayeeID, b.Amount, b.ScheduleDate.AddMonths(1), b.Period);
                                }
                                // create a new schedule for next quather
                                if (b.Period == "Q")
                                {
                                    bPayBusinessObject.CreateBillPay(b.AccountNumber, b.PayeeID, b.Amount, b.ScheduleDate.AddMonths(3), b.Period);
                                }
                                // create a new schedule for next year
                                if (b.Period == "Y")
                                {
                                    bPayBusinessObject.CreateBillPay(b.AccountNumber, b.PayeeID, b.Amount, b.ScheduleDate.AddYears(1), b.Period);
                                }
                                Transaction fee = new Transaction();
                                fee.TransactionTypeID = 4;
                                fee.AccountNumber = b.AccountNumber;
                                fee.TransactionType = "S";
                                fee.DestinationAccount = null;
                                fee.Amount = (decimal)0.30;
                                fee.Comment = "BPay Fee";
                                fee.ModifyDate = DateTime.Now;
                                db.Transactions.Add(fee);
                                db.SaveChanges();
                                db.Accounts.FirstOrDefault(a => a.AccountNumber == b.AccountNumber).Balance = transactionBO.getBalanceByaccountNumber(b.AccountNumber);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        if ((accountType == "C" && balance -b.Amount>= 200) || (accountType == "S" && balance-b.Amount>= 0))
                        {
                            using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                            {
                                Transaction transaction = transactionBO.CreateTransaction("B", b.PayeeID, b.Amount);
                                var billPay = db.BillPays.SingleOrDefault(bp => bp.BillPayID == b.BillPayID);
                                billPay.Status = "N";
                                db.SaveChanges();
                                // create a new schedule for next month
                                if (b.Period == "M")
                                {
                                    bPayBusinessObject.CreateBillPay(b.AccountNumber, b.PayeeID, b.Amount, b.ScheduleDate.AddMonths(1), b.Period);
                                }
                                // create a new schedule for next quather
                                if (b.Period == "Q")
                                {
                                    bPayBusinessObject.CreateBillPay(b.AccountNumber, b.PayeeID, b.Amount, b.ScheduleDate.AddMonths(3), b.Period);
                                }
                                // create a new schedule for next year
                                if (b.Period == "Y")
                                {
                                    bPayBusinessObject.CreateBillPay(b.AccountNumber, b.PayeeID, b.Amount, b.ScheduleDate.AddYears(1), b.Period);
                                }
                                Transaction fee = new Transaction();
                                fee.TransactionTypeID = 4;
                                fee.AccountNumber = b.AccountNumber;
                                fee.TransactionType = "S";
                                fee.DestinationAccount = null;
                                fee.Amount = (decimal)0;
                                fee.Comment = "Free BPay";
                                fee.ModifyDate = DateTime.Now;
                                db.Transactions.Add(fee);
                                db.SaveChanges();

                                db.Accounts.FirstOrDefault(a => a.AccountNumber == b.AccountNumber).Balance = transactionBO.getBalanceByaccountNumber(b.AccountNumber);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            Server.ClearError();

            int statusCode = 0;

            if (lastError.GetType() == typeof(HttpException))
            {
                statusCode = ((HttpException)lastError).GetHttpCode();
            }
            else
            {
                statusCode = 500;  //internal server error if error status code is 500
            }

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("statusCode", statusCode);
            routeData.Values.Add("exception", lastError);

            IController controller = new ErrorController();

            RequestContext requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);

            controller.Execute(requestContext);
            Response.End();
        }
    }
}
       