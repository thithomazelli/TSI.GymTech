using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Manager.Utitlities;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ExerciseManager _exerciseManager;
        private PhotoManager _photoManager;
        private ValidationErrorManager _validationErrorManager;

        public ExerciseController()
        {
            _exerciseManager = new ExerciseManager();
        }

        // GET: Exercise
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetExercises()
        {
            var exerciseList = _exerciseManager.FindAll().Data
                .Select(_ => new
                {
                    Id = _.ExerciseId,
                    _.Name,
                    _.Description,
                    _.Comments,
                    _.MuscleWorked,
                    MuscularGroup = _.MuscularGroup != null
                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.MuscularGroup)),
                                Enum.GetName(typeof(MuscularGroup), _.MuscularGroup))
                        : null
                });

            return Json(new { data = exerciseList }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(Exercise exercise)
        {
            ValidateDuplicated(exercise);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                exercise.CreateUserId = 0;
                exercise.CreateDate = DateTime.Now;
                exercise.ModifyUserId = 0;
                exercise.ModifyDate = DateTime.Now;
                _exerciseManager.Create(exercise);

                return Json(new { Success = true, Message = "Exercício cadastrado com sucesso.", Id = exercise.ExerciseId });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exercise exercise)
        {
            ValidateDuplicated(exercise);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                exercise.ModifyUserId = 0;
                exercise.ModifyDate = DateTime.Now;
                _exerciseManager.Update(exercise);

                return Json(new { Success = true, Message = "Exercício atualizado com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
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

            try
            {
                _exerciseManager.Remove(exercise);

                //return RedirectToAction("Index");
                return Json(new { Type = "Success", Message = "O Exercício " + exercise.Name + " foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o Exercício " + exercise.Name + "." });
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapturePhoto(int? id, string base64image, string fileExtension)
        {
            try
            {
                Exercise exercise = _exerciseManager.FindById(id).Data;
                exercise.Photo = exercise.Photo ?? exercise.ExerciseId + "_" + exercise.Name + "." + fileExtension;
                _photoManager = new PhotoManager(Server.MapPath("~/Images/Exercises/"));

                if (_photoManager.CapturePhoto(base64image, exercise.Photo) == ResultEnum.Success)
                {
                    _exerciseManager.Update(exercise);
                    return Json(new { Type = "Success", Message = "A nova imagem foi capturada com sucesso.", ImageName = exercise.Photo });
                }
                else
                {
                    return Json(new { Type = "Error", Message = "Não foi possível capturar a nova imagem.", ImageName = exercise.Photo });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemovePhoto(int? id)
        {
            Exercise exercise = _exerciseManager.FindById(id).Data;
            _photoManager = new PhotoManager(Server.MapPath("~/Images/Exercises/"));

            if (_photoManager.RemovePhoto(exercise.Photo) == ResultEnum.Success)
            {
                exercise.Photo = null;
                _exerciseManager.Update(exercise);
                return Json(new { Type = "Success", Message = "A imagem foi removida com sucesso." });
            }
            else
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover a imagem do Exercício." });
            }
        }

        private void ValidateDuplicated(Exercise exercise)
        {
            // Validate if Name is duplicated
            if (_exerciseManager.IsNameDuplicated(exercise))
            {
                ModelState.AddModelError("Name", "Já existe um aluno ou usuário cadastrado com Nome informado.");
            }
        }

        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }
    }
}