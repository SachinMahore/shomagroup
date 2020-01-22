using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class FOBController : Controller
    {
        // GET: Admin/Storage
        public ActionResult Index(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new FOBModel().GetStorageData(Id);
            return View("..\\FOB\\Index", model);
        }
        public ActionResult GetStorageList()
        {
            try
            {
                return Json((new FOBModel()).GetStorageList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetStorageInfo(int StorageID = 0)
        {
            try
            {
                return Json((new FOBModel()).GetStorageInfo(StorageID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateStorage(FOBModel model)
        {
            try
            {
                return Json(new { result = 1, StorageID = model.SaveUpdateStorage(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetStorageSearchList(string SearchText)
        {
            try
            {
                return Json((new FOBModel()).GetStorageSearchList(SearchText), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //For TenantFOBModel

        public ActionResult GetTenantStorageList(long TenantId)
        {
            try
            {
                return Json((new TenantFOBModel()).GetTenantStorageList(TenantId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateTenantStorage(TenantFOBModel model)
        {
            try
            {
                return Json(new { result = 1, totalStorageAmt = model.SaveUpdateTenantStorage(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillStorageSearchGrid(FOBListModel model)
        {
            try
            {
                return Json((new FOBModel()).FillStorageSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationStorageList(FOBListModel model)
        {
            try
            {
                return Json(new { NOP = (new FOBModel()).BuildPaganationStorageList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteFOB(long StorageID)
        {
            try
            {
                return Json(new { model = new FOBModel().DeleteFOB(StorageID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}