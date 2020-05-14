using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class StorageController : Controller
    {
        // GET: Admin/Storage
        public ActionResult Index(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new StorageModel().GetStorageData(Id);
            return View("..\\Storage\\Index",model);
        }
        public ActionResult GetStorageList(long TenantID, string OrderBy, string SortBy)
        {
            try
            {
                return Json((new StorageModel()).GetStorageList(TenantID, OrderBy, SortBy), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetStorageData(int Id)
        {
            try
            {
                return Json((new StorageModel()).GetStorageData(Id), JsonRequestBehavior.AllowGet);
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
                return Json((new StorageModel()).GetStorageInfo(StorageID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateStorage(StorageModel model)
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
                return Json((new StorageModel()).GetStorageSearchList(SearchText), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //For TenantStorageModel

        public ActionResult GetTenantStorageList(long TenantId)
        {
            try
            {
                return Json((new TenantStorageModel()).GetTenantStorageList(TenantId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateTenantStorage(TenantStorageModel model)
        {
            try
            {
                string[] storageDetails = model.SaveUpdateTenantStorage(model).Split('|');

                return Json(new { result = storageDetails[0], msg= storageDetails[1], totalStorageAmt = storageDetails[2] }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillStorageSearchGrid(StorageListModel model)
        {
            try
            {
                return Json((new StorageModel()).FillStorageSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationStorageList(StorageListModel model)
        {
            try
            {
                return Json(new { NOP = (new StorageModel()).BuildPaganationStorageList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteStorage(long StorageID)
        {
            try
            {
                return Json(new { model = new StorageModel().DeleteStorage(StorageID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}