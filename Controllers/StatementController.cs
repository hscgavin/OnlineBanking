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
    [Authorize(Roles = "User")]
    [InitializeSimpleMembership]
    public class StatementController : Controller
    {
        //
        // GET: /Stament/
        
        public ActionResult Index()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    AccountBO accountBO = new AccountBO();
                    var model = new StatementModel();
                    model.Accounts = (from s in db.Accounts
                                         where s.CustomerID == WebSecurity.CurrentUserId
                                         select s).ToList();
                    model.Transactions = db.Transactions.ToList();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    AccountBO accountBO = new AccountBO();
                    var model = new StatementModel();
                    int accountNo=Convert.ToInt32(form[0]);
                    model.Transactions = db.Transactions.Where(t => t.AccountNumber == accountNo).ToList();
                    ViewBag.isPost = true;
                    return View(model);
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
