using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class EventBookingController : Controller
    {
        // GET: Tenant/EventBooking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            var model = new EventBookingModel().GetEventBookingData(Id);
            return View("..\\EventBooking\\Edit", model);
        }

        public ActionResult SaveUpdateEventBooking(EventBookingModel model)
        {
            try
            {
                return Json(new { msg = (new EventBookingModel().SaveUpdateEventBooking(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetEventBookingData(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new EventBookingModel().GetEventBookingListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}