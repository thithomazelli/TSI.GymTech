using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TSI.GymTech.Entity.Enumerates;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly PersonManager _personManager;
        
        public UserController()
        {
            _personManager = new PersonManager();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_personManager.FindByProfileType(PersonType.Student, false).Data);
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
                _personManager.Create(person);
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
                _personManager.Update(person);
                //return RedirectToAction("Index");
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

        public void SaveToDirectory()
        {
            var stream = Request.InputStream;
            string dump;

            using (var reader = new StreamReader(stream))
                dump = reader.ReadToEnd();

            var path = Server.MapPath("~/Images/Persons/test.jpg");
            System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapturePhoto(int? id, string base64image)
        {
            try
            {
                Person person = _personManager.FindById(id).Data;

                if (string.IsNullOrEmpty(base64image))
                    return Json(new { Type = "Error", Message = "Não foi possível carregar a imagem." });

                var pictureObj = string.Empty;

                if (base64image.IndexOf("png;base64") > 0)
                {
                    pictureObj = base64image.Replace("data:image/png;base64,", String.Empty);
                }
                else if (base64image.IndexOf("jpg;base64") > 0)
                {
                    pictureObj = base64image.Replace("data:image/jpg;base64,", String.Empty);
                }
                else if (base64image.IndexOf("jpeg;base64") > 0)
                {
                    pictureObj = base64image.Replace("data:image/jpeg;base64,", String.Empty);
                }

                var fileName = person.Photo ?? person.PersonId + "_" + person.Name + ".jpg";
                RemoveOldImage(fileName);
                fileName = person.PersonId + "_" + person.Name + ".jpg";

                var fullPath = Path.Combine(Server.MapPath("~/Images/Persons/"), fileName);
                string converted = pictureObj.Replace('-', '+');
                converted = converted.Replace('_', '/');
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(pictureObj)))
                {
                    using (Bitmap bm1 = new Bitmap(ms))
                    {
                        bm1.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }

                person.Photo = fileName;
                _personManager.Update(person);

                return Json(new { Type = "Success", Message = "A nova imagem foi capturada com sucesso.", ImageName = fileName });
            }
            catch(Exception ex)
            {
                return Json(new { Type = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemovePhoto(int? id)
        {
            Person person = _personManager.FindById(id).Data;
            
            if (RemoveOldImage(person.Photo))
            {
                person.Photo = null;
                _personManager.Update(person);
                return Json(new { Type = "Success", Message = "A imagem foi removida com sucesso." });
            }
            else
            {
                return Json(new { Type = "Error", Message = "Não foi possível remover a imagem do Usuário." });
            }
            
        }

        private bool RemoveOldImage(string fileName)
        {
            try
            {
                var fullPath = Path.Combine(Server.MapPath("~/Images/Persons/"), fileName);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}