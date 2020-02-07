using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class MyAccountController : Controller
    {
        // GET: Tenant/MyAccount
        public ActionResult Index(int ID)
        {
            ViewBag.ActiveMenu = "myhome";
            ViewBag.TenantID = ID;
            if(Session["ARID"] !=null)
            {
                ViewBag.ARID = Session["ARID"].ToString();
                Session["ARID"] = null;
            }
            else
            {
                ViewBag.ARID = "0";
            }
            
            if (Session["StepIdMakePayment1"] != null && Session["PayStepIdMakePayment"] != null)
            {
                ViewBag.StepIdMakePayment1 = Session["StepIdMakePayment1"].ToString();
                ViewBag.PayStepIdMakePayment = Session["PayStepIdMakePayment"].ToString();

                Session["StepIdMakePayment1"] = null;
                Session["PayStepIdMakePayment"] = null;
            }

            if (Session["StepIdMakePayment2"] != null && Session["PayStepIdRecurringPayment"] != null)
            {
                ViewBag.StepIdMakePayment2 = Session["StepIdMakePayment2"].ToString();
                ViewBag.PayStepIdRecurringPayment = Session["PayStepIdRecurringPayment"].ToString();

                Session["StepIdMakePayment2"] = null;
                Session["PayStepIdRecurringPayment"] = null;
            }

            if (Session["StepRegisterGuest"] != null)
            {
                ViewBag.StepRegisterGuest = Session["StepRegisterGuest"].ToString();
                

                Session["StepRegisterGuest"] = null;
               
            }
            if (Session["StepReserveAmenities"] != null && Session["PayStepIdReserveAmenities"] != null)
            {
                ViewBag.StepReserveAmenities = Session["StepReserveAmenities"].ToString();
                ViewBag.PayStepIdReserveAmenities = Session["PayStepIdReserveAmenities"].ToString();

                Session["StepReserveAmenities"] = null;
                Session["PayStepIdReserveAmenities"] = null;
            }

            if (Session["StepIdServiceRequest2"] != null && Session["ServiceStepIdSubmitServiceRequest"] != null)
            {
                ViewBag.StepServiceRequest2 = Session["StepIdServiceRequest2"].ToString();
                ViewBag.ServiceRequestStepIdSubmitServiceRequest = Session["ServiceStepIdSubmitServiceRequest"].ToString();

                Session["StepIdServiceRequest2"] = null;
                Session["ServiceStepIdSubmitServiceRequest"] = null;
            }
          
            return View();
        }
        public ActionResult GetTenantInfo(long TenantID,long UserId)
        {
            try
            {
                return Json((new MyAccountModel()).GetTenantInfo(TenantID, UserId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateTenant(MyAccountModel model)
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
        public ActionResult LeaseInformation()
        {
            return View();
        }
        public ActionResult GetTenantDetails(long TenantID)
        {
            try
            {
                return Json((new MyAccountModel()).GetTenantDetails(TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateContactInfo(MyAccountModel model,long UserId)
        {
            try
            {
                return Json(new { result = 1, ID = model.UpdateContactInfo(model, UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateEmContactInfo(MyAccountModel model,long UserId)
        {
            try
            {
                return Json(new { result = 1, ID = model.UpdateEmContactInfo(model,UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateWorkInfo(MyAccountModel model,long UserId)
        {
            try
            {
                return Json(new { msg = new MyAccountModel().UpdateWorkInfo(model,UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllLeaseDocuments(MyAccountModel model)
        {
            try
            {
                return Json(new { model = new MyAccountModel().GetAllLeaseDocuments(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPetLeaseDocuments(MyAccountModel model)
        {
            try
            {
                return Json(new { model = new MyAccountModel().GetPetLeaseDocuments(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetVehicleLeaseDocuments(MyAccountModel model)
        {
            try
            {
                return Json(new { model = new MyAccountModel().GetVehicleLeaseDocuments(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAmenityList()
        {
            try
            {
                return Json(new { model = new MyAccountModel().GetAmenityList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillRRList(AmenitiesReservationModel model)
        {
            try
            {
                return Json((new AmenitiesReservationModel()).FillRRList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTenantLeaseDocuments(MyAccountModel model)
        {
            try
            {
                return Json(new { model = new MyAccountModel().GetTenantLeaseDocuments(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }

}