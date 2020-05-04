using ShomaRM.Data;
using ShomaRM.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ShomaRM.Areas.Tenant.Models
{
    public class ApplicantModel
    {
        public long ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string DateOfBirthTxt { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string Type { get; set; }
        public string Relationship { get; set; }

        public Nullable<decimal> MoveInPercentage { get; set; }
        public Nullable<decimal> MoveInCharge { get; set; }
        public Nullable<decimal> MonthlyPercentage { get; set; }
        public Nullable<decimal> MonthlyPayment { get; set; }
        public string ComplStatus { get; set; }
        public string OtherGender { get; set; }
        public string RelationshipString { get; set; }
        public string GenderString { get; set; }
        public int StepCompleted { get; set; }
        public long ProspectID { get; set; }
        public Nullable<int> Paid { get; set; }
        public string FeesPaidType { get; set; }

        //sachin m 04 May
        public string SSN { get; set; }
        public string IDNumber { get; set; }
        public string Country { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public Nullable<long> StateHome { get; set; }

        public string CityHome { get; set; }
        public string ZipHome { get; set; }
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];

        public string SaveUpdateApplicant(ApplicantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
            {
                model.DateOfBirth = null;
            }
            else
            {
                model.DateOfBirth = model.DateOfBirth;
            }
            if (model.ApplicantID == 0)
            {
                var saveApplicant = new tbl_Applicant()
                {
                    ApplicantID = model.ApplicantID,
                    TenantID = model.TenantID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Type = model.Type,
                    Relationship = model.Relationship,
                    OtherGender = model.OtherGender,
                    Paid=0,
                };
                db.tbl_Applicant.Add(saveApplicant);
                db.SaveChanges();

                var mainappdet = db.tbl_ApplyNow.Where(c => c.ID == model.TenantID).FirstOrDefault();
                string pass = "";
                string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
                Random randNum = new Random();
                char[] chars = new char[8];
                int allowedCharCount = _allowedChars.Length;
                for (int i = 0; i < 8; i++)
                {
                    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                }
                pass = new string(chars);

                string encpass = new EncryptDecrypt().EncryptText(pass);

                var createCoApplLogin = new tbl_Login()
                {
                    Username = model.Email,
                    Password = encpass,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    CellPhone = model.Phone,
                    IsActive = 1,
                    TenantID = 0,
                    IsSuperUser = 0,
                    UserType = model.Type == "Guarantor" ?34:33,
                    ParentUserID = ShomaGroupWebSession.CurrentUser.UserID,

                };
                db.tbl_Login.Add(createCoApplLogin);
                db.SaveChanges();
              
                var getAppldata = new tbl_TenantOnline()
                {
                    ProspectID = model.TenantID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    Mobile = model.Phone,
                    CreatedDate = DateTime.Now,
                    IsInternational = 0,
                    Gender = 0,
                    IDType = 0,
                    State = model.StateHome,
                    Country = model.Country,
                    StateHome = model.StateHome,
                    RentOwn = 0,
                    JobType = 0,
                    OfficeCountry = "1",
                    OfficeState = 0,
                    EmergencyCountry = "1",
                    EmergencyStateHome = 0,
                   
                    SSN = model.SSN,
                    IDNumber = model.IDNumber,
                    CityHome = model.CityHome,
                    HomeAddress1 = model.HomeAddress1,
                    HomeAddress2 = model.HomeAddress2,
                    ZipHome = model.ZipHome,
                    OtherGender = model.OtherGender,
                    //MiddleInitial = model.MiddleInitial,
                    ParentTOID = createCoApplLogin.UserID,

                };
                db.tbl_TenantOnline.Add(getAppldata);
                db.SaveChanges();
                if (model.Type == "Co-Applicant")
                {
                    if (model.Email != "")
                    {
                        string reportCoappHTML = "";

                        string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                        reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect5.html");

                        reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", model.Type);
                        reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added. Fill your Details");
                        reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                        reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string coappbody = reportCoappHTML;
                        new EmailSendModel().SendEmail(model.Email, "Your Application Added. Fill your Details", coappbody);

                        if (SendMessage == "yes")
                        {
                            new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                        }
                    }
                }
                if (model.Type == "Guarantor")
                {
                    if (model.Email != "")
                    {
                        string reportCoappHTML = "";

                        string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                        reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect5.html");

                        reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", model.Type);
                        reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added as Guarantor. Fill your Details");
                        reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added as Guarantor by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                        reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string coappbody = reportCoappHTML;
                        new EmailSendModel().SendEmail(model.Email, "Your Application Added. Fill your Details", coappbody);

                        if (SendMessage == "yes")
                        {
                            new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                        }
                    }
                }

                if (model.Type == "Primary Applicant")
                {
                    var updateTenantOnline = db.tbl_TenantOnline.Where(co => co.ProspectID == TenantID).FirstOrDefault();
                    if (updateTenantOnline != null)
                    {
                        updateTenantOnline.DateOfBirth = model.DateOfBirth;
                        updateTenantOnline.Gender = model.Gender;
                        updateTenantOnline.OtherGender = model.OtherGender;
                        db.SaveChanges();
                    }
                }

               

                msg = "Applicant Saved Successfully";
            }
            else
            {
                var getAppldata = db.tbl_Applicant.Where(p => p.ApplicantID == model.ApplicantID).FirstOrDefault();
                if (getAppldata != null)
                {

                    getAppldata.FirstName = model.FirstName;
                    getAppldata.LastName = model.LastName;
                    getAppldata.Phone = model.Phone;
                    getAppldata.Email = model.Email;
                    if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                    {
                        getAppldata.DateOfBirth = null;
                    }
                    else
                    {
                        getAppldata.DateOfBirth = model.DateOfBirth;
                    }

                    getAppldata.Gender = model.Gender;
                    getAppldata.Type = model.Type;
                    getAppldata.Relationship = model.Relationship;
                    getAppldata.OtherGender = model.OtherGender;

                    if (model.Type == "Primary Applicant")
                    {
                        var updateTenantOnline = db.tbl_TenantOnline.Where(co => co.ProspectID == model.TenantID).FirstOrDefault();
                        if (updateTenantOnline != null)
                        {
                            updateTenantOnline.DateOfBirth = model.DateOfBirth;
                            updateTenantOnline.Gender = model.Gender;
                            updateTenantOnline.OtherGender = model.OtherGender;

                            db.SaveChanges();
                        }
                    }
                }
                db.SaveChanges();
                msg = "Applicant Updated Successfully";
            }

            db.Dispose();
            return msg;


        }

        public List<ApplicantModel> GetApplicantList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplicantModel> lstProp = new List<ApplicantModel>();
            var vehList = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
            foreach (var ap in vehList)
            {
                string compl = "";
                string Rel = "";
                if (ap.Phone == null && ap.Email == null && ap.DateOfBirth == null)
                {
                    compl = "Unstarted";
                }
                else if (ap.Phone != null && ap.Email != null && ap.DateOfBirth != null)
                {
                    compl = "Completed";
                }
                else
                {
                    compl = "Pending";
                }
                DateTime? dobDateTime = null;
                try
                {
                    dobDateTime = Convert.ToDateTime(ap.DateOfBirth);
                }
                catch { }
                if (ap.Type == "Primary Applicant")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Self" : "";
                }
                else if (ap.Type == "Co-Applicant")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Spouse" : ap.Relationship == "2" ? "Partner" : ap.Relationship == "3" ? "Adult Child" : ap.Relationship == "4" ? "Friend/Roommate" : "";
                }
                else if (ap.Type == "Minor")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Family Member" : ap.Relationship == "2" ? "Child" : "";
                }
                else if (ap.Type == "Guarantor")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Family Member" : ap.Relationship == "2" ? "Friend" : "";
                }
                lstProp.Add(new ApplicantModel
                {
                    
                    ApplicantID = ap.ApplicantID,
                    FirstName = ap.FirstName,
                    LastName = ap.LastName,
                    Phone = ap.Phone,
                    Email = ap.Email,
                    Type = ap.Type,
                    Gender = ap.Gender,
                    MoveInPercentage = ap.MoveInPercentage != null ? ap.MoveInPercentage : 0,
                    MoveInCharge = ap.MoveInCharge != null ? ap.MoveInCharge : 0,
                    MonthlyPercentage = ap.MonthlyPercentage != null ? ap.MonthlyPercentage : 0,
                    MonthlyPayment = ap.MonthlyPayment != null ? ap.MonthlyPayment : 0,
                    ComplStatus = compl,
                    OtherGender = ap.OtherGender != null ? OtherGender : "",
                    
                    RelationshipString = Rel,
                    DateOfBirth = ap.DateOfBirth,
                    DateOfBirthTxt = dobDateTime == null ? "" : dobDateTime.Value.ToString("MM/dd/yyyy"),
                    GenderString = ap.Gender == 1 ? "Male" : ap.Gender == 2 ? "Female" : ap.Gender == 3 ? "Other" : "",
                    Paid=ap.Paid==null?0:ap.Paid

                });
            }
            return lstProp;
        }
        public ApplicantModel GetApplicantDetails(int id,int chargetype)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantModel model = new ApplicantModel();

            var getApplicantDet = db.tbl_Applicant.Where(p => p.ApplicantID == id).FirstOrDefault();
            var getTenantDet = db.tbl_ApplyNow.Where(p => p.ID == getApplicantDet.TenantID).FirstOrDefault();
            var getAppliTransDet = db.tbl_Transaction.Where(p => p.TenantID ==getTenantDet.UserId  && p.Batch==id.ToString() && p.Charge_Type== chargetype).FirstOrDefault();
            if (getAppliTransDet == null)
            {
                DateTime? dobDateTime = null;
                try
                {

                    dobDateTime = Convert.ToDateTime(getApplicantDet.DateOfBirth);
                }
                catch
                {

                }

                model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy")!="01/01/0001"? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
                model.FirstName = getApplicantDet.FirstName;
                model.LastName = getApplicantDet.LastName;
                model.Phone = getApplicantDet.Phone;
                model.Email = getApplicantDet.Email;
                model.Gender = getApplicantDet.Gender;
                model.Type = getApplicantDet.Type;
                model.Relationship = getApplicantDet.Relationship;
                model.MoveInPercentage = getApplicantDet.MoveInPercentage;
                model.MoveInCharge = getApplicantDet.MoveInCharge;
                model.MonthlyPercentage = getApplicantDet.MonthlyPercentage;
                model.MonthlyPayment = getApplicantDet.MonthlyPayment;
                model.OtherGender = getApplicantDet.OtherGender;
                model.TenantID = getApplicantDet.TenantID;
            }
            else
            {
                model.FirstName = getApplicantDet.FirstName;
                model.LastName = getApplicantDet.LastName;
                model.FeesPaidType = GetChargeType(getAppliTransDet.Charge_Type ?? 0);
                model.Type = "5";
            }
            return model;
        }
        public string GetChargeType(int chargetype)
        {
            string chargeType = "";
            if (chargetype == 1)
            {
                chargeType = "Application Fees";
            }
            else if (chargetype == 2)
            {
                chargeType = "Move In Charge";
            }
            else if (chargetype == 3)
            {
                chargeType = "Administrative Fee";
            }
            else
            {
                chargeType = "General/Miscellaneous Charge";
            }
            return chargeType;

        }
        public string DeleteApplicant(long AID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (AID != 0)
            {

                var appliData = db.tbl_Applicant.Where(p => p.ApplicantID == AID).FirstOrDefault();
                if (appliData != null)
                {
                    db.tbl_Applicant.Remove(appliData);
                    db.SaveChanges();
                  
                }

                var tenData = db.tbl_Applicant.Where(p => p.ApplicantID == AID).FirstOrDefault();
                if (tenData != null)
                {
                    db.tbl_Applicant.Remove(tenData);
                    db.SaveChanges();
                   
                }
                var logData = db.tbl_Applicant.Where(p => p.ApplicantID == AID).FirstOrDefault();
                if (logData != null)
                {
                    db.tbl_Applicant.Remove(logData);
                    db.SaveChanges();
                    msg = "Applicant Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public string SaveUpdatePaymentResponsibility(List<ApplicantModel> model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            long prospectID = 0;
            int stepcompModel = 0;
            foreach (ApplicantModel item in model)
            {
                prospectID = item.ProspectID;
                stepcompModel = item.StepCompleted;
                if (item.ApplicantID == 0)
                {
                    var saveResponsibility = new tbl_Applicant()
                    {
                        MoveInPercentage = item.MoveInPercentage,
                        MoveInCharge = item.MoveInCharge,
                        MonthlyPercentage = item.MonthlyPercentage,
                        MonthlyPayment = item.MonthlyPayment
                    };
                    db.tbl_Applicant.Add(saveResponsibility);
                    db.SaveChanges();


                    msg = "Applicant Saved Successfully";
                }
                else
                {
                    var getAppldata = db.tbl_Applicant.Where(p => p.ApplicantID == item.ApplicantID).FirstOrDefault();
                    if (getAppldata != null)
                    {
                        getAppldata.MoveInPercentage = item.MoveInPercentage;
                        getAppldata.MoveInCharge = item.MoveInCharge;
                        getAppldata.MonthlyPercentage = item.MonthlyPercentage;
                        getAppldata.MonthlyPayment = item.MonthlyPayment;

                    }
                    db.SaveChanges();
                    msg = "Applicant Updated Successfully";
                }
            }

            var applyNow = db.tbl_ApplyNow.Where(p => p.ID == prospectID).FirstOrDefault();

            if(applyNow!=null)
            {
                int stepcomp = 0;
                stepcomp = applyNow.StepCompleted ?? 0;
                if (stepcomp < stepcompModel)
                {
                    stepcomp = stepcompModel;
                    db.SaveChanges();
                }
            }

            db.Dispose();
            return msg;
        }
    }
}