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
    public class TrainingSheetController : Controller
    {
        private readonly TrainingSheetManager _trainingSheetManager;

        public TrainingSheetController()
        {
            _trainingSheetManager = new TrainingSheetManager();
        }

        // GET: TrainingSheet
        public ActionResult Index()
        {
            return View(_trainingSheetManager.FindAll().Data);
        }
        
        // GET: TrainingSheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingSheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingSheetId,Cycle,Status,Type,Revaluation,Comments,StudentId,TrainerId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] TrainingSheet trainingSheet)
        {
            if (ModelState.IsValid)
            {
                _trainingSheetManager.Create(trainingSheet);
                return RedirectToAction("Index");
            }

            return View(trainingSheet);
        }

        // GET: TrainingSheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSheet trainingSheet = _trainingSheetManager.FindById(id).Data;
            if (trainingSheet == null)
            {
                return HttpNotFound();
            }
            return View(trainingSheet);
        }

        // POST: TrainingSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingSheetId,Cycle,Status,Type,Revaluation,Comments,StudentId,TrainerId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] TrainingSheet trainingSheet)
        {
            if (ModelState.IsValid)
            {
                _trainingSheetManager.Update(trainingSheet);
                return RedirectToAction("Index");
            }
            return View(trainingSheet);
        }

        // GET: TrainingSheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSheet trainingSheet = _trainingSheetManager.FindById(id).Data;
            if (trainingSheet == null)
            {
                return HttpNotFound();
            }
            return View(trainingSheet);
        }

        // POST: TrainingSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingSheet trainingSheet = _trainingSheetManager.FindById(id).Data;
            _trainingSheetManager.Remove(trainingSheet);
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
