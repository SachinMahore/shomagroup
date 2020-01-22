using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;


namespace ShomaRM.Areas.Tenant.Models
{
    public class LeaseModel
    {
        public long LID { get; set; }
        public long PID { get; set; }
        public long UID { get; set; }
        public long TID { get; set; }
        public Nullable<int> Revision_Num { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Being_Terminated { get; set; }
        public string Phase { get; set; }
        public Nullable<System.DateTime> Notice_Date { get; set; }
        public string Termination_Reason { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> Previous_Balance { get; set; }
        public Nullable<int> Returned_Check_Limit { get; set; }
        public Nullable<int> Number_Returned_Checks { get; set; }
        public Nullable<System.DateTime> Last_Return_Ck_Date { get; set; }
        public Nullable<int> Last_Return_Pmt_ID { get; set; }
        public Nullable<int> Last_Return_Ck_Num { get; set; }
        public Nullable<System.DateTime> Original_Start { get; set; }
        public Nullable<System.DateTime> Actual_Start { get; set; }
        public Nullable<System.DateTime> Original_Lease_End { get; set; }
        public Nullable<System.DateTime> Actual_Lease_End { get; set; }
        public Nullable<System.DateTime> Intended_MoveIn_Date { get; set; }
        public Nullable<System.DateTime> Actual_MoveIn_Date { get; set; }
        public Nullable<System.DateTime> Intend_MoveOut_Date { get; set; }
        public Nullable<System.DateTime> Actual_MoveOut_Date { get; set; }
        public string Statement_Type { get; set; }
        public Nullable<System.DateTime> Last_Rent_Roll_Date { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> Reconciled { get; set; }
        public string Term { get; set; }

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

        public LeaseModel GetLeaseInformation(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            LeaseModel model = new LeaseModel();
            if (id != 0)
            {
                var getLeaseInfo = db.tbl_Lease.Where(p => p.TenantID == id).FirstOrDefault();
                if (getLeaseInfo != null)
                {
                    model.LID = getLeaseInfo.LID;
                    model.PID =getLeaseInfo.PID.HasValue ? getLeaseInfo.PID.Value : 0; ;
                    model.UID = getLeaseInfo.UID.HasValue ? getLeaseInfo.UID.Value : 0; ;
                    model.TID = getLeaseInfo.TenantID;
                    model.Revision_Num =getLeaseInfo.Revision_Num;
                    model.Status = getLeaseInfo.Status;
                    model.Being_Terminated = getLeaseInfo.Being_Terminated;
                    model.Phase = getLeaseInfo.Phase;
                    model.Notice_Date = getLeaseInfo.Notice_Date;
                    model.Termination_Reason =getLeaseInfo.Termination_Reason;
                    model.Balance = getLeaseInfo.Balance;
                    model.Previous_Balance = getLeaseInfo.Previous_Balance;
                    model.Returned_Check_Limit = getLeaseInfo.Returned_Check_Limit;
                    model.Number_Returned_Checks = getLeaseInfo.Number_Returned_Checks;
                    model.Last_Return_Ck_Date = getLeaseInfo.Last_Return_Ck_Date;
                    model.Last_Return_Pmt_ID = getLeaseInfo.Last_Return_Pmt_ID;
                    model.Last_Return_Ck_Num = getLeaseInfo.Last_Return_Ck_Num;
                    model.Original_Start = getLeaseInfo.Original_Start;
                    model.Actual_Start = getLeaseInfo.Actual_Start;
                    model.Original_Lease_End =getLeaseInfo.Original_Lease_End;
                    model.Actual_Lease_End   = getLeaseInfo.Actual_Lease_End;
                    model.Intended_MoveIn_Date = getLeaseInfo.Intended_MoveIn_Date;
                    model.Actual_MoveIn_Date = getLeaseInfo.Actual_MoveIn_Date;
                    model.Intend_MoveOut_Date = getLeaseInfo.Intend_MoveOut_Date;
                    model.Actual_MoveOut_Date = getLeaseInfo.Actual_MoveOut_Date;
                    model.Statement_Type = getLeaseInfo.Statement_Type;
                    model.Last_Rent_Roll_Date = getLeaseInfo.Last_Rent_Roll_Date;
                    model.CreatedBy = getLeaseInfo.CreatedBy;
                    model.CreatedDate = getLeaseInfo.CreatedDate;
                    model.Reconciled = getLeaseInfo.Reconciled;
                    model.Term = getLeaseInfo.Term;

                }
            }
            return model;
        }

        public string SaveUpdateLease(LeaseModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            
                var getLIdata = db.tbl_Lease.Where(p => p.LID == model.LID).FirstOrDefault();
                if (getLIdata != null)
                {
                  
                    getLIdata.PID = model.PID;
                    getLIdata.UID = model.UID;
                    
                    getLIdata.Revision_Num = model.Revision_Num;
                    getLIdata.Status = model.Status;
                    getLIdata.Being_Terminated = model.Being_Terminated;
                    getLIdata.Phase = model.Phase;
                    getLIdata.Notice_Date = model.Notice_Date;
                    getLIdata.Termination_Reason = model.Termination_Reason;
                    getLIdata.Balance = model.Balance;
                    getLIdata.Previous_Balance = model.Previous_Balance;
                    getLIdata.Returned_Check_Limit = model.Returned_Check_Limit;
                    getLIdata.Number_Returned_Checks = model.Number_Returned_Checks;
                    getLIdata.Last_Return_Ck_Date = model.Last_Return_Ck_Date;
                    getLIdata.Last_Return_Pmt_ID = model.Last_Return_Pmt_ID;
                    getLIdata.Last_Return_Ck_Num = model.Last_Return_Ck_Num;
                    getLIdata.Original_Start = model.Original_Start;
                    getLIdata.Actual_Start = model.Actual_Start;
                    getLIdata.Original_Lease_End = model.Original_Lease_End;
                    getLIdata.Actual_Lease_End = model.Actual_Lease_End;
                    getLIdata.Intended_MoveIn_Date = model.Intended_MoveIn_Date;
                    getLIdata.Actual_MoveIn_Date = model.Actual_MoveIn_Date;
                    getLIdata.Intend_MoveOut_Date = model.Intend_MoveOut_Date;
                    getLIdata.Actual_MoveOut_Date = model.Actual_MoveOut_Date;
                    getLIdata.Statement_Type = model.Statement_Type;
                    getLIdata.Last_Rent_Roll_Date = model.Last_Rent_Roll_Date;
                  
                    getLIdata.Reconciled = model.Reconciled;
                    getLIdata.Term = model.Term;
                    

                }
                db.SaveChanges();
                msg = "Lease Information Updated Successfully";
          
            db.Dispose();
            return msg;
        }
    }
}