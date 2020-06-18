using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Models.TwilioApi;
using System.Web.Configuration;
using ShomaRM.Controllers;
using System.Web.Mvc;
using iText;
using iText.Html2pdf;
using iText.Layout.Element;
using iText.Kernel.Pdf;
using iText.Layout;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShomaRM.Models
{
    public class ApplyNowModel
    {
        public int ID { get; set; }
        public Nullable<long> PID { get; set; }
        public string Name_On_Card { get; set; }
        public string CardNumber { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CCVNumber { get; set; }
        public Nullable<long> ProspectId { get; set; }
        public Nullable<decimal> Charge_Amount { get; set; }
        public Nullable<decimal> Credit_Amount { get; set; }

        public int Charge_Type { get; set; }
        public string Transaction_Type { get; set; }
        public string Description { get; set; }
        public string GL_Trans_Description { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public int PaymentMethod { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public string TotalAmountString { get; set; }
        public string ConvergentAmountString { get; set; }
        public string ParkingAmountString { get; set; }
        public string FOBAmountString { get; set; }
        public string PetPlaceAmountString { get; set; }
        public string StorageAmountString { get; set; }
        public string Rent { set; get; }
        public Nullable<long> AID { get; set; }
        public Nullable<decimal> MoveInPercentage { get; set; }
        public Nullable<decimal> ProcessingFees { get; set; }
        public int AcceptSummary { get; set; }
        public int FromAcc { get; set; }
        public int PAID { get; set; }
        public int IsSaveAcc { get; set; }
        // Sachin M added 28 Apr
        public List<ApplicantModel> lstApp { get; set; }
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];

        public string SavePaymentDetails(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.PID != 0)
            {
                var GetApplicantData = db.tbl_Applicant.Where(p => p.UserID == userid).FirstOrDefault();
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
             
                var GetPropertyDetails = db.tbl_Properties.Where(P => P.PID == 8).FirstOrDefault();
                
                decimal processingFees = 0;

                if(GetPropertyDetails!=null)
                {
                    processingFees = GetPropertyDetails.ProcessingFees ?? 0;
                }

                string encrytpedCardNumber = new EncryptDecrypt().EncryptText(model.CardNumber);
                string encrytpedCardMonth = new EncryptDecrypt().EncryptText(model.CardMonth);
                string encrytpedCardYear = new EncryptDecrypt().EncryptText(model.CardYear);
                string encrytpedRoutingNumber = new EncryptDecrypt().EncryptText(model.CCVNumber);

                string decryptedPayemntCardNumber = new EncryptDecrypt().DecryptText(encrytpedCardNumber);

                string transStatus = "";
                string cardaccnum = "";
                List<string> filePaths = new List<string>();
                string emailBody = "";
                string sub = "";
                int chargeType = 4;
                model.Email = GetProspectData.Email;
                model.ProcessingFees = processingFees;
                if (model.PaymentMethod == 2)
                {
                    cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (model.PaymentMethod == 1)
                {
                    cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeACH(model);
                }

                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    if (model.lstApp != null)
                    {
                        foreach (var coapp in model.lstApp)
                        { //Added by Sachin M 28 Apr 7:26PM
                            var coappliList = db.tbl_Applicant.Where(pp => pp.ApplicantID == coapp.ApplicantID).FirstOrDefault();
                            if (coappliList != null)
                            {
                                if(coapp.Type=="4")
                                {
                                    chargeType = 4;
                                    coappliList.CreditPaid = 1;
                                }
                                if(coapp.Type=="5")
                                {
                                    chargeType = 5;
                                    coappliList.BackGroundPaid = 1;
                                }
                                if ((coappliList.CreditPaid ?? 0) == 1 && (coappliList.BackGroundPaid ?? 0) == 1)
                                {
                                    coappliList.Paid = 1;
                                }
                                db.SaveChanges();
                            }
                        }
                    }
                    long opid = 0;
                    var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ApplicantID == GetApplicantData.ApplicantID && P.CardNumber == encrytpedCardNumber && P.CardMonth == encrytpedCardMonth && P.CardYear == encrytpedCardYear).FirstOrDefault();

                    if (GetPayDetails == null)
                    {
                        var savePaymentDetails = new tbl_OnlinePayment()
                        {
                            PID = model.PID,
                            Name_On_Card = model.Name_On_Card,
                            CardNumber = !string.IsNullOrWhiteSpace(model.CardNumber) ? new EncryptDecrypt().EncryptText(model.CardNumber) : "",
                            CardMonth = !string.IsNullOrWhiteSpace(model.CardMonth) ? new EncryptDecrypt().EncryptText(model.CardMonth) : "",
                            CardYear = !string.IsNullOrWhiteSpace(model.CardYear) ? new EncryptDecrypt().EncryptText(model.CardYear) : "",
                            CCVNumber = !string.IsNullOrWhiteSpace(model.RoutingNumber) ? new EncryptDecrypt().EncryptText(model.RoutingNumber) : "",
                            ProspectId = model.ProspectId,
                            PaymentMethod = model.PaymentMethod,
                            ApplicantID = GetApplicantData.ApplicantID,
                        };
                        db.tbl_OnlinePayment.Add(savePaymentDetails);
                        db.SaveChanges();
                        opid = savePaymentDetails.ID;
                    }
                    else
                    {
                        opid = GetPayDetails.ID;
                    }
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = userid,                       
                        PAID = opid.ToString(),
                        Transaction_Date = DateTime.Now,                                              
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[2],
                        Charge_Date = DateTime.Now,
                        Charge_Type = chargeType,
                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,
                        Batch = "1",                    
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        UserID=userid,
                        RefNum= strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();

                    var TransId = saveTransaction.TransID;
                    MyTransactionModel mm = new MyTransactionModel();
                    mm.CreateTransBill(TransId, Convert.ToDecimal(model.Charge_Amount), model.Description);

                    GetProspectData.IsApplyNow = 2;
                    GetProspectData.AcceptSummary = 1;
                    db.SaveChanges();

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    string message = "";
                    string phonenumber = GetProspectData.Phone;
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    if (model != null)
                    {
                        if (chargeType == 4)
                        {
                            sub = "Payment Confirmation, Application agreement and Rental Qualifications";
                            emailBody += "<p style=\"margin-bottom: 0px;\">It’s all happening, " + GetProspectData.FirstName + " " + GetProspectData.LastName + "! You’ve submitted the required information for your credit check and accepted the “Application Agreement & Rental Qualifications” document in the prospect portal. We’ve attached documents for your review. Please contact us with any questions.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Application fees are non refundable, even if the application is denied except to the extent otherwise required by applicable law</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                            string rulepolicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rules_Policies_fragmented.pdf"); ;
                            string rentalqualification = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rental_qualifications_fragmented.pdf"); ;
                            
                            filePaths.Add(rulepolicy);
                            filePaths.Add(rentalqualification);
                            message = "Payment Confirmation, Application agreement and Rental Qualifications";
                        }
                        if (chargeType == 5)
                        {
                            sub = "Your Sanctuary Rental Application and Rules and Policies";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetProspectData.FirstName + " " + GetProspectData.LastName + "<br/>Thank you for submitting your application to sanctuary Doral.We are excited that you are interested in joining our community.This email confirms we have received your online application fees payment, please save this email for your personal records.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Application fees are non refundable, even if the application is denied except to the extent otherwise required by applicable law</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us at <a style=\"text-decoration:none;\" href=\"tel:+1-786-684-7634\">(786) 648-7634</a></p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login to edit application</p>";
                            emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "/Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                            string rulepolicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rules_Policies_fragmented.pdf"); ;
                            string rentalqualification = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rental_qualifications_fragmented.pdf"); ;
                            string appSummary = new ApplyNowModel().PrintApplicationForm(GetProspectData.ID);

                            filePaths.Add(appSummary);
                            filePaths.Add(rulepolicy);
                            filePaths.Add(rentalqualification);

                            message = "Your Sanctuary Rental Application and Rules and Policies. Please check the email for detail.";
                        }
                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Payment Received");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", GetProspectData.FirstName + " " + GetProspectData.LastName);
                        //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetProspectData.Email);

                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmailWithAttachment(GetProspectData.Email, sub, body, filePaths);

                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
                    }
                    msg = "1";
                }
                else
                {
                    msg = "0";
                }
            }

            db.Dispose();
            return msg;
        }
        public string SaveCoGuPaymentDetails(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.PID != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
              
                var GetPropertyDetails = db.tbl_Properties.Where(P => P.PID == 8).FirstOrDefault();

                decimal processingFees = 0;

                if (GetPropertyDetails != null)
                {
                    processingFees = GetPropertyDetails.ProcessingFees ?? 0;
                }

                string encrytpedCardNumber = new EncryptDecrypt().EncryptText(model.CardNumber);
                string encrytpedCardMonth = new EncryptDecrypt().EncryptText(model.CardMonth);
                string encrytpedCardYear = new EncryptDecrypt().EncryptText(model.CardYear);
                string encrytpedRoutingNumber = new EncryptDecrypt().EncryptText(model.CCVNumber);
                string decryptedPayemntCardNumber = new EncryptDecrypt().DecryptText(encrytpedCardNumber);

                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ApplicantID == model.AID && P.CardNumber== encrytpedCardNumber && P.CardMonth==encrytpedCardMonth && P.CardYear==encrytpedCardYear).FirstOrDefault();

                string transStatus = "";
                string cardaccnum = "";
                model.Email = GetProspectData.Email;
                model.ProcessingFees = processingFees;
                if (model.PaymentMethod == 2)
                {
                    cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (model.PaymentMethod == 1)
                {
                    cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeACH(model);
                }

                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    //Added by Sachin M 29 Apr 5:16PM
                    string opid = "1";
                    if (GetPayDetails == null)
                    {
                        //updated by Sachin M 18 May
                        var savePaymentDetails = new tbl_OnlinePayment()
                        {
                            PID = model.PID,
                            Name_On_Card = model.Name_On_Card,
                            CardNumber = !string.IsNullOrWhiteSpace(model.CardNumber) ? new EncryptDecrypt().EncryptText(model.CardNumber) : "",
                            CardMonth = !string.IsNullOrWhiteSpace(model.CardMonth) ? new EncryptDecrypt().EncryptText(model.CardMonth) : "",
                            CardYear = !string.IsNullOrWhiteSpace(model.CardYear) ? new EncryptDecrypt().EncryptText(model.CardYear) : "",
                            CCVNumber = !string.IsNullOrWhiteSpace(model.CCVNumber) ? new EncryptDecrypt().EncryptText(model.CCVNumber) : "",
                            ProspectId = model.ProspectId,
                            PaymentMethod = model.PaymentMethod,
                            ApplicantID = model.AID,
                        };
                        db.tbl_OnlinePayment.Add(savePaymentDetails);
                        db.SaveChanges();
                        opid = savePaymentDetails.ID.ToString();
                    }
                    
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = userid,
                         PAID=opid,
                        Transaction_Date = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[2],
                        Charge_Date = DateTime.Now,
                        Charge_Type = 1,
                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,
                        Batch = "1",
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        UserID = userid,
                        RefNum = strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();

                    var TransId = saveTransaction.TransID;
                    MyTransactionModel mm = new MyTransactionModel();
                    mm.CreateTransBill(TransId, Convert.ToDecimal(model.Charge_Amount), model.Description);

                    var coappliList = db.tbl_Applicant.Where(pp => pp.ApplicantID == model.AID).FirstOrDefault();
                    if (coappliList != null)
                    {
                        coappliList.Paid = 1;
                        db.SaveChanges();
                    }
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    string message = "";
                    string phonenumber = coappliList.Phone;
                    if (model != null)
                    {
                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + coappliList.FirstName + " " + coappliList.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Payment Received");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", coappliList.FirstName + " " + coappliList.LastName);
                        //reportHTML = reportHTML.Replace("[%TenantEmail%]", coappliList.Email);
                    }
                    string body = reportHTML;

                    new EmailSendModel().SendEmail(coappliList.Email, "Sanctuary Payment Confirmation", body);
                    message = "Sanctuary Payment Confirmation. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
                    }
                    msg = "1";
                }
                else
                {
                    msg = "0";
                }

            }

            db.Dispose();
            return msg;
        }

        public async Task<string> saveCoAppPayment(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.ProspectId != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                // var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId).FirstOrDefault();
                var GetCoappDet = db.tbl_Applicant.Where(c => c.ApplicantID == model.AID).FirstOrDefault();
                var GePropertyData = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                var UserData = db.tbl_Login.Where(p => p.UserID == GetCoappDet.UserID).FirstOrDefault();

                string encrytpedCardNumber = new EncryptDecrypt().EncryptText(model.CardNumber);
                string encrytpedCardMonth = new EncryptDecrypt().EncryptText(model.CardMonth);
                string encrytpedCardYear = new EncryptDecrypt().EncryptText(model.CardYear);
                string encrytpedRoutingNumber = new EncryptDecrypt().EncryptText(model.CCVNumber);

                string decryptedPayemntCardNumber = new EncryptDecrypt().DecryptText(encrytpedCardNumber);

                decimal processingFees = 0;

                if (GePropertyData != null)
                {
                    processingFees = GePropertyData.ProcessingFees ?? 0;
                }

                string transStatus = "";
                string cardaccnum = "";

                model.Email = GetCoappDet.Email;
                model.ProcessingFees = processingFees;
                if (model.PaymentMethod == 2)
                {
                    cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (model.PaymentMethod == 1)
                {
                    model.AccountNumber = model.CardNumber;
                    cardaccnum = model.AccountNumber.Substring(model.AccountNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeACH(model);
                }

                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                string bat = "";

                if (strlist[1] != "000000")
                {
                    if (model.FromAcc == 1)
                    {
                        bat = model.AID.ToString();
                        //Added by Sachin M 28 Apr 8:28PM
                        var coappliList = db.tbl_Applicant.Where(pp => pp.ApplicantID == model.AID).FirstOrDefault();
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
                    if (model.FromAcc == 4)
                    {
                        bat = "4";
                    }
                    if (model.FromAcc == 5)
                    {
                        bat = "5";
                    }
                    long paid = 0;

                    if (model.IsSaveAcc == 1)
                    {
                        var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ApplicantID == model.AID && P.CardNumber == encrytpedCardNumber && P.CardMonth == encrytpedCardMonth && P.CardYear == encrytpedCardYear).FirstOrDefault();

                        if (GetPayDetails == null)
                        {
                            var savePaymentDetails = new tbl_OnlinePayment()
                            {
                                PID = model.PID,
                                Name_On_Card = model.Name_On_Card,
                                CardNumber = !string.IsNullOrWhiteSpace(model.CardNumber) ? new EncryptDecrypt().EncryptText(model.CardNumber) : "",
                                CardMonth = !string.IsNullOrWhiteSpace(model.CardMonth) ? new EncryptDecrypt().EncryptText(model.CardMonth) : "",
                                CardYear = !string.IsNullOrWhiteSpace(model.CardYear) ? new EncryptDecrypt().EncryptText(model.CardYear) : "",
                                CCVNumber = !string.IsNullOrWhiteSpace(model.RoutingNumber) ? new EncryptDecrypt().EncryptText(model.RoutingNumber) : "",
                                ProspectId = model.ProspectId,
                                PaymentMethod = model.PaymentMethod,
                                ApplicantID = model.AID,
                            };
                            db.tbl_OnlinePayment.Add(savePaymentDetails);
                            db.SaveChanges();
                            paid = savePaymentDetails.ID;
                        }
                        else
                        {
                            paid = GetPayDetails.ID;
                        }
                    }
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = Convert.ToInt64(GetProspectData.UserId),
                        PAID = paid.ToString(),
                        Transaction_Date = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[2],
                        Charge_Date = DateTime.Now,
                        Charge_Type =Convert.ToInt32(bat),
                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,
                        Batch = bat,
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        UserID = GetCoappDet.UserID,
                        RefNum= strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();

                    var TransId = saveTransaction.TransID;
                    MyTransactionModel mm = new MyTransactionModel();
                    mm.CreateTransBill(TransId, Convert.ToDecimal(model.Charge_Amount), model.Description);

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    string message = "";
                    string phonenumber = GetCoappDet.Phone;
                    string sub = "";
                    if (model != null)
                    {
                        if (model.FromAcc == 1 || model.FromAcc == 2)
                        {
                            GetProspectData.IsApplyNow = 2;
                            db.SaveChanges();

                            //sub = "Online Application Fees Payment Received";
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Online Application Fees Payment Received");
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                            //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);

                            sub = "Sanctuary Payment Confirmation";
                            
                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear: "+ GetCoappDet.FirstName + " " + GetCoappDet.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0)+processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                            string body = reportHTML;

                            //sachin m 01 may 2020
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Sanctuary Payment Confirmation. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }
                        }
                        else if (model.FromAcc == 4)
                        {
                            //sachin m 01 may 2020
                            // Credit Check Fees Paid //

                            //sub = "Credit Check Fees Payment Received";
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Credit Check Fees Payment Received");
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your Credit Check fees payment.  Please save this email for your personal records.  Your application is being processed for background check, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                            //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            //string body = reportHTML;
                            //new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            //message = "Credit Check Fees Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";
                            //if (SendMessage == "yes")
                            //{
                            //    if (!string.IsNullOrWhiteSpace(phonenumber))
                            //    {
                            //        new TwilioService().SMS(phonenumber, message);
                            //    }
                            //}
                            sub = "Payment Confirmation, Application agreement and Rental Qualifications";
                            
                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">It’s all happening, "+ GetCoappDet.FirstName + " " + GetCoappDet.LastName+"! You’ve submitted the required information for your credit check and accepted the “Application Agreement & Rental Qualifications” document in the prospect portal. We’ve attached documents for your review. Please contact us with any questions.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount??0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$"+ processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Application fees are non refundable, even if the application is denied except to the extent otherwise required by applicable law</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                            string body = reportHTML;

                            string rulepolicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rules_Policies_fragmented.pdf"); ;
                            string rentalqualification = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rental_qualifications_fragmented.pdf"); ;

                            List<string> filePaths = new List<string>();
                            filePaths.Add(rulepolicy);
                            filePaths.Add(rentalqualification);

                            new EmailSendModel().SendEmailWithAttachment(GetCoappDet.Email, sub, body, filePaths);
                            message = "Payment Confirmation, Application agreement and Rental Qualifications";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }

                            string pass = "";
                            pass = new EncryptDecrypt().DecryptText(UserData.Password);


                            // Credit Check Background Check //
                            
                            //var acutraqrequest = new AcutraqRequest();
                            //TenantOnlineModel modelTD = new TenantOnlineModel().GetTenantOnlineList((int)GetProspectData.ID, GetCoappDet.UserID ?? 0);
                            //string result = await acutraqrequest.PostAqutraqTenant(modelTD);

                            //if (result == "1")
                            //{
                            //var gerResultData = db.tbl_BackgroundScreening.Where(p => p.TenantId == model.ProspectId && p.Type == "TENTCREDIT").FirstOrDefault();

                            GetCoappDet.CreditPaid = 1;
                            db.SaveChanges();

                            //string reportHTMLbc = "";
                            //reportHTMLbc = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect5.html");
                            //reportHTMLbc = reportHTMLbc.Replace("[%ServerURL%]", serverURL);
                            //sub = "Credit Check Approved and Complete Online Application";
                            //reportHTMLbc = reportHTMLbc.Replace("[%EmailHeader%]", "Credit Check Approved and Complete Online Application");
                            //reportHTMLbc = reportHTMLbc.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Congratulation! Your application for credit check is approved. Please complete your Online Application by clicking below link</p>");
                            //reportHTMLbc = reportHTMLbc.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTMLbc = reportHTMLbc.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            //reportHTMLbc = reportHTMLbc.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                            //string bodybc = reportHTMLbc;
                            //new EmailSendModel().SendEmail(GetCoappDet.Email, "Credit Check Approved", bodybc);
                            //message = "Credit Check Approved. Please check the email for detail.";
                            //if (SendMessage == "yes")
                            //{
                            //    if (!string.IsNullOrWhiteSpace(phonenumber))
                            //    {
                            //        new TwilioService().SMS(phonenumber, message);
                            //    }
                            //}
                            ////}
                        }
                        else if (model.FromAcc == 5)
                        {
                            GetCoappDet.BackGroundPaid = 1;
                            GetCoappDet.Paid = 1;
                            db.SaveChanges();

                            filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                            reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                            message = "";
                            phonenumber = GetCoappDet.Phone;
                            if (model != null)
                            {
                                //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Background Check Payment Received");
                                //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                                //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);

                                //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);

                                string emailBody = "";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Dear "+ GetCoappDet.FirstName + " " + GetCoappDet.LastName + "<br/>Thank you for submitting your application to sanctuary Doral.We are excited that you are interested in joining our community.This email confirms we have received your online application fees payment, please save this email for your personal records.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Application fees are non refundable, even if the application is denied except to the extent otherwise required by applicable law</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us at <a style=\"text-decoration:none;\" href=\"tel:+1-786-684-7634\">(786) 648-7634</a></p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login to edit application</p>";
                                emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "/Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                                reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                            }
                            string body = reportHTML;

                            string rulepolicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rules_Policies_fragmented.pdf");
                            string rentalqualification = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rental_qualifications_fragmented.pdf");
                            string appSummary = new ApplyNowModel().PrintApplicationForm(GetProspectData.ID);

                            List<string> filePaths = new List<string>();
                            filePaths.Add(appSummary);
                            filePaths.Add(rulepolicy);
                            filePaths.Add(rentalqualification);

                            new EmailSendModel().SendEmailWithAttachment(GetCoappDet.Email, "Your Sanctuary Rental Application and Rules and Policies", body, filePaths);
                            message = "Your Sanctuary Rental Application and Rules and Policies. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }
                        }
                        else
                        {
                            GetProspectData.IsApplyNow = 2;
                            db.SaveChanges();

                            //sub = "Administration Fees Payment Received";
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Administration Fees Payment Received");
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p>");
                            //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            ////sachin m 01 may 2020
                            //string body = reportHTML;
                            //new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            //message = "Administration Fees Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";

                            sub = "Sanctuary Payment Confirmation";

                            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + GetCoappDet.FirstName + " " + GetCoappDet.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                            string body = reportHTML;

                            //sachin m 01 may 2020
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Sanctuary Payment Confirmation. Please check the email for detail.";

                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }
                            var currentUser = new CurrentUser();
                            currentUser.UserID = Convert.ToInt32(GetProspectData.UserId);

                            (new ShomaGroupWebSession()).SetWebSession(currentUser);
                        }
                    }

                    msg = "1";
                }
                else
                {
                    msg = "0";
                }

            }

            db.Dispose();
            return msg;
        }

        // Sachin M 09 june 2020
        public async Task<string> saveListPayment(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.ProspectId != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                var GetCoappDet = db.tbl_Applicant.Where(c => c.ApplicantID == model.AID).FirstOrDefault();
                var GePropertyData = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                var UserData = db.tbl_Login.Where(p => p.UserID == GetCoappDet.UserID).FirstOrDefault();

                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ID == model.PAID).FirstOrDefault();

                string decrytpedCardNumber = new EncryptDecrypt().DecryptText(GetPayDetails.CardNumber);
                string decrytpedCardMonth = new EncryptDecrypt().DecryptText(GetPayDetails.CardMonth);
                string decrytpedCardYear = new EncryptDecrypt().DecryptText(GetPayDetails.CardYear);
                string decryptedPayemntCardNumber = new EncryptDecrypt().DecryptText(GetPayDetails.CardNumber);

                decimal processingFees = 0;

                if (GePropertyData != null)
                {
                    processingFees = GePropertyData.ProcessingFees ?? 0;
                }

                string transStatus = "";
                string cardaccnum = "";
                model.Email = GetCoappDet.Email;
                model.ProcessingFees = processingFees;
                model.Name_On_Card = GetPayDetails.Name_On_Card;
                if (GetPayDetails.PaymentMethod == 2)
                {
                    model.CardNumber = decryptedPayemntCardNumber;
                    model.CardMonth = decrytpedCardMonth;
                    model.CardYear = decrytpedCardYear;
                    cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (GetPayDetails.PaymentMethod == 1)
                {
                    model.AccountNumber = decrytpedCardNumber;
                    model.RoutingNumber = model.CCVNumber;
                    cardaccnum = model.AccountNumber.Substring(model.AccountNumber.Length - 4);
                    transStatus = new UsaePayModel().ChargeACH(model);
                }

                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                string bat = "";

                if (strlist[1] != "000000")
                {
                    if (model.FromAcc == 1)
                    {
                        bat = model.AID.ToString();
                        //Added by Sachin M 28 Apr 8:28PM
                        var coappliList = db.tbl_Applicant.Where(pp => pp.ApplicantID == model.AID).FirstOrDefault();
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
                    if (model.FromAcc == 4)
                    {
                        bat = "4";
                    }
                    if (model.FromAcc == 5)
                    {
                        bat = "5";
                    }
                    
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = Convert.ToInt64(GetProspectData.UserId),
                        PAID = model.PAID.ToString(),
                        Transaction_Date = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[2],
                        Charge_Date = DateTime.Now,
                        Charge_Type = Convert.ToInt32(bat),
                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,
                        Batch = bat,
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        UserID = GetCoappDet.UserID,
                        RefNum = strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();

                    var TransId = saveTransaction.TransID;
                    MyTransactionModel mm = new MyTransactionModel();
                    mm.CreateTransBill(TransId, Convert.ToDecimal(model.Charge_Amount), model.Description);

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    string message = "";
                    string phonenumber = GetCoappDet.Phone;
                    string sub = "";
                    if (model != null)
                    {
                        if (model.FromAcc == 1 || model.FromAcc == 2)
                        {
                            GetProspectData.IsApplyNow = 2;
                            db.SaveChanges();

                            //sub = "Online Application Fees Payment Received";
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Online Application Fees Payment Received");
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                            //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);

                            sub = "Sanctuary Payment Confirmation";

                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + GetCoappDet.FirstName + " " + GetCoappDet.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                            string body = reportHTML;

                            //sachin m 01 may 2020
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Sanctuary Payment Confirmation. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }
                        }
                        else if (model.FromAcc == 4)
                        {
                            //sachin m 01 may 2020
                            // Credit Check Fees Paid //

                            //sub = "Credit Check Fees Payment Received";
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Credit Check Fees Payment Received");
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your Credit Check fees payment.  Please save this email for your personal records.  Your application is being processed for background check, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                            //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            //string body = reportHTML;
                            //new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            //message = "Credit Check Fees Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";
                            //if (SendMessage == "yes")
                            //{
                            //    if (!string.IsNullOrWhiteSpace(phonenumber))
                            //    {
                            //        new TwilioService().SMS(phonenumber, message);
                            //    }
                            //}
                            sub = "Payment Confirmation, Application agreement and Rental Qualifications";

                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">It’s all happening, " + GetCoappDet.FirstName + " " + GetCoappDet.LastName + "! You’ve submitted the required information for your credit check and accepted the “Application Agreement & Rental Qualifications” document in the prospect portal. We’ve attached documents for your review. Please contact us with any questions.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Application fees are non refundable, even if the application is denied except to the extent otherwise required by applicable law</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                            string body = reportHTML;

                            string rulepolicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rules_Policies_fragmented.pdf"); ;
                            string rentalqualification = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rental_qualifications_fragmented.pdf"); ;

                            List<string> filePaths = new List<string>();
                            filePaths.Add(rulepolicy);
                            filePaths.Add(rentalqualification);

                            new EmailSendModel().SendEmailWithAttachment(GetCoappDet.Email, sub, body, filePaths);
                            message = "Payment Confirmation, Application agreement and Rental Qualifications";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }

                            string pass = "";
                            pass = new EncryptDecrypt().DecryptText(UserData.Password);


                            // Credit Check Background Check //

                            //var acutraqrequest = new AcutraqRequest();
                            //TenantOnlineModel modelTD = new TenantOnlineModel().GetTenantOnlineList((int)GetProspectData.ID, GetCoappDet.UserID ?? 0);
                            //string result = await acutraqrequest.PostAqutraqTenant(modelTD);

                            //if (result == "1")
                            //{
                            //var gerResultData = db.tbl_BackgroundScreening.Where(p => p.TenantId == model.ProspectId && p.Type == "TENTCREDIT").FirstOrDefault();

                            GetCoappDet.CreditPaid = 1;
                            db.SaveChanges();

                            //string reportHTMLbc = "";
                            //reportHTMLbc = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect5.html");
                            //reportHTMLbc = reportHTMLbc.Replace("[%ServerURL%]", serverURL);
                            //sub = "Credit Check Approved and Complete Online Application";
                            //reportHTMLbc = reportHTMLbc.Replace("[%EmailHeader%]", "Credit Check Approved and Complete Online Application");
                            //reportHTMLbc = reportHTMLbc.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Congratulation! Your application for credit check is approved. Please complete your Online Application by clicking below link</p>");
                            //reportHTMLbc = reportHTMLbc.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTMLbc = reportHTMLbc.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            //reportHTMLbc = reportHTMLbc.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                            //string bodybc = reportHTMLbc;
                            //new EmailSendModel().SendEmail(GetCoappDet.Email, "Credit Check Approved", bodybc);
                            //message = "Credit Check Approved. Please check the email for detail.";
                            //if (SendMessage == "yes")
                            //{
                            //    if (!string.IsNullOrWhiteSpace(phonenumber))
                            //    {
                            //        new TwilioService().SMS(phonenumber, message);
                            //    }
                            //}
                            ////}
                        }
                        else if (model.FromAcc == 5)
                        {
                            GetCoappDet.BackGroundPaid = 1;
                            GetCoappDet.Paid = 1;
                            db.SaveChanges();

                            filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                            reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                            message = "";
                            phonenumber = GetCoappDet.Phone;
                            if (model != null)
                            {
                                //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Background Check Payment Received");
                                //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                                //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);

                                //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);

                                string emailBody = "";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetCoappDet.FirstName + " " + GetCoappDet.LastName + "<br/>Thank you for submitting your application to sanctuary Doral.We are excited that you are interested in joining our community.This email confirms we have received your online application fees payment, please save this email for your personal records.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Application fees are non refundable, even if the application is denied except to the extent otherwise required by applicable law</p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us at <a style=\"text-decoration:none;\" href=\"tel:+1-786-684-7634\">(786) 648-7634</a></p>";
                                emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login to edit application</p>";
                                emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "/Account/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";
                                reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                            }
                            string body = reportHTML;

                            string rulepolicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rules_Policies_fragmented.pdf"); ;
                            string rentalqualification = HttpContext.Current.Server.MapPath("~/Content/assets/img/Policy/Rental_qualifications_fragmented.pdf"); ;
                            string appSummary = new ApplyNowModel().PrintApplicationForm(GetProspectData.ID);

                            List<string> filePaths = new List<string>();
                            filePaths.Add(appSummary);
                            filePaths.Add(rulepolicy);
                            filePaths.Add(rentalqualification);

                            new EmailSendModel().SendEmailWithAttachment(GetCoappDet.Email, "Your Sanctuary Rental Application and Rules and Policies", body, filePaths);
                            message = "Your Sanctuary Rental Application and Rules and Policies. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }
                        }
                        else
                        {
                            GetProspectData.IsApplyNow = 2;
                            db.SaveChanges();

                            //sub = "Administration Fees Payment Received";
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Administration Fees Payment Received");
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p>");
                            //reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            ////sachin m 01 may 2020
                            //string body = reportHTML;
                            //new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            //message = "Administration Fees Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";

                            sub = "Sanctuary Payment Confirmation";

                            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + GetCoappDet.FirstName + " " + GetCoappDet.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (model.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + processingFees.ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((model.Charge_Amount ?? 0) + processingFees).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                            string body = reportHTML;

                            //sachin m 01 may 2020
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Sanctuary Payment Confirmation. Please check the email for detail.";

                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }
                            var currentUser = new CurrentUser();
                            currentUser.UserID = Convert.ToInt32(GetProspectData.UserId);

                            (new ShomaGroupWebSession()).SetWebSession(currentUser);
                        }
                    }
                    msg = "1";
                }
                else
                {
                    msg = "0";
                }

            }

            db.Dispose();
            return msg;
        }
        public ApplyNowModel ForgetPassword(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            var forgetPassword = db.tbl_Login.Where(co => co.Username == model.Email).FirstOrDefault();
            string message = "";
            string phonenumber = "";
            if (forgetPassword != null)
            {
                forgetPassword.IsChangePass = 1;
                forgetPassword.ChangeDate = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                model.Message = "Invalid UserName";
            }
            db.Dispose();
            if (model.Message != "Invalid UserName")
            {
                string password = "";
                try
                {
                    password = (!string.IsNullOrWhiteSpace(model.Password) ? new EncryptDecrypt().DecryptText(model.Password) : "");
                }
                catch
                {
                }
                string uidd = new EncryptDecrypt().EncryptText(forgetPassword.UserID.ToString() + "," + (forgetPassword.IsTempPass ?? 0).ToString());
                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "ForgetPassword.html");

                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                //reportHTML = reportHTML.Replace("[%TenantName%]", model.FullName);
                //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/ApplyNow/ChangePassword/?uid=" + uidd + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/ApplyNow/ChangePassword/?uid=" + uidd + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Change Password</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                //string body = reportHTML;

                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                string emailBody = "";
                emailBody += "<p style=\"margin-bottom: 0px;\">Dear "+ model.FullName + "</p>";
                emailBody += "<p style=\"margin-bottom: 0px;\">At your request we have reset your password, please click the link below. Which will prompt you to create a new password. </p>";
                emailBody += "<p style=\"margin-bottom: 0px;\">Reset password</p>";
                emailBody += "<p style=\"margin-bottom: 0px;text-align: center;\"><a href=\"" + serverURL + "/ApplyNow/ChangePassword/?uid=" + uidd + "\" class=\"link-button\" target=\"_blank\">Reset Password</a></p>";
                emailBody += "<p style=\"margin-bottom: 20px;\">Your account security is important to us, if any of the above information is inaccurate please contact us using the information below.</p>";
                reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                string body = reportHTML;

                new EmailSendModel().SendEmail(model.Email, "Reset Password Link", body);

                message = "Your password change link has been sent to your email. Please check the email for detail.";
                if (SendMessage == "yes")
                {
                    if (!string.IsNullOrWhiteSpace(phonenumber))
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }
            }
            return model;
        }
        public string ExpireChangePassword(long UserID)
        {
            string IsLinkExpired = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var isUserCorrct = db.tbl_Login.Where(co => co.UserID == UserID).FirstOrDefault();
            if (isUserCorrct.ChangeDate <= DateTime.Now.AddMinutes(-5))
            {
                IsLinkExpired = "Yes";
            }
            else
            {
                IsLinkExpired = "No";
            }
            db.Dispose();
            return IsLinkExpired;
        }
        public string GetEmailAddress(long UserID)
        {
            string emailaddress = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var user = db.tbl_Login.Where(co => co.UserID == UserID).FirstOrDefault();
            if (user!=null)
            {
                emailaddress = user.Email;
            }
            db.Dispose();
            return emailaddress;
        }
        public string SaveChangePassword(long UserID,string EmailId,string NewPassword)
        {
            string IsEmailExist = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            string pass = new EncryptDecrypt().EncryptText(NewPassword.ToString());
            var isUserCorrct= db.tbl_Login.Where(co => co.Email == EmailId && co.UserID==UserID).FirstOrDefault();
            if (isUserCorrct != null)
            {
                isUserCorrct.Password = pass;
                isUserCorrct.IsChangePass = 0;
                isUserCorrct.IsTempPass = 0;
                db.SaveChanges();
            }
            else
            {
                IsEmailExist = "No";
            }
            db.Dispose();
            return IsEmailExist;
        }
        public string CheckEmailAreadyExist(string EmailId)
        {
            string IsEmailExist = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var isEmailExistApplyNow = db.tbl_ApplyNow.Where(co => co.Email == EmailId).FirstOrDefault();
            if (isEmailExistApplyNow != null)
            {
                var isEmailExistTenantInfo = db.tbl_TenantInfo.Where(co => co.Email == EmailId).FirstOrDefault();

                if (isEmailExistTenantInfo != null)
                {
                    IsEmailExist = "Yes Tenant";
                }
                else
                {
                    IsEmailExist = "Yes But Not Tenant";
                }
            }
            else
            {
                IsEmailExist = "No";
            }
            db.Dispose();
            return IsEmailExist;
        }
        public string SaveUpdateStep(long ID, int StepCompleted)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == ID).FirstOrDefault();
            var tenentUID = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var tenantData = db.tbl_TenantOnline.Where(p => p.ParentTOID == tenentUID).FirstOrDefault();
            var applicantData = db.tbl_Applicant.Where(p => p.UserID == tenentUID).FirstOrDefault();

            int stepcomp = 0;
            stepcomp = onlineProspectData.StepCompleted ?? 0;
            if (stepcomp < StepCompleted)
            {
                stepcomp = StepCompleted;
            }
            TenantOnlineModel mm = new TenantOnlineModel();
            mm.CallAptly();
            if (onlineProspectData != null)
            {
                if (applicantData.Type == "Primary Applicant ")
                {
                    onlineProspectData.StepCompleted = stepcomp;
                    db.SaveChanges();
                }
            }

            if (tenantData != null)
            {
                tenantData.StepCompleted = stepcomp;
                db.SaveChanges();
            }
            if (StepCompleted == 18)
            {
                if (applicantData.Type != "Primary Applicant ")
                {
                    var filePathSummaryPrint = HttpContext.Current.Server.MapPath(PrintApplicationForm(ID));
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");

                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    string phonenumber = onlineProspectData.Phone;
                    if (tenantData != null)
                    {
                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">"+applicantData.Type + " " + applicantData.FirstName + " " + applicantData.LastName + " has Finished the application</p>";
                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", applicantData.Type + " " + applicantData.FirstName + " " + applicantData.LastName + " has Finished the application");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'></br>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; " + applicantData.Type + " " + applicantData.FirstName + " " + applicantData.LastName + " has finished the application on " + DateTime.Now + "</p>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", onlineProspectData.FirstName + " " + onlineProspectData.LastName);
                        //reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                        message = applicantData.Type + " " + applicantData.FirstName + " " + applicantData.LastName + " has completed the application";
                    }
                    string body = reportHTML;
                    List<string> filePaths = new List<string>();
                    filePaths.Add(filePathSummaryPrint);

                    new EmailSendModel().SendEmailWithAttachment(onlineProspectData.Email, applicantData.Type + " " + applicantData.FirstName + " " + applicantData.LastName + " has completed the application", body, filePaths);
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
                    }
                }
            }
            return "1";
        }

        public string CheckUnitAvailable(long UnitID, long ProspectID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string isavailable = "1";
            var applyNowData = db.tbl_ApplyNow.Where(p => p.PropertyId == UnitID &&  p.ID!= ProspectID && p.Status.Trim()!= "Denied").FirstOrDefault();
            var unitData = db.tbl_PropertyUnits.Where(p => p.UID == UnitID).FirstOrDefault();
            string unitNumber = unitData.UnitNo;
            if (applyNowData!=null)
            {
                isavailable = "0|"+ unitNumber;
            }
            else
            {
                isavailable = "1|" + unitNumber;
            }

            return isavailable;
        }
        public string PrintApplicationForm(long TenantID)
        {
            string filename = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            try
            {
                DataSet dtTableSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetApplicationSummaryData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramUID = cmd.CreateParameter();
                    paramUID.ParameterName = "UserId";
                    paramUID.Value = userid;
                    cmd.Parameters.Add(paramUID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTableSet);
                    db.Database.Connection.Close();
                }

                if(dtTableSet.Tables.Count>0)
                {
                    dtTableSet.Tables[0].TableName = "ApartmentInfo";
                    dtTableSet.Tables[1].TableName = "ApplicantFullInfo";
                    dtTableSet.Tables[2].TableName = "OtherResInfo";
                    dtTableSet.Tables[3].TableName = "OtherEmpInfo";
                    dtTableSet.Tables[4].TableName = "PetInfo";
                    dtTableSet.Tables[5].TableName = "VehicleInfo";
                }


                string reportHTML = "";
                string savePath =HttpContext.Current. Server.MapPath("~/Content/assets/img/Document/");
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "ApplicationPrintTemplate.html");
                //--ServerURL--//
                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                //-- ApartmentInfo --//
                reportHTML = reportHTML.Replace("[%UnitNo%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["UnitNo"].ToString());
                reportHTML = reportHTML.Replace("[%MoveInDate%]", DateString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["MoveInDate"].ToString()));
                reportHTML = reportHTML.Replace("[%Model%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Building"].ToString());
                reportHTML = reportHTML.Replace("[%LeaseTerm%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["LeaseTerm"].ToString());
                reportHTML = reportHTML.Replace("[%BedRoom%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Bedroom"].ToString());
                reportHTML = reportHTML.Replace("[%Deposite%]", DecimalString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["Deposit"].ToString()));
                reportHTML = reportHTML.Replace("[%Bath%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Bathroom"].ToString());
                reportHTML = reportHTML.Replace("[%Rent%]", DecimalString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["MonthlyCharges"].ToString()));
                reportHTML = reportHTML.Replace("[%Area%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Area"].ToString()+ " Sq. Ft.");

                //-- ApplicantInfo --//
                reportHTML = reportHTML.Replace("[%ApplicantName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantName"].ToString());
                reportHTML = reportHTML.Replace("[%ApplicantDOB%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantDOB"].ToString()));
                reportHTML = reportHTML.Replace("[%ApplicantEmail%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantEmail"].ToString());

                string ssn = "";
                try { ssn = new EncryptDecrypt().DecryptText(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantSSN"].ToString()); ssn = "***-**-" + ssn.Substring(5, 4); } catch { }

                reportHTML = reportHTML.Replace("[%ApplicantSSN%]", ssn);

                reportHTML = reportHTML.Replace("[%ApplicantPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantPhone"].ToString()));
                reportHTML = reportHTML.Replace("[%ApplicantIDType%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDType"].ToString());

                string idnumber = "";
                try { idnumber = new EncryptDecrypt().DecryptText(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDNumber"].ToString()); idnumber = new String('*', idnumber.Length - 4) + idnumber.Substring(idnumber.Length-4, 4); } catch { }
                reportHTML = reportHTML.Replace("[%ApplicantIDNumber%]", idnumber);

                reportHTML = reportHTML.Replace("[%ApplicantIDIssuingState%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDIssuingState"].ToString());
                reportHTML = reportHTML.Replace("[%ApplicantIssuingCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIssuingCountry"].ToString());

                //-- CurrentResidenceInfo --//
                reportHTML = reportHTML.Replace("[%CurrentMoveInDate%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMoveInDate"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentAddress%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAddress"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentMonthlyPayment%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMonthlyPayment"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentCountry"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAptCommunity%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAptCommunity"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentNoticeGiven%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentNoticeGiven"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentMgmtCompany%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMgmtCompany"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentMgmtCompanyPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMgmtCompanyPhone"].ToString()));

                //--PerviousResidenceInfo --//
                int startIndexPRI = reportHTML.IndexOf("[%PRHStart%]") + 12;
                int endIndexPRI = reportHTML.IndexOf("[%PRHEnd%]");
                int stringLengthPRI = endIndexPRI - startIndexPRI;
                string otherResRepeat = reportHTML.Substring(startIndexPRI, stringLengthPRI);
                string otherResData = "";
                reportHTML = reportHTML.Replace(otherResRepeat, "");
                reportHTML = reportHTML.Replace("[%PRHStart%]", "");
                reportHTML = reportHTML.Replace("[%PRHEnd%]", "");

                if (dtTableSet.Tables["OtherResInfo"].Rows.Count > 0)
                {
                    reportHTML = reportHTML.Replace("[%PerviousResidenceHidden%]", "");
                    foreach (DataRow drPRH in dtTableSet.Tables["OtherResInfo"].Rows)
                    {
                        otherResData += otherResRepeat;
                        otherResData = otherResData.Replace("[%OtherMoveInDateFrom%]", DateString(drPRH["OtherMoveInDateFrom"].ToString()));
                        otherResData = otherResData.Replace("[%OtherMoveInDateTo%]", DateString(drPRH["OtherMoveInDateTo"].ToString()));
                        otherResData = otherResData.Replace("[%OtherAddress%]", drPRH["OtherAddress"].ToString());
                        otherResData = otherResData.Replace("[%OtherMonthlyPayment%]", DecimalString(drPRH["OtherMonthlyPayment"].ToString()));
                        otherResData = otherResData.Replace("[%OtherCountry%]", drPRH["OtherCountry"].ToString());
                        otherResData = otherResData.Replace("[%OtherAptCommunity%]", drPRH["OtherAptCommunity"].ToString());
                        otherResData = otherResData.Replace("[%OtherNoticeGiven%]", drPRH["OtherNoticeGiven"].ToString());
                        otherResData = otherResData.Replace("[%OtherMgmtCompany%]", drPRH["OtherMgmtCompany"].ToString());
                        otherResData = otherResData.Replace("[%OtherMgmtCompanyPhone%]", FormatPhoneNumber(drPRH["OtherMgmtCompanyPhone"].ToString()));
                    }
                    reportHTML = reportHTML.Replace("[%PreviousResidenceInformation%]", otherResData);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PerviousResidenceHidden%]", "hidden");
                }

                //-- CurrentEmploymentInfo --//
                reportHTML = reportHTML.Replace("[%CurrentOfficeCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentOfficeCountry"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentJobTitle%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentJobTitle"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentJobType%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentJobType"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentStartDate%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentStartDate"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentEmployerName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentEmployerName"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAnnualIncome%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAnnualIncome"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentSupervisorName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentSupervisorName"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAnnualAddIncome%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAnnualAddIncome"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentEmployeerAddress%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentEmployeerAddress"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentSupervisorPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentSupervisorPhone"].ToString()));

                //--PreviousEmploymentInfo --//
                int startIndexPEI = reportHTML.IndexOf("[%PEHStart%]") + 12;
                int endIndexPEI = reportHTML.IndexOf("[%PEHEnd%]");
                int stringLengthPEI = endIndexPEI - startIndexPEI;
                string otherEmpRepeat = reportHTML.Substring(startIndexPEI, stringLengthPEI);
                string otherEmpData = "";
                reportHTML = reportHTML.Replace(otherEmpRepeat, "");
                reportHTML = reportHTML.Replace("[%PEHStart%]", "");
                reportHTML = reportHTML.Replace("[%PEHEnd%]", "");

                if (dtTableSet.Tables["OtherEmpInfo"].Rows.Count > 0)
                {
                    reportHTML = reportHTML.Replace("[%PerviousResidenceHidden%]", "");
                    foreach (DataRow drPEH in dtTableSet.Tables["OtherEmpInfo"].Rows)
                    {
                        otherEmpData += otherEmpRepeat;
                        otherEmpData = otherEmpData.Replace("[%OtherEmployerName%]", drPEH["OtherEmployerName"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherJobTitle%]", drPEH["OtherJobTitle"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherStartDate%]", DateString(drPEH["OtherStartDate"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherTerminationDate%]", DateString(drPEH["OtherTerminationDate"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherSupervisorName%]", drPEH["OtherSupervisorName"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherEmployeerAddress%]", drPEH["OtherEmployeerAddress"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherAnnualIncome%]", DecimalString(drPEH["OtherAnnualIncome"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherAnnualAddIncome%]", DecimalString(drPEH["OtherAnnualAddIncome"].ToString()));
                    }
                    reportHTML = reportHTML.Replace("[%PreviousEmploymentInformation%]", otherResData);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PerviousEmploymentHidden%]", "hidden");
                }

                //-- EmergencyInfo --//
                reportHTML = reportHTML.Replace("[%EmergencyName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EmergencyName"].ToString());
                reportHTML = reportHTML.Replace("[%EmergencyRelationship%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EmergencyRelationship"].ToString());
                reportHTML = reportHTML.Replace("[%EmergencyHomePhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EmergencyHomePhone"].ToString()));
                reportHTML = reportHTML.Replace("[%EmergencyEmail%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EmergencyEmail"].ToString());
                reportHTML = reportHTML.Replace("[%EmergencyCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EmergencyCountry"].ToString());
                reportHTML = reportHTML.Replace("[%EmergencyAddress%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EmergencyAddress"].ToString());

                //-- PetInfo --//
                string petInfo = "";
                if(dtTableSet.Tables["PetInfo"].Rows.Count>0)
                {
                    foreach (DataRow drPet in dtTableSet.Tables["PetInfo"].Rows)
                    {
                        petInfo += "<tr>";
                        petInfo += "<td class='td-pet'>" + drPet["PetName"].ToString() + "</td>";
                        petInfo += "<td class='td-pet'>" + drPet["Breed"].ToString() + "</td>";
                        petInfo += "<td class='td-pet'>" + drPet["Weight"].ToString() + "</td>";
                        petInfo += "</tr>";
                    }

                    reportHTML = reportHTML.Replace("[%PetInformation%]", petInfo);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PetInformation%]", "");
                }

                //-- VehicleInfo --//
                string vehicleInfo = "";
                if (dtTableSet.Tables["VehicleInfo"].Rows.Count > 0)
                {
                    foreach (DataRow drPet in dtTableSet.Tables["VehicleInfo"].Rows)
                    {
                        vehicleInfo += "<tr>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Make"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Model"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Year"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Color"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["License"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["StateName"].ToString() + "</td>";
                        vehicleInfo += "</tr>";
                    }

                    reportHTML = reportHTML.Replace("[%VehicleInfromation%]", vehicleInfo);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%VehicleInfromation%]", "");
                }

                //--AdditionalQuestions--//

                reportHTML = reportHTML.Replace("[%EvictedDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EvictedDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EvictedDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%Evicted%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["Evicted"].ToString());
                reportHTML = reportHTML.Replace("[%EvictedHidden%]", (dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["Evicted"].ToString()=="No"?"hidden":""));
                reportHTML = reportHTML.Replace("[%ConvictedFelonyDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelonyDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelonyDetails"].ToString() : "__________"); 
                reportHTML = reportHTML.Replace("[%ConvictedFelony%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelony"].ToString());
                reportHTML = reportHTML.Replace("[%CriminalChargPenDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPenDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPenDetails"].ToString() : "__________"); 
                reportHTML = reportHTML.Replace("[%CriminalChargPen%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPen"].ToString());
                reportHTML = reportHTML.Replace("[%DoYouSmoke%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["DoYouSmoke"].ToString());
                reportHTML = reportHTML.Replace("[%ReferredResident%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResident"].ToString());
                reportHTML = reportHTML.Replace("[%ReferredResidentName%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResidentName"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResidentName"].ToString() : "__________"); 
                reportHTML = reportHTML.Replace("[%ReferredBrokerMerchant%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredBrokerMerchant"].ToString());
                

                List<IElement> elements = iText.Html2pdf.HtmlConverter.ConvertToElements(reportHTML).ToList();
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
                    pdf.SetTagged();
                    Document document = new Document(pdf);
                    document.SetMargins(0, 0, 0, 0);
                    foreach (IElement element in elements)
                    {
                        document.Add((IBlockElement)element);
                    }
                    document.Close();
                    bytes = stream.ToArray();
                }
                filename = savePath + "ApplicationSummary_" + TenantID.ToString() + ".pdf";
                System.IO.File.WriteAllBytes(filename, bytes);
                filename = "/Content/assets/img/Document/ApplicationSummary_" + TenantID.ToString() + ".pdf";
                db.Dispose();
               
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }

            return filename;
        }
        public string FormatPhoneNumber(string phoneNum)
        {
            string phoneFormat = "(###) ###-####";
            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");
            if (phoneNum.Length > 0)
            {
                phoneNum = Convert.ToInt64(phoneNum).ToString(phoneFormat);
            }
            return phoneNum;
        }
        public string DateString(string converDate)
        {
            string datestring = "";
            try { datestring = Convert.ToDateTime(converDate).ToString("MM/dd/yyyy"); } catch { datestring = ""; }
            return datestring;
        }
        public string DecimalString(string converDecimal)
        {
            string decimaltring = "";
            try { decimaltring = Convert.ToDecimal(converDecimal).ToString("0.00"); } catch { decimaltring = "0.00"; }
            return decimaltring;
        }
        public string PrintGuarantorForm(long TenantID)
        {
            string filename = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            try
            {
                DataSet dtTableSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetGuarantorApplicationSummaryData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramUID = cmd.CreateParameter();
                    paramUID.ParameterName = "UserId";
                    paramUID.Value = userid;
                    cmd.Parameters.Add(paramUID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTableSet);
                    db.Database.Connection.Close();
                }

                if (dtTableSet.Tables.Count > 0)
                {
                    dtTableSet.Tables[0].TableName = "ApartmentInfo";
                    dtTableSet.Tables[1].TableName = "ApplicantFullInfo";
                    dtTableSet.Tables[2].TableName = "OtherEmpInfo";
                }


                string reportHTML = "";
                string savePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "GuarantorPrintTemplate.html");
                //--ServerURL--//
                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                // --ApartmentInfo--//
                reportHTML = reportHTML.Replace("[%UnitNo%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["UnitNo"].ToString());
                reportHTML = reportHTML.Replace("[%MoveInDate%]", DateString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["MoveInDate"].ToString()));
                reportHTML = reportHTML.Replace("[%Model%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Building"].ToString());
                reportHTML = reportHTML.Replace("[%LeaseTerm%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["LeaseTerm"].ToString());
                reportHTML = reportHTML.Replace("[%BedRoom%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Bedroom"].ToString());
                reportHTML = reportHTML.Replace("[%Deposite%]", DecimalString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["Deposit"].ToString()));
                reportHTML = reportHTML.Replace("[%Bath%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Bathroom"].ToString());
                reportHTML = reportHTML.Replace("[%Rent%]", DecimalString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["MonthlyCharges"].ToString()));
                reportHTML = reportHTML.Replace("[%Area%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Area"].ToString() + " Sq. Ft.");

                //-- ApplicantInfo --//
                reportHTML = reportHTML.Replace("[%ApplicantName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantName"].ToString());
                reportHTML = reportHTML.Replace("[%ApplicantDOB%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantDOB"].ToString()));
                reportHTML = reportHTML.Replace("[%ApplicantEmail%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantEmail"].ToString());

                string ssn = "";
                try { ssn = new EncryptDecrypt().DecryptText(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantSSN"].ToString()); ssn = "***-**-" + ssn.Substring(5, 4); } catch { }

                reportHTML = reportHTML.Replace("[%ApplicantSSN%]", ssn);

                reportHTML = reportHTML.Replace("[%ApplicantPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantPhone"].ToString()));
                reportHTML = reportHTML.Replace("[%ApplicantIDType%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDType"].ToString());

                string idnumber = "";
                try { idnumber = new EncryptDecrypt().DecryptText(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDNumber"].ToString()); idnumber = new String('*', idnumber.Length - 4) + idnumber.Substring(idnumber.Length - 4, 4); } catch { }
                reportHTML = reportHTML.Replace("[%ApplicantIDNumber%]", idnumber);

                reportHTML = reportHTML.Replace("[%ApplicantIDIssuingState%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDIssuingState"].ToString());
                reportHTML = reportHTML.Replace("[%ApplicantIssuingCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIssuingCountry"].ToString());

                //-- CurrentEmploymentInfo --//
                reportHTML = reportHTML.Replace("[%CurrentOfficeCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentOfficeCountry"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentJobTitle%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentJobTitle"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentJobType%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentJobType"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentStartDate%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentStartDate"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentEmployerName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentEmployerName"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAnnualIncome%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAnnualIncome"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentSupervisorName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentSupervisorName"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAnnualAddIncome%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAnnualAddIncome"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentEmployeerAddress%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentEmployeerAddress"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentSupervisorPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentSupervisorPhone"].ToString()));

                //--PreviousEmploymentInfo --//
                int startIndexPEI = reportHTML.IndexOf("[%PEHStart%]") + 12;
                int endIndexPEI = reportHTML.IndexOf("[%PEHEnd%]");
                int stringLengthPEI = endIndexPEI - startIndexPEI;
                string otherEmpRepeat = reportHTML.Substring(startIndexPEI, stringLengthPEI);
                string otherEmpData = "";
                reportHTML = reportHTML.Replace(otherEmpRepeat, "");
                reportHTML = reportHTML.Replace("[%PEHStart%]", "");
                reportHTML = reportHTML.Replace("[%PEHEnd%]", "");

                if (dtTableSet.Tables["OtherEmpInfo"].Rows.Count > 0)
                {
                    reportHTML = reportHTML.Replace("[%PerviousResidenceHidden%]", "");
                    foreach (DataRow drPEH in dtTableSet.Tables["OtherEmpInfo"].Rows)
                    {
                        otherEmpData += otherEmpRepeat;
                        otherEmpData = otherEmpData.Replace("[%OtherEmployerName%]", drPEH["OtherEmployerName"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherJobTitle%]", drPEH["OtherJobTitle"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherStartDate%]", DateString(drPEH["OtherStartDate"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherTerminationDate%]", DateString(drPEH["OtherTerminationDate"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherSupervisorName%]", drPEH["OtherSupervisorName"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherEmployeerAddress%]", drPEH["OtherEmployeerAddress"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherAnnualIncome%]", DecimalString(drPEH["OtherAnnualIncome"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherAnnualAddIncome%]", DecimalString(drPEH["OtherAnnualAddIncome"].ToString()));
                    }
                    reportHTML = reportHTML.Replace("[%PreviousEmploymentInformation%]", otherEmpData);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PerviousEmploymentHidden%]", "hidden");
                }



                //--AdditionalQuestions--//

                reportHTML = reportHTML.Replace("[%EvictedDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EvictedDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EvictedDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%Evicted%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["Evicted"].ToString());
                reportHTML = reportHTML.Replace("[%EvictedHidden%]", (dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["Evicted"].ToString() == "No" ? "hidden" : ""));
                reportHTML = reportHTML.Replace("[%ConvictedFelonyDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelonyDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelonyDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%ConvictedFelony%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelony"].ToString());
                reportHTML = reportHTML.Replace("[%CriminalChargPenDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPenDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPenDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%CriminalChargPen%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPen"].ToString());
                reportHTML = reportHTML.Replace("[%DoYouSmoke%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["DoYouSmoke"].ToString());
                reportHTML = reportHTML.Replace("[%ReferredResident%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResident"].ToString());
                reportHTML = reportHTML.Replace("[%ReferredResidentName%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResidentName"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResidentName"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%ReferredBrokerMerchant%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredBrokerMerchant"].ToString());


                List<IElement> elements = iText.Html2pdf.HtmlConverter.ConvertToElements(reportHTML).ToList();
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
                    pdf.SetTagged();
                    Document document = new Document(pdf);
                    document.SetMargins(0, 0, 0, 0);
                    foreach (IElement element in elements)
                    {
                        document.Add((IBlockElement)element);
                    }
                    document.Close();
                    bytes = stream.ToArray();
                }
                filename = savePath + "GuarantorApplicationSummary_" + TenantID.ToString() + ".pdf";
                System.IO.File.WriteAllBytes(filename, bytes);
                filename = "/Content/assets/img/Document/GuarantorApplicationSummary_" + TenantID.ToString() + ".pdf";
                db.Dispose();

            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }

            return filename;
        }
        public string PrintCoapplicantForm(long TenantID)
        {
            string filename = "";
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
                DataSet dtTableSet = new DataSet();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCoapplicantSummaryData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramUID = cmd.CreateParameter();
                    paramUID.ParameterName = "UserId";
                    paramUID.Value = userid;
                    cmd.Parameters.Add(paramUID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTableSet);
                    db.Database.Connection.Close();
                }

                if (dtTableSet.Tables.Count > 0)
                {
                    dtTableSet.Tables[0].TableName = "ApartmentInfo";
                    dtTableSet.Tables[1].TableName = "ApplicantFullInfo";
                    dtTableSet.Tables[2].TableName = "OtherResInfo";
                    dtTableSet.Tables[3].TableName = "OtherEmpInfo";
                    dtTableSet.Tables[4].TableName = "PetInfo";
                    dtTableSet.Tables[5].TableName = "VehicleInfo";
                }


                string reportHTML = "";
                string savePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "CoapplicantApplicationPrintTemplate.html");
                //--ServerURL--//
                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                //-- ApartmentInfo --//
                reportHTML = reportHTML.Replace("[%UnitNo%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["UnitNo"].ToString());
                reportHTML = reportHTML.Replace("[%MoveInDate%]", DateString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["MoveInDate"].ToString()));
                reportHTML = reportHTML.Replace("[%Model%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Building"].ToString());
                reportHTML = reportHTML.Replace("[%LeaseTerm%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["LeaseTerm"].ToString());
                reportHTML = reportHTML.Replace("[%BedRoom%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Bedroom"].ToString());
                reportHTML = reportHTML.Replace("[%Deposite%]", DecimalString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["Deposit"].ToString()));
                reportHTML = reportHTML.Replace("[%Bath%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Bathroom"].ToString());
                reportHTML = reportHTML.Replace("[%Rent%]", DecimalString(dtTableSet.Tables["ApartmentInfo"].Rows[0]["MonthlyCharges"].ToString()));
                reportHTML = reportHTML.Replace("[%Area%]", dtTableSet.Tables["ApartmentInfo"].Rows[0]["Area"].ToString() + " Sq. Ft.");

                //-- ApplicantInfo --//
                reportHTML = reportHTML.Replace("[%ApplicantName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantName"].ToString());
                reportHTML = reportHTML.Replace("[%ApplicantDOB%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantDOB"].ToString()));
                reportHTML = reportHTML.Replace("[%ApplicantEmail%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantEmail"].ToString());

                string ssn = "";
                try { ssn = new EncryptDecrypt().DecryptText(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantSSN"].ToString()); ssn = "***-**-" + ssn.Substring(5, 4); } catch { }

                reportHTML = reportHTML.Replace("[%ApplicantSSN%]", ssn);

                reportHTML = reportHTML.Replace("[%ApplicantPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantPhone"].ToString()));
                reportHTML = reportHTML.Replace("[%ApplicantIDType%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDType"].ToString());

                string idnumber = "";
                try { idnumber = new EncryptDecrypt().DecryptText(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDNumber"].ToString()); idnumber = new String('*', idnumber.Length - 4) + idnumber.Substring(idnumber.Length - 4, 4); } catch { }
                reportHTML = reportHTML.Replace("[%ApplicantIDNumber%]", idnumber);

                reportHTML = reportHTML.Replace("[%ApplicantIDIssuingState%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIDIssuingState"].ToString());
                reportHTML = reportHTML.Replace("[%ApplicantIssuingCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ApplicantIssuingCountry"].ToString());

                //-- CurrentResidenceInfo --//
                reportHTML = reportHTML.Replace("[%CurrentMoveInDate%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMoveInDate"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentAddress%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAddress"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentMonthlyPayment%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMonthlyPayment"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentCountry"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAptCommunity%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAptCommunity"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentNoticeGiven%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentNoticeGiven"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentMgmtCompany%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMgmtCompany"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentMgmtCompanyPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentMgmtCompanyPhone"].ToString()));

                //--PerviousResidenceInfo --//
                int startIndexPRI = reportHTML.IndexOf("[%PRHStart%]") + 12;
                int endIndexPRI = reportHTML.IndexOf("[%PRHEnd%]");
                int stringLengthPRI = endIndexPRI - startIndexPRI;
                string otherResRepeat = reportHTML.Substring(startIndexPRI, stringLengthPRI);
                string otherResData = "";
                reportHTML = reportHTML.Replace(otherResRepeat, "");
                reportHTML = reportHTML.Replace("[%PRHStart%]", "");
                reportHTML = reportHTML.Replace("[%PRHEnd%]", "");

                if (dtTableSet.Tables["OtherResInfo"].Rows.Count > 0)
                {
                    reportHTML = reportHTML.Replace("[%PerviousResidenceHidden%]", "");
                    foreach (DataRow drPRH in dtTableSet.Tables["OtherResInfo"].Rows)
                    {
                        otherResData += otherResRepeat;
                        otherResData = otherResData.Replace("[%OtherMoveInDateFrom%]", DateString(drPRH["OtherMoveInDateFrom"].ToString()));
                        otherResData = otherResData.Replace("[%OtherMoveInDateTo%]", DateString(drPRH["OtherMoveInDateTo"].ToString()));
                        otherResData = otherResData.Replace("[%OtherAddress%]", drPRH["OtherAddress"].ToString());
                        otherResData = otherResData.Replace("[%OtherMonthlyPayment%]", DecimalString(drPRH["OtherMonthlyPayment"].ToString()));
                        otherResData = otherResData.Replace("[%OtherCountry%]", drPRH["OtherCountry"].ToString());
                        otherResData = otherResData.Replace("[%OtherAptCommunity%]", drPRH["OtherAptCommunity"].ToString());
                        otherResData = otherResData.Replace("[%OtherNoticeGiven%]", drPRH["OtherNoticeGiven"].ToString());
                        otherResData = otherResData.Replace("[%OtherMgmtCompany%]", drPRH["OtherMgmtCompany"].ToString());
                        otherResData = otherResData.Replace("[%OtherMgmtCompanyPhone%]", FormatPhoneNumber(drPRH["OtherMgmtCompanyPhone"].ToString()));
                    }
                    reportHTML = reportHTML.Replace("[%PreviousResidenceInformation%]", otherResData);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PerviousResidenceHidden%]", "hidden");
                }

                //-- CurrentEmploymentInfo --//
                reportHTML = reportHTML.Replace("[%CurrentOfficeCountry%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentOfficeCountry"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentJobTitle%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentJobTitle"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentJobType%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentJobType"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentStartDate%]", DateString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentStartDate"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentEmployerName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentEmployerName"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAnnualIncome%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAnnualIncome"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentSupervisorName%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentSupervisorName"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentAnnualAddIncome%]", DecimalString(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentAnnualAddIncome"].ToString()));
                reportHTML = reportHTML.Replace("[%CurrentEmployeerAddress%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentEmployeerAddress"].ToString());
                reportHTML = reportHTML.Replace("[%CurrentSupervisorPhone%]", FormatPhoneNumber(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CurrentSupervisorPhone"].ToString()));

                //--PreviousEmploymentInfo --//
                int startIndexPEI = reportHTML.IndexOf("[%PEHStart%]") + 12;
                int endIndexPEI = reportHTML.IndexOf("[%PEHEnd%]");
                int stringLengthPEI = endIndexPEI - startIndexPEI;
                string otherEmpRepeat = reportHTML.Substring(startIndexPEI, stringLengthPEI);
                string otherEmpData = "";
                reportHTML = reportHTML.Replace(otherEmpRepeat, "");
                reportHTML = reportHTML.Replace("[%PEHStart%]", "");
                reportHTML = reportHTML.Replace("[%PEHEnd%]", "");

                if (dtTableSet.Tables["OtherEmpInfo"].Rows.Count > 0)
                {
                    reportHTML = reportHTML.Replace("[%PerviousEmploymentHidden%]", "");
                    foreach (DataRow drPEH in dtTableSet.Tables["OtherEmpInfo"].Rows)
                    {
                        otherEmpData += otherEmpRepeat;
                        otherEmpData = otherEmpData.Replace("[%OtherEmployerName%]", drPEH["OtherEmployerName"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherJobTitle%]", drPEH["OtherJobTitle"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherStartDate%]", DateString(drPEH["OtherStartDate"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherTerminationDate%]", DateString(drPEH["OtherTerminationDate"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherSupervisorName%]", drPEH["OtherSupervisorName"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherEmployeerAddress%]", drPEH["OtherEmployeerAddress"].ToString());
                        otherEmpData = otherEmpData.Replace("[%OtherAnnualIncome%]", DecimalString(drPEH["OtherAnnualIncome"].ToString()));
                        otherEmpData = otherEmpData.Replace("[%OtherAnnualAddIncome%]", DecimalString(drPEH["OtherAnnualAddIncome"].ToString()));
                    }
                    reportHTML = reportHTML.Replace("[%PreviousEmploymentInformation%]", otherEmpData);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PerviousEmploymentHidden%]", "hidden");
                }

                //-- PetInfo --//
                string petInfo = "";
                if (dtTableSet.Tables["PetInfo"].Rows.Count > 0)
                {
                    foreach (DataRow drPet in dtTableSet.Tables["PetInfo"].Rows)
                    {
                        petInfo += "<tr>";
                        petInfo += "<td class='td-pet'>" + drPet["PetName"].ToString() + "</td>";
                        petInfo += "<td class='td-pet'>" + drPet["Breed"].ToString() + "</td>";
                        petInfo += "<td class='td-pet'>" + drPet["Weight"].ToString() + "</td>";
                        petInfo += "</tr>";
                    }

                    reportHTML = reportHTML.Replace("[%PetInformation%]", petInfo);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PetInformation%]", "");
                }

                //-- VehicleInfo --//
                string vehicleInfo = "";
                if (dtTableSet.Tables["VehicleInfo"].Rows.Count > 0)
                {
                    foreach (DataRow drPet in dtTableSet.Tables["VehicleInfo"].Rows)
                    {
                        vehicleInfo += "<tr>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Make"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Model"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Year"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["Color"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["License"].ToString() + "</td>";
                        vehicleInfo += "<td class='td-vehicle'>" + drPet["StateName"].ToString() + "</td>";
                        vehicleInfo += "</tr>";
                    }

                    reportHTML = reportHTML.Replace("[%VehicleInfromation%]", vehicleInfo);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%VehicleInfromation%]", "");
                }

                //--AdditionalQuestions--//

                reportHTML = reportHTML.Replace("[%EvictedDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EvictedDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["EvictedDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%Evicted%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["Evicted"].ToString());
                reportHTML = reportHTML.Replace("[%EvictedHidden%]", (dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["Evicted"].ToString() == "No" ? "hidden" : ""));
                reportHTML = reportHTML.Replace("[%ConvictedFelonyDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelonyDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelonyDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%ConvictedFelony%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ConvictedFelony"].ToString());
                reportHTML = reportHTML.Replace("[%CriminalChargPenDetails%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPenDetails"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPenDetails"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%CriminalChargPen%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["CriminalChargPen"].ToString());
                reportHTML = reportHTML.Replace("[%DoYouSmoke%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["DoYouSmoke"].ToString());
                reportHTML = reportHTML.Replace("[%ReferredResident%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResident"].ToString());
                reportHTML = reportHTML.Replace("[%ReferredResidentName%]", !string.IsNullOrWhiteSpace(dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResidentName"].ToString()) ? dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredResidentName"].ToString() : "__________");
                reportHTML = reportHTML.Replace("[%ReferredBrokerMerchant%]", dtTableSet.Tables["ApplicantFullInfo"].Rows[0]["ReferredBrokerMerchant"].ToString());


                List<IElement> elements = iText.Html2pdf.HtmlConverter.ConvertToElements(reportHTML).ToList();
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
                    pdf.SetTagged();
                    Document document = new Document(pdf);
                    document.SetMargins(0, 0, 0, 0);
                    foreach (IElement element in elements)
                    {
                        document.Add((IBlockElement)element);
                    }
                    document.Close();
                    bytes = stream.ToArray();
                }
                filename = savePath + "Coapplicant_ApplicationSummary_" + TenantID.ToString() + ".pdf";
                System.IO.File.WriteAllBytes(filename, bytes);
                filename = "/Content/assets/img/Document/Coapplicant_ApplicationSummary_" + TenantID.ToString() + ".pdf";
                db.Dispose();

            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }

            return filename;
        }
        public string PrintQuotation(PrintQuotationModel model)
        {
            string filename = "";
            try
            {

                string reportHTML = "";
                string savePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "QuotationPrint.html");
               
                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                reportHTML = reportHTML.Replace("[%QuoteDate%]", model.QuoteDate);
                reportHTML = reportHTML.Replace("[%ApplicantName%]", model.ApplicantName);
                reportHTML = reportHTML.Replace("[%QuoteExpires%]", model.QuoteExpires);
                reportHTML = reportHTML.Replace("[%PhoneNumber%]", model.PhoneNumber);
                reportHTML = reportHTML.Replace("[%Email%]", model.Email);
                reportHTML = reportHTML.Replace("[%DesiredMoveIn%]", model.DesiredMoveIn);
                reportHTML = reportHTML.Replace("[%UnitNo%]", model.UnitNo);
                reportHTML = reportHTML.Replace("[%ModelName%]", model.ModelName);
                reportHTML = reportHTML.Replace("[%LeaseTerm%]", model.LeaseTerm);
                reportHTML = reportHTML.Replace("[%AssignParkingSpace%]", model.AssignParkingSpace);
                reportHTML = reportHTML.Replace("[%ApplicationFees%]", model.ApplicationFees);
                reportHTML = reportHTML.Replace("[%SecurityDeposit%]", model.SecurityDeposit);
                reportHTML = reportHTML.Replace("[%GuarantorFees%]", model.GuarantorFees.Replace("/Guarantor (if needed)", ""));
                reportHTML = reportHTML.Replace("[%PetNonRefundableFee%]", model.PetNonRefundableFee);
                reportHTML = reportHTML.Replace("[%AdministratorFee%]", model.AdministratorFee);
                reportHTML = reportHTML.Replace("[%PetDNAFee%]", model.PetDNAFee    );
                reportHTML = reportHTML.Replace("[%VehicleRegistration%]", model.VehicleRegistration);
                reportHTML = reportHTML.Replace("[%MonthlyRent%]", model.MonthlyRent);
                reportHTML = reportHTML.Replace("[%ProratedMonthlyRent%]", model.ProratedMonthlyRent);
                reportHTML = reportHTML.Replace("[%TrashFee%]", model.TrashFee);
                reportHTML = reportHTML.Replace("[%ProratedTrashFee%]", model.ProratedTrashFee);
                reportHTML = reportHTML.Replace("[%PestControlFee%]", model.PestControlFee);
                reportHTML = reportHTML.Replace("[%ProratedPestControlFee%]", model.ProratedPestControlFee);
                reportHTML = reportHTML.Replace("[%ConvergentBillingFee%]", model.ConvergentBillingFee);
                reportHTML = reportHTML.Replace("[%ProratedConvergentBillingFee%]", model.ProratedConvergentBillingFee);
                reportHTML = reportHTML.Replace("[%AdditionalParking%]", model.AdditionalParking);
                reportHTML = reportHTML.Replace("[%ProratedAdditionalParking%]", model.ProratedAdditionalParking);
                reportHTML = reportHTML.Replace("[%StorageAmount%]", model.StorageAmount);
                reportHTML = reportHTML.Replace("[%ProratedStorageAmount%]", model.ProratedStorageAmount);
                reportHTML = reportHTML.Replace("[%PetFee%]", model.PetFee);
                reportHTML = reportHTML.Replace("[%ProratedPetFee%]", model.ProratedPetFee);
                reportHTML = reportHTML.Replace("[%MonthlyCharges%]", model.MonthlyCharges);
                reportHTML = reportHTML.Replace("[%ProratedMonthlyCharges%]", model.ProratedMonthlyCharges);
                reportHTML = reportHTML.Replace("[%ModelImage%]", serverURL+ "content/assets/img/plan/" + model.ModelName+".jpg");

                reportHTML = reportHTML.Replace("[%CreditCheck%]", model.AppCreditFees);
                reportHTML = reportHTML.Replace("[%BackGroundCheck%]", model.AppBackgroundFees);

                try { reportHTML = reportHTML.Replace("[%NumberOfPet%]", (Convert.ToInt32(model.NumberOfPet) > 0 ? "(" + model.NumberOfPet + " Pet)" : "")); } catch { reportHTML = reportHTML.Replace("[%NumberOfPet%]", ""); }
                
                reportHTML = reportHTML.Replace("[%DueAtMoveIn%]", model.DueAtMoveIn);
                try { reportHTML = reportHTML.Replace("[%NumberOfVehicle%]", (Convert.ToInt32(model.NumberOfParking) > 0 ? "(" + model.NumberOfParking + " Vehicle)" : "")); } catch { reportHTML = reportHTML.Replace("[%NumberOfVehicle%]", ""); }
                

                List<IElement> elements = iText.Html2pdf.HtmlConverter.ConvertToElements(reportHTML).ToList();
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
                    pdf.SetTagged();
                    Document document = new Document(pdf);
                    document.SetMargins(0, 0, 0, 0);
                    foreach (IElement element in elements)
                    {
                        document.Add((IBlockElement)element);
                    }
                    document.Close();
                    bytes = stream.ToArray();
                }
                filename = savePath + "Quotation_" + model.TenantID + ".pdf";
                System.IO.File.WriteAllBytes(filename, bytes);
                filename = "/Content/assets/img/Document/Quotation_" + model.TenantID + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return filename;
        }
        public string PrintMonthlySummary(PrintMonthlySummary model)
        {
            string filename = "";
            try
            {

                string reportHTML = "";
                string savePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "MonthlySummary.html");

                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                reportHTML = reportHTML.Replace("[%ModelImage%]", model.ModelImage);
                reportHTML = reportHTML.Replace("[%ModelName%]", model.ModelName);
                reportHTML = reportHTML.Replace("[%UnitNo%]", model.UnitNo);
                reportHTML = reportHTML.Replace("[%LeaseTerm%]", model.LeaseTerm);
                reportHTML = reportHTML.Replace("[%MoveInDate%]", model.MoveInDate);
                reportHTML = reportHTML.Replace("[%Bedrooms%]", model.Bedrooms);
                reportHTML = reportHTML.Replace("[%Bathrooms%]", model.Bathrooms);
                reportHTML = reportHTML.Replace("[%SqFt%]", model.SqFt);
                reportHTML = reportHTML.Replace("[%Occupancy%]", model.Occupancy);
                reportHTML = reportHTML.Replace("[%Deposit%]", model.Deposit);
                reportHTML = reportHTML.Replace("[%BaseRent%]", model.BaseRent);
                reportHTML = reportHTML.Replace("[%Premium%]", model.Premium);
                reportHTML = reportHTML.Replace("[%Promotion%]", model.Promotion);
                reportHTML = reportHTML.Replace("[%Subtotal%]", model.Subtotal);
                reportHTML = reportHTML.Replace("[%PestControl%]", model.PestControl);
                reportHTML = reportHTML.Replace("[%TrashRecycle%]", model.TrashRecycle);
                reportHTML = reportHTML.Replace("[%ConvergentBillingFee%]", model.ConvergentBillingFee);
                reportHTML = reportHTML.Replace("[%AdditionalSubtotal%]", model.AdditionalSubtotal);
                reportHTML = reportHTML.Replace("[%TotalMonthlyCharges%]", model.TotalMonthlyCharges);

                List<IElement> elements = iText.Html2pdf.HtmlConverter.ConvertToElements(reportHTML).ToList();
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    PdfDocument pdf = new PdfDocument(new PdfWriter(stream));
                    pdf.SetTagged();
                    Document document = new Document(pdf);
                    document.SetMargins(0, 0, 0, 0);
                    foreach (IElement element in elements)
                    {
                        document.Add((IBlockElement)element);
                    }
                    document.Close();
                    bytes = stream.ToArray();
                }
                string fileUID = DateTime.Now.ToFileTime().ToString();
                filename = savePath + "MonthlySummary_" + fileUID + ".pdf";
                System.IO.File.WriteAllBytes(filename, bytes);
                filename = "/Content/assets/img/Document/MonthlySummary_" + fileUID + ".pdf";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return filename;
        }
        public string SendQuotationEmail(PrintQuotationModel model)
        {
            string ApplyNowQuotationNo = "";
            var filePathQuotation = HttpContext.Current.Server.MapPath(PrintQuotation(model));
            var filePathPetPloicy = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/Pet_Policies.pdf");

            string msg = "";
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                ShomaRMEntities db = new ShomaRMEntities();

                long tenantID = 0;
                try
                {
                    tenantID = Convert.ToInt64(model.TenantID);
                }
                catch { }

                if (tenantID > 0)
                {
                    var applyNowData = db.tbl_ApplyNow.Where(p => p.ID == tenantID).FirstOrDefault();
                    var appDetails = db.tbl_Login.Where(co => co.UserID == applyNowData.UserId).FirstOrDefault();
                    var getAppldata = db.tbl_Applicant.Where(p => p.UserID == applyNowData.UserId).FirstOrDefault();
                    string pass = new EncryptDecrypt().DecryptText(appDetails.Password);

                    if (appDetails != null)
                    {
                        if((appDetails.IsTempPass??1)==1)
                        {
                            appDetails.IsTempPass = 1;
                            db.SaveChanges();
                        }

                        ApplyNowQuotationNo = applyNowData.CreatedDate.HasValue ? applyNowData.CreatedDate.Value.ToString("yyyyMMddHHmm")+"-"+ applyNowData.ID.ToString() : "";
                        var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                        string payid = new EncryptDecrypt().EncryptText(getAppldata.ApplicantID.ToString() + ",4," + propertDet.AppCCCheckFees.Value.ToString("0.00"));

                        string reportCoappHTML = "";

                        string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        //reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect6.html");
                        //reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);


                        reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect.html");
                        reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);
                        reportCoappHTML = reportCoappHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Hello <b>" + appDetails.FirstName + " " + appDetails.LastName + "</b>! Your Online Application Added as per details provided by you for Sanctuary Doral. Fill your Details by clicking \"LOGIN\" link using credintials .</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">User Credentials</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">User ID :" + appDetails.Username + " </br>Password :" + pass + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Using Quotation Number : " + ApplyNowQuotationNo + ".</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Please click here for Login</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "/Accounty/login\" class=\"link-button\" target=\"_blank\">Login</a></p>";

                        reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", emailBody);

                        //reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added as Primary Applicant. Fill your Details");
                        ////reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added as per details provided by you for Sanctuary Doral. Fill your Details by clicking \"LOGIN\" link using credintials <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + appDetails.Username + " </br>Password :" + pass + "<br/><br/>OR<br/><br/>Using Quotation Number : " + ApplyNowQuotationNo + "<br/><br/>You can pay your credit check fees by clicking \"PAY NOW\" link");
                        //reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added as per details provided by you for Sanctuary Doral. Fill your Details by clicking \"LOGIN\" link using credintials <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + appDetails.Username + " </br>Password :" + pass + "<br/><br/>OR<br/><br/>Using Quotation Number : " + ApplyNowQuotationNo + "<br/>");
                        //reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", appDetails.FirstName + " " + appDetails.LastName);
                        ////reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]--><!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                        //reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                        string coappbody = reportCoappHTML;

                        List<string> filePaths = new List<string>();
                        filePaths.Add(filePathQuotation);

                        try
                        {
                            if (Convert.ToInt32(model.NumberOfPet) > 0)
                            {
                                filePaths.Add(filePathPetPloicy);
                            }
                        }
                        catch { }
                        new EmailSendModel().SendEmailWithAttachment(model.Email, "Your Application Added. Fill your Details", coappbody, filePaths);

                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
                            {
                                new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.PhoneNumber, "Hello this is Sanctuary Doral. "+ appDetails.FirstName + " "+ appDetails.LastName + " has added you as a primary applicant. Please go to "+ serverURL + "/Account/Login and signin using quotation #"+ ApplyNowQuotationNo);
                            }
                        }
                    }
                    msg = "Email send successfully";
                }
                else
                {
                    msg = "Error Occured Please Try Again";
                }
            }
            else
            {
                msg = "Email not send please verify email once";
            }
            return msg;
        }
        public string VerifyQuotationNo(string QuotationNo)
        {
            string msg = string.Empty;
            long ApplyNowId = 0;
            long ApplyNowUserId = 0;
            string ApplyNowQuotationNo = string.Empty;
            string ApplyNowEmail = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplyNowModel> lstpr = new List<ApplyNowModel>();
            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = "usp_GetSignInCredentialByQuotationNo";
                cmd.CommandType = CommandType.StoredProcedure;

                DbParameter paramCUID = cmd.CreateParameter();
                paramCUID.ParameterName = "QuotationNo";
                paramCUID.Value = QuotationNo;
                cmd.Parameters.Add(paramCUID);

                DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dtTable);
                db.Database.Connection.Close();
            }
            if (dtTable.Rows.Count > 0)
            {
                ApplyNowId = Convert.ToInt64(dtTable.Rows[0]["ID"].ToString());
                ApplyNowUserId = Convert.ToInt64(dtTable.Rows[0]["UserId"].ToString());
                ApplyNowQuotationNo = dtTable.Rows[0]["QuotationNo"].ToString();
                ApplyNowEmail = dtTable.Rows[0]["Email"].ToString();

                msg = dtTable.Rows[0]["Email"].ToString() + "|"+ dtTable.Rows[0]["IsTempPass"].ToString();
                //msg = ApplyNowEmail;

            }
            else
            {
                msg = "Quotation Number Not Found";
            }
            return msg;
        }
        public string SignInUsingQuotationNo(string QuotationNo, string UserName, string Password, int IsTempPass)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var userLogin = db.tbl_Login.Where(co => co.Username == UserName).FirstOrDefault();
            if (userLogin != null)
            {
                if (IsTempPass == 1)
                {
                    string pass = new EncryptDecrypt().EncryptText(Password.ToString());
                    userLogin.Password = pass;
                    userLogin.IsTempPass = 0;
                    db.SaveChanges();
                }
                string encryptedPassword = new EncryptDecrypt().EncryptText(Password.ToString());
                var user = db.tbl_Login.Where(p => p.Username == UserName && p.Password == encryptedPassword && p.IsActive == 1).FirstOrDefault();
                if (user != null)
                {

                    //LogError("Vijay Ramteke");
                    //SignIn(user.Username, false);
                    // Set Current User
                    var currentUser = new CurrentUser();
                    currentUser.UserID = user.UserID;
                    currentUser.Username = user.Username;
                    currentUser.FullName = user.FirstName + " " + user.LastName;
                    currentUser.EmailAddress = user.Email;
                    currentUser.IsAdmin = (user.IsSuperUser.HasValue ? user.IsSuperUser.Value : 0);
                    currentUser.EmailAddress = user.Email;
                    currentUser.UserType = Convert.ToInt32(user.UserType == null ? 0 : user.UserType);
                    currentUser.LoggedInUser = user.FirstName;
                    currentUser.TenantID = user.TenantID == 0 ? 0 : Convert.ToInt64(user.TenantID);
                    currentUser.UserType = Convert.ToInt32((user.UserType).ToString());

                    (new ShomaGroupWebSession()).SetWebSession(currentUser);
                    // Store the Log.
                    //var loginHistory = new tbl_LoginHistory
                    //{
                    //    UserID = user.UserID,
                    //    IPAddress = Request.UserHostAddress,
                    //    PageName = "Home",
                    //    LoginDateTime = DateTime.Now,
                    //    SessionID = Session.SessionID.ToString()
                    //};

                    //db.tbl_LoginHistory.Add(loginHistory);
                    //db.SaveChanges();

                    if (currentUser.TenantID == 0 && currentUser.UserType != 3 && currentUser.UserType != 33 && currentUser.UserType != 34)
                    {
                        msg = "/Admin/AdminHome";
                    }
                    else if (currentUser.TenantID != 0)
                    {
                        msg = "/Tenant/Dashboard";
                    }
                    else if (user.ParentUserID == null)
                    {
                        var checkExpiry = db.tbl_ApplyNow.Where(co => co.UserId == currentUser.UserID).FirstOrDefault();
                        checkExpiry.Status = (!string.IsNullOrWhiteSpace(checkExpiry.Status) ? checkExpiry.Status : "");
                        if ((checkExpiry.StepCompleted ?? 0) == 18 && checkExpiry.Status.Trim() == "")
                        {
                            msg = "/ApplicationStatus/Index/" + (new EncryptDecrypt().EncryptText("In Progress"));
                        }
                        else if (checkExpiry.Status.Trim() == "Approved")
                        {
                            checkExpiry.StepCompleted = 18;
                            db.SaveChanges();
                            msg = "/ApplicationStatus/Index/" + (new EncryptDecrypt().EncryptText("Approved"));
                        }
                        else if (checkExpiry.Status.Trim() == "Signed")
                        {
                            msg = "/Checklist/";
                        }
                        else
                        {
                            checkExpiry.Status = (!string.IsNullOrWhiteSpace(checkExpiry.Status) ? checkExpiry.Status : "");
                            if (checkExpiry != null)
                            {
                                DateTime expDate = Convert.ToDateTime(DateTime.Now.AddHours(-72).ToString("MM/dd/yyyy") + " 23:59:59");

                                if (checkExpiry.CreatedDate < expDate)
                                {
                                    new ApplyNowController().DeleteApplicantTenantID(checkExpiry.ID, currentUser.UserID);
                                    HttpContext.Current.Session["DelDatAll"] = "Del";
                                    msg = "/Home";
                                }
                                else
                                {
                                    HttpContext.Current.Session["DelDatAll"] = null;
                                    msg = "/ApplyNow/Index/" + currentUser.UserID; ;
                                }
                            }
                        }
                    }
                    else if (user.ParentUserID != null)
                    {
                        if (IsTempPass != 1)
                        {
                            if (currentUser.UserType == 33)
                            {
                                msg = "/ApplyNow/CoApplicantDet/" + user.ParentUserID + "-" + currentUser.UserID;
                            }
                            else if (currentUser.UserType == 34)
                            {
                                msg = "/ApplyNow/GuarantorDet/" + user.ParentUserID + "-" + currentUser.UserID;
                            }
                        }else
                        {
                            msg = "/Account/Login/";
                        }
                    }
                    // return RedirectToLocal(returnUrl);
                }
            }
            return msg;
        }
        public string CheckUserNameAndPassword(long UserID, string EmailId, string OldPassword)
        {
            string IsAllCorrect = "1";
            ShomaRMEntities db = new ShomaRMEntities();
            string pass = new EncryptDecrypt().EncryptText(OldPassword.ToString());
            var isUserCorrct = db.tbl_Login.Where(co => co.Email == EmailId && co.UserID == UserID && co.Password == pass).FirstOrDefault();
            if (isUserCorrct != null)
            {
                IsAllCorrect = "1";
            }
            else
            {
                IsAllCorrect = "0";
            }
            db.Dispose();
            return IsAllCorrect;
        }
        public bool CheckModelExist(string ModelName)
        {
            bool ismodelexists = false;
            ShomaRMEntities db = new ShomaRMEntities();
            var modelInfo = db.tbl_Models.Where(m => m.ModelName== ModelName).FirstOrDefault();
            if (modelInfo != null)
            {
                ismodelexists = true;
            }
            else
            {
                ismodelexists = false;
            }
            db.Dispose();
            return ismodelexists;
        }
    }
    public class BankCCModel
    {
        public int ID { get; set; }
        public Nullable<long> PID { get; set; }
        public string Name_On_Card { get; set; }
        public string CardNumber { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CCVNumber { get; set; }
        public Nullable<long> ProspectId { get; set; }
        public Nullable<int> PaymentMethod { get; set; }
        public Nullable<long> ApplicantID { get; set; }
        public string PaymentMethodString { get; set; }
        
        //Sachin Mahore 08 June 2020
        public List<BankCCModel> GetBankCCList(long ApplicantID)
        {
            List<BankCCModel> listBankCC = new List<BankCCModel>();
            ShomaRMEntities db = new ShomaRMEntities();

            var getBankCCList = db.tbl_OnlinePayment.Where(p => p.ApplicantID == ApplicantID).ToList();
            if (getBankCCList != null)
            {
                foreach (var bc in getBankCCList)
                {
                    BankCCModel model = new BankCCModel();
                    model.PaymentMethod = bc.PaymentMethod;
                    model.PaymentMethodString = bc.PaymentMethod == 1 ? "Bank Account" : "Credit Card";
                    model.Name_On_Card = bc.Name_On_Card;
                    string decryptedBankCC = new EncryptDecrypt().DecryptText(bc.CardNumber);
                    string decryptedMM = new EncryptDecrypt().DecryptText(bc.CardMonth);
                    string decryptedYY= new EncryptDecrypt().DecryptText(bc.CardYear);
                    model.CardNumber = decryptedBankCC;
                    model.CardMonth =decryptedMM;
                    model.CardYear = decryptedYY;
                    model.ID = bc.ID;
                    listBankCC.Add(model);
                }


            }

            return listBankCC;
        }
    }
}