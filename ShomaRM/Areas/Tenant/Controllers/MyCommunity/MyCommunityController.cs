using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Areas.Tenant.Models.Club;
using ShomaRM.Areas.Tenant.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class MyCommunityController : Controller
    {
        // GET: Tenant/MyCommunity
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "mycommunity";
            ClubModel Model = new ClubModel();
            ViewBag.CallenderHidden = "";
            return View(Model);
        }

        public JsonResult CreateClub(ClubModel _model)
        {
            try
            {
                return Json(new { modal = new ClubModel().SaveclubEvent(_model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { modal = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       
        public ActionResult JoinClubPartial(string Search)
        {
            var Model = new ClubModel().GetClubList(UserId:0);
            ViewBag.CallenderHidden = "";
            return PartialView("~/Areas/Tenant/Views/MyCommunity/_JoinClub.cshtml", Model);
        }

        public ActionResult JoinClubPartialByUser(string Search,long UserId)
        {
            var Model = new ClubModel().GetClubList(UserId: UserId);
            ViewBag.CallenderHidden = "hidden";
            return PartialView("~/Areas/Tenant/Views/MyCommunity/_JoinClub.cshtml", Model);
        }

        public JsonResult GetClubById(long Id)
        {
            try
            {
                return Json(new { model = new ClubModel().GetClubbyId(ClubId: Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAmenitiesList()
        {
            try
            {
                return Json(new { model = new MyCommunityModel().GetAmenitiesList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEventDataForTenant(int EventID)
        {
            try
            {
                return Json(new { modal = new EventModel().GetEventData(EventID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { modal = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveTenantEventJoin(TenantEventJoinModel model)
        {
            try
            {
                return Json(new { modal = new TenantEventJoinModel().SaveTenantEventJoin(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { modal = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AlreadyJoinTenantEvent(TenantEventJoinModel model)
        {
            try
            {
                return Json(new { modal = new TenantEventJoinModel().AlreadyJoinTenantEvent(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { modal = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}