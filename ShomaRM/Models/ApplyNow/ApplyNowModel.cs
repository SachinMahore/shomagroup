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
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId).FirstOrDefault();
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
                model.Email = GetProspectData.Email;
                model.ProcessingFees = processingFees;
                if (model.PaymentMethod == 2)
                {

                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (model.PaymentMethod == 1)
                {

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
                                coappliList.Paid = 1;
                                db.SaveChanges();

                            }
                        }
                    }
                    if (GetPayDetails != null)
                    {
                        GetPayDetails.Name_On_Card = model.Name_On_Card;
                        GetPayDetails.CardNumber = encrytpedCardNumber;
                        GetPayDetails.CardMonth = encrytpedCardMonth;
                        GetPayDetails.CardYear = encrytpedCardYear;
                        GetPayDetails.CCVNumber = encrytpedRoutingNumber;
                        GetPayDetails.ProspectId = model.ProspectId;
                        GetPayDetails.PaymentMethod = model.PaymentMethod;
                        GetPayDetails.ApplicantID = 0;
                        db.SaveChanges();

                    }
                    else
                    {
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
                            ApplicantID = 0,
                    };
                        db.tbl_OnlinePayment.Add(savePaymentDetails);
                        db.SaveChanges();
                    }
                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = userid,
                        Revision_Num = 1,
                        Transaction_Type = "1",
                        Transaction_Date = DateTime.Now,
                        Run = 1,
                        LeaseID = 0,
                        Reference = "PID" + model.ProspectId,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[1],
                        Charge_Date = DateTime.Now,
                        Charge_Type = 1,

                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,

                        Batch = "1",
                        Batch_Source = "",
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        GL_Trans_Description = transStatus.ToString(),
                        ProspectID = 0,
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
                    if (model != null)
                    {
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Payment Received");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                        reportHTML = reportHTML.Replace("[%TenantName%]", GetProspectData.FirstName + " " + GetProspectData.LastName);

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", GetProspectData.Email);

                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(GetProspectData.Email, "Application Completed and Payment Received", body);
                    message = "Online Application Completed and Payment of $"+ model.Charge_Amount + " Received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
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
                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId).FirstOrDefault();
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

                string transStatus = "";
                model.Email = GetProspectData.Email;
                model.ProcessingFees = processingFees;
                if (model.PaymentMethod == 2)
                {

                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (model.PaymentMethod == 1)
                {

                    transStatus = new UsaePayModel().ChargeACH(model);
                }

                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    //Added by Sachin M 29 Apr 5:16PM
                   
                    if (GetPayDetails != null)
                    {
                        GetPayDetails.Name_On_Card = model.Name_On_Card;
                        GetPayDetails.CardNumber = encrytpedCardNumber;
                        GetPayDetails.CardMonth = encrytpedCardMonth;
                        GetPayDetails.CardYear = encrytpedCardYear;
                        GetPayDetails.CCVNumber = encrytpedRoutingNumber;
                        GetPayDetails.ProspectId = model.ProspectId;
                        GetPayDetails.PaymentMethod = model.PaymentMethod;
                        GetPayDetails.ApplicantID = 0;
                        db.SaveChanges();

                    }
                    else
                    {
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
                    }
                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = userid,
                        Revision_Num = 1,
                        Transaction_Type = "1",
                        Transaction_Date = DateTime.Now,
                        Run = 1,
                        LeaseID = 0,
                        Reference = "PID" + model.ProspectId,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[1],
                        Charge_Date = DateTime.Now,
                        Charge_Type = 1,

                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,

                        Batch = "1",
                        Batch_Source = "",
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        GL_Trans_Description = transStatus.ToString(),
                        ProspectID = 0,
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
                    string message = "";
                    string phonenumber = coappliList.Phone;
                    if (model != null)
                    {
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Payment Received");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                        reportHTML = reportHTML.Replace("[%TenantName%]", coappliList.FirstName + " " + coappliList.LastName);

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", coappliList.Email);

                    }
                    string body = reportHTML;

                    new EmailSendModel().SendEmail(coappliList.Email, "Application Completed and Payment Received", body);
                    message = "Online Application Completed and Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
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

        public string saveCoAppPayment(ApplyNowModel model)
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

                decimal processingFees = 0;

                if(GePropertyData!=null)
                {
                    processingFees = GePropertyData.ProcessingFees ?? 0;
                }

                string transStatus = "";
                model.Email = GetCoappDet.Email;
                model.ProcessingFees = processingFees;
                if (model.PaymentMethod == 2)
                {

                    transStatus = new UsaePayModel().ChargeCard(model);
                }
                else if (model.PaymentMethod == 1)
                {
                    model.AccountNumber = model.CardNumber;
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
                        long paid = 0;
                    var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId && P.ApplicantID == model.AID).FirstOrDefault();
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
                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = Convert.ToInt64(GetProspectData.UserId),
                        Revision_Num = 1,
                        Transaction_Type = paid.ToString(),
                        Transaction_Date = DateTime.Now,
                        Run = 1,
                        LeaseID = 0,
                        Reference = "PID" + model.ProspectId,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[1],
                        Charge_Date = DateTime.Now,
                        Charge_Type = model.Charge_Type,

                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,

                        Batch = bat,
                        Batch_Source = "",
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                     
                        GL_Trans_Description = transStatus.ToString(),
                        ProspectID = 0,
                        

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
                    string message = "";
                    string phonenumber = GetCoappDet.Phone;
                    string sub = "";
                    if (model != null)
                    {
                        if (model.FromAcc == 1 || model.FromAcc == 2)
                        {
                            GetProspectData.IsApplyNow = 2;
                            db.SaveChanges();

                            sub = "Online Application Fees Payment Received";
                            reportHTML = reportHTML.Replace("[%EmailHeader%]", "Online Application Fees Payment Received");
                            reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                            reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);

                            reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            //sachin m 01 may 2020
                            string body = reportHTML;
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Online Application Completed and Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                        }
                       else if (model.FromAcc == 4)
                        {

                            GetCoappDet.CreditPaid = 1;
                            db.SaveChanges();

                            //sachin m 01 may 2020
                            sub = "Credit Check Fees Payment Received";
                            reportHTML = reportHTML.Replace("[%EmailHeader%]", "Credit Check Fees Payment Received");
                            reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your Credit Check fees payment.  Please save this email for your personal records.  Your application is being processed for background check, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                            reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            string body = reportHTML;
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Credit Check Fees Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                            string pass = "";
                            pass = new EncryptDecrypt().DecryptText(UserData.Password);

                            // Call Background check function
                            string reportHTMLbc = "";
                            reportHTMLbc = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect5.html");
                            reportHTMLbc = reportHTMLbc.Replace("[%ServerURL%]", serverURL);
                            sub = "Background Process Approved and Complete Online Application";
                            reportHTMLbc = reportHTMLbc.Replace("[%EmailHeader%]", "Background Process Approved and Complete Online Application");
                            reportHTMLbc = reportHTMLbc.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Congratulation! Your application for credit check is approved. Please complete your Online Application by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br></p>");
                            reportHTMLbc = reportHTMLbc.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            reportHTMLbc = reportHTMLbc.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            reportHTMLbc = reportHTMLbc.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                            string bodybc = reportHTMLbc;
                            new EmailSendModel().SendEmail(GetCoappDet.Email, "Background Process Approved", bodybc);
                            message = "Credit Process Approved. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                        }
                        else
                        {
                            GetProspectData.IsApplyNow = 2;
                            db.SaveChanges();

                            sub = "Administration Fees Payment Received";
                            reportHTML = reportHTML.Replace("[%EmailHeader%]", "Administration Fees Payment Received");
                            reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p>");
                            reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                            reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);
                            //sachin m 01 may 2020
                            string body = reportHTML;
                            new EmailSendModel().SendEmail(GetCoappDet.Email, sub, body);
                            message = "Administration Fees Payment of $" + model.Charge_Amount + " Received. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                new TwilioService().SMS(phonenumber, message);
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
                string uidd = new EncryptDecrypt().EncryptText(forgetPassword.UserID.ToString());
                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "ForgetPassword.html");

                reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);

                reportHTML = reportHTML.Replace("[%TenantName%]", model.FullName);
                reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/ApplyNow/ChangePassword/?uid=" + uidd + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/ApplyNow/ChangePassword/?uid=" + uidd + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Change Password</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                string body = reportHTML;
                new EmailSendModel().SendEmail(model.Email, "Reset Password Link", body);

                message = "Your password change link has been sent to your email. Please check the email for detail.";
                if (SendMessage == "yes")
                {
                    new TwilioService().SMS(phonenumber, message);
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
        public string  SaveUpdateStep(long ID, int StepCompleted)
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
            if(StepCompleted==18)
            {
                if(applicantData.Type!= "Primary Applicant ")
                {
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");

                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);

                    string phonenumber = onlineProspectData.Phone;
                    if (tenantData != null)
                    {
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", applicantData.Type+ " "+ applicantData.FirstName + " " + applicantData.LastName + " has Finished the application");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'></br>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; "+ applicantData.Type +" "+ applicantData.FirstName + " " + applicantData.LastName + " has finished the application on " + DateTime.Now + "</p>");

                        reportHTML = reportHTML.Replace("[%TenantName%]", onlineProspectData.FirstName + " " + onlineProspectData.LastName);

                        reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");

                        message =applicantData.Type+ " " + applicantData.FirstName + " " + applicantData.LastName + " has completed the application";
                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(onlineProspectData.Email, applicantData.Type + " " + applicantData.FirstName + " " + applicantData.LastName + " has completed the application", body);
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
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
                reportHTML = reportHTML.Replace("[%GuarantorFees%]", model.GuarantorFees);
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
        public string SendQuotationEmail(string Email)
        {
            string msg = string.Empty;
            if (!string.IsNullOrWhiteSpace(Email))
            {
                ShomaRMEntities db = new ShomaRMEntities();
                var appDetails = db.tbl_Login.Where(co => co.Email == Email).FirstOrDefault();
                string pass = new EncryptDecrypt().DecryptText(appDetails.Password);
                if (appDetails != null)
                {

                    string reportCoappHTML = "";

                    string coappfilePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportCoappHTML = System.IO.File.ReadAllText(coappfilePath + "EmailTemplateProspect6.html");

                    reportCoappHTML = reportCoappHTML.Replace("[%ServerURL%]", serverURL);

                    //reportCoappHTML = reportCoappHTML.Replace("[%CoAppType%]", $"{appDetails.FirstName} {appDetails.LastName}");
                    reportCoappHTML = reportCoappHTML.Replace("[%EmailHeader%]", "Your Application Added as Primary Applicant. Fill your Details");
                    reportCoappHTML = reportCoappHTML.Replace("[%EmailBody%]", "Your Online Application Added as per details provided by you for Sanctuary Doral. Fill your Details by clicking below link <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + appDetails.Username + " </br>Password :" + pass);
                    reportCoappHTML = reportCoappHTML.Replace("[%TenantName%]", $"{appDetails.FirstName} {appDetails.LastName}");
                    reportCoappHTML = reportCoappHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Login</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                    string coappbody = reportCoappHTML;
                    new EmailSendModel().SendEmail(Email, "Your Application Added. Fill your Details", coappbody);

                    //if (SendMessage == "yes")
                    //{
                    //    new ShomaRM.Models.TwilioApi.TwilioService().SMS(model.Phone, "Your Application Added. Fill your Details. Credentials has been sent on your email. Please check the email for detail.");
                    //}
                }
                msg = "Email send successfully";
            }
            else
            {
                msg = "Email not send please verify email once";
            }
            return msg;
        }
    }
}