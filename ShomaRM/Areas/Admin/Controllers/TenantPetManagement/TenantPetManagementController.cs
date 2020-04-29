using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class TenantPetManagementController : Controller
    {
        // GET: Admin/TenantPetManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FillTenantPetSearchGrid(TenantPetSearchListModel model)
        {
            try
            {
                return Json((new TenantPetSearchListModel()).FillTenantPetSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildTenantPetPlaceList(TenantPetSearchListModel model)
        {
            try
            {
                return Json(new { NOP = (new TenantPetSearchListModel()).BuildPaganationTenantPetList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdateTenantPet(TenantPetListModel model)
        {
            try
            {
                return Json(new { msg = (new TenantPetListModel().SaveUpdateTenantPet(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateWeight(TenantPetListModel model)
        {
            try
            {
                return Json(new { msg = (new TenantPetListModel().UpdateWeight(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateUniqId(TenantPetListModel model)
        {
            try
            {
                return Json(new { msg = (new TenantPetListModel().UpdateUniqId(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateExpiryDate(TenantPetListModel model)
        {
            try
            {
                return Json(new { msg = (new TenantPetListModel().UpdateExpiryDate(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SendRemainder(TenantPetListModel model)
        {
            try
            {
                return Json(new { msg = (new TenantPetListModel().SendRemainder(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}