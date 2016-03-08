using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.BusinessLogicLayer;
using WDTAssignment2NWBA.DataAccessLayer;
using WDTAssignment2NWBA.Filters;
using WDTAssignment2NWBA.Models;
using WebMatrix.WebData;

namespace WDTAssignment2NWBA.Controllers
{
    [InitializeSimpleMembership]
    [Authorize(Roles = "Admin")]
    public class ScheduledPaymentsController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /ScheduledPayments/

        public ActionResult Index(string searchStringCustName)
        {
            /*
            var billpays = db.BillPays.Include(b => b.Account).Include(b => b.Payee);
            return View(billpays.ToList());
             */
            BPayBO bPayBo = new BPayBO();
            PayeeBO payeeBo = new PayeeBO();
            AccountBO accountBo = new AccountBO();
            var model = new BPayModel();

            List<BillPay> scheduledPayments = (from bp in db.BillPays
                                    select bp).ToList();

            if (!String.IsNullOrEmpty(searchStringCustName))
            {
                scheduledPayments = scheduledPayments.Where(s => s.Account.Customer.CustomerName.Contains(searchStringCustName)).ToList();
            }

            return View(scheduledPayments);

        }

        //
        // GET: /ScheduledPayments/Details/5

        public ActionResult Details(int id = 0)
        {
            BillPay billpay = db.BillPays.Find(id);
            if (billpay == null)
            {
                return HttpNotFound();
            }
            return View(billpay);
        }

        //
        // GET: /ShceduledPayments/
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BillPay billpay = db.BillPays.Find(id);
            if (billpay == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountNumber = new SelectList(db.Accounts, "AccountNumber", "AccountType", billpay.AccountNumber);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "PayeeName", billpay.PayeeID);

            return View(billpay);
        }


        //
        // POST: /ScheduledPayments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BillPay billpay)
        {
            BillPay bpay = new BillPay();

            bpay.AccountNumber = billpay.AccountNumber;
            bpay.PayeeID = billpay.PayeeID;
            bpay.Amount = billpay.Amount;
            bpay.ScheduleDate = billpay.ScheduleDate;
            bpay.Period = billpay.Period;
            bpay.Status = billpay.Status;
            bpay.StopDate = DateTime.Now;
            bpay.StoppedBy = HttpContext.User.Identity.Name;
            bpay.ModifyDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(billpay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }

            //return RedirectToAction("Index", "ScheduledPayments");
            return View(billpay);

        }

        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
