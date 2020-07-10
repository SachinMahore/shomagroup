using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class FobManagementController : Controller
    {
        // GET: Admin/FobManagement
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "fobmanagement";
            var getDetails = new FobManagementModel().GetDetails();
            return View(getDetails);
        }

        public ActionResult AddEdit(long id)
        {
            ViewBag.TenantID = id;
            var model = new FobManagementModel().GetDetailsForAddEdit(id);
            return View(model);
        }

        public JsonResult GetApplicantData(long ApplicantId)
        {
            try
            {
                return Json(new FobManagementModel().GetApplicantData(ApplicantId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveTenantFob(long TenantID, long ApplicantId, int Status, string FobKey,int OtherId)
        {
            try
            {
                return Json(new FobManagementModel().SaveTenantFob(TenantID, ApplicantId, Status, FobKey, OtherId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAllApplicantList(long TenantID)
        {
            try
            {
                return Json(new FobManagementModel().GetAllApplicantList(TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAllApplicant(long TenantID)
        {
            try
            {
                return Json(new FobManagementModel().GetAllApplicant(TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DelTenantFob(long TenantID, long ApplicantId, int OtherId)
        {
            try
            {
                return Json(new FobManagementModel().DelTenantFob(TenantID, ApplicantId, OtherId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetApplicantNamesForFob(long TenantID)
        {
            try
            {
                return Json(new FobManagementModel().GetApplicantNamesForFob(TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTenantFobData(long ApplicantId, long TenantID)
        {
            try
            {
                return Json(new FobManagementModel().GetTenantFobData(ApplicantId, TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveOtherTenantFob(long ApplicantId, long TenantID, string Fname, string Lname, string Relationship, string FobKey, int Status, long OtherId)
        {
            try
            {
                return Json(new FobManagementModel().SaveOtherTenantFob(ApplicantId, TenantID, Fname, Lname, Relationship, FobKey, Status, OtherId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuildPaganationPreMoving(FobManagementModel model)
        {
            try
            {
                return Json(new { NOP = new FobManagementModel().BuildPaganationPreMoving(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillPreMovingSearchGrid(FobManagementModel model)
        {
            try
            {
                return Json(new FobManagementModel().FillPreMovingSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult saveMoveInTime(string MoveInTime, long TenantID)
        {
            try
            {
                return Json(new FobManagementModel().saveMoveInTime(MoveInTime, TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}