using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ParkingController : Controller
    {
        // GET: Admin/Parking
        public ActionResult Index(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new ParkingModel().GetParkingData(Id);
            return View("..\\Parking\\Index", model);
        }
        public ActionResult GetParkingList()
        {
            try
            {
                return Json((new ParkingModel()).GetParkingList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetParkingInfo(int ParkingID = 0)
        {
            try
            {
                return Json((new ParkingModel()).GetParkingInfo(ParkingID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateParking(ParkingModel model)
        {
            try
            {
                return Json(new { result = 1, ParkingID = model.SaveUpdateParking(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetParkingSearchList(string SearchText)
        {
            try
            {
                return Json((new ParkingModel()).GetParkingSearchList(SearchText), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //For TenantParkingModel

        public ActionResult GetTenantParkingList(long TenantId)
        {
            try
            {
                return Json((new TenantParkingModel()).GetTenantParkingList(TenantId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateTenantParking(TenantParkingModel model)
        {
            try
            {
                return Json(new { result = 1, totalParkingAmt = model.SaveUpdateTenantParking(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillParkingSearchGrid(ParkingListModel model)
        {
            try
            {
                return Json((new ParkingModel()).FillParkingSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationParkingList(ParkingListModel model)
        {
            try
            {
                return Json(new { NOP = (new ParkingModel()).BuildPaganationParkingList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteParking(long ParkingID)
        {
            try
            {
                return Json(new { model = new TenantParkingModel().DeleteParking(ParkingID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}