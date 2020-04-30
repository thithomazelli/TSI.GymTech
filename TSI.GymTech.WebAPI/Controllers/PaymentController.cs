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
    public class PaymentController : Controller
    {
        private readonly PaymentManager _paymentManager;
        private ValidationErrorManager _validationErrorManager;

        public PaymentController()
        {
            _paymentManager = new PaymentManager();
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPayments(string filter)
        {
            var currentDate = DateTime.Now;

            switch (filter)
            {
                case "PaymentDelayed":
                    {
                        var paymentList = _paymentManager.FindAllByView()
                            .Data
                            .Where(_ => _.Status == PaymentStatus.Pending && 
                                        DateTime.Parse(_.DatePaymentEstimated?.Day + "." + 
                                                       _.DatePaymentEstimated?.Month + "." +
                                                       _.DatePaymentEstimated?.Year).Date <= currentDate.Date)
                            .Select(_ => new
                            {
                                Id = _.PaymentId,
                                _.Description,
                                _.StudentName,
                                Status = _.Status != null
                                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PaymentStatus)),
                                            Enum.GetName(typeof(PaymentStatus), _.Status))
                                        : null,
                                DatePaymentEstimated = _.DatePaymentEstimated?.ToString("dd/MM/yyyy"),
                                Discount = _.Discount != null && _.Discount > 0
                                    ? string.Format("{0:P2}", _.Discount / 100)
                                    : string.Format("{0:P2}", 0),
                                TotalPrice = _.TotalPrice?.ToString("C")
                            });

                        return Json(new { data = paymentList }, JsonRequestBehavior.AllowGet);
                    }
                case "PaymentDue":
                    {
                        var paymentList = _paymentManager.FindAllByView()
                            .Data
                            .Where(_ => _.Status == PaymentStatus.Pending &&
                                        DateTime.Parse(_.DatePaymentEstimated?.Day + "." +
                                                       _.DatePaymentEstimated?.Month + "." +
                                                       _.DatePaymentEstimated?.Year).Date >= currentDate.Date)
                            .Select(_ => new
                            {
                                Id = _.PaymentId,
                                _.Description,
                                _.StudentName,
                                Status = _.Status != null
                                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PaymentStatus)),
                                            Enum.GetName(typeof(PaymentStatus), _.Status))
                                        : null,
                                DatePaymentEstimated = _.DatePaymentEstimated?.ToString("dd/MM/yyyy"),
                                Discount = _.Discount != null && _.Discount > 0
                                    ? string.Format("{0:P2}", _.Discount / 100)
                                    : string.Format("{0:P2}", 0),
                                TotalPrice = _.TotalPrice?.ToString("C")
                            });

                        return Json(new { data = paymentList }, JsonRequestBehavior.AllowGet);
                    }
                case "PaymentPaid":
                    {
                        var paymentList = _paymentManager.FindAllByView()
                            .Data
                            .Where(_ => _.Status == PaymentStatus.Completed)
                            .Select(_ => new
                            {
                                Id = _.PaymentId,
                                _.Description,
                                _.StudentName,
                                Status = _.Status != null
                                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PaymentStatus)),
                                            Enum.GetName(typeof(PaymentStatus), _.Status))
                                        : null,
                                DatePaymentEstimated = _.DatePaymentEstimated?.ToString("dd/MM/yyyy"),
                                Discount = _.Discount != null && _.Discount > 0
                                    ? string.Format("{0:P2}", _.Discount / 100)
                                    : string.Format("{0:P2}", 0),
                                TotalPrice = _.TotalPrice?.ToString("C")
                            });

                        return Json(new { data = paymentList }, JsonRequestBehavior.AllowGet);
                    }
                default:
                    {
                        var paymentList = _paymentManager.FindAllByView()
                            .Data
                            .Select(_ => new
                            {
                                Id = _.PaymentId,
                                _.Description,
                                _.StudentId,
                                _.StudentName,
                                Status = _.Status != null
                                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.PaymentStatus)),
                                            Enum.GetName(typeof(PaymentStatus), _.Status))
                                        : null,
                                DatePaymentEstimated = _.DatePaymentEstimated?.ToString("dd/MM/yyyy"),
                                Discount = _.Discount != null && _.Discount > 0
                                    ? string.Format("{0:P2}", _.Discount / 100)
                                    : string.Format("{0:P2}", 0),
                                TotalPrice = _.TotalPrice?.ToString("C")
                            });

                        return Json(new { data = paymentList }, JsonRequestBehavior.AllowGet);
                    }
            }
        }

        // GET: Payment/Create
        public ActionResult Create(int? personId)
        {
            var model = new Payment();

            if (personId != null)
            {
                PersonManager personManager = new PersonManager();
                Person person = personManager.FindById(personId).Data;

                model = new Payment
                {
                    Student = person ?? null,
                    StudentId = person?.PersonId ?? 0
                };
            }

            model.Discount = 0;
            model.TotalPrice = _paymentManager.CalcuatePaymentPrice(model);

            model.Status = PaymentStatus.Pending;
            return View(model);
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment payment)
        {
            if (payment?.StudentId == 0)
            {
                return Json(new { Success = false, Message = "O Aluno é obrigatório para pagamentos" });
            }

            if (ModelState.IsValid)
            {
                //Change to current user id later
                payment.CreateUserId = 0;
                payment.CreateDate = DateTime.Now;
                payment.ModifyUserId = 0;
                payment.ModifyDate = DateTime.Now;
                _paymentManager.Create(payment);

                return Json(new { Success = true, Message = "Pagamento cadastrado com sucesso.", Id = payment.PaymentId});
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payment = _paymentManager.FindById(id).Data;

            if (payment == null)
            {
                return HttpNotFound();
            }

            payment.TotalPrice = _paymentManager.CalcuatePaymentPrice(payment);
            return View(payment);
        }

        // POST: Payment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Payment payment)
        {
            if (payment?.StudentId == 0)
            {
                return Json(new { Success = false, Message = "O Aluno é obrigatório para pagamentos" });
            }

            if (ModelState.IsValid)
            {
                payment.ModifyUserId = 0;
                payment.ModifyDate = DateTime.Now;
                _paymentManager.Update(payment);

                return Json(new { Success = true, Message = "Pagamento atualizado com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = _paymentManager.FindById(id).Data;
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = _paymentManager.FindById(id).Data;
            _paymentManager.Remove(payment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult UpdatePrices(Payment payment)
        {
            payment.TotalPrice = _paymentManager.CalcuatePaymentPrice(payment);

            return Json(new
            {
                payment.TotalPrice,
                TotalPriceFormated = payment.TotalPrice.ToString("C")
            }, JsonRequestBehavior.AllowGet);
        }

        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }
    }
}
