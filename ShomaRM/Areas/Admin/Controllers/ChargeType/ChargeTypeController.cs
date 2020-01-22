using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ChargeTypeController : Controller
    {
        public ActionResult Index()
        {
            UsersModel model = new UsersModel();
            if (model.HasRight == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult GetCTypeList()
        {
            try
            {
                return Json(new { model = (new ChargeTypeModel().GetCTypeList()) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetChargeTypeList(string ChargeType)
        {
            try
            {
                return Json((new ChargeTypeModel()).GetChargeTypeList(ChargeType), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetChargeTypeInfo(int CTID = 0)
        {
            try
            {
                return Json((new ChargeTypeModel()).GetChargeTypeInfo(CTID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateChargeType(ChargeTypeModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateChargeType(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}