using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Tenant/Vehicle
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveUpdateVehicle(VehicleModel model)
        {
            try
            {
                return Json(new { Msg = (new VehicleModel().SaveUpdateVehicle(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVehicleList(long TenantID)
        {
            try
            {
                return Json(new { model = new VehicleModel().GetVehicleList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult DeleteTenantVehicle(long VID)
        {
            try
            {
                return Json(new { model = new VehicleModel().DeleteTenantVehicle(VID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetVehicleInfo(long VehicleId)
        {
            try
            {
                return Json(new { model = new VehicleModel().GetVehicleInfo(VehicleId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProfileVehicleList(long TenantID)
        {
            try
            {
                return Json(new { model = new VehicleModel().GetProfileVehicleList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult SaveUpdateVehicleTenanat(VehicleModel model, long UserId)
        {
            try
            {
                return Json(new { Msg = (new VehicleModel().SaveUpdateVehicleTenanat(model, UserId)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}