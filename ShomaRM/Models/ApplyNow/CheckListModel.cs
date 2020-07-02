using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Data;
using ShomaRM.Models.TwilioApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ShomaRM.Models
{
    public class CheckListModel
    {
        public long MIID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public Nullable<System.DateTime> MoveInDate { get; set; }
        public string MoveInDateTxt { get; set; }
        public string MoveInTime { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public string InsuranceDoc { get; set; }
        public string ElectricityDoc { get; set; }


        public Nullable<int> IsCheckPay { get; set; }
        public Nullable<int> IsCheckSch { get; set; }
        public Nullable<int> IsCheckIns { get; set; }
        public Nullable<int> IsCheckElc { get; set; }
        public Nullable<int> IsCheckPreSch { get; set; }

        public Nullable<int> IsCheckPO { get; set; }
        public Nullable<int> IsCheckATT { get; set; }
        public Nullable<int> IsCheckWater { get; set; }
        public Nullable<int> IsCheckSD { get; set; }

        public Nullable<System.DateTime> PreMoveInDate { get; set; }
        public string PreMoveInDateTxt { get; set; }
        public string PreMoveInTime { get; set; }
        public Nullable<int> Movers { get; set; }

        public int IsAllChecked { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }

        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        public CheckListModel GetMoveInData(long Id, long UID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CheckListModel model = new CheckListModel();
            model.MIID = 0;
            model.MoveInTime = "";
            model.PreMoveInTime = "";
            model.IsCheckATT = 0;
            model.IsCheckPO = 0;
            model.IsCheckWater = 0;
            model.IsCheckPay = 0;
            model.IsCheckSch = 0;
            model.IsCheckPreSch = 0;
            model.IsCheckIns = 0;
            model.IsCheckElc = 0;
            model.IsCheckSD = 0;
            model.IsAllChecked = 0;
            model.InsuranceDoc = "";
            model.ElectricityDoc = "";
            model.Movers = 0;
            var MoveInData = db.tbl_MoveInChecklist.Where(co => co.ProspectID == Id && co.ParentTOID==UID).FirstOrDefault();
            var isSDcheck = db.tbl_MoveInChecklist.Where(co => co.ProspectID == Id && co.IsCheckSD==1).FirstOrDefault();
            if (MoveInData != null)
            {
                model.MIID = MoveInData.MIID;
                if (MoveInData.MoveInCharges.HasValue)
                {
                    model.IsCheckPay = 1;
                }
                model.MoveInDateTxt = MoveInData.MoveInDate.HasValue ? MoveInData.MoveInDate.Value.ToString("MM/dd/yyyy") : "";
                if (MoveInData.MoveInDate.HasValue)
                {
                    model.IsCheckSch = 1;
                }
                model.MoveInTime = MoveInData.MoveInTime;

                model.PreMoveInDateTxt = MoveInData.PreMoveInDate.HasValue ? MoveInData.PreMoveInDate.Value.ToString("MM/dd/yyyy") : "";
                if (MoveInData.PreMoveInDate.HasValue)
                {
                    model.IsCheckPreSch = 1;
                }
                model.PreMoveInTime = MoveInData.PreMoveInTime;

                model.InsuranceDoc = MoveInData.InsuranceDoc;
                if (!string.IsNullOrWhiteSpace(MoveInData.InsuranceDoc))
                {
                    model.IsCheckIns = 1;
                }
                model.ElectricityDoc = MoveInData.ElectricityDoc;
                if (!string.IsNullOrWhiteSpace(MoveInData.ElectricityDoc))
                {
                    model.IsCheckElc = 1;
                }

                model.IsCheckATT = MoveInData.IsCheckATT ?? 0;
                model.IsCheckPO = MoveInData.IsCheckPO ?? 0;
                model.IsCheckWater = MoveInData.IsCheckWater ?? 0;
               
                
                model.Movers = MoveInData.Movers ?? 0;
                if (model.IsCheckPay == 1 && model.IsCheckSch == 1 && model.IsCheckPreSch == 1 && model.IsCheckIns == 1 && model.IsCheckElc == 1 && model.IsCheckATT == 1 && model.IsCheckPO == 1 && model.IsCheckWater == 1)
                {
                    model.IsAllChecked = 1;
                }
            }
            if (isSDcheck != null)
            {
                model.IsCheckSD = 1;
            }
            return model;
        }
        public string SaveMoveInCheckList(CheckListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.ProspectID != null)
            {
                var loginDet = db.tbl_MoveInChecklist.Where(p => p.ProspectID == model.ProspectID && p.ParentTOID == ShomaGroupWebSession.CurrentUser.UserID).FirstOrDefault();
                if (loginDet == null)
                {
                    var saveMoveInCheckList = new tbl_MoveInChecklist()
                    {
                        ProspectID = model.ProspectID,
                        MoveInDate = model.MoveInDate,
                        MoveInTime = model.MoveInTime,
                        PreMoveInDate = model.PreMoveInDate,
                        PreMoveInTime = model.PreMoveInTime,
                        MoveInCharges = model.MoveInCharges,
                        InsuranceDoc = model.InsuranceDoc,
                        ElectricityDoc = model.ElectricityDoc,
                        IsCheckPO = model.IsCheckPO,
                        IsCheckATT = model.IsCheckATT,
                        IsCheckWater = model.IsCheckWater,
                        IsCheckSD = model.IsCheckSD,
                        CreatedDate = DateTime.Now,
                        Movers = model.Movers,
                        ParentTOID= ShomaGroupWebSession.CurrentUser.UserID

                };
                    db.tbl_MoveInChecklist.Add(saveMoveInCheckList);
                    db.SaveChanges();
                }
                else
                {

                    loginDet.ProspectID = model.ProspectID;
                    loginDet.MoveInDate = model.MoveInDate;
                    loginDet.MoveInTime = model.MoveInTime;
                    loginDet.PreMoveInDate = model.PreMoveInDate;
                    loginDet.PreMoveInTime = model.PreMoveInTime;
                    loginDet.MoveInCharges = model.MoveInCharges;
                    loginDet.InsuranceDoc = model.InsuranceDoc;
                    loginDet.ElectricityDoc = model.ElectricityDoc;
                    loginDet.IsCheckPO = model.IsCheckPO;
                    loginDet.IsCheckATT = model.IsCheckATT;
                    loginDet.IsCheckWater = model.IsCheckWater;
                    loginDet.IsCheckSD = model.IsCheckSD;
                    loginDet.Movers = model.Movers;

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
                int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                var GetApplicantData = db.tbl_Applicant.Where(c => c.UserID==userid).FirstOrDefault();
                var GetPropertyDetails = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                var UserData = db.tbl_TenantOnline.Where(p => p.ParentTOID == userid).FirstOrDefault();
                decimal processingFees = 0;

                if(GetPropertyDetails!=null)
                {
                    processingFees = GetPropertyDetails.ProcessingFees ?? 0;
                }

                string transStatus = "";
                string pmid = "";
                string custID = "";
                long paid = 0;
                string cardaccnum = model.CardNumber.Substring(model.CardNumber.Length - 4);

                model.Email = GetApplicantData.Email;
                model.ProcessingFees = processingFees;

                if (GetApplicantData.CustID == "" || GetApplicantData.CustID == null)
                {
                    TenantOnlineModel tm = new TenantOnlineModel();
                    tm.FirstName = UserData.FirstName;
                    tm.LastName = UserData.LastName;
                    tm.Email = UserData.Email;
                    tm.HomeAddress1 = UserData.HomeAddress1;
                    tm.HomeAddress2 = UserData.HomeAddress2;
                    tm.CityHome = UserData.CityHome;
                    tm.ZipHome = UserData.ZipHome;
                    if (UserData.StateHome != 0)
                    {
                        var statename = db.tbl_State.Where(p => p.ID == UserData.StateHome).FirstOrDefault();
                        tm.StateString = statename.StateName;
                    }
                    int cid = Convert.ToInt32(UserData.Country);
                    var countdet = db.tbl_Country.Where(p => p.ID == cid).FirstOrDefault();
                    tm.CountryString = countdet.CountryName;
                    custID = new UsaePayWSDLModel().CreateUsaePayAccount(tm);
                    GetApplicantData.CustID = custID;
                    db.SaveChanges();
                }
                else
                {
                    custID = GetApplicantData.CustID;
                }
                model.CustID = custID;

                if (custID != "" || custID != null)
                {


                    if (model.IsSaveAcc == 1)
                    {

                        var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ApplicantID == model.AID && P.PaymentName == model.Name_On_Card).FirstOrDefault();

                        if (GetPayDetails == null)
                        {
                            pmid = new UsaePayWSDLModel().AddCustPaymentMethod(model);
                            var savePaymentDetails = new tbl_OnlinePayment()
                            {
                                PID = model.PID,
                                PaymentName = model.Name_On_Card,
                                PaymentID = !string.IsNullOrWhiteSpace(pmid) ? new EncryptDecrypt().EncryptText(pmid) : "",
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
                            pmid = new EncryptDecrypt().DecryptText(GetPayDetails.PaymentID);
                        }
                        if (custID != "" & pmid != "")
                        {
                            transStatus = new UsaePayWSDLModel().PayUsingCustomerNum(custID, pmid, Convert.ToDecimal(model.Charge_Amount), model.Description);
                        }
                    }
                    else
                    {
                        transStatus = new UsaePayWSDLModel().ChargeCardSoap(model);
                    }
                }

                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
           
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = Convert.ToInt64(GetProspectData.UserId),
                        PAID = paid.ToString(),
                        Transaction_Date = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + "| TransID: " + strlist[2],
                        Charge_Date = DateTime.Now,
                        Charge_Type = 2,
                        Authcode = strlist[1],
                        Charge_Amount = model.Charge_Amount,
                        Miscellaneous_Amount = processingFees,
                        Accounting_Date = DateTime.Now,
                        Batch = "1",
                        CreatedBy = Convert.ToInt32(GetProspectData.UserId),
                        UserID = Convert.ToInt32(GetProspectData.UserId),
                        RefNum = strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();
                    var TransId = saveTransaction.TransID;

                    MyTransactionModel mm = new MyTransactionModel();
                    mm.CreateTransBill(TransId, Convert.ToDecimal(((GetProspectData.Prorated_Rent)*model.MoveInPercentage)/100), "Prorated Rent");
                    //mm.CreateTransBill(TransId, Convert.ToDecimal(((GetProspectData.AdministrationFee) * model.MoveInPercentage) / 100), "Administration Fee");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(((GetProspectData.Deposit) * model.MoveInPercentage) / 100), "Security Deposit");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(((GetProspectData.PetDeposit) * model.MoveInPercentage) / 100), "Pet Deposit");
                    mm.CreateTransBill(TransId, Convert.ToDecimal(((GetProspectData.VehicleRegistration) * model.MoveInPercentage) / 100), "Vehicle Registration Charges");

                    //Save  Data In tbl_MoveInChecklist//
                    CheckListModel modelCheckList = new CheckListModel();
                    modelCheckList.ProspectID = model.ProspectId;
                    modelCheckList.MoveInCharges = model.Charge_Amount;

                    string result = (new CheckListModel().SaveMoveInCheckList(modelCheckList));
                    //Save  Data In tbl_MoveInChecklist//

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");

                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                   
                    if (model != null)
                    {
                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + GetProspectData.FirstName + " " + GetProspectData.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
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

                        //sachin m 01 may 2020
                        
                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Completed and Move In Charges Received");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Application Completed and Move In Charges Received.  This email confirms that we have received your Move In Charges payment.  Please save this email for your personal records.  Your application is being processed, and we will soon contact you with your next step.  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;PAYMENT INFORMATION: </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment confirmation number: #" + strlist[1] + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Date : " + DateTime.Now + " </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Payment Amount: $" + model.Charge_Amount + "  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; For your convenience, we have attached a copy of your signed application together with the Terms and Conditions and Policies and Procedures for your review.  Please save these documents for your records. </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; If you need to edit your online application, kindly contact us, and we will be happy to assist you.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;You are just steps away from signing your lease and moving in to the home of your dreams.” </p><p style='font-size: 14px;font-style:italic; line-height: 21px; text-align: justify; margin: 0;'><br/><br/>*Application fees are non-refundable, even if the application is denied, except to the extent otherwise required by applicable law. </p>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", GetProspectData.FirstName + " " + GetProspectData.LastName);
                        //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetProspectData.Email);
                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(GetProspectData.Email, "Sanctuary Payment Confirmation", body);
                    string message = "Sanctuary Payment Confirmation. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(GetApplicantData.Phone))
                        {
                            new TwilioService().SMS(GetApplicantData.Phone, message);
                        }
                    }
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
        public string GetProcessingFees()
        {
            string processingFees = "0.00";
            ShomaRMEntities db = new ShomaRMEntities();
            var propertyDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
            if (propertyDet != null)
            {
                processingFees = (propertyDet.ProcessingFees ?? 0).ToString("0.00");

            }
            db.Dispose();
            return processingFees;
        }
        public List<ESignatureKeysModel> GetSignedList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ESignatureKeysModel> lstpr = new List<ESignatureKeysModel>();
            List<ESignatureKeysModel> lstTransaction = new List<ESignatureKeysModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetSignedList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ESignatureKeysModel pr = new ESignatureKeysModel();
                    DateTime? createdDateString = null;
                    try
                    {
                        createdDateString = Convert.ToDateTime(dr["DateSigned"].ToString());
                    }
                    catch
                    {

                    }
                    pr.ESID = Convert.ToInt32(dr["ESID"].ToString());
                    pr.DateSigned = createdDateString == null ? "" : createdDateString.ToString();
                    pr.ApplicantName = dr["ApplicantName"].ToString();
                    pr.Email = dr["Email"].ToString();
                    pr.Key = dr["Key"].ToString();
                    pr.ApplicantID = Convert.ToInt64(dr["ApplicantID"].ToString());
                    pr.IsSigned = createdDateString == null ? 0 : 1;
                    pr.IsSignedAll = Convert.ToInt32(dr["IsSignedAll"].ToString());
                    pr.IsLeaseExecuted = Convert.ToInt32(dr["IsLeaseExecuted"].ToString());
                    lstpr.Add(pr);
                }
                db.Dispose();
                return lstpr.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public string UploadInsurenceDocAdminSide(HttpPostedFileBase fileBaseUploadInsurenceDoc, long ProspectId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
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
                sysFileName = fileName + ProspectId + Path.GetExtension(fileBaseUploadInsurenceDoc.FileName);
                fileBaseUploadInsurenceDoc.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUploadInsurenceDoc.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/ChecklistDocument/") + "/" + sysFileName;

                }

                var moveInDet = db.tbl_MoveInChecklist.Where(p => p.ProspectID == ProspectId).FirstOrDefault();
                if (moveInDet == null)
                {
                    var saveMoveInCheckList = new tbl_MoveInChecklist()
                    {
                        ProspectID = ProspectId,
                        InsuranceDoc = sysFileName.ToString(),
                    };
                    db.tbl_MoveInChecklist.Add(saveMoveInCheckList);
                    db.SaveChanges();
                }
                else
                {
                    moveInDet.InsuranceDoc = sysFileName.ToString();
                    db.SaveChanges();
                }
                msg = "File Uploaded Successfully |" + sysFileName.ToString();
            }
            else
            {
                msg = "Something went wrong file not Uploaded";
            }
            return msg;
        }
        public string UploadProofOfElectricityDocAdminSide(HttpPostedFileBase fileBaseUploadProofOfElectricityDoc, long ProspectId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();

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
                sysFileName = fileName + ProspectId + Path.GetExtension(fileBaseUploadProofOfElectricityDoc.FileName);
                fileBaseUploadProofOfElectricityDoc.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUploadProofOfElectricityDoc.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/ChecklistDocument/") + "/" + sysFileName;

                }

                var moveInDet = db.tbl_MoveInChecklist.Where(p => p.ProspectID == ProspectId).FirstOrDefault();
                if (moveInDet == null)
                {
                    var saveMoveInCheckList = new tbl_MoveInChecklist()
                    {
                        ProspectID = ProspectId,
                        ElectricityDoc = sysFileName.ToString(),
                    };
                    db.tbl_MoveInChecklist.Add(saveMoveInCheckList);
                    db.SaveChanges();
                }
                else
                {
                    moveInDet.ElectricityDoc = sysFileName.ToString();
                    db.SaveChanges();
                }
                msg = "File Uploaded Successfully |" + sysFileName.ToString();
            }
            else
            {
                msg = "Something went wrong file not Uploaded|0";
            }
            return msg;
        }
    }
    public partial class ESignatureKeysModel
    {
        public long ESID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<long> ApplicantID { get; set; }
        public string Key { get; set; }
        public Nullable<long> EsignatureId { get; set; }
        public string DateSigned { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public int IsSigned { get; set; }
        public int IsSignedAll { get; set; }
        public int IsLeaseExecuted { get; set; }
    }
}