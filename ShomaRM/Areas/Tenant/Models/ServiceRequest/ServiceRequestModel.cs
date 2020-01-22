using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

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
                    ServiceDate = DateTime.Now
                };
                db.tbl_ServiceRequest.Add(saveServiceRequest);
                db.SaveChanges();
                msg = "Progress Saved";
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
                    db.SaveChanges();
                    msg = "Progress Updated";
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
            if (model.ServiceRequest==4)
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
                            ProblemCategorystring = item.ProblemCategory == 1 ? "Appliance" : item.ProblemCategory == 2 ? "Doors & Locks" : item.ProblemCategory == 3 ? "Electrical and Lighting" : item.ProblemCategory == 4 ? "Flooring" : item.ProblemCategory == 5 ? "General" : item.ProblemCategory == 6 ? "Heating & cooling" : item.ProblemCategory == 7 ? "Plumbing & bath" : item.ProblemCategory == 8 ? "Safety equipment" : item.ProblemCategory == 9 ? "Preventative maintenance" : item.ProblemCategory == 10 ? "" : "Other",
                            Details = item.Details,
                            PermissionEnterApartment = item.PermissionEnterApartment,
                            PermissionComeDate = item.PermissionComeDate,
                            PetInforChange = item.PetInforChange,
                            AlarmCodeChange = item.AlarmCodeChange,
                            Notes = item.Notes,
                            Status = item.Status,
                            DateString = item.PermissionComeDate != null ? item.PermissionComeDate.Value.ToString("MM/dd/yyyy") : "Any Time",
                            StatusString = item.Status == 1 ? "Open" : item.Status == 2 ? "Completed" : item.Status == 3 ? "Cancelled" : ""
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
                            ProblemCategorystring = item.ProblemCategory == 1 ? "Appliance" : item.ProblemCategory == 2 ? "Doors & Locks" : item.ProblemCategory == 3 ? "Electrical and Lighting" : item.ProblemCategory == 4 ? "Flooring" : item.ProblemCategory == 5 ? "General" : item.ProblemCategory == 6 ? "Heating & cooling" : item.ProblemCategory == 7 ? "Plumbing & bath" : item.ProblemCategory == 8 ? "Safety equipment" : item.ProblemCategory == 9 ? "Preventative maintenance" : item.ProblemCategory == 10 ? "" : "Other",
                            Details = item.Details,
                            PermissionEnterApartment = item.PermissionEnterApartment,
                            PermissionComeDate = item.PermissionComeDate,
                            PetInforChange = item.PetInforChange,
                            AlarmCodeChange = item.AlarmCodeChange,
                            Notes = item.Notes,
                            Status = item.Status,
                            DateString = item.PermissionComeDate != null ? item.PermissionComeDate.Value.ToString("MM/dd/yyyy") : "Any Time",
                            StatusString = item.Status == 1 ? "Open" : item.Status == 2 ? "Completed" : item.Status == 3 ? "Cancelled" : ""
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
    }
}