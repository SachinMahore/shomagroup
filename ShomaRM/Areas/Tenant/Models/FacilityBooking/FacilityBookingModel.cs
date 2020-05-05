using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Tenant.Models
{
    public class FacilityBookingModel
    {
        public long FacilityBookingID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<int> FacilityID { get; set; }
        public string FacilityName { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public string BookingDateString { get; set; }
        public string BookingDetails { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> RequiredFromDate { get; set; }
        public string RequiredFromDateString { get; set; }
        public Nullable<System.DateTime> RequiredToDate { get; set; }
        public string RequiredToDateString { get; set; }

        public string SaveUpdateFacilityBooking(FacilityBookingModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            long tenantid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID : 0;
            if (model.FacilityBookingID == 0)
            {
                var saveFacilityBooking=new tbl_FacilityBooking()
                {
                    FacilityBookingID = model.FacilityBookingID,
                    TenantID = tenantid,
                    FacilityID = model.FacilityID,
                    BookingDate = model.BookingDate,
                    RequiredFromDate = model.RequiredFromDate,
                    RequiredToDate = model.RequiredToDate,
                    BookingDetails = model.BookingDetails,
                    CreatedByID = userid,
                    CreatedDate=DateTime.Now
                };
                db.tbl_FacilityBooking.Add(saveFacilityBooking);
                db.SaveChanges();
                msg = "Facility Booking Save Successfully";
            }
            else
            {
                var GetFacilityBookingData = db.tbl_FacilityBooking.Where(p => p.FacilityBookingID == model.FacilityBookingID).FirstOrDefault();

                if (GetFacilityBookingData != null)
                {
                    GetFacilityBookingData.FacilityBookingID = model.FacilityBookingID;
                    GetFacilityBookingData.TenantID = tenantid;
                    GetFacilityBookingData.FacilityID = model.FacilityID;
                    GetFacilityBookingData.BookingDate = model.BookingDate;
                    GetFacilityBookingData.RequiredFromDate = model.RequiredFromDate;
                    GetFacilityBookingData.RequiredToDate = model.RequiredToDate;
                    GetFacilityBookingData.BookingDetails = model.BookingDetails;
                    CreatedByID = userid;
                    CreatedDate = DateTime.Now;
                    db.SaveChanges();
                    msg = "Facility Booking Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public FacilityBookingModel GetFacilityBookingData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            FacilityBookingModel model = new FacilityBookingModel();

            var GetFacilityBookingData = db.tbl_FacilityBooking.Where(p => p.FacilityBookingID == Id).FirstOrDefault();

            if (GetFacilityBookingData != null)
            {
                model.FacilityBookingID = GetFacilityBookingData.FacilityBookingID;
                model.TenantID = GetFacilityBookingData.TenantID;
                model.FacilityID = GetFacilityBookingData.FacilityID;
                model.BookingDate = GetFacilityBookingData.BookingDate;
                model.RequiredFromDate = GetFacilityBookingData.RequiredFromDate;
                model.RequiredToDate = GetFacilityBookingData.RequiredToDate;
                model.BookingDetails = GetFacilityBookingData.BookingDetails;
            }
            model.FacilityBookingID = Id;
            return model;
        }

        public List<FacilityBookingModel> GetFacilityBookingListData(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FacilityBookingModel> lstpr = new List<FacilityBookingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetFacilityBookingList";
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
                    FacilityBookingModel pr = new FacilityBookingModel();

                    DateTime? bookingDate = null;
                    try
                    {

                        bookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? requiredFromDate = null;
                    try
                    {

                        requiredFromDate = Convert.ToDateTime(dr["RequiredFromDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? requiredToDate = null;
                    try
                    {

                        requiredToDate = Convert.ToDateTime(dr["RequiredToDate"].ToString());
                    }
                    catch
                    {

                    }
                    pr.FacilityBookingID = Convert.ToInt64(dr["FacilityBookingID"].ToString());
                    pr.TenantID = Convert.ToInt32(dr["TenantID"].ToString());
                    pr.FacilityName = dr["FacilityName"].ToString();
                    pr.BookingDateString = bookingDate == null ? "" : bookingDate.Value.ToString("MM/dd/yyy");
                    pr.RequiredFromDateString = requiredFromDate == null ? "" : requiredFromDate.Value.ToString("MM/dd/yyy");
                    pr.RequiredToDateString = requiredToDate == null ? "" : requiredToDate.Value.ToString("MM/dd/yyy");
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