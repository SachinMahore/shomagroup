using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ServiceIssueController : Controller
    {
        // GET: Admin/ServiceIssue
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            UsersModel model = new UsersModel();
            if (model.HasRight == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult fillServiceIssueSearchGrid(SIListModel model)
        {
            try
            {
                return Json((new ServiceIssueModel()).FillSISSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationSLList(SIListModel model)
        {
            try
            {
                return Json(new { NOP = (new ServiceIssueModel()).BuildPaganationSIList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getIssueData(int IssueID = 0)
        {
            try
            {
                return Json((new ServiceIssueModel()).GetServiceIssueInfo(IssueID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult saveUpdateIssue(ServiceIssueModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateServiceIssue(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}