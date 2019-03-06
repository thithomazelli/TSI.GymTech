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
    public class AccessLogController : Controller
    {
        private readonly AccessLogManager _accessLogManager;

        public AccessLogController()
        {
            _accessLogManager = new AccessLogManager();
        }

        // GET: AccessLog
        public ActionResult Index()
        {
            return View(_accessLogManager.FindAll().Data);
        }
        
        // GET: AccessLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccessLogId,AccessType,MessageDisplayed,PersonId,AccessControlId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] AccessLog accessLog)
        {
            if (ModelState.IsValid)
            {
                _accessLogManager.Create(accessLog);
                return RedirectToAction("Index");
            }

            return View(accessLog);
        }

        // GET: AccessLog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessLog accessLog = _accessLogManager.FindById(id).Data;
            if (accessLog == null)
            {
                return HttpNotFound();
            }
            return View(accessLog);
        }

        // POST: AccessLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccessLogId,AccessType,MessageDisplayed,PersonId,AccessControlId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] AccessLog accessLog)
        {
            if (ModelState.IsValid)
            {
                _accessLogManager.Update(accessLog);
                return RedirectToAction("Index");
            }
            return View(accessLog);
        }

        // GET: AccessLog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessLog accessLog = _accessLogManager.FindById(id).Data;
            if (accessLog == null)
            {
                return HttpNotFound();
            }
            return View(accessLog);
        }

        // POST: AccessLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessLog accessLog = _accessLogManager.FindById(id).Data;
            _accessLogManager.Remove(accessLog);
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
