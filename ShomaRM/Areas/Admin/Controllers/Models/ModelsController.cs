using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ModelsController : Controller
    {
        // GET: Admin/Models
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            var model = new ModelsModel().GetModelsData(Id);
            return View("..\\Models\\Edit", model);
        }

        //Save Update Method

        public ActionResult SaveUpdateModels(ModelsModel model)
        {
            try
            {
                return Json(new { model = new ModelsModel().SaveUpdateModels(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getModelsList()
        {
            try
            {
                return Json(new { model =new ModelsModel().GetModelsListDetail()},JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPropertyModelDetails(string ModelName)
        {
            try
            {
                return Json(new { model = new ModelsModel().GetPropertyModelDetails(ModelName) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UploadModelsFloorPlan(ModelsModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }

                return Json(new { model = new ModelsModel().UploadModelsFloorPlan(fb, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UploadModelsFloorPlanDetails(ModelsModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }

                return Json(new { model = new ModelsModel().UploadModelsFloorPlanDetails(fb, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}