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

namespace ShomaRM.Areas.Admin.Models
{
    public class TenantModel
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string DateOfBirthText { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<int> MaritalStatus { get; set; }
        public Nullable<int> StudentStatus { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> State { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string SocialSecurityNum { get; set; }
        public string DriverLicense { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarLic { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyPhone { get; set; }
        public Nullable<int> RentResp { get; set; }
        public Nullable<decimal> Income { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string Occupation { get; set; }
        public string OfficeName { get; set; }
        public string JobCode { get; set; }
        public string OfficeEmail { get; set; }
        public string OfficePhone { get; set; }
        public string OfficePhoneExtension { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficeCity { get; set; }
        public Nullable<int> OfficeState { get; set; }
        public string OfficeZip { get; set; }
        public string EmployerContact { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
        public int ProspectID { get; set; }
        public string FullName { get; set; }
        
        public List<PropertyList> FillPropertyDropDownList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyList> model = new List<PropertyList>();
            var pData = db.tbl_Properties.OrderBy(p => p.Title).ToList();
            foreach (var property in pData)
            {
                PropertyList pl = new PropertyList();
                pl.PID = property.PID;
                pl.Title = property.Title;
                model.Add(pl);
            }
            return model.ToList();
        }

        public List<TenantModel> FillTenantDropDownList(long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantModel> lstTenanat = new List<TenantModel>();
            var tenantList = db.tbl_Tenant.Where(p => p.UnitID == PID).ToList();
            foreach (var tl in tenantList)
            {
                lstTenanat.Add(new TenantModel
                {
                    ID = tl.ID,
                    FirstName = tl.FirstName,
                    LastName = tl.LastName,
                    FullName = tl.FirstName + " " + tl.LastName
                });

            }
            return lstTenanat;
        }
        public string BuildPaganationTenantList(DateTime FromDate, DateTime ToDate, int NumberOfRows)
        {
            string NOR = "0";
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantList> model = new List<TenantList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_BuildPagination_TenantList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                    NOR = dtTable.Rows[0]["PageNumber"].ToString();
                }
                db.Dispose();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
            return NOR;
        }
        public List<TenantList> GetTenantList(TenantSearch model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantList> lstTenant = new List<TenantList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_TenantList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "PageNumber";
                    param1.Value = model.PageNumber;
                    cmd.Parameters.Add(param1);
                    DbParameter param2 = cmd.CreateParameter();
                    param2.ParameterName = "NumberOfRows";
                    param2.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param2);
                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "FromDate";
                    param3.Value = model.FromDate;
                    cmd.Parameters.Add(param3);
                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "ToDate";
                    param4.Value = model.ToDate;
                    cmd.Parameters.Add(param4);
                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "FirstName";
                    param5.Value = model.FirstName == null ? "" : model.FirstName;
                    cmd.Parameters.Add(param5);
                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "LastName";
                    param6.Value = model.LastName == null ? "" : model.LastName;
                    cmd.Parameters.Add(param6);
                    DbParameter param7 = cmd.CreateParameter();
                    param7.ParameterName = "Gender";
                    param7.Value = model.Gender;
                    cmd.Parameters.Add(param7);
                    DbParameter param8 = cmd.CreateParameter();
                    param8.ParameterName = "MaritalStatus";
                    param8.Value = model.MaritalStatus;
                    cmd.Parameters.Add(param8);
                    DbParameter param9 = cmd.CreateParameter();
                    param9.ParameterName = "State";
                    param9.Value = model.MaritalStatus;
                    cmd.Parameters.Add(param9);
                    DbParameter param10 = cmd.CreateParameter();
                    param10.ParameterName = "City";
                    param10.Value = model.City;
                    cmd.Parameters.Add(param10);
                    DbParameter param11 = cmd.CreateParameter();
                    param11.ParameterName = "PropertyID";
                    param11.Value = model.PropertyID;
                    cmd.Parameters.Add(param11);
                    DbParameter param12 = cmd.CreateParameter();
                    param12.ParameterName = "UnitID";
                    param12.Value = model.UnitID;
                    cmd.Parameters.Add(param12);
                    DbParameter param13 = cmd.CreateParameter();
                    param13.ParameterName = "SocialSecurityNum";
                    param13.Value = model.SocialSecurityNum == null ? "" : model.SocialSecurityNum;
                    cmd.Parameters.Add(param13);
                    DbParameter param14 = cmd.CreateParameter();
                    param14.ParameterName = "Occupation";
                    param14.Value = model.Occupation == null ? "" : model.Occupation;
                    cmd.Parameters.Add(param14);
                    DbParameter param15 = cmd.CreateParameter();
                    param15.ParameterName = "OfficeState";
                    param15.Value = model.OfficeState;
                    cmd.Parameters.Add(param15);
                    DbParameter param16 = cmd.CreateParameter();
                    param16.ParameterName = "OfficeCity";
                    param16.Value = model.OfficeCity;
                    cmd.Parameters.Add(param16);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantList usm = new TenantList();
                    usm.TenantID = int.Parse(dr["TenantID"].ToString());
                    usm.FirstName = dr["FirstName"].ToString();
                    usm.LastName = dr["LastName"].ToString();
                    usm.Property = dr["Property"].ToString();
                    usm.Unit = dr["UnitNo"].ToString();
                    lstTenant.Add(usm);
                }
                db.Dispose();
                return lstTenant.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public TenantModel GetTenantInfo(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantModel model = new TenantModel();
            var tenantData = db.tbl_Tenant.Where(p => p.ID == TenantID).FirstOrDefault();
            ClearAllFields(model);
            if (tenantData != null)
            {
                DateTime? dob = null;
                try
                {
                    dob = Convert.ToDateTime(tenantData.DateOfBirth.ToString());
                }
                catch
                {

                }
                model.ID = tenantData.ID;
                model.LastName = tenantData.LastName;
                model.FirstName = tenantData.FirstName;
                model.PropertyID = tenantData.PropertyID;
                model.UnitID = tenantData.UnitID;
                model.Address = tenantData.Address;
                model.City = tenantData.City;
                model.State =Convert.ToInt32(tenantData.State);
                model.Zip = tenantData.Zip;
                model.HomePhone = tenantData.HomePhone;
                model.OfficeEmail = tenantData.OfficeEmail;
                model.OfficePhone = tenantData.OfficePhone;
                model.OfficeName = tenantData.OfficeName;
                model.OfficeAddress = tenantData.OfficeAddress;
                model.OfficeCity = tenantData.OfficeCity;
                model.OfficeState = tenantData.OfficeState;
                model.OfficeZip = tenantData.OfficeZip;
                model.Occupation = tenantData.Occupation;
                model.SocialSecurityNum = tenantData.SocialSecurityNum;
                model.DriverLicense = tenantData.DriverLicense;
                model.DateOfBirthText = dob == null ? "" : dob.Value.ToString("MM/dd/yyyy");
                model.CarMake = tenantData.CarMake;
                model.CarModel = tenantData.CarModel;
                model.CarLic = tenantData.CarLic;
                model.EmergencyContact = tenantData.EmergencyContact;
                model.EmergencyPhone = tenantData.EmergencyPhone;
                model.RentResp = tenantData.RentResp;
                model.Income = tenantData.Income;
                model.EmployerContact = tenantData.EmployerContact;
                model.OfficePhoneExtension = tenantData.OfficePhoneExtension;
                model.JobCode = tenantData.JobCode;
                model.MiddleInitial = tenantData.MiddleInitial;
                model.Gender = tenantData.Gender;
                model.MaritalStatus = tenantData.MaritalStatus;
                model.StudentStatus = tenantData.StudentStatus;
            }
            db.Dispose();
            return model;
        }
        public long SaveUpdateTenant(TenantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var tblTenant = db.tbl_Tenant.Where(p => p.ID == model.ID).FirstOrDefault();
            if (tblTenant == null)
            {
                tbl_Tenant objTable = new tbl_Tenant();
                objTable.ID = model.ID;
                objTable.LastName = model.LastName;
                objTable.FirstName = model.FirstName;
                objTable.PropertyID = model.PropertyID;
                objTable.UnitID = model.UnitID;
                objTable.Address = model.Address;
                objTable.City = model.City;
                objTable.State = model.State;
                objTable.Zip = model.Zip;
                objTable.HomePhone = model.HomePhone;
                objTable.OfficeEmail = model.OfficeEmail;
                objTable.OfficePhone = model.OfficePhone;
                objTable.OfficeName = model.OfficeName;
                objTable.OfficeAddress = model.OfficeAddress;
                objTable.OfficeCity = model.OfficeCity;
                objTable.OfficeState = model.OfficeState;
                objTable.OfficeZip = model.OfficeZip;
                objTable.Occupation = model.Occupation;
                objTable.SocialSecurityNum = model.SocialSecurityNum;
                objTable.DriverLicense = model.DriverLicense;
                objTable.DateOfBirth = model.DateOfBirth;
                objTable.CarMake = model.CarMake;
                objTable.CarModel = model.CarModel;
                objTable.CarLic = model.CarLic;
                objTable.EmergencyContact = model.EmergencyContact;
                objTable.EmergencyPhone = model.EmergencyPhone;
                objTable.RentResp = model.RentResp;
                objTable.Income = model.Income;
                objTable.EmployerContact = model.EmployerContact;
                objTable.OfficePhoneExtension = model.OfficePhoneExtension;
                objTable.JobCode = model.JobCode;
                objTable.MiddleInitial = model.MiddleInitial;
                objTable.Gender = model.Gender;
                objTable.MaritalStatus = model.MaritalStatus;
                objTable.StudentStatus = model.StudentStatus;
                objTable.CreatedBy = 1;
                objTable.CreatedDate = DateTime.Now;
                db.tbl_Tenant.Add(objTable);
                db.SaveChanges();
                model.ID = objTable.ID;
            }
            else
            {
                tblTenant.ID = model.ID;
                tblTenant.LastName = model.LastName;
                tblTenant.FirstName = model.FirstName;
                tblTenant.PropertyID = model.PropertyID;
                tblTenant.UnitID = model.UnitID;
                tblTenant.Address = model.Address;
                tblTenant.City = model.City;
                tblTenant.State = model.State;
                tblTenant.Zip = model.Zip;
                tblTenant.HomePhone = model.HomePhone;
                tblTenant.OfficeEmail = model.OfficeEmail;
                tblTenant.OfficePhone = model.OfficePhone;
                tblTenant.OfficeName = model.OfficeName;
                tblTenant.OfficeAddress = model.OfficeAddress;
                tblTenant.OfficeCity = model.OfficeCity;
                tblTenant.OfficeState = model.OfficeState;
                tblTenant.OfficeZip = model.OfficeZip;
                tblTenant.Occupation = model.Occupation;
                tblTenant.SocialSecurityNum = model.SocialSecurityNum;
                tblTenant.DriverLicense = model.DriverLicense;
                tblTenant.DateOfBirth = model.DateOfBirth;
                tblTenant.CarMake = model.CarMake;
                tblTenant.CarModel = model.CarModel;
                tblTenant.CarLic = model.CarLic;
                tblTenant.EmergencyContact = model.EmergencyContact;
                tblTenant.EmergencyPhone = model.EmergencyPhone;
                tblTenant.RentResp = model.RentResp;
                tblTenant.Income = model.Income;
                tblTenant.EmployerContact = model.EmployerContact;
                tblTenant.OfficePhoneExtension = model.OfficePhoneExtension;
                tblTenant.JobCode = model.JobCode;
                tblTenant.MiddleInitial = model.MiddleInitial;
                tblTenant.Gender = model.Gender;
                tblTenant.MaritalStatus = model.MaritalStatus;
                tblTenant.StudentStatus = model.StudentStatus;
                tblTenant.LastModifiedBy = 1;
                tblTenant.LastModifiedeDate = DateTime.Now;
                db.SaveChanges();
            }
            db.Dispose();
            return model.ID;
        }
        public long ConvertToTenant(TenantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            tbl_Tenant objTable = new tbl_Tenant();
            objTable.ID = model.ID;
            objTable.LastName = model.LastName;
            objTable.FirstName = model.FirstName;
            objTable.PropertyID = model.PropertyID;
            objTable.UnitID = model.UnitID;
            objTable.Address = model.Address;
            objTable.City = model.City;
            objTable.State = model.State;
            objTable.OfficeEmail = model.OfficeEmail;
            objTable.CreatedBy = 1;
            objTable.CreatedDate = DateTime.Now;
            objTable.LastModifiedBy = 1;
            objTable.LastModifiedeDate = DateTime.Now;

            db.tbl_Tenant.Add(objTable);
            db.SaveChanges();
            model.ID = objTable.ID;

            var addLease = new tbl_Lease()
            {
                TenantID = objTable.ID,
                PID = Convert.ToInt32(model.PropertyID),
                UID = Convert.ToInt32(model.UnitID),
                Revision_Num = 1,
                Status = 1,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,

            };
            db.tbl_Lease.Add(addLease);
            db.SaveChanges();

            var addUser = new tbl_Login()
            {
                TenantID = objTable.ID,
                Username = model.OfficeEmail,
                Password = model.FirstName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.OfficeEmail,
                IsActive = 1,

            };
            db.tbl_Login.Add(addUser);
            db.SaveChanges();

            var prospectDet = db.tbl_Prospect.Where(p => p.PID == model.ProspectID).FirstOrDefault();
            if (prospectDet != null)
            {
                prospectDet.Status = 1;
                prospectDet.LastModifiedeDate = DateTime.Now;
            }
            db.SaveChanges();

            var transList = db.tbl_Transaction.Where(p => p.ProspectID == model.ProspectID).ToList();
            if (transList != null)
            {
                foreach (var tl in transList)
                {
                    tl.TenantID = model.ID;
                    db.SaveChanges();
                }
            }
           

            var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == model.UnitID).FirstOrDefault();
            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplate.html");
            if (model != null)
            {
                reportHTML = reportHTML.Replace("[%EmailHeader%]", "Tenant Registration");
                reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + model.FirstName + " " + model.LastName + "</b>,<br/>Your Tenant Account created successfully. Please login to see status. <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.OfficeEmail + " </br>Password :" + model.FirstName);

                reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                reportHTML = reportHTML.Replace("[%TenantAddress%]", model.Address);
                reportHTML = reportHTML.Replace("[%LeaseDate%]", DateTime.Now.ToString());
                reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString());
                reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString());
                reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
            }
            string body = reportHTML;
            new EmailSendModel().SendEmail(model.OfficeEmail, "Tenant Registration Successfull", body);

            db.Dispose();
            return model.ID;
        }
        public long OnlinePToTenant(TenantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            tbl_Tenant objTable = new tbl_Tenant();
            objTable.ID = model.ID;
            objTable.LastName = model.LastName;
            objTable.FirstName = model.FirstName;
            objTable.PropertyID = model.PropertyID;
            objTable.UnitID = model.UnitID;
            objTable.Address = model.Address;
            objTable.City = model.City;
            objTable.State = model.State;
            objTable.OfficeEmail = model.OfficeEmail;
            objTable.CreatedBy = 1;
            objTable.CreatedDate = DateTime.Now;
            objTable.LastModifiedBy = 1;
            objTable.LastModifiedeDate = DateTime.Now;
            objTable.DateOfBirth = model.DateOfBirth;
            objTable.HomePhone = model.HomePhone;
            objTable.Income = model.Income;

            db.tbl_Tenant.Add(objTable);
            db.SaveChanges();
            model.ID = objTable.ID;

            var addLease = new tbl_Lease()
            {
                TenantID = objTable.ID,
                PID = Convert.ToInt32(model.PropertyID),
                UID = Convert.ToInt32(model.UnitID),
                Revision_Num = 1,
                Status = 1,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
                
            };
            db.tbl_Lease.Add(addLease);
            db.SaveChanges();

            var prospectDet = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectID).FirstOrDefault();
            if (prospectDet != null)
            {
                prospectDet.IsApplyNow = 3;

            }
            db.SaveChanges();

            var loginDet = db.tbl_Login.Where(p => p.UserID == prospectDet.UserId).FirstOrDefault();
            if (loginDet != null)
            {
                loginDet.UserType = 4;
                loginDet.TenantID = model.ID;

            }
            db.SaveChanges();

            var transList = db.tbl_Transaction.Where(p => p.TenantID == loginDet.UserID).ToList();
            if (transList != null)
            {
                foreach (var tl in transList)
                {
                    tl.TenantID = model.ID;
                }
            }
            db.SaveChanges();


            var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == model.UnitID).FirstOrDefault();
            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplate.html");
            if (model != null)
            {
                reportHTML = reportHTML.Replace("[%EmailHeader%]", "Tenant Registration");
                reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + model.FirstName + " " + model.LastName + "</b>,<br/>Your Tenant Account created successfully. Please login to see status. <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.OfficeEmail + " </br>Password :" + loginDet.Password);

                reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                reportHTML = reportHTML.Replace("[%TenantAddress%]", model.Address);
                reportHTML = reportHTML.Replace("[%LeaseDate%]", DateTime.Now.ToString());
                reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString());
                reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString());
                reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
            }
            string body = reportHTML;
            new EmailSendModel().SendEmail(model.OfficeEmail, "Tenant Registration Successfull", body);

            db.Dispose();
            return model.ID;
        }
        private void ClearAllFields(TenantModel model)
        {
            model.ID = 0;
            model.LastName = "";
            model.FirstName = "";
            model.PropertyID = 0;
            model.UnitID = 0;
            model.Address = "";
            model.City = "";
            model.State = 0;
            model.Zip = "";
            model.HomePhone = "";
            model.OfficeEmail = "";
            model.OfficePhone = "";
            model.OfficeName = "";
            model.OfficeAddress = "";
            model.OfficeCity = "";
            model.OfficeState = 0;
            model.OfficeZip = "";
            model.Occupation = "";
            model.SocialSecurityNum = "";
            model.DriverLicense = "";
            model.DateOfBirth = null;
            model.CarMake = "";
            model.CarModel = "";
            model.CarLic = "";
            model.EmergencyContact = "";
            model.EmergencyPhone = "";
            model.RentResp = 0;
            model.Income = 0;
            model.EmployerContact = "";
            model.OfficePhoneExtension = "";
            model.JobCode = "";
            model.MiddleInitial = "";
            model.Gender = 0;
            model.MaritalStatus = 0;
            model.StudentStatus = 0;
        }
        public class TenantList
        {
            public long TenantID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Property { get; set; }
            public string Unit { get; set; }
        }
        public class PropertyList
        {
            public long PID { get; set; }
            public string Title { get; set; }
        }
        public class TenantSearch
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Gender { get; set; }
            public int MaritalStatus { get; set; }
            public int City { get; set; }
            public int State { get; set; }
            public string SocialSecurityNum { get; set; }
            public long PropertyID { get; set; }
            public long UnitID { get; set; }
            public string Occupation { get; set; }
            public string OfficeCity { get; set; }
            public int OfficeState { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
    }

    public class TenantOnlineModel
    {
        public long TenantID { get; set; }
        public long UnitID { get; set; }
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
        public string HaveVehicleString { get; set; }
        public string HavePetString { get; set; }
        public Nullable<int> IsAgreePostDisclaimer { get; set; }

        public TenantOnlineModel getTenantOnlineData(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel lstpr = new TenantOnlineModel();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantInfoData";
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
                    DateTime? moveInDate = null;
                    try
                    {

                        moveInDate = Convert.ToDateTime(dr["MoveInDate"].ToString());
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

                    lstpr.IsInternational = Convert.ToInt32(dr["IsInternational"].ToString());
                    //lstpr.IsAdditionalRHistory = Convert.ToInt32(dr["IsAdditionalRHistory"].ToString());
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
                    lstpr.HaveVehicleString = dr["HaveVehicle"].ToString();
                    lstpr.HavePetString = dr["HavePet"].ToString();

                    lstpr.UploadOriginalFileName1 = dr["TaxReturnOrginalFile"].ToString();
                    lstpr.UploadOriginalFileName2 = dr["TaxReturnOrginalFile2"].ToString();
                    lstpr.UploadOriginalFileName3 = dr["TaxReturnOrginalFile3"].ToString();
                    lstpr.UploadOriginalPassportName = dr["PassportDocumentOriginalFile"].ToString();
                    lstpr.UploadOriginalIdentityName = dr["IdentityDocumentOriginalFile"].ToString();
                    lstpr.IsPaystub = Convert.ToBoolean(dr["IsPaystub"].ToString());
                    lstpr.ProspectID = Convert.ToInt64(dr["ProspectID"].ToString());
                    lstpr.IsAgreePostDisclaimer = Convert.ToInt32(dr["IsAgreePostDisclaimer"].ToString());
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

            var getAppldata = db.tbl_TenantInfo.Where(p => p.TenantID == model.TenantID).FirstOrDefault();
            if (getAppldata == null)
            {
                tbl_TenantInfo objTable = new tbl_TenantInfo();

                objTable.IsInternational = model.IsInternational;
                objTable.FirstName = model.FirstName;
                objTable.MiddleInitial = model.MiddleInitial;
                objTable.LastName = model.LastName;
                objTable.DateOfBirth = model.DateOfBirth;
                objTable.Gender = model.Gender;
                objTable.Email = model.Email;
                objTable.Mobile = model.Mobile;
                objTable.PassportNumber = model.PassportNumber;
                objTable.CountryIssuance = model.CountryIssuance;
                objTable.DateIssuance = model.DateIssuance;
                objTable.DateExpire = model.DateExpire;
                objTable.IDType = model.IDType;
                objTable.State = model.State;
                objTable.IDNumber = model.IDNumber;
                objTable.Country = model.Country;
                objTable.HomeAddress1 = model.HomeAddress1;
                objTable.HomeAddress2 = model.HomeAddress2;
                objTable.StateHome = model.StateHome;
                objTable.CityHome = model.CityHome;
                objTable.ZipHome = model.ZipHome;
                objTable.RentOwn = model.RentOwn;
                objTable.MoveInDate = model.MoveInDate;
                objTable.MonthlyPayment = model.MonthlyPayment;
                objTable.Reason = model.Reason;
                objTable.EmployerName = model.EmployerName;
                objTable.JobTitle = model.JobTitle;
                objTable.JobType = model.JobType;
                objTable.StartDate = model.StartDate;
                objTable.Income = model.Income;
                objTable.AdditionalIncome = model.AdditionalIncome;
                objTable.SupervisorName = model.SupervisorName;
                objTable.SupervisorPhone = model.SupervisorPhone;
                objTable.SupervisorEmail = model.SupervisorEmail;
                objTable.OfficeCountry = model.OfficeCountry;
                objTable.OfficeAddress1 = model.OfficeAddress1;
                objTable.OfficeAddress2 = model.OfficeAddress2;
                objTable.OfficeState = model.OfficeState;
                objTable.OfficeCity = model.OfficeCity;
                objTable.OfficeZip = model.OfficeZip;
                objTable.Relationship = model.Relationship;
                objTable.EmergencyFirstName = model.EmergencyFirstName;
                objTable.EmergencyLastName = model.EmergencyLastName;
                objTable.EmergencyMobile = model.EmergencyMobile;
                objTable.EmergencyHomePhone = model.EmergencyHomePhone;
                objTable.EmergencyWorkPhone = model.EmergencyWorkPhone;
                objTable.EmergencyEmail = model.EmergencyEmail;
                objTable.EmergencyCountry = model.EmergencyCountry;
                objTable.EmergencyAddress1 = model.EmergencyAddress1;
                objTable.EmergencyAddress2 = model.EmergencyAddress2;
                objTable.EmergencyStateHome = model.EmergencyStateHome;
                objTable.EmergencyCityHome = model.EmergencyCityHome;
                objTable.EmergencyZipHome = model.EmergencyZipHome;
                objTable.CreatedDate = DateTime.Now.Date;

                db.tbl_TenantInfo.Add(objTable);
                db.SaveChanges();
                model.TenantID = objTable.TenantID;
                msg = "Tenant Saved Successfully";
            }
            else
            {

                getAppldata.IsInternational = model.IsInternational;
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
                getAppldata.MoveInDate = model.MoveInDate;
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

                db.SaveChanges();
                msg = "Tenant Updated Successfully";
            }

            db.Dispose();
            return msg;

        }

        public string OnlinePToTenant(TenantOnlineModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

                var getAppldata1 = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID).FirstOrDefault();
                if (getAppldata1 != null)
                {
                    tbl_TenantInfo getAppldata = new tbl_TenantInfo();
                getAppldata.ProspectID = getAppldata1.ProspectID;
                getAppldata.PropertyID = 8;
                getAppldata.UnitID = model.UnitID;
                getAppldata.IsInternational = getAppldata1.IsInternational;
                getAppldata.IsAdditionalRHistory = getAppldata1.IsAdditionalRHistory;
                getAppldata.FirstName = getAppldata1.FirstName;
                getAppldata.MiddleInitial = getAppldata1.MiddleInitial;
                getAppldata.LastName = getAppldata1.LastName;
                getAppldata.DateOfBirth = getAppldata1.DateOfBirth;
                getAppldata.Gender = getAppldata1.Gender;
                getAppldata.Email = getAppldata1.Email;
                getAppldata.Mobile = getAppldata1.Mobile;
                getAppldata.PassportNumber = getAppldata1.PassportNumber;
                getAppldata.CountryIssuance = getAppldata1.CountryIssuance;
                getAppldata.DateIssuance = getAppldata1.DateIssuance;
                getAppldata.DateExpire = getAppldata1.DateExpire;
                getAppldata.IDType = getAppldata1.IDType;
                getAppldata.State = getAppldata1.State;
                getAppldata.IDNumber = getAppldata1.IDNumber;
                getAppldata.Country = getAppldata1.Country;
                getAppldata.HomeAddress1 = getAppldata1.HomeAddress1;
                getAppldata.HomeAddress2 = getAppldata1.HomeAddress2;
                getAppldata.StateHome = getAppldata1.StateHome;
                getAppldata.CityHome = getAppldata1.CityHome;
                getAppldata.ZipHome = getAppldata1.ZipHome;
                getAppldata.RentOwn = getAppldata1.RentOwn;
                if (getAppldata1.MoveInDateFrom > Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                {
                    getAppldata.MoveInDateFrom = getAppldata1.MoveInDateFrom;
                    getAppldata.MoveInDateTo = getAppldata1.MoveInDateTo;
                }
                getAppldata.MonthlyPayment = getAppldata1.MonthlyPayment;
                getAppldata.Reason = getAppldata1.Reason;
                getAppldata.EmployerName = getAppldata1.EmployerName;
                getAppldata.JobTitle = getAppldata1.JobTitle;
                getAppldata.JobType = getAppldata1.JobType;
                getAppldata.StartDate = getAppldata1.StartDate;
                getAppldata.Income = getAppldata1.Income;
                getAppldata.AdditionalIncome = getAppldata1.AdditionalIncome;
                getAppldata.SupervisorName = getAppldata1.SupervisorName;
                getAppldata.SupervisorPhone = getAppldata1.SupervisorPhone;
                getAppldata.SupervisorEmail = getAppldata1.SupervisorEmail;
                getAppldata.OfficeCountry = getAppldata1.OfficeCountry;
                getAppldata.OfficeAddress1 = getAppldata1.OfficeAddress1;
                getAppldata.OfficeAddress2 = getAppldata1.OfficeAddress2;
                getAppldata.OfficeState = getAppldata1.OfficeState;
                getAppldata.OfficeCity = getAppldata1.OfficeCity;
                getAppldata.OfficeZip = getAppldata1.OfficeZip;
                getAppldata.Relationship = getAppldata1.Relationship;
                getAppldata.EmergencyFirstName = getAppldata1.EmergencyFirstName;
                getAppldata.EmergencyLastName = getAppldata1.EmergencyLastName;
                getAppldata.EmergencyMobile = getAppldata1.EmergencyMobile;
                getAppldata.EmergencyHomePhone = getAppldata1.EmergencyHomePhone;
                getAppldata.EmergencyWorkPhone = getAppldata1.EmergencyWorkPhone;
                getAppldata.EmergencyEmail = getAppldata1.EmergencyEmail;
                getAppldata.EmergencyCountry = getAppldata1.EmergencyCountry;
                getAppldata.EmergencyAddress1 = getAppldata1.EmergencyAddress1;
                getAppldata.EmergencyAddress2 = getAppldata1.EmergencyAddress2;
                getAppldata.EmergencyStateHome = getAppldata1.EmergencyStateHome;
                getAppldata.EmergencyCityHome = getAppldata1.EmergencyCityHome;
                getAppldata.EmergencyZipHome = getAppldata1.EmergencyZipHome;
                getAppldata.CreatedDate = DateTime.Now.Date;
                getAppldata.OtherGender = getAppldata1.OtherGender;
                getAppldata.SSN = getAppldata1.SSN;
                getAppldata.TaxReturn = getAppldata1.TaxReturn;
                getAppldata.TaxReturn2 = getAppldata1.TaxReturn2;
                getAppldata.TaxReturn3 = getAppldata1.TaxReturn3;
                getAppldata.PassportDocument = getAppldata1.PassportDocument;
                getAppldata.IdentityDocument = getAppldata1.IdentityDocument;
                getAppldata.TaxReturnOrginalFile = model.UploadOriginalFileName1;
                getAppldata.TaxReturnOrginalFile2 = model.UploadOriginalFileName2;
                getAppldata.TaxReturnOrginalFile3 = model.UploadOriginalFileName3;
                getAppldata.PassportDocumentOriginalFile = model.UploadOriginalPassportName;
                getAppldata.IdentityDocumentOriginalFile = model.UploadOriginalIdentityName;
                getAppldata.IsPaystub = getAppldata1.IsPaystub;
                getAppldata.HaveVehicle = getAppldata1.HaveVehicle;
                getAppldata.HavePet = getAppldata1.HavePet;
                db.tbl_TenantInfo.Add(getAppldata);
                    db.SaveChanges();
                    model.TenantID = getAppldata.TenantID;

                var addLease = new tbl_Lease()
                {
                    TenantID = getAppldata.TenantID,
                    PID = Convert.ToInt32(8),
                    UID = Convert.ToInt32(model.UnitID),
                    Revision_Num = 1,
                    Status = 1,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    //Actual_MoveIn_Date=model.MoveInDate,
                };
                db.tbl_Lease.Add(addLease);
                db.SaveChanges();

                var getPayMeth = db.tbl_OnlinePayment.Where(p => p.ProspectId == model.ProspectID).FirstOrDefault();
                var addPaymentMethod = new tbl_PaymentAccounts()
                {

                    NameOnCard = getPayMeth.Name_On_Card,
                    CardNumber = getPayMeth.CardNumber,
                    CardType = 1,
                    Month = getPayMeth.CardMonth != null? getPayMeth.CardMonth.ToString():"0",
                    Year= getPayMeth.CardYear != null ? getPayMeth.CardYear.ToString() : "0",
                    TenantId= getAppldata.TenantID,
                    NickName=getPayMeth.Name_On_Card,
                    
            };
                db.tbl_PaymentAccounts.Add(addPaymentMethod);
                db.SaveChanges();


                var prospectDet = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectID).FirstOrDefault();
                if (prospectDet != null)
                {
                    prospectDet.IsApplyNow = 3;

                }
                db.SaveChanges();

                var loginDet = db.tbl_Login.Where(p => p.UserID == prospectDet.UserId).FirstOrDefault();
                if (loginDet != null)
                {
                    loginDet.UserType = 4;
                    loginDet.TenantID = model.TenantID;

                }
                db.SaveChanges();

                var transList = db.tbl_Transaction.Where(p => p.TenantID == loginDet.UserID).ToList();
                if (transList != null)
                {
                    foreach (var tl in transList)
                    {
                        tl.TenantID = model.TenantID;
                    }
                }
                db.SaveChanges();


                var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == model.UnitID).FirstOrDefault();
                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplate.html");
                if (model != null)
                {
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Tenant Registration");
                    reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + model.FirstName + " " + model.LastName + "</b>,<br/>Your Tenant Account created successfully. Please login to see status. <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + loginDet.Password);

                    reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                    reportHTML = reportHTML.Replace("[%TenantAddress%]", model.HomeAddress1);
                    reportHTML = reportHTML.Replace("[%LeaseDate%]", DateTime.Now.ToString());
                    reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                    reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                    reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString());
                    reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString());
                    reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
                }
                string body = reportHTML;
                new EmailSendModel().SendEmail(model.Email, "Tenant Registration Successfull", body);

                msg = model.TenantID.ToString();
                }
             
            db.Dispose();
            return msg;

        }


        public string SaveUpdatePostDisclaimer(TenantOnlineModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var getAppldata = db.tbl_TenantInfo.Where(p => p.TenantID == model.TenantID).FirstOrDefault();
            if (getAppldata == null)
            {
                tbl_TenantInfo objTable = new tbl_TenantInfo();

                objTable.IsAgreePostDisclaimer = model.IsAgreePostDisclaimer;
                db.tbl_TenantInfo.Add(objTable);
                db.SaveChanges();
                model.TenantID = objTable.TenantID;
                msg = "Terms and Condition Saved Successfully";
            }
            else
            {

                getAppldata.IsAgreePostDisclaimer = model.IsAgreePostDisclaimer;
                

                db.SaveChanges();
                msg = "Terms and Condition Updated Successfully";
            }

            db.Dispose();
            return msg;

        }
    }
}