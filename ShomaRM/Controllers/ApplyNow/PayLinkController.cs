using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Areas.Admin.Models;
using static ShomaRM.Areas.Admin.Models.ServicesManagementModel;

namespace ShomaRM.Controllers
{
    public class PayLinkController : Controller
    {
        // GET: PayLink
        public ActionResult Index(string pid)
        {
            string[] payid = new EncryptDecrypt().DecryptText(pid).Split(',');
            int AID=Convert.ToInt32(payid[0]);
            int FromAcc = Convert.ToInt32(payid[1]);
            string Amt= payid[2]; ;

            ViewBag.UID = "0";
            ViewBag.FromAcc = FromAcc;
            ViewBag.AID = AID;
            ViewBag.Amt = Amt;
            ViewBag.ProcessingFees = new CheckListModel().GetProcessingFees();
            var model = new ApplicantModel().GetApplicantDetails(AID, FromAcc);
            if (FromAcc != 0)
            {
                if (ShomaGroupWebSession.CurrentUser != null)
                {
                    ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
                }
            }
            return View("..//Paylink//Index", model);
        }
        public ActionResult LeaseNow()
        {
            ViewBag.UID = "0";
            if(ShomaGroupWebSession.CurrentUser!=null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }
          
            return View();
        }
        
        public ActionResult GetProspectData(long UID)
        {
            try
            {
                return Json(new { model = (new OnlineProspectModule().GetProspectData(UID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PayAmenityCharges(string pid)
        {
            string[] payid = new EncryptDecrypt().DecryptText(pid).Split(',');
            int ARID=Convert.ToInt32(payid[0]);
            int FromAcc= Convert.ToInt32(payid[1]);

            ViewBag.UID = "0";
            ViewBag.FromAcc = FromAcc;
            ViewBag.ARID = ARID;
            ViewBag.ProcessingFees = new CheckListModel().GetProcessingFees();
            var model = new AmenitiesReservationModel().GetRRInfo(ARID);
            if (FromAcc != 0)
            {
                if (ShomaGroupWebSession.CurrentUser != null)
                {
                    ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
                }
            }
            return View("..//Paylink//PayAmenityCharges", model);
        }
        public ActionResult PayServiceCharges(string pid)
        {
            string[] payid = new EncryptDecrypt().DecryptText(pid).Split(',');
            int EID = Convert.ToInt32(payid[0]);
            int FromAcc = Convert.ToInt32(payid[1]);
            ViewBag.UID = "0";
            ViewBag.FromAcc = FromAcc;
            ViewBag.ProcessingFees = new CheckListModel().GetProcessingFees();
            ViewBag.EID = EID;
            var model = new EstimateModel().GetEstimateInvData(EID);

            if (FromAcc != 0)
            {
                if (ShomaGroupWebSession.CurrentUser != null)
                {
                    ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
                }
            }
            return View("..//Paylink//PayServiceCharges", model);
        }
    }
}