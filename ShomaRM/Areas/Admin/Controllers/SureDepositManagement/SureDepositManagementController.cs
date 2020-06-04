using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class SureDepositManagementController : Controller
    {
        // GET: Admin/SureDepositManagement
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "sdmanagement";
            var getDetails = new SureDepositManagementModel().GetDetails();
            return View(getDetails);
        }
        public ActionResult UploadSureDeposit(long ProspectId)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadInsurenceDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadInsurenceDoc = Request.Files[i];

                }

                return Json(new { model = new SureDepositManagementModel().UploadSureDeposit(fileBaseUploadInsurenceDoc, ProspectId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateSureDeposit(SureDepositManagementModel model)
        {
            try
            {
                string msg = new SureDepositManagementModel().SaveUpdateSureDeposit(model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantSDDetails(int id, long TenantID)
        {
            try
            {
                return Json(new { model = new SureDepositManagementModel().GetTenantSDDetails(id, TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}