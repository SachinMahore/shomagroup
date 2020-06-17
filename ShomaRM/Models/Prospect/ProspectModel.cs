using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Web.Configuration;

namespace ShomaRM.Models
{
    public class ProspectModel
    {
        public long PID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> City { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public Nullable<int> HavingPets { get; set; }
        public string PetsDetails { get; set; }
        public string VehicleDetails { get; set; }
        public Nullable<int> MinBedroom { get; set; }
        public Nullable<int> MinBathroom { get; set; }
        public Nullable<decimal> MinRent { get; set; }
        public Nullable<int> MaxBedroom { get; set; }
        public Nullable<int> MaxBathroom { get; set; }
        public Nullable<decimal> MaxRent { get; set; }
        public Nullable<long> UnitID { get; set; }
        public int PropertyID { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<int> Term { get; set; }
        public string ReasonForMovingIn { get; set; }
        public Nullable<System.DateTime> VisitDateTime { get; set; }
        public Nullable<int> LeasingAgent { get; set; }
        public string MarketSource { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
        public string OutlookID { get; set; }

        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];

        public string SaveProspectForm(ProspectModel model)
        {
            string MSG = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var saveTenant = new tbl_Prospect()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNo = model.PhoneNo,
                EmailId = model.EmailId,
                State = null,
                City = model.City,
                Address = model.Address,
                Message = model.Message,
                HavingPets = model.HavingPets,
                UnitID = model.UnitID,
                PropertyID = null,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                VisitDateTime=model.VisitDateTime,
                Status=0,
                MarketSource=model.MarketSource,
                AssignAgentId = 0,
                OutlookID=model.OutlookID
                
            };
            db.tbl_Prospect.Add(saveTenant);
            db.SaveChanges();
            string appid = new EncryptDecrypt().EncryptText(saveTenant.PID.ToString());
            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
            //reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
            reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

            string emailBody = "";
            emailBody += "<p style=\"margin-bottom: 0px;\">Hello <b>" + saveTenant.FirstName + " " + saveTenant.LastName + "</b>! We’ll be contacting you shortly to confirm your appointment.</p>";
            emailBody += "<p style=\"margin-bottom: 0px;\">Congratulations ! Your Application is Approved and Pay your Administration Fees.</p>";
            emailBody += "<p style=\"margin-bottom: 0px;\">Good news!You have been approved.We welcome you to our community.Your next step is to pay the Administration fee of $350.00 to ensure your unit is reserved until you move -in. Once you process your payment, you will be directed to prepare your lease.</p>";
            emailBody += "<p style=\"margin-bottom: 0px;\">Please click here to Click To Cancel Appointment</p>";
            emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + " /Appointment/?id=" + appid + "\" class=\"link-button\" target=\"_blank\">Click To Cancel Appointment</a></p>";
            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

            //reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
            //reportHTML = reportHTML.Replace("[%TenantName%]", saveTenant.FirstName + " " + saveTenant.LastName);
            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We’ll be contacting you shortly to confirm your appointment. </p>");
            //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + " /Appointment/?id=" + appid + "\"  style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Appointment/?id=" + appid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Click To Cancel Appointment</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

            string body = reportHTML;
            new EmailSendModel().SendEmail(saveTenant.EmailId, "Your appointment has been received", body);
            MSG = "Prospect Form Submitted Successfully";
            return MSG;
        }

        public List<ProspectModel> GetProspectusList()
        {
            ShomaRMEntities db = new ShomaRMEntities();

            return db.tbl_Prospect.ToList().Select(a=>new ProspectModel {
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhoneNo = a.PhoneNo,
                EmailId = a.EmailId,
                State = null,
                City =Convert.ToInt32( a.City),
                Address = a.Address,
                Message = a.Message,
                HavingPets = a.HavingPets,
                UnitID = a.UnitID,
                PropertyID = Convert.ToInt32(a.PropertyID),
                CreatedBy = 1,
                CreatedDate =a.CreatedDate,
                VisitDateTime = a.VisitDateTime,
                MarketSource = a.MarketSource,
                OutlookID = a.OutlookID
            }).ToList();
        }
    }
    
}