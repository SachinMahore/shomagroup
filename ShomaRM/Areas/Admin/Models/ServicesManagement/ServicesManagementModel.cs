using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ShomaRM.Models.TwilioApi;
using ShomaRM.Models;

namespace ShomaRM.Areas.Admin.Models
{
    public class ServicesManagementModel
    {
        public long ServiceID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<int> ProblemCategory { get; set; }
        public string Details { get; set; }
        public Nullable<int> PermissionEnterApartment { get; set; }
        public Nullable<System.DateTime> PermissionComeDate { get; set; }
        public Nullable<int> PetInforChange { get; set; }
        public Nullable<int> AlarmCodeChange { get; set; }
        public string Notes { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Phone { get; set; }
        public string EmergencyPhone { get; set; }
        public string Email { get; set; }
        public Nullable<int> Status { get; set; }
        public string DateString { get; set; }
        public string StatusString { get; set; }
        public string ProblemCategorystring { get; set; }
        public int ServiceRequest { get; set; }
        public Nullable<int> Location { get; set; }
        public Nullable<int> ItemCaussing { get; set; }
        public Nullable<int> ItemIssue { get; set; }
        public string OtherItemCaussing { get; set; }
        public string OtherItemIssue { get; set; }
        public string ProblemCategoryName { get; set; }
        public string PermissionComeDateString { get; set; }
        public string PermissionComeTime { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public string PriorityString { get; set; }
        public string OriginalServiceFile { get; set; }
        public string TempServiceFile { get; set; }
        public int LocationId { get; set; }
        public string LocationString { get; set; }
        public int ServiceIssueID { get; set; }
        public string ServiceIssueString { get; set; }
        public string EmergencyMobile { get; set; }
        public string TenantName { get; set; }
        public long CausingIssueID { get; set; }
        public string CausingIssue { get; set; }
        public long IssueID { get; set; }
        public string Issue { get; set; }
        public string CompletedPicture { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public string TempCompletedPicture { get; set; }
        public Nullable<int> ServicePerson { get; set; }
        public string ServicePersonString { get; set; }
        public Nullable<int> UrgentStatus { get; set; }
        public string ClosingNotes { get; set; }
        public string TaskNotes { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public string ClosingDatestring { get; set; }
        public string Project { get; set; }
        public string MoveIndate { get; set; }
        public string OwnerSignature { get; set; }
        public string TempOwnerSignature { get; set; }
        public string MStartDateString { get; set; }
        public string MLeaseEndDateString { get; set; }
        public Nullable<int> WarrantyStatus { get; set; }

        public string EventName { get; set; }
        public string EventDate { get; set; }
        public string UserName { get; set; }
        public string AuditDetail { get; set; }


        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string ServerURL = WebConfigurationManager.AppSettings["ServerURL"];

        public int BuildPaganationUserList(ServicesSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServicePaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
                    cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "Piority";
                    param3.Value = model.Priority;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "Statue";
                    param4.Value = model.Status;
                    cmd.Parameters.Add(param4);


                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "UserID";
                    param5.Value = userid;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "Criteria";
                    param6.Value = model.Criteria;
                    cmd.Parameters.Add(param6);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramsb= cmd.CreateParameter();
                    paramsb.ParameterName = "SortBy";
                    paramsb.Value = model.SortBy;
                    cmd.Parameters.Add(paramsb);

                    DbParameter paramOb = cmd.CreateParameter();
                    paramOb.ParameterName = "OrderBy";
                    paramOb.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOb);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                if (dtTable.Rows.Count > 0)
                {
                    NOP = int.Parse(dtTable.Rows[0]["NumberOfPages"].ToString());
                }
                db.Dispose();
                return NOP;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<ServicesSearchModel> FillServicesSearchGrid(ServicesSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            List<ServicesSearchModel> lstUser = new List<ServicesSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServicePaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
                    cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "Piority";
                    param3.Value = model.Priority;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "Statue";
                    param4.Value = model.Status;
                    cmd.Parameters.Add(param4);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "UserID";
                    param5.Value = userid;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "Criteria";
                    param6.Value = model.Criteria;
                    cmd.Parameters.Add(param6);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramsb = cmd.CreateParameter();
                    paramsb.ParameterName = "SortBy";
                    paramsb.Value = model.SortBy;
                    cmd.Parameters.Add(paramsb);

                    DbParameter paramOb = cmd.CreateParameter();
                    paramOb.ParameterName = "OrderBy";
                    paramOb.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOb);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ServicesSearchModel searchmodel = new ServicesSearchModel();
                    searchmodel.ServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                    searchmodel.TenantName = dr["TenantName"].ToString();
                    searchmodel.Problemcategory = dr["ProblemCategory"].ToString();
                    searchmodel.ItemCaussing = dr["ItemCaussing"].ToString();
                    searchmodel.ItemIssue = dr["ItemIssue"].ToString();
                    searchmodel.PriorityString = dr["Priority"].ToString();
                    searchmodel.StatusString = dr["Status"].ToString();
                    searchmodel.Location = dr["Location"].ToString();
                    searchmodel.EmergencyMobile = dr["EmergencyMobile"].ToString();
                    searchmodel.Notes = dr["Notes"].ToString();
                    searchmodel.OtherItemCaussing = dr["OtherItemCaussing"].ToString();
                    searchmodel.OtherItemIssue = dr["OtherItemIssue"].ToString();
                    searchmodel.UnitNo = dr["UnitNo"].ToString(); 
                    lstUser.Add(searchmodel);
                }
                db.Dispose();
                return lstUser.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }


        public ServicesManagementModel goToServiceDetails(long ServiceID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServicesManagementModel pr = new ServicesManagementModel();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServiceInfo";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "ServiceID";
                    paramF.Value = ServiceID;
                    cmd.Parameters.Add(paramF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    pr.ServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                    pr.TenantName = dr["TenantName"].ToString();
                    pr.ProblemCategorystring = dr["ProblemCategory"].ToString();
                    pr.PermissionComeDateString = dr["PermissionComeDateString"].ToString();
                    pr.PermissionComeTime = dr["PermissionComeTime"].ToString();
                    pr.Details = dr["Details"].ToString();
                    pr.PermissionEnterApartment = Convert.ToInt32(dr["PermissionEnterApartment"].ToString());
                    pr.StatusString =dr["Status"].ToString();
                    pr.LocationString = dr["Location"].ToString();
                    pr.CausingIssue = dr["ItemCaussing"].ToString();
                    pr.Issue = dr["ItemIssue"].ToString();
                    pr.Notes = dr["Notes"].ToString();
                    pr.OtherItemCaussing = dr["OtherItemCaussing"].ToString();
                    pr.OtherItemIssue = dr["OtherItemIssue"].ToString();
                    pr.Unit = dr["UnitNo"].ToString();
                    pr.Phone = dr["Mobile"].ToString();
                    pr.EmergencyMobile = dr["EmergencyMobile"].ToString();
                    pr.TempServiceFile = dr["TempServiceFile"].ToString();
                    pr.OriginalServiceFile = dr["OriginalServiceFile"].ToString();
                    pr.Email = dr["email"].ToString();
                    pr.Status = Convert.ToInt32(dr["StatusInt"].ToString());
                    pr.UrgentStatus = Convert.ToInt32(dr["UrgentStatus"].ToString());
                    pr.ServicePerson = Convert.ToInt32(dr["ServicePerson"].ToString());
                    pr.ServicePerson = Convert.ToInt32(dr["ServicePerson"].ToString());
                    pr.ClosingDatestring = dr["ClosingDate"].ToString();
                    pr.ClosingNotes = dr["ClosingNotes"].ToString();
                    pr.TaskNotes = dr["TaskNotes"].ToString();
                    pr.Project = dr["Title"].ToString();
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    pr.MoveIndate = dr["MoveIndate"].ToString();
                    pr.MStartDateString = dr["MStartDate"].ToString();
                    pr.MLeaseEndDateString = dr["LeaseEndDate"].ToString();
                    pr.WarrantyStatus = Convert.ToInt32(dr["WarrantyStatus"].ToString());
                }
                db.Dispose();
                return pr;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
           
        }

        //public string StatusUpdateServiceRequest(ServicesManagementModel model)
        //{
        //    string msg = "";
        //    ShomaRMEntities db = new ShomaRMEntities();

        //    var UpdateStatusService = db.tbl_ServiceRequest.Where(co => co.ServiceID == model.ServiceID).FirstOrDefault();

        //    if (UpdateStatusService != null)
        //    {
        //        UpdateStatusService.CompletedPicture = model.CompletedPicture;
        //        UpdateStatusService.TempCompletedPicture = model.TempCompletedPicture;
        //        UpdateStatusService.ClosingNotes = model.ClosingNotes;
        //        UpdateStatusService.ClosingDate = model.ClosingDate;
        //        UpdateStatusService.OwnerSignature= model.OwnerSignature;
        //        UpdateStatusService.TempOwnerSignature = model.TempOwnerSignature;
        //        db.SaveChanges();
        //        msg = "Service Request Status Update Successfully";
        //    }

        //    db.Dispose();
        //    return msg;
        //}
        public string StatusUpdateForServicePerson(ServicesManagementModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var UpdateStatusService = db.tbl_ServiceRequest.Where(co => co.ServiceID == model.ServiceID).FirstOrDefault();
            string message = "";
            string phonenumber = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (UpdateStatusService != null)
            {

                UpdateStatusService.ServicePerson = model.ServicePerson;
                UpdateStatusService.PermissionComeDate = model.PermissionComeDate;
                UpdateStatusService.PermissionComeTime = model.PermissionComeTime;
                UpdateStatusService.Status = model.Status;
                UpdateStatusService.UrgentStatus = model.UrgentStatus;

                UpdateStatusService.CompletedPicture = model.CompletedPicture;
                UpdateStatusService.TempCompletedPicture = model.TempCompletedPicture;
                UpdateStatusService.ClosingNotes = model.ClosingNotes;
                UpdateStatusService.TaskNotes = model.TaskNotes;
                UpdateStatusService.ClosingDate = model.ClosingDate;
                UpdateStatusService.OwnerSignature = model.OwnerSignature;
                UpdateStatusService.TempOwnerSignature = model.TempOwnerSignature;
                UpdateStatusService.WarrantyStatus = model.WarrantyStatus;
                UpdateStatusService.ApprovedBy = userid;
                db.SaveChanges();
                msg = "Service Request Assign Successfully";

                var userdetail = db.tbl_Login.Where(co => co.UserID == model.ServicePerson).FirstOrDefault();


                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                reportHTML = reportHTML.Replace("[%ServerURL%]", ServerURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                string emailBody = "";
                emailBody += "<p style=\"margin-bottom: 0px;\">We hereby assign your service of " + model.ProblemCategorystring + " (facility) on " + model.PermissionComeDate.Value.ToString("MM/dd/yyyy") + " (date) at " + model.PermissionComeTime + " to " + userdetail.FirstName + " " + userdetail.LastName + "</p>";
                reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);


                //reportHTML = reportHTML.Replace("[%TenantName%]", model.TenantName);
                //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We hereby assign your service of " + model.ProblemCategorystring + " (facility) on " + model.PermissionComeDate.Value.ToString("MM/dd/yyyy") + " (date) at " + model.PermissionComeTime + " to " + userdetail.FirstName + " " + userdetail.LastName + "</p>");
                //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");

                string body = reportHTML;
                new EmailSendModel().SendEmail(model.Email, "Service Request Assign Successfully ", body);
                new EmailSendModel().SendEmail(userdetail.Email, "Service Request --(" + model.TenantName + ") ", body);
                phonenumber = model.Phone;
                message = "Your service of " + model.ProblemCategorystring + " (facility) on " + model.PermissionComeDate.Value.ToString("MM/dd/yyyy") + " (date) at " + model.PermissionComeTime + " to " + userdetail.FirstName + " " + userdetail.LastName + ". Please check the email for detail.";
                if (SendMessage == "yes")
                {
                    if (!string.IsNullOrWhiteSpace(phonenumber))
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }

                db.Dispose();
            }
            return msg;
        }

        public ServicesManagementModel UploadServiceFile(HttpPostedFileBase fileBaseUpload, ServicesManagementModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServicesManagementModel CompletedFile = new ServicesManagementModel();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUpload != null && fileBaseUpload.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUpload.FileName;
                Extension = Path.GetExtension(fileBaseUpload.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload.FileName);
                fileBaseUpload.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUpload.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/") + "/" + sysFileName;

                }
                CompletedFile.TempCompletedPicture = sysFileName.ToString();
                CompletedFile.CompletedPicture = fileName;
            }

            return CompletedFile;
        }
        public ServicesManagementModel OwnerSignatureFile(HttpPostedFileBase fileBaseUpload, ServicesManagementModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServicesManagementModel OwnerSignatureFile = new ServicesManagementModel();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUpload != null && fileBaseUpload.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUpload.FileName;
                Extension = Path.GetExtension(fileBaseUpload.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload.FileName);
                fileBaseUpload.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUpload.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/") + "/" + sysFileName;

                }
                OwnerSignatureFile.TempOwnerSignature = sysFileName.ToString();
                OwnerSignatureFile.OwnerSignature = fileName;
            }

            return OwnerSignatureFile;
        }

        public List<ServicesManagementModel> FillAssignmentAuditHistoryList(ServicesManagementModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ServicesManagementModel> lstAssigment = new List<ServicesManagementModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAssigmentAuditList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "ServiceID";
                    param0.Value = model.ServiceID;
                    cmd.Parameters.Add(param0);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ServicesManagementModel searchmodel = new ServicesManagementModel();

                    //searchmodel.ServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                    searchmodel.EventName = dr["EventName"].ToString();
                    searchmodel.EventDate = Convert.ToDateTime(dr["EventDate"]).ToString("MM/dd/yyyy");
                    searchmodel.UserName = dr["UserName"].ToString();
                    searchmodel.AuditDetail = dr["AuditDetails"].ToString();

                    //searchmodel.ServicePersonString = dr["ServicePersonName"].ToString();
                    //searchmodel.PermissionComeDateString = Convert.ToDateTime(dr["PermissionComeDate"]).ToString("MM/dd/yyyy");
                    //searchmodel.PermissionComeTime = Convert.ToDateTime(dr["PermissionComeTime"]).ToString("HH:mm:ss");
                    //searchmodel.TaskNotes = dr["TaskNotes"].ToString();
                    //searchmodel.ClosingDatestring = Convert.ToDateTime(dr["ClosingDate"]).ToString();
                    //searchmodel.ClosingNotes = dr["ClosingNotes"].ToString();

                    lstAssigment.Add(searchmodel);
                }
                db.Dispose();
                return lstAssigment.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }




        public class ServicesSearchModel
        {
            public long ServiceID { get; set; }
            public string TenantName { get; set; }
            public string Problemcategory { get; set; }
            public string ItemCaussing { get; set; }
            public string ItemIssue { get; set; }
            public string Location { get; set; }
            public string StatusString { get; set; }
            public string PriorityString { get; set; }
            public string OtherItemCaussing { get; set; }
            public string OtherItemIssue { get; set; }
            public string EmergencyMobile { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Notes { get; set; }
            public string UnitNo { get; set; }
            public string Criteria { get; set; }
            public string SortBy { get; set; }
            public string OrderBy { get; set; }

            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int Status { get; set; }
            public int Priority { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }

        public class EstimateModel
        {
            public int EID { get; set; }
            public Nullable<long> ServiceID { get; set; }
            public string Description { get; set; }
            public string Vendor { get; set; }
            public string Amount { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<long> CreatedBy { get; set; }
            public string Status { get; set; }
            public string CreatedDateTxt { get; set; }
            public string CreatedByTxt { get; set; }
            public string SendMessage { get; private set; }
            public Nullable<long> TenantID { get; set; }
            string serverURL = WebConfigurationManager.AppSettings["ServerURL"];
            string sendMessage = WebConfigurationManager.AppSettings["SendMessage"];
            public  string TenantName { get; set; }

            public string SaveUpdateEstimate(EstimateModel model)
            {
                string msg = "";
                ShomaRMEntities db = new ShomaRMEntities();
                int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
                if (model.EID == 0)
                {
                    var saveEstimate = new tbl_Estimate()
                    {

                        ServiceID = model.ServiceID,
                        Vendor = model.Vendor,
                        Amount = model.Amount,
                        Description = model.Description,
                        Status = model.Status,
                        CreatedDate = DateTime.Now,
                        CreatedBy = userid
                    };
                    db.tbl_Estimate.Add(saveEstimate);
                    db.SaveChanges();
                    model.EID = saveEstimate.EID;
                    var GetServiceData = db.tbl_ServiceRequest.Where(p => p.ServiceID == model.ServiceID).FirstOrDefault();
                    var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == GetServiceData.TenantID).FirstOrDefault();
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Template/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");

                    string message = "";
                    string phonenumber = GetTenantData.Mobile;
                    string accserviceid = "";
                    string denserviceid = "";

                    accserviceid = new EncryptDecrypt().EncryptText(model.EID.ToString() + ",1");
                    denserviceid = new EncryptDecrypt().EncryptText(model.EID.ToString() + ",2");

                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    string emailBody = "";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName+"</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">We hereby send the  Estimate of Repair of Amount: $" + model.Amount + " for Vendor: " + model.Vendor + ",  with Description: " + model.Description + ". Please Accept or Deny the status on click of following link. </p>";
                    emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + " /Service/?sid=" + accserviceid + "\" class=\"link-button\" target=\"_blank\">ACCEPT</a> <a href=\"" + serverURL + " /Service/?sid=" + denserviceid + "\" class=\"link-button\" target=\"_blank\">DENY</a></p>";
                    reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Estimate of Repair");
                    //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We hereby send the  Estimate of Repair of Amount: $" + model.Amount + " for Vendor: " + model.Vendor + ",  with Description: " + model.Description + ". Please Accept or Deny the status on click of following link. </p>");
                    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + " /Service/?sid=" + accserviceid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Service/?sid=" + accserviceid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">ACCEPT</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table>  <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + " /Service/?sid=" + denserviceid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Service/?sid=" + denserviceid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">DENY</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                    //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetTenantData.Email);

                    message = "Estimate of Repair has been sent on your email. Please check the email for detail.";

                    string body = reportHTML;
                    new EmailSendModel().SendEmail(GetTenantData.Email, "Estimate of Repair", body);
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
                    }
                    msg = "Estimate Saved Successfully";

                }
                else
                {
                    var getEstimatedata = db.tbl_Estimate.Where(p => p.EID == model.EID).FirstOrDefault();
                    if (getEstimatedata != null)
                    {

                        getEstimatedata.ServiceID = model.ServiceID;
                        getEstimatedata.Vendor = model.Vendor;
                        getEstimatedata.Amount = model.Amount;
                        getEstimatedata.Description = model.Description;
                        getEstimatedata.Status = model.Status;
                    }
                    db.SaveChanges();
                    var GetServiceData = db.tbl_ServiceRequest.Where(p => p.ServiceID == model.ServiceID).FirstOrDefault();
                    var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == GetServiceData.TenantID).FirstOrDefault();
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Template/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    string message = "";
                    string phonenumber = GetTenantData.Mobile;
                    string accserviceid = "";
                    string denserviceid = "";
                    string payid = "";
                    if (model.Status != "3")
                    {
                        accserviceid = new EncryptDecrypt().EncryptText(model.EID.ToString() + ",1"); 
                        denserviceid = new EncryptDecrypt().EncryptText(model.EID.ToString() + ",2");

                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">We hereby send the  Estimate of Repair of Amount: $" + model.Amount + " for Vendor: " + model.Vendor + ",  with Description: " + model.Description + ". Click following link to Accept or Deny the payment.</p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + " /Service/?sid=" + accserviceid + "\" class=\"link-button\" target=\"_blank\">ACCEPT</a> <a href=\"" + serverURL + " /Service/?sid=" + denserviceid + "\" class=\"link-button\" target=\"_blank\">DENY</a></p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Estimate of Repair");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We hereby send the  Estimate of Repair of Amount: $" + model.Amount + " for Vendor: " + model.Vendor + ",  with Description: " + model.Description + ". Click following link to Accept or Deny the payment. </p>");
                        //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + " /Service/?sid=" + accserviceid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Service/?sid=" + accserviceid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">ACCEPT</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table>  <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + " /Service/?sid=" + denserviceid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/Service/?sid=" + denserviceid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">DENY</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                        
                        message = "Estimate of Repair has been sent on your email. Please check the email for detail.";

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", GetTenantData.Email);

                        string body = reportHTML;
                        new EmailSendModel().SendEmail(GetTenantData.Email, "Estimate of Repair", body);
                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(phonenumber))
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                        }
                        msg = "Estimate Updated Successfully";
                    }
                    else
                    {
                        payid = new EncryptDecrypt().EncryptText(model.EID.ToString() + ",0");

                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">We hereby send the  Estimate of Repair of Amount: $" + model.Amount + " for Vendor: " + model.Vendor + ",  with Description: " + model.Description + ". Click here to pay the Invoice. </p>";
                        emailBody += "<p style=\"margin-bottom: 20px;text-align: center;\"><a href=\"" + serverURL + "/PayLink/PayServiceCharges/?pid=" + payid + "\" class=\"link-button\" target=\"_blank\">PAY NOW</a></p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Invoice of Repair");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We hereby send the  Estimate of Repair of Amount: $" + model.Amount + " for Vendor: " + model.Vendor + ",  with Description: " + model.Description + ". Click here to pay the Invoice. </p>");
                        //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/PayServiceCharges/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/PayServiceCharges/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                        //reportHTML = reportHTML.Replace("[%TenantEmail%]", GetTenantData.Email);

                        message = "Invoice of Repair has been sent on your email. Please check the email for detail.";

                        string body = reportHTML;
                        new EmailSendModel().SendEmail(GetTenantData.Email, "Invoice of Repair", body);
                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(phonenumber))
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                        }
                        msg = "Service Invoice Generated Successfully";

                    }
                }

                db.Dispose();
                return msg;
            }

            public List<EstimateModel> FillEstimateList(EstimateModel model)
            {
                ShomaRMEntities db = new ShomaRMEntities();
                List<EstimateModel> lstEstimate = new List<EstimateModel>();
                try
                {
                    DataTable dtTable = new DataTable();
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        db.Database.Connection.Open();
                        cmd.CommandText = "usp_GetEstimateList";
                        cmd.CommandType = CommandType.StoredProcedure;

                        DbParameter param0 = cmd.CreateParameter();
                        param0.ParameterName = "ServiceID";
                        param0.Value = model.ServiceID;
                        cmd.Parameters.Add(param0);

                        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dtTable);
                        db.Database.Connection.Close();
                    }
                    foreach (DataRow dr in dtTable.Rows)
                    {
                        EstimateModel searchmodel = new EstimateModel();
                        searchmodel.EID = Convert.ToInt32(dr["EID"].ToString());
                        searchmodel.ServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                        searchmodel.Vendor = dr["Vendor"].ToString();
                        searchmodel.Amount = dr["Amount"].ToString();
                        searchmodel.Description = dr["Description"].ToString();
                        searchmodel.CreatedByTxt = dr["CreatedBy"].ToString();
                        searchmodel.CreatedDateTxt = Convert.ToDateTime(dr["CreatedDate"]).ToString("MM/dd/yyyy");
                        searchmodel.Status = dr["Status"].ToString();

                        lstEstimate.Add(searchmodel);
                    }
                    db.Dispose();
                    return lstEstimate.ToList();
                }
                catch (Exception ex)
                {
                    db.Database.Connection.Close();
                    throw ex;
                }
            }

            public EstimateModel GetEstimateData(EstimateModel model)
            {
                ShomaRMEntities db = new ShomaRMEntities();
                var getEstimateInfo = db.tbl_Estimate.Where(co => co.EID == model.EID).FirstOrDefault();
                if (getEstimateInfo != null)
                {
                    model.ServiceID = getEstimateInfo.ServiceID;
                    model.Vendor = getEstimateInfo.Vendor;
                    model.Amount = getEstimateInfo.Amount;
                    model.Description = getEstimateInfo.Description;
                    model.Status = getEstimateInfo.Status;
                }
                db.Dispose();
                return model;
            }

            public EstimateModel GetEstimateInvData(int EID)
            {
                ShomaRMEntities db = new ShomaRMEntities();
                EstimateModel model = new EstimateModel();

                var getEstimateInfo = db.tbl_Estimate.Where(co => co.EID == EID).FirstOrDefault();
                if (getEstimateInfo != null)
                {
                    model.CreatedDate = getEstimateInfo.CreatedDate ;
                  
                    model.ServiceID = getEstimateInfo.ServiceID;
                    model.Vendor = getEstimateInfo.Vendor;
                    model.Amount = getEstimateInfo.Amount;
                    model.Description = getEstimateInfo.Description;
                    model.Status = getEstimateInfo.Status;
                }

                var GetservData = db.tbl_ServiceRequest.Where(p => p.ServiceID == getEstimateInfo.ServiceID).FirstOrDefault();
                if (GetservData != null)
                {
                    model.TenantID = GetservData.TenantID;
                }

                var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == model.TenantID).FirstOrDefault();
                if (GetTenantData != null)
                {
                    model.TenantName = GetTenantData != null ? GetTenantData.FirstName + " " + GetTenantData.LastName : "";
                }
                model.EID = EID;
                return model;
            }
        }
    }
}