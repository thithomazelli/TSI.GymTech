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
    public class AccessControlController : Controller
    {
        private readonly AccessControlManager _accessControlManager;
        
        public AccessControlController()
        {
            _accessControlManager = new AccessControlManager();
        }

        // GET: AccessControl
        public ActionResult Index()
        {
            return View(_accessControlManager.FindAll().Data);
        }
        
        // GET: AccessControl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessControl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccessControlId,IpAddress,Name,IsStandard,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] AccessControl accessControl)
        {
            if (ModelState.IsValid)
            {
                _accessControlManager.Create(accessControl);
                return RedirectToAction("Index");
            }

            return View(accessControl);
        }

        // GET: AccessControl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _accessControlManager.FindById(id).Data;
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessControl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccessControlId,IpAddress,Name,IsStandard,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] AccessControl accessControl)
        {
            if (ModelState.IsValid)
            {
                _accessControlManager.Update(accessControl);
                return RedirectToAction("Index");
            }
            return View(accessControl);
        }

        // GET: AccessControl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _accessControlManager.FindById(id).Data;
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessControl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessControl accessControl = _accessControlManager.FindById(id).Data;
            _accessControlManager.Remove(accessControl);
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
