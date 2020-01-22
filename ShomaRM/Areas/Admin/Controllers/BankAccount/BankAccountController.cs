using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class BankAccountController : Controller
    {
        // GET: /Admin/BankAccount/
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "transaction";
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "transaction";
            var model = new BankAccountModel().GetBankDetails(id);
            return View("..\\BankAccount\\Edit", model);
        }
        public ActionResult BuildPaganationBankAccountList(BankAccountModel.BankAccountSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new BankAccountModel()).BuildPaganationBankAccountList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBankAccountList(BankAccountModel.BankAccountSearchModel model)
        {
            try
            {
                return Json((new BankAccountModel()).GetBankAccountList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillBankAccountDropDownList(long PID)
        {
            try
            {
                return Json(new { result = new BankAccountModel().FillBankAccountDropDownList(PID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateBankDetails(BankAccountModel model)
        {
            try
            {
                return Json(new { Msg = (new BankAccountModel().SaveUpdateBankDetails(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}