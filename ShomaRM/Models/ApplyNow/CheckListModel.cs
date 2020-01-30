using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShomaRM.Models
{
    public class CheckListModel
    {
        public long MIID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public Nullable<System.DateTime> MoveInDate { get; set; }
        public string MoveInTime { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public string InsuranceDoc { get; set; }
        public string ElectricityDoc { get; set; }
        public Nullable<int> IsCheckPO { get; set; }
        public Nullable<int> IsCheckATT { get; set; }
        public Nullable<int> IsCheckWater { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string SaveMoveInCheckList(CheckListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
           
            if (model.ProspectID != null)
            {
                var loginDet = db.tbl_MoveInChecklist.Where(p => p.ProspectID == model.ProspectID).FirstOrDefault();
                if (loginDet == null)
                {
                    var saveMoveInCheckList = new tbl_MoveInChecklist()
                    {
                       
                        ProspectID = model.ProspectID,
                        MoveInDate = model.MoveInDate,
                        MoveInTime = model.MoveInTime,
                        MoveInCharges = model.MoveInCharges,
                        InsuranceDoc = model.InsuranceDoc,
                        ElectricityDoc = model.ElectricityDoc,
                        IsCheckPO = model.IsCheckPO,
                        IsCheckATT = model.IsCheckATT,
                        IsCheckWater = model.IsCheckWater,
                         CreatedDate = DateTime.Now,
                    };
                    db.tbl_MoveInChecklist.Add(saveMoveInCheckList);
                    db.SaveChanges();
                  

                }
               
            }
            msg = "Move In Check List Save Successfully";

           
            return msg;
        }

        public CheckListModel UploadInsurenceDoc(HttpPostedFileBase fileBaseUploadInsurenceDoc, CheckListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CheckListModel checklistModelIns = new CheckListModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUploadInsurenceDoc != null && fileBaseUploadInsurenceDoc.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/ChecklistDocument/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = Path.GetFileNameWithoutExtension(fileBaseUploadInsurenceDoc.FileName);
                    Extension = Path.GetExtension(fileBaseUploadInsurenceDoc.FileName);
                    sysFileName = fileName + model.ProspectID + Path.GetExtension(fileBaseUploadInsurenceDoc.FileName);
                    fileBaseUploadInsurenceDoc.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUploadInsurenceDoc.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/ChecklistDocument/") + "/" + sysFileName;

                    }
                    checklistModelIns.InsuranceDoc = sysFileName.ToString();
                }

            }
            return checklistModelIns;
        }

        public CheckListModel UploadProofOfElectricityDoc(HttpPostedFileBase fileBaseUploadProofOfElectricityDoc, CheckListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CheckListModel checklistModel = new CheckListModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUploadProofOfElectricityDoc != null && fileBaseUploadProofOfElectricityDoc.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/ChecklistDocument/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = Path.GetFileNameWithoutExtension(fileBaseUploadProofOfElectricityDoc.FileName);
                    Extension = Path.GetExtension(fileBaseUploadProofOfElectricityDoc.FileName);
                    sysFileName = fileName + model.ProspectID + Path.GetExtension(fileBaseUploadProofOfElectricityDoc.FileName);
                    fileBaseUploadProofOfElectricityDoc.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUploadProofOfElectricityDoc.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/ChecklistDocument/") + "/" + sysFileName;

                    }
                    checklistModel.ElectricityDoc = sysFileName.ToString();
                }

            }
            return checklistModel;
        }
        public string SaveMoveInPayment(ApplyNowModel model)
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
                if (strlist[1] != "000000")
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
                        Description = model.Description + "| TransID: " + Convert.ToInt32(strlist[1]),
                        Charge_Date = DateTime.Now,
                        Charge_Type = 2,
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
                    mm.CreateTransBill(TransId, Convert.ToDecimal(GetProspectData.Prorated_Rent), "Prorated Rent");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(GetProspectData.AdministrationFee), "Administration Fee");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(GetProspectData.Deposit), "Security Deposit");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(GetProspectData.PetDeposit), "Pet Deposit");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(GetProspectData.VehicleRegistration), "Vehicle Registration Charges");
                   


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


    }
}