using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
        //
        // GET: /Admin/Vendor/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new VendorModel().GetVendorData(Id);
            return View("..\\Vendor\\Edit", model);
        }

        public ActionResult SaveUpdateVendor(VendorModel model)
        {
            try
            {
                return Json(new { msg = new VendorModel().SaveUpdateVendor(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { msg = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetVendorDataList(DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        return Json(new { result = new VendorModel().GetVendorListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult BuildPaganationVendorList(VendorModel.VendorSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new VendorModel()).BuildPaganationVendorList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVendorDataList(VendorModel.VendorSearchModel model)
        {
            try
            {
                return Json((new VendorModel()).GetVendorDataList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult VendorDataList()
        {
            try
            {
                return Json(new { result = new VendorModel().VendorList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}