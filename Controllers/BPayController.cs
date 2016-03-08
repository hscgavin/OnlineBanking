using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "User")]
    public class BPayController : Controller
    {
        //
        // GET: /BPay/

        public ActionResult Index()
        {
            BPayBO bPayBo = new BPayBO();
            PayeeBO payeeBo = new PayeeBO();
            AccountBO accountBo = new AccountBO();
            var model = new BPayModel();
            model.Payees = payeeBo.GetAll().Select(p => new SelectListItem
            {
                Value = p.PayeeID.ToString(),
                Text = p.PayeeID+"--"+p.PayeeName
            });
            model.Accounts = accountBo.getByCustomerId(WebSecurity.CurrentUserId).Select(a => new SelectListItem
            {
                Value = a.AccountNumber.ToString(),
                Text = a.AccountType+": "+a.AccountNumber.ToString()+"--balance: $"+a.Balance
            });
           
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BPayModel model)
        {
            PayeeBO payeeBo = new PayeeBO();
            AccountBO accountBo = new AccountBO();
            model.Payees = payeeBo.GetAll().Select(p => new SelectListItem
            {
                Value = p.PayeeID.ToString(),
                Text = p.PayeeID + "--" + p.PayeeName
            });
            model.Accounts = accountBo.getByCustomerId(WebSecurity.CurrentUserId).Select(a => new SelectListItem
            {
                Value = a.AccountNumber.ToString(),
                Text = a.AccountType + ": " + a.AccountNumber.ToString() + "--balance: $" + a.Balance
            });
            if (ModelState.IsValid)
            {
                BPayBO bPayBo = new BPayBO();
                bPayBo.CreateBillPay(model.BillPay.AccountNumber, model.BillPay.PayeeID, model.BillPay.Amount, model.BillPay.ScheduleDate, model.BillPay.Period);
                return RedirectToAction("list");
            }
            return View(model);
        }


        public ActionResult List()
        {
            try
            {
                var model = new BPayModel();
                BPayBO bPayBO = new BPayBO();
                model.BillPays = bPayBO.GetAllIncludePayeeById(WebSecurity.CurrentUserId);
                return View(model);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public ActionResult Edit(int id)
        {
            var model = new BPayModel();
            PayeeBO payeeBo = new PayeeBO();
            BPayBO bPayBO = new BPayBO();
            AccountBO accountBo = new AccountBO();
            model.Payees = payeeBo.GetAll().Select(p => new SelectListItem
            {
                Value = p.PayeeID.ToString(),
                Text = p.PayeeID + "--" + p.PayeeName
            });
            model.Accounts = accountBo.getByCustomerId(WebSecurity.CurrentUserId).Select(a => new SelectListItem
            {
                Value = a.AccountNumber.ToString(),
                Text = a.AccountType + ": " + a.AccountNumber.ToString() + "--balance: $" + a.Balance
            });
            model.BillPay = bPayBO.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BPayModel model)
        {

            
            if (ModelState.IsValid)
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    BillPay billpay = db.BillPays.SingleOrDefault(b => b.BillPayID == model.BillPay.BillPayID);
                    billpay.AccountNumber = model.BillPay.AccountNumber;
                    billpay.PayeeID = model.BillPay.PayeeID;
                    billpay.Amount = model.BillPay.Amount;
                    billpay.ScheduleDate = model.BillPay.ScheduleDate;
                    billpay.Period = model.BillPay.Period;
                    db.SaveChanges();
                    return RedirectToAction("list");
                }
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            using(WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
            {
                BillPay billPay = db.BillPays.Find(id);
                db.BillPays.Remove(billPay);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

    }
}
