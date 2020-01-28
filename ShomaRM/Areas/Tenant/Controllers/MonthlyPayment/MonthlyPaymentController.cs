using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class MonthlyPaymentController : Controller
    {
        // GET: Tenant/MonthlyPayment
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveMonthlyPayment(MonthlyPaymentModel model)
        {
            try
            {
                return Json(new { modal = new MonthlyPaymentModel().SaveMonthlyPayment(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { modal = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMonthlyPayment(long UserId)
        {
            try
            {
                return Json(new { modal = new MonthlyPaymentModel().GetMonthlyPayment(UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { modal = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}