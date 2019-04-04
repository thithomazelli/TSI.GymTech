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
    public class ExerciseController : Controller
    {
        private readonly ExerciseManager _exerciseManager;

        public ExerciseController()
        {
            _exerciseManager = new ExerciseManager();
        }

        // GET: Exercise
        public ActionResult Index()
        {
            return View(_exerciseManager.FindAll().Data);
        }
        
        // GET: Exercise/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExerciseId,Name,Description,Photo,Comments,MuscleWorked,MuscularGroup,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                //Change to current user id later
                exercise.CreateUserId = 0;
                exercise.CreateDate = DateTime.Now;
                exercise.ModifyUserId = 0;
                exercise.ModifyDate = DateTime.Now;
                _exerciseManager.Create(exercise);
                return RedirectToAction("Index");
            }

            return View(exercise);
        }

        // GET: Exercise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = _exerciseManager.FindById(id).Data;
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExerciseId,Name,Description,Photo,Comments,MuscleWorked,MuscularGroup,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                //Change to current user id later
                exercise.ModifyUserId = 0;
                exercise.ModifyDate = DateTime.Now;
                _exerciseManager.Update(exercise);
                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = _exerciseManager.FindById(id).Data;
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercise exercise = _exerciseManager.FindById(id).Data;
            _exerciseManager.Remove(exercise);
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
