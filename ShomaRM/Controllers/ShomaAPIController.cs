using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Models;

namespace ShomaRM.Controllers
{
    public class AllowCrossJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            var headers = Enumerable.ToList(HttpContext.Current.Request.Headers.AllKeys);
            headers.Add("X-HTTP-Method-Override");

            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Headers", string.Join(", ", headers));

            filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            base.OnActionExecuting(filterContext);
        }
    }
    [AllowCrossJson]
    public class ShomaAPIController : Controller
    {
        // GET: ShomaAPI
        public ActionResult Index()
        {
            return null;
        }
        [AllowCrossJson]
        public ActionResult ScheduleRecurring()
        {
            try
            {
                new MyTransactionModel().ScheduleRecurring();
                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowCrossJson]
        public ActionResult GenerateMonthlyRent()
        {
            try
            {
               string result = new MyTransactionModel().GenerateMonthlyRent();
                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowCrossJson]
        public ActionResult GenerateLateFee()
        {
            try
            {
                string result = new MyTransactionModel().GenerateLateFee();
                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }
        [AllowCrossJson]
        public ActionResult ScheduleEmail()
        {
            
            try
            {
                string result = new OnlineProspectModule().ScheduleEmail();
                return Json(new { msg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Fail" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}