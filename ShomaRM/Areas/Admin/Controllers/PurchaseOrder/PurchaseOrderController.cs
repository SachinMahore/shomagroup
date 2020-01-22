using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: /Admin/PurchaseOrder/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model=new PurchaseOrderModel().GetPurchaseOrderData(Id);
            return View("..\\PurchaseOrder\\Edit", model);
        }
        public ActionResult SaveUpdatePurchaseOrder(PurchaseOrderModel model)
        {
            try
            {
                return Json(new { msg = new PurchaseOrderModel().SaveUpdatePurchaseOrder(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { msg = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationPurchaseOrderList(PurchaseOrderModel.PurchaseOrderSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new PurchaseOrderModel()).BuildPaganationPurchaseOrderList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillPurchaseOrderSearchGrid(PurchaseOrderModel.PurchaseOrderSearchModel model)
        {
            try
            {
                return Json((new PurchaseOrderModel()).FillPurchaseOrderSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}