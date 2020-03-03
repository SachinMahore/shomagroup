using ShomaRM.Areas.Admin.Models;
using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers.ApplyNow
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index(int EID, int Status)
        {
            ViewBag.UID = "0";
            ViewBag.Status = Status;
            ViewBag.EID = EID;
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var GetServiceData = db.tbl_Estimate.Where(p => p.EID == EID).FirstOrDefault();
            var salesPersonnInfo = db.tbl_Login.Where(p => p.UserID == GetServiceData.CreatedBy).FirstOrDefault();
            var GetServiceDet = db.tbl_ServiceRequest.Where(p => p.ServiceID == GetServiceData.ServiceID).FirstOrDefault();
            var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == GetServiceDet.TenantID).FirstOrDefault();
            if (GetServiceData!=null)
            {
                if (GetServiceData.Status == "0")
                {
                    GetServiceData.Status = Status.ToString();
                    db.SaveChanges();

                    string reportHTMLAgent = "";
                    string filePathAg = HttpContext.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTMLAgent = System.IO.File.ReadAllText(filePathAg + "EmailTemplateAmenity.html");

                    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                    reportHTMLAgent = reportHTMLAgent.Replace("[%TenantName%]", salesPersonnInfo.FirstName + " " + salesPersonnInfo.LastName);

                    reportHTMLAgent = reportHTMLAgent.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Service Repair Estimate of Amount: $" + GetServiceData.Amount + " for Vendor: " + GetServiceData.Vendor + " with Description: " + GetServiceData.Description + ". </p><p><h2>STATUS: " + (Status == 1 ? "ACCEPTED" : "DENIED") + "</h2></p>");


                    reportHTMLAgent = reportHTMLAgent.Replace("[%LeaseNowButton%]", "");

                    string bodyAg = reportHTMLAgent;

                    //salesPersonnInfo.Email = "sachinmahore@gmail.com";
                    new EmailSendModel().SendEmail(salesPersonnInfo.Email, "Service Repair Estimate Status of " + GetTenantData.FirstName + " " + GetTenantData.LastName + "- STATUS: " + (Status == 1 ? "ACCEPTED" : "DENIED") + "", bodyAg);


                }
                else
                {
                    ViewBag.Status = 3;
                }

                
            }
            return View();
        }
    }
}