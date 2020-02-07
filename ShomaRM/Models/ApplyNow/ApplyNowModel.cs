using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Models
{
    public class ApplyNowModel
    {
        public int ID { get; set; }
        public Nullable<long> PID { get; set; }
        public string Name_On_Card { get; set; }
        public string CardNumber { get; set; }
        public Nullable<int> CardMonth { get; set; }
        public Nullable<int> CardYear { get; set; }
        public Nullable<int> CCVNumber { get; set; }
        public Nullable<long> ProspectId { get; set; }
        public Nullable<decimal> Charge_Amount { get; set; }
        public Nullable<decimal> Credit_Amount { get; set; }
        
        public string Transaction_Type { get; set; }
        public string Description { get; set; }
        public string GL_Trans_Description { get; set; }
        public string RoutingNumber { get; set; }
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

        public string SavePaymentDetails(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.PID != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ProspectId == model.ProspectId).FirstOrDefault();
                if (GetPayDetails != null)
                {
                    GetPayDetails.Name_On_Card = model.Name_On_Card;
                    GetPayDetails.CardNumber = model.CardNumber;
                    GetPayDetails.CardMonth = model.CardMonth;
                    GetPayDetails.CardYear = model.CardYear;
                    GetPayDetails.CCVNumber = model.CCVNumber;
                    GetPayDetails.ProspectId = model.ProspectId;
                    GetPayDetails.PaymentMethod = model.PaymentMethod;
                    
                    db.SaveChanges();

                }
                else
                {
                    var savePaymentDetails = new tbl_OnlinePayment()
                    {
                        PID = model.PID,
                        Name_On_Card = model.Name_On_Card,
                        CardNumber = model.CardNumber,
                        CardMonth = model.CardMonth,
                        CardYear = model.CardYear,
                        CCVNumber = model.CCVNumber,
                        ProspectId = model.ProspectId,
                        PaymentMethod = model.PaymentMethod,
                    };
                    db.tbl_OnlinePayment.Add(savePaymentDetails);
                    db.SaveChanges();
                }


                string transStatus = new UsaePayModel().ChargeCard(model);
                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if(strlist[1]!="000000")
                {
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
                        Description = model.Description +"| TransID: "+ Convert.ToInt32(strlist[1]),
                        Charge_Date = DateTime.Now,
                        Charge_Type = 1,
                        Payment_ID = Convert.ToInt32(strlist[1]),
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = 0,
                        Accounting_Date = DateTime.Now,
                        Journal = 0,
                        Accrual_Debit_Acct = "400-5000-10500",
                        Accrual_Credit_Acct = "400-5000-40030",
                        Cash_Debit_Account = "400-5100-10011",
                        Cash_Credit_Account = "400-5100-40085",
                        Appl_of_Origin = "SRM",
                        Batch = "1",
                        Batch_Source = "",
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        GL_Trans_Reference_1 = model.PID.ToString(),
                        GL_Trans_Reference_2 = GetProspectData.FirstName + " " + GetProspectData.LastName,
                        GL_Entries_Created = 1,
                        GL_Trans_Description = transStatus.ToString(),
                        ProspectID = 0,
                        TAccCardName = model.Name_On_Card,
                        TAccCardNumber = model.CardNumber,
                        TBankName = model.BankName,
                        TRoutingNumber = model.RoutingNumber,
                        TCardExpirationMonth = model.CardMonth.ToString(),
                        TCardExpirationYear = model.CardYear.ToString(),
                        TSecurityNumber = model.CCVNumber.ToString(),

                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();

                    var TransId = saveTransaction.TransID;
                    MyTransactionModel mm = new MyTransactionModel();
                    mm.CreateTransBill(TransId, Convert.ToDecimal(model.Charge_Amount), model.Description);

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    if (model != null)
                    {
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Payment Received");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Thank you for signing and submitting your application.  This email confirms that we have received your online application fees payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");

                        reportHTML = reportHTML.Replace("[%TenantName%]", GetProspectData.FirstName + " " + GetProspectData.LastName);

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", GetProspectData.Email);

                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(GetProspectData.Email, "Application Completed and Payment Received", body);
                }
               
                msg = transStatus.ToString();
            }
            else
            {
                msg = "Property Not Found";
            }
            db.Dispose();
            return msg;
        }
        public ApplyNowModel ForgetPassword(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            var forgetPassword = db.tbl_Login.Where(co => co.Username == model.Email).FirstOrDefault();
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

    }
}