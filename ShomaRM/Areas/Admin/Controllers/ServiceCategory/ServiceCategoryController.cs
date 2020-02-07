using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ServiceCategoryController : Controller
    {
        // GET: Admin/ServiceCategory
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            UsersModel model = new UsersModel();
            if (model.HasRight == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult fillSCategorySearchGrid(SCListModel model)
        {
            try
            {
                return Json((new ServiceCategoryModel()).FillSCSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult buildPaganationScategoryList(SCListModel model)
        {
            try
            {
                return Json(new { NOP = (new ServiceCategoryModel()).BuildPaganationSCList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetServiceCategoryInfo(int ServiceCategoryID = 0)
        {
            try
            {
                return Json((new ServiceCategoryModel()).GetServiceCategoryInfo(ServiceCategoryID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdateServiceCategory(ServiceCategoryModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateServiceCategory(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteServiceCategory(long ServiceIssueID)
        {
            try
            {
                return Json(new { model = new ServiceCategoryModel().DeleteServiceCategory(ServiceIssueID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}