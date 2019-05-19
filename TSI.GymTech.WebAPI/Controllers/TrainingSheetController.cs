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

namespace TSI.GymTech.WebAPI.Controllers
{
    public class TrainingSheetController : Controller
    {
        private readonly TrainingSheetManager _trainingSheetManager;
        private ValidationErrorManager _validationErrorManager;

        public TrainingSheetController()
        {
            _trainingSheetManager = new TrainingSheetManager();
        }

        // GET: TrainingSheet
        public ActionResult Index()
        {
            return View(_trainingSheetManager.FindAll().Data.ToList());
        }
        // GET: Students
        public ActionResult Select(int? personId)
        {
            ViewBag.PersonId = personId;
            return View(_trainingSheetManager.FindAll().Data.ToList());
        }

        // POST: TrainingSheet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select(int? trainingSheetId, int? personId)
        {
            try
            {
                if (personId != null)
                { 
                    PersonManager personManager = new PersonManager();
                    var person = personManager.FindById(personId).Data;
                    var baseTrainingSheet = _trainingSheetManager.FindById(trainingSheetId).Data;

                    // Copy TrainingSheet
                    var newTrainingSheet = new TrainingSheet
                    {
                        Name = "Cópia de " + baseTrainingSheet.Name,
                        Description = "Cópia de " + baseTrainingSheet.Description,
                        Cycle = baseTrainingSheet.Cycle,
                        Status = baseTrainingSheet.Status,
                        Model = TrainingSheetModel.Personal,
                        Type = baseTrainingSheet.Type,
                        Comments = baseTrainingSheet.Comments,
                        Revaluation = baseTrainingSheet.Revaluation,
                        StudentId = personId,
                        CreateDate = DateTime.Now,
                        CreateUserId = 0,
                        ModifyDate = DateTime.Now,
                        ModifyUserId = 0
                    };

                    // 
                    newTrainingSheet.TrainingSheetExercises = new List<TrainingSheetExercise>();

                    // Copy TrainingSheet Exercises
                    foreach (var baseTrainingSheetExercise in baseTrainingSheet.TrainingSheetExercises)
                    {
                        TrainingSheetExercise newTrainingSheetExercise = new TrainingSheetExercise
                        {
                            ExerciseId = baseTrainingSheetExercise.ExerciseId,
                            Charge = baseTrainingSheetExercise.Charge,
                            NumberOfRepetitions = baseTrainingSheetExercise.NumberOfRepetitions,
                            Serie = baseTrainingSheetExercise.Serie,
                            CreateDate = DateTime.Now,
                            CreateUserId = 0,
                            ModifyDate = DateTime.Now,
                            ModifyUserId = 0
                        };

                        newTrainingSheet.TrainingSheetExercises.Add(newTrainingSheetExercise);
                    }

                    // Insert the new TrainingSheet copied
                    _trainingSheetManager.Create(newTrainingSheet);

                    //Entity.App_LocalResources.TrainingSheetModel. GetProperty(Enum.GetName(typeof(TSI.GymTech.Entity.Enumerates.TrainingSheetModel), item.Model)).GetValue(null)
                    //(Entity.App_LocalResources.TrainingSheetStatus).GetProperty(Enum.GetName(typeof(TSI.GymTech.Entity.Enumerates.TrainingSheetStatus), item.Status)).GetValue(null)
                    //(Entity.App_LocalResources.TrainingSheetType).GetProperty(Enum.GetName(typeof(TSI.GymTech.Entity.Enumerates.TrainingSheetType), item.Type)).GetValue(null)</span>
                    
                    // Return successfully message
                    return Json(new
                    {
                        Success = true,
                        Model = new Dictionary<string, dynamic>()
                        {
                            { "TrainingSheetId", newTrainingSheet.TrainingSheetId },
                            { "Name", newTrainingSheet.Name },
                            { "Cycle", newTrainingSheet.Cycle },
                            { "Model", GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.TrainingSheetModel)),
                                                        Enum.GetName(typeof(TrainingSheetModel), newTrainingSheet.Model)) },
                            { "Status", GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.TrainingSheetStatus)),
                                                        Enum.GetName(typeof(TrainingSheetStatus), newTrainingSheet.Status)) },
                            { "Type", GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.TrainingSheetType)),
                                                        Enum.GetName(typeof(TrainingSheetType), newTrainingSheet.Type)) }
                        },
                        Message = "O Treino " + newTrainingSheet?.Name + " foi criado."
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Return error message
                    return Json(new
                    {
                        Success = false,
                        Message = "Não foi possível copiar o treino selecionado."
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                // Return error message
                return Json(new
                {
                    Success = false,
                    Message = "Erro ao copiar o treino. Ex: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: TrainingSheet/Create
        public ActionResult Create(int? personId)
        {
            var model = new TrainingSheet();
            if (personId == null)
            { 
                model = new TrainingSheet
                {
                    Status = TrainingSheetStatus.Available
                };
            }
            else
            {
                PersonManager personManager = new PersonManager();
                Person person = personManager.FindById(personId).Data;

                model = new TrainingSheet
                {
                    Model = TrainingSheetModel.Personal,
                    Status = TrainingSheetStatus.Available,
                    Student = person ?? null,
                    StudentId = person?.PersonId ?? null
                };
            }

            return View(model);
        }

        // POST: TrainingSheet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingSheet trainingSheet)
        {
            if (trainingSheet.Model != TrainingSheetModel.Standard && trainingSheet.StudentId == null)
            {
                return Json(new { Success = false, Message = "O Aluno é obrigatório para treinos personalizados" });
            }

            if (ModelState.IsValid)
            {
                //Change to current user id later
                trainingSheet.CreateUserId = 0;
                trainingSheet.CreateDate = DateTime.Now;
                trainingSheet.ModifyUserId = 0;
                trainingSheet.ModifyDate = DateTime.Now;
                _trainingSheetManager.Create(trainingSheet);

                return Json(new { Success = true, Message = "Treino cadastrado com sucesso.", Id = trainingSheet.TrainingSheetId });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }
    
        // GET: TrainingSheet/Edit/5
        public ActionResult Edit(int? id)
        {
            //PersonManager personManager = new PersonManager();

            ////Gets all active students
            //List<Person> students = new List<Person>();
            //students = personManager.FindByProfileType(Entity.Enumerates.PersonType.Student).Data.ToList();
            //ViewBag.Students = new SelectList(students, "PersonId", "Name", trainingSheet.StudentId);

            ////Gets all active trainers
            //List<Person> trainers = new List<Person>();
            //trainers = personManager.FindByProfileType(Entity.Enumerates.PersonType.Teacher).Data.ToList();
            //ViewBag.Trainers = new SelectList(trainers, "PersonId", "Name", trainingSheet.TrainerId);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrainingSheet trainingSheet)
        {
            //PersonManager personManager = new PersonManager();

            ////Gets all active students
            //List<Person> students = new List<Person>();
            //students = personManager.FindByProfileType(Entity.Enumerates.PersonType.Student).Data.ToList();
            //ViewBag.Students = new SelectList(students, "PersonId", "Name", trainingSheet.StudentId);

            ////Gets all active trainers
            //List<Person> trainers = new List<Person>();
            //trainers = personManager.FindByProfileType(Entity.Enumerates.PersonType.Teacher).Data.ToList();
            //ViewBag.Trainers = new SelectList(trainers, "PersonId", "Name", trainingSheet.TrainerId);

            //if (ModelState.IsValid)
            //{
            //    if (trainingSheet != null)
            //    {
            //        //Change to current user id later
            //        trainingSheet.ModifyUserId = 0;
            //        trainingSheet.ModifyDate = DateTime.Now;
            //        _trainingSheetManager.Update(trainingSheet);
            //    }
            //    return RedirectToAction("Index");
            //}
            //return View(trainingSheet);

            ModelState.Remove("Student.Name");
            ModelState.Remove("Student.SocialSecurityCard");

            if (trainingSheet.Model != TrainingSheetModel.Standard && trainingSheet.StudentId == null)
            {
                return Json(new { Success = false, Message = "O Aluno é obrigatório para treinos personalizados" });
            }
            
            if (ModelState.IsValid)
            {
                if (trainingSheet.Model == TrainingSheetModel.Standard)
                {
                    trainingSheet.Student = null;
                    trainingSheet.StudentId = null;
                }

                trainingSheet.ModifyUserId = 0;
                trainingSheet.ModifyDate = DateTime.Now;

                _trainingSheetManager.Update(trainingSheet);

                return Json(new { Success = true, Message = "Treino atualizado com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
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
            
            try
            {
                _trainingSheetManager.Remove(trainingSheet);
                return Json(new { Type = "Success", Message = "O Treino " + trainingSheet.Name + " foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o Treino " + trainingSheet.Name + "." });
            }

            //return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }
    }
}
