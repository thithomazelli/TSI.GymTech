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
    public class TrainingSheetExerciseController : Controller
    {
        private TrainingSheetExerciseManager _trainingSheetExerciseManager;

        public TrainingSheetExerciseController()
        {
            _trainingSheetExerciseManager = new TrainingSheetExerciseManager();
        }
        
        // GET: TrainingSheetExercises/Create
        public ActionResult Create(int? trainingSheetId)
        {
            if (trainingSheetId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var model = new TrainingSheetExercise();
                var exerciseManager = new ExerciseManager();

                //Gets all active students
                List<Exercise> exercises = new List<Exercise>();
                exercises = exerciseManager.FindAll().Data.ToList();
                ViewBag.ExerciseId = new SelectList(exercises, "ExerciseId", "Name", model.ExerciseId);
                
                return View(model);
            }

            //ViewBag.ExerciseId = new SelectList(db.Exercises, "ExerciseId", "Name");
            //return View();
        }

        // POST: TrainingSheetExercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingSheetExercise trainingSheetExercise)
        {
            if (ModelState.IsValid)
            {
                if (trainingSheetExercise != null)
                {
                    //Change to current user id later
                    trainingSheetExercise.CreateUserId = 0;
                    trainingSheetExercise.CreateDate = DateTime.Now;
                    trainingSheetExercise.ModifyUserId = 0;
                    trainingSheetExercise.ModifyDate = DateTime.Now;
                    _trainingSheetExerciseManager.Create(trainingSheetExercise);
                }

                return RedirectToAction("Edit/" + trainingSheetExercise.TrainingSheetId, "TrainingSheet");
            }

            return View(trainingSheetExercise);

            //if (ModelState.IsValid)
            //{
            //    db.TrainingSheetExercises.Add(trainingSheetExercise);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.ExerciseId = new SelectList(db.Exercises, "ExerciseId", "Name", trainingSheetExercise.ExerciseId);
            //return View(trainingSheetExercise);
        }

        // GET: TrainingSheetExercises/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TrainingSheetExercise trainingSheetExercise = _trainingSheetExerciseManager.FindById(id).Data;

            //Gets all active students
            var exerciseManager = new ExerciseManager();
            List<Exercise> exercises = new List<Exercise>();
            exercises = exerciseManager.FindAll().Data.ToList();
            ViewBag.ExerciseId = new SelectList(exercises, "ExerciseId", "Name", trainingSheetExercise.ExerciseId);
            
            if (trainingSheetExercise == null)
            {
                return HttpNotFound();
            }
            
            return View(trainingSheetExercise);
        }

        // POST: TrainingSheetExercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainingSheetExercise trainingSheetExercise)
        {
            if (ModelState.IsValid)
            {
                if (trainingSheetExercise != null)
                {
                    //Change to current user id later
                    trainingSheetExercise.ModifyUserId = 0;
                    trainingSheetExercise.ModifyDate = DateTime.Now;
                    _trainingSheetExerciseManager.Update(trainingSheetExercise);
                }
                return RedirectToAction("Edit", "TrainingSheet", new { id = trainingSheetExercise.TrainingSheetId});
            }
            return View(trainingSheetExercise);

            //if (ModelState.IsValid)
            //{
            //    db.Entry(trainingSheetExercise).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.ExerciseId = new SelectList(db.Exercises, "ExerciseId", "Name", trainingSheetExercise.ExerciseId);
            //return View(trainingSheetExercise);
        }

        // GET: TrainingSheetExercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingSheetExercise trainingSheetExercise = _trainingSheetExerciseManager.FindById(id).Data;
            if (trainingSheetExercise == null)
            {
                return HttpNotFound();
            }
            return View(trainingSheetExercise);
        }

        // POST: TrainingSheetExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingSheetExercise trainingSheetExercise = _trainingSheetExerciseManager.FindById(id).Data;

            try
            {
                _trainingSheetExerciseManager.Remove(trainingSheetExercise);
                return Json(new { Type = "Success", Message = "O exercício foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o exercício." });
            }

            //TrainingSheetExercise trainingSheetExercise = db.TrainingSheetExercises.Find(id);
            //db.TrainingSheetExercises.Remove(trainingSheetExercise);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }
    }
}
