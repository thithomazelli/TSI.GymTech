using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;
using TSI.GymTech.Manager.Result;
using TSI.GymTech.Manager.Utitlities;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;
        private PhotoManager _photoManager;

        public ProductController()
        {
            _productManager = new ProductManager();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View(_productManager.FindAll().Data);
        }
        
        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,Type,Status,SuggestedPrice,QuantityStock,Duplication,Photo,Comments,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productManager.Create(product);
                return RedirectToAction("Edit/" + product.ProductId);
            }

            return View(product);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description,Type,Status,SuggestedPrice,QuantityStock,Duplication,Photo,Comments,CreateDate,CreateUserId,ModifyDate,ModifyUserId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productManager.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
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
    }
}
