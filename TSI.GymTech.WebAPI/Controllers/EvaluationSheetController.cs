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
    public class EvaluationSheetController : Controller
    {
        private readonly EvaluationSheetManager _evaluationSheetManager;

        public EvaluationSheetController()
        {
            _evaluationSheetManager = new EvaluationSheetManager();
        }

        // GET: EvaluationSheet
        public ActionResult Index()
        {
            return View(_evaluationSheetManager.FindAll().Data);
        }
        
        // GET: EvaluationSheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvaluationSheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvaluationSheetId,Revaluation,Comments,StudentId,TrainerId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] EvaluationSheet evaluationSheet)
        {
            if (ModelState.IsValid)
            {
                _evaluationSheetManager.Create(evaluationSheet);
                return RedirectToAction("Index");
            }
            
            return View(evaluationSheet);
        }

        // GET: EvaluationSheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationSheet evaluationSheet = _evaluationSheetManager.FindById(id).Data;
            if (evaluationSheet == null)
            {
                return HttpNotFound();
            }
            return View(evaluationSheet);
        }

        // POST: EvaluationSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvaluationSheetId,Revaluation,Comments,StudentId,TrainerId,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] EvaluationSheet evaluationSheet)
        {
            if (ModelState.IsValid)
            {
                _evaluationSheetManager.Update(evaluationSheet);
                return RedirectToAction("Index");
            }
            return View(evaluationSheet);
        }

        // GET: EvaluationSheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationSheet evaluationSheet = _evaluationSheetManager.FindById(id).Data;
            if (evaluationSheet == null)
            {
                return HttpNotFound();
            }
            return View(evaluationSheet);
        }

        // POST: EvaluationSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluationSheet evaluationSheet = _evaluationSheetManager.FindById(id).Data;
            _evaluationSheetManager.Remove(evaluationSheet);
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
