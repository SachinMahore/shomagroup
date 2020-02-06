using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "home";
            return View();
        }
        public ActionResult GenerateMonthlyRent()
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GenerateMonthlyRent() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GenerateLateFee()
        {
            try
            {
                return Json(new { model = new MyTransactionModel().GenerateLateFee() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}