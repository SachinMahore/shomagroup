using ShomaRM.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class LeaseTermsController : Controller
    {
        // GET: Admin/LeaseTerms
        public ActionResult Index()
        {
            return View();
        }

        // Save update method

        public ActionResult SaveUpdateLeaseTerms(LeaseTermsModel model)
        {
            try
            {
                return Json(new { model = new LeaseTermsModel().SaveUpdateLeaseTerms(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetLeaseTermsList(string SortBy, string OrderBy)
        {
            try
            {
                return Json(new { model = new LeaseTermsModel().GetLeaseTermsList(SortBy, OrderBy) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetLeaseTermsDetails(int Id)
        {
            try
            {
                return Json(new { model = new LeaseTermsModel().GetLeaseTermsDetails(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteLeaseTermsDetails(int Id)
        {
            try
            {
                new LeaseTermsModel().DeleteLeaseTermsDetails(Id);
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}