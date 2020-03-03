using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Models.Bluemoon;

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
        public async System.Threading.Tasks.Task<ActionResult> GetLeaseDocBlumoonAdm(string LeaseId, string EsignatureId)
        {
            try
            {
                var data = await GetLeaseDocBlumoonAsyncAdm(LeaseId, EsignatureId);
                if (data != null)
                {
                    System.IO.File.WriteAllBytes(Server.MapPath("/Content/assets/img/Document/LeaseDocument_" + data.LeaseId + ".pdf"), data.leasePdf);
                }
                return Json(new { LeaseId = data.LeaseId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { LeaseId = "0" }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public async System.Threading.Tasks.Task<LeaseResponseModel> GetLeaseDocBlumoonAsyncAdm(string LeaseId, string EsignatureId)
        {
            var bmservice = new BluemoonService();
            LeaseResponseModel authenticateData = await bmservice.CreateSession();
            if (!string.IsNullOrWhiteSpace(EsignatureId))
            {
                LeaseResponseModel leaseDocumentWithEsignature = await bmservice.GetLeaseDocumentWithEsignature(SessionId: authenticateData.SessionId, EsignatureId: EsignatureId);
                await bmservice.CloseSession(sessionId: authenticateData.SessionId);
                leaseDocumentWithEsignature.LeaseId = LeaseId;
                return leaseDocumentWithEsignature;
            }
            else
            {
                LeaseResponseModel leasePdfResponse = await bmservice.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: LeaseId);
                await bmservice.CloseSession(sessionId: authenticateData.SessionId);
                leasePdfResponse.LeaseId = LeaseId;
                return leasePdfResponse;
            }
        }
    }
}