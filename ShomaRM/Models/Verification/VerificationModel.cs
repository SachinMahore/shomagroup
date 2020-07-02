using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.IO;

namespace ShomaRM.Models
{
    public class VerificationModel
    {
        public long DocID { get; set; }
        public Nullable<long> ProspectusID { get; set; }
        public string Address { get; set; }
        public Nullable<int> DocumentType { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> VerificationStatus { get; set; }
        public string VerificationStatusString { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string DocumentState { get; set; }
        public string DocumentIDNumber { get; set; }
        public long TenantID { get; set; }
        public long ID { get; set; }
        public Nullable<long> PropertyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public Nullable<int> IsApplyNow { get; set; }
        public string IsApplyNowStatus { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string AnnualIncome { get; set; }
        public string AddiAnnualIncome { get; set; }
        public Nullable<decimal> Charge_Amount { get; set; }
        public int PaymentID { get; set; }
        public Nullable<long> PID { get; set; }
        public string Name_On_Card { get; set; }
        public string CardNumber { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CCVNumber { get; set; }
        public Nullable<long> ProspectId { get; set; }

        public string SaveLeaseDocumentVerifications(HttpPostedFileBase fb,long ProspectusID)
        {
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            VerificationModel model = new VerificationModel();
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/") + "/" + sysFileName;

                }
            }

            var saveLeaseDocumentVerification = db.tbl_DocumentVerification.Where(co => co.ProspectusID == ProspectusID).FirstOrDefault();

            if (saveLeaseDocumentVerification != null)
            {
                saveLeaseDocumentVerification.LeaseDocuments = sysFileName;
                db.SaveChanges();
            }
            else
            {
                var saveDocumentVerification = new tbl_DocumentVerification()
                {
                    ProspectusID = ProspectusID,
                    DocumentType = 1,
                    LeaseDocuments = sysFileName,

                    DocumentIDNumber = "LA",
                    VerificationStatus = 0,
                    //CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                    CreatedDate = DateTime.Now.Date
                };
                db.tbl_DocumentVerification.Add(saveDocumentVerification);
                db.SaveChanges();
            }

            msg = "Lease Agrrement Saved Successfully";

            db.Dispose();
            return msg;
        }
        public string SavePaymentDetails(VerificationModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.PID != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();

                var saveTransaction = new tbl_Transaction()
                {
                   
                    TenantID = Convert.ToInt64(GetProspectData.UserId),
                 
                    PAID = "7",
                    Transaction_Date = DateTime.Now,
              
                    CreatedDate = DateTime.Now,
                    Credit_Amount = 0,
                    Description = "Online Application Depsosit + First Month Charge",
                    Charge_Date = DateTime.Now,
                    Charge_Type = 16,
             
                    Charge_Amount = model.Charge_Amount,
                    Miscellaneous_Amount = 0,
                    Accounting_Date = DateTime.Now,
                
                    Batch = "1",
                
                    CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                
                    //TAccCardName = model.Name_On_Card,
                    //TAccCardNumber = model.CardNumber,
                    //TBankName = "",
                    //TRoutingNumber = "",
                    //TCardExpirationMonth = model.CardMonth.ToString(),
                    //TCardExpirationYear = model.CardYear.ToString(),
                    //TSecurityNumber = model.CCVNumber.ToString(),

                };
                db.tbl_Transaction.Add(saveTransaction);
                db.SaveChanges();
                msg = "Transaction Successfull";
            }
            else
            {
                msg = "Property Not Found";
            }
            db.Dispose();
            return msg;
        }
        public VerificationModel GetProspectData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            VerificationModel model = new VerificationModel();

            var GetProspectData = db.tbl_ApplyNow.Where(p => p.UserId == Id).FirstOrDefault();
            var GetPaymentProspectData = db.tbl_OnlinePayment.Where(p => p.ProspectId == GetProspectData.ID).FirstOrDefault();
            var GetDocumentVerificationData = db.tbl_DocumentVerification.Where(p => p.ProspectusID == GetProspectData.ID).FirstOrDefault();
            if (GetProspectData != null)
            {
                
                model.FirstName = GetProspectData.FirstName;
                model.LastName = GetProspectData.LastName;
                model.Email = GetProspectData.Email;
                model.Phone = GetProspectData.Phone.ToString();
                model.Date = GetProspectData.Date;
                model.Status = GetProspectData.Status;
                model.Address = GetProspectData.Address;
                model.IsApplyNow = GetProspectData.IsApplyNow;
                model.DateofBirth = GetProspectData.DateofBirth;
                model.AnnualIncome = GetProspectData.AnnualIncome;
                model.AddiAnnualIncome = GetProspectData.AddiAnnualIncome;
                model.IsApplyNowStatus = GetProspectData.IsApplyNow == 1 ? "Pending" : GetProspectData.IsApplyNow == 2 ? "Approved" : "Rejected";
                model.PID = GetProspectData.PropertyId;
                model.ProspectId = GetProspectData.ID;
                model.TenantID = Convert.ToInt64(GetProspectData.UserId);
                if (GetPaymentProspectData != null)
                {
                    
                    model.Name_On_Card =GetPaymentProspectData.PaymentName;
                    model.CardNumber = !string.IsNullOrWhiteSpace(GetPaymentProspectData.PaymentID)?new EncryptDecrypt().DecryptText(GetPaymentProspectData.PaymentID) :"";
                 }
                if (GetDocumentVerificationData != null)
                {
                    model.DocumentName = GetDocumentVerificationData.DocumentName;
                }
            }
            model.ID = Id;
            return model;
        }
    }
}