using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Areas.Tenant
{
    public class ServiceRequestModel
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
        public Nullable<int> UrgentStatus { get; set; }

        public string SaveUpdateServiceRequest(ServiceRequestModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.ServiceID == 0)
            {
                var saveServiceRequest = new tbl_ServiceRequest()
                {
                    TenantID = model.TenantID,
                    ProblemCategory = model.ProblemCategory,
                    Details = model.Details,
                    PermissionEnterApartment = model.PermissionEnterApartment,
                    PermissionComeDate = model.PermissionComeDate,
                    PetInforChange = model.PetInforChange,
                    // AlarmCodeChange = model.AlarmCodeChange,
                    Notes = model.Notes,
                    Status = 1,
                    Location = model.Location,
                    ItemCaussing = model.ItemCaussing,
                    ItemIssue = model.ItemIssue,
                    OtherItemCaussing = model.OtherItemCaussing,
                    OtherItemIssue = model.OtherItemIssue,
                    ServiceDate = DateTime.Now,
                    Priority = model.Priority,
                    OriginalServiceFile = model.OriginalServiceFile,
                    TempServiceFile = model.TempServiceFile,
                    EmergencyMobile = model.EmergencyMobile,
                    ServicePerson=1,
                    PermissionComeTime = model.PermissionComeTime,
                    UrgentStatus=model.UrgentStatus,
                };
                db.tbl_ServiceRequest.Add(saveServiceRequest);
                db.SaveChanges();
                msg = "Service Request Saved Successfully";
            }
            else
            {
                var updateServiceRequest = db.tbl_ServiceRequest.Where(co => co.ServiceID == model.ServiceID).FirstOrDefault();

                if (updateServiceRequest != null)
                {
                    updateServiceRequest.TenantID = model.TenantID;
                    updateServiceRequest.ProblemCategory = model.ProblemCategory;
                    updateServiceRequest.Details = model.Details;
                    updateServiceRequest.PermissionEnterApartment = model.PermissionEnterApartment;
                    updateServiceRequest.PermissionComeDate = model.PermissionComeDate;
                    updateServiceRequest.PetInforChange = model.PetInforChange;
                    //updateServiceRequest.AlarmCodeChange = model.AlarmCodeChange;
                    updateServiceRequest.Notes = model.Notes;
                    updateServiceRequest.Status = 1;
                    updateServiceRequest.Location = model.Location;
                    updateServiceRequest.ItemCaussing = model.ItemCaussing;
                    updateServiceRequest.ItemIssue = model.ItemIssue;
                    updateServiceRequest.OtherItemCaussing = model.OtherItemCaussing;
                    updateServiceRequest.OtherItemIssue = model.OtherItemIssue;
                    updateServiceRequest.ServiceDate = DateTime.Now;
                    updateServiceRequest.Priority = model.Priority;
                    updateServiceRequest.OriginalServiceFile = model.OriginalServiceFile;
                    updateServiceRequest.TempServiceFile = model.TempServiceFile;
                    updateServiceRequest.EmergencyMobile = model.EmergencyMobile;
                    updateServiceRequest.PermissionComeTime = model.PermissionComeTime;
                    db.SaveChanges();
                    msg = "Service Request Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public ServiceRequestModel GetServiceInfo(ServiceRequestModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var getTenantInfo = db.tbl_TenantInfo.Where(co => co.TenantID == model.TenantID).FirstOrDefault();
            if (getTenantInfo != null)
            {
                var getUnit = db.tbl_PropertyUnits.Where(co => co.UID == getTenantInfo.UnitID).FirstOrDefault();
                if (getUnit != null)
                {
                    model.Unit = getUnit.UnitNo;
                }
                model.Name = getTenantInfo.FirstName + " " + getTenantInfo.LastName;
                model.Phone = getTenantInfo.Mobile;
                model.EmergencyPhone = getTenantInfo.EmergencyMobile;
                model.Email = getTenantInfo.Email;
            }
            db.Dispose();
            return model;
        }

        public List<ServiceRequestModel> GetServiceRequestList(ServiceRequestModel model)
        {
            List<ServiceRequestModel> listServiceRequestList = new List<ServiceRequestModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.ServiceRequest == 4)
            {
                var getServiceRequestList = db.tbl_ServiceRequest.Where(co => co.TenantID == model.TenantID).ToList();
                if (getServiceRequestList != null)
                {
                    foreach (var item in getServiceRequestList)
                    {
                        listServiceRequestList.Add(new ServiceRequestModel()
                        {
                            ServiceID = item.ServiceID,
                            TenantID = item.TenantID,
                            ProblemCategorystring = item.ProblemCategory == 1 ? "Appliance" : item.ProblemCategory == 2 ? "Doors & Locks" : item.ProblemCategory == 3 ? "Electrical and Lighting" : item.ProblemCategory == 4 ? "Flooring" : item.ProblemCategory == 5 ? "General" : item.ProblemCategory == 6 ? "Heating & cooling" : item.ProblemCategory == 7 ? "Plumbing & bath" : item.ProblemCategory == 8 ? "Safety equipment" : item.ProblemCategory == 9 ? "Preventative maintenance" : item.ProblemCategory == 10 ? "Other" : "",
                            Details = item.Details,
                            PermissionEnterApartment = item.PermissionEnterApartment,
                            PermissionComeDate = item.PermissionComeDate,
                            PetInforChange = item.PetInforChange,
                            AlarmCodeChange = item.AlarmCodeChange,
                            Notes = item.Notes,
                            Status = item.Status,
                            DateString = item.PermissionComeDate != null ? item.PermissionComeDate.Value.ToString("MM/dd/yyyy") : "Any Time",
                            StatusString = item.Status == 1 ? "Open" : item.Status == 2 ? "Completed" : item.Status == 3 ? "Cancelled" : item.Status == 4 ? "In Process" : "",
                            PriorityString = item.Priority == 1 ? "Normal" : item.Priority == 2 ? "Medium" : item.Priority == 3 ? "High" : item.Priority == 4 ? "Emergency" : "",
                            OriginalServiceFile = item.OriginalServiceFile,
                            TempServiceFile = item.TempServiceFile,
                        });
                    }
                }
            }
            else
            {
                var getServiceRequestList = db.tbl_ServiceRequest.Where(co => co.TenantID == model.TenantID && co.Status == model.ServiceRequest).ToList();
                if (getServiceRequestList != null)
                {
                    foreach (var item in getServiceRequestList)
                    {
                        listServiceRequestList.Add(new ServiceRequestModel()
                        {
                            ServiceID = item.ServiceID,
                            TenantID = item.TenantID,
                            ProblemCategorystring = item.ProblemCategory == 1 ? "Appliance" : item.ProblemCategory == 2 ? "Doors & Locks" : item.ProblemCategory == 3 ? "Electrical and Lighting" : item.ProblemCategory == 4 ? "Flooring" : item.ProblemCategory == 5 ? "General" : item.ProblemCategory == 6 ? "Heating & cooling" : item.ProblemCategory == 7 ? "Plumbing & bath" : item.ProblemCategory == 8 ? "Safety equipment" : item.ProblemCategory == 9 ? "Preventative maintenance" : item.ProblemCategory == 10 ? "Other" : "",
                            Details = item.Details,
                            PermissionEnterApartment = item.PermissionEnterApartment,
                            PermissionComeDate = item.PermissionComeDate,
                            PetInforChange = item.PetInforChange,
                            AlarmCodeChange = item.AlarmCodeChange,
                            Notes = item.Notes,
                            Status = item.Status,
                            DateString = item.PermissionComeDate != null ? item.PermissionComeDate.Value.ToString("MM/dd/yyyy") : "Any Time",
                            StatusString = item.Status == 1 ? "Open" : item.Status == 2 ? "Completed" : item.Status == 3 ? "Cancelled" : item.Status == 4 ? "In Process" : "",
                            PriorityString = item.Priority == 1 ? "Normal" : item.Priority == 2 ? "Medium" : item.Priority == 3 ? "High" : item.Priority == 4 ? "Emergency" : "",
                            OriginalServiceFile = item.OriginalServiceFile,
                            TempServiceFile = item.TempServiceFile,
                        });
                    }
                }
            }

            db.Dispose();
            return listServiceRequestList;
        }

        public string CancelServiceRequest(ServiceRequestModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var cancelServiceRequest = db.tbl_ServiceRequest.Where(co => co.ServiceID == model.ServiceID).FirstOrDefault();

            if (cancelServiceRequest != null)
            {
                cancelServiceRequest.Status = 3;
                db.SaveChanges();
                msg = "Service Request Canceled Successfully";
            }

            db.Dispose();
            return msg;
        }

        public List<ServiceRequestModel> GetServiceRequestForAlarm(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ServiceRequestModel> lstpr = new List<ServiceRequestModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServiceRequestByDate";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "TenantId";
                    paramF.Value = TenantId;
                    cmd.Parameters.Add(paramF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ServiceRequestModel pr = new ServiceRequestModel();

                    pr.ServiceID = Convert.ToInt64(dr["ServiceID"].ToString());
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    pr.ProblemCategory = Convert.ToInt32(dr["ProblemCategory"].ToString());
                    pr.ProblemCategoryName = dr["ProblemCategoryName"].ToString();
                    pr.PermissionComeDateString = dr["PermissionComeDateString"].ToString();
                    pr.PermissionComeTime = dr["PermissionComeTime"].ToString();
                    pr.Details = dr["Details"].ToString();
                    pr.PermissionEnterApartment = Convert.ToInt32(dr["PermissionEnterApartment"].ToString());
                    pr.Status = Convert.ToInt32(dr["Status"].ToString());
                    pr.Location = Convert.ToInt32(dr["Location"].ToString());
                    pr.ItemCaussing = Convert.ToInt32(dr["ItemCaussing"].ToString());
                    pr.ItemIssue = Convert.ToInt32(dr["ItemIssue"].ToString());
                    pr.ServiceDate = Convert.ToDateTime(dr["ServiceDate"].ToString());
                    lstpr.Add(pr);
                }
                db.Dispose();

            }
            catch
            {

            }
            return lstpr.ToList();
        }

        public ServiceRequestModel UploadServiceFile(HttpPostedFileBase fileBaseUpload, ServiceRequestModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServiceRequestModel ServiceFile = new ServiceRequestModel();

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
                ServiceFile.TempServiceFile = sysFileName.ToString();
                ServiceFile.OriginalServiceFile = fileName;
            }

            return ServiceFile;
        }

        public List<ServiceRequestModel> GetDdlLocation()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var Location = db.tbl_ServiceLocation.OrderBy(co => co.Location).ToList();
            List<ServiceRequestModel> li = new List<ServiceRequestModel>();

            foreach (var item in Location)
            {
                li.Add(new ServiceRequestModel
                {
                    LocationId = item.LocationID,
                    LocationString = item.Location
                });
            }

            return li.ToList();
        }

        public List<ServiceRequestModel> GetDdlServiceCategory()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var ServiceIssue = db.tbl_ServiceIssue.OrderBy(co => co.ServiceIssueID).ToList();
            List<ServiceRequestModel> li = new List<ServiceRequestModel>();

            foreach (var item in ServiceIssue)
            {
                li.Add(new ServiceRequestModel
                {
                    ServiceIssueID = item.ServiceIssueID,
                    ServiceIssueString = item.ServiceIssue
                });
            }

            return li.ToList();
        }

        public class CaussingIssueList
        {
            public long CausingIssueID { get; set; }
            public string CausingIssue { get; set; }
            public long IssueID { get; set; }
            public string Issue { get; set; }
        }

        public List<CaussingIssueList> GetDdlCausingIssue(long ServiceIssueID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CaussingIssueList> lstData = new List<CaussingIssueList>();
            DataTable dtTable = new DataTable();
            try
            {
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_FillCausingIssueID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "ServiceIssueID";
                    param4.Value = ServiceIssueID;
                    cmd.Parameters.Add(param4);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (System.Data.DataRow dr in dtTable.Rows)
                {
                    CaussingIssueList model = new CaussingIssueList();
                    model.CausingIssueID = Convert.ToInt64(dr["CausingIssueID"].ToString());
                    model.CausingIssue = dr["CausingIssue"].ToString();
                    lstData.Add(model);
                }
                db.Dispose();
                return lstData.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public List<CaussingIssueList> GetDdlIssue(long CausingIssueID, long ServiceIssueID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CaussingIssueList> lstData = new List<CaussingIssueList>();
            DataTable dtTable = new DataTable();
            try
            {
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_FillIssueID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "CausingIssueID";
                    param1.Value = CausingIssueID;
                    cmd.Parameters.Add(param1);

                    DbParameter param2 = cmd.CreateParameter();
                    param2.ParameterName = "ServiceIssueID";
                    param2.Value = ServiceIssueID;
                    cmd.Parameters.Add(param2);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (System.Data.DataRow dr in dtTable.Rows)
                {
                    CaussingIssueList model = new CaussingIssueList();
                    model.IssueID = Convert.ToInt64(dr["IssueID"].ToString());
                    model.Issue = dr["Issue"].ToString();
                    lstData.Add(model);
                }
                db.Dispose();
                return lstData.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

    }
}