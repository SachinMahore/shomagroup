using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class CashReceiptController : Controller
    {
        // GET: /Admin/CashReceipt/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new CashReceiptModel().GetCashReceiptDetails(id);
            return View("..\\CashReceipt\\Edit", model);
        }
        public ActionResult SaveUpdateCashReceipt(CashReceiptModel model)
        {
            try
            {
                return Json(new { Msg = (new CashReceiptModel().SaveUpdateCashReceipt(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCashReceiptList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new CashReceiptModel().GetCashReceiptList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationCashReceiptList(CashReceiptModel.CashReceiptSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new CashReceiptModel()).BuildPaganationCashReceiptList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillCashReceiptSearchGrid(CashReceiptModel.CashReceiptSearchModel model)
        {
            try
            {
                return Json((new CashReceiptModel()).FillCashReceiptSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}