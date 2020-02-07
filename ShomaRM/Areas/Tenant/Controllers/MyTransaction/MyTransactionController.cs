using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class MyTransactionController : Controller
    {
        // GET: Tenant/MyTransaction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveUpdateTransaction(MyTransactionModel model,string NameOnCardString, string NumberOnCardString, string ExpirationMonthOnCardString, string ExpirationYearOnCardString)
        {
            try
            {
                return Json(new { Msg = (new MyTransactionModel().SaveUpdateTransaction(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveAmenityTransaction(MyTransactionModel model)
        {
            try
            {
                return Json(new { Msg = (new MyTransactionModel().SaveAmenityTransaction(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTransactionDetails(int id)
        {
           
            try
            {
                return Json(new { model = new MyTransactionModel().GetTransactionDetails(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantTransactionList(long TenantID, int AccountHistoryDDL)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetTenantTransactionList(TenantID,AccountHistoryDDL) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTotalDue(long TenantID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetTotalDue(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantUpTransactionList(long UserId)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetTenantUpTransactionList(UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAccountHistoryListByDateRange(long TenantID,DateTime FromDate,DateTime ToDate)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetAccountHistoryListByDateRange(TenantID, FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTenantAccountHistoryList(long TenantID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetTenantAccountHistoryList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantBillList(long TransID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetTenantBillList(TransID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetRecurringPayLists(long TenantID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetRecurringPayLists(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateRecurringTransaction(MyTransactionModel model, string NameOnCardString, string NumberOnCardString, string ExpirationMonthOnCardString, string ExpirationYearOnCardString)
        {
            try
            {
                return Json(new { Msg = (new MyTransactionModel().SaveUpdateRecurringTransaction(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SetUpRecurringTransaction(MyTransactionModel model, string NameOnCardString, string NumberOnCardString, string ExpirationMonthOnCardString, string ExpirationYearOnCardString)
        {
            try
            {
                return Json(new { Msg = (new MyTransactionModel().SetUpRecurringTransaction(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteRecurringTransaction(long TenantID)
        {
            try
            {
                return Json(new { Msg = (new MyTransactionModel().DeleteRecurringTransaction(TenantID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetUpTransationLists(long TenantID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetUpTransationLists(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetReservationPaymentList(long ARID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetReservationPaymentList(ARID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RefundRRCharges(long TransID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().RefundRRCharges(TransID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}