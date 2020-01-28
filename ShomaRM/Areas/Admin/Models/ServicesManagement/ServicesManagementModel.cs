using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
                db.SaveChanges();
                msg = "Service Request Status Update Successfully";
            }

            db.Dispose();
            return msg;
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