using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDTAssignment2NWBA.DataAccessLayer;
using WebMatrix.WebData;
using WDTAssignment2NWBA.Models;
using WDTAssignment2NWBA.BusinessLogicLayer;

namespace WDTAssignment2NWBA.Controllers
{
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        private WDTAssignment2NWBAEntities db = new WDTAssignment2NWBAEntities();

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            
            var model = new CustomerModel();
           
            model.Customer = db.Customers.SingleOrDefault(c => c.CustomerID == WebSecurity.CurrentUserId);
            return View(model);
        }

        //
        // GET: /Profile/Details/5

        public ActionResult Details()
        {
            Customer customer = db.Customers.Find(WebSecurity.CurrentUserId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Profile/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Profile/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Profile/Edit/5
        
        public ActionResult Edit()
        {
            Customer customer = db.Customers.Find(WebSecurity.CurrentUserId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            
            CustomerBO customerBusinessObj = new CustomerBO();
            var customerToUpdate = customerBusinessObj.GetById(customer.CustomerID);
            if (ModelState.IsValid)
            {
                var customerUpdated = customerBusinessObj.UpdateCustomer(customer.CustomerID, customer.CustomerName, customer.TFN, customer.Address, customer.City, customer.State, customer.PostCode, customer.Phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Profile/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}