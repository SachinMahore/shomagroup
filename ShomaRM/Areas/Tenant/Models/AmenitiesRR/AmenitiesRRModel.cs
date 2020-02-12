using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ShomaRM.Data;
using System.Data.Common;
using System.IO;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Tenant.Models
{
    public class AmenitiesRRModel
    {
    }
    public class AmenitiesReservationModel
    {
        public long ARID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<long> AmenityID { get; set; }
        public Nullable<System.DateTime> DesiredDate { get; set; }
        public string DesiredDateString { get; set; }
        public string DesiredTime { get; set; }
        public Nullable<long> DurationID { get; set; }
        public string DepositFee { get; set; }
        public string ReservationFee { get; set; }
        public Nullable<int> Status { get; set; }
        public string Duration { get; set; }
        public string DesiredTimeFrom { get; set; }
        public string DesiredTimeTo { get; set; }
        public Nullable<long> Guest { get; set; }
        public string AmenityName { get; set; }
        public string TenantName { get; set; }

        public string SaveUpdateReservationRequest(AmenitiesReservationModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            DateTime dt = DateTime.Parse(model.DesiredTime != null ? model.DesiredTime : "00:00");
            DateTime dtF = DateTime.Parse(model.DesiredTimeFrom != null ? model.DesiredTimeFrom : "00:00");
            DateTime dtT = DateTime.Parse(model.DesiredTimeTo != null ? model.DesiredTimeTo : "00:00");

            if (model.ARID == 0)
            {
                var saveReservationRequest = new tbl_AmenityReservation()
                {
                    TenantID = model.TenantID,
                    AmenityID = model.AmenityID,
                    DesiredDate = model.DesiredDate,
                    DesiredTime = dt.ToString("HH:mm"),
                    Duration = model.Duration,
                    DurationID = model.DurationID,
                    DepositFee = model.DepositFee,
                    ReservationFee = model.ReservationFee,
                    Status = model.Status,
                    DesiredTimeFrom = dtF.ToString("HH:mm"),
                    DesiredTimeTo = dtT.ToString("HH:mm"),
                    Guest = model.Guest
                };
                db.tbl_AmenityReservation.Add(saveReservationRequest);
                db.SaveChanges();
                msg = "Reservation Request Save Successfully";
            }
            else
            {
                var GetReservationRequestData = db.tbl_AmenityReservation.Where(p => p.TenantID == model.TenantID).FirstOrDefault();
                if (GetReservationRequestData != null)
                {
                    GetReservationRequestData.AmenityID = model.AmenityID;
                    GetReservationRequestData.DesiredDate = model.DesiredDate;
                    GetReservationRequestData.DesiredTime= dt.ToString("HH:mm");
                    GetReservationRequestData.Duration = model.Duration;
                    GetReservationRequestData.DurationID = model.DurationID;
                    GetReservationRequestData.DepositFee = model.DepositFee;
                    GetReservationRequestData.ReservationFee = model.ReservationFee;
                    GetReservationRequestData.Status= model.Status;
                    GetReservationRequestData.DesiredTimeFrom = model.DesiredTimeFrom;
                    GetReservationRequestData.DesiredTimeTo = model.DesiredTimeTo;
                    GetReservationRequestData.Guest = model.Guest;
                   

                    
                    db.SaveChanges();
                    msg = "Reservation Request Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public AmenitiesReservationModel GetRRData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            AmenitiesReservationModel model = new AmenitiesReservationModel();

            var GetRRData = db.tbl_AmenityReservation.Where(p => p.ARID == Id).FirstOrDefault();
            if (GetRRData != null)
            {
                model.AmenityID = GetRRData.AmenityID;
                model.DesiredDate = GetRRData.DesiredDate;
                model.DesiredTime = GetRRData.DesiredTime;
                model.Duration = GetRRData.Duration;
                model.DurationID = GetRRData.DurationID;
                model.DepositFee = GetRRData.DepositFee;
                model.ReservationFee = GetRRData.ReservationFee;
                model.Status = GetRRData.Status;
                model.DesiredTimeFrom = GetRRData.DesiredTimeFrom;
                model.DesiredTimeTo = GetRRData.DesiredTimeTo;
                model.Guest = GetRRData.Guest;
            }
            model.ARID = Id;
            return model;
        }

        public AmenitiesReservationModel GetRRInfo(long Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            AmenitiesReservationModel model = new AmenitiesReservationModel();

            var GetRRData = db.tbl_AmenityReservation.Where(p => p.ARID == Id).FirstOrDefault();
            if (GetRRData != null)
            {
                model.TenantID = GetRRData.TenantID;
                model.AmenityID = GetRRData.AmenityID;
                model.DesiredDate = GetRRData.DesiredDate;
                model.DesiredDateString = GetRRData.DesiredDate != null ? GetRRData.DesiredDate.Value.ToString("MM/dd/yyyy"): "";
                DateTime dt = DateTime.Parse(GetRRData.DesiredTime != string.Empty ? GetRRData.DesiredTime : "00:00");
                model.DesiredTime = dt.ToString("hh:mm tt");
                model.Duration = GetRRData.Duration;
                model.DurationID = GetRRData.DurationID;
                model.DepositFee = GetRRData.DepositFee;
                model.ReservationFee = GetRRData.ReservationFee;
                model.Status = GetRRData.Status;
                model.DesiredTimeFrom = GetRRData.DesiredTimeFrom;
                model.DesiredTimeTo = GetRRData.DesiredTimeTo;
                model.Guest = GetRRData.Guest;


            }

            var GetAmenityData = db.tbl_Amenities.Where(p => p.ID == model.AmenityID).FirstOrDefault();
            if (GetAmenityData != null)
            {
                model.AmenityName = GetAmenityData.Amenity;
            }

            var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == model.TenantID).FirstOrDefault();
            if (GetTenantData != null)
            {
                model.TenantName = GetTenantData != null ? GetTenantData.FirstName + " " + GetTenantData.LastName : "";
            }
            model.ARID = Id;
            return model;
        }

        public List<AmenitiesReservationModel> GetReservationRequestList()
        {

            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenitiesReservationModel> listReservationRequest = new List<AmenitiesReservationModel>();
            var requestList = db.tbl_AmenityReservation.ToList();
            if (requestList != null)
            {
                foreach (var item in requestList)
                {

                    listReservationRequest.Add(new AmenitiesReservationModel()
                    {
                        ARID = item.ARID,
                        TenantID = item.TenantID,
                        AmenityID = item.AmenityID,
                        DesiredDate = item.DesiredDate,
                        DesiredTime = item.DesiredTime,
                        Duration = item.Duration,
                        DurationID = item.DurationID,
                        DepositFee = item.DepositFee,
                        ReservationFee = item.ReservationFee,
                        Status = item.Status,
                        DesiredTimeFrom = item.DesiredTimeFrom,
                        DesiredTimeTo = item.DesiredTimeTo,
                        Guest = item.Guest

                });
                }
            }

            db.Dispose();
            return listReservationRequest;
        }

        public List<AmenitiesReservationSearchModel> FillRRList(AmenitiesReservationModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenitiesReservationSearchModel> lstReservationRequest = new List<AmenitiesReservationSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_ReservationRequestList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "id";
                    param0.Value = model.TenantID;
                    cmd.Parameters.Add(param0);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    AmenitiesReservationSearchModel searchmodel = new AmenitiesReservationSearchModel();

                    searchmodel.ARID = Convert.ToInt64(dr["ARID"].ToString());
                    searchmodel.TenantID = Convert.ToInt64(dr["AmenityID"].ToString());
                    searchmodel.AmenityID = Convert.ToInt64(dr["AmenityID"].ToString());
                    searchmodel.DesiredDate = dr["DesiredDate"].ToString();
                    string time = dr["DesiredTime"].ToString();
                    DateTime dt = DateTime.Parse(time != string.Empty ? time : "00:00 AM");
                    searchmodel.DesiredTime = dt.ToString("hh:mm tt");
                    searchmodel.DurationID = dr["DurationID"].ToString();
                    searchmodel.DepositFee = dr["DepositFee"].ToString();
                    searchmodel.ReservationFee = dr["ReservationFee"].ToString();
                    searchmodel.Status = dr["Status"].ToString() == null ? "" : dr["Status"].ToString();
                    searchmodel.Duration = dr["Duration"].ToString();
                    searchmodel.TenantName = dr["TenantName"].ToString();
                    searchmodel.AmenityName = dr["AmenityName"].ToString();

                    searchmodel.DesiredTimeFrom = dr["DesiredTimeFrom"].ToString();
                    searchmodel.DesiredTimeTo = dr["DesiredTimeTo"].ToString();
                    searchmodel.Guest = dr["Guest"].ToString();

                    lstReservationRequest.Add(searchmodel);
                }
                db.Dispose();
                return lstReservationRequest.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public class AmenitiesReservationSearchModel
        {
            public long ARID { get; set; }
            public long TenantID { get; set; }
            public long AmenityID { get; set; }
            public string DesiredDate { get; set; }
            public string DesiredTime { get; set; }
            public string DurationID { get; set; }
            public string DepositFee { get; set; }
            public string ReservationFee { get; set; }
            public string Status { get; set; }
            public string Duration { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
            public string TenantName { get; set; }
            public string AmenityName { get; set; }
            public string DesiredTimeFrom { get; set; }
            public string DesiredTimeTo { get; set; }
            public string Guest { get; set; }

        }

        public int BuildPaganationRRList(AmenitiesReservationSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetReservationRequestPaginationAndSearchData";
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
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

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
        public List<AmenitiesReservationSearchModel> FillRRSearchGrid(AmenitiesReservationSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenitiesReservationSearchModel> lstReservationRequest = new List<AmenitiesReservationSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetReservationRequestPaginationAndSearchData";
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
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    AmenitiesReservationSearchModel searchmodel = new AmenitiesReservationSearchModel();

                    searchmodel.ARID = Convert.ToInt64(dr["ARID"].ToString());
                    searchmodel.TenantID = Convert.ToInt64(dr["AmenityID"].ToString());
                    searchmodel.AmenityID = Convert.ToInt64(dr["AmenityID"].ToString());
                    searchmodel.DesiredDate = dr["DesiredDate"].ToString();
                    string time = dr["DesiredTime"].ToString();
                    DateTime dt = DateTime.Parse(time != string.Empty ? time : "00:00 AM");
                    searchmodel.DesiredTime = dt.ToString("hh:mm tt");
                    searchmodel.DurationID = dr["DurationID"].ToString();
                    searchmodel.DepositFee = dr["DepositFee"].ToString();
                    searchmodel.ReservationFee = dr["ReservationFee"].ToString();
                    searchmodel.Status = dr["Status"].ToString()==null? "": dr["Status"].ToString();
                    searchmodel.Duration = dr["Duration"].ToString();
                    searchmodel.TenantName = dr["TenantName"].ToString();
                    searchmodel.AmenityName = dr["AmenityName"].ToString();
                    searchmodel.DesiredTimeFrom = dr["DesiredTimeFrom"].ToString();
                    searchmodel.DesiredTimeTo = dr["DesiredTimeTo"].ToString();
                    searchmodel.Guest = dr["Guest"].ToString();

                    lstReservationRequest.Add(searchmodel);
                }
                db.Dispose();
                return lstReservationRequest.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public string SaveUpdateAmenityRequestStatus(string AmenityName, int ARID, int Status)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (ARID != 0)
          
            {
                var GetARRData = db.tbl_AmenityReservation.Where(p => p.ARID == ARID).FirstOrDefault();
                if (GetARRData != null)
                {

                    GetARRData.Status = Status;
                    db.SaveChanges();
                    msg = "Reservation Request Status Updated Successfully";

                    var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == GetARRData.TenantID).FirstOrDefault();
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                    if (GetTenantData != null)
                    {
                        DateTime? arDate = null;
                        try
                        {

                            arDate = Convert.ToDateTime(GetARRData.DesiredDate);
                        }
                        catch
                        {

                        }
                        var tm = new MyTransactionModel();
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Request Status");
                        reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                        if(Status==1)
                        {
                            reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We hereby approve your reservation of the " + AmenityName + " (facility) on " + arDate.Value.ToString("MM/dd/yyyy") + " (date) at " + GetARRData.DesiredTime + " (time) for a total of " + GetARRData.Duration + " (hours).  For your reservation to be confirmed, the security deposit and reservation 	fee must be paid.  If you desire to make your payment now, kindly follow this link.  Please note 	that the date will not be reserved in the system until payment is received. </p>");
                            reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"http://52.4.251.162:8086/PayLink/PayAmenityCharges/?ARID=" + ARID+ "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"http://52.4.251.162:8086/PayLink/PayAmenityCharges/?ARID=" + ARID + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");

                           
                        }
                        else if (Status == 2)
                        {
                            reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We hereby approve your reservation of the " + AmenityName + " (facility) on " + GetARRData.DesiredDate + " (date) at " + GetARRData.DesiredTime + " (time) for a total of " + GetARRData.Duration + " (hours).  For your reservation to be Completed. Please pay your deposit. If you desire to make your payment now, kindly follow this link.  Please note 	that the date will not be reserved in the system until payment is received. </p>");
                            reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"http://52.4.251.162:8086/PayLink/PayAmenityCharges/?ARID=" + ARID + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"http://52.4.251.162:8086/PayLink/PayAmenityCharges/?ARID=" + ARID + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");


                        }
                        else
                        {
                            reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We are sorry for your reservation of the " + AmenityName + " (facility) on " + GetARRData.DesiredDate + " (date) at " + GetARRData.DesiredTime + " (time) for a total of " + GetARRData.Duration + " (hours). Your reservation date is not be available. </p>");
                            reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                        }

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", GetTenantData.Email);

                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Request Status", body);

                }
            }
            db.Dispose();
            return msg;
        }

        public string CancleReservationRequest(int ARID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var GetReservationRequestData = db.tbl_AmenityReservation.Where(p => p.ARID == ARID).FirstOrDefault();
            if (GetReservationRequestData != null)
            {
                GetReservationRequestData.Status = 4;

                db.SaveChanges();
                msg = "Reservation Request Cancelled Successfully";
            }
            db.Dispose();
            return msg;
        }
    }
}