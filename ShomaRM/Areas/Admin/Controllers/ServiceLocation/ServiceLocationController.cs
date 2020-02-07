using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ServiceLocationController : Controller
    {
        // GET: Admin/ServiceLocation
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
        public ActionResult fillLocationSearchGrid(SLListModel model)
        {
            try
            {
                return Json((new ServiceLocationModel()).FillLSSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationSLList(SLListModel model)
        {
            try
            {
                return Json(new { NOP = (new ServiceLocationModel()).BuildPaganationSLList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetServiceLocationInfo(int LocationID = 0)
        {
            try
            {
                return Json((new ServiceLocationModel()).GetServiceLocationInfo(LocationID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult saveUpdateLocation(ServiceLocationModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateServiceLocation(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteServiceLocation(long LocationID)
        {
            try
            {
                return Json(new { model = new ServiceLocationModel().DeleteServiceLocation(LocationID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }


    }
}