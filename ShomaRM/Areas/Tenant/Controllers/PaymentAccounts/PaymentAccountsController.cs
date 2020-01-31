using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class PaymentAccountsController : Controller
    {
        // GET: Tenant/PaymentAccounts
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveUpdatePaymentAccounts(PaymentAccountsModel model)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().SaveUpdatePaymentsAccounts(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPaymentsAccountsList(long TenantId)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().GetPaymentsAccountsList(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPaymentsBankAccountsList(long TenantId)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().GetPaymentsBankAccountsList(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditPaymentsAccounts(long PAID)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().EditPaymentsAccounts(PAID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditPaymentsBankAccounts(long PAID)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().EditPaymentsBankAccounts(PAID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeletePaymentsAccounts(long PAID)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().DeletePaymentsAccounts(PAID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult MakeDefaultPaymentSystem(long TenantId, long PAID)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().MakeDefaultPaymentSystem(TenantId, PAID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPaymentMethods(long TenantId)
        {
            try
            {
                return Json(new { model = new PaymentAccountsModel().GetPaymentMethods(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}