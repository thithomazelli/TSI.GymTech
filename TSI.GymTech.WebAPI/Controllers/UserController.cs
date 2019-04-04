using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Manager.EntityManagers;
using TSI.GymTech.Manager.Utitlities;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly PersonManager _personManager;
        private PhotoManager _photoManager;

        public UserController()
        {
            _personManager = new PersonManager();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_personManager.FindByProfileType(PersonType.Student, false).Data);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,Name,ProfileType,Password,Gender,NationalIDCard,SocialSecurityCard,BirthDate,RegistrationDate,DueDate,Status,Photo,Comments,Phone,MobilePhone,Email,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _personManager.Create(person);
                return RedirectToAction("Edit/" + person.PersonId);
            }

            return View(person);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,Name,ProfileType,Password,Gender,NationalIDCard,SocialSecurityCard,BirthDate,RegistrationDate,DueDate,Status,Photo,Comments,Phone,MobilePhone,Email,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _personManager.Update(person);
                //return RedirectToAction("Index");
            }
            return View(person);
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
                
                //return RedirectToAction("Index");
                return Json(new { Type = "Success", Message = "O Usuário " + person.Name + " foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o Usuário " + person.Name + "." });
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
            catch(Exception ex)
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
    }
}