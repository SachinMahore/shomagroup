using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class NoticeController : Controller
    {
        // GET: Admin/Notice
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new NoticeModel().GetNoticeDeatails(id);
            return View("..\\Notice\\Edit", model);
        }

        public ActionResult SaveUpdateNotice(NoticeModel model)
        {
            try
            {
                return Json(new { Msg = (new NoticeModel().SaveUpdateNotice(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetNoticeDataList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { result = new NoticeModel().GetNoticeListData(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantNoticeList(DateTime FromDate, DateTime ToDate,long TenantID)
        {
            try
            {
                return Json(new { result = new NoticeModel().GetTenantNoticeList(FromDate, ToDate, TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationNoticeList(NoticeModel.NoticeSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new NoticeModel()).BuildPaganationNoticeList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillNoticeSearchGrid(NoticeModel.NoticeSearchModel model)
        {
            try
            {
                return Json((new NoticeModel()).FillNoticeSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}