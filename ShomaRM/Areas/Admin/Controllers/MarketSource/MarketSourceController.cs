using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class MarketSourceController : Controller
    {
        // GET: Admin/MarketSource
        public ActionResult Index(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new MarketSourceModel().GetMarketSourceData(Id);
            return View("..\\MarketSource\\Index",model);
        }
        public ActionResult GetMarketSourceList()
        {
            try
            {
                return Json((new MarketSourceModel()).GetMarketSourceList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMarketSourceInfo(int MarketSourceID = 0)
        {
            try
            {
                return Json((new MarketSourceModel()).GetMarketSourceInfo(MarketSourceID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateMarketSource(MarketSourceModel model)
        {
            try
            {
                return Json(new { result = 1, AdID = model.SaveUpdateMarketSource(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetMarketSourceSearchList(string SearchText)
        {
            try
            {
                return Json((new MarketSourceModel()).GetMarketSourceSearchList(SearchText), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillMSSearchGrid(MSListModel model)
        {
            try
            {
                return Json((new MarketSourceModel()).FillMSSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationMSList(MSListModel model)
        {
            try
            {
                return Json(new { NOP = (new MarketSourceModel()).BuildPaganationMSList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteMarketSource(long ADID)
        {
            try
            {
                return Json(new { model = new MarketSourceModel().DeleteMarketSource(ADID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}