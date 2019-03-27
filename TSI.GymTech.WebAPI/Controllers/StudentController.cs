using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class StudentController : Controller
    {
        private readonly PersonManager _personManager;

        public StudentController()
        {
            _personManager = new PersonManager();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_personManager.FindByProfileType(PersonType.Student, true).Data);
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
                if (person != null)
                {
                    //Change to current user id later
                    person.CreateUserId = 0;
                    person.CreateDate = DateTime.Now;
                    person.ModifyUserId = 0;
                    person.ModifyDate = DateTime.Now;
                    _personManager.Create(person);
                }
                return RedirectToAction("Index");
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
                if (person != null)
                {
                    //Change to current user id later
                    person.ModifyUserId = 0;
                    person.ModifyDate = DateTime.Now;
                    _personManager.Update(person);
                }                
                return RedirectToAction("Index");
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
            _personManager.Remove(person);
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
