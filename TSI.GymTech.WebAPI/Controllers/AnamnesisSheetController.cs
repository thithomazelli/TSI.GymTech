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
    public class AnamnesisSheetController : Controller
    {
        private readonly AnamnesisSheetManager _anamnesisSheetManager;

        public AnamnesisSheetController()
        {
            _anamnesisSheetManager = new AnamnesisSheetManager();
        }

        // GET: AnamnesisSheet
        public ActionResult Index()
        {
            return View(_anamnesisSheetManager.FindAll().Data);
        }
        
        // GET: AnamnesisSheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnamnesisSheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnamnesisSheetId,Revaluation,Comments,StudentId,TrainerId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] AnamnesisSheet anamnesisSheet)
        {
            if (ModelState.IsValid)
            {
                _anamnesisSheetManager.Create(anamnesisSheet);
                return RedirectToAction("Index");
            }

            return View(anamnesisSheet);
        }

        // GET: AnamnesisSheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnamnesisSheet anamnesisSheet = _anamnesisSheetManager.FindById(id).Data;
            if (anamnesisSheet == null)
            {
                return HttpNotFound();
            }
            return View(anamnesisSheet);
        }

        // POST: AnamnesisSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnamnesisSheetId,Revaluation,Comments,StudentId,TrainerId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] AnamnesisSheet anamnesisSheet)
        {
            if (ModelState.IsValid)
            {
                _anamnesisSheetManager.Update(anamnesisSheet);
                return RedirectToAction("Index");
            }
            return View(anamnesisSheet);
        }

        // GET: AnamnesisSheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnamnesisSheet anamnesisSheet = _anamnesisSheetManager.FindById(id).Data;
            if (anamnesisSheet == null)
            {
                return HttpNotFound();
            }
            return View(anamnesisSheet);
        }

        // POST: AnamnesisSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnamnesisSheet anamnesisSheet = _anamnesisSheetManager.FindById(id).Data;
            _anamnesisSheetManager.Remove(anamnesisSheet);
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
