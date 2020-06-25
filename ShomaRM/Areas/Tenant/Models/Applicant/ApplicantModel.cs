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

        public string SSN { get; set; }
        public string SSNEnc { get; set; }
        public string IDNumber { get; set; }
        public string IDNumberEnc { get; set; }
        public string Country { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public Nullable<long> StateHome { get; set; }
        public Nullable<long> State { get; set; }

        public string CityHome { get; set; }
        public string ZipHome { get; set; }
        public string MiddleName { get; set; }

        public Nullable<long> AddedBy { get; set; }
        public Nullable<int> IDType { get; set; }

        public long? ApplicantUserId { get; set; }
        public long? ApplicantAddedBy { get; set; }
        public Nullable<int> CreditPaid { get; set; }
        public Nullable<int> BackGroundPaid { get; set; }
        public Nullable<decimal> AppCreditFees { get; set; }
        public Nullable<decimal> AppBackGroundFees { get; set; }
        public Nullable<decimal> GuarCreditFees { get; set; }
        public Nullable<decimal> GuarBackGroundFees { get; set; }

        public int HasSSN { get; set; }
       

        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];

        public Nullable<decimal> AdminFee { get; set; }
        public Nullable<decimal> AdminFeePercentage { get; set; }
        public int IsInternational { get; set; }
        public string SaveUpdateApplicant(ApplicantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var mainappdet = db.tbl_ApplyNow.Where(c => c.ID == model.TenantID).FirstOrDefault();
            var countCoApp = db.tbl_Applicant.Where(p => p.TenantID == model.TenantID && p.Type == "Co-Applicant").Count();
            var countMinor = db.tbl_Applicant.Where(p => p.TenantID == model.TenantID && p.Type == "Minor").Count();
            var countGuar = db.tbl_Applicant.Where(p => p.TenantID == model.TenantID && p.Type == "Guarantor").Count();
            var unitDet = db.tbl_PropertyUnits.Where(p => p.UID == mainappdet.PropertyId).FirstOrDefault();

            string ApplyNowQuotationNo = "";

            if (model.ApplicantID == 0)
            {
                if (model.Type != "Guarantor")
                {
                    if ((unitDet.Bedroom ?? 0) == 1)
                    {
                        if (countCoApp == 1 || countMinor == 1)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                    }
                    if ((unitDet.Bedroom ?? 0) == 2)
                    {
                        if (countCoApp + countMinor > 3)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                    }
                    if ((unitDet.Bedroom ?? 0) == 3)
                    {
                        if (countCoApp > 3 && countMinor > 3)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                        else if (countCoApp + countMinor > 5)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                    }
                }
                else
                {
                    if (countGuar == 1)
                    {
                        msg = model.Type + " is already added.";
                        return msg;
                    }
                }
            }
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
                    Paid = 0,
                    CreditPaid = 0,
                    BackGroundPaid = 0,
                    AddedBy = userid
                };
                db.tbl_Applicant.Add(saveApplicant);
                db.SaveChanges();

                
                string pass = "";
                string _allowedChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*";

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
                    UserType = model.Type == "Guarantor" ? 34 : 33,
                    ParentUserID = mainappdet.UserId,

                };
                db.tbl_Login.Add(createCoApplLogin);
                db.SaveChanges();

                saveApplicant.UserID = createCoApplLogin.UserID;
                db.SaveChanges();

                if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                {
                    model.DateOfBirth = null;
                }
                else
                {
                    model.DateOfBirth = model.DateOfBirth;
                }
                var getAppldata = new tbl_TenantOnline()
                {
                    ProspectID = model.TenantID,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Email = model.Email,
                    Mobile = model.Phone,
                    PassportNumber = "",

                    IDType = model.IDType,
                    State = model.State,
                    IDNumber = model.IDNumber,
                    Country = model.Country,
                    HomeAddress1 = model.HomeAddress1,
                    HomeAddress2 = model.HomeAddress2,
                    StateHome = model.StateHome,
                    CityHome = model.CityHome,
                    ZipHome = model.ZipHome,
                    RentOwn = 0,
                    ////MoveInDate = model.MoveInDate,
                    JobType = 0,
                    OfficeCountry = "1",
                    OfficeState = 0,
                    EmergencyCountry = "1",
                    EmergencyStateHome = 0,
                    CreatedDate = DateTime.Now,
                    IsInternational = model.IsInternational,
                    OtherGender = model.OtherGender,
                    Country2 = "1",
                    StateHome2 = 0,
                    ZipHome2 = "",
                    RentOwn2 = 0,
                    SSN = model.SSN,
                    CountryOfOrigin = 1,
                    Evicted = 1,
                    ConvictedFelony = 1,
                    CriminalChargPen = 1,
                    DoYouSmoke = 1,
                    ReferredResident = 1,
                    ReferredBrokerMerchant = 1,
                    IsProprNoticeLeaseAgreement = 1,
                    StepCompleted = 4,
                    ParentTOID = createCoApplLogin.UserID,
                    IsRentalPolicy = 0,
                    IsRentalQualification = 0,
                };
                db.tbl_TenantOnline.Add(getAppldata);
                db.SaveChanges();
                if (model.Type == "Co-Applicant")
                {
                    ApplyNowQuotationNo = mainappdet.CreatedDate.HasValue ? mainappdet.CreatedDate.Value.ToString("yyyyMMddHHmm") + "-" + mainappdet.ID.ToString() : "";
                    if (model.Email != "")
                    {
                        if((createCoApplLogin.IsTempPass??1)==1)
                        {
                            createCoApplLogin.IsTempPass = 1;
                            db.SaveChanges();
                        }


                        string reportCoappHTML = "";
                        string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        //reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect5.html");
                        reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect.html");
                        reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);
                        reportCoappHTML = reportCoappHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));


                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Co-applicant. Fill your Details.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                        // emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                        reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", emailBody);



                        //reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", model.Type);
                        //reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added As Co-applicant. Fill your Details");
                        //reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                        //reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        //reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string coappbody = reportCoappHTML;
                        new EmailSendModel().SendEmail(model.Email, "Your Application Added as Co-applicant. Fill your Details", coappbody);

                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.Phone))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Hello this is Sanctuary Doral. " + mainappdet.FirstName + " " + mainappdet.LastName + " has added you as a "+ model.Type + ". Please go to "+ serverURL+"/Account/Login and signin with ID: " + model.Email + " & Password :" + pass );
                            }
                        }
                    }
                }
                else if (model.Type == "Guarantor")
                {
                    if (model.Email != "")
                    {
                        if ((createCoApplLogin.IsTempPass ?? 1) == 1)
                        {
                            createCoApplLogin.IsTempPass = 1;
                            db.SaveChanges();
                        }

                        string reportGurHTML = "";
                        string gurfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        //reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect5.html");
                        //reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);


                        reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect.html");
                        reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);
                        reportGurHTML = reportGurHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));


                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Guarantor. Fill your Details.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                        //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                        reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", emailBody);




                        //reportGurHTML = reportGurHTML.Replace("[%CoAppType%]", model.Type);
                        //reportGurHTML = reportGurHTML.Replace("[%EmailHeader%]", "Your Application Added As Guarantor. Fill your Details");
                        //reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", "Your are Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                        //reportGurHTML = reportGurHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        //reportGurHTML = reportGurHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string gurbody = reportGurHTML;
                        new EmailSendModel().SendEmail(model.Email, "Your Application Added As Guaranter. Fill your Details", gurbody);

                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.Phone))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Hello this is Sanctuary Doral. " + mainappdet.FirstName + " " + mainappdet.LastName + " has added you as a " + model.Type + ". Please go to " + serverURL + "/Account/Login and signin with ID: " + model.Email + " & Password :" + pass);
                            }
                        }
                    }
                }
                else
                {
                    saveApplicant.CreditPaid = 1;
                    saveApplicant.Paid = 1;
                    saveApplicant.BackGroundPaid = 1;
                    db.SaveChanges();
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

                msg = "Progress Saved.";
            }
            else
            {
                var getAppldata = db.tbl_Applicant.Where(p => p.ApplicantID == model.ApplicantID).FirstOrDefault();
                int loggedinuserid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
                if (getAppldata != null)
                {
                    var oldemail = getAppldata.Email;
                    getAppldata.FirstName = model.FirstName;
                    getAppldata.LastName = model.LastName;
                    getAppldata.Phone = model.Phone;
                    getAppldata.Email = model.Email;

                    var userdata = db.tbl_Login.Where(p => p.UserID == getAppldata.UserID).FirstOrDefault();

                    string pass = "";
                    try
                    {
                        pass = new EncryptDecrypt().DecryptText(userdata.Password);
                    }
                    catch
                    {
                        pass = "";
                    }

                    if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                    {
                        getAppldata.DateOfBirth = null;
                    }
                    else
                    {
                        getAppldata.DateOfBirth = model.DateOfBirth;
                    }

                    getAppldata.Gender = model.Gender;
                    getAppldata.Relationship = model.Relationship;
                    getAppldata.OtherGender = model.OtherGender;

                    if (model.Type == "Primary Applicant")
                    {
                        var updateTenantOnline = db.tbl_TenantOnline.Where(co => co.ProspectID == model.TenantID).FirstOrDefault();
                        if (updateTenantOnline != null)
                        {
                            //updateTenantOnline.DateOfBirth = model.DateOfBirth;
                            //updateTenantOnline.Gender = model.Gender;
                            //updateTenantOnline.OtherGender = model.OtherGender;
                            //updateTenantOnline.SSN = model.SSN;
                            //updateTenantOnline.StateHome = model.StateHome;
                            //updateTenantOnline.HomeAddress1 = model.HomeAddress1;
                            //updateTenantOnline.HomeAddress2 = model.HomeAddress2;
                            //updateTenantOnline.CityHome = model.CityHome;
                            //updateTenantOnline.ZipHome = model.ZipHome;
                            //updateTenantOnline.MiddleInitial = model.MiddleName;
                            //updateTenantOnline.Country = model.Country;

                            updateTenantOnline.FirstName = model.FirstName;
                            updateTenantOnline.MiddleInitial = model.MiddleName;
                            updateTenantOnline.LastName = model.LastName;
                            updateTenantOnline.DateOfBirth = model.DateOfBirth;
                            updateTenantOnline.Gender = model.Gender;
                            updateTenantOnline.Mobile = model.Phone;
                            updateTenantOnline.Email = model.Email;
                            updateTenantOnline.SSN = model.SSN;
                            updateTenantOnline.StateHome = model.StateHome;
                            updateTenantOnline.HomeAddress1 = model.HomeAddress1;
                            updateTenantOnline.HomeAddress2 = model.HomeAddress2;
                            updateTenantOnline.CityHome = model.CityHome;
                            updateTenantOnline.ZipHome = model.ZipHome;
                            updateTenantOnline.Country = model.Country;
                            updateTenantOnline.IsInternational = model.IsInternational;
                            db.SaveChanges();
                        }
                    }
                    //var getLoginDet = db.tbl_Login.Where(p => p.Email == getAppldata.Email).FirstOrDefault();
                    //if (getLoginDet != null)
                    //{
                    var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == userid).FirstOrDefault();
                    if (getTenantOnline != null)
                    {
                        getTenantOnline.FirstName = model.FirstName;
                        getTenantOnline.MiddleInitial = model.MiddleName;
                        getTenantOnline.LastName = model.LastName;
                        getTenantOnline.DateOfBirth = model.DateOfBirth;
                        getTenantOnline.Gender = model.Gender;
                        getTenantOnline.Mobile = model.Phone;
                        getTenantOnline.Email = model.Email;
                        getTenantOnline.SSN = model.SSN;
                        getTenantOnline.IDNumber = model.IDNumber;
                        getTenantOnline.IDType = model.IDType;
                        getTenantOnline.State = model.State;
                        getTenantOnline.StateHome = model.StateHome;
                        getTenantOnline.HomeAddress1 = model.HomeAddress1;
                        getTenantOnline.HomeAddress2 = model.HomeAddress2;
                        getTenantOnline.CityHome = model.CityHome;
                        getTenantOnline.ZipHome = model.ZipHome;
                        getTenantOnline.Country = model.Country;
                        getTenantOnline.IsInternational = model.IsInternational;
                        db.SaveChanges();
                    }
                    //}

                    db.SaveChanges();
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    //reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect5.html");
                    //reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);

                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));


                    string body = "";
                    if ((getAppldata.CreditPaid ?? 0) == 0 && loggedinuserid == getAppldata.UserID)
                    {

                        var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                        string payid = new EncryptDecrypt().EncryptText(getAppldata.ApplicantID.ToString() + ",4," + propertDet.AppCCCheckFees.Value.ToString("0.00"));


                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Payment Link for Credit Check.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please pay your fees $" + propertDet.AppCCCheckFees.Value.ToString("0.00") + " for credit check to continue the Online Application Process.</p>";
                        //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);



                        //reportHTML = reportHTML.Replace("[%CoAppType%]", model.Type);
                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Payment Link for Credit Check");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", "Please pay your fees $" + propertDet.AppCCCheckFees.Value.ToString("0.00") + " for credit check to continue the Online Application Process.<br/><br/>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        body = reportHTML;
                        new EmailSendModel().SendEmail(model.Email, "Payment Link for Credit Check", body);
                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.Phone))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Payment Link for Credit Check. Please check the email for detail.");
                            }
                        }
                    }
                    else if ((getAppldata.CreditPaid ?? 0) == 0 && oldemail != model.Email)
                    {
                        userdata.Email = model.Email;
                        userdata.Username = model.Email;
                        db.SaveChanges();

                        if (model.Type == "Co-Applicant")
                        {
                            if (model.Email != "")
                            {
                                string reportCoappHTML = "";
                                string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                                //reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect5.html");
                                //reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);

                                reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect.html");
                                reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);
                                reportCoappHTML = reportCoappHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                                string emailBody = "";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Co-applicant. Fill your Details.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass +"</p>";
                                //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                                emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                                reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", emailBody);

                                //reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", model.Type);
                                //reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added As Co-applicant. Fill your Details");
                                //reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                                //reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                                //reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                                string coappbody = reportCoappHTML;
                                new EmailSendModel().SendEmail(model.Email, "Your Application Added as Co-applicant. Fill your Details", coappbody);

                                if (SendMessage == "yes")
                                {
                                    if (!string.IsNullOrWhiteSpace(model.Phone))
                                    {
                                        new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added As Co-applicant. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                                    }
                                }
                            }
                        }
                        if (model.Type == "Guarantor")
                        {
                            if (model.Email != "")
                            {
                                string reportGurHTML = "";
                                string gurfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                                reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect5.html");
                                reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);

                                reportGurHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                                reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);
                                reportGurHTML = reportGurHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));


                                string emailBody = "";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Guarantor. Fill your Details.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                                // emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                                emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                                reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", emailBody);


                                //reportGurHTML = reportGurHTML.Replace("[%CoAppType%]", model.Type);
                                //reportGurHTML = reportGurHTML.Replace("[%EmailHeader%]", "Your Application Added As Guarantor. Fill your Details");
                                //reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", "Your are Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                                //reportGurHTML = reportGurHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                                //reportGurHTML = reportGurHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                                string gurbody = reportGurHTML;
                                new EmailSendModel().SendEmail(model.Email, "Your Application Added As Guaranter. Fill your Details", gurbody);

                                if (SendMessage == "yes")
                                {
                                    if (!string.IsNullOrWhiteSpace(model.Phone))
                                    {
                                        new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added As Guarantor. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                                    }
                                }
                            }
                        }
                    }

                }
                msg = "Progress Saved.";
            }

            db.Dispose();
            return msg;
        }

        public List<ApplicantModel> GetApplicantList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplicantModel> lstAppli = new List<ApplicantModel>();

            var propertyData = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
            var applicantDataList = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
            decimal appCreditFees = 0;
            decimal appBackGroundFees = 0;
            decimal guarCreditFees = 0;
            decimal guarBackGroundFees = 0;

            appCreditFees = propertyData.AppCCCheckFees ?? 0;
            appBackGroundFees = propertyData.AppCCCheckFees ?? 0;
            guarCreditFees = propertyData.AppCCCheckFees ?? 0;
            guarBackGroundFees = propertyData.AppCCCheckFees ?? 0;

            foreach (var ap in applicantDataList)
            {
                int hasSSN = 0;
                var tenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ap.UserID).FirstOrDefault();
                if (tenantOnline != null)
                {
                    if (!string.IsNullOrWhiteSpace(tenantOnline.SSN))
                    {
                        hasSSN = 1;
                    }
                }

                string compl = "";
                string Rel = "";
                if ((ap.CreditPaid ?? 0) == 0)
                {
                    compl = "In Progress (Credit Check Not Done)";
                }
                else if ((ap.BackGroundPaid ?? 0) == 0)
                {
                    compl = "In Progress (Background Check Not Done)";
                }
                else
                {
                    compl = "Completed";
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
                lstAppli.Add(new ApplicantModel
                {
                    ApplicantID = ap.ApplicantID,
                    FirstName = ap.FirstName,
                    LastName = ap.LastName,
                    Phone = !string.IsNullOrWhiteSpace(ap.Phone) ? ap.Phone : "",
                    Email = !string.IsNullOrWhiteSpace(ap.Email) ? ap.Email : "",
                    Type = ap.Type,
                    Gender = ap.Gender,
                    MoveInPercentage = ap.MoveInPercentage != null ? ap.MoveInPercentage : 0,
                    MoveInCharge = ap.MoveInCharge != null ? ap.MoveInCharge : 0,
                    MonthlyPercentage = ap.MonthlyPercentage != null ? ap.MonthlyPercentage : 0,
                    MonthlyPayment = ap.MonthlyPayment != null ? ap.MonthlyPayment : 0,
                    AdminFeePercentage = ap.AdminFeePercentage != null ? ap.AdminFeePercentage : 0,
                    AdminFee = ap.AdminFee != null ? ap.AdminFee : 0,

                    ComplStatus = compl,
                    OtherGender = ap.OtherGender != null ? OtherGender : "",

                    RelationshipString = Rel,
                    DateOfBirth = ap.DateOfBirth,
                    DateOfBirthTxt = dobDateTime == null ? "" : dobDateTime.Value.ToString("MM/dd/yyyy"),
                    GenderString = ap.Gender == 1 ? "Male" : ap.Gender == 2 ? "Female" : ap.Gender == 3 ? "Other" : "",
                    Paid = ap.Paid == null ? 0 : ap.Paid,
                    AddedBy = ap.AddedBy,
                    CreditPaid = ap.CreditPaid ?? 0,
                    BackGroundPaid = ap.BackGroundPaid ?? 0,
                    AppCreditFees = propertyData.AppCCCheckFees,
                    AppBackGroundFees = propertyData.AppBGCheckFees,
                    GuarCreditFees = propertyData.GuaCCCheckFees,
                    GuarBackGroundFees = propertyData.GuaBGCheckFees,
                    HasSSN = hasSSN,
                    StepCompleted=Convert.ToInt32(tenantOnline.StepCompleted),
                });
            }
            return lstAppli;
        }

        public List<ApplicantModel> GetCoApplicantList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplicantModel> lstProp = new List<ApplicantModel>();

            var propertyData = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
            var applicantDataList = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
            decimal appCreditFees = 0;
            decimal appBackGroundFees = 0;
            decimal guarCreditFees = 0;
            decimal guarBackGroundFees = 0;

            appCreditFees = propertyData.AppCCCheckFees ?? 0;
            appBackGroundFees = propertyData.AppCCCheckFees ?? 0;
            guarCreditFees = propertyData.AppCCCheckFees ?? 0;
            guarBackGroundFees = propertyData.AppCCCheckFees ?? 0;

            long addedby = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var CoAppDataList = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
            foreach (var ap in CoAppDataList)
            {
                int hasSSN = 0;
                var tenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ap.UserID).FirstOrDefault();
                if (tenantOnline != null)
                {
                    if (!string.IsNullOrWhiteSpace(tenantOnline.SSN))
                    {
                        hasSSN = 1;
                    }
                }

                string compl = "";
                string Rel = "";
                if ((ap.CreditPaid ?? 0) == 0)
                {
                    compl = "In Progress (Credit Check Not Done)";
                }
                else if ((ap.Paid ?? 0) == 0)
                {
                    compl = "In Progress (Background Check Not Done)";
                }
                else
                {
                    compl = "Completed";
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
                    Phone = !string.IsNullOrWhiteSpace(ap.Phone) ? ap.Phone : "",
                    Email = !string.IsNullOrWhiteSpace(ap.Email) ? ap.Email : "",
                    Type = ap.Type,
                    Gender = ap.Gender,
                    MoveInPercentage = ap.MoveInPercentage != null ? ap.MoveInPercentage : 0,
                    MoveInCharge = ap.MoveInCharge != null ? ap.MoveInCharge : 0,
                    MonthlyPercentage = ap.MonthlyPercentage != null ? ap.MonthlyPercentage : 0,
                    MonthlyPayment = ap.MonthlyPayment != null ? ap.MonthlyPayment : 0,
                    AdminFee = ap.AdminFee != null ? ap.AdminFee : 0,
                    AdminFeePercentage = ap.AdminFeePercentage != null ? ap.AdminFeePercentage :0,
                    ComplStatus = compl,
                    OtherGender = ap.OtherGender != null ? OtherGender : "",

                    RelationshipString = Rel,
                    DateOfBirth = ap.DateOfBirth,
                    DateOfBirthTxt = dobDateTime == null ? "" : dobDateTime.Value.ToString("MM/dd/yyyy"),
                    GenderString = ap.Gender == 1 ? "Male" : ap.Gender == 2 ? "Female" : ap.Gender == 3 ? "Other" : "",
                    Paid = ap.Paid == null ? 0 : ap.Paid,
                    AddedBy = addedby,
                    CreditPaid = ap.CreditPaid ?? 0,
                    BackGroundPaid = ap.BackGroundPaid ?? 0,
                    AppCreditFees = propertyData.AppCCCheckFees,
                    AppBackGroundFees = propertyData.AppBGCheckFees,
                    GuarCreditFees = propertyData.GuaCCCheckFees,
                    GuarBackGroundFees = propertyData.GuaBGCheckFees,
                    ApplicantUserId = ap.UserID,
                    ApplicantAddedBy = ap.AddedBy??0,
                    HasSSN = hasSSN
                });
            }

           
            return lstProp;
        }
        
        public ApplicantModel GetApplicantDetails(int id, int chargetype)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantModel model = new ApplicantModel();
            var ptotid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            string bat = "";
            if (chargetype == 1)
            {
                bat = id.ToString();
                var coappliList = db.tbl_Applicant.Where(pp => pp.ApplicantID == id).FirstOrDefault();
                if (coappliList != null)
                {
                    coappliList.Paid = 1;
                    db.SaveChanges();
                }
            }
            else
            {
                bat = "1";
            }
            if (chargetype == 4)
            {
                bat = "4";
            }
            if (chargetype == 5)
            {
                bat = "5";
            }
            //var getApplicantDet = db.tbl_Applicant.Where(p => p.ApplicantID == id).FirstOrDefault();
            //var getTenantDet = db.tbl_ApplyNow.Where(p => p.ID == getApplicantDet.TenantID).FirstOrDefault();
            var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ApplicantID == id).FirstOrDefault();
            //var GetTenantData = db.tbl_TenantOnline.Where(P => P.ParentTOID == getApplicantDet.UserID).FirstOrDefault();

            string transType = "0";
            if (GetPayDetails != null)
            {
                transType = GetPayDetails.ID.ToString();
            }
            var getApplicantDet = db.tbl_Applicant.Where(p => p.ApplicantID == id).FirstOrDefault();
            var getTenantDet = db.tbl_ApplyNow.Where(p => p.ID == getApplicantDet.TenantID).FirstOrDefault();
            var getAppliTransDet = db.tbl_Transaction.Where(p => p.TenantID == getTenantDet.UserId && p.PAID == transType).FirstOrDefault();
            //if(ptotid==0)
            //{
                ptotid = getApplicantDet.UserID??0;
            //}
            //var getAppliTransDet = db.tbl_Transaction.Where(p => p.TenantID == getTenantDet.UserId && p.Batch == id.ToString() && p.Charge_Type == chargetype).FirstOrDefault();
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

                model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy") != "01/01/0001" ? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
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
                model.AdminFee = getApplicantDet.AdminFee;
                model.AdminFeePercentage = getApplicantDet.AdminFeePercentage;
                model.OtherGender = getApplicantDet.OtherGender;
                model.TenantID = getApplicantDet.TenantID;
                model.ApplicantAddedBy = getApplicantDet.AddedBy;
                model.ApplicantUserId = getApplicantDet.UserID;
                //New
                var getTenantOnline = db.tbl_TenantOnline.Where(p =>  p.ParentTOID==ptotid).FirstOrDefault();
                model.IsInternational =Convert.ToInt32(getTenantOnline.IsInternational);
                if (!string.IsNullOrWhiteSpace(getTenantOnline.IDNumber))
                {
                    try
                    {
                        model.IDNumberEnc = getTenantOnline.IDNumber;
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(getTenantOnline.IDNumber);
                        int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < idnumlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedIDNumber.Length > 4)
                        {
                            model.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                        }
                        else
                        {
                            model.IDNumber = decryptedIDNumber;
                        }
                    }
                    catch
                    {
                        model.IDNumberEnc = "";
                        model.IDNumber = "";
                    }

                }
                else
                {
                    model.IDNumberEnc = "";
                    model.IDNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(getTenantOnline.SSN))
                {
                    try
                    {
                        model.SSNEnc = getTenantOnline.SSN;
                        string decryptedSSN = new EncryptDecrypt().DecryptText(getTenantOnline.SSN);
                        if (decryptedSSN.Length > 5)
                        {
                            model.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 5, 4);
                        }
                        else
                        {
                            model.SSN = decryptedSSN;
                        }
                    }
                    catch
                    {
                        model.SSNEnc = "";
                        model.SSN = "";
                    }

                }
                else
                {
                    model.SSNEnc = "";
                    model.SSN = ""; ;
                }

                model.IDType = getTenantOnline.IDType;
                model.State = getTenantOnline.State;
                model.MiddleName = getTenantOnline.MiddleInitial;
                model.Country = !string.IsNullOrWhiteSpace(getTenantOnline.Country) ? getTenantOnline.Country : "1";
                model.StateHome = getTenantOnline.StateHome??0;
                model.HomeAddress1 = getTenantOnline.HomeAddress1;
                model.HomeAddress2 = getTenantOnline.HomeAddress2;
                model.CityHome = getTenantOnline.CityHome;
                model.ZipHome = getTenantOnline.ZipHome;
                model.MiddleName = getTenantOnline.MiddleInitial;
            }
            else if (getAppliTransDet != null && chargetype != 0)
            {
                DateTime? dobDateTime = null;
                try
                {
                    dobDateTime = Convert.ToDateTime(getApplicantDet.DateOfBirth);
                }
                catch
                {
                }

                model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy") != "01/01/0001" ? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
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
                model.AdminFee = getApplicantDet.AdminFee;
                model.AdminFeePercentage = getApplicantDet.AdminFeePercentage;
                model.OtherGender = getApplicantDet.OtherGender;
                model.TenantID = getApplicantDet.TenantID;
                model.ApplicantAddedBy = getApplicantDet.AddedBy;
                model.ApplicantUserId = getApplicantDet.UserID;
                //New
                var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ptotid).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(getTenantOnline.IDNumber))
                {
                    try
                    {
                        model.IDNumberEnc = getTenantOnline.IDNumber;
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(getTenantOnline.IDNumber);
                        int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < idnumlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedIDNumber.Length > 4)
                        {
                            model.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                        }
                        else
                        {
                            model.IDNumber = decryptedIDNumber;
                        }
                    }
                    catch
                    {
                        model.IDNumberEnc = "";
                        model.IDNumber = "";
                    }

                }
                else
                {
                    model.IDNumberEnc = "";
                    model.IDNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(getTenantOnline.SSN))
                {
                    try
                    {
                        model.SSNEnc = getTenantOnline.SSN;
                        string decryptedSSN = new EncryptDecrypt().DecryptText(getTenantOnline.SSN);
                        if (decryptedSSN.Length > 5)
                        {
                            model.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 5, 4);
                        }
                        else
                        {
                            model.SSN = decryptedSSN;
                        }
                    }
                    catch
                    {
                        model.SSNEnc = "";
                        model.SSN = "";
                    }

                }
                else
                {
                    model.SSNEnc = "";
                    model.SSN = ""; ;
                }

                model.IDType = getTenantOnline.IDType;
                model.State = getTenantOnline.State;
                model.MiddleName = getTenantOnline.MiddleInitial;
                model.Country = !string.IsNullOrWhiteSpace(getTenantOnline.Country) ? getTenantOnline.Country : "1";
                model.StateHome = getTenantOnline.StateHome ?? 0;
                model.HomeAddress1 = getTenantOnline.HomeAddress1;
                model.HomeAddress2 = getTenantOnline.HomeAddress2;
                model.CityHome = getTenantOnline.CityHome;
                model.ZipHome = getTenantOnline.ZipHome;
                model.MiddleName = getTenantOnline.MiddleInitial;
            }
            else if (getAppliTransDet != null && chargetype == 0)
            {

                DateTime? dobDateTime = null;
                try
                {
                    dobDateTime = Convert.ToDateTime(getApplicantDet.DateOfBirth);
                }
                catch
                {
                }

                model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy") != "01/01/0001" ? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
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
                model.AdminFee = getApplicantDet.AdminFee;
                model.AdminFeePercentage = getApplicantDet.AdminFeePercentage;
                model.OtherGender = getApplicantDet.OtherGender;
                model.TenantID = getApplicantDet.TenantID;
                model.ApplicantAddedBy = getApplicantDet.AddedBy;
                model.ApplicantUserId = getApplicantDet.UserID;
                //New

                var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ptotid).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(getTenantOnline.IDNumber))
                {
                    try
                    {
                        model.IDNumberEnc = getTenantOnline.IDNumber;
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(getTenantOnline.IDNumber);
                        int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < idnumlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedIDNumber.Length > 4)
                        {
                            model.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                        }
                        else
                        {
                            model.IDNumber = decryptedIDNumber;
                        }
                    }
                    catch
                    {
                        model.IDNumberEnc = "";
                        model.IDNumber = "";
                    }

                }
                else
                {
                    model.IDNumberEnc = "";
                    model.IDNumber = "";
                }

                if (!string.IsNullOrWhiteSpace(getTenantOnline.SSN))
                {
                    try
                    {
                        model.SSNEnc = getTenantOnline.SSN;
                        string decryptedSSN = new EncryptDecrypt().DecryptText(getTenantOnline.SSN);
                        if (decryptedSSN.Length > 5)
                        {
                            model.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 5, 4);
                        }
                        else
                        {
                            model.SSN = decryptedSSN;
                        }
                    }
                    catch
                    {
                        model.SSNEnc = "";
                        model.SSN = "";
                    }

                }
                else
                {
                    model.SSNEnc = "";
                    model.SSN = ""; ;
                }

                model.IDType = getTenantOnline.IDType;
                model.State = getTenantOnline.State;
                model.MiddleName = getTenantOnline.MiddleInitial;
                model.Country = getTenantOnline.Country;
                model.StateHome = getTenantOnline.StateHome;
                model.HomeAddress1 = getTenantOnline.HomeAddress1;
                model.HomeAddress2 = getTenantOnline.HomeAddress2;
                model.CityHome = getTenantOnline.CityHome;
                model.ZipHome = getTenantOnline.ZipHome;
                model.MiddleName = getTenantOnline.MiddleInitial;

            }
            else
            {
                model.FirstName = getApplicantDet.FirstName;
                model.LastName = getApplicantDet.LastName;
                model.FeesPaidType = GetChargeType(getAppliTransDet.Charge_Type ?? 0);
                model.Type = "100";
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
            else if (chargetype == 4)
            {
                chargeType = "Credit Check Fee";
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
                var tenData = db.tbl_TenantOnline.Where(p => p.Email == appliData.Email && p.ProspectID == appliData.TenantID).FirstOrDefault();
               

                

                if (appliData != null)
                {
                    db.tbl_Applicant.Remove(appliData);
                    db.SaveChanges();
                }
                if (tenData != null)
                {
                    db.tbl_TenantOnline.Remove(tenData);
                    db.SaveChanges();
                }
                if (tenData != null)
                {
                    var logData = db.tbl_Login.Where(p => p.UserID == tenData.ParentTOID).FirstOrDefault();
                    if (logData != null)
                    {
                        db.tbl_Login.Remove(logData);
                        db.SaveChanges();
                    }
                }
                msg = "Applicant Removed Successfully";
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
                        MonthlyPayment = item.MonthlyPayment,
                        AdminFee = item.AdminFee,
                        AdminFeePercentage = item.AdminFeePercentage
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
                        getAppldata.AdminFee = item.AdminFee;
                        getAppldata.AdminFeePercentage = item.AdminFeePercentage;

                    }
                    db.SaveChanges();
                    msg = "Applicant Updated Successfully";
                }
            }

            var applyNow = db.tbl_ApplyNow.Where(p => p.ID == prospectID).FirstOrDefault();

            if (applyNow != null)
            {
                int stepcomp = 0;
                stepcomp = applyNow.StepCompleted ?? 0;
                if (stepcomp < stepcompModel)
                {
                    stepcomp = stepcompModel;
                    applyNow.StepCompleted = stepcomp;
                    db.SaveChanges();
                }
                var tenentUID = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
                var tenantData = db.tbl_TenantOnline.Where(p => p.ParentTOID == tenentUID).FirstOrDefault();
                if(tenantData!=null)
                {
                    tenantData.StepCompleted = stepcomp;
                    db.SaveChanges();
                }
            }

            db.Dispose();
            return msg;
        }
        public ApplicantModel GetGuarantorApplicantData(string Email, long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantModel model = new ApplicantModel();
            var getGuarantorData = db.tbl_Applicant.Where(p => p.TenantID == TenantID && p.Email == Email).FirstOrDefault();
            if (getGuarantorData != null)
            {
                model.ApplicantID = getGuarantorData.ApplicantID;
                model.FirstName = getGuarantorData.FirstName;
                model.LastName = getGuarantorData.LastName;
                model.Phone = !string.IsNullOrWhiteSpace(getGuarantorData.Phone) ? getGuarantorData.Phone : "";
                model.Email = !string.IsNullOrWhiteSpace(getGuarantorData.Email) ? getGuarantorData.Email : "";
                model.Type = getGuarantorData.Type;
                model.Gender = getGuarantorData.Gender;
                model.MoveInPercentage = getGuarantorData.MoveInPercentage != null ? getGuarantorData.MoveInPercentage : 0;
                model.MoveInCharge = getGuarantorData.MoveInCharge != null ? getGuarantorData.MoveInCharge : 0;
                model.MonthlyPercentage = getGuarantorData.MonthlyPercentage != null ? getGuarantorData.MonthlyPercentage : 0;
                model.MonthlyPayment = getGuarantorData.MonthlyPayment != null ? getGuarantorData.MonthlyPayment : 0;
                model.AdminFeePercentage = getGuarantorData.AdminFeePercentage != null ? getGuarantorData.AdminFeePercentage : 0;
                model.AdminFee = getGuarantorData.AdminFee != null ? getGuarantorData.AdminFee : 0;
                model.OtherGender = getGuarantorData.OtherGender != null ? OtherGender : "";

                model.RelationshipString = getGuarantorData.Relationship == null ? "" : getGuarantorData.Relationship == "1" ? "Family Member" : getGuarantorData.Relationship == "2" ? "Friend" : "";
                model.DateOfBirth = getGuarantorData.DateOfBirth;
                model.DateOfBirthTxt = getGuarantorData.DateOfBirth == null ? "" : getGuarantorData.DateOfBirth.Value.ToString("MM/dd/yyyy");
                model.GenderString = getGuarantorData.Gender == 1 ? "Male" : getGuarantorData.Gender == 2 ? "Female" : getGuarantorData.Gender == 3 ? "Other" : "";
                model.Paid = getGuarantorData.Paid == null ? 0 : getGuarantorData.Paid;
            }
            return model;
        }
        public ApplicantModel IsCreditPaidBackgroundPaid()
        {
            ApplicantModel model = new ApplicantModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var getCreditCheck = db.tbl_Applicant.Where(co => co.UserID == ShomaGroupWebSession.CurrentUser.UserID).FirstOrDefault();
            if (getCreditCheck != null)
            {
                model.CreditPaid = getCreditCheck.CreditPaid ?? 0;
                model.BackGroundPaid = getCreditCheck.BackGroundPaid ?? 0;
            }
            return model;
        }
        public string SaveUpdateApplicantGenerateQuotation(ApplicantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            var user = db.tbl_ApplyNow.Where(co => co.ID == model.TenantID).FirstOrDefault();
            long? userid = user == null ? 0 : user.UserId != null ? user.UserId : 0;
            var mainappdet = db.tbl_ApplyNow.Where(c => c.ID == model.TenantID).FirstOrDefault();
            var countCoApp = db.tbl_Applicant.Where(p => p.TenantID == model.TenantID && p.Type == "Co-Applicant").Count();
            var countMinor = db.tbl_Applicant.Where(p => p.TenantID == model.TenantID && p.Type == "Minor").Count();
            var countGuar = db.tbl_Applicant.Where(p => p.TenantID == model.TenantID && p.Type == "Guarantor").Count();
            var unitDet = db.tbl_PropertyUnits.Where(p => p.UID == mainappdet.PropertyId).FirstOrDefault();
            if (model.ApplicantID == 0)
            {
                if (model.Type != "Guarantor")
                {
                    if ((unitDet.Bedroom ?? 0) == 1)
                    {
                        if (countCoApp == 1 || countMinor == 1)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                    }
                    if ((unitDet.Bedroom ?? 0) == 2)
                    {
                        if (countCoApp + countMinor > 3)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                    }
                    if ((unitDet.Bedroom ?? 0) == 3)
                    {
                        if (countCoApp > 3 && countMinor > 3)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                        else if (countCoApp + countMinor > 5)
                        {
                            msg = "Occupancy is already filled. Can not add " + model.Type;
                            return msg;
                        }
                    }
                }
                else
                {
                    if (countGuar == 1)
                    {
                        msg = model.Type + " is already added.";
                        return msg;
                    }
                }
            }
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
                    Paid = 0,
                    CreditPaid = 0,
                    BackGroundPaid = 0,
                    AddedBy = userid
                };
                db.tbl_Applicant.Add(saveApplicant);
                db.SaveChanges();


                string pass = "";
                string _allowedChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*";

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
                    UserType = model.Type == "Guarantor" ? 34 : 33,
                    ParentUserID = mainappdet.UserId,

                };
                db.tbl_Login.Add(createCoApplLogin);
                db.SaveChanges();

                saveApplicant.UserID = createCoApplLogin.UserID;
                db.SaveChanges();

                if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                {
                    model.DateOfBirth = null;
                }
                else
                {
                    model.DateOfBirth = model.DateOfBirth;
                }
                var getAppldata = new tbl_TenantOnline()
                {
                    ProspectID = model.TenantID,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Email = model.Email,
                    Mobile = model.Phone,
                    PassportNumber = "",

                    IDType = model.IDType,
                    State = model.State,
                    IDNumber = model.IDNumber,
                    Country = model.Country,
                    HomeAddress1 = model.HomeAddress1,
                    HomeAddress2 = model.HomeAddress2,
                    StateHome = model.StateHome,
                    CityHome = model.CityHome,
                    ZipHome = model.ZipHome,
                    RentOwn = 0,
                    ////MoveInDate = model.MoveInDate,
                    JobType = 0,
                    OfficeCountry = "1",
                    OfficeState = 0,
                    EmergencyCountry = "1",
                    EmergencyStateHome = 0,
                    CreatedDate = DateTime.Now,
                    IsInternational = model.IsInternational,
                    OtherGender = model.OtherGender,
                    Country2 = "1",
                    StateHome2 = 0,
                    ZipHome2 = "",
                    RentOwn2 = 0,
                    SSN = model.SSN,
                    CountryOfOrigin = 1,
                    Evicted = 1,
                    ConvictedFelony = 1,
                    CriminalChargPen = 1,
                    DoYouSmoke = 1,
                    ReferredResident = 1,
                    ReferredBrokerMerchant = 1,
                    IsProprNoticeLeaseAgreement = 1,
                    StepCompleted = 4,
                    ParentTOID = createCoApplLogin.UserID,
                    IsRentalPolicy = 0,
                    IsRentalQualification = 0,
                };
                db.tbl_TenantOnline.Add(getAppldata);
                db.SaveChanges();
                if (model.Type == "Co-Applicant")
                {
                    if (model.Email != "")
                    {
                        string reportCoappHTML = "";
                        string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        //reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect5.html");
                        //reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);

                        reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect.html");
                        reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);
                        reportCoappHTML = reportCoappHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Co-applicant. Fill your Details.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                        //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                        reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", emailBody);


                        //reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", model.Type);
                        //reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added As Co-applicant. Fill your Details");
                        //reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                        //reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        //reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string coappbody = reportCoappHTML;
                        new EmailSendModel().SendEmail(model.Email, "Your Application Added as Co-applicant. Fill your Details", coappbody);

                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.Phone))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added As Co-applicant. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                            }
                        }
                    }
                }
                else if (model.Type == "Guarantor")
                {
                    if (model.Email != "")
                    {
                        string reportGurHTML = "";
                        string gurfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        //reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect5.html");
                        //reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);

                        reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect.html");
                        reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);
                        reportGurHTML = reportGurHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));


                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Guarantor. Fill your Details.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                        //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                        reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", emailBody);


                        //reportGurHTML = reportGurHTML.Replace("[%CoAppType%]", model.Type);
                        //reportGurHTML = reportGurHTML.Replace("[%EmailHeader%]", "Your Application Added As Guarantor. Fill your Details");
                        //reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", "Your are Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                        //reportGurHTML = reportGurHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        //reportGurHTML = reportGurHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string gurbody = reportGurHTML;
                        new EmailSendModel().SendEmail(model.Email, "Your Application Added As Guaranter. Fill your Details", gurbody);

                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.Phone))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added As Guarantor. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                            }
                        }
                    }
                }
                else
                {
                    saveApplicant.CreditPaid = 1;
                    saveApplicant.Paid = 1;
                    saveApplicant.BackGroundPaid = 1;
                    db.SaveChanges();
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

                msg = "Progress Saved.";
            }
            else
            {
                var getAppldata = db.tbl_Applicant.Where(p => p.ApplicantID == model.ApplicantID).FirstOrDefault();
                int loggedinuserid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
                if (getAppldata != null)
                {
                    var oldemail = getAppldata.Email;
                    getAppldata.FirstName = model.FirstName;
                    getAppldata.LastName = model.LastName;
                    getAppldata.Phone = model.Phone;
                    getAppldata.Email = model.Email;

                    var userdata = db.tbl_Login.Where(p => p.UserID == getAppldata.UserID).FirstOrDefault();

                    string pass = "";
                    try
                    {
                        pass = new EncryptDecrypt().DecryptText(userdata.Password);
                    }
                    catch
                    {
                        pass = "";
                    }

                    if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                    {
                        getAppldata.DateOfBirth = null;
                    }
                    else
                    {
                        getAppldata.DateOfBirth = model.DateOfBirth;
                    }

                    getAppldata.Gender = model.Gender;
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
                    //var getLoginDet = db.tbl_Login.Where(p => p.Email == getAppldata.Email).FirstOrDefault();
                    //if (getLoginDet != null)
                    //{
                    var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == userid).FirstOrDefault();
                    if (getTenantOnline != null)
                    {
                        getTenantOnline.FirstName = model.FirstName;
                        getTenantOnline.MiddleInitial = model.MiddleName;
                        getTenantOnline.LastName = model.LastName;
                        getTenantOnline.DateOfBirth = model.DateOfBirth;
                        getTenantOnline.Gender = model.Gender;
                        getTenantOnline.Mobile = model.Phone;
                        getTenantOnline.Email = model.Email;
                        getTenantOnline.SSN = model.SSN;
                        getTenantOnline.IDNumber = model.IDNumber;
                        getTenantOnline.IDType = model.IDType;
                        getTenantOnline.State = model.State;
                        getTenantOnline.StateHome = model.StateHome;
                        getTenantOnline.HomeAddress1 = model.HomeAddress1;
                        getTenantOnline.HomeAddress2 = model.HomeAddress2;
                        getTenantOnline.CityHome = model.CityHome;
                        getTenantOnline.ZipHome = model.ZipHome;
                        getTenantOnline.MiddleInitial = model.MiddleName;
                        getTenantOnline.Country = model.Country;
                        db.SaveChanges();
                    }
                    //}

                    db.SaveChanges();
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    //reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect5.html");
                    //reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);

                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));



                    string body = "";
                    if ((getAppldata.CreditPaid ?? 0) == 0 )
                    {

                        var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                        string payid = new EncryptDecrypt().EncryptText(getAppldata.ApplicantID.ToString() + ",4," + propertDet.AppCCCheckFees.Value.ToString("0.00"));

                        

                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Payment Link for Credit Check.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please pay your fees $" + propertDet.AppCCCheckFees.Value.ToString("0.00") + " for credit check to continue the Online Application Process.</p>";
                        //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);



                        //reportHTML = reportHTML.Replace("[%CoAppType%]", model.Type);
                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Payment Link for Credit Check");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", "Please pay your fees $" + propertDet.AppCCCheckFees.Value.ToString("0.00") + " for credit check to continue the Online Application Process.<br/><br/>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                        //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                        body = reportHTML;
                        new EmailSendModel().SendEmail(model.Email, "Payment Link for Credit Check", body);
                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.Phone))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Payment Link for Credit Check. Please check the email for detail.");
                            }
                        }
                    }
                    else if ((getAppldata.CreditPaid ?? 0) == 0 && oldemail != model.Email)
                    {
                        userdata.Email = model.Email;
                        userdata.Username = model.Email;
                        db.SaveChanges();

                        if (model.Type == "Co-Applicant")
                        {
                            if (model.Email != "")
                            {
                                string reportCoappHTML = "";
                                string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                                reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect5.html");
                                reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);

                                reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect.html");
                                reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);
                                reportCoappHTML = reportCoappHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                                string emailBody = "";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Co-applicant. Fill your Details.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                                //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                                emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                                reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", emailBody);

                                //reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", model.Type);
                                //reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added As Co-applicant. Fill your Details");
                                //reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                                //reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                                //reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                                string coappbody = reportCoappHTML;
                                new EmailSendModel().SendEmail(model.Email, "Your Application Added as Co-applicant. Fill your Details", coappbody);

                                if (SendMessage == "yes")
                                {
                                    if (!string.IsNullOrWhiteSpace(model.Phone))
                                    {
                                        new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added As Co-applicant. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                                    }
                                }
                            }
                        }
                        if (model.Type == "Guarantor")
                        {
                            if (model.Email != "")
                            {
                                string reportGurHTML = "";
                                string gurfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                                //reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect5.html");
                                //reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);

                                reportGurHTML = System.IO.File.ReadAllText(gurfilePath + "EmailTemplateProspect.html");
                                reportGurHTML = reportGurHTML.Replace("[%ServerURL%]", serverURL);
                                reportGurHTML = reportGurHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));


                                string emailBody = "";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Hello! Your Application Added As Guarantor. Fill your Details.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Your Online Application Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass + "</p>";
                                //emailBody += "<p style=\"margin-bottom: 0px;\"><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--></p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                                emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                                reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", emailBody);

                                //reportGurHTML = reportGurHTML.Replace("[%CoAppType%]", model.Type);
                                //reportGurHTML = reportGurHTML.Replace("[%EmailHeader%]", "Your Application Added As Guarantor. Fill your Details");
                                //reportGurHTML = reportGurHTML.Replace("[%EmailBody%]", "Your are Added by " + mainappdet.FirstName + " " + mainappdet.LastName + " for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + pass);
                                //reportGurHTML = reportGurHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                                //reportGurHTML = reportGurHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                                string gurbody = reportGurHTML;
                                new EmailSendModel().SendEmail(model.Email, "Your Application Added As Guaranter. Fill your Details", gurbody);

                                if (SendMessage == "yes")
                                {
                                    if (!string.IsNullOrWhiteSpace(model.Phone))
                                    {
                                        new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added As Guarantor. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                                    }
                                }
                            }
                        }
                    }

                }
                msg = "Progress Saved.";
            }

            db.Dispose();
            return msg;
        }

        public ApplicantModel GetApplicantDetailsGenerateQuotation(int id, int chargetype)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantModel model = new ApplicantModel();
            var userApp = db.tbl_Applicant.Where(co => co.ApplicantID == id).FirstOrDefault();
            if (userApp != null)
            {
                var user = db.tbl_ApplyNow.Where(co => co.ID == userApp.TenantID).FirstOrDefault();

                var ptotid = user == null ? 0 : user.UserId != null ? user.UserId : 0;
                string bat = "";
                if (chargetype == 1)
                {
                    bat = id.ToString();
                    var coappliList = db.tbl_Applicant.Where(pp => pp.ApplicantID == id).FirstOrDefault();
                    if (coappliList != null)
                    {
                        coappliList.Paid = 1;
                        db.SaveChanges();
                    }
                }
                else
                {
                    bat = "1";
                }
                if (chargetype == 4)
                {
                    bat = "4";
                }
                if (chargetype == 5)
                {
                    bat = "5";
                }
                //var getApplicantDet = db.tbl_Applicant.Where(p => p.ApplicantID == id).FirstOrDefault();
                //var getTenantDet = db.tbl_ApplyNow.Where(p => p.ID == getApplicantDet.TenantID).FirstOrDefault();
                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ApplicantID == id).FirstOrDefault();
                //var GetTenantData = db.tbl_TenantOnline.Where(P => P.ParentTOID == getApplicantDet.UserID).FirstOrDefault();

                string transType = "0";
                if (GetPayDetails != null)
                {
                    transType = GetPayDetails.ID.ToString();
                }
                var getApplicantDet = db.tbl_Applicant.Where(p => p.ApplicantID == id).FirstOrDefault();
                var getTenantDet = db.tbl_ApplyNow.Where(p => p.ID == getApplicantDet.TenantID).FirstOrDefault();
                var getAppliTransDet = db.tbl_Transaction.Where(p => p.TenantID == getTenantDet.UserId && p.PAID == transType).FirstOrDefault();
                if (ptotid == 0)
                {
                    ptotid = getApplicantDet.UserID ?? 0;
                }
                //var getAppliTransDet = db.tbl_Transaction.Where(p => p.TenantID == getTenantDet.UserId && p.Batch == id.ToString() && p.Charge_Type == chargetype).FirstOrDefault();
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

                    model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy") != "01/01/0001" ? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
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
                    model.AdminFee = getApplicantDet.AdminFee;
                    model.AdminFeePercentage = getApplicantDet.AdminFeePercentage;
                    model.OtherGender = getApplicantDet.OtherGender;
                    model.TenantID = getApplicantDet.TenantID;
                    model.ApplicantAddedBy = getApplicantDet.AddedBy;
                    model.ApplicantUserId = getApplicantDet.UserID;
                    //New
                    var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ptotid).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(getTenantOnline.IDNumber))
                    {
                        try
                        {
                            model.IDNumberEnc = getTenantOnline.IDNumber;
                            string decryptedIDNumber = new EncryptDecrypt().DecryptText(getTenantOnline.IDNumber);
                            int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                            string maskidnumber = "";
                            for (int i = 0; i < idnumlength; i++)
                            {
                                maskidnumber += "*";
                            }
                            if (decryptedIDNumber.Length > 4)
                            {
                                model.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                            }
                            else
                            {
                                model.IDNumber = decryptedIDNumber;
                            }
                        }
                        catch
                        {
                            model.IDNumberEnc = "";
                            model.IDNumber = "";
                        }

                    }
                    else
                    {
                        model.IDNumberEnc = "";
                        model.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(getTenantOnline.SSN))
                    {
                        try
                        {
                            model.SSNEnc = getTenantOnline.SSN;
                            string decryptedSSN = new EncryptDecrypt().DecryptText(getTenantOnline.SSN);
                            if (decryptedSSN.Length > 5)
                            {
                                model.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 5, 4);
                            }
                            else
                            {
                                model.SSN = decryptedSSN;
                            }
                        }
                        catch
                        {
                            model.SSNEnc = "";
                            model.SSN = "";
                        }

                    }
                    else
                    {
                        model.SSNEnc = "";
                        model.SSN = ""; ;
                    }

                    model.IDType = getTenantOnline.IDType;
                    model.State = getTenantOnline.State;
                    model.MiddleName = getTenantOnline.MiddleInitial;
                    model.Country = !string.IsNullOrWhiteSpace(getTenantOnline.Country) ? getTenantOnline.Country : "1";
                    model.StateHome = getTenantOnline.StateHome ?? 0;
                    model.HomeAddress1 = getTenantOnline.HomeAddress1;
                    model.HomeAddress2 = getTenantOnline.HomeAddress2;
                    model.CityHome = getTenantOnline.CityHome;
                    model.ZipHome = getTenantOnline.ZipHome;
                    model.MiddleName = getTenantOnline.MiddleInitial;
                }
                else if (getAppliTransDet != null && (chargetype == 4 || chargetype == 5))
                {
                    DateTime? dobDateTime = null;
                    try
                    {
                        dobDateTime = Convert.ToDateTime(getApplicantDet.DateOfBirth);
                    }
                    catch
                    {
                    }

                    model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy") != "01/01/0001" ? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
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
                    model.AdminFee = getApplicantDet.AdminFee;
                    model.AdminFeePercentage = getApplicantDet.AdminFeePercentage;
                    model.OtherGender = getApplicantDet.OtherGender;
                    model.TenantID = getApplicantDet.TenantID;
                    model.ApplicantAddedBy = getApplicantDet.AddedBy;
                    model.ApplicantUserId = getApplicantDet.UserID;
                    //New
                    var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ptotid).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(getTenantOnline.IDNumber))
                    {
                        try
                        {
                            model.IDNumberEnc = getTenantOnline.IDNumber;
                            string decryptedIDNumber = new EncryptDecrypt().DecryptText(getTenantOnline.IDNumber);
                            int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                            string maskidnumber = "";
                            for (int i = 0; i < idnumlength; i++)
                            {
                                maskidnumber += "*";
                            }
                            if (decryptedIDNumber.Length > 4)
                            {
                                model.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                            }
                            else
                            {
                                model.IDNumber = decryptedIDNumber;
                            }
                        }
                        catch
                        {
                            model.IDNumberEnc = "";
                            model.IDNumber = "";
                        }

                    }
                    else
                    {
                        model.IDNumberEnc = "";
                        model.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(getTenantOnline.SSN))
                    {
                        try
                        {
                            model.SSNEnc = getTenantOnline.SSN;
                            string decryptedSSN = new EncryptDecrypt().DecryptText(getTenantOnline.SSN);
                            if (decryptedSSN.Length > 5)
                            {
                                model.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 5, 4);
                            }
                            else
                            {
                                model.SSN = decryptedSSN;
                            }
                        }
                        catch
                        {
                            model.SSNEnc = "";
                            model.SSN = "";
                        }

                    }
                    else
                    {
                        model.SSNEnc = "";
                        model.SSN = ""; ;
                    }

                    model.IDType = getTenantOnline.IDType;
                    model.State = getTenantOnline.State;
                    model.MiddleName = getTenantOnline.MiddleInitial;
                    model.Country = !string.IsNullOrWhiteSpace(getTenantOnline.Country) ? getTenantOnline.Country : "1";
                    model.StateHome = getTenantOnline.StateHome ?? 0;
                    model.HomeAddress1 = getTenantOnline.HomeAddress1;
                    model.HomeAddress2 = getTenantOnline.HomeAddress2;
                    model.CityHome = getTenantOnline.CityHome;
                    model.ZipHome = getTenantOnline.ZipHome;
                    model.MiddleName = getTenantOnline.MiddleInitial;
                }
                else if (getAppliTransDet != null && chargetype == 0)
                {

                    DateTime? dobDateTime = null;
                    try
                    {
                        dobDateTime = Convert.ToDateTime(getApplicantDet.DateOfBirth);
                    }
                    catch
                    {
                    }

                    model.DateOfBirthTxt = dobDateTime == null ? "" : (dobDateTime.Value.ToString("MM/dd/yyyy") != "01/01/0001" ? dobDateTime.Value.ToString("MM/dd/yyyy") : "");
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
                    model.AdminFee = getApplicantDet.AdminFee;
                    model.AdminFeePercentage = getApplicantDet.AdminFeePercentage;
                    model.OtherGender = getApplicantDet.OtherGender;
                    model.TenantID = getApplicantDet.TenantID;
                    model.ApplicantAddedBy = getApplicantDet.AddedBy;
                    model.ApplicantUserId = getApplicantDet.UserID;
                    //New

                    var getTenantOnline = db.tbl_TenantOnline.Where(p => p.ParentTOID == ptotid).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(getTenantOnline.IDNumber))
                    {
                        try
                        {
                            model.IDNumberEnc = getTenantOnline.IDNumber;
                            string decryptedIDNumber = new EncryptDecrypt().DecryptText(getTenantOnline.IDNumber);
                            int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                            string maskidnumber = "";
                            for (int i = 0; i < idnumlength; i++)
                            {
                                maskidnumber += "*";
                            }
                            if (decryptedIDNumber.Length > 4)
                            {
                                model.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                            }
                            else
                            {
                                model.IDNumber = decryptedIDNumber;
                            }
                        }
                        catch
                        {
                            model.IDNumberEnc = "";
                            model.IDNumber = "";
                        }

                    }
                    else
                    {
                        model.IDNumberEnc = "";
                        model.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(getTenantOnline.SSN))
                    {
                        try
                        {
                            model.SSNEnc = getTenantOnline.SSN;
                            string decryptedSSN = new EncryptDecrypt().DecryptText(getTenantOnline.SSN);
                            if (decryptedSSN.Length > 5)
                            {
                                model.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 5, 4);
                            }
                            else
                            {
                                model.SSN = decryptedSSN;
                            }
                        }
                        catch
                        {
                            model.SSNEnc = "";
                            model.SSN = "";
                        }

                    }
                    else
                    {
                        model.SSNEnc = "";
                        model.SSN = ""; ;
                    }

                    model.IDType = getTenantOnline.IDType;
                    model.State = getTenantOnline.State;
                    model.MiddleName = getTenantOnline.MiddleInitial;
                    model.Country = getTenantOnline.Country;
                    model.StateHome = getTenantOnline.StateHome;
                    model.HomeAddress1 = getTenantOnline.HomeAddress1;
                    model.HomeAddress2 = getTenantOnline.HomeAddress2;
                    model.CityHome = getTenantOnline.CityHome;
                    model.ZipHome = getTenantOnline.ZipHome;
                    model.MiddleName = getTenantOnline.MiddleInitial;

                }
                else
                {
                    model.FirstName = getApplicantDet.FirstName;
                    model.LastName = getApplicantDet.LastName;
                    model.FeesPaidType = GetChargeType(getAppliTransDet.Charge_Type ?? 0);
                    model.Type = "100";
                }
            }
            return model;
        }
    }
}