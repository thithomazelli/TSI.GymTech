using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class PaymentProductController : Controller
    {
        private PaymentProductManager _paymentProductManager;
        private ValidationErrorManager _validationErrorManager;

        public PaymentProductController()
        {
            _paymentProductManager = new PaymentProductManager();
        }

        // GET: PaymentProduct/Create
        public ActionResult Create(int? paymentId)
        {
            if (paymentId == null)
            {
                _validationErrorManager = new ValidationErrorManager();
                return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = new PaymentProduct();
                var productManager = new ProductManager();

                List<Product> products = new List<Product>();
                products = productManager.FindAll().Data.ToList();
                ViewBag.ProductId = new SelectList(products, "ProductId", "Name", model.ProductId);

                var firstProduct = products.FirstOrDefault();

                model.Quantity = 1;
                model.Discount = 0;
                model.Quota = firstProduct.Quota;
                model.UnitPrice = firstProduct.SuggestedPrice;
                model.TotalPrice = _paymentProductManager.CalcuatePaymentProductPrice(model);

                return View(model);
            }
        }

        // POST: PaymentProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentProduct paymentProduct)
        {
            if (!ModelState.IsValid)
            {
                _validationErrorManager = new ValidationErrorManager();
                return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
            }

            if (paymentProduct != null)
            {
                var productManager = new ProductManager();
                var product = productManager.FindById(paymentProduct.ProductId).Data;
                
                for (var counter = 1; counter <= product.Quota; counter++)
                {
                    _paymentProductManager = new PaymentProductManager();
                    var quota = product.Quota ?? 1;
                    var newPaymentProduct = paymentProduct;

                    newPaymentProduct.Description = quota > 1 
                        ? string.Format("{0} - {1}/{2}", product.Description, counter, product.Quota)
                        : product.Description;

                    newPaymentProduct.Quota = counter;
                    newPaymentProduct.UnitPrice = product.SuggestedPrice;
                    newPaymentProduct.CreateUserId = 0;
                    newPaymentProduct.CreateDate = DateTime.Now;
                    newPaymentProduct.ModifyUserId = 0;
                    newPaymentProduct.ModifyDate = DateTime.Now;

                    _paymentProductManager.Create(paymentProduct);
                }

            }

            return RedirectToAction("Edit/" + paymentProduct.PaymentId, "Payment");
        }

        // GET: PaymentProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentProduct paymentProduct = _paymentProductManager.FindById(id).Data;

            if (paymentProduct == null)
            {
                return HttpNotFound();
            }

            var productManager = new ProductManager();
            var product = productManager.FindById(paymentProduct.ProductId).Data;

            List<Product> products = new List<Product>();
            products = productManager.FindAll().Data.ToList();
            ViewBag.ProductId = new SelectList(products, "ProductId", "Name", paymentProduct.ProductId);
            
            return View(paymentProduct);
        }

        // POST: PaymentProduct/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentProduct paymentProduct)
        {
            if (!ModelState.IsValid)
            {
                _validationErrorManager = new ValidationErrorManager();
                return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
            }

            if (paymentProduct != null)
            {
                var productManager = new ProductManager();
                var product = productManager.FindById(paymentProduct.ProductId).Data;

                //Change to current
                paymentProduct.UnitPrice = product.SuggestedPrice;
                paymentProduct.ModifyUserId = 0;
                paymentProduct.ModifyDate = DateTime.Now;
                _paymentProductManager.Update(paymentProduct);
            }

            return RedirectToAction("Edit", "Payment", new { id = paymentProduct.PaymentId });
        }

        // GET: PaymentProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentProduct paymentProduct = _paymentProductManager.FindById(id).Data;

            if (paymentProduct == null)
            {
                return HttpNotFound();
            }

            return View(paymentProduct);
        }

        // POST: TrainingSheetExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentProduct paymentProduct = _paymentProductManager.FindById(id).Data;

            try
            {
                _paymentProductManager.Remove(paymentProduct);

                var paymentManager = new PaymentManager();
                var payment = paymentManager.FindById(paymentProduct.PaymentId).Data;

                return Json(new
                {
                    Type = "Success",
                    Message = "O produto do pagamento foi removido com sucesso.",
                    payment.TotalPrice,
                    TotalPriceFormated = payment.TotalPrice.ToString("C")
                });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o produto do pagamento." });
            }
        }

        [HttpGet]
        public JsonResult UpdateProductValues(PaymentProduct paymentProduct)
        {
            var productManager = new ProductManager();
            var product = productManager.FindById(paymentProduct.ProductId).Data;
            var productQuota = product.Quota ?? 1;

            paymentProduct.UnitPrice = product.SuggestedPrice;
            paymentProduct.Quota = product.Quota ?? 1;
            paymentProduct.TotalPrice = productQuota > 0 
                ? _paymentProductManager.CalcuatePaymentProductPrice(paymentProduct) / productQuota
                : _paymentProductManager.CalcuatePaymentProductPrice(paymentProduct);
            
            return Json(new
            {
                paymentProduct.Quantity,
                paymentProduct.Discount,
                paymentProduct.UnitPrice,
                paymentProduct.TotalPrice,
                paymentProduct.Quota
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
