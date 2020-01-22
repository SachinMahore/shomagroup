using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Admin/Document
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            var model = new DocumentModel().GetDocumentData(Id);
            return View("..\\Document\\Edit", model);
        }

        public ActionResult SaveUpdateDocument(DocumentModel model)
        {
            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = new DocumentModel().SaveUpdateDocument(fb, model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetDocumentData(long ProspectID)
        {
            try
            {
                return Json(new { model = new DocumentModel().GetDocumentListData(ProspectID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult DocumentDataList()
        {
            try
            {
                return Json(new { result = new DocumentModel().DocumentList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { result = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}