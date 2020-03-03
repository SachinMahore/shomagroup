using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class TenantController : Controller
    {
        // GET: Admin/Tenant
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "tenant";
            UsersModel model = new UsersModel();
            if (model.HasRight == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult BuildPaganationTenantList(DateTime FromDate, DateTime ToDate, int NumberOfRows,string SortBy, string OrderBy)
        {
            try
            {
                return Json(new { PageNumber = (new TenantModel()).BuildPaganationTenantList(FromDate, ToDate, NumberOfRows, SortBy, OrderBy) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantList(TenantModel.TenantSearch model)
        {
            try
            {
                return Json((new TenantModel()).GetTenantList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillPropertyDropDownList()
        {
            try
            {
                return Json((new TenantModel()).FillPropertyDropDownList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillStateDropDownList()
        {
            try
            {
                return Json((new CityModel()).FillStateDropDownList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillTenantDropDownList(long PID)
        {
            try
            {
                return Json(new { result = new TenantModel().FillTenantDropDownList(PID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult New()
        {
            ViewBag.ActiveMenu = "tenant";
            ViewBag.TenantID = 0;
            //var model = new TenantModel().GetTenantInfo(0);
            return View("AddEdit");
        }
        public ActionResult Edit(Int32 ID)
        {
            ViewBag.ActiveMenu = "tenant";
            if (ID != 0)
            {
                ViewBag.TenantID = ID;
                return View("AddEdit");
            }
            else
            {
                return RedirectToAction("../Tenant/Index");
            }
        }
        public ActionResult GetTenantInfo(long TenantID = 0)
        {
            try
            {
                return Json((new TenantModel()).GetTenantInfo(TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateTenant(TenantModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateTenant(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult OnlinePToTenant(TenantOnlineModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.OnlinePToTenant(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Amit's Work

        public ActionResult SaveTenantOnline(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveTenantOnline(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getTenantOnlineData(int id)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().getTenantOnlineData(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SaveUpdatePostDisclaimer(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveUpdatePostDisclaimer(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}