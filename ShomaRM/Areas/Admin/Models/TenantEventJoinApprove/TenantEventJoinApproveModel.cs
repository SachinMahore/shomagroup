using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Models
{
    public class TenantEventJoinApproveModel
    {
        public long TEJAID { get; set; }
        public Nullable<long> EventID { get; set; }
        public Nullable<long> EventJoinID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public string SaveTenantEventJoinApprove(long TEID, int TenantEventListStatus)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var getTenantEventJoinData = db.tbl_TenantEventJoin.Where(co => co.TEID == TEID).FirstOrDefault();
            if (getTenantEventJoinData != null)
            {
                var saveTenantEventJoinApprove = new tbl_TenantEventJoinApprove()
                {
                    EventID = getTenantEventJoinData.EventID,
                    EventJoinID = getTenantEventJoinData.TEID,
                    TenantID = getTenantEventJoinData.TenantID,
                    Status = TenantEventListStatus,
                    Date = DateTime.Now
                };
                db.tbl_TenantEventJoinApprove.Add(saveTenantEventJoinApprove);
                db.SaveChanges();
                msg = "Progress Saved";
            }
            db.Dispose();
            return msg;
        }

        public string DeleteTenantEventJoinApprove(long TEJAID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var delTenantEventJoinApproveData = db.tbl_TenantEventJoinApprove.Where(co => co.TEJAID == TEJAID).FirstOrDefault();
            if (delTenantEventJoinApproveData != null)
            {
                db.tbl_TenantEventJoinApprove.Remove(delTenantEventJoinApproveData);
                db.SaveChanges();
                msg = "Removed Successfully";
            }
            db.Dispose();
            return msg;
        }
    }
}