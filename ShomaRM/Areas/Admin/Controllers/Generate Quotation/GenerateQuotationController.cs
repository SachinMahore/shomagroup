using ShomaRM.Data;
using ShomaRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class GenerateQuotationController : Controller
    {
        // GET: Admin/GenerateQuotation
        public ActionResult Index(string id)
        {
            var model = new OnlineProspectModule().GetProspectData(Convert.ToInt64(id));

            if (Session["Bedroom"] != null)
            {
                model.Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                model.MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                model.MaxRent = Convert.ToDecimal(Session["MaxRent"].ToString());
                model.LeaseTermID = Convert.ToInt32(Session["LeaseTerm"].ToString());
                var leaseDet = new Areas.Admin.Models.LeaseTermsModel().GetLeaseTermsDetails(model.LeaseTermID);
                model.LeaseTerm = Convert.ToInt32(leaseDet.LeaseTerms);

                model.FromHome = 1;
                Session.Remove("Bedroom");
                Session.Remove("MoveInDate");
                Session.Remove("MaxRent");
                Session.Remove("LeaseTerm");
            }
            else
            {
                model.Bedroom = 0;
                model.MoveInDate = DateTime.Now.AddDays(30);
                //model.MaxRent = 0;
                model.FromHome = 0;
                if (model.LeaseTermID == 0)
                {
                    ShomaRMEntities db = new ShomaRMEntities();
                    var leaseDet = db.tbl_LeaseTerms.Where(p => p.LeaseTerms == 12).FirstOrDefault();
                    if (leaseDet != null)
                    {
                        model.LeaseTermID = leaseDet.LTID;
                    }
                    else
                    {
                        model.LeaseTermID = 0;
                    }
                }

            }
            if (Session["StepNo"] != null)
            {
                model.StepNo = Convert.ToInt32(Session["StepNo"].ToString());
                Session.Remove("StepNo");
            }
            else
            {
                model.StepNo = 0;

            }

            return View(model);
        }

        public ActionResult SaveGenerateQuotation(OnlineProspectModule model)
        {
            try
            {
                return Json(new { msg = (new OnlineProspectModule().SaveGenerateQuotation(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTenantOnlineListGenerateQuotation(int id, long UserId)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().GetTenantOnlineListGenerateQuotation(id, UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SendQuotationEmail(string Email)
        {
            try
            {
                return Json(new { model = new ApplyNowModel().SendQuotationEmail(Email) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}