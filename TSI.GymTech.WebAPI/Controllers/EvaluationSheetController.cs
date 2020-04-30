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
    public class EvaluationSheetController : Controller
    {
        private readonly EvaluationSheetManager _evaluationSheetManager;
        private readonly SheetQuestionManager _sheetQuestionManager;
        private readonly EvaluationSheetAnswerManager _evaluationSheetAnswerManager;
        private readonly AnamnesisSheetAnswerManager _anamnesisSheetAnswerManager;
        private ValidationErrorManager _validationErrorManager;

        public EvaluationSheetController()
        {
            _evaluationSheetManager = new EvaluationSheetManager();
            _sheetQuestionManager = new SheetQuestionManager();
            _evaluationSheetAnswerManager = new EvaluationSheetAnswerManager();
            _anamnesisSheetAnswerManager = new AnamnesisSheetAnswerManager();
        }

        // GET: EvaluationSheet
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetEvaluationSheets()
        {
            var evaluationSheetList = _evaluationSheetManager.FindAllByView().Data
                .Select(_ => new
                {
                    Id = _.EvaluationSheetId,
                    _.StudentId,
                    _.StudentName,
                    _.Description,
                    Revaluation = _.Revaluation?.ToString("dd/MM/yyyy"),
                    _.Comments
                });

            return Json(new { data = evaluationSheetList }, JsonRequestBehavior.AllowGet);
        }

        // GET: EvaluationSheet/Create
        public ActionResult Create(int? personId)
        {
            var model = new EvaluationSheet();
            var sheetEvaluationAnswerList = new List<EvaluationSheetAnswer>();
            var sheetAnamnesisAnswerList = new List<AnamnesisSheetAnswer>();

            _sheetQuestionManager
                .FindBySheetQuestionType(SheetQuestionType.Evaluation).Data.ToList()
                .ForEach(_ =>
                    sheetEvaluationAnswerList.Add(new EvaluationSheetAnswer
                    {
                        EvaluationSheetId = model.EvaluationSheetId,
                        SheetQuestionId = _.SheetQuestionId,
                        SheetQuestion = _
                    }));
            
            _sheetQuestionManager
                .FindBySheetQuestionType(SheetQuestionType.Anamnesis).Data.ToList()
                .ForEach(_ =>
                    sheetAnamnesisAnswerList.Add(new AnamnesisSheetAnswer
                    {
                        EvaluationSheetId = model.EvaluationSheetId,
                        SheetQuestionId = _.SheetQuestionId,
                        SheetQuestion = _
                    }));

            model.Status = TrainingSheetStatus.Available;
            model.Revaluation = DateTime.Now.AddYears(1);
            model.EvaluationSheetAnswers = sheetEvaluationAnswerList;
            model.AnamnesisSheetAnswers = sheetAnamnesisAnswerList;

            if (personId != null)
            {
                PersonManager personManager = new PersonManager();
                Person person = personManager.FindById(personId).Data;
                model.Student = person ?? null;
                model.StudentId = person?.PersonId ?? null;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvaluationSheet evaluationSheet, IDictionary<int, string> sheetEvaluationAnswer, IDictionary<int, string> sheetAnamnesisAnswer)
        {
            if (ModelState.IsValid)
            {
                //Change to current user id later
                evaluationSheet.CreateUserId = 0;
                evaluationSheet.CreateDate = DateTime.Now;
                evaluationSheet.ModifyUserId = 0;
                evaluationSheet.ModifyDate = DateTime.Now;

                evaluationSheet.EvaluationSheetAnswers = new List<EvaluationSheetAnswer>();
                _sheetQuestionManager
                    .FindBySheetQuestionType(SheetQuestionType.Evaluation).Data.ToList()
                    .ForEach(_ =>
                        evaluationSheet.EvaluationSheetAnswers.Add(new EvaluationSheetAnswer
                        {
                            EvaluationSheetId = evaluationSheet.EvaluationSheetId,
                            SheetQuestionId = _.SheetQuestionId,
                            Answer = sheetEvaluationAnswer.FirstOrDefault(__ => __.Key == _.SheetQuestionId).Value
                        }));

                evaluationSheet.AnamnesisSheetAnswers = new List<AnamnesisSheetAnswer>();
                _sheetQuestionManager
                    .FindBySheetQuestionType(SheetQuestionType.Anamnesis).Data.ToList()
                    .ForEach(_ =>
                        evaluationSheet.AnamnesisSheetAnswers.Add(new AnamnesisSheetAnswer
                        {
                            EvaluationSheetId = evaluationSheet.EvaluationSheetId,
                            SheetQuestionId = _.SheetQuestionId,
                            Answer = sheetAnamnesisAnswer.FirstOrDefault(__ => __.Key == _.SheetQuestionId).Value
                        }));

                _evaluationSheetManager.Create(evaluationSheet);

                return Json(new
                {
                    Success = true,
                    Message = "Avaliação cadastrada com sucesso.",
                    Id = evaluationSheet.EvaluationSheetId
                });
            }

            string message = evaluationSheet.StudentId == null
                ? "É obrigatório selecionar o Aluno para cadastrar novas Avaliações"
                : string.Empty;

            _validationErrorManager = new ValidationErrorManager();
            return Json(new
            {
                success = false,
                Message = message,
                Errors = _validationErrorManager.GetModelStateErrors(ModelState)
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: EvaluationSheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationSheet evaluationSheet = _evaluationSheetManager.FindById(id).Data;
            if (evaluationSheet == null)
            {
                return HttpNotFound();
            }
            return View(evaluationSheet);
        }

        // POST: EvaluationSheet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvaluationSheet evaluationSheet, IDictionary<int, string> sheetAnswers)
        {
            if (ModelState.IsValid)
            {
                _evaluationSheetManager.Update(evaluationSheet);
                return Json(new
                {
                    Success = true,
                    Message = "Avaliação atualizada com sucesso."
                });
            }

            string message = evaluationSheet.StudentId == null
                ? "É obrigatório selecionar o Aluno para cadastrar novas Avaliações"
                : string.Empty;

            _validationErrorManager = new ValidationErrorManager();
            return Json(new
            {
                success = false,
                Message = message,
                Errors = _validationErrorManager.GetModelStateErrors(ModelState)
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: EvaluationSheet/UpdateSheetAnswer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSheetAnswer(int sheetAnswerId, string newAnswer, string answerType)
        {
            string question;

            if (answerType == "SheetEvaluationAnswer")
            {
                EvaluationSheetAnswer evaluationSheetAnswer = _evaluationSheetAnswerManager.FindById(sheetAnswerId).Data;
                evaluationSheetAnswer.Answer = newAnswer;
                _evaluationSheetAnswerManager.Update(evaluationSheetAnswer);
                question = evaluationSheetAnswer.SheetQuestion.Question;
            }
            else
            {
                AnamnesisSheetAnswer anaminesisSheetAnswer = _anamnesisSheetAnswerManager.FindById(sheetAnswerId).Data;
                anaminesisSheetAnswer.Answer = newAnswer;
                _anamnesisSheetAnswerManager.Update(anaminesisSheetAnswer);
                question = anaminesisSheetAnswer.SheetQuestion.Question;
            }

            return Json(new
            {
                success = true,
                Message = "A resposta da pergunta " + question + " foi atualizada com sucesso."
            });
        }

        // GET: EvaluationSheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluationSheet evaluationSheet = _evaluationSheetManager.FindById(id).Data;
            if (evaluationSheet == null)
            {
                return HttpNotFound();
            }
            return View(evaluationSheet);
        }

        // POST: EvaluationSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluationSheet evaluationSheet = _evaluationSheetManager.FindById(id).Data;

            try
            {
                var studentName = evaluationSheet?.Student?.Name;
                _evaluationSheetManager.Remove(evaluationSheet);
                return Json(new
                {
                    Type = "Success",
                    Message = "A Avaliação de " + studentName + " foi removida com sucesso."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Type = "Error",
                    Message = "Não foi possível remover a Avaliação de " + evaluationSheet.Student.Name + "."
                });
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
    }
}
