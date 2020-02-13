using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class GuestRegistrationController : Controller
    {
        // GET: Tenant/GuestRegistration
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UploadGuestDriverLicence(GuestRegistrationModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseGuestDriverLicence = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseGuestDriverLicence = Request.Files[i];

                }

                return Json(new { model = new GuestRegistrationModel().UploadGuestDriverLicence(fileBaseGuestDriverLicence , model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UploadGuestVehicleRegistration(GuestRegistrationModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseGuestVehicleRegistration = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseGuestVehicleRegistration = Request.Files[i];

                }

                return Json(new { model = new GuestRegistrationModel().UploadGuestVehicleRegistration(fileBaseGuestVehicleRegistration, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveUpdateGuestRegistration(GuestRegistrationModel model)
        {
            try
            {
                return Json(new { model = new GuestRegistrationModel().SaveUpdateGuestRegistration(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetTagInfo(long TagId)
        {
            try
            {
                return Json(new { msg = (new GuestRegistrationModel().gotiGuestList(TagId)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { msg = (Ex.Message) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}