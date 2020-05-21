using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Tenant.Models
{
    public class EventBookingModel
    {
        public long EventBookingID { get; set; }
        public long TenantID { get; set; }
        public long EventID { get; set; }
        public string EventName { get; set; }
        public System.DateTime BookingDate { get; set; }
        public string BookingDateString { get; set; }
        public string BookingDetails { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> NoOfGuest { get; set; }


        public string SaveUpdateEventBooking(EventBookingModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            long tenantid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID : 0;
            if (model.EventBookingID == 0)
            {
                var saveEventBooking = new tbl_EventBooking()
                {
                    EventBookingID = model.EventBookingID,
                    TenantID = tenantid,
                    EventID = model.EventID,
                    BookingDate = model.BookingDate,
                    NoOfGuest = model.NoOfGuest,
                    BookingDetails = model.BookingDetails,
                    CreatedByID = userid,
                    CreatedDate = DateTime.Now
                };
                db.tbl_EventBooking.Add(saveEventBooking);
                db.SaveChanges();
                msg = "Event Booking Save Successfully";
            }
            else
            {
                var GetEventBookingData = db.tbl_EventBooking.Where(p => p.EventBookingID == model.EventBookingID).FirstOrDefault();

                if (GetEventBookingData != null)
                {
                    GetEventBookingData.EventBookingID = model.EventBookingID;
                    GetEventBookingData.TenantID = tenantid;
                    GetEventBookingData.EventID = model.EventID;
                    GetEventBookingData.BookingDate = model.BookingDate;
                    GetEventBookingData.NoOfGuest = model.NoOfGuest;
                    GetEventBookingData.BookingDetails = model.BookingDetails;
                    CreatedByID = userid;
                    CreatedDate = DateTime.Now;
                    db.SaveChanges();
                    msg = "Event Booking Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public EventBookingModel GetEventBookingData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EventBookingModel model = new EventBookingModel();

            var GetEventBookingData = db.tbl_EventBooking.Where(p => p.EventBookingID == Id).FirstOrDefault();

            if (GetEventBookingData != null)
            {
                model.EventBookingID = GetEventBookingData.EventBookingID;
                model.TenantID = GetEventBookingData.TenantID.HasValue? GetEventBookingData.TenantID.Value:0;
                model.EventID = GetEventBookingData.EventID.HasValue ? GetEventBookingData.EventID.Value : 0;
                model.BookingDate = GetEventBookingData.BookingDate;
                model.NoOfGuest = GetEventBookingData.NoOfGuest;
                model.BookingDetails = GetEventBookingData.BookingDetails;
            }
            model.EventBookingID = Id;
            return model;
        }

        public List<EventBookingModel> GetEventBookingListData(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<EventBookingModel> lstpr = new List<EventBookingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetEventBookingList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramD = cmd.CreateParameter();
                    paramD.ParameterName = "UserId";
                    paramD.Value = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                    cmd.Parameters.Add(paramD);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    EventBookingModel pr = new EventBookingModel();

                    DateTime? bookingDate = null;
                    try
                    {

                        bookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                    }
                    catch
                    {

                    }

                    pr.EventBookingID = Convert.ToInt64(dr["EventBookingID"].ToString());
                    pr.TenantID = Convert.ToInt32(dr["TenantID"].ToString());
                    pr.EventName = dr["EventName"].ToString();
                    pr.BookingDateString = bookingDate == null ? "" : bookingDate.Value.ToString("MM/dd/yyy");
                    pr.NoOfGuest = Convert.ToInt32(dr["NoOfGuest"].ToString());
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
    }
}