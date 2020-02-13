using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ProspectVerificationController : Controller
    {
        // GET: Admin/ProspectVerification
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "prospectverification";
            return View();
        }
        public ActionResult GetProspectVerificationList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new ProspectVerificationModel().GetProspectVerificationList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
     
        public ActionResult EditProspect(int id)
        {
            ViewBag.ActiveMenu = "prospectverification";
            if (id != 0)
            {
                var model = new ProspectVerificationModel().GetProspectData(id);
                return View("..\\ProspectVerification\\EditProspect", model);
            }
            else
            {
                return RedirectToAction("../ProspectVerification/Index");
            }
        }
        public ActionResult SaveProspectVerification(long PID, long DocId, int VerificationStatus)
        {
            try
            {
                new ProspectVerificationModel().SaveProspectVerification(DocId, VerificationStatus, PID);
                return Json(new { msg ="Data Updated Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PDFBuilder(string TenantID)
        {
            try
            {
                return Json(new { result = (new ProspectVerificationModel()).PDFBuilder(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationProspectVerifyList(ProspectVerificationModel.ProspectVerifySearchModel model)
        {
            try
            {
                return Json(new { NOP = (new ProspectVerificationModel()).BuildPaganationProspectVerifyList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillProspectVerifySearchGrid(ProspectVerificationModel.ProspectVerifySearchModel model)
        {
            try
            {
                return Json((new ProspectVerificationModel()).FillProspectVerifySearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveScreeningStatus(string Email, long ProspectId, string Status)
        {
            try
            {
                return Json(new { model = new ProspectVerificationModel().SaveScreeningStatus(Email,ProspectId, Status) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}