using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class FacilityBookingController : Controller
    {
        //
        // GET: /Tenant/Booking/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            var model = new FacilityBookingModel().GetFacilityBookingData(Id);
            return View("..\\FacilityBooking\\Edit", model);
        }

        public ActionResult SaveUpdateFacilityBooking(FacilityBookingModel model)
        {
            try
            {
                return Json(new { msg = (new FacilityBookingModel().SaveUpdateFacilityBooking(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFacilityBookingData(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new FacilityBookingModel().GetFacilityBookingListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
	}
}