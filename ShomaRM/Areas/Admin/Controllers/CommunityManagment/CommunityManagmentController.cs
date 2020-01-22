using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class CommunityManagmentController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            return View();
        }

        public ActionResult BuildPaganationCommunityList(CommunityActivityModel.CommunityActivitySearchModel model)
        {
            try
            {
                return Json(new { NOP = (new CommunityActivityModel()).BuildPaganationCommunityList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillCommunitySearchGrid(CommunityActivityModel.CommunityActivitySearchModel model)
        {
            try
            {
                return Json((new CommunityActivityModel()).FillCommunitySearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCommunityPost(int CID)
        {
            try
            {
                (new CommunityActivityModel()).DeleteCommunityPost(CID);
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}