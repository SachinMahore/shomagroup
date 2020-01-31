using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class ServiceRequestController : Controller
    {
        // GET: Tenant/ServiceRequest
        public ActionResult Index(int Id)
        {
            return View();
        }

        public JsonResult SaveUpdateServiceRequest(ServiceRequestModel model)
        {
            try
            {
                return Json(new { model = new ServiceRequestModel().SaveUpdateServiceRequest(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetServiceInfo(ServiceRequestModel model)
        {
            try
            {
                return Json(new { msg = (new ServiceRequestModel().GetServiceInfo(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { msg = (Ex.Message) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetServiceRequestList(ServiceRequestModel model)
        {
            try
            {
                return Json(new { model = new ServiceRequestModel().GetServiceRequestList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelServiceRequest(ServiceRequestModel model)
        {
            try
            {
                return Json(new { model = new ServiceRequestModel().CancelServiceRequest(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetServiceRequestForAlarm(long TenantId)
        {
            try
            {
                return Json(new { model = new ServiceRequestModel().GetServiceRequestForAlarm(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDdlLocation()
        {
            try
            {
                return Json(new { model = new ServiceRequestModel().GetDdlLocation() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetDdlServiceCategory()
        {
            try
            {
                return Json(new { model = new ServiceRequestModel().GetDdlServiceCategory() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetDdlCausingIssue(long ServiceIssueID)
        {
            try
            {
                return Json((new ServiceRequestModel()).GetDdlCausingIssue(ServiceIssueID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDdlIssue(long CausingIssueID, long ServiceIssueID)
        {
            try
            {
                return Json((new ServiceRequestModel()).GetDdlIssue(CausingIssueID, ServiceIssueID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadServiceFile(ServiceRequestModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload = Request.Files[i];

                }

                return Json(new { model = new ServiceRequestModel().UploadServiceFile(fileBaseUpload, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}