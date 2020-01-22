using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
        // GET: Admin/Promotion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = new PromotionModel().GetPromotionDetails(id);
            return View("..\\Promotion\\Edit", model);
        }

        public ActionResult GetPromotionList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new PromotionModel().GetPromotionList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPromotionListDetails(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new PromotionModel().GetPromotionListDetails(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdatePromotion(PromotionModel model)
        {
            try
            {
                return Json(new { Msg = (new PromotionModel().SaveUpdatePromotion(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillPromotionSearchGrid(PromotionListModel model)
        {
            try
            {
                return Json((new PromotionModel()).FillPromotionSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationPromotionList(PromotionListModel model)
        {
            try
            {
                return Json(new { NOP = (new PromotionModel()).BuildPaganationPromotionList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}