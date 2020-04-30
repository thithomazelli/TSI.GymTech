using System;
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
    public class PortalUserController : Controller
    {
        private readonly TrainingSheetManager _trainingSheetManager;
        
        public PortalUserController()
        {
            _trainingSheetManager = new TrainingSheetManager();
        }

        // GET: PortalUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTrainingSheets()
        {
            var trainingSheetList = _trainingSheetManager.FindAllByView()
                .Data
                .Select(_ => new
                {
                    Id = _.TrainingSheetId,
                    _.Name,
                    _.Cycle,
                    _.StudentId,
                    _.StudentName,
                    Status = _.Status != null
                            ? GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.TrainingSheetStatus)),
                                Enum.GetName(typeof(TrainingSheetStatus), _.Status))
                            : null,
                    Type = GetResourceName(new ResourceManager(typeof(Entity.App_LocalResources.TrainingSheetType)),
                                Enum.GetName(typeof(TrainingSheetType), _.Type))
                });

            return Json(new { data = trainingSheetList }, JsonRequestBehavior.AllowGet);
        }

        // GET: PortalUser/Print/5
        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingSheet trainingSheet = _trainingSheetManager.FindById(id).Data;

            if (trainingSheet == null)
            {
                return HttpNotFound();
            }
            return View(trainingSheet);
        }
        
        public string GetResourceName(ResourceManager resourceManager, string enumName)
        {
            CultureInfo _cultureInfo = new CultureInfo("pt");
            return resourceManager.GetString(enumName, _cultureInfo);
        }

    }
}