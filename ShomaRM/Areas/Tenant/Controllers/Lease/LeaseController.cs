using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class LeaseController : Controller
    {
        //
        // GET: /Tenant/Lease/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = new LeaseModel().GetLeaseInformation(id);
            return View("..\\Lease\\_Lease", model);
        }
        public ActionResult GetPropertyList()
        {
            try
            {
                return Json(new { model = (new LeaseModel().GetPropertyList()) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetPropertyUnitList(long PID)
        {
            try
            {
                return Json(new { model = (new LeaseModel().GetPropertyUnitList(PID)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SaveUpdateLease(LeaseModel model)
        {
            try
            {
                return Json(new { Msg = (new LeaseModel().SaveUpdateLease(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}