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
                return Json(new { result = 1, numOfParking= parkingDetails[0], totalParkingAmt = parkingDetails[1] }, JsonRequestBehavior.AllowGet);
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