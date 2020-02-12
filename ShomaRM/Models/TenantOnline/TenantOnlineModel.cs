using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Net;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Models
{
    public class TenantOnlineModel
    {
        public long ID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string DateOfBirthTxt { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PassportNumber { get; set; }
        public string CountryIssuance { get; set; }
        public Nullable<System.DateTime> DateIssuance { get; set; }
        public string DateIssuanceTxt { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        public string DateExpireTxt { get; set; }
        public Nullable<int> IDType { get; set; }
        public Nullable<long> State { get; set; }

        public string IDNumber { get; set; }
        public string Country { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public Nullable<long> StateHome { get; set; }

        public string CityHome { get; set; }
        public string ZipHome { get; set; }
        public Nullable<int> RentOwn { get; set; }
        public Nullable<System.DateTime> MoveInDate { get; set; }
        public string MoveInDateTxt { get; set; }
        public string MonthlyPayment { get; set; }
        public string Reason { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> JobType { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string StartDateTxt { get; set; }
        public Nullable<decimal> Income { get; set; }
        public Nullable<decimal> AdditionalIncome { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorPhone { get; set; }
        public string SupervisorEmail { get; set; }
        public string OfficeCountry { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public Nullable<long> OfficeState { get; set; }

        public string OfficeCity { get; set; }
        public string OfficeZip { get; set; }
        public string Relationship { get; set; }
        public string EmergencyFirstName { get; set; }
        public string EmergencyLastName { get; set; }
        public string EmergencyMobile { get; set; }
        public string EmergencyHomePhone { get; set; }
        public string EmergencyWorkPhone { get; set; }
        public string EmergencyEmail { get; set; }
        public string EmergencyCountry { get; set; }
        public string EmergencyAddress1 { get; set; }
        public string EmergencyAddress2 { get; set; }
        public Nullable<long> EmergencyStateHome { get; set; }

        public string EmergencyCityHome { get; set; }
        public string EmergencyZipHome { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> IsInternational { get; set; }
        public string OtherGender { get; set; }

        public DateTime MoveInDateFrom { get; set; }
        public string MoveInDateFromTxt { get; set; }
        public DateTime MoveInDateTo { get; set; }
        public string MoveInDateToTxt { get; set; }
        public string Country2 { get; set; }
        public string HomeAddress12 { get; set; }
        public string HomeAddress22 { get; set; }
        public long StateHome2 { get; set; }
        public string CityHome2 { get; set; }
        public string ZipHome2 { get; set; }
        public int RentOwn2 { get; set; }
        public DateTime MoveInDateFrom2 { get; set; }
        public string MoveInDateFrom2Txt { get; set; }
        public DateTime MoveInDateTo2 { get; set; }
        public string MoveInDateTo2Txt { get; set; }
        public string MonthlyPayment2 { get; set; }
        public string Reason2 { get; set; }
        public Nullable<int> IsAdditionalRHistory { get; set; }
        public string PassportDocument { get; set; }
        public string IdentityDocument { get; set; }
        public string TaxReturn { get; set; }
        public string SSN { get; set; }

        public string TaxReturn2 { get; set; }
        public string TaxReturn3 { get; set; }
        public Nullable<bool> HaveVehicle { get; set; }
        public Nullable<bool> HavePet { get; set; }
        public string tempUpload1 { get; set; }
        public string UploadOriginalFileName1 { get; set; }
        public string tempUpload2 { get; set; }
        public string UploadOriginalFileName2 { get; set; }
        public string tempUpload3 { get; set; }
        public string UploadOriginalFileName3 { get; set; }
        public string tempPassportUpload { get; set; }
        public string UploadOriginalPassportName { get; set; }
        public string tempIdentityUpload { get; set; }
        public string UploadOriginalIdentityName { get; set; }
        public bool IsPaystub { get; set; }
        public string StatePersonalString { get; set; }
        public string StateHomeString { get; set; }
        public string CountryString { get; set; }
        public string OfficeCountryString { get; set; }
        public string EmergencyCountryString { get; set; }
        public string EmergencyStateHomeString { get; set; }

        public string SaveHavePet(long id, bool PetValue)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var getAppldata = db.tbl_TenantOnline.Where(p => p.ProspectID == id).FirstOrDefault();

            if (getAppldata != null)
            {
                if (PetValue)
                {
                    getAppldata.HavePet = true;
                }
                else
                {
                    getAppldata.HavePet = false;
                }
                db.SaveChanges();
                db.Dispose();
                msg = "Data Save";
            }
            else
            {
                msg = "Data Save";
            }
            return msg;
        }

        public string SaveHaveVehicle(long id, bool vehicleValue)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var getAppldata = db.tbl_TenantOnline.Where(p => p.ProspectID == id).FirstOrDefault();

            if (getAppldata != null)
            {
                var delVehicleData = db.tbl_Vehicle.Where(co => co.TenantID == id).ToList();
                if (delVehicleData!=null)
                {
                    db.tbl_Vehicle.RemoveRange(delVehicleData);
                    db.SaveChanges();
                }
                if (vehicleValue)
                {
                    getAppldata.HaveVehicle = true;
                }
                else
                {
                    getAppldata.HaveVehicle = false;
                }
                db.SaveChanges();
                db.Dispose();
                msg = "Data Save";
            }
            else
            {
                msg = "Data Save";
            }
            return msg;
        }

        public TenantOnlineModel GetTenantOnlineList(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel lstpr = new TenantOnlineModel();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantOnlineData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "id";
                    paramF.Value = id;
                    cmd.Parameters.Add(paramF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    DateTime? dateOfBirth = null;
                    try
                    {

                        dateOfBirth = Convert.ToDateTime(dr["DateOfBirth"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? dateIssuance = null;
                    try
                    {

                        dateIssuance = Convert.ToDateTime(dr["DateIssuance"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? dateExpire = null;
                    try
                    {

                        dateExpire = Convert.ToDateTime(dr["DateExpire"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveInDateFrom = null;
                    try
                    {

                        moveInDateFrom = Convert.ToDateTime(dr["MoveInDateFrom"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveInDateTo = null;
                    try
                    {

                        moveInDateTo = Convert.ToDateTime(dr["MoveInDateTo"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveInDateFrom2 = null;
                    try
                    {

                        moveInDateFrom2 = Convert.ToDateTime(dr["MoveInDateFrom2"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveInDateTo2 = null;
                    try
                    {

                        moveInDateTo2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? startDate = null;
                    try
                    {

                        startDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    }
                    catch
                    {

                    }
                    lstpr.IsInternational = Convert.ToInt32(dr["IsInternational"].ToString());
                    lstpr.IsAdditionalRHistory = Convert.ToInt32(dr["IsAdditionalRHistory"].ToString());
                    lstpr.FirstName = dr["FirstName"].ToString();
                    lstpr.MiddleInitial = dr["MiddleInitial"].ToString();
                    lstpr.LastName = dr["LastName"].ToString();
                    lstpr.DateOfBirthTxt = dateOfBirth == null ? "" : dateOfBirth.Value.ToString("MM/dd/yyy");
                    lstpr.Gender = Convert.ToInt32(dr["Gender"].ToString());
                    lstpr.Email = dr["Email"].ToString();
                    lstpr.Mobile = dr["Mobile"].ToString();
                    lstpr.PassportNumber = dr["PassportNumber"].ToString();
                    lstpr.CountryIssuance = dr["CountryIssuance"].ToString();
                    lstpr.DateIssuanceTxt = dateIssuance == null ? "" : dateIssuance.Value.ToString("MM/dd/yyy");
                    lstpr.DateExpireTxt = dateExpire == null ? "" : dateExpire.Value.ToString("MM/dd/yyy");
                    lstpr.IDType = Convert.ToInt32(dr["IDType"].ToString());
                    lstpr.State = Convert.ToInt64(dr["State"].ToString());
                    lstpr.IDNumber = dr["IDNumber"].ToString();
                    lstpr.SSN = dr["SSN"].ToString();
                    lstpr.Country = dr["Country"].ToString();
                    lstpr.HomeAddress1 = dr["HomeAddress1"].ToString();
                    lstpr.HomeAddress2 = dr["HomeAddress2"].ToString();
                    lstpr.StateHome = Convert.ToInt64(dr["StateHome"].ToString());
                    lstpr.CityHome = dr["CityHome"].ToString();
                    lstpr.ZipHome = dr["ZipHome"].ToString();
                    lstpr.RentOwn = Convert.ToInt16(dr["RentOwn"].ToString());
                    lstpr.MoveInDateFromTxt = moveInDateFrom == null ? "" : moveInDateFrom.Value.ToString("MM/dd/yyy");
                    lstpr.MoveInDateToTxt = moveInDateTo == null ? "" : moveInDateTo.Value.ToString("MM/dd/yyy");
                    lstpr.MonthlyPayment = dr["MonthlyPayment"].ToString();
                    lstpr.Reason = dr["Reason"].ToString();
                    lstpr.EmployerName = dr["EmployerName"].ToString();
                    lstpr.JobTitle = dr["JobTitle"].ToString();
                    lstpr.JobType = Convert.ToInt32(dr["JobType"].ToString());
                    lstpr.StartDateTxt = startDate == null ? "" : startDate.Value.ToString("MM/dd/yyy");
                    lstpr.Income = Convert.ToDecimal(dr["Income"].ToString());
                    lstpr.AdditionalIncome = Convert.ToDecimal(dr["AdditionalIncome"].ToString());
                    lstpr.SupervisorName = dr["SupervisorName"].ToString();
                    lstpr.SupervisorPhone = dr["SupervisorPhone"].ToString();
                    lstpr.SupervisorEmail = dr["SupervisorEmail"].ToString();
                    lstpr.OfficeCountry = dr["OfficeCountry"].ToString();
                    lstpr.OfficeAddress1 = dr["OfficeAddress1"].ToString();
                    lstpr.OfficeAddress2 = dr["OfficeAddress2"].ToString();
                    lstpr.OfficeState = Convert.ToInt32(dr["OfficeState"].ToString());
                    lstpr.OfficeCity = dr["OfficeCity"].ToString();
                    lstpr.OfficeZip = dr["OfficeZip"].ToString();
                    lstpr.Relationship = dr["Relationship"].ToString();
                    lstpr.EmergencyFirstName = dr["EmergencyFirstName"].ToString();
                    lstpr.EmergencyLastName = dr["EmergencyLastName"].ToString();
                    lstpr.EmergencyMobile = dr["EmergencyMobile"].ToString();
                    lstpr.EmergencyHomePhone = dr["EmergencyHomePhone"].ToString();
                    lstpr.EmergencyWorkPhone = dr["EmergencyWorkPhone"].ToString();
                    lstpr.EmergencyEmail = dr["EmergencyEmail"].ToString();
                    lstpr.EmergencyCountry = dr["EmergencyCountry"].ToString();
                    lstpr.EmergencyAddress1 = dr["EmergencyAddress1"].ToString();
                    lstpr.EmergencyAddress2 = dr["EmergencyAddress2"].ToString();
                    lstpr.EmergencyStateHome = Convert.ToInt32(dr["EmergencyStateHome"].ToString());
                    lstpr.EmergencyCityHome = dr["EmergencyCityHome"].ToString();
                    lstpr.EmergencyZipHome = dr["EmergencyZipHome"].ToString();
                    lstpr.OtherGender = dr["OtherGender"].ToString();

                    lstpr.IdentityDocument = dr["IdentityDocument"].ToString();
                    lstpr.PassportDocument = dr["PassportDocument"].ToString();
                    lstpr.TaxReturn = dr["TaxReturn"].ToString();

                    lstpr.TaxReturn2 = dr["TaxReturn2"].ToString();
                    lstpr.TaxReturn3 = dr["TaxReturn3"].ToString();
                    lstpr.HaveVehicle = Convert.ToBoolean(dr["HaveVehicle"].ToString());
                    lstpr.HavePet = Convert.ToBoolean(dr["HavePet"].ToString());
                    lstpr.UploadOriginalFileName1 = dr["TaxReturnOrginalFile"].ToString();
                    lstpr.UploadOriginalFileName2 = dr["TaxReturnOrginalFile2"].ToString();
                    lstpr.UploadOriginalFileName3 = dr["TaxReturnOrginalFile3"].ToString();
                    lstpr.UploadOriginalPassportName = dr["PassportDocumentOriginalFile"].ToString();
                    lstpr.UploadOriginalIdentityName = dr["IdentityDocumentOriginalFile"].ToString();
                    lstpr.IsPaystub = Convert.ToBoolean(dr["IsPaystub"].ToString());

                    var PersonalStateVar = db.tbl_State.Where(co => co.ID == lstpr.State).FirstOrDefault();
                    lstpr.StatePersonalString = PersonalStateVar != null ? PersonalStateVar.StateName : "";
                    var StateHomeVar = db.tbl_State.Where(co => co.ID == lstpr.StateHome).FirstOrDefault();
                    lstpr.StateHomeString = StateHomeVar != null ? StateHomeVar.StateName : "";
                    int contryTemp = lstpr.Country != "" ? Convert.ToInt32(lstpr.Country) : 0;
                    var CountryVar = db.tbl_Country.Where(co => co.ID == contryTemp).FirstOrDefault();
                    lstpr.CountryString = CountryVar != null ? CountryVar.CountryName : "";
                    int officeContryTemp = lstpr.OfficeCountry != "" ? Convert.ToInt32(lstpr.OfficeCountry) : 0;
                    var OfficeCountryVar = db.tbl_Country.Where(co => co.ID == officeContryTemp).FirstOrDefault();
                    lstpr.OfficeCountryString = OfficeCountryVar != null ? OfficeCountryVar.CountryName : "";
                    int emergencyContryTemp = lstpr.EmergencyCountry != "" ? Convert.ToInt32(lstpr.EmergencyCountry) : 0;
                    var EmergencyCountryVar = db.tbl_Country.Where(co => co.ID == emergencyContryTemp).FirstOrDefault();
                    lstpr.EmergencyCountryString = EmergencyCountryVar != null ? EmergencyCountryVar.CountryName : "";
                    int emergencyStateHomeTemp = lstpr.EmergencyStateHome != null ? Convert.ToInt32(lstpr.EmergencyStateHome) : 0;
                    var EmergencyStateHomeVar = db.tbl_State.Where(co => co.ID == emergencyStateHomeTemp).FirstOrDefault();
                    lstpr.EmergencyStateHomeString = EmergencyStateHomeVar != null ? EmergencyStateHomeVar.StateName : "";
                }
                db.Dispose();
                return lstpr;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public string SaveTenantOnline(TenantOnlineModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.ProspectID != 0)
            {
                var getAppldata = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID).FirstOrDefault();
                if (getAppldata != null)
                {

                    getAppldata.IsInternational = model.IsInternational;
                    getAppldata.IsAdditionalRHistory = model.IsAdditionalRHistory;
                    getAppldata.FirstName = model.FirstName;
                    getAppldata.MiddleInitial = model.MiddleInitial;
                    getAppldata.LastName = model.LastName;
                    getAppldata.DateOfBirth = model.DateOfBirth;
                    getAppldata.Gender = model.Gender;
                    getAppldata.Email = model.Email;
                    getAppldata.Mobile = model.Mobile;
                    getAppldata.PassportNumber = model.PassportNumber;
                    getAppldata.CountryIssuance = model.CountryIssuance;
                    getAppldata.DateIssuance = model.DateIssuance;
                    getAppldata.DateExpire = model.DateExpire;
                    getAppldata.IDType = model.IDType;
                    getAppldata.State = model.State;
                    getAppldata.IDNumber = model.IDNumber;
                    getAppldata.Country = model.Country;
                    getAppldata.HomeAddress1 = model.HomeAddress1;
                    getAppldata.HomeAddress2 = model.HomeAddress2;
                    getAppldata.StateHome = model.StateHome;
                    getAppldata.CityHome = model.CityHome;
                    getAppldata.ZipHome = model.ZipHome;
                    getAppldata.RentOwn = model.RentOwn;
                    if (model.MoveInDateFrom > Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                    {
                        getAppldata.MoveInDateFrom = model.MoveInDateFrom;
                        getAppldata.MoveInDateTo = model.MoveInDateTo;
                    }
                    getAppldata.MonthlyPayment = model.MonthlyPayment;
                    getAppldata.Reason = model.Reason;

                   
                    getAppldata.EmployerName = model.EmployerName;
                    getAppldata.JobTitle = model.JobTitle;
                    getAppldata.JobType = model.JobType;
                    getAppldata.StartDate = model.StartDate;
                    getAppldata.Income = model.Income;
                    getAppldata.AdditionalIncome = model.AdditionalIncome;
                    getAppldata.SupervisorName = model.SupervisorName;
                    getAppldata.SupervisorPhone = model.SupervisorPhone;
                    getAppldata.SupervisorEmail = model.SupervisorEmail;
                    getAppldata.OfficeCountry = model.OfficeCountry;
                    getAppldata.OfficeAddress1 = model.OfficeAddress1;
                    getAppldata.OfficeAddress2 = model.OfficeAddress2;
                    getAppldata.OfficeState = model.OfficeState;
                    getAppldata.OfficeCity = model.OfficeCity;
                    getAppldata.OfficeZip = model.OfficeZip;
                    getAppldata.Relationship = model.Relationship;
                    getAppldata.EmergencyFirstName = model.EmergencyFirstName;
                    getAppldata.EmergencyLastName = model.EmergencyLastName;
                    getAppldata.EmergencyMobile = model.EmergencyMobile;
                    getAppldata.EmergencyHomePhone = model.EmergencyHomePhone;
                    getAppldata.EmergencyWorkPhone = model.EmergencyWorkPhone;
                    getAppldata.EmergencyEmail = model.EmergencyEmail;
                    getAppldata.EmergencyCountry = model.EmergencyCountry;
                    getAppldata.EmergencyAddress1 = model.EmergencyAddress1;
                    getAppldata.EmergencyAddress2 = model.EmergencyAddress2;
                    getAppldata.EmergencyStateHome = model.EmergencyStateHome;
                    getAppldata.EmergencyCityHome = model.EmergencyCityHome;
                    getAppldata.EmergencyZipHome = model.EmergencyZipHome;
                    getAppldata.CreatedDate = DateTime.Now.Date;
                    getAppldata.OtherGender = model.OtherGender;
                    getAppldata.SSN = model.SSN;
                    getAppldata.TaxReturn = model.TaxReturn;
                    getAppldata.TaxReturn2 = model.TaxReturn2;
                    getAppldata.TaxReturn3 = model.TaxReturn3;
                    getAppldata.PassportDocument = model.PassportDocument;
                    getAppldata.IdentityDocument = model.IdentityDocument;
                    getAppldata.TaxReturnOrginalFile = model.UploadOriginalFileName1;
                    getAppldata.TaxReturnOrginalFile2 = model.UploadOriginalFileName2;
                    getAppldata.TaxReturnOrginalFile3 = model.UploadOriginalFileName3;
                    getAppldata.PassportDocumentOriginalFile = model.UploadOriginalPassportName;
                    getAppldata.IdentityDocumentOriginalFile = model.UploadOriginalIdentityName;
                    getAppldata.IsPaystub = model.IsPaystub;
                }
                db.SaveChanges();

                var saveApplicantGender = db.tbl_Applicant.Where(p => p.Email == model.Email).FirstOrDefault();
                if (saveApplicantGender != null)
                {
                    DateOfBirth = model.DateOfBirth;
                    Gender = model.Gender;

                };
                db.SaveChanges();

                msg = "Applicant Updated Successfully";
            }


            db.Dispose();
            return msg;

        }

        //File Upload1,2,3

        public TenantOnlineModel SaveTaxUpload1(HttpPostedFileBase fileBaseUpload1, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU1 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload1 != null && fileBaseUpload1.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload1.FileName;
                    Extension = Path.GetExtension(fileBaseUpload1.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload1.FileName);
                    fileBaseUpload1.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload1.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU1.tempUpload1 = sysFileName;
                    tenantModelU1.UploadOriginalFileName1 = fileName;
                }

            }
            return tenantModelU1;
        }
        public TenantOnlineModel SaveTaxUpload2(HttpPostedFileBase fileBaseUpload2, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU2 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload2 != null && fileBaseUpload2.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload2.FileName;
                    Extension = Path.GetExtension(fileBaseUpload2.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload2.FileName);
                    fileBaseUpload2.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload2.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU2.tempUpload2 = sysFileName.ToString();
                    tenantModelU2.UploadOriginalFileName2 = fileName;
                }

            }
            return tenantModelU2;
        }
        public TenantOnlineModel SaveTaxUpload3(HttpPostedFileBase fileBaseUpload3, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU3 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload3 != null && fileBaseUpload3.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload3.FileName;
                    Extension = Path.GetExtension(fileBaseUpload3.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload3.FileName);
                    fileBaseUpload3.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload3.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU3.tempUpload3 = sysFileName.ToString();
                    tenantModelU3.UploadOriginalFileName3 = fileName;
                }

            }
            return tenantModelU3;
        }
        public TenantOnlineModel SaveUploadPassport(HttpPostedFileBase fileBaseUploadPassport, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelPassportU = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUploadPassport != null && fileBaseUploadPassport.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUploadPassport.FileName;
                    Extension = Path.GetExtension(fileBaseUploadPassport.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUploadPassport.FileName);
                    fileBaseUploadPassport.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUploadPassport.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelPassportU.tempPassportUpload = sysFileName.ToString();
                    tenantModelPassportU.UploadOriginalPassportName = fileName;
                }

            }
            return tenantModelPassportU;
        }
        public TenantOnlineModel SaveUploadIdentity(HttpPostedFileBase fileBaseUploadIdentity, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelIdentityU = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUploadIdentity != null && fileBaseUploadIdentity.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUploadIdentity.FileName;
                    Extension = Path.GetExtension(fileBaseUploadIdentity.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUploadIdentity.FileName);
                    fileBaseUploadIdentity.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUploadIdentity.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelIdentityU.tempIdentityUpload = sysFileName.ToString();
                    tenantModelIdentityU.UploadOriginalIdentityName = fileName;
                }

            }
            return tenantModelIdentityU;
        }
    }

    public class TenantPetPlace
    {
        public long TPetID { get; set; }
        public Nullable<long> PetPlaceID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public TenantPetPlace GetTenantPetPlaceData(long Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantPetPlace model = new TenantPetPlace();

            var getTenantPetPlaceData = db.tbl_TenantPetPlace.Where(p => p.TenantID == Id).FirstOrDefault();

            if (getTenantPetPlaceData != null)
            {
                model.PetPlaceID = getTenantPetPlaceData.PetPlaceID;
            }
            model.TenantID = Id;
            return model;
        }
    }
}