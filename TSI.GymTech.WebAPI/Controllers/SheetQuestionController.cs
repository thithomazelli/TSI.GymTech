﻿using System;
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
    public class SheetQuestionController : Controller
    {
        private readonly SheetQuestionManager _sheetQuestionManager;

        public SheetQuestionController()
        {
            _sheetQuestionManager = new SheetQuestionManager();
        }

        // GET: SheetQuestion
        public ActionResult Index()
        {
            return View(_sheetQuestionManager.FindAll().Data);
        }
        
        // GET: SheetQuestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SheetQuestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SheetQuestionId,TypeQuestion,Order,Question,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] SheetQuestion sheetQuestion)
        {
            if (ModelState.IsValid)
            {
                _sheetQuestionManager.Create(sheetQuestion);
                return RedirectToAction("Index");
            }

            return View(sheetQuestion);
        }

        // GET: SheetQuestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SheetQuestion sheetQuestion = _sheetQuestionManager.FindById(id).Data;
            if (sheetQuestion == null)
            {
                return HttpNotFound();
            }
            return View(sheetQuestion);
        }

        // POST: SheetQuestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SheetQuestionId,TypeQuestion,Order,Question,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] SheetQuestion sheetQuestion)
        {
            if (ModelState.IsValid)
            {
                _sheetQuestionManager.Update(sheetQuestion);
                return RedirectToAction("Index");
            }
            return View(sheetQuestion);
        }

        // GET: SheetQuestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SheetQuestion sheetQuestion = _sheetQuestionManager.FindById(id).Data;
            if (sheetQuestion == null)
            {
                return HttpNotFound();
            }
            return View(sheetQuestion);
        }

        // POST: SheetQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SheetQuestion sheetQuestion = _sheetQuestionManager.FindById(id).Data;
            _sheetQuestionManager.Remove(sheetQuestion);
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