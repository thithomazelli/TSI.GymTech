using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressManager _addressManager;

        public AddressController()
        {
            _addressManager = new AddressManager();
        }

        ////GET: Address
        //public ActionResult Index(int personId)
        //{
        //    return View(_addressManager.FindByPersonId(personId).Data);
        //}

        // GET: Address/Create
        public ActionResult Create(int? personId)
        {
            if (personId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var model = new Address();
                return View(model);
            }
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,AddressType,PostalCode,Street,Number,Comments,District,State,City,Country,CreateDate,CreateUserId,ModifyDate,ModifyUserId,PersonId")] Address address)
        {
            if (ModelState.IsValid)
            {
                if (address != null)
                {
                    //Change to current user id later
                    address.CreateUserId = 0;
                    address.CreateDate = DateTime.Now;
                    address.ModifyUserId = 0;
                    address.ModifyDate = DateTime.Now;
                    _addressManager.Create(address);
                }

                return RedirectToAction("Edit/" + address.PersonId, "Student");
            }

            return View(address);
        }

        // GET: Address/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addressManager.FindById(id).Data;
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,AddressType,PostalCode,Street,Number,Comments,District,State,City,Country,CreateDate,CreateUserId,ModifyDate,ModifyUserId,PersonId")] Address address)
        {
            if (ModelState.IsValid)
            {
                if (address != null)
                {
                    //Change to current user id later
                    address.ModifyUserId = 0;
                    address.ModifyDate = DateTime.Now;
                    _addressManager.Update(address);
                }
                return RedirectToAction("Edit", "Student", new { id = address.PersonId });
            }
            return View(address);
        }

        // GET: Address/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addressManager.FindById(id).Data;
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Address/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Address address = _addressManager.FindById(id).Data;

            try
            {
                _addressManager.Remove(address);
                return Json(new { Type = "Success", Message = "O endereco foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o endereco." });
            }

            //_addressManager.Remove(address);
            //return RedirectToAction("Edit/" + address.PersonId, "Student");
        }
    }
}
