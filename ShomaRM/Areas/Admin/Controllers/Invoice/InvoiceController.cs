using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: /Admin/Invoice/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }
        public ActionResult Edit(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new InvoiceModel().GetInvoiceData(Id);
            return View("..\\Invoice\\Edit", model);
        }
        public ActionResult SaveUpdateInvoice(InvoiceModel model)
        {
            try
            {
                return Json(new { msg = (new InvoiceModel().SaveUpdateInvoice(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetInvoiceData(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new InvoiceModel().GetInvoiceListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationInvoiceList(InvoiceModel.InvoiceSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new InvoiceModel()).BuildPaganationInvoiceList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillInvoiceSearchGrid(InvoiceModel.InvoiceSearchModel model)
        {
            try
            {
                return Json((new InvoiceModel()).FillInvoiceSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}