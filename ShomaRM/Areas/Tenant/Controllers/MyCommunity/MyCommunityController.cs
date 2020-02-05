using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Areas.Tenant.Models.Club;
using ShomaRM.Areas.Tenant.Models.Enum;
using ShomaRM.Models;
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
        #region CommunityModule
        public JsonResult CreateClub(ClubModel _model)
        {
            ResponseModel _response = new ResponseModel();
            try
            {
                return Json(new { _response = new ClubModel().SaveclubEvent(_model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                _response.Status = false;
                _response.msg = Ex.Message;
                return Json(_response, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult JoinClubPartial(long SearchId,long UserId)
        {
            if (SearchId <= 1)
            {
                var Model = new ClubModel().GetClubList().Where(a=>a.UserId!= UserId).OrderByDescending(a => a.ClubTitle).ToList();
                ViewBag.CallenderHidden = "";
                return PartialView("~/Areas/Tenant/Views/MyCommunity/_JoinClub.cshtml", Model);
            }
            else
            {
                var Model = new ClubModel().GetClubList().Where(a => a.UserId != UserId).OrderByDescending(a => a.StartDate).ToList();
                ViewBag.CallenderHidden = "";
                return PartialView("~/Areas/Tenant/Views/MyCommunity/_JoinClub.cshtml", Model);
            }
        }

        public ActionResult JoinClubPartialByUser(long SearchId, long UserId)
        {
            if (SearchId <= 1)
            {
                var Model = new ClubModel().GetClubList().Where(a=>a.UserId== UserId).OrderByDescending(a => a.ClubTitle).ToList();
                ViewBag.CallenderHidden = "hidden";
                return PartialView("~/Areas/Tenant/Views/MyCommunity/_JoinClub.cshtml", Model);
            }
            else
            {
                var Model = new ClubModel().GetClubList().Where(a => a.UserId == UserId).OrderByDescending(a=>a.StartDate).ToList();
                ViewBag.CallenderHidden = "hidden";
                return PartialView("~/Areas/Tenant/Views/MyCommunity/_JoinClub.cshtml", Model);
            }
        }

        public JsonResult GetClubById(long Id, long UserId)
        {
            try
            {
                return Json(new { model = new ClubModel().GetClubbyId(ClubId: Id, UserId: UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetClubJoinStatus(long Id, long UserId)
        {
            try
            {
                return Json(new { model = new ClubModel().GetClubJoinStatusId(ClubId: Id, UserId: UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult JoinunJoinClub(long ClubId, long UserId)
        {
            try
            {
                return Json(new { model = new ClubMappingModel().RemoveMappingByClubIdandUserId(ClubId: ClubId, UserId: UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var model = false;
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

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