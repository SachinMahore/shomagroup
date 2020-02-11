using ShomaRM.Areas.Tenant.Models.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers.Club
{
    public class ClubManagementController : Controller
    {
        // GET: Admin/ClubManagement
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetClubManagementList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new ClubModel().GetClubList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateClubManagement(long Id, bool Active)
        {
            try
            {
                return Json(new { model = new ClubModel().UpdateClubActiveDeactive(Id:Id,Active: Active) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}