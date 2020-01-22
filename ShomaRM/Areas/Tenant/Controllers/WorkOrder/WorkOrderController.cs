using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class WorkOrderController : Controller
    {
        //
        // GET: /Tenant/WorkOrder/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = new WorkOrderModel().GetWorkOrderDeatails(id);
            return View("..\\WorkOrder\\Edit", model);
        }
        public ActionResult GetPropertyList()
        {
            try
            {
                return Json(new { model = (new WorkOrderModel().GetPropertyList()) }, JsonRequestBehavior.AllowGet);

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
                return Json(new { model = (new WorkOrderModel().GetPropertyUnitList(PID)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SaveUpdateWorkOrder(WorkOrderModel model)
        {
            try
            {
                return Json(new { Msg = (new WorkOrderModel().SaveUpdateWorkOrder(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetWorkOrderList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new WorkOrderModel().GetWorkOrderList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
	}
}