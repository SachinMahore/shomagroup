using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class TenantEventJoinApproveController : Controller
    {
        // GET: Admin/TenantEventJoinApprove
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTenantEventJoinList(int TenantEventListStatus)
        {
            try
            {
                return Json(new { model = new TenantEventJoinModel().GetTenantEventJoinList(TenantEventListStatus) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveTenantEventJoinApprove(long TEID, int TenantEventListStatus)
        {
            try
            {
                return Json(new { model = new TenantEventJoinApproveModel().SaveTenantEventJoinApprove(TEID, TenantEventListStatus) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteTenantEventJoinApprove(long TEJAID)
        {
            try
            {
                return Json(new { model = new TenantEventJoinApproveModel().DeleteTenantEventJoinApprove(TEJAID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}