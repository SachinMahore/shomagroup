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
using System.Web.Configuration;
using ShomaRM.Models.TwilioApi;

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
        public int IsPaystub { get; set; }
        public string StatePersonalString { get; set; }
        public string StateHomeString { get; set; }
        public string CountryString { get; set; }
        public string OfficeCountryString { get; set; }
        public string EmergencyCountryString { get; set; }
        public string EmergencyStateHomeString { get; set; }
        public int StepCompleted { get; set; }
        public Nullable<long> CountryOfOrigin { get; set; }
        public Nullable<int> Evicted { get; set; }
        public string EvictedDetails { get; set; }
        public Nullable<int> ConvictedFelony { get; set; }
        public string ConvictedFelonyDetails { get; set; }
        public Nullable<int> CriminalChargPen { get; set; }
        public string CriminalChargPenDetails { get; set; }
        public Nullable<int> DoYouSmoke { get; set; }
        public Nullable<int> ReferredResident { get; set; }
        public string ReferredResidentName { get; set; }
        public Nullable<int> ReferredBrokerMerchant { get; set; }
        public string ApartmentCommunity { get; set; }
        public string ManagementCompany { get; set; }
        public string ManagementCompanyPhone { get; set; }
        public Nullable<int> IsProprNoticeLeaseAgreement { get; set; }
        public string CountryOfOriginString { get; set; }

        public string StringEvicted { get; set; }
        public String StringConvictedFelony { get; set; }
        public String StringCriminalChargPen { get; set; }
        public string StringDoYouSmoke { get; set; }
        public String StringReferredResident { get; set; }
        public String StringReferredBrokerMerchant { get; set; }
        public string stringIsProprNoticeLeaseAgreement { get; set; }

        public int StepCompletedCoappGu { get; set; }

        public string CreatedDateString { get; set; }
        public string ExpireDate { get; set; }
        //sachin m 11 may
        public string TaxReturn4 { get; set; }
        public string TaxReturn5 { get; set; }
        public string TaxReturn6 { get; set; }
        public string TaxReturn7 { get; set; }
        public string TaxReturn8 { get; set; }
        public string tempUpload4 { get; set; }
        public string UploadOriginalFileName4 { get; set; }
        public string tempUpload5 { get; set; }
        public string UploadOriginalFileName5 { get; set; }
        public string tempUpload6 { get; set; }
        public string UploadOriginalFileName6 { get; set; }
        public string tempUpload7 { get; set; }
        public string UploadOriginalFileName7 { get; set; }
        public string tempUpload8 { get; set; }
        public string UploadOriginalFileName8 { get; set; }
        public int IsFedralTax { get; set; }
        public int IsBankState { get; set; }
        //sachin m 13 may
        public List<ApplicantHistoryModel> lstaph { get; set; }
        public string StateString { get; set; }
        public string RentOwnString { get; set; }
        public string AppType { get; set; }
        public Nullable<int> ResidenceStatus { get; set; }
        public string ResidenceNotes { get; set; }
        public Nullable<int> EmpStatus { get; set; }
        public string EmpNotes { get; set; }
        public string UserID { get; set; }
        public string FloorPlanImageUnit { get; set; }
        public string FloorPlanBedUnit { get; set; }
        public string FloorPlanBathUnit { get; set; }
        public string FloorPlanAreaUnit { get; set; }
        public string FloorPlanStartPriceUnit { get; set; }
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];
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
                long toid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
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
                    //Sachin Mahore 21 Apr 2020
                    DbParameter paramfF = cmd.CreateParameter();
                    paramfF.ParameterName = "toid";
                    paramfF.Value = toid;
                    cmd.Parameters.Add(paramfF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                lstpr.ID = 0;
                lstpr.IsInternational = 0;
                lstpr.Gender = 0;
                lstpr.IDType = 0;
                lstpr.State = 0;
                lstpr.Country = "1";
                lstpr.StateHome = 0;
                lstpr.RentOwn = 0;

                lstpr.Country2 = "0";
                lstpr.StateHome2 = 0;
                lstpr.RentOwn2 = 0;

                lstpr.JobType = 0;
                lstpr.OfficeCountry = "1";
                lstpr.OfficeState = 0;

                lstpr.EmergencyCountry = "1";
                lstpr.EmergencyStateHome = 0;
                lstpr.StepCompleted = 1;
                foreach (DataRow dr in dtTable.Rows)
                {
                    DateTime? dateOfBirth = null;
                    try { dateOfBirth = Convert.ToDateTime(dr["DateOfBirth"].ToString()); }
                    catch { }
                    DateTime? dateIssuance = null;
                    try { dateIssuance = Convert.ToDateTime(dr["DateIssuance"].ToString()); }
                    catch { }
                    DateTime? dateExpire = null;
                    try { dateExpire = Convert.ToDateTime(dr["DateExpire"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom = null;
                    try { moveInDateFrom = Convert.ToDateTime(dr["MoveInDateFrom"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo = null;
                    try { moveInDateTo = Convert.ToDateTime(dr["MoveInDateTo"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom2 = null;
                    try { moveInDateFrom2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo2 = null;
                    try { moveInDateTo2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? startDate = null;
                    try { startDate = Convert.ToDateTime(dr["StartDate"].ToString()); }
                    catch { }
                    lstpr.ID = Convert.ToInt32(dr["ID"].ToString());
                    lstpr.ProspectID= Convert.ToInt32(dr["ProspectID"].ToString());
                    lstpr.IsInternational = Convert.ToInt32(dr["IsInternational"].ToString());
                    lstpr.IsAdditionalRHistory = Convert.ToInt32(dr["IsAdditionalRHistory"].ToString());
                    lstpr.FirstName = dr["FirstName"].ToString();
                    lstpr.MiddleInitial = dr["MiddleInitial"].ToString();
                    lstpr.LastName = dr["LastName"].ToString();
                    lstpr.DateOfBirthTxt = dateOfBirth == null ? "" : dateOfBirth.Value.ToString("MM/dd/yyy");
                    lstpr.Gender = Convert.ToInt32(dr["Gender"].ToString());
                    lstpr.Email = dr["Email"].ToString();
                    lstpr.Mobile = dr["Mobile"].ToString();

                    if (!string.IsNullOrWhiteSpace(dr["PassportNumber"].ToString()))
                    {
                        string decryptedPassportNumber = new EncryptDecrypt().DecryptText(dr["PassportNumber"].ToString());
                        int passportlength = decryptedPassportNumber.Length > 4 ? decryptedPassportNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < passportlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedPassportNumber.Length > 4)
                        {
                            lstpr.PassportNumber = maskidnumber + decryptedPassportNumber.Substring(decryptedPassportNumber.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.PassportNumber = decryptedPassportNumber;
                        }
                    }
                    else
                    {
                        lstpr.PassportNumber = "";
                    }

                    lstpr.CountryIssuance = dr["CountryIssuance"].ToString();
                    lstpr.DateIssuanceTxt = dateIssuance == null ? "" : dateIssuance.Value.ToString("MM/dd/yyy");
                    lstpr.DateExpireTxt = dateExpire == null ? "" : dateExpire.Value.ToString("MM/dd/yyy");
                    lstpr.IDType = Convert.ToInt32(dr["IDType"].ToString());
                    lstpr.State = Convert.ToInt64(dr["State"].ToString());
                    if (!string.IsNullOrWhiteSpace(dr["IDNumber"].ToString()))
                    {
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(dr["IDNumber"].ToString());
                        int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < idnumlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedIDNumber.Length > 4)
                        {
                            lstpr.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.IDNumber = decryptedIDNumber;
                        }
                    }
                    else
                    {
                        lstpr.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(dr["SSN"].ToString()))
                    {
                        string decryptedSSN = new EncryptDecrypt().DecryptText(dr["SSN"].ToString());
                        if (decryptedSSN.Length > 5)
                        {
                            lstpr.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.SSN = decryptedSSN;
                        }
                    }
                    else
                    {
                        lstpr.SSN = "";
                    }
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
                    lstpr.IsPaystub = Convert.ToInt32(dr["IsPaystub"].ToString());

                    lstpr.CountryOfOrigin = Convert.ToInt64(dr["CountryOfOrigin"].ToString());
                    lstpr.Evicted = Convert.ToInt32(dr["Evicted"].ToString());
                    lstpr.EvictedDetails = dr["EvictedDetails"].ToString();
                    lstpr.ConvictedFelony = Convert.ToInt32(dr["ConvictedFelony"].ToString());
                    lstpr.ConvictedFelonyDetails = dr["ConvictedFelonyDetails"].ToString();
                    lstpr.CriminalChargPen = Convert.ToInt32(dr["CriminalChargPen"].ToString());
                    lstpr.CriminalChargPenDetails = dr["CriminalChargPenDetails"].ToString();
                    lstpr.DoYouSmoke = Convert.ToInt32(dr["DoYouSmoke"].ToString());
                    lstpr.ReferredResident = Convert.ToInt32(dr["ReferredResident"].ToString());
                    lstpr.ReferredResidentName = dr["ReferredResidentName"].ToString();
                    lstpr.ReferredBrokerMerchant = Convert.ToInt32(dr["ReferredBrokerMerchant"].ToString());
                    lstpr.ApartmentCommunity = dr["ApartmentCommunity"].ToString();
                    lstpr.ManagementCompany = dr["ManagementCompany"].ToString();
                    lstpr.ManagementCompanyPhone = dr["ManagementCompanyPhone"].ToString();
                    lstpr.IsProprNoticeLeaseAgreement = Convert.ToInt32(dr["IsProprNoticeLeaseAgreement"].ToString());

                    int countryOrigintemp = lstpr.CountryOfOrigin.HasValue ? Convert.ToInt32(lstpr.CountryOfOrigin) : 0;
                    var countryOriginVar = db.tbl_Country.Where(co => co.ID == countryOrigintemp).FirstOrDefault();
                    lstpr.CountryOfOriginString = countryOriginVar != null ? countryOriginVar.CountryName : "";

                    //var PersonalStateVar = db.tbl_State.Where(co => co.ID == lstpr.State).FirstOrDefault();
                    //lstpr.StatePersonalString = PersonalStateVar != null ? PersonalStateVar.StateName : "";
                    //var StateHomeVar = db.tbl_State.Where(co => co.ID == lstpr.StateHome).FirstOrDefault();
                    //lstpr.StateHomeString = StateHomeVar != null ? StateHomeVar.StateName : "";
                    //int contryTemp = lstpr.Country != "" ? Convert.ToInt32(lstpr.Country) : 0;
                    //var CountryVar = db.tbl_Country.Where(co => co.ID == contryTemp).FirstOrDefault();
                    //lstpr.CountryString = CountryVar != null ? CountryVar.CountryName : "";
                    //int officeContryTemp = lstpr.OfficeCountry != "" ? Convert.ToInt32(lstpr.OfficeCountry) : 0;
                    //var OfficeCountryVar = db.tbl_Country.Where(co => co.ID == officeContryTemp).FirstOrDefault();
                    //lstpr.OfficeCountryString = OfficeCountryVar != null ? OfficeCountryVar.CountryName : "";
                    //int emergencyContryTemp = lstpr.EmergencyCountry != "" ? Convert.ToInt32(lstpr.EmergencyCountry) : 0;
                    //var EmergencyCountryVar = db.tbl_Country.Where(co => co.ID == emergencyContryTemp).FirstOrDefault();
                    //lstpr.EmergencyCountryString = EmergencyCountryVar != null ? EmergencyCountryVar.CountryName : "";
                    //int emergencyStateHomeTemp = lstpr.EmergencyStateHome != null ? Convert.ToInt32(lstpr.EmergencyStateHome) : 0;
                    //var EmergencyStateHomeVar = db.tbl_State.Where(co => co.ID == emergencyStateHomeTemp).FirstOrDefault();
                    //lstpr.EmergencyStateHomeString = EmergencyStateHomeVar != null ? EmergencyStateHomeVar.StateName : "";

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

                    lstpr.StringEvicted = Convert.ToInt32(dr["Evicted"]) == 1 ? "No" : "Yes";
                    lstpr.StringConvictedFelony = Convert.ToInt32(dr["ConvictedFelony"]) == 1 ? "No" : "Yes";
                    lstpr.StringCriminalChargPen = Convert.ToInt32(dr["CriminalChargPen"]) == 1 ? "No" : "Yes";
                    lstpr.StringDoYouSmoke = Convert.ToInt32(dr["DoYouSmoke"]) == 1 ? "No" : "Yes";
                    lstpr.StringReferredResident = Convert.ToInt32(dr["ReferredResident"]) == 1 ? "No" : "Yes";
                    lstpr.StringReferredBrokerMerchant = Convert.ToInt32(dr["ReferredBrokerMerchant"]) == 1 ? "No" : "Yes";
                    lstpr.stringIsProprNoticeLeaseAgreement = Convert.ToInt32(dr["IsProprNoticeLeaseAgreement"]) == 1 ? "Yes" : "No";

                    //sachin m 11 may
                    lstpr.TaxReturn4 = dr["TaxReturn4"].ToString();
                    lstpr.TaxReturn5 = dr["TaxReturn5"].ToString();
                    lstpr.TaxReturn6 = dr["TaxReturn6"].ToString();
                    lstpr.TaxReturn7 = dr["TaxReturn7"].ToString();
                    lstpr.TaxReturn8 = dr["TaxReturn8"].ToString();
                    lstpr.UploadOriginalFileName4 = dr["TaxReturnOrginalFile4"].ToString();
                    lstpr.UploadOriginalFileName5 = dr["TaxReturnOrginalFile5"].ToString();
                    lstpr.UploadOriginalFileName6 = dr["TaxReturnOrginalFile6"].ToString();
                    lstpr.UploadOriginalFileName7 = dr["TaxReturnOrginalFile7"].ToString();
                    lstpr.UploadOriginalFileName8 = dr["TaxReturnOrginalFile8"].ToString();
                    lstpr.IsFedralTax = Convert.ToInt32(dr["IsFedralTax"].ToString());
                    lstpr.IsBankState = Convert.ToInt32(dr["IsBankState"].ToString());

                    var stepCompleted = Convert.ToInt32(dr["StepCompleted"].ToString());
                    lstpr.StepCompleted = stepCompleted;

                    var getApplyNowData = db.tbl_ApplyNow.Where(co => co.ID == id).FirstOrDefault();
                    if (getApplyNowData != null)
                    {
                        var getPropertyUnitData = db.tbl_PropertyUnits.Where(co => co.UID == getApplyNowData.PropertyId).FirstOrDefault();
                        lstpr.FloorPlanImageUnit = getPropertyUnitData.Building;
                        lstpr.FloorPlanBedUnit = !string.IsNullOrWhiteSpace(Convert.ToString(getPropertyUnitData.Bedroom)) ? Convert.ToString(getPropertyUnitData.Bedroom) : "";
                        lstpr.FloorPlanBathUnit = !string.IsNullOrWhiteSpace(Convert.ToString(getPropertyUnitData.Bathroom)) ? Convert.ToString(getPropertyUnitData.Bathroom) : "";
                        lstpr.FloorPlanAreaUnit = getPropertyUnitData.Area;
                        var getUnitLesePriceData = db.tbl_UnitLeasePrice.Where(co => co.UnitID == getPropertyUnitData.UID).FirstOrDefault();
                        if (getUnitLesePriceData != null)
                        {
                            lstpr.FloorPlanStartPriceUnit = getUnitLesePriceData.Price.Value.ToString("0.00");
                        }
                    }
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
        public string GetSSNIdNumberPassportNumber(int id, int vid)
        {
            //Sachin Mahore 21 Apr 2020
            ShomaRMEntities db = new ShomaRMEntities();
            string result = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var appData = db.tbl_TenantOnline.Where(p => p.ProspectID == id && p.ParentTOID == userid).FirstOrDefault();
            if (appData != null)
            {
                if (vid == 1)
                {
                    result = !string.IsNullOrWhiteSpace(appData.SSN) ? new EncryptDecrypt().DecryptText(appData.SSN) : "";
                }
                else if (vid == 2)
                {
                    result = !string.IsNullOrWhiteSpace(appData.PassportNumber) ? new EncryptDecrypt().DecryptText(appData.PassportNumber) : "";
                }
                else if (vid == 3)
                {
                    result = !string.IsNullOrWhiteSpace(appData.IDNumber) ? new EncryptDecrypt().DecryptText(appData.IDNumber) : "";
                }
            }
            return result;
        }
        public string SaveTenantOnline(TenantOnlineModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.ProspectID != 0)
            {
                int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
                var applyNow = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectID).FirstOrDefault();
                var getAppldata = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID && p.ParentTOID == userid).FirstOrDefault();
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
                    //getAppldata.PassportNumber = model.PassportNumber;
                    getAppldata.CountryIssuance = model.CountryIssuance;
                    getAppldata.DateIssuance = model.DateIssuance;
                    getAppldata.DateExpire = model.DateExpire;
                    getAppldata.IDType = model.IDType;
                    getAppldata.State = model.State;
                    // getAppldata.IDNumber = model.IDNumber;
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
                        //getAppldata.MoveInDateTo = model.MoveInDateTo;
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
                    // getAppldata.SSN = model.SSN;
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
                    getAppldata.CountryOfOrigin = model.CountryOfOrigin;
                    getAppldata.Evicted = model.Evicted;
                    getAppldata.EvictedDetails = model.EvictedDetails;
                    getAppldata.ConvictedFelony = model.ConvictedFelony;
                    getAppldata.ConvictedFelonyDetails = model.ConvictedFelonyDetails;
                    getAppldata.CriminalChargPen = model.CriminalChargPen;
                    getAppldata.CriminalChargPenDetails = model.CriminalChargPenDetails;
                    getAppldata.DoYouSmoke = model.DoYouSmoke;
                    getAppldata.ReferredResident = model.ReferredResident;
                    getAppldata.ReferredResidentName = model.ReferredResidentName;
                    getAppldata.ReferredBrokerMerchant = model.ReferredBrokerMerchant;
                    getAppldata.ApartmentCommunity = model.ApartmentCommunity;
                    getAppldata.ManagementCompany = model.ManagementCompany;
                    getAppldata.ManagementCompanyPhone = model.ManagementCompanyPhone;
                    getAppldata.IsProprNoticeLeaseAgreement = model.IsProprNoticeLeaseAgreement;

                    //sachin m 11 may
                    getAppldata.TaxReturn4 = model.TaxReturn4;
                    getAppldata.TaxReturn5 = model.TaxReturn5;
                    getAppldata.TaxReturn6 = model.TaxReturn6;
                    getAppldata.TaxReturn7 = model.TaxReturn7;
                    getAppldata.TaxReturn8 = model.TaxReturn8;
                    getAppldata.TaxReturnOrginalFile4 = model.UploadOriginalFileName4;
                    getAppldata.TaxReturnOrginalFile5 = model.UploadOriginalFileName5;
                    getAppldata.TaxReturnOrginalFile6 = model.UploadOriginalFileName6;
                    getAppldata.TaxReturnOrginalFile7 = model.UploadOriginalFileName7;
                    getAppldata.TaxReturnOrginalFile8 = model.UploadOriginalFileName8;
                    getAppldata.IsFedralTax = model.IsFedralTax;
                    getAppldata.IsBankState = model.IsBankState;

                    db.SaveChanges();
                    //Sachin Mahore 21 Apr 2020
                    var updateGuarantorPhone = db.tbl_Applicant.Where(co => co.TenantID == model.ProspectID && co.Email == model.Email).FirstOrDefault();
                    if (updateGuarantorPhone != null)
                    {
                        updateGuarantorPhone.Phone = model.Mobile;
                        db.SaveChanges();
                    }
                    if (applyNow != null)
                    {
                        int stepcomp = 0;
                        stepcomp = applyNow.StepCompleted ?? 0;
                        if (stepcomp < model.StepCompleted)
                        {
                            stepcomp = model.StepCompleted;
                        }
                        applyNow.StepCompleted = stepcomp;
                        db.SaveChanges();
                        var tenentUID = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
                        var tenantData = db.tbl_TenantOnline.Where(p => p.ParentTOID == tenentUID).FirstOrDefault();
                        if (tenantData != null)
                        {
                            tenantData.StepCompleted = stepcomp;
                            db.SaveChanges();
                        }
                    }
                }

                var saveApplicantGender = db.tbl_Applicant.Where(p => p.Email == model.Email).FirstOrDefault();
                if (saveApplicantGender != null)
                {
                    saveApplicantGender.DateOfBirth = model.DateOfBirth;
                    saveApplicantGender.Gender = model.Gender;
                    saveApplicantGender.OtherGender = model.OtherGender;
                    saveApplicantGender.Relationship = "1";
                    db.SaveChanges();
                }

                msg = "Applicant Updated Successfully";
            }


            db.Dispose();
            return msg;

        }
        //Sachin Mahore 28 Apr 2020
        public string SaveCoGuTenantOnline(TenantOnlineModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.ProspectID != 0)
            {

                var applyNow = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectID).FirstOrDefault();
                var getAppldata = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID && p.ParentTOID == userid).FirstOrDefault();
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
                    //getAppldata.PassportNumber = model.PassportNumber;
                    getAppldata.CountryIssuance = model.CountryIssuance;
                    getAppldata.DateIssuance = model.DateIssuance;
                    getAppldata.DateExpire = model.DateExpire;
                    getAppldata.IDType = model.IDType;
                    getAppldata.State = model.State;
                    // getAppldata.IDNumber = model.IDNumber;
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
                        //getAppldata.MoveInDateTo = model.MoveInDateTo;
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
                    // getAppldata.SSN = model.SSN;
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
                    getAppldata.CountryOfOrigin = model.CountryOfOrigin;
                    getAppldata.Evicted = model.Evicted;
                    getAppldata.EvictedDetails = model.EvictedDetails;
                    getAppldata.ConvictedFelony = model.ConvictedFelony;
                    getAppldata.ConvictedFelonyDetails = model.ConvictedFelonyDetails;
                    getAppldata.CriminalChargPen = model.CriminalChargPen;
                    getAppldata.CriminalChargPenDetails = model.CriminalChargPenDetails;
                    getAppldata.DoYouSmoke = model.DoYouSmoke;
                    getAppldata.ReferredResident = model.ReferredResident;
                    getAppldata.ReferredResidentName = model.ReferredResidentName;
                    getAppldata.ReferredBrokerMerchant = model.ReferredBrokerMerchant;
                    getAppldata.ApartmentCommunity = model.ApartmentCommunity;
                    getAppldata.ManagementCompany = model.ManagementCompany;
                    getAppldata.ManagementCompanyPhone = model.ManagementCompanyPhone;
                    getAppldata.IsProprNoticeLeaseAgreement = model.IsProprNoticeLeaseAgreement;

                    getAppldata.StepCompleted = model.StepCompleted;

                    getAppldata.TaxReturn4 = model.TaxReturn4;
                    getAppldata.TaxReturn5 = model.TaxReturn5;
                    getAppldata.TaxReturn6 = model.TaxReturn6;
                    getAppldata.TaxReturn7 = model.TaxReturn7;
                    getAppldata.TaxReturn8 = model.TaxReturn8;
                    getAppldata.TaxReturnOrginalFile4 = model.UploadOriginalFileName4;
                    getAppldata.TaxReturnOrginalFile5 = model.UploadOriginalFileName5;
                    getAppldata.TaxReturnOrginalFile6 = model.UploadOriginalFileName6;
                    getAppldata.TaxReturnOrginalFile7 = model.UploadOriginalFileName7;
                    getAppldata.TaxReturnOrginalFile8 = model.UploadOriginalFileName8;
                    getAppldata.IsFedralTax = model.IsFedralTax;
                    getAppldata.IsBankState = model.IsBankState;

                    db.SaveChanges();
                }

                var saveApplicantGender = db.tbl_Applicant.Where(p => p.Email == model.Email).FirstOrDefault();
                if (saveApplicantGender != null)
                {

                    saveApplicantGender.DateOfBirth = model.DateOfBirth;
                    saveApplicantGender.Gender = model.Gender;
                    saveApplicantGender.OtherGender = model.OtherGender;
                    saveApplicantGender.Relationship = "1";
                    saveApplicantGender.Phone = model.Mobile;
                    db.SaveChanges();

                }

                msg = "Applicant Updated Successfully";
            }

            db.Dispose();
            return msg;

        }
        public string SaveUpdateSSN(TenantOnlineModel model)
        {
            //Sachin Mahore 22 Apr 2020
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.ProspectID != 0)
            {
                string encryptedSSN = new EncryptDecrypt().EncryptText(model.SSN);
                var getSSNdata = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID && p.ParentTOID == userid).FirstOrDefault();
                if (getSSNdata != null)
                {
                    getSSNdata.SSN = encryptedSSN;
                }
                db.SaveChanges();

                msg = "SSN Number Updated Successfully";
            }


            db.Dispose();
            return msg;

        }
        public string SaveUpdateIDNumber(TenantOnlineModel model)
        {
            //Sachin Mahore 22 Apr 2020
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.ProspectID != 0)
            {
                string encryptedData = new EncryptDecrypt().EncryptText(model.IDNumber);
                var getdata = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID && p.ParentTOID == userid).FirstOrDefault();
                if (getdata != null)
                {
                    getdata.IDNumber = encryptedData;
                }
                db.SaveChanges();

                msg = "IDNumber Number Updated Successfully";
            }
            db.Dispose();
            return msg;
        }
        public string SaveUpdatePassportNumber(TenantOnlineModel model)
        {
            //Sachin Mahore 22 Apr 2020
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.ProspectID != 0)
            {
                string encryptedData = new EncryptDecrypt().EncryptText(model.PassportNumber);
                var getdata = db.tbl_TenantOnline.Where(p => p.ProspectID == model.ProspectID && p.ParentTOID == userid).FirstOrDefault();
                if (getdata != null)
                {
                    getdata.PassportNumber = encryptedData;
                }
                db.SaveChanges();

                msg = "Passport Number Updated Successfully";
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


        public TenantOnlineModel SaveTaxUpload4(HttpPostedFileBase fileBaseUpload4, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU3 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload4 != null && fileBaseUpload4.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload4.FileName;
                    Extension = Path.GetExtension(fileBaseUpload4.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload4.FileName);
                    fileBaseUpload4.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload4.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU3.tempUpload4 = sysFileName.ToString();
                    tenantModelU3.UploadOriginalFileName4 = fileName;
                }

            }
            return tenantModelU3;
        }
        public TenantOnlineModel SaveTaxUpload5(HttpPostedFileBase fileBaseUpload5, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU3 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload5 != null && fileBaseUpload5.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload5.FileName;
                    Extension = Path.GetExtension(fileBaseUpload5.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload5.FileName);
                    fileBaseUpload5.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload5.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU3.tempUpload5 = sysFileName.ToString();
                    tenantModelU3.UploadOriginalFileName5 = fileName;
                }

            }
            return tenantModelU3;
        }
        public TenantOnlineModel SaveTaxUpload6(HttpPostedFileBase fileBaseUpload6, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU3 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload6 != null && fileBaseUpload6.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload6.FileName;
                    Extension = Path.GetExtension(fileBaseUpload6.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload6.FileName);
                    fileBaseUpload6.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload6.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU3.tempUpload6 = sysFileName.ToString();
                    tenantModelU3.UploadOriginalFileName6 = fileName;
                }

            }
            return tenantModelU3;
        }
        public TenantOnlineModel SaveTaxUpload7(HttpPostedFileBase fileBaseUpload7, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU3 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload7 != null && fileBaseUpload7.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload7.FileName;
                    Extension = Path.GetExtension(fileBaseUpload7.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload7.FileName);
                    fileBaseUpload7.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload7.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU3.tempUpload7 = sysFileName.ToString();
                    tenantModelU3.UploadOriginalFileName7 = fileName;
                }

            }
            return tenantModelU3;
        }
        public TenantOnlineModel SaveTaxUpload8(HttpPostedFileBase fileBaseUpload8, TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel tenantModelU3 = new TenantOnlineModel();
            if (model.ProspectID != 0)
            {
                string filePath = "";
                string fileName = "";
                string sysFileName = "";
                string Extension = "";

                if (fileBaseUpload8 != null && fileBaseUpload8.ContentLength > 0)
                {
                    filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/");
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    FileInfo _FileInfo = new FileInfo(filePath);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    fileName = fileBaseUpload8.FileName;
                    Extension = Path.GetExtension(fileBaseUpload8.FileName);
                    sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload8.FileName);
                    fileBaseUpload8.SaveAs(filePath + "//" + sysFileName);
                    if (!string.IsNullOrWhiteSpace(fileBaseUpload8.FileName))
                    {
                        string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/PersonalInformation/") + "/" + sysFileName;

                    }
                    tenantModelU3.tempUpload8 = sysFileName.ToString();
                    tenantModelU3.UploadOriginalFileName8 = fileName;
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
        public string GetEncDecSSNPassportIDNum(string EncDecVal, int EncDec)
        {
            string result = "";
            if (EncDec == 1)
            {
                result = !string.IsNullOrWhiteSpace(EncDecVal) ? new EncryptDecrypt().DecryptText(EncDecVal) : "";

            }
            else
            {
                result = !string.IsNullOrWhiteSpace(EncDecVal) ? new EncryptDecrypt().EncryptText(EncDecVal) : "";
            }
            return result;
        }
        public TenantOnlineModel GetTenantOnlineListGenerateQuotation(int id, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel lstpr = new TenantOnlineModel();
            try
            {
                long toid = UserId;
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
                    //Sachin Mahore 21 Apr 2020
                    DbParameter paramfF = cmd.CreateParameter();
                    paramfF.ParameterName = "toid";
                    paramfF.Value = toid;
                    cmd.Parameters.Add(paramfF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                lstpr.ID = 0;
                lstpr.ProspectID = 0;
                lstpr.IsInternational = 0;
                lstpr.Gender = 0;
                lstpr.IDType = 0;
                lstpr.State = 0;
                lstpr.Country = "1";
                lstpr.StateHome = 0;
                lstpr.RentOwn = 0;

                lstpr.Country2 = "0";
                lstpr.StateHome2 = 0;
                lstpr.RentOwn2 = 0;

                lstpr.JobType = 0;
                lstpr.OfficeCountry = "1";
                lstpr.OfficeState = 0;

                lstpr.EmergencyCountry = "1";
                lstpr.EmergencyStateHome = 0;
                lstpr.StepCompleted = 1;
                foreach (DataRow dr in dtTable.Rows)
                {
                    DateTime? dateOfBirth = null;
                    try { dateOfBirth = Convert.ToDateTime(dr["DateOfBirth"].ToString()); }
                    catch { }
                    DateTime? dateIssuance = null;
                    try { dateIssuance = Convert.ToDateTime(dr["DateIssuance"].ToString()); }
                    catch { }
                    DateTime? dateExpire = null;
                    try { dateExpire = Convert.ToDateTime(dr["DateExpire"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom = null;
                    try { moveInDateFrom = Convert.ToDateTime(dr["MoveInDateFrom"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo = null;
                    try { moveInDateTo = Convert.ToDateTime(dr["MoveInDateTo"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom2 = null;
                    try { moveInDateFrom2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo2 = null;
                    try { moveInDateTo2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? startDate = null;
                    try { startDate = Convert.ToDateTime(dr["StartDate"].ToString()); }
                    catch { }
                    lstpr.ID = Convert.ToInt32(dr["ID"].ToString());
                    lstpr.ProspectID = Convert.ToInt32(dr["ProspectID"].ToString());
                    lstpr.IsInternational = Convert.ToInt32(dr["IsInternational"].ToString());
                    lstpr.IsAdditionalRHistory = Convert.ToInt32(dr["IsAdditionalRHistory"].ToString());
                    lstpr.FirstName = dr["FirstName"].ToString();
                    lstpr.MiddleInitial = dr["MiddleInitial"].ToString();
                    lstpr.LastName = dr["LastName"].ToString();
                    lstpr.DateOfBirthTxt = dateOfBirth == null ? "" : dateOfBirth.Value.ToString("MM/dd/yyy");
                    lstpr.Gender = Convert.ToInt32(dr["Gender"].ToString());
                    lstpr.Email = dr["Email"].ToString();
                    lstpr.Mobile = dr["Mobile"].ToString();
                    DateTime? dt = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    lstpr.CreatedDateString = dt != null ? dt.Value.ToString("MM/dd/yyyy") : "";
                    lstpr.ExpireDate = Convert.ToDateTime(dt).AddHours(48).ToString("MM/dd/yyyy") + " 23:59:59";
                    if (!string.IsNullOrWhiteSpace(dr["PassportNumber"].ToString()))
                    {
                        string decryptedPassportNumber = new EncryptDecrypt().DecryptText(dr["PassportNumber"].ToString());
                        int passportlength = decryptedPassportNumber.Length > 4 ? decryptedPassportNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < passportlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedPassportNumber.Length > 4)
                        {
                            lstpr.PassportNumber = maskidnumber + decryptedPassportNumber.Substring(decryptedPassportNumber.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.PassportNumber = decryptedPassportNumber;
                        }
                    }
                    else
                    {
                        lstpr.PassportNumber = "";
                    }

                    lstpr.CountryIssuance = dr["CountryIssuance"].ToString();
                    lstpr.DateIssuanceTxt = dateIssuance == null ? "" : dateIssuance.Value.ToString("MM/dd/yyy");
                    lstpr.DateExpireTxt = dateExpire == null ? "" : dateExpire.Value.ToString("MM/dd/yyy");
                    lstpr.IDType = Convert.ToInt32(dr["IDType"].ToString());
                    lstpr.State = Convert.ToInt64(dr["State"].ToString());
                    if (!string.IsNullOrWhiteSpace(dr["IDNumber"].ToString()))
                    {
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(dr["IDNumber"].ToString());
                        int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < idnumlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedIDNumber.Length > 4)
                        {
                            lstpr.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.IDNumber = decryptedIDNumber;
                        }
                    }
                    else
                    {
                        lstpr.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(dr["SSN"].ToString()))
                    {
                        string decryptedSSN = new EncryptDecrypt().DecryptText(dr["SSN"].ToString());
                        if (decryptedSSN.Length > 5)
                        {
                            lstpr.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.SSN = decryptedSSN;
                        }
                    }
                    else
                    {
                        lstpr.SSN = "";
                    }
                    lstpr.Country = dr["Country"].ToString();
                    lstpr.HomeAddress1 = dr["HomeAddress1"].ToString();
                    lstpr.HomeAddress2 = dr["HomeAddress2"].ToString();
                    lstpr.StateHome = Convert.ToInt64(dr["StateHome"].ToString());
                    lstpr.CityHome = dr["CityHome"].ToString();
                    lstpr.ZipHome = dr["ZipHome"].ToString();

                    lstpr.OtherGender = dr["OtherGender"].ToString();


                    int countryOrigintemp = lstpr.CountryOfOrigin.HasValue ? Convert.ToInt32(lstpr.CountryOfOrigin) : 0;
                    var countryOriginVar = db.tbl_Country.Where(co => co.ID == countryOrigintemp).FirstOrDefault();
                    lstpr.CountryOfOriginString = countryOriginVar != null ? countryOriginVar.CountryName : "";



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


                    var stepCompleted = Convert.ToInt32(dr["StepCompleted"].ToString());
                    lstpr.StepCompleted = stepCompleted;
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
        public string CheckApplicationStatus(long TenantID)
        {
            string result = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var applicantData = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
            var numOfPets = db.tbl_TenantPetPlace.Where(p => p.TenantID == TenantID).FirstOrDefault();
            var petData = db.tbl_TenantPet.Where(p => p.TenantID == TenantID).Count();

            var allPmtReceived = 1;
            var allPetAdded = 1;
            long petsNumber = 0;

            foreach(var ad in applicantData)
            {
                if((ad.CreditPaid??0)==0 || (ad.BackGroundPaid ?? 0) == 0)
                {
                    allPmtReceived = 0;
                }
            }

            if (numOfPets != null)
            {
                petsNumber = (numOfPets.PetPlaceID ?? 0);
            }


            if (petsNumber != petData && petsNumber > 0)
            {
                allPetAdded = 0;
            }

            if(allPmtReceived!=1)
            {
                result += "Full Application Payment Is Not Received<br/>";
            }

            if (allPetAdded != 1)
            {
                result += "All Pet Information Is Not Added<br/>";
            }

            if(result!="")
            {
                result += "<br/>Please Complete All Requirement To Submit Complete Application<br/>";
            }
            return result;
        }
        public TenantOnlineModel GetTenantOnlineListProspectVerification(int id, long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel lstpr = new TenantOnlineModel();
            try
            {
                long toid = TenantID;
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
                    //Sachin Mahore 21 Apr 2020
                    DbParameter paramfF = cmd.CreateParameter();
                    paramfF.ParameterName = "toid";
                    paramfF.Value = toid;
                    cmd.Parameters.Add(paramfF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                lstpr.ID = 0;
                lstpr.ProspectID = 0;
                lstpr.IsInternational = 0;
                lstpr.Gender = 0;
                lstpr.IDType = 0;
                lstpr.State = 0;
                lstpr.Country = "1";
                lstpr.StateHome = 0;
                lstpr.RentOwn = 0;

                lstpr.Country2 = "0";
                lstpr.StateHome2 = 0;
                lstpr.RentOwn2 = 0;

                lstpr.JobType = 0;
                lstpr.OfficeCountry = "1";
                lstpr.OfficeState = 0;

                lstpr.EmergencyCountry = "1";
                lstpr.EmergencyStateHome = 0;
                lstpr.StepCompleted = 1;
                foreach (DataRow dr in dtTable.Rows)
                {
                    DateTime? dateOfBirth = null;
                    try { dateOfBirth = Convert.ToDateTime(dr["DateOfBirth"].ToString()); }
                    catch { }
                    DateTime? dateIssuance = null;
                    try { dateIssuance = Convert.ToDateTime(dr["DateIssuance"].ToString()); }
                    catch { }
                    DateTime? dateExpire = null;
                    try { dateExpire = Convert.ToDateTime(dr["DateExpire"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom = null;
                    try { moveInDateFrom = Convert.ToDateTime(dr["MoveInDateFrom"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo = null;
                    try { moveInDateTo = Convert.ToDateTime(dr["MoveInDateTo"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom2 = null;
                    try { moveInDateFrom2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo2 = null;
                    try { moveInDateTo2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? startDate = null;
                    try { startDate = Convert.ToDateTime(dr["StartDate"].ToString()); }
                    catch { }
                    lstpr.ID = Convert.ToInt32(dr["ID"].ToString());
                    lstpr.ProspectID = Convert.ToInt32(dr["ProspectID"].ToString());
                    lstpr.IsInternational = Convert.ToInt32(dr["IsInternational"].ToString());
                    lstpr.IsAdditionalRHistory = Convert.ToInt32(dr["IsAdditionalRHistory"].ToString());
                    lstpr.FirstName = !string.IsNullOrWhiteSpace(dr["FirstName"].ToString()) ? dr["FirstName"].ToString() : "";
                    lstpr.MiddleInitial = !string.IsNullOrWhiteSpace(dr["MiddleInitial"].ToString()) ? dr["MiddleInitial"].ToString() : "";
                    lstpr.LastName = !string.IsNullOrWhiteSpace(dr["LastName"].ToString()) ? dr["LastName"].ToString() : "";
                    lstpr.DateOfBirthTxt = dateOfBirth == null ? "" : dateOfBirth.Value.ToString("MM/dd/yyy");
                    lstpr.Gender = Convert.ToInt32(dr["Gender"].ToString());
                    lstpr.Email = !string.IsNullOrWhiteSpace(dr["Email"].ToString()) ? dr["Email"].ToString() : "";
                    lstpr.Mobile = !string.IsNullOrWhiteSpace(dr["Mobile"].ToString()) ? dr["Mobile"].ToString() : "";

                    if (!string.IsNullOrWhiteSpace(dr["PassportNumber"].ToString()))
                    {
                        string decryptedPassportNumber = new EncryptDecrypt().DecryptText(dr["PassportNumber"].ToString());
                        int passportlength = decryptedPassportNumber.Length > 4 ? decryptedPassportNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < passportlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedPassportNumber.Length > 4)
                        {
                            lstpr.PassportNumber = maskidnumber + decryptedPassportNumber.Substring(decryptedPassportNumber.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.PassportNumber = decryptedPassportNumber;
                        }
                    }
                    else
                    {
                        lstpr.PassportNumber = "";
                    }

                    lstpr.CountryIssuance = !string.IsNullOrWhiteSpace(dr["CountryIssuance"].ToString()) ? dr["CountryIssuance"].ToString() : "";
                    lstpr.DateIssuanceTxt = dateIssuance == null ? "" : dateIssuance.Value.ToString("MM/dd/yyy");
                    lstpr.DateExpireTxt = dateExpire == null ? "" : dateExpire.Value.ToString("MM/dd/yyy");
                    lstpr.IDType = Convert.ToInt32(dr["IDType"].ToString());
                    lstpr.State = Convert.ToInt64(dr["State"].ToString());
                    if (!string.IsNullOrWhiteSpace(dr["IDNumber"].ToString()))
                    {
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(dr["IDNumber"].ToString());
                        int idnumlength = decryptedIDNumber.Length > 4 ? decryptedIDNumber.Length - 4 : 0;
                        string maskidnumber = "";
                        for (int i = 0; i < idnumlength; i++)
                        {
                            maskidnumber += "*";
                        }
                        if (decryptedIDNumber.Length > 4)
                        {
                            lstpr.IDNumber = maskidnumber + decryptedIDNumber.Substring(decryptedIDNumber.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.IDNumber = decryptedIDNumber;
                        }
                    }
                    else
                    {
                        lstpr.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(dr["SSN"].ToString()))
                    {
                        string decryptedSSN = new EncryptDecrypt().DecryptText(dr["SSN"].ToString());
                        if (decryptedSSN.Length > 5)
                        {
                            lstpr.SSN = "***-**-" + decryptedSSN.Substring(decryptedSSN.Length - 4, 4);
                        }
                        else
                        {
                            lstpr.SSN = decryptedSSN;
                        }
                    }
                    else
                    {
                        lstpr.SSN = "";
                    }
                    lstpr.Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "";
                    lstpr.HomeAddress1 = !string.IsNullOrWhiteSpace(dr["HomeAddress1"].ToString()) ? dr["HomeAddress1"].ToString() : "";
                    lstpr.HomeAddress2 = !string.IsNullOrWhiteSpace(dr["HomeAddress2"].ToString()) ? dr["HomeAddress2"].ToString() : "";
                    lstpr.StateHome = Convert.ToInt64(dr["StateHome"].ToString());
                    lstpr.CityHome = !string.IsNullOrWhiteSpace(dr["CityHome"].ToString()) ? dr["CityHome"].ToString() : "";
                    lstpr.ZipHome = !string.IsNullOrWhiteSpace(dr["ZipHome"].ToString()) ? dr["ZipHome"].ToString() : "";
                    lstpr.RentOwn = Convert.ToInt16(dr["RentOwn"].ToString());
                    lstpr.MoveInDateFromTxt = moveInDateFrom == null ? "" : moveInDateFrom.Value.ToString("MM/dd/yyy");
                    lstpr.MoveInDateToTxt = moveInDateTo == null ? "" : moveInDateTo.Value.ToString("MM/dd/yyy");
                    lstpr.MonthlyPayment = !string.IsNullOrWhiteSpace(dr["MonthlyPayment"].ToString()) ? dr["MonthlyPayment"].ToString() : "";
                    lstpr.Reason = !string.IsNullOrWhiteSpace(dr["Reason"].ToString()) ? dr["Reason"].ToString() : "";
                    lstpr.EmployerName = !string.IsNullOrWhiteSpace(dr["EmployerName"].ToString()) ? dr["EmployerName"].ToString() : "";
                    lstpr.JobTitle = !string.IsNullOrWhiteSpace(dr["JobTitle"].ToString()) ? dr["JobTitle"].ToString() : "";
                    lstpr.JobType = Convert.ToInt32(dr["JobType"].ToString());
                    lstpr.StartDateTxt = startDate == null ? "" : startDate.Value.ToString("MM/dd/yyy");
                    lstpr.Income = Convert.ToDecimal(dr["Income"].ToString());
                    lstpr.AdditionalIncome = Convert.ToDecimal(dr["AdditionalIncome"].ToString());
                    lstpr.SupervisorName = !string.IsNullOrWhiteSpace(dr["SupervisorName"].ToString()) ? dr["SupervisorName"].ToString() : "";
                    lstpr.SupervisorPhone = !string.IsNullOrWhiteSpace(dr["SupervisorPhone"].ToString()) ? dr["SupervisorPhone"].ToString() : "";
                    lstpr.SupervisorEmail = !string.IsNullOrWhiteSpace(dr["SupervisorEmail"].ToString()) ? dr["SupervisorEmail"].ToString() : "";
                    lstpr.OfficeCountry = !string.IsNullOrWhiteSpace(dr["OfficeCountry"].ToString()) ? dr["OfficeCountry"].ToString() : "";
                    lstpr.OfficeAddress1 = !string.IsNullOrWhiteSpace(dr["OfficeAddress1"].ToString()) ? dr["OfficeAddress1"].ToString() : "";
                    lstpr.OfficeAddress2 = !string.IsNullOrWhiteSpace(dr["OfficeAddress2"].ToString()) ? dr["OfficeAddress2"].ToString() : "";
                    lstpr.OfficeState = Convert.ToInt32(dr["OfficeState"].ToString());
                    lstpr.OfficeCity = !string.IsNullOrWhiteSpace(dr["OfficeCity"].ToString()) ? dr["OfficeCity"].ToString() : "";
                    lstpr.OfficeZip = !string.IsNullOrWhiteSpace(dr["OfficeZip"].ToString()) ? dr["OfficeZip"].ToString() : "";
                    lstpr.Relationship = !string.IsNullOrWhiteSpace(dr["Relationship"].ToString()) ? dr["Relationship"].ToString() : "";
                    lstpr.EmergencyFirstName = !string.IsNullOrWhiteSpace(dr["EmergencyFirstName"].ToString()) ? dr["EmergencyFirstName"].ToString() : "";
                    lstpr.EmergencyLastName = !string.IsNullOrWhiteSpace(dr["EmergencyLastName"].ToString()) ? dr["EmergencyLastName"].ToString() : "";
                    lstpr.EmergencyMobile = !string.IsNullOrWhiteSpace(dr["EmergencyMobile"].ToString()) ? dr["EmergencyMobile"].ToString() : "";
                    lstpr.EmergencyHomePhone = !string.IsNullOrWhiteSpace(dr["EmergencyHomePhone"].ToString()) ? dr["EmergencyHomePhone"].ToString() : "";
                    lstpr.EmergencyWorkPhone = !string.IsNullOrWhiteSpace(dr["EmergencyWorkPhone"].ToString()) ? dr["EmergencyWorkPhone"].ToString() : "";
                    lstpr.EmergencyEmail = !string.IsNullOrWhiteSpace(dr["EmergencyEmail"].ToString()) ? dr["EmergencyEmail"].ToString() : "";
                    lstpr.EmergencyCountry = !string.IsNullOrWhiteSpace(dr["EmergencyCountry"].ToString()) ? dr["EmergencyCountry"].ToString() : "";
                    lstpr.EmergencyAddress1 = !string.IsNullOrWhiteSpace(dr["EmergencyAddress1"].ToString()) ? dr["EmergencyAddress1"].ToString() : "";
                    lstpr.EmergencyAddress2 = !string.IsNullOrWhiteSpace(dr["EmergencyAddress2"].ToString()) ? dr["EmergencyAddress2"].ToString() : "";
                    lstpr.EmergencyStateHome = Convert.ToInt32(dr["EmergencyStateHome"].ToString());
                    lstpr.EmergencyCityHome = !string.IsNullOrWhiteSpace(dr["EmergencyCityHome"].ToString()) ? dr["EmergencyCityHome"].ToString() : "";
                    lstpr.EmergencyZipHome = !string.IsNullOrWhiteSpace(dr["EmergencyZipHome"].ToString()) ? dr["EmergencyZipHome"].ToString() : "";
                    lstpr.OtherGender = !string.IsNullOrWhiteSpace(dr["OtherGender"].ToString()) ? dr["OtherGender"].ToString() : "";

                    lstpr.IdentityDocument = !string.IsNullOrWhiteSpace(dr["IdentityDocument"].ToString()) ? dr["IdentityDocument"].ToString() : "";
                    lstpr.PassportDocument = !string.IsNullOrWhiteSpace(dr["PassportDocument"].ToString()) ? dr["PassportDocument"].ToString() : "";
                    lstpr.TaxReturn = !string.IsNullOrWhiteSpace(dr["TaxReturn"].ToString()) ? dr["TaxReturn"].ToString() : "";

                    lstpr.TaxReturn2 = !string.IsNullOrWhiteSpace(dr["TaxReturn2"].ToString()) ? dr["TaxReturn2"].ToString() : "";
                    lstpr.TaxReturn3 = !string.IsNullOrWhiteSpace(dr["TaxReturn3"].ToString()) ? dr["TaxReturn3"].ToString() : "";
                    lstpr.HaveVehicle = Convert.ToBoolean(dr["HaveVehicle"].ToString());
                    lstpr.HavePet = Convert.ToBoolean(dr["HavePet"].ToString());
                    lstpr.UploadOriginalFileName1 = !string.IsNullOrWhiteSpace(dr["TaxReturnOrginalFile"].ToString()) ? dr["TaxReturnOrginalFile"].ToString() : "";
                    lstpr.UploadOriginalFileName2 = !string.IsNullOrWhiteSpace(dr["TaxReturnOrginalFile2"].ToString()) ? dr["TaxReturnOrginalFile2"].ToString() : "";
                    lstpr.UploadOriginalFileName3 = !string.IsNullOrWhiteSpace(dr["TaxReturnOrginalFile3"].ToString()) ? dr["TaxReturnOrginalFile3"].ToString() : "";
                    lstpr.UploadOriginalPassportName = !string.IsNullOrWhiteSpace(dr["PassportDocumentOriginalFile"].ToString()) ? dr["PassportDocumentOriginalFile"].ToString() : "";
                    lstpr.UploadOriginalIdentityName = !string.IsNullOrWhiteSpace(dr["IdentityDocumentOriginalFile"].ToString()) ? dr["IdentityDocumentOriginalFile"].ToString() : "";
                    lstpr.IsPaystub = Convert.ToInt32(dr["IsPaystub"].ToString());

                    lstpr.CountryOfOrigin = Convert.ToInt64(dr["CountryOfOrigin"].ToString());
                    lstpr.Evicted = Convert.ToInt32(dr["Evicted"].ToString());
                    lstpr.EvictedDetails = !string.IsNullOrWhiteSpace(dr["EvictedDetails"].ToString()) ? dr["EvictedDetails"].ToString() : "";
                    lstpr.ConvictedFelony = Convert.ToInt32(dr["ConvictedFelony"].ToString());
                    lstpr.ConvictedFelonyDetails = !string.IsNullOrWhiteSpace(dr["ConvictedFelonyDetails"].ToString()) ? dr["ConvictedFelonyDetails"].ToString() : "";
                    lstpr.CriminalChargPen = Convert.ToInt32(dr["CriminalChargPen"].ToString());
                    lstpr.CriminalChargPenDetails = !string.IsNullOrWhiteSpace(dr["CriminalChargPenDetails"].ToString()) ? dr["CriminalChargPenDetails"].ToString() : "";
                    lstpr.DoYouSmoke = Convert.ToInt32(dr["DoYouSmoke"].ToString());
                    lstpr.ReferredResident = Convert.ToInt32(dr["ReferredResident"].ToString());
                    lstpr.ReferredResidentName = !string.IsNullOrWhiteSpace(dr["ReferredResidentName"].ToString()) ? dr["ReferredResidentName"].ToString() : "";
                    lstpr.ReferredBrokerMerchant = Convert.ToInt32(dr["ReferredBrokerMerchant"].ToString());
                    lstpr.ApartmentCommunity = !string.IsNullOrWhiteSpace(dr["ApartmentCommunity"].ToString()) ? dr["ApartmentCommunity"].ToString() : "";
                    lstpr.ManagementCompany = !string.IsNullOrWhiteSpace(dr["ManagementCompany"].ToString()) ? dr["ManagementCompany"].ToString() : "";
                    lstpr.ManagementCompanyPhone = !string.IsNullOrWhiteSpace(dr["ManagementCompanyPhone"].ToString()) ? dr["ManagementCompanyPhone"].ToString() : "";
                    lstpr.IsProprNoticeLeaseAgreement = Convert.ToInt32(dr["IsProprNoticeLeaseAgreement"].ToString());

                    int countryOrigintemp = lstpr.CountryOfOrigin.HasValue ? Convert.ToInt32(lstpr.CountryOfOrigin) : 0;
                    var countryOriginVar = db.tbl_Country.Where(co => co.ID == countryOrigintemp).FirstOrDefault();
                    lstpr.CountryOfOriginString = countryOriginVar != null ? countryOriginVar.CountryName : "";

                    //var PersonalStateVar = db.tbl_State.Where(co => co.ID == lstpr.State).FirstOrDefault();
                    //lstpr.StatePersonalString = PersonalStateVar != null ? PersonalStateVar.StateName : "";
                    //var StateHomeVar = db.tbl_State.Where(co => co.ID == lstpr.StateHome).FirstOrDefault();
                    //lstpr.StateHomeString = StateHomeVar != null ? StateHomeVar.StateName : "";
                    //int contryTemp = lstpr.Country != "" ? Convert.ToInt32(lstpr.Country) : 0;
                    //var CountryVar = db.tbl_Country.Where(co => co.ID == contryTemp).FirstOrDefault();
                    //lstpr.CountryString = CountryVar != null ? CountryVar.CountryName : "";
                    //int officeContryTemp = lstpr.OfficeCountry != "" ? Convert.ToInt32(lstpr.OfficeCountry) : 0;
                    //var OfficeCountryVar = db.tbl_Country.Where(co => co.ID == officeContryTemp).FirstOrDefault();
                    //lstpr.OfficeCountryString = OfficeCountryVar != null ? OfficeCountryVar.CountryName : "";
                    //int emergencyContryTemp = lstpr.EmergencyCountry != "" ? Convert.ToInt32(lstpr.EmergencyCountry) : 0;
                    //var EmergencyCountryVar = db.tbl_Country.Where(co => co.ID == emergencyContryTemp).FirstOrDefault();
                    //lstpr.EmergencyCountryString = EmergencyCountryVar != null ? EmergencyCountryVar.CountryName : "";
                    //int emergencyStateHomeTemp = lstpr.EmergencyStateHome != null ? Convert.ToInt32(lstpr.EmergencyStateHome) : 0;
                    //var EmergencyStateHomeVar = db.tbl_State.Where(co => co.ID == emergencyStateHomeTemp).FirstOrDefault();
                    //lstpr.EmergencyStateHomeString = EmergencyStateHomeVar != null ? EmergencyStateHomeVar.StateName : "";

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

                    lstpr.StringEvicted = Convert.ToInt32(dr["Evicted"]) == 1 ? "No" : "Yes";
                    lstpr.StringConvictedFelony = Convert.ToInt32(dr["ConvictedFelony"]) == 1 ? "No" : "Yes";
                    lstpr.StringCriminalChargPen = Convert.ToInt32(dr["CriminalChargPen"]) == 1 ? "No" : "Yes";
                    lstpr.StringDoYouSmoke = Convert.ToInt32(dr["DoYouSmoke"]) == 1 ? "No" : "Yes";
                    lstpr.StringReferredResident = Convert.ToInt32(dr["ReferredResident"]) == 1 ? "No" : "Yes";
                    lstpr.StringReferredBrokerMerchant = Convert.ToInt32(dr["ReferredBrokerMerchant"]) == 1 ? "No" : "Yes";
                    lstpr.stringIsProprNoticeLeaseAgreement = Convert.ToInt32(dr["IsProprNoticeLeaseAgreement"]) == 1 ? "Yes" : "No";

                    lstpr.TaxReturn4 = dr["TaxReturn4"].ToString();
                    lstpr.TaxReturn5 = dr["TaxReturn5"].ToString();
                    lstpr.TaxReturn6 = dr["TaxReturn6"].ToString();
                    lstpr.TaxReturn7 = dr["TaxReturn7"].ToString();
                    lstpr.TaxReturn8 = dr["TaxReturn8"].ToString();
                    lstpr.UploadOriginalFileName4 = dr["TaxReturnOrginalFile4"].ToString();
                    lstpr.UploadOriginalFileName5 = dr["TaxReturnOrginalFile5"].ToString();
                    lstpr.UploadOriginalFileName6 = dr["TaxReturnOrginalFile6"].ToString();
                    lstpr.UploadOriginalFileName7 = dr["TaxReturnOrginalFile7"].ToString();
                    lstpr.UploadOriginalFileName8 = dr["TaxReturnOrginalFile8"].ToString();
                    lstpr.IsFedralTax = Convert.ToInt32(dr["IsFedralTax"].ToString());
                    lstpr.IsBankState = Convert.ToInt32(dr["IsBankState"].ToString());

                    var stepCompleted = Convert.ToInt32(dr["StepCompleted"].ToString());
                    lstpr.StepCompleted = stepCompleted;
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

        public List<TenantOnlineModel> GetAppResidenceHistory(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel model = new TenantOnlineModel();
            List<TenantOnlineModel> lstpr = new List<TenantOnlineModel>();
            var coapplist = db.tbl_TenantOnline.Where(p => p.ProspectID == id).ToList();
            if (coapplist != null)
            {
                foreach (var cl in coapplist)
                {
                    TenantOnlineModel ahm = new TenantOnlineModel();
                    List<ApplicantHistoryModel> lstahm = new List<ApplicantHistoryModel>();
                    DateTime? stdtFrom = null;
                    try
                    {

                        stdtFrom = Convert.ToDateTime(cl.MoveInDateFrom);
                    }
                    catch
                    {

                    }
                    ahm.ID = cl.ID;
                    var apptype = db.tbl_Applicant.Where(c => c.UserID == cl.ParentTOID).FirstOrDefault();
                    ahm.AppType = apptype.Type;
                    ahm.FirstName = cl.FirstName + " " + cl.LastName;
                    ahm.Country = cl.Country;
                    ahm.StateHome = cl.StateHome;
                    ahm.CityHome = cl.CityHome;
                    ahm.ZipHome = cl.ZipHome;
                    ahm.RentOwn = cl.RentOwn;
                    ahm.HomeAddress1 = cl.HomeAddress1;
                    ahm.HomeAddress2 = cl.HomeAddress2;
                    ahm.MoveInDateFromTxt = stdtFrom == null ? "" : stdtFrom.Value.ToString("MM/dd/yyy");
                    ahm.MonthlyPayment = !string.IsNullOrWhiteSpace(cl.MonthlyPayment) ? cl.MonthlyPayment : "";
                    ahm.Reason = !string.IsNullOrWhiteSpace(cl.Reason) ? cl.Reason : "";
                    var stateStr = db.tbl_State.Where(co => co.ID == cl.StateHome).FirstOrDefault();
                    ahm.StateString = stateStr.StateName;
                    int ctryString = Convert.ToInt32(ahm.Country);
                    var countryStr = db.tbl_Country.Where(co => co.ID == ctryString).FirstOrDefault();
                    ahm.CountryString = countryStr.CountryName;
                    ahm.RentOwnString = ahm.RentOwn == 1 ? "Rent" : ahm.RentOwn == 2 ? "Own" : "";
                    ahm.ApartmentCommunity = !string.IsNullOrWhiteSpace(cl.ApartmentCommunity) ? cl.ApartmentCommunity : "";
                    ahm.ManagementCompany = !string.IsNullOrWhiteSpace(cl.ManagementCompany) ? cl.ManagementCompany : "";
                    ahm.ManagementCompanyPhone = !string.IsNullOrWhiteSpace(cl.ManagementCompanyPhone) ? cl.ManagementCompanyPhone : "";
                    ahm.IsProprNoticeLeaseAgreement = cl.IsProprNoticeLeaseAgreement;
                    ahm.stringIsProprNoticeLeaseAgreement = ahm.IsProprNoticeLeaseAgreement == 1 ? "Yes" : "No";
                    ahm.ResidenceStatus = cl.ResidenceStatus==null?0: cl.ResidenceStatus;
                    ahm.ResidenceNotes = cl.ResidenceNotes==null?"":cl.ResidenceNotes;
                    
                    lstpr.Add(ahm);
                }

            }

            return lstpr;

        }
        public List<TenantOnlineModel> GetAppEmpHistory(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantOnlineModel model = new TenantOnlineModel();
            List<TenantOnlineModel> lstpr = new List<TenantOnlineModel>();
            var coapplist = db.tbl_TenantOnline.Where(p => p.ProspectID == id).ToList();
            if (coapplist != null)
            {
                foreach (var cl in coapplist)
                {
                    TenantOnlineModel ahm = new TenantOnlineModel();
                    List<ApplicantHistoryModel> lstahm = new List<ApplicantHistoryModel>();
                    DateTime? stdtFrom = null;
                    try
                    {

                        stdtFrom = Convert.ToDateTime(cl.StartDate);
                    }
                    catch
                    {

                    }
                    ahm.ID = cl.ID;
                    var apptype = db.tbl_Applicant.Where(c => c.UserID == cl.ParentTOID).FirstOrDefault();
                    ahm.AppType = apptype.Type;
                    ahm.FirstName = cl.FirstName + " " + cl.LastName;
                 
                    ahm.EmployerName = !string.IsNullOrWhiteSpace(cl.EmployerName) ? cl.EmployerName : "";
                    ahm.JobTitle = !string.IsNullOrWhiteSpace(cl.JobTitle) ? cl.JobTitle : "";
                    ahm.JobType = cl.JobType;
                    ahm.MoveInDateFromTxt = stdtFrom == null ? "" : stdtFrom.Value.ToString("MM/dd/yyy");

                    ahm.Income = cl.Income;
                    ahm.AdditionalIncome = cl.AdditionalIncome;
                    ahm.SupervisorName = !string.IsNullOrWhiteSpace(cl.SupervisorName) ? cl.SupervisorName : "";
                    ahm.SupervisorPhone = cl.SupervisorPhone;
                    ahm.SupervisorEmail = !string.IsNullOrWhiteSpace(cl.SupervisorEmail) ? cl.SupervisorEmail : "";
                    ahm.Country = cl.Country;
                    ahm.OfficeAddress1 = !string.IsNullOrWhiteSpace(cl.OfficeAddress1) ? cl.OfficeAddress1 : "";
                    ahm.OfficeAddress2 = !string.IsNullOrWhiteSpace(cl.OfficeAddress2) ? cl.OfficeAddress2 : "";
                    ahm.State = cl.State;
                    ahm.OfficeCity = !string.IsNullOrWhiteSpace(cl.OfficeCity) ? cl.OfficeCity : "";
                    ahm.OfficeZip = !string.IsNullOrWhiteSpace(cl.OfficeZip) ? cl.OfficeZip : "";
                    ahm.StartDateTxt = cl.StartDate != null ? cl.StartDate.Value.ToString("MM/dd/yyyy") : "";

                    long cid = Convert.ToInt64(cl.OfficeCountry);
                    var countryName = db.tbl_Country.Where(co => co.ID ==cid).FirstOrDefault();
                    var StateName = db.tbl_State.Where(co => co.ID == cl.OfficeState).FirstOrDefault();

                    ahm.CountryString = countryName != null ? countryName.CountryName : "";
                    ahm.StateHomeString = StateName != null ? StateName.StateName : "";
                
                    ahm.EmpStatus = cl.EmpStatus == null ? 0 : cl.EmpStatus;
                    ahm.EmpNotes = cl.EmpNotes==null ? "" : cl.EmpNotes;
                    lstpr.Add(ahm);
                }

            }

            return lstpr;

        }

        //Sachin Mahore 13 may 2020
        public string UpdateResStatus(long ID, int ResidenceStatus, string ResidenceNotes)
        {

            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (ID != 0)
            {

                var getdata = db.tbl_TenantOnline.Where(p => p.ID == ID).FirstOrDefault();
                if (getdata != null)
                {
                    getdata.ResidenceStatus = ResidenceStatus;
                    getdata.ResidenceNotes = ResidenceNotes;
                }
                db.SaveChanges();
                if (ResidenceStatus == 2)
                {
                    var log = db.tbl_Login.Where(p => p.UserID == getdata.ParentTOID).FirstOrDefault();
                    log.IsActive = 0;
                    db.SaveChanges();
                }
                else
                {
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect3.html");
                    string message = "";
                    var applist = db.tbl_TenantOnline.Where(p => p.ProspectID == getdata.ProspectID).ToList();
                    var prospdata = db.tbl_ApplyNow.Where(v => v.ID == getdata.ProspectID && (v.Status == "Approved" || v.Status == "Conditional")).FirstOrDefault();
                    int approveCount = 0;
                    if (prospdata != null)
                    {
                        foreach (var aapl in applist)
                        {
                            var getStatus = db.tbl_TenantOnline.Where(p => p.ID == aapl.ID && (p.ResidenceStatus == 1 || p.ResidenceStatus == 3) && (p.EmpStatus == 1 || p.EmpStatus == 3)).FirstOrDefault();
                            if (getStatus != null)
                            {
                                approveCount += 1;
                            }
                        }

                        if (approveCount == applist.Count)
                        {

                            var emaildata = db.tbl_Applicant.Where(c => c.TenantID == getdata.ProspectID && c.Type == "Primary Applicant").FirstOrDefault();

                            reportHTML = reportHTML.Replace("[%EmailHeader%]", "Your Application is Approved. Pay Administration Fees");
                            reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + emaildata.FirstName + " " + emaildata.LastName + "</b>,<br/>Your Online application submitted successfully. Please click below to Pay Application fees. <br/><br/><u><b>Payment Link :<a href=''></a> </br></b></u>  </br>");
                            reportHTML = reportHTML.Replace("[%TenantName%]", emaildata.FirstName + " " + emaildata.LastName);
                            var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();

                            string payid = new EncryptDecrypt().EncryptText(emaildata.ApplicantID.ToString() + ",3," + propertDet.AdminFees.Value.ToString("0.00"));
                            reportHTML = reportHTML.Replace("[%Status%]", "Congratulations ! Your Application is Approved and Pay your Administration Fees");
                            reportHTML = reportHTML.Replace("[%StatusDet%]", "Good news! You have been approved.We welcome you to our community.Your next step is to pay the Administration fee of $350.00 to ensure your unit is reserved until you move -in. Once you process your payment, you will be directed to prepare your lease.  ");
                            reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                            message = "Notification: Your Application is Approved and pay your Administration Fees. Please check the email for detail.";
                            string body = reportHTML;
                            new EmailSendModel().SendEmail(emaildata.Email, " Notification: Your Application is Approved and pay your Administration Fees.", body);
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(emaildata.Phone))
                                {
                                    new TwilioService().SMS(emaildata.Phone, message);
                                }
                            }
                        }
                    }
                }
                //if (ResidenceStatus==1)
                //{
                //    //var keysign = db.tbl_ESignatureKeys.Where(c => c.DateSigned == "" && c.ApplicantID == ApplicantID).ToList();
                //    //string payid = app.Key.ToString();
                //    //reportHTML = reportHTML.Replace("[%Status%]", "Residential status approved. Please Sign your application");
                //    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Residential status approved. Sign your application");
                //    //reportHTML = reportHTML.Replace("[%StatusDet%]", "Hi <b>" + getdata.FirstName + " " + getdata.LastName + "</b>,<br/>Your Online application is Approved. Please click below to sign the lease </u> ");
                //    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Review and Sign</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                //    //reportHTML = reportHTML.Replace("[%TenantName%]", getdata.FirstName + " " + getdata.LastName);

                //    reportHTML = reportHTML.Replace("[%Status%]", "Residential status approved.");
                //    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Residential status approved.");
                //    reportHTML = reportHTML.Replace("[%StatusDet%]", "Hi <b>" + getdata.FirstName + " " + getdata.LastName + "</b>,<br/>Your Online application is Approved.  <br/> Notes: "+ResidenceNotes);
                //    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Review and Sign</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                //    reportHTML = reportHTML.Replace("[%TenantName%]", getdata.FirstName + " " + getdata.LastName);

                //    string body = reportHTML;
                //    new EmailSendModel().SendEmail(getdata.Email, "Residential status Aprroved", body);
                //    if (SendMessage == "yes")
                //    {
                //        if (!string.IsNullOrWhiteSpace(getdata.Mobile))
                //        {
                //            new TwilioService().SMS(getdata.Mobile, "Residential status Aprroved. Please check email for details");
                //        }
                //    }
                //}else if (ResidenceStatus == 3)
                //{

                //    reportHTML = reportHTML.Replace("[%Status%]", "Residential status approved with Conditions.");
                //    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Residential status approved with Conditions.");
                //    reportHTML = reportHTML.Replace("[%StatusDet%]", "Hi <b>" + getdata.FirstName + " " + getdata.LastName + "</b>,<br/>Your Online application is Approved with Conditions. <br/> Notes: " + ResidenceNotes);
                //    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Review and Sign</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                //    reportHTML = reportHTML.Replace("[%TenantName%]", getdata.FirstName + " " + getdata.LastName);

                //    string body = reportHTML;
                //    new EmailSendModel().SendEmail(getdata.Email, "Residential status Conditionally approved", body);
                //    if (SendMessage == "yes")
                //    {
                //        if (!string.IsNullOrWhiteSpace(getdata.Mobile))
                //        {
                //            new TwilioService().SMS(getdata.Mobile, "Residential status Conditionally approved. Please check email for details");
                //        }
                //    }
                //}else if (ResidenceStatus == 2)
                //{

                //    reportHTML = reportHTML.Replace("[%Status%]", "Residential status Denied.");
                //    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Residential status Denied.");
                //    reportHTML = reportHTML.Replace("[%StatusDet%]", "Hi <b>" + getdata.FirstName + " " + getdata.LastName + "</b>,<br/>Your Online application is Denied. <br/> Notes: " + ResidenceNotes);
                //    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Review and Sign</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                //    reportHTML = reportHTML.Replace("[%TenantName%]", getdata.FirstName + " " + getdata.LastName);

                //    string body = reportHTML;
                //    new EmailSendModel().SendEmail(getdata.Email, "Residential status Denied", body);
                //    if (SendMessage == "yes")
                //    {
                //        if (!string.IsNullOrWhiteSpace(getdata.Mobile))
                //        {
                //            new TwilioService().SMS(getdata.Mobile, "Residential status Denied. Please check email for details");
                //        }
                //    }
                //}

                msg = "Residence Status Saved Successfully";
            }
            db.Dispose();
            return msg;
        }
        public string UpdateEmpStatus(long ID, int EmpStatus, string EmpNotes)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (ID !=  0)
            {

                var getdata = db.tbl_TenantOnline.Where(p => p.ID == ID).FirstOrDefault();
                if (getdata != null)
                {
                    getdata.EmpStatus = EmpStatus;
                    getdata.EmpNotes = EmpNotes;
                }
                db.SaveChanges();
                if(EmpStatus==2)
                {
                    var log = db.tbl_Login.Where(p => p.UserID == getdata.ParentTOID).FirstOrDefault();
                    log.IsActive = 0;
                    db.SaveChanges();
                }
                else
                {

                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect3.html");
                string message = "";
                var applist = db.tbl_TenantOnline.Where(p => p.ProspectID == getdata.ProspectID).ToList();
                var prospdata = db.tbl_ApplyNow.Where(v => v.ID == getdata.ProspectID && (v.Status == "Approved" || v.Status == "Conditional")).FirstOrDefault();
                int approveCount = 0;
                    if (prospdata != null)
                    {
                        foreach (var aapl in applist)
                        {
                            var getStatus = db.tbl_TenantOnline.Where(p => p.ID == aapl.ID && (p.ResidenceStatus == 1 || p.ResidenceStatus == 3) && (p.EmpStatus == 1 || p.EmpStatus == 3)).FirstOrDefault();
                            if (getStatus != null)
                            {
                                approveCount += 1;
                            }
                        }

                        if (approveCount == applist.Count)
                        {
                            var emaildata = db.tbl_Applicant.Where(c => c.TenantID == getdata.ProspectID && c.Type == "Primary Applicant").FirstOrDefault();

                            reportHTML = reportHTML.Replace("[%EmailHeader%]", "Congratulations ! Your Application is Approved and Pay your Administration Fees");
                            reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + emaildata.FirstName + " " + emaildata.LastName + "</b>,<br/>Your Online application submitted successfully. Please click below to Pay Application fees. <br/><br/><u><b>Payment Link :<a href=''></a> </br></b></u>  </br>");
                            reportHTML = reportHTML.Replace("[%TenantName%]", emaildata.FirstName + " " + emaildata.LastName);
                            var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();

                            string payid = new EncryptDecrypt().EncryptText(emaildata.ApplicantID.ToString() + ",3," + propertDet.AdminFees.Value.ToString("0.00"));
                            reportHTML = reportHTML.Replace("[%Status%]", "Congratulations ! Your Application is Approved and Pay your Administration Fees");
                            reportHTML = reportHTML.Replace("[%StatusDet%]", "Good news!You have been approved.We welcome you to our community.Your next step is to pay the Administration fee of $350.00 to ensure your unit is reserved until you move -in. Once you process your payment, you will be directed to prepare your lease.  ");
                            reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                            message = "Notification: Your Application is Approved and pay your Administration Fees. Please check the email for detail.";
                            string body = reportHTML;
                            new EmailSendModel().SendEmail(emaildata.Email, "Notification: Your Application is Approved and pay your Administration Fees.", body);
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(emaildata.Phone))
                                {
                                    new TwilioService().SMS(emaildata.Phone, message);
                                }
                            }
                        }
                    }
                }
                msg = "Employment Status Saved Successfully";
            }
            db.Dispose();
            return msg;
        }
        public string UpdateBGCCStatus(int ID,int TenantID, int Status, string Notes)
        {

            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (ID != 0)
            {

                var saveBGCC = new tbl_BackgroundScreening()
                {
                    TenantId = TenantID,
                    Type="0",
                    OrderID = ID,
                    Status = Status.ToString(),
                    Notes = Notes,
                };
                db.tbl_BackgroundScreening.Add(saveBGCC);
                db.SaveChanges();

                msg = " Status Saved Successfully";
            }
            db.Dispose();
            return msg;
        }
        public TenantOnlineModel GetTenantOnlineList(int id, int userid)
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
                    //Sachin Mahore 21 Apr 2020
                    DbParameter paramfF = cmd.CreateParameter();
                    paramfF.ParameterName = "toid";
                    paramfF.Value = userid;
                    cmd.Parameters.Add(paramfF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                lstpr.ID = 0;
                lstpr.ProspectID = 0;
                lstpr.IsInternational = 0;
                lstpr.Gender = 0;
                lstpr.IDType = 0;
                lstpr.State = 0;
                lstpr.Country = "1";
                lstpr.StateHome = 0;
                lstpr.RentOwn = 0;

                lstpr.Country2 = "0";
                lstpr.StateHome2 = 0;
                lstpr.RentOwn2 = 0;

                lstpr.JobType = 0;
                lstpr.OfficeCountry = "1";
                lstpr.OfficeState = 0;

                lstpr.EmergencyCountry = "1";
                lstpr.EmergencyStateHome = 0;
                lstpr.StepCompleted = 1;
                foreach (DataRow dr in dtTable.Rows)
                {
                    DateTime? dateOfBirth = null;
                    try { dateOfBirth = Convert.ToDateTime(dr["DateOfBirth"].ToString()); }
                    catch { }
                    DateTime? dateIssuance = null;
                    try { dateIssuance = Convert.ToDateTime(dr["DateIssuance"].ToString()); }
                    catch { }
                    DateTime? dateExpire = null;
                    try { dateExpire = Convert.ToDateTime(dr["DateExpire"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom = null;
                    try { moveInDateFrom = Convert.ToDateTime(dr["MoveInDateFrom"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo = null;
                    try { moveInDateTo = Convert.ToDateTime(dr["MoveInDateTo"].ToString()); }
                    catch { }
                    DateTime? moveInDateFrom2 = null;
                    try { moveInDateFrom2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? moveInDateTo2 = null;
                    try { moveInDateTo2 = Convert.ToDateTime(dr["MoveInDateTo2"].ToString()); }
                    catch { }
                    DateTime? startDate = null;
                    try {startDate = Convert.ToDateTime(dr["StartDate"].ToString());}
                    catch { }
                    lstpr.ID = Convert.ToInt32(dr["ID"].ToString());
                    lstpr.ProspectID = Convert.ToInt32(dr["ProspectID"].ToString());
                    lstpr.IsInternational = Convert.ToInt32(dr["IsInternational"].ToString());
                    lstpr.IsAdditionalRHistory = Convert.ToInt32(dr["IsAdditionalRHistory"].ToString());
                    lstpr.FirstName = dr["FirstName"].ToString();
                    lstpr.MiddleInitial = dr["MiddleInitial"].ToString();
                    lstpr.LastName = dr["LastName"].ToString();
                    lstpr.DateOfBirthTxt = dateOfBirth == null ? "" : dateOfBirth.Value.ToString("MM/dd/yyy");
                    lstpr.Gender = Convert.ToInt32(dr["Gender"].ToString());
                    lstpr.Email = dr["Email"].ToString();
                    lstpr.Mobile = dr["Mobile"].ToString();

                    if (!string.IsNullOrWhiteSpace(dr["PassportNumber"].ToString()))
                    {
                        string decryptedPassportNumber = new EncryptDecrypt().DecryptText(dr["PassportNumber"].ToString());
                        lstpr.PassportNumber = decryptedPassportNumber;
                    }
                    else
                    {
                        lstpr.PassportNumber = "";
                    }

                    lstpr.CountryIssuance = dr["CountryIssuance"].ToString();
                    lstpr.DateIssuanceTxt = dateIssuance == null ? "" : dateIssuance.Value.ToString("MM/dd/yyy");
                    lstpr.DateExpireTxt = dateExpire == null ? "" : dateExpire.Value.ToString("MM/dd/yyy");
                    lstpr.IDType = Convert.ToInt32(dr["IDType"].ToString());
                    lstpr.State = Convert.ToInt64(dr["State"].ToString());
                    if (!string.IsNullOrWhiteSpace(dr["IDNumber"].ToString()))
                    {
                        string decryptedIDNumber = new EncryptDecrypt().DecryptText(dr["IDNumber"].ToString());
                        lstpr.IDNumber = decryptedIDNumber;
                    }
                    else
                    {
                        lstpr.IDNumber = "";
                    }

                    if (!string.IsNullOrWhiteSpace(dr["SSN"].ToString()))
                    {
                        string decryptedSSN = new EncryptDecrypt().DecryptText(dr["SSN"].ToString());

                        lstpr.SSN = decryptedSSN.Substring(0, 2) + "-"+ decryptedSSN.Substring(2, 3) + "-" + decryptedSSN.Substring(decryptedSSN.Length - 4, 4);
                        //lstpr.SSN = decryptedSSN;
                    }
                    else
                    {
                        lstpr.SSN = "";
                    }
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
                    lstpr.IsPaystub = Convert.ToInt32(dr["IsPaystub"].ToString());

                    lstpr.CountryOfOrigin = Convert.ToInt64(dr["CountryOfOrigin"].ToString());
                    lstpr.Evicted = Convert.ToInt32(dr["Evicted"].ToString());
                    lstpr.EvictedDetails = dr["EvictedDetails"].ToString();
                    lstpr.ConvictedFelony = Convert.ToInt32(dr["ConvictedFelony"].ToString());
                    lstpr.ConvictedFelonyDetails = dr["ConvictedFelonyDetails"].ToString();
                    lstpr.CriminalChargPen = Convert.ToInt32(dr["CriminalChargPen"].ToString());
                    lstpr.CriminalChargPenDetails = dr["CriminalChargPenDetails"].ToString();
                    lstpr.DoYouSmoke = Convert.ToInt32(dr["DoYouSmoke"].ToString());
                    lstpr.ReferredResident = Convert.ToInt32(dr["ReferredResident"].ToString());
                    lstpr.ReferredResidentName = dr["ReferredResidentName"].ToString();
                    lstpr.ReferredBrokerMerchant = Convert.ToInt32(dr["ReferredBrokerMerchant"].ToString());
                    lstpr.ApartmentCommunity = dr["ApartmentCommunity"].ToString();
                    lstpr.ManagementCompany = dr["ManagementCompany"].ToString();
                    lstpr.ManagementCompanyPhone = dr["ManagementCompanyPhone"].ToString();
                    lstpr.IsProprNoticeLeaseAgreement = Convert.ToInt32(dr["IsProprNoticeLeaseAgreement"].ToString());

                    int countryOrigintemp = lstpr.CountryOfOrigin.HasValue ? Convert.ToInt32(lstpr.CountryOfOrigin) : 0;
                    var countryOriginVar = db.tbl_Country.Where(co => co.ID == countryOrigintemp).FirstOrDefault();
                    lstpr.CountryOfOriginString = countryOriginVar != null ? countryOriginVar.CountryName : "";

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

                    lstpr.StringEvicted = Convert.ToInt32(dr["Evicted"]) == 1 ? "No" : "Yes";
                    lstpr.StringConvictedFelony = Convert.ToInt32(dr["ConvictedFelony"]) == 1 ? "No" : "Yes";
                    lstpr.StringCriminalChargPen = Convert.ToInt32(dr["CriminalChargPen"]) == 1 ? "No" : "Yes";
                    lstpr.StringDoYouSmoke = Convert.ToInt32(dr["DoYouSmoke"]) == 1 ? "No" : "Yes";
                    lstpr.StringReferredResident = Convert.ToInt32(dr["ReferredResident"]) == 1 ? "No" : "Yes";
                    lstpr.StringReferredBrokerMerchant = Convert.ToInt32(dr["ReferredBrokerMerchant"]) == 1 ? "No" : "Yes";
                    lstpr.stringIsProprNoticeLeaseAgreement = Convert.ToInt32(dr["IsProprNoticeLeaseAgreement"]) == 1 ? "Yes" : "No";

                    //sachin m 11 may
                    lstpr.TaxReturn4 = dr["TaxReturn4"].ToString();
                    lstpr.TaxReturn5 = dr["TaxReturn5"].ToString();
                    lstpr.TaxReturn6 = dr["TaxReturn6"].ToString();
                    lstpr.TaxReturn7 = dr["TaxReturn7"].ToString();
                    lstpr.TaxReturn8 = dr["TaxReturn8"].ToString();
                    lstpr.UploadOriginalFileName4 = dr["TaxReturnOrginalFile4"].ToString();
                    lstpr.UploadOriginalFileName5 = dr["TaxReturnOrginalFile5"].ToString();
                    lstpr.UploadOriginalFileName6 = dr["TaxReturnOrginalFile6"].ToString();
                    lstpr.UploadOriginalFileName7 = dr["TaxReturnOrginalFile7"].ToString();
                    lstpr.UploadOriginalFileName8 = dr["TaxReturnOrginalFile8"].ToString();
                    lstpr.IsFedralTax = Convert.ToInt32(dr["IsFedralTax"].ToString());
                    lstpr.IsBankState = Convert.ToInt32(dr["IsBankState"].ToString());

                    var stepCompleted = Convert.ToInt32(dr["StepCompleted"].ToString());
                    lstpr.StepCompleted = stepCompleted;
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
    }

    public class TenantPetPlace
    {
        public long TPetID { get; set; }
        public Nullable<long> PetPlaceID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public int TenantPetCount { get; set; }
        public int NumberOfPets { get; set; }

        public TenantPetPlace GetTenantPetPlaceData(long Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TenantPetPlace model = new TenantPetPlace();

            var getTenantPetPlaceData = db.tbl_TenantPetPlace.Where(p => p.TenantID == Id).FirstOrDefault();
            var getPetCount = db.tbl_TenantPet.Where(p => p.TenantID == Id).ToList();

            if (getTenantPetPlaceData != null)
            {
                var getPetData = db.tbl_PetPlace.Where(p => p.PetPlaceID == getTenantPetPlaceData.PetPlaceID).FirstOrDefault();
                model.PetPlaceID = getTenantPetPlaceData.PetPlaceID;
                model.NumberOfPets = getPetData.Type ?? 0;

            }
            model.TenantPetCount = getPetCount != null ? getPetCount.Count : 0;
            model.TenantID = Id;
            return model;
        }
    }
}