using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class UtilityBillingController : Controller
    {
        //
        // GET: /Tenant/UtilityBilling/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = new UtilityBillingModel().GetUBData(id);
            return View("..\\UtilityBilling\\Edit", model);
        }
        public ActionResult SaveUpdateUtilityBilling(UtilityBillingModel model)
        {
            try
            {
                return Json(new { result = (new UtilityBillingModel().SaveUpdateUtilityBilling(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { error = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUtilityBillingList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new UtilityBillingModel().GetUtilityBillingList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}