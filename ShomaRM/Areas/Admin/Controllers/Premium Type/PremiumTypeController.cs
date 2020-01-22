using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers.Premium_Type
{
    public class PremiumTypeController : Controller
    {
        // GET: Admin/PremiumType
        public ActionResult Index()
        {
            return View();
        }

        // Save update method

        public ActionResult SaveUpdatePremiumType(PremiumTypeModel model)
        {
            try
            {
                return Json(new { model = new PremiumTypeModel().SaveUpdatePremiumType(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPremiumTypeList(string SearchText)
        {
            try
            {
                return Json(new { model = new PremiumTypeModel().GetPremiumTypeList(SearchText) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPremiumTypeDetails(int Id)
        {
            try
            {
                return Json(new { model = new PremiumTypeModel().GetPremiumTypeDetails(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeletePremiumTypeDetails(int Id)
        {
            try
            {
                new PremiumTypeModel().DeletePremiumTypeDetails(Id);
                return Json(new { result =1  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}