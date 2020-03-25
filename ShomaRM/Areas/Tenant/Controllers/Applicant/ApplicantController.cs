using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class ApplicantController : Controller
    {
        // GET: Tenant/Applicant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveUpdateApplicant(ApplicantModel model)
        {
            try
            {
                return Json(new { Msg = (new ApplicantModel().SaveUpdateApplicant(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetApplicantList(long TenantID)
        {
            try
            {
                return Json(new { model = new ApplicantModel().GetApplicantList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetApplicantDetails(int id,int FromAcc)
        {
            try
            {
                return Json(new { model = new ApplicantModel().GetApplicantDetails(id,FromAcc) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult DeleteApplicant(long AID)
        {
            try
            {
                return Json(new { model = new ApplicantModel().DeleteApplicant(AID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SaveUpdatePaymentResponsibility(List<ApplicantModel>  model)
        {
            try
            {
                return Json(new { Msg = (new ApplicantModel().SaveUpdatePaymentResponsibility(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}