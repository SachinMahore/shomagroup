using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;

namespace ShomaRM.Controllers
{
    public class ProspectController : Controller
    {
        // GET: Tenant
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "prospect";
            return View();
        }

        public ActionResult SaveProspectForm(ProspectModel model)
        {
            try
            {
                return Json(new { model = new ProspectModel().SaveProspectForm(model) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}