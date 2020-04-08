using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Models;
using ShomaRM.Data;
using System.IO;
using System.Drawing;

namespace ShomaRM.Areas.Tenant.Models
{
    public class MyAccountModel
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
        public Nullable<long> State { get; set; }
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
        public Nullable<int> LeaseTerm { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmFirstNane { get; set; }
        public string EmMiddleName { get; set; }
        public string EmLastName { get; set; }
        public string EmEmail { get; set; }
        public string EmMobile { get; set; }
        public string EmWorkPhone { get; set; }
        public string EmHomePhone { get; set; }
        public string EmRelation { get; set; }
        public string EmAddress1 { get; set; }
        public string EmAddress2 { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> JobType { get; set; }
        public string JobTypeString { get; set; }
        public bool? HavePet { get; set; }
        public bool? HaveVehicle { get; set; }
        public string TempProfilePic { get; set; }
        public string OriginalProfileFile { get; set; }

        public string EnvelopeID { get; set; }
        public string EsignatureID { get; set; }

        public string PassportDoc { get; set; }
        public string OriginalPassportDoc { get; set; }
        public string IdentityDoc { get; set; }
        public string OriginalIdentityDoc { get; set; }
        public string TaxReturnDoc1 { get; set; }
        public string OriginalTaxReturnDoc1 { get; set; }
        public string TaxReturnDoc2 { get; set; }
        public string OriginalTaxReturnDoc2 { get; set; }
        public string TaxReturnDoc3 { get; set; }
        public string OriginalTaxReturnDoc3 { get; set; }
        public string VehicleRegistrationDoc { get; set; }
        public string OriginalVehicleRegistrationDoc { get; set; }
        public string PetPhotoDoc { get; set; }
        public string OriginalPetPhotoDoc { get; set; }
        public string PetVaccinationDoc { get; set; }
        public string OriginalPetVaccinationDoc { get; set; }
        public long UserId { get; set; }
        public string UnitName { get; set; }
        public long ProspectID { get; set; }
        public decimal MonthlyCharges { get; set; }
        

        public List<MyAccountModel> GetTenantList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyAccountModel> model = new List<MyAccountModel>();
            db.Dispose();
            return model.ToList();
        }
        public MyAccountModel GetTenantInfo(long TenantID, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MyAccountModel model = new MyAccountModel();
            var tenantData = db.tbl_TenantInfo.Where(p => p.TenantID == TenantID).FirstOrDefault();
            // ClearAllFields(model);
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
                model.ID = tenantData.TenantID;
                model.LastName = tenantData.LastName;
                model.FirstName = tenantData.FirstName;
                model.PropertyID = tenantData.PropertyID;
                model.UnitID = tenantData.UnitID;
                model.MiddleInitial = tenantData.MiddleInitial;

                var unitname = db.tbl_PropertyUnits.Where(u => u.UID == model.UnitID).FirstOrDefault();
                if (unitname != null)
                {
                    model.UnitName = unitname.UnitNo;
                }
                //var getTenantDet = db.tbl_ApplyNow.Where(p => p.UserId == UserId).FirstOrDefault();
                //model.LeaseTerm = getTenantDet.LeaseTerm;

                var  monthlyRent = db.tbl_TenantMonthlyPayments.Where(p => p.TenantID == TenantID).FirstOrDefault();
                model.MonthlyCharges =Convert.ToDecimal(monthlyRent.Charge_Amount);
            }
            db.Dispose();
            return model;
        }
        public long SaveUpdateTenant(MyAccountModel model)
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
                objTable.LastModifiedBy = Convert.ToInt32(ShomaRM.Models.ShomaGroupWebSession.CurrentUser);
                objTable.LastModifiedeDate = DateTime.Now;
                db.tbl_Tenant.Add(objTable);
                db.SaveChanges();
                // model.ID = objTable.ID;
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
        private void ClearAllFields(MyAccountModel model)
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
        public MyAccountModel GetTenantDetails(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MyAccountModel model = new MyAccountModel();
            var tenantData = db.tbl_TenantInfo.Where(p => p.TenantID == TenantID).FirstOrDefault();
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
                model.ID = tenantData.TenantID;
                model.ProspectID = Convert.ToInt64((tenantData.ProspectID).ToString());
                model.FirstName = tenantData.FirstName;
                model.MiddleInitial = tenantData.MiddleInitial;
                model.LastName = tenantData.LastName;
                model.Mobile = tenantData.Mobile;
                model.Email = tenantData.Email;
                model.EmFirstNane = tenantData.EmergencyFirstName != null ? tenantData.EmergencyFirstName : "";
                // model.EmMiddleName = tenantData.EmergencyLastName;
                model.EmLastName = tenantData.EmergencyLastName != null ? tenantData.EmergencyLastName : "";
                model.EmEmail = tenantData.EmergencyEmail != null ? tenantData.EmergencyEmail : "";
                model.EmMobile = tenantData.EmergencyMobile != null ? tenantData.EmergencyMobile : "";
                model.EmWorkPhone = tenantData.EmergencyWorkPhone != null ? tenantData.EmergencyWorkPhone : "";
                model.EmHomePhone = tenantData.EmergencyHomePhone != null ? tenantData.EmergencyHomePhone : "";
                model.EmRelation = tenantData.Relationship != null ? tenantData.Relationship : "";
                model.EmAddress1 = tenantData.EmergencyAddress1 != null ? tenantData.EmergencyAddress1 : "";
                model.EmAddress2 = tenantData.EmergencyAddress2 != null ? tenantData.EmergencyAddress2 : "";
                model.EmployerName = tenantData.EmployerName != null ? tenantData.EmployerName : "";
                model.JobTitle = tenantData.JobTitle != null ? tenantData.JobTitle : "";
                model.JobTypeString = tenantData.JobType == 1 ? "Permanent" : tenantData.JobType == 2 ? "Contract Basis" : "";
                model.JobType = tenantData.JobType;
                model.OriginalProfileFile = tenantData.OrginalProfilePicture;
                model.TempProfilePic = tenantData.ProfilePicture;

            }
            db.Dispose();
            return model;
        }
        public string UpdateContactInfo(MyAccountModel model , long UserId)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var tblTenant = db.tbl_TenantInfo.Where(p => p.TenantID == model.ID).FirstOrDefault();
            if (tblTenant != null)
            {
                tbl_TenantInfo objTable = new tbl_TenantInfo();
                tblTenant.TenantID = model.ID;
                tblTenant.FirstName = model.FirstName;
                tblTenant.LastName = model.LastName;
                tblTenant.MiddleInitial = model.MiddleInitial;
                tblTenant.Mobile = model.Mobile;
                tblTenant.Email = model.Email;
                db.SaveChanges();

                var applyNow = db.tbl_ApplyNow.Where(p => p.UserId == UserId).FirstOrDefault();
                if (applyNow != null)
                {
                    applyNow.FirstName = model.FirstName;
                    applyNow.LastName = model.LastName;
                    applyNow.Phone = model.Mobile;
                    applyNow.Email = model.Email;
                }
                db.SaveChanges();

                var loginDet = db.tbl_Login.Where(p => p.UserID == UserId).FirstOrDefault();
                if (loginDet != null)
                {
                    loginDet.FirstName = model.FirstName;
                    loginDet.LastName = model.LastName;
                    loginDet.Email = model.Email;
                    loginDet.Username = model.Email;

                }
                db.SaveChanges();

                var tenantOnline = db.tbl_TenantOnline.Where(p => p.ProspectID == tblTenant.ProspectID).FirstOrDefault();
                if (tenantOnline != null)
                {
                    tenantOnline.FirstName = model.FirstName;
                    tenantOnline.LastName = model.LastName;
                    tenantOnline.MiddleInitial = model.MiddleInitial;
                    tenantOnline.Mobile = model.Mobile;
                    tenantOnline.Email = model.Email;

                }
                db.SaveChanges();
            }

            db.Dispose();
            return msg;
        }
        public string UpdateEmContactInfo(MyAccountModel model, long UserId)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var tblTenant = db.tbl_TenantInfo.Where(p => p.TenantID == model.ID).FirstOrDefault();
            if (tblTenant != null)
            {
                tbl_TenantInfo objTable = new tbl_TenantInfo();
                tblTenant.TenantID = model.ID;
                tblTenant.EmergencyFirstName = model.EmFirstNane;
                tblTenant.EmergencyLastName = model.EmLastName;
                tblTenant.EmergencyMobile = model.EmMobile;
                tblTenant.EmergencyWorkPhone = model.EmWorkPhone;
                tblTenant.EmergencyHomePhone = model.EmHomePhone;
                tblTenant.EmergencyAddress1 = model.EmAddress1;
                tblTenant.EmergencyAddress2 = model.EmAddress2;
                tblTenant.EmergencyEmail = model.EmEmail;
                tblTenant.Relationship = model.EmRelation;
                db.SaveChanges();
            }
            var getTenatId = db.tbl_ApplyNow.Where(co => co.UserId == UserId).FirstOrDefault();

            var tblTInfo = db.tbl_TenantInfo.Where(p => p.ProspectID == getTenatId.ID).FirstOrDefault();

            var tblTenantOnline = db.tbl_TenantOnline.Where(p => p.ProspectID == tblTInfo.ProspectID).FirstOrDefault();
            if (tblTenantOnline != null)
            {
                tbl_TenantOnline objTable = new tbl_TenantOnline();

                tblTenantOnline.EmergencyFirstName = model.EmFirstNane;
                tblTenantOnline.EmergencyLastName = model.EmLastName;
                tblTenantOnline.EmergencyMobile = model.EmMobile;
                tblTenantOnline.EmergencyWorkPhone = model.EmWorkPhone;
                tblTenantOnline.EmergencyHomePhone = model.EmHomePhone;
                tblTenantOnline.EmergencyAddress1 = model.EmAddress1;
                tblTenantOnline.EmergencyAddress2 = model.EmAddress2;
                tblTenantOnline.EmergencyEmail = model.EmEmail;
                tblTenantOnline.Relationship = model.EmRelation;
                db.SaveChanges();
            }

            db.Dispose();
            return msg;
        }
        public string UpdateWorkInfo(MyAccountModel model,long UserID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var tblTenant = db.tbl_TenantInfo.Where(p => p.TenantID == model.ID).FirstOrDefault();
            if (tblTenant != null)
            {
                tbl_TenantInfo objTable = new tbl_TenantInfo();
                tblTenant.TenantID = model.ID;
                tblTenant.JobTitle = model.JobTitle;
                tblTenant.JobType = Convert.ToInt32(model.JobType);
                tblTenant.EmployerName = model.EmployerName;
                db.SaveChanges();

            }
            var getTenatId = db.tbl_ApplyNow.Where(co => co.UserId == UserID).FirstOrDefault();

            var tblTInfo = db.tbl_TenantInfo.Where(p => p.ProspectID == getTenatId.ID).FirstOrDefault();

            var tblTenantOnline = db.tbl_TenantOnline.Where(p => p.ProspectID == tblTInfo.ProspectID).FirstOrDefault();
            if (tblTenantOnline != null)
            {
                tbl_TenantOnline objTable = new tbl_TenantOnline();

                tblTenantOnline.JobTitle = model.JobTitle;
                tblTenantOnline.JobType = Convert.ToInt32(model.JobType);
                tblTenantOnline.EmployerName = model.EmployerName;
                db.SaveChanges();
            }


            db.Dispose();
            return msg;
        }
        public MyAccountModel CheckTenantPet(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MyAccountModel model = new MyAccountModel();

            var tenantData = db.tbl_TenantInfo.Where(p => p.TenantID == TenantID).FirstOrDefault();

            if (tenantData != null)
            {
                model.HavePet = tenantData.HavePet;

                if (tenantData.HavePet != true)
                {
                    // var petstorage = db.tbl_ApplyNow.Where(p => p.tena == TenantID).FirstOrDefault();

                }

            }
            db.Dispose();
            return model;
        }
        public string SaveProfilePic(MyAccountModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var tblTenant = db.tbl_TenantInfo.Where(p => p.TenantID == model.ID).FirstOrDefault();
            if (tblTenant != null)
            {
                tbl_TenantInfo objTable = new tbl_TenantInfo();
                tblTenant.TenantID = model.ID;
                tblTenant.ProfilePicture = model.TempProfilePic;
                tblTenant.OrginalProfilePicture = model.OriginalProfileFile;
                db.SaveChanges();

            }

            db.Dispose();
            return msg;
        }
        public string GetProcessingFees()
        {
            string processingFees = "0.00";
            ShomaRMEntities db = new ShomaRMEntities();
            var propertyDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
            if (propertyDet != null)
            {
                processingFees = (propertyDet.ProcessingFees ?? 0).ToString("0.00");

            }
            db.Dispose();
            return processingFees;
        }
        public MyAccountModel UploadProfile(HttpPostedFileBase fileBaseUpload, MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MyAccountModel profilePic = new MyAccountModel();
            
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUpload != null && fileBaseUpload.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/tenantProfile/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUpload.FileName;
                Extension = Path.GetExtension(fileBaseUpload.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload.FileName);
                fileBaseUpload.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUpload.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/tenantProfile/") + "/" + sysFileName;

                }
                profilePic.TempProfilePic = sysFileName.ToString();
                profilePic.OriginalProfileFile = fileName;
            }

            return profilePic;
        }
        public MyAccountModel GetAllLeaseDocuments(MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var appNow = db.tbl_ApplyNow.Where(co => co.UserId == model.UserId).FirstOrDefault();
            if (appNow != null)
            {
                var getCertificates = db.tbl_TenantOnline.Where(co => co.ProspectID == appNow.ID).FirstOrDefault();
                if (getCertificates != null)
                {
                    model.EnvelopeID = appNow.EnvelopeID;
                    model.PassportDoc = getCertificates.PassportDocument;
                    model.OriginalPassportDoc = getCertificates.PassportDocumentOriginalFile;
                    model.IdentityDoc = getCertificates.IdentityDocument;
                    model.OriginalIdentityDoc = getCertificates.IdentityDocumentOriginalFile;
                    model.TaxReturnDoc1 = getCertificates.TaxReturn;
                    model.OriginalTaxReturnDoc1 = getCertificates.TaxReturnOrginalFile;
                    model.TaxReturnDoc2 = getCertificates.TaxReturn2;
                    model.OriginalTaxReturnDoc2 = getCertificates.TaxReturnOrginalFile2;
                    model.TaxReturnDoc3 = getCertificates.TaxReturn3;
                    model.OriginalTaxReturnDoc3 = getCertificates.TaxReturnOrginalFile3;
                }
            }

            return model;
        }
        public List<MyAccountModel> GetPetLeaseDocuments(MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyAccountModel> listPetCertificate = new List<MyAccountModel>();
            var appNow = db.tbl_ApplyNow.Where(co => co.UserId == model.UserId).FirstOrDefault();
            if (appNow != null)
            {
                var getPetCertificates = db.tbl_TenantPet.Where(co => co.TenantID == appNow.ID).ToList();
                if (getPetCertificates != null)
                {
                    foreach (var item in getPetCertificates)
                    {
                        listPetCertificate.Add(new MyAccountModel()
                        {
                            PetPhotoDoc = item.Photo,
                            OriginalPetPhotoDoc = item.OriginalPhoto,
                            PetVaccinationDoc = item.PetVaccinationCert,
                            OriginalPetVaccinationDoc = item.OriginalVaccinationCert
                        });
                    }
                }
            }

            return listPetCertificate;
        }
        public List<MyAccountModel> GetVehicleLeaseDocuments(MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyAccountModel> listVehicleCertificate = new List<MyAccountModel>();
            var appNow = db.tbl_ApplyNow.Where(co => co.UserId == model.UserId).FirstOrDefault();
            if (appNow != null)
            {
                var getVehicleCertificates = db.tbl_Vehicle.Where(co => co.TenantID == appNow.ID).ToList();
                if (getVehicleCertificates != null)
                {
                    foreach (var item in getVehicleCertificates)
                    {
                        listVehicleCertificate.Add(new MyAccountModel()
                        {
                            VehicleRegistrationDoc = item.VehicleRegistration,
                            OriginalVehicleRegistrationDoc = item.OriginalVehicleReg
                        });
                    }
                }
            }

            return listVehicleCertificate;
        }
        public MyAccountModel GetTenantLeaseDocuments(MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var appNow = db.tbl_ApplyNow.Where(co => co.ID == model.UserId).FirstOrDefault();
            if (appNow != null)
            {
                var getCertificates = db.tbl_TenantOnline.Where(co => co.ProspectID == appNow.ID).FirstOrDefault();
                if (getCertificates != null)
                {
                    model.EnvelopeID = appNow.EnvelopeID;
                    model.EsignatureID = appNow.EsignatureID;
                    model.PassportDoc = getCertificates.PassportDocument;
                    model.OriginalPassportDoc = getCertificates.PassportDocumentOriginalFile;
                    model.IdentityDoc = getCertificates.IdentityDocument;
                    model.OriginalIdentityDoc = getCertificates.IdentityDocumentOriginalFile;
                    model.TaxReturnDoc1 = getCertificates.TaxReturn;
                    model.OriginalTaxReturnDoc1 = getCertificates.TaxReturnOrginalFile;
                    model.TaxReturnDoc2 = getCertificates.TaxReturn2;
                    model.OriginalTaxReturnDoc2 = getCertificates.TaxReturnOrginalFile2;
                    model.TaxReturnDoc3 = getCertificates.TaxReturn3;
                    model.OriginalTaxReturnDoc3 = getCertificates.TaxReturnOrginalFile3;
                }
            }

            return model;
        }
        public List<MyAccountModel> GetTenantPetLeaseDocuments(MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyAccountModel> listPetCertificate = new List<MyAccountModel>();
            var appNow = db.tbl_ApplyNow.Where(co => co.ID == model.UserId).FirstOrDefault();
            if (appNow != null)
            {
                var getPetCertificates = db.tbl_TenantPet.Where(co => co.TenantID == appNow.ID).ToList();
                if (getPetCertificates != null)
                {
                    foreach (var item in getPetCertificates)
                    {
                        listPetCertificate.Add(new MyAccountModel()
                        {
                            PetPhotoDoc = item.Photo,
                            OriginalPetPhotoDoc = item.OriginalPhoto,
                            PetVaccinationDoc = item.PetVaccinationCert,
                            OriginalPetVaccinationDoc = item.OriginalVaccinationCert
                        });
                    }
                }
            }

            return listPetCertificate;
        }
        public List<MyAccountModel> GetTenantVehicleLeaseDocuments(MyAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyAccountModel> listVehicleCertificate = new List<MyAccountModel>();
            var appNow = db.tbl_ApplyNow.Where(co => co.ID == model.UserId).FirstOrDefault();
            if (appNow != null)
            {
                var getVehicleCertificates = db.tbl_Vehicle.Where(co => co.TenantID == appNow.ID).ToList();
                if (getVehicleCertificates != null)
                {
                    foreach (var item in getVehicleCertificates)
                    {
                        listVehicleCertificate.Add(new MyAccountModel()
                        {
                            VehicleRegistrationDoc = item.VehicleRegistration,
                            OriginalVehicleRegistrationDoc = item.OriginalVehicleReg
                        });
                    }
                }
            }

            return listVehicleCertificate;
        }
        public List<AmenitiesModel> GetAmenityList()
        {

            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenitiesModel> listAmenity = new List<AmenitiesModel>();
            var amenityList = db.tbl_Amenities.ToList();
            if(amenityList != null)
            {
                foreach (var item in amenityList)
                {

                    listAmenity.Add(new AmenitiesModel()
                    {
                        ID = item.ID,
                        Amenity = item.Amenity,
                        AmenityDetails = item.AmenityDetails
                    });
                }
            }
            
            db.Dispose();
            return listAmenity;
        }
        public class AmenitiesModel
        {
            public long ID { get; set; }
            public string Amenity { get; set; }
            public string AmenityDetails { get; set; }
         

        }
    }
}