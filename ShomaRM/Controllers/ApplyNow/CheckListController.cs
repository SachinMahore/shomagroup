using ShomaRM.Data;
using ShomaRM.Models;
using ShomaRM.Models.Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers
{
    public class CheckListController : Controller
    {
        // GET: CheckList
        public ActionResult Index()
        {
            ViewBag.UID = "0";
            if (ShomaGroupWebSession.CurrentUser != null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }

            return View();
        }
        public ActionResult SaveMoveInCheckList(CheckListModel model)
        {
            try
            {
                return Json(new { msg = (new CheckListModel().SaveMoveInCheckList(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadInsurenceDoc(CheckListModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadInsurenceDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadInsurenceDoc = Request.Files[i];

                }

                return Json(new { model = new CheckListModel().UploadInsurenceDoc(fileBaseUploadInsurenceDoc, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadProofOfElectricityDoc(CheckListModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadProofOfElectricityDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadProofOfElectricityDoc = Request.Files[i];

                }

                return Json(new { model = new CheckListModel().UploadProofOfElectricityDoc(fileBaseUploadProofOfElectricityDoc, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveMoveInPayment(ApplyNowModel model)
        {
            try
            {
                return Json(new { Msg = (new CheckListModel().SaveMoveInPayment(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> LeaseBlumoon()
        {
          
            var data = await LeaseBlumoonAsync();
            if (data != null)
            {

            }

            return File(data.leasePdf, "application/pdf", $"LeaseDocument_{0}.pdf");
        }

        public async System.Threading.Tasks.Task<LeaseResponseModel> LeaseBlumoonAsync()
        {

            var test = new BluemoonService();
            LeaseRequestModel leaseRequestModel = new LeaseRequestModel();

            ShomaRMEntities db = new ShomaRMEntities();
            string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            long UserID = Convert.ToInt64(uid);
            var appData = db.tbl_ApplyNow.Where(p => p.UserId == UserID).FirstOrDefault();
            long tid = 0;

            if (appData != null)
            {
                tid = appData.ID;
            }            

            var tenantdata = db.tbl_TenantOnline.Where(p => p.ProspectID == tid).FirstOrDefault();
            var GetCoappDet = db.tbl_Applicant.Where(c => c.TenantID == appData.ID).ToList();
            var GetVehicleList = db.tbl_Vehicle.Where(c => c.TenantID == appData.ID).ToList();
            var GetPetList = db.tbl_TenantPet.Where(c => c.TenantID == appData.ID).ToList();

            leaseRequestModel.UNIT_NUMBER ="Unit-"+appData.PropertyId.ToString();
         
            LeaseResponseModel authenticateData = await test.CreateSession();
            LeaseResponseModel leaseCreateResponse = await test.CreateLease(leaseRequestModel: leaseRequestModel, PropertyId: "112154", sessionId: authenticateData.SessionId);
            LeaseResponseModel leaseEditResponse = await test.EditLease(leaseRequestModel: leaseRequestModel, leaseId: leaseCreateResponse.LeaseId, sessionId: authenticateData.SessionId);
            LeaseResponseModel leasePdfResponse = await test.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: leaseCreateResponse.LeaseId);
            await test.CloseSession(sessionId: authenticateData.SessionId);
            return leasePdfResponse;


        }

    }

}