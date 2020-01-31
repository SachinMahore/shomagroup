using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;
using ShomaRM.Data;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class AmenitiesSlotController : Controller
    {
        // GET: Admin/AmenitiesSlot
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new AmenitiesSlotModel().GetEventData(Id);
            return View("..\\AmenitiesSlot\\Edit", model);
        }
        public ActionResult SaveUpdateSlot(AmenitiesSlotModel model)
        {
            try
            {
                return Json(new { model = new AmenitiesSlotModel().SaveUpdateSlot(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetSlotData(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new AmenitiesSlotModel().GetSlotListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SlotDataList()
        {
            try
            {
                return Json(new { result = new AmenitiesSlotModel().SlotList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult BuildPaganationSlotList(AmenitiesSlotModel.AmenitiesSlotSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new AmenitiesSlotModel()).BuildPaganationSlotList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillSlotSearchGrid(AmenitiesSlotModel.AmenitiesSlotSearchModel model)
        {
            try
            {
                return Json((new AmenitiesSlotModel()).FillSlotSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}