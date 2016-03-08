using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.Models;

namespace WDTAssignment2NWBA.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(int statusCode, Exception exception)
        {
            //error
            var model = new ErrorModel();
            model.HttpStatusCode = statusCode;
            Response.StatusCode = statusCode;
            return View(model);
        }
    }
}
