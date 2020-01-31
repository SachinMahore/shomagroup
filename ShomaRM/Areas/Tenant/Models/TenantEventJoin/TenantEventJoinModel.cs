using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Tenant.Models
{
    public class TenantEventJoinModel
    {
        public long TEID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<long> EventID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
        public Nullable<decimal> Fees { get; set; }
        public string Description { get; set; }
        public string DateString { get; set; }
        public string TimeString { get; set; }
        public string TenantName { get; set; }
        public string EventName { get; set; }
        public string TenantEventJoinStatus { get; set; }
        public long? TEJAID { get; set; }

        public string SaveTenantEventJoin(TenantEventJoinModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var isAlreadyTenantEventJoin = db.tbl_TenantEventJoin.Where(co => co.EventID == model.EventID && co.TenantID == model.TenantID).FirstOrDefault();
            if (isAlreadyTenantEventJoin == null)
            {
                var saveTenantEventJoin = new tbl_TenantEventJoin()
                {
                    TenantID = model.TenantID,
                    EventID = model.EventID,
                    Date = DateTime.Now,
                    Time = DateTime.Now.TimeOfDay,
                    Fees = model.Fees,
                    Description = model.Description
                };
                db.tbl_TenantEventJoin.Add(saveTenantEventJoin);
                db.SaveChanges();
                msg = "Progress Saved";
            }
            else
            {
                msg = "You Already Registered For This Event";
            }
            db.Dispose();
            return msg;
        }

        public List<TenantEventJoinModel> GetTenantEventJoinList(int TenantEventListStatus)
        {
            List<TenantEventJoinModel> listTenantEventJoin = new List<TenantEventJoinModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "sp_GetTenantEventJoinList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "TenantEventListStatus";
                    paramC.Value = TenantEventListStatus;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantEventJoinModel model = new TenantEventJoinModel();
                    model.TEID = Convert.ToInt64(dr["TEID"].ToString());
                    model.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    model.EventID = Convert.ToInt64(dr["EventID"].ToString());
                    model.Date = Convert.ToDateTime(dr["EventDate"].ToString());
                    model.Fees = Convert.ToDecimal(dr["Fees"].ToString());
                    model.Description = dr["Description"].ToString();
                    model.DateString = dr["EventDate"].ToString();
                    model.TimeString = dr["EventTime"].ToString();
                    model.TenantName = dr["Name"].ToString();
                    model.EventName = dr["EventName"].ToString();
                    model.TenantEventJoinStatus = dr["status"].ToString();
                    model.TEJAID = Convert.ToInt64(dr["TEJAID"].ToString());
                    listTenantEventJoin.Add(model);
                }
                db.Dispose();
                return listTenantEventJoin.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public string AlreadyJoinTenantEvent(TenantEventJoinModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var isAlreadyTenantEventJoin = db.tbl_TenantEventJoin.Where(co => co.EventID == model.EventID && co.TenantID == model.TenantID).FirstOrDefault();
            if (isAlreadyTenantEventJoin != null)
            {
                msg = "You Already Registered For This Event";
            }
            else
            {
                msg = "";
            }
            db.Dispose();
            return msg;
        }
    }
}