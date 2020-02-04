using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

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
                    OriginalVehicleRegistration = model.OriginalVehicleRegistration
                };
                db.tbl_GuestRegistration.Add(saveGuestRegistration);
                db.SaveChanges();
                db.Dispose();
                Msg = "Progress Saved";
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

                    db.SaveChanges();
                    db.Dispose();
                    Msg = "Progress Updated";
                }
            }
            return Msg;
        }

        public List<GuestRegistrationModel> GetGuestRegistrationList(DateTime FromDate, DateTime ToDate)
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