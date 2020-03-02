using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ServicesManagementController : Controller
    {
        // GET: Admin/ServicesManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuildPaganationUserList(ServicesManagementModel.ServicesSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new ServicesManagementModel()).BuildPaganationUserList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(Int32 ID)
        {
            ViewBag.ActiveMenu = "admin";
            ViewBag.ServiceID = ID;
            //var model = new UsersModel().GetUserInfo(ID);
            return View("AddEdit");
        }
        public ActionResult FillServicesSearchGrid(ServicesManagementModel.ServicesSearchModel model)
    {
            try
            {
                return Json(new { model = new ServicesManagementModel().FillServicesSearchGrid(model)}, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
        }
    }
        public JsonResult goToServiceDetails(long ServiceID)
        {
            try
            {
                return Json(new { model = new ServicesManagementModel().goToServiceDetails(ServiceID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //public JsonResult StatusUpdateServiceRequest(ServicesManagementModel model)
        //{
        //    try
        //    {
        //        return Json(new { model = new ServicesManagementModel().StatusUpdateServiceRequest(model) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult StatusUpdateForServicePerson(ServicesManagementModel model)
        {
            try
            {
                return Json(new { model = new ServicesManagementModel().StatusUpdateForServicePerson(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadServiceFile(ServicesManagementModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload = Request.Files[i];

                }

                return Json(new { model = new ServicesManagementModel().UploadServiceFile(fileBaseUpload, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult OwnerSignatureFile(ServicesManagementModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload = Request.Files[i];

                }

                return Json(new { model = new ServicesManagementModel().OwnerSignatureFile(fileBaseUpload, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveUpdateEstimate(ServicesManagementModel.EstimateModel model)
        {
            try
            {
                return Json(new { model = new ServicesManagementModel.EstimateModel().SaveUpdateEstimate(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillEstimateList()
        {
            try
            {
                return Json(new { model = new ServicesManagementModel.EstimateModel().FillEstimateList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEstimateData(ServicesManagementModel.EstimateModel model)
        {
            try
            {
                return Json(new { model = (new ServicesManagementModel.EstimateModel().GetEstimateData(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = (Ex.Message) }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillAssignmentAuditHistoryList(ServicesManagementModel model)
        {
            try
            {
                return Json(new { model = new ServicesManagementModel().FillAssignmentAuditHistoryList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}