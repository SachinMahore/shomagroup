using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class TransactionController : Controller
    {
        //
        // GET: /Admin/Transaction/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "transaction";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "transaction";
            var model = new TransactionModel().GetTransactionDetails(id);
            return View("..\\Transaction\\Edit", model);
        }

        public ActionResult SaveUpdateTransaction(TransactionModel model)
        {
            try
            {
                return Json(new { Msg = (new TransactionModel().SaveUpdateTransaction(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTransactionList(TransactionModel.TransactionSearchModel model)
        {
            try
            {
                return Json((new TransactionModel()).GetTransactionList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationTransactionList(TransactionModel.TransactionSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new TransactionModel()).BuildPaganationTransactionList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantTransactionList(DateTime FromDate, DateTime ToDate, long TenantID)
        {
            try
            {
                return Json(new { model = new TransactionModel().GetTenantTransactionList(FromDate, ToDate, TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProspectTransactionList( long ProspectID)
        {
            try
            {
                return Json(new { model = new TransactionModel().GetProspectTransactionList(ProspectID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetOnlineTransactionList(long TenantID)
        {
            try
            {
                return Json(new { model = new TransactionModel().GetOnlineTransactionList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllTransactionListOP(long TenantID)
        {
            try
            {
                return Json(new { model = new TransactionModel().GetAllTransactionListOP(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAccountHistory(long TenantId)
        {
            try
            {
                return Json(new { model = new TransactionModel().GetAccountHistory(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}