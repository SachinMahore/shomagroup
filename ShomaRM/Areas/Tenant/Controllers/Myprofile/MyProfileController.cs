using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers.Myprofile

{
    public class MyProfileController : Controller
    {
        // GET: Tenant/MyProfil
        public ActionResult Index(int ID)
        {
            ViewBag.ActiveMenu = "myprofile";
            ViewBag.TenantID = ID;
           
            return View();
        }
        public JsonResult SetSessionMakePayments(int StepId, int PayStepId)
        {
            try
            {
                Session["StepIdMakePayment1"] = StepId.ToString();
                Session["PayStepIdMakePayment"] = PayStepId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SetSessionSetRecurringPayments(int StepId, int PayStepId)
        {
            try
            {
                Session["StepIdMakePayment2"] = StepId.ToString();
                Session["PayStepIdRecurringPayment"] = PayStepId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SetSessionSetReserveAmenities(int StepId, int PayStepId)
        {
            try
            {
                Session["StepReserveAmenities"] = StepId.ToString();
                Session["PayStepIdReserveAmenities"] = PayStepId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SetSessionSetRegisterGuest(int StepId)
        {
            try
            {
                Session["StepRegisterGuest"] = StepId.ToString();

                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SetSessionSetSubmitServiceRequest(int StepId, int ServiceRequestId)
        {
            try
            {
                Session["StepIdServiceRequest2"] = StepId.ToString();
                Session["ServiceStepIdSubmitServiceRequest"] = ServiceRequestId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UploadProfile(MyAccountModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload = Request.Files[i];

                }

                return Json(new { model = new MyAccountModel().UploadProfile(fileBaseUpload, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveProfilePicture(MyAccountModel model)
        {
            try
            {
                return Json(new { msg = new MyAccountModel().SaveProfilePic(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}