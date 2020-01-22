using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class LeaseManagementController : Controller
    {
        //
        // GET: /Tenant/Lease/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = new LeaseManagementModel().GetLeaseInformation(id);
            return View("..\\LeaseManagement\\_Edit", model);
        }
        

        public ActionResult SaveUpdateLease(LeaseManagementModel model)
        {
            try
            {
                return Json(new { Msg = (new LeaseManagementModel().SaveUpdateLease(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}