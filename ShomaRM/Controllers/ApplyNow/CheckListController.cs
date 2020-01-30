using ShomaRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers
{
    public class CheckListController : Controller
    {
        // GET: CheckList
        public ActionResult Index()
        {
            ViewBag.UID = "0";
            if (ShomaGroupWebSession.CurrentUser != null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }

            return View();
        }
        public ActionResult SaveMoveInCheckList(CheckListModel model)
        {
            try
            {
                return Json(new { msg = (new CheckListModel().SaveMoveInCheckList(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadInsurenceDoc(CheckListModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadInsurenceDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadInsurenceDoc = Request.Files[i];

                }

                return Json(new { model = new CheckListModel().UploadInsurenceDoc(fileBaseUploadInsurenceDoc, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadProofOfElectricityDoc(CheckListModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadProofOfElectricityDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadProofOfElectricityDoc = Request.Files[i];

                }

                return Json(new { model = new CheckListModel().UploadProofOfElectricityDoc(fileBaseUploadProofOfElectricityDoc, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveMoveInPayment(ApplyNowModel model)
        {
            try
            {
                return Json(new { Msg = (new CheckListModel().SaveMoveInPayment(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
    
}