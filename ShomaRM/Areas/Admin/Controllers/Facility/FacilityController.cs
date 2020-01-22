using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class FacilityController : Controller
    {
        // GET: /Admin/Facility/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new FacilityModel().GetFacilityData(Id);
            return View("..\\Facility\\Edit", model);
        }
        public ActionResult SaveUpdateFacility(FacilityModel model)
        {
            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = new FacilityModel().SaveUpdateFacility(fb, model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FacilityDataList()
        {
            try
            {
                return Json(new { result = new FacilityModel().FacilityList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationFacilityList(FacilityModel.FacilitySearchModel model)
        {
            try
            {
                return Json(new { NOP = (new FacilityModel()).BuildPaganationFacilityList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillFacilitySearchGrid(FacilityModel.FacilitySearchModel model)
        {
            try
            {
                return Json((new FacilityModel()).FillFacilitySearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}