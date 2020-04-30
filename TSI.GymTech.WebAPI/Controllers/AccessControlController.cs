using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class AccessControlController : Controller
    {
        private readonly AccessControlManager _accessControlManager;
        private ValidationErrorManager _validationErrorManager;

        public AccessControlController()
        {
            _accessControlManager = new AccessControlManager();
        }

        // GET: AccessControl
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAccessControls()
        {
            var accessControlList = _accessControlManager.FindAll().Data
                .Select(_ => new
                {
                    Id = _.AccessControlId,
                    _.Name,
                    _.IpAddress,
                    _.IsStandard
                });

            return Json(new { data = accessControlList }, JsonRequestBehavior.AllowGet);
        }

        // GET: AccessControl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessControl/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccessControl accessControl)
        {
            ValidateDuplicated(accessControl);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                accessControl.CreateUserId = 0;
                accessControl.CreateDate = DateTime.Now;
                accessControl.ModifyUserId = 0;
                accessControl.ModifyDate = DateTime.Now;
                _accessControlManager.Create(accessControl);

                return Json(new { Success = true, Message = "Controle de Acesso cadastrado com sucesso.", Id = accessControl.AccessControlId });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: AccessControl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _accessControlManager.FindById(id).Data;
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessControl/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccessControl accessControl)
        {
            ValidateDuplicated(accessControl);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                accessControl.ModifyUserId = 0;
                accessControl.ModifyDate = DateTime.Now;
                _accessControlManager.Update(accessControl);

                return Json(new { Success = true, Message = "Controle de Acesso atualizado com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: AccessControl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = _accessControlManager.FindById(id).Data;
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessControl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessControl accessControl = _accessControlManager.FindById(id).Data;

            try
            {
                _accessControlManager.Remove(accessControl);
                return Json(new { Type = "Success", Message = "A Catraca " + accessControl.Name + " foi removida com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover a Catraca " + accessControl.Name + "." });
            }
        }

        private void ValidateDuplicated(AccessControl accessControl)
        {
            // Validate if IpAddress is duplicated
            if (_accessControlManager.IsIpAddressDuplicated(accessControl))
            {
                ModelState.AddModelError("IpAddress", "Já existe um Equipamento cadastrado com IP informado.");
            }
        }
    }
}