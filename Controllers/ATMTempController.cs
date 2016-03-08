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
    public class ATMTempController : Controller
    {
        //
        // GET: /ATMTemp/

        public ActionResult Index()
        {
            try
            {
                using (WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities())
                {
                    AccountBO accountBO = new AccountBO();
                    var model = new ATMTempModel();
                    var accounts = accountBO.getByCustomerId(WebSecurity.CurrentUserId);
                    List<string> s = new List<string>();
                    foreach (var a in accounts)
                    {
                        s.Add(a.AccountNumber+" "+a.AccountType);
                    }
                    model.ToAcounts = s;
                    var toAccounts = accountBO.getAll();
                    List<string> ts = new List<string>();
                    foreach (var b in toAccounts)
                    {
                        ts.Add(b.AccountNumber + " "+ b.AccountType);
                    }
                    
                     
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
