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
        public ActionResult GetTenantUpTransactionList(long TenantID)
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GetTenantUpTransactionList(TenantID) }, JsonRequestBehavior.AllowGet);
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
    }
}