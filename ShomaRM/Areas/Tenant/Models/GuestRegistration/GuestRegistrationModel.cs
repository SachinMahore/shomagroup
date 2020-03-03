using ShomaRM.Areas.Admin.Models;
using ShomaRM.Data;
using ShomaRM.Models.TwilioApi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ShomaRM.Areas.Tenant.Models
{
    public class GuestRegistrationModel
    {
        public long GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> VisitStartDate { get; set; }
        public Nullable<System.DateTime> VisitEndDate { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string Tag { get; set; }
        public string DriverLicence { get; set; }
        public string VehicleRegistration { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string OriginalDriverLicence { get; set; }
        public string OriginalVehicleRegistration { get; set; }
        public string TenantName { get; set; }
        public string VisitStartDateString { get; set; }
        public string VisitEndDateString { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UnitNo { get; set; }
        public string GuestName { get; set; }
        public Nullable<bool> HaveVehicle { get; set; }
        public string HaveVehicleString { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public string StatusString { get; set; }
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];

        public GuestRegistrationModel UploadGuestDriverLicence(HttpPostedFileBase fileBaseGuestDriverLicence, GuestRegistrationModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseGuestDriverLicence != null && fileBaseGuestDriverLicence.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/TenantGuestInformation/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseGuestDriverLicence.FileName;
                Extension = Path.GetExtension(fileBaseGuestDriverLicence.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseGuestDriverLicence.FileName);
                fileBaseGuestDriverLicence.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseGuestDriverLicence.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/TenantGuestInformation/") + "/" + sysFileName;
                }
                model.DriverLicence = sysFileName;
                model.OriginalDriverLicence = fileName;
            }
            return model;
        }

        public GuestRegistrationModel UploadGuestVehicleRegistration(HttpPostedFileBase fileBaseGuestVehicleRegistration, GuestRegistrationModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseGuestVehicleRegistration != null && fileBaseGuestVehicleRegistration.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/TenantGuestInformation/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseGuestVehicleRegistration.FileName;
                Extension = Path.GetExtension(fileBaseGuestVehicleRegistration.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseGuestVehicleRegistration.FileName);
                fileBaseGuestVehicleRegistration.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseGuestVehicleRegistration.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/TenantGuestInformation/") + "/" + sysFileName;
                }
                model.VehicleRegistration = sysFileName;
                model.OriginalVehicleRegistration = fileName;
            }
            return model;
        }

        public string SaveUpdateGuestRegistration(GuestRegistrationModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string Msg = "";
            if (model.GuestID == 0)
            {
                var saveGuestRegistration = new tbl_GuestRegistration()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Email = model.Email,
                    VisitStartDate = model.VisitStartDate,
                    VisitEndDate = model.VisitEndDate,
                    VehicleMake = model.VehicleMake,
                    VehicleModel = model.VehicleModel,
                    Tag = model.Tag,
                    DriverLicence = model.DriverLicence,
                    VehicleRegistration = model.VehicleRegistration,
                    TenantID = model.TenantID,
                    OriginalDriverLicence = model.OriginalDriverLicence,
                    OriginalVehicleRegistration = model.OriginalVehicleRegistration,
                    HaveVehicle = model.HaveVehicle,
                    Status=0
                };
                db.tbl_GuestRegistration.Add(saveGuestRegistration);
                db.SaveChanges();
                var guestID = saveGuestRegistration.GuestID;
                db.Dispose();
                Msg = "Progress Saved | " + guestID;
            }
            else
            {
                var updateGuestRegistration = db.tbl_GuestRegistration.Where(co => co.GuestID == model.GuestID).FirstOrDefault();

                if (updateGuestRegistration != null)
                {
                    updateGuestRegistration.FirstName = model.FirstName;
                    updateGuestRegistration.LastName = model.LastName;
                    updateGuestRegistration.Address = model.Address;
                    updateGuestRegistration.Phone = model.Phone;
                    updateGuestRegistration.Email = model.Email;
                    updateGuestRegistration.VisitStartDate = model.VisitStartDate;
                    updateGuestRegistration.VisitEndDate = model.VisitEndDate;
                    updateGuestRegistration.VehicleMake = model.VehicleMake;
                    updateGuestRegistration.VehicleModel = model.VehicleModel;
                    updateGuestRegistration.Tag = model.Tag;
                    updateGuestRegistration.DriverLicence = model.DriverLicence;
                    updateGuestRegistration.VehicleRegistration = model.VehicleRegistration;
                    updateGuestRegistration.TenantID = model.TenantID;
                    updateGuestRegistration.OriginalDriverLicence = model.OriginalDriverLicence;
                    updateGuestRegistration.OriginalVehicleRegistration = model.OriginalVehicleRegistration;
                    updateGuestRegistration.HaveVehicle = model.HaveVehicle;
                    db.SaveChanges();
                    db.Dispose();
                    Msg = "Progress Updated";
                }
            }
            return Msg;
        }

        public GuestRegistrationModel goToGuestDetails(long GuestID)
        {
            GuestRegistrationModel model = new GuestRegistrationModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var getTenantGuest = db.tbl_GuestRegistration.Where(co => co.GuestID == GuestID).FirstOrDefault();
            if (getTenantGuest != null)
            {
                var tenantInfo = db.tbl_TenantInfo.Where(co => co.TenantID == getTenantGuest.TenantID).FirstOrDefault();

                if (tenantInfo != null)
                {
                    var getUnit = db.tbl_PropertyUnits.Where(co => co.UID == tenantInfo.UnitID).FirstOrDefault();
                    if (getUnit != null)
                    {
                        model.UnitNo = getUnit.UnitNo;
                    }
                   
                    model.TenantName = tenantInfo.FirstName + " " + tenantInfo.LastName;
                    model.GuestName = getTenantGuest.FirstName + " " + getTenantGuest.LastName;
                    model.VehicleMake = getTenantGuest.VehicleMake;
                    model.VehicleModel = getTenantGuest.VehicleModel;
                    model.Tag = getTenantGuest.Tag;
                    model.Email = getTenantGuest.Email;
                    model.FirstName = getTenantGuest.FirstName;
                    model.LastName = getTenantGuest.LastName;
                    model.Address = getTenantGuest.Address;
                    model.Phone = getTenantGuest.Phone;
                    model.VisitStartDateString = getTenantGuest.VisitStartDate.Value.ToString("MM/dd/yyyy");
                    model.VisitEndDateString = getTenantGuest.VisitEndDate.Value.ToString("MM/dd/yyyy");
                    model.OriginalDriverLicence = getTenantGuest.OriginalDriverLicence;
                    model.DriverLicence = getTenantGuest.DriverLicence;
                    model.OriginalVehicleRegistration = getTenantGuest.OriginalVehicleRegistration;
                    model.VehicleRegistration = getTenantGuest.VehicleRegistration;
                    model.HaveVehicleString = getTenantGuest.HaveVehicle == true ? "Yes" : getTenantGuest.HaveVehicle == false ? "No" : "";
                    model.Status = getTenantGuest.Status;
                    model.StatusString = getTenantGuest.Status == 1 ? "Approved" : getTenantGuest.Status == 2 ? "Decline" : getTenantGuest.Status == 0 ? "" : ""; 

                }
            }
            db.Dispose();
            return model;
        }

        public string StatusUpdate(GuestRegistrationModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var UpdateStatus = db.tbl_GuestRegistration.Where(co => co.GuestID == model.GuestID).FirstOrDefault();

            if (UpdateStatus != null)
            {
                UpdateStatus.Status = model.Status;
                UpdateStatus.ApprovedBy = Convert.ToInt32(ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID);

                db.SaveChanges();
                msg = "Guest Status Update Successfully";

                var tenantInfo = db.tbl_TenantInfo.Where(co => co.TenantID == UpdateStatus.TenantID).FirstOrDefault();
                string message = "";
                string phonenumber = tenantInfo.Mobile;
                if (model.Status != 2)
                {
                    if (model.HaveVehicleString == "Yes")
                    {
                        string reportHTML = "";
                        string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                        reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");

                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                        reportHTML = reportHTML.Replace("[%TenantName%]", model.TenantName);
                        reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We thank you for your request and  We are pleased to confirm your guest reservation on " + model.VisitStartDateString + " to " + model.VisitEndDateString + "  for guest " + model.GuestName + "and your Tag Information is " + model.Tag + " </p>");
                        reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");

                        string body = reportHTML;
                        new EmailSendModel().SendEmail(tenantInfo.Email, "Your Reuest for Guest Registration  is Confirmed ", body);
                        message = "Thank you for your guest reservation request. We will inform you on email. Please check the email for detail.";
                        if (SendMessage == "yes")
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
                    }
                    else
                    {
                        string reportHTMLTag = "";
                        string filePathTag = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                        reportHTMLTag = System.IO.File.ReadAllText(filePathTag + "EmailTemplateAmenity.html");

                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                        reportHTMLTag = reportHTMLTag.Replace("[%TenantName%]", model.TenantName);
                        reportHTMLTag = reportHTMLTag.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We thank you for your request and  We are pleased to confirm your guest reservation on " + model.VisitStartDateString + " to " + model.VisitEndDateString + " for guest " + model.GuestName + ".  </p>");
                        reportHTMLTag = reportHTMLTag.Replace("[%LeaseNowButton%]", "");

                        string bodyTAg = reportHTMLTag;
                        new EmailSendModel().SendEmail(tenantInfo.Email, "Your Reuest for Guest Registration  is Confirmed. ", bodyTAg);
                        message = "Thank you for your guest reservation request. We will inform you on email. Please check the email for detail.";
                        if (SendMessage == "yes")
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }

                    }
                }
                else
                {

                    string reportHTMLTag = "";
                    string filePathTag = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTMLTag = System.IO.File.ReadAllText(filePathTag + "EmailTemplateAmenity.html");

                    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                    reportHTMLTag = reportHTMLTag.Replace("[%TenantName%]", model.TenantName);
                    reportHTMLTag = reportHTMLTag.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; I would like to inform you that Your Request is Decline For Guest " + model.GuestName + " due to some reason please contact the Administrator. </p>");
                    reportHTMLTag = reportHTMLTag.Replace("[%LeaseNowButton%]", "");

                    string bodyTAg = reportHTMLTag;
                    new EmailSendModel().SendEmail(tenantInfo.Email, "Your Reuest for Guest Registration  is Decline. ", bodyTAg);
                    message = "Your status regarding guest reservation is sent on email. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }


            }

            db.Dispose();
            return msg;
        }


        public GuestRegistrationModel gotiGuestList(long TagId)
        {
            GuestRegistrationModel model = new GuestRegistrationModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var getTenantGuest = db.tbl_GuestRegistration.Where(co => co.GuestID == TagId).FirstOrDefault();
            if (getTenantGuest != null)
            {
                var tenantInfo = db.tbl_TenantInfo.Where(co => co.TenantID == getTenantGuest.TenantID).FirstOrDefault();

                if (tenantInfo != null)
                {
                    var getUnit = db.tbl_PropertyUnits.Where(co => co.UID == tenantInfo.UnitID).FirstOrDefault();
                    if (getUnit != null)
                    {
                        model.UnitNo = getUnit.UnitNo;
                    }
                    model.TenantName = tenantInfo.FirstName + " " + tenantInfo.LastName;
                    model.GuestName = getTenantGuest.FirstName + " " + getTenantGuest.LastName;
                    model.VehicleMake = getTenantGuest.VehicleMake;
                    model.VehicleModel = getTenantGuest.VehicleModel;
                    model.Tag = getTenantGuest.Tag;
                    model.Email = getTenantGuest.Email;
                    model.FirstName = getTenantGuest.FirstName;
                    model.LastName = getTenantGuest.LastName;
                    model.Address = getTenantGuest.Address;
                    model.Phone = getTenantGuest.Phone;
                    model.VisitStartDateString = getTenantGuest.VisitStartDate.Value.ToString("MM/dd/yyyy");
                    model.VisitEndDateString = getTenantGuest.VisitEndDate.Value.ToString("MM/dd/yyyy");
                    model.OriginalDriverLicence = getTenantGuest.OriginalDriverLicence;
                    model.OriginalVehicleRegistration = getTenantGuest.OriginalVehicleRegistration;
                    model.HaveVehicleString = getTenantGuest.HaveVehicle == true ? "Yes" : getTenantGuest.HaveVehicle == false ? "No" : "";


                }
            }
            db.Dispose();
            return model;
        }
        public List<GuestRegistrationModel> GetGuestRegistrationList(DateTime FromDate, DateTime ToDate, string SortBy, string OrderBy)
        {
            List<GuestRegistrationModel> listGuestRegistration = new List<GuestRegistrationModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "sp_GetGuestRegistrationList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    GuestRegistrationModel pr = new GuestRegistrationModel();

                    pr.GuestID = Convert.ToInt64(dr["GuestId"].ToString());
                    pr.FirstName = dr["FirstName"].ToString();
                    pr.LastName = dr["LastName"].ToString();
                    pr.Address = dr["Address"].ToString();
                    pr.Phone = dr["Phone"].ToString();
                    pr.Email = dr["Email"].ToString();
                    pr.VisitStartDate = Convert.ToDateTime(dr["VisitStartDate"].ToString());
                    pr.VisitEndDate = Convert.ToDateTime(dr["VisitEndDate"].ToString());
                    pr.VehicleMake = dr["VehicleMake"].ToString();
                    pr.VehicleModel = dr["VehicleModel"].ToString();
                    pr.Tag = dr["Tag"].ToString();
                    pr.DriverLicence = dr["DriverLicence"].ToString();
                    pr.VehicleRegistration = dr["VehicleRegistration"].ToString();
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    pr.OriginalDriverLicence = dr["OriginalDriverLicence"].ToString();
                    pr.OriginalVehicleRegistration = dr["OriginalVehicleRegistration"].ToString();
                    pr.TenantName = dr["TenantName"].ToString();
                    pr.VisitStartDateString = dr["VisitStartDate"].ToString();
                    pr.VisitEndDateString = dr["VisitEndDate"].ToString();
                    pr.StatusString = dr["Status"].ToString();
                    listGuestRegistration.Add(pr);
                }
                db.Dispose();
                return listGuestRegistration.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public string DeleteGuestRegistrationList(long GuestID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var deleteGuestRegistration = db.tbl_GuestRegistration.Where(co => co.GuestID == GuestID).FirstOrDefault();
            if (deleteGuestRegistration != null)
            {
                db.tbl_GuestRegistration.Remove(deleteGuestRegistration);
                db.SaveChanges();
                msg = "Data Removed Successfully";
            }
            else
            {
                msg = "Data Not Remove";
            }
            return msg;
        }

        public List<GuestRegistrationModel> GetGuestList(GuestRegistrationModel model)
        {
            List<GuestRegistrationModel> listGuestRegistration = new List<GuestRegistrationModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {

                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetGuestList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "TenantID";
                    param1.Value = model.TenantID;
                    cmd.Parameters.Add(param1);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    GuestRegistrationModel pr = new GuestRegistrationModel();

                    pr.GuestID = Convert.ToInt64(dr["GuestId"].ToString());
                    pr.FirstName = dr["FirstName"].ToString();
                    pr.LastName = dr["LastName"].ToString();
                    pr.Address = dr["Address"].ToString();
                    pr.Phone = dr["Phone"].ToString();
                    pr.Email = dr["Email"].ToString();
                    pr.VisitStartDate = Convert.ToDateTime(dr["VisitStartDate"].ToString());
                    pr.VisitEndDate = Convert.ToDateTime(dr["VisitEndDate"].ToString());
                    pr.VehicleMake = dr["VehicleMake"].ToString();
                    pr.VehicleModel = dr["VehicleModel"].ToString();
                    pr.Tag = dr["Tag"].ToString();
                    pr.DriverLicence = dr["DriverLicence"].ToString();
                    pr.VehicleRegistration = dr["VehicleRegistration"].ToString();
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    pr.OriginalDriverLicence = dr["OriginalDriverLicence"].ToString();
                    pr.OriginalVehicleRegistration = dr["OriginalVehicleRegistration"].ToString();
                    pr.TenantName = dr["TenantName"].ToString();
                    pr.VisitStartDateString = dr["VisitStartDate"].ToString();
                    pr.VisitEndDateString = dr["VisitEndDate"].ToString();
                    pr.StatusString = dr["Status"].ToString();
                    listGuestRegistration.Add(pr);
                }
                db.Dispose();
                return listGuestRegistration.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}