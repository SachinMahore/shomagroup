using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

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
       


        public int BuildPaganationUserList(ServicesSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
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
                    param5.Value = Convert.ToInt32(ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID);
                    cmd.Parameters.Add(param5);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

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
                    param5.Value = Convert.ToInt32(ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID);
                    cmd.Parameters.Add(param5);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

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
                    //lstpr.Add(pr);
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

        public string StatusUpdateServiceRequest(ServicesManagementModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var UpdateStatusService = db.tbl_ServiceRequest.Where(co => co.ServiceID == model.ServiceID).FirstOrDefault();

            if (UpdateStatusService != null)
            {
                UpdateStatusService.Status = model.Status;
                UpdateStatusService.ApprovedBy = Convert.ToInt32(ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID);
                UpdateStatusService.CompletedPicture = model.CompletedPicture;
                UpdateStatusService.TempCompletedPicture = model.TempCompletedPicture;
                UpdateStatusService.ServicePerson=model.ServicePerson;
                db.SaveChanges();
                msg = "Service Request Status Update Successfully";
            }

            db.Dispose();
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


            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int Status { get; set; }
            public int Priority { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
    }
}