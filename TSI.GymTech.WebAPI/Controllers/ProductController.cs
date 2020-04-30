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
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Manager.Utitlities;
using System.Resources;
using System.Globalization;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;
        private PhotoManager _photoManager;
        private ValidationErrorManager _validationErrorManager;

        public ProductController()
        {
            _productManager = new ProductManager();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            var productList = _productManager.FindAll().Data
                .Select(_ => new
                {
                    Id = _.ProductId,
                    _.Name,
                    Type = _.Type != null
                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.ProductType)),
                            Enum.GetName(typeof(ProductType), _.Type))
                        : null,
                    Status = _.Status != null
                        ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.ProductStatus)),
                            Enum.GetName(typeof(ProductStatus), _.Status))
                        : null,
                    SuggestedPrice = _.SuggestedPrice.ToString("C"),
                    _.QuantityStock,
                    _.Quota
                });

            return Json(new { data = productList }, JsonRequestBehavior.AllowGet);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var model = new Product
            {
                Status = ProductStatus.Active
            };
            return View(model);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            ValidateDuplicated(product);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                product.CreateUserId = 0;
                product.CreateDate = DateTime.Now;
                product.ModifyUserId = 0;
                product.ModifyDate = DateTime.Now;
                _productManager.Create(product);

                return Json(new { Success = true, Message = "Produto cadastrado com sucesso.", Id = product.ProductId });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _productManager.FindById(id).Data;

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            ValidateDuplicated(product);
            if (ModelState.IsValid)
            {
                //Change to current user id later
                product.ModifyUserId = 0;
                product.ModifyDate = DateTime.Now;
                _productManager.Update(product);

                return Json(new { Success = true, Message = "Produto atualizado com sucesso." });
            }

            _validationErrorManager = new ValidationErrorManager();
            return Json(new { success = false, Errors = _validationErrorManager.GetModelStateErrors(ModelState) }, JsonRequestBehavior.AllowGet);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productManager.FindById(id).Data;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _productManager.FindById(id).Data;
            
            try
            {
                _productManager.Remove(product);
                return Json(new { Type = "Success", Message = "O Produto " + product.Name + " foi removido com sucesso." });
            }
            catch (Exception ex)
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover o Produto " + product.Name + "." });
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
                Product product = _productManager.FindById(id).Data;
                product.Photo = product.Photo ?? product.ProductId + "_" + product.Name + "." + fileExtension;
                _photoManager = new PhotoManager(Server.MapPath("~/Images/Products/"));

                if (_photoManager.CapturePhoto(base64image, product.Photo) == ResultEnum.Success)
                {
                    _productManager.Update(product);
                    return Json(new { Type = "Success", Message = "A nova imagem foi capturada com sucesso.", ImageName = product.Photo });
                }
                else
                {
                    return Json(new { Type = "Error", Message = "Não foi possível capturar a nova imagem.", ImageName = product.Photo });
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
            Product product = _productManager.FindById(id).Data;
            _photoManager = new PhotoManager(Server.MapPath("~/Images/Products/"));

            if (_photoManager.RemovePhoto(product.Photo) == ResultEnum.Success)
            {
                product.Photo = null;
                _productManager.Update(product);
                return Json(new { Type = "Success", Message = "A imagem foi removida com sucesso." });
            }
            else
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover a imagem do Produto." });
            }
        }

        private void ValidateDuplicated(Product product)
        {
            // Validate if Name is duplicated
            if (_productManager.IsNameDuplicated(product))
            {
                ModelState.AddModelError("Name", "Já existe um Produto cadastrado com o Nome informado.");
            }
        }

        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }
    }
}
