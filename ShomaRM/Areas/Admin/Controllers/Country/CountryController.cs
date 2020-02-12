using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        // GET: Admin/Country
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCountryList(CountryList model)
        {
            try
            {
                return Json((new CountryModel()).GetCountryList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult BuildPaganationCountryList(CountryList model)
        {
            try
            {
                return Json(new { NOP = (new CountryModel()).BuildPaganationCountryList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCountryInfo(int CountryId = 0)
        {
            try
            {
                return Json((new CountryModel()).GetCountryInfo(CountryId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateCountry(CountryModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateCountry(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCountry(long CountryId)
        {
            try
            {
                return Json(new { model = new CountryModel().DeleteCountry(CountryId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}