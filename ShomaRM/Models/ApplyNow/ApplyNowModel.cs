﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Models.TwilioApi;
using System.Web.Configuration;

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
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];

        public string SavePaymentDetails(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.PID != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId).FirstOrDefault();

                string encrytpedCardNumber = new EncryptDecrypt().EncryptText(model.CardNumber);
                string encrytpedCardMonth = new EncryptDecrypt().EncryptText(model.CardMonth);
                string encrytpedCardYear = new EncryptDecrypt().EncryptText(model.CardYear);
                string encrytpedRoutingNumber = new EncryptDecrypt().EncryptText(model.CCVNumber);

                string decryptedPayemntCardNumber = new EncryptDecrypt().DecryptText(encrytpedCardNumber);

                string transStatus = "";
                model.Email = GetProspectData.Email;
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
                    var coappliList = db.tbl_Applicant.Where(pp => pp.TenantID == model.ProspectId && pp.Type != "Co-Applicant" ).ToList();
                    if(coappliList!=null)
                    {
                        foreach(var coapp in coappliList)
                        {
                            coapp.Paid = 1;
                            db.SaveChanges();
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

                        TenantID = Convert.ToInt64(GetProspectData.UserId),
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
                        Miscellaneous_Amount =Convert.ToDecimal("3.95"),
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
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    string message = "";
                    string phonenumber = GetProspectData.Phone;
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
        public string saveCoAppPayment(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.ProspectId != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                // var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId).FirstOrDefault();
                var GetCoappDet = db.tbl_Applicant.Where(c => c.ApplicantID == model.AID).FirstOrDefault();              
               
                string transStatus = "";
                model.Email = GetCoappDet.Email;
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
                if (strlist[1] != "000000")
                {
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
                        Miscellaneous_Amount = Convert.ToDecimal("3.95"),
                        Accounting_Date = DateTime.Now,

                        Batch = model.AID.ToString(),
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
                    db.SaveChanges();

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    string message = "";
                    string phonenumber = GetCoappDet.Phone;
                    if (model != null)
                    {
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Online Application Fees Payment Received");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                        reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", GetCoappDet.Email);

                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(GetCoappDet.Email, "Online Application Fees Payment Received", body);
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
        public ApplyNowModel ForgetPassword(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            var forgetPassword = db.tbl_Login.Where(co => co.Username == model.Email).FirstOrDefault();
            string message = "";
            string phonenumber = "";
            if (forgetPassword != null)
            {
                model.Password = forgetPassword.Password;
                model.FullName = forgetPassword.FirstName + " " + forgetPassword.LastName;
            }
            else
            {
                model.Message = "Invalid UserName";
            }
            db.Dispose();
            if (model.Message != "Invalid UserName")
            {
                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                reportHTML = System.IO.File.ReadAllText(filePath + "ForgetPassword.html");

                //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                reportHTML = reportHTML.Replace("[%TenantName%]", model.FullName);
                reportHTML = reportHTML.Replace("[%TenantPassword%]", model.Password);

                string body = reportHTML;
                new EmailSendModel().SendEmail(model.Email, "Password", body);

                message = "Your credentials has been sent to your email. Please check the email for detail.";
                if (SendMessage == "yes")
                {
                    new TwilioService().SMS(phonenumber, message);
                }
            }
            return model;
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
            int stepcomp = 0;
            stepcomp = onlineProspectData.StepCompleted ?? 0;
            if (stepcomp < StepCompleted)
            {
                stepcomp = StepCompleted;
            }

            if (onlineProspectData != null)
            {
                onlineProspectData.StepCompleted = stepcomp;
                db.SaveChanges();
            }
            return "1";
        }
    }
}