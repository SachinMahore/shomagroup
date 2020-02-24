using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ProspectManagementController : Controller
    {
        // GET: Admin/ProspectManagement
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "prospect";
            return View();
        }
        public ActionResult AddEdit(int id)
        {
            ViewBag.ActiveMenu = "prospect";
            if (id !=0)
            {
                var model = new ProspectManagementModel().GetProspectDetails(id);
                return View("..\\ProspectManagement\\AddEdit", model);
            }
            else
            {
                return RedirectToAction("../ProspectManagement/Index");
                //return View("..\\ProspectManagement\\Index");
            }
        }
        public ActionResult GetProspectList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().GetProspectList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveProspectForm(ProspectManagementModel model)
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().SaveProspectForm(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProspectDetails(int id)
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().GetProspectDetails(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SaveUpdateVisit(VisitModel model)
        {
            try
            {
                return Json(new { Msg = (new VisitModel().SaveUpdateVisit(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVisitDetails(int id)
        {
            try
            {
                return Json(new { model = new VisitModel().GetVisitDetails(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetVisitList(long ProspectID)
        {
            try
            {
                return Json(new { model = new VisitModel().GetVisitList(ProspectID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetDdlMarketSourceList()
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().GetDdlMarketSourceList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult BuildPaganationProspectList(ProspectSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new ProspectManagementModel()).BuildPaganationProspectList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillProspectSearchGrid(ProspectSearchModel model)
        {
            try
            {
                return Json((new ProspectManagementModel()).FillProspectSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}