using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class EventController : Controller
    {
        // GET: /Admin/Event/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new EventModel().GetEventData(Id);
            return View("..\\Event\\Edit", model);
        }
        public ActionResult SaveUpdateEvent(EventModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }

                return Json(new { models = new EventModel().SaveUpdateEvent(fb, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { models = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEventData(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new EventModel().GetEventListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EventDataList()
        {
            try
            {
                return Json(new { result = new EventModel().EventList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationEventList(EventModel.EventSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new EventModel()).BuildPaganationEventList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillEventSearchGrid(EventModel.EventSearchModel model)
        {
            try
            {
                return Json((new EventModel()).FillEventSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}