using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;
using ShomaRM.Data;
using System.Data;

namespace ShomaRM.Controllers
{
    public class VerificationController : Controller
    {
        // GET: Verification
        public ActionResult Index(int Id)
        {
            ViewBag.ActiveMenu = "verification";
            var model = new VerificationModel().GetProspectData(Id);
            return View(model);
        }

        public ActionResult SaveLeaseDocumentVerification(long ProspectusID)
        {
            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = new VerificationModel().SaveLeaseDocumentVerifications(fb, ProspectusID);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SavePaymentDetails(VerificationModel model)
        {
            try
            {
                return Json(new { Msg = (new VerificationModel().SavePaymentDetails(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}