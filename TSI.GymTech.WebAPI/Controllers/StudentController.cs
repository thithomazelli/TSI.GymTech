﻿using System;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Manager.EntityManagers;
using TSI.GymTech.Manager.Utitlities;
using TSI.GymTech.Manager.Result;
using System.Resources;
using System.Globalization;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly PersonManager _personManager;
        private PhotoManager _photoManager;
        private ValidationErrorManager _validationErrorManager;
        
        public StudentController()
        {
            _personManager = new PersonManager();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStudents(string filter)
        {
            var currentDate = DateTime.Now;
            
            switch(filter)
            {
                case "Birthday":
                    {
                        var studentList = _personManager.FindByProfileType(PersonType.Student, false, true).Data
                            .Where(_ => _.Status == PersonStatus.Active && _.BirthDate != null
                                        && DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year).Date >= currentDate.Date
                                        && DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year).Date <= currentDate.Date.AddDays(4))
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email,
                                _.BirthDate
                            })
                            .OrderBy(_ => DateTime.Parse(_.BirthDate?.Day + "." + _.BirthDate?.Month + "." + currentDate.Year).Date);

                        return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
                    }
                case "Frequent":
                    {
                        var studentList = _personManager.FindAllByStudentFrequentView().Data
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            });

                        return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
                    }
                case "NotFrequent":
                    {
                        var studentList = _personManager.FindAllByStudentNotFrequentView().Data
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            });

                        return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
                    }
                case "Inactive":
                    {
                        var studentList = _personManager.FindByProfileType(PersonType.Student, false, true).Data
                            .Where(_ => _.Status != PersonStatus.Active)
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            }); ;

                        return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
                    }
                default:
                    {
                        var studentList = _personManager.FindByProfileType(PersonType.Student, false, true).Data
                            .Select(_ => new
                            {
                                Id = _.PersonId,
                                Status = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PersonStatus)),
                                        Enum.GetName(typeof(PersonStatus), _.Status)),
                                _.Name,
                                _.SocialSecurityCard,
                                _.Email
                            }); ;

                        return Json(new { data = studentList }, JsonRequestBehavior.AllowGet);
                    }
            }
        }

        // GET: Students
        public ActionResult Select()
        {
            return View();
        }
        
        // POST: TrainingSheet/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select(int? personId)
        {
            var person = _personManager.FindById(personId).Data;

            if (person == null)
            {
                return Json(new { Success = true, Message = "Não foi possível incluir o aluno selecionado." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Success = true,
                Id = person?.PersonId,
                PersonName = person?.Name,
                Message = "O Aluno " + person?.Name + " foi selecionado."
            }, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Student/Create
        public ActionResult Create()
        {
            var model = new Person
            {
                ProfileType = PersonType.Student,
                Status = PersonStatus.Active
            }; 
            return View(model);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            ValidateDuplicated(person);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                person.ProfileType = PersonType.Student;
                person.CreateUserId = 0;
                person.CreateDate = DateTime.Now;
                person.ModifyUserId = 0;
                person.ModifyDate = DateTime.Now;
                _personManager.Create(person);

                return Json(new { Success = true, Message = "Aluno cadastrado com sucesso.", Id = person.PersonId });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Person person = _personManager.FindById(id).Data;

            if (person == null)
            {
                return HttpNotFound();
            }
            
            return View(person);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            ValidateDuplicated(person);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                person.ModifyUserId = 0;
                person.ModifyDate = DateTime.Now;
                _personManager.Update(person);

                return Json(new { Success = true, Message = "Aluno atualizado com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = _personManager.FindById(id).Data;
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Person person = _personManager.FindById(id).Data;
            
            try
            {
                _personManager.Remove(person);
                return Json(new { Type = "Success", Message = "O Aluno " + person.Name + " foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o Aluno " + person.Name + "." });
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
                Person person = _personManager.FindById(id).Data;
                person.Photo = person.Photo ?? person.PersonId + "_" + person.Name + "." + fileExtension;
                _photoManager = new PhotoManager(Server.MapPath("~/Images/Persons/"));

                if (_photoManager.CapturePhoto(base64image, person.Photo) == ResultEnum.Success)
                {
                    _personManager.Update(person);
                    return Json(new { Type = "Success", Message = "A nova imagem foi capturada com sucesso.", ImageName = person.Photo });
                }
                else
                {
                    return Json(new { Type = "Error", Message = "Não foi possível capturar a nova imagem.", ImageName = person.Photo });
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
            Person person = _personManager.FindById(id).Data;
            _photoManager = new PhotoManager(Server.MapPath("~/Images/Persons/"));

            if (_photoManager.RemovePhoto(person.Photo) == ResultEnum.Success)
            {
                person.Photo = null;
                _personManager.Update(person);
                return Json(new { Type = "Success", Message = "A imagem foi removida com sucesso." });
            }
            else
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover a imagem do Usuário." });
            }
        }
        
        private void ValidateDuplicated(Person person)
        {
            // Validate if Name is duplicated
            if (_personManager.IsNameDuplicated(person))
            {
                ModelState.AddModelError("Name", "Já existe um aluno ou usuário cadastrado com Nome informado.");
            }

            // Validate if Email is duplicated
            if (_personManager.IsEmailDuplicated(person))
            {
                ModelState.AddModelError("Email", "Já existe um aluno ou usuário cadastrado com E-mail informado.");
            }

            // Validate if SocialSecurityCard is duplicated
            if (_personManager.IsSocialSecurityCardDuplicated(person))
            {
                ModelState.AddModelError("SocialSecurityCard", "Já existe um aluno ou usuário cadastrado com o CPF informado.");
            }

            // Validate if NationalIDCard is duplicated
            if (_personManager.IsNationalIDCardDuplicated(person))
            {
                ModelState.AddModelError("NationalIDCard", "Já existe um aluno ou usuário cadastrado com o RG informado.");
            }
        }
        
        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }
    }
}
