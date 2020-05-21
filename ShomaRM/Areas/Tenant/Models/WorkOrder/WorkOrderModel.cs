using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;




namespace ShomaRM.Areas.Tenant.Models
{
    public class WorkOrderModel
    {
        public long WOID { get; set; }
        public long TenantID { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string UnitIDString { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyIDString { get; set; }
        public string ProblemID { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateOpened { get; set; }
        public string DateOpenedString { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public string DateClosedString { get; set; }
        public Nullable<int> ReportedBy { get; set; }
        public string ContactPhone { get; set; }
        public string AssignedTo { get; set; }
        public string Resolution { get; set; }
        public long VendorID { get; set; }

        public List<WorkOrderModel> GetWorkOrderList(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<WorkOrderModel> lstpr = new List<WorkOrderModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetWorkOrderList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    WorkOrderModel pr = new WorkOrderModel();
                   
                    DateTime? dateOpened = null;
                    try
                    {
                        dateOpened = Convert.ToDateTime(dr["DateOpened"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? dateClosed = null;
                    try
                    {
                        dateClosed = Convert.ToDateTime(dr["DateClosed"].ToString());
                    }
                    catch
                    {

                    }
                    pr.WOID = Convert.ToInt32(dr["WOID"].ToString());
                    pr.PropertyIDString = dr["PropertyID"].ToString();
                    pr.UnitIDString = dr["UnitID"].ToString();
                    pr.ProblemID = dr["ProblemID"].ToString();
                    pr.DateOpenedString = dateOpened == null ? "" : dateOpened.ToString();
                    pr.DateClosedString = dateClosed == null ? "" : dateClosed.ToString();
                    pr.ReportedBy = Convert.ToInt16(dr["ReportedBy"].ToString());
                    pr.AssignedTo = dr["AssignedTo"].ToString();
                    pr.VendorID = long.Parse(!string.IsNullOrEmpty(dr["VendorID"].ToString()) ? dr["VendorID"].ToString() : "0");

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
        public List<TenantPropertyModel> GetPropertyList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantPropertyModel> lstProp = new List<TenantPropertyModel>();
            var propList = db.tbl_Properties.ToList();
            foreach (var pl in propList)
            {
                lstProp.Add(new TenantPropertyModel
                {
                    PID = pl.PID,
                    Title = pl.Title,
                    
                });

            }
            return lstProp;
        }
        public List<TenantPropertyUnits> GetPropertyUnitList(long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantPropertyUnits> lstUnitProp = new List<TenantPropertyUnits>();
            var propList = db.tbl_PropertyUnits.Where(p => p.PID == PID).ToList();
            foreach (var pl in propList)
            {
                lstUnitProp.Add(new TenantPropertyUnits
                {
                    UID = pl.UID,
                    UnitNo = pl.UnitNo,
                   

                });

            }
            return lstUnitProp;
        }
        public WorkOrderModel GetWorkOrderDeatails(int id)
        {
             ShomaRMEntities db = new ShomaRMEntities();
            WorkOrderModel model=new WorkOrderModel ();
            if(id!=0)
            {
                var getWorkOrderDet = db.tbl_WorkOrder.Where(p => p.WOID == id).FirstOrDefault();
                if (getWorkOrderDet != null)
                {
                    model.WOID = getWorkOrderDet.WOID;
                    model.TenantID = getWorkOrderDet.TenantID.HasValue? getWorkOrderDet.TenantID.Value:0;
                     model.PropertyID = getWorkOrderDet.PropertyID;
                     model.ProblemID = getWorkOrderDet.ProblemID;
                     model.UnitID = getWorkOrderDet.UnitID;
                     model.DateOpened = getWorkOrderDet.DateOpened;
                     model.DateClosed = getWorkOrderDet.DateClosed;
                     model.Description = getWorkOrderDet.Description;
                     model.ReportedBy = getWorkOrderDet.ReportedBy;
                     model.ContactPhone = getWorkOrderDet.ContactPhone;
                     model.AssignedTo = getWorkOrderDet.AssignedTo;
                     model.VendorID = getWorkOrderDet.VendorID.HasValue ? getWorkOrderDet.VendorID.Value : 0;
                    model.Resolution = getWorkOrderDet.Resolution;
                }
            }
            return model;
        }
        public string SaveUpdateWorkOrder(WorkOrderModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.WOID == 0)
            {
                var saveWorkorder = new tbl_WorkOrder()
                {
                    WOID = model.WOID,
                    TenantID =long.Parse(userid.ToString()),
                    PropertyID = model.PropertyID,
                    ProblemID = model.ProblemID,
                    UnitID = model.UnitID,
                    DateOpened = model.DateOpened,
                    DateClosed = model.DateClosed,
                    Description = model.Description,
                    ReportedBy = model.ReportedBy,
                    ContactPhone = model.ContactPhone,
                    AssignedTo = model.AssignedTo,
                    VendorID = model.VendorID,
                    Resolution = model.Resolution

                };
                db.tbl_WorkOrder.Add(saveWorkorder);
                db.SaveChanges();

                
                msg = "Work order Saved Successfully";
            }
            else
            {
                var getWOdata = db.tbl_WorkOrder.Where(p => p.WOID == model.WOID).FirstOrDefault();
                if(getWOdata!=null)
                {
                    getWOdata.PropertyID = model.PropertyID;
                    getWOdata.ProblemID = model.ProblemID;
                    getWOdata.UnitID = model.UnitID;
                    getWOdata.DateOpened = model.DateOpened;
                    getWOdata.DateClosed = model.DateClosed;
                    getWOdata.Description = model.Description;
                    getWOdata.ReportedBy = model.ReportedBy;
                    getWOdata.ContactPhone = model.ContactPhone;
                    getWOdata.AssignedTo = model.AssignedTo;
                    getWOdata.VendorID = model.VendorID;
                    getWOdata.Resolution = model.Resolution;

                }
                db.SaveChanges();
                msg = "Work order Updated Successfully";
            }
            
            db.Dispose();
            return msg;


        }
       
    }

    public class TenantPropertyModel
    {
        public long PID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string LocationGoogle { get; set; }
        public Nullable<int> Garages { get; set; }
        public string BuiltIn { get; set; }
        public string Parking { get; set; }
        public string Waterfront { get; set; }
        public string Amenities { get; set; }
        public string Picture { get; set; }
        public string YouTube { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> NoOfUnits { get; set; }
        public Nullable<int> NoOfFloors { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
       
    }
     public partial class TenantPropertyUnits
    {
        public long UID { get; set; }
        public Nullable<long> PID { get; set; }
        public string UnitNo { get; set; }
        public string Wing { get; set; }
        public string Building { get; set; }
        public Nullable<int> FloorNo { get; set; }
        public decimal Current_Rent { get; set; }
        public decimal Previous_Rent { get; set; }
        public decimal Market_Rent { get; set; }
        public decimal Deposit { get; set; }
        public string Leased { get; set; }
        public int Rooms { get; set; }
        public Nullable<int> Bedroom { get; set; }
        public Nullable<int> Bathroom { get; set; }
        public Nullable<int> Hall { get; set; }
        public int Den { get; set; }
        public int Furnished { get; set; }
        public int Fireplace { get; set; }
        public int Carpet { get; set; }
        public string Carpet_Color { get; set; }
        public string Wall_Paint_Color { get; set; }
        public int Drapes { get; set; }
        public int Air_Conditioning { get; set; }
        public int Range { get; set; }
        public int Gas_Range { get; set; }
        public int Elec_Range { get; set; }
        public int Washer_Hookup { get; set; }
        public int Dryer_Hookup { get; set; }
        public int Gas_Dryer_Hookup { get; set; }
        public int Elec_Dryer_Hookup { get; set; }
        public int Washer { get; set; }
        public int Dryer { get; set; }
        public int Refrigerator { get; set; }
        public int Dishwasher { get; set; }
        public int Disposal { get; set; }
        public string PetDetails { get; set; }
        public string Area { get; set; }
        public string FloorPlan { get; set; }
        public Nullable<System.DateTime> AvailableDate { get; set; }
        public string AvailableDateText { get; set; }
        public int PendingMoveIn { get; set; }
        public Nullable<System.DateTime> IntendedMoveIn_Date { get; set; }
        public Nullable<System.DateTime> ActualMoveInDate { get; set; }
        public int PendingMoveOut { get; set; }
        public Nullable<System.DateTime> IntendMoveOutDate { get; set; }
        public Nullable<System.DateTime> ActualMoveOutDate { get; set; }
        public Nullable<System.DateTime> VacancyLoss_Date { get; set; }
        public Nullable<System.DateTime> OccupancyDate { get; set; }
     
    }
}