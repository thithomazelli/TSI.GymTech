using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentManager _paymentManager;

        public PaymentController()
        {
            _paymentManager = new PaymentManager();
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View(_paymentManager.FindAll().Data);
        }
        
        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,ProductName,PersonId,Discount,TotalPrice,PaymentType,Status,DatePaymentEstimated,DatePaymentCompleted,Comments,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentManager.Create(payment);
                return RedirectToAction("Index");
            }
            
            return View(payment);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = _paymentManager.FindById(id).Data;
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,ProductName,PersonId,Discount,TotalPrice,PaymentType,Status,DatePaymentEstimated,DatePaymentCompleted,Comments,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentManager.Update(payment);
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = _paymentManager.FindById(id).Data;
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = _paymentManager.FindById(id).Data;
            _paymentManager.Remove(payment);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
