using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class AmenitiesRRController : Controller
    {
        // GET: Tenant/Amenities
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveUpdateReservationRequest(AmenitiesReservationModel model)
        {
            try
            {
                return Json(new { model = new AmenitiesReservationModel().SaveUpdateReservationRequest(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetReservationRequestList()
        {
            try
            {
                return Json(new { model = new AmenitiesReservationModel().GetReservationRequestList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BuildPaganationReservationRequestList(AmenitiesReservationModel.AmenitiesReservationSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new AmenitiesReservationModel()).BuildPaganationRRList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillReservationRequestSearchGrid(AmenitiesReservationModel.AmenitiesReservationSearchModel model)
        {
            try
            {
                return Json((new AmenitiesReservationModel()).FillRRSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}