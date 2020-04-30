using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web.Mvc;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class SheetQuestionController : Controller
    {
        private readonly SheetQuestionManager _sheetQuestionManager;
        private ValidationErrorManager _validationErrorManager;

        public SheetQuestionController()
        {
            _sheetQuestionManager = new SheetQuestionManager();
        }

        // GET: SheetQuestion
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSheetQuestions()
        {
            var sheetQuestionList = _sheetQuestionManager.FindAll().Data
                .Select(_ => new
                {
                    Id = _.SheetQuestionId,
                    _.Question,
                    QuestionType = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.SheetQuestionType)),
                            Enum.GetName(typeof(SheetQuestionType), _.QuestionType)),
                    AnswerType = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.SheetAnswerType)),
                            Enum.GetName(typeof(SheetAnswerType), _.AnswerType)),
                    _.Order
                });

            return Json(new { data = sheetQuestionList }, JsonRequestBehavior.AllowGet);
        }

        // GET: SheetQuestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SheetQuestion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SheetQuestion sheetQuestion)
        {
            ValidateDuplicated(sheetQuestion);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                sheetQuestion.CreateUserId = 0;
                sheetQuestion.CreateDate = DateTime.Now;
                sheetQuestion.ModifyUserId = 0;
                sheetQuestion.ModifyDate = DateTime.Now;
                _sheetQuestionManager.Create(sheetQuestion);

                return Json(new { Success = true, Message = "Pergunta cadastrada com sucesso.", Id = sheetQuestion.SheetQuestionId });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SheetQuestion sheetQuestion)
        {
            ValidateDuplicated(sheetQuestion);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                sheetQuestion.ModifyUserId = 0;
                sheetQuestion.ModifyDate = DateTime.Now;
                _sheetQuestionManager.Update(sheetQuestion);

                return Json(new { Success = true, Message = "Pergunta atualizada com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
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

            try
            {
                _sheetQuestionManager.Remove(sheetQuestion);
                return Json(new { Type = "Success", Message = "A Questão " + sheetQuestion.Question + " foi removida com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover a Questão " + sheetQuestion.Question + "." });
            }
        }

        private void ValidateDuplicated(SheetQuestion sheetQuestion)
        {
            // Validate if the question is duplicated
            if (_sheetQuestionManager.IsDuplicated(sheetQuestion))
            {
                ModelState.AddModelError("Question", "Já existe uma pergunta cadastrada com o mesmo título e tipo.");
            }
        }
        
        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }
    }
}
