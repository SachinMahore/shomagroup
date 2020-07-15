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
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            int id = 0;
            var model = new ParkingModel().GetParkingData(id);
            return View("..\\Parking\\Index", model);
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new ParkingModel().GetParkingData(Id);
            return View("..\\Parking\\Edit", model);
        }
        public ActionResult ParkingExcel(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new ParkingModel().GetParkingData(Id);
            return View("..\\Parking\\ParkingExcel", model);
        }

        public ActionResult GetParkingList(long TenantID)
        {
            try
            {
                return Json((new ParkingModel()).GetParkingList(TenantID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetParkingListByBedRoom(long TenantID, int BedRoom)
        {
            try
            {
                return Json((new ParkingModel()).GetParkingListByBedRoom(TenantID, BedRoom), JsonRequestBehavior.AllowGet);
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
                return Json(new { models = new ParkingModel().SaveUpdateParking(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { models = ex.Message }, JsonRequestBehavior.AllowGet);
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
                string[] parkingDetails = model.SaveUpdateTenantParking(model).Split('|');
                return Json(new { result = parkingDetails[0], mas = parkingDetails[1], numOfParking = parkingDetails[2], totalParkingAmt = parkingDetails[3] }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetUnitParkingList(int UID)
        {
            try
            {
                return Json((new ParkingModel()).GetUnitParkingList(UID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetParkingNewList()
        {
            try
            {
                return Json((new ParkingModel()).GetParkingNewList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateParkingName(ParkingModel model)
        {
            try
            {
                return Json(new { models = new ParkingModel().SaveUpdateParkingName(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { models = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateParkingLocation(ParkingModel model)
        {
            try
            {
                return Json(new { models = new ParkingModel().SaveUpdateParkingLocation(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { models = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSelectedParkingData(long PropertyId)
        {
            try
            {
                return Json((new ParkingModel()).GetSelectedParkingData(PropertyId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}