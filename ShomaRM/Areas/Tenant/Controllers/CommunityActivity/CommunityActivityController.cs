using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class CommunityActivityController : Controller
    {
        // GET: Tenant/CommunityActivity
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveUpdateCommunityActivity(CommunityActivityModel model)
        {
            try
            {
                return Json(new { model = new CommunityActivityModel().SaveUpdateCommunityPost(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult uploadAttatchFileCommunityActivity(CommunityActivityModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload = Request.Files[i];

                }

                return Json(new { model = new CommunityActivityModel().SaveUploadAttachFileCommunityActivity(fileBaseUpload, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCommunityActivityList(/*CommunityActivityModel model*/)
        {
            try
            {
                return Json(new { model = new CommunityActivityModel().GetCommunityActivityList(/*model*/) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}