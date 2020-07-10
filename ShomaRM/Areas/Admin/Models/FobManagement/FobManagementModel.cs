using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Models;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class FobManagement
    {
        public long TFobID { get; set; }
        public string FobID { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<long> ApplicantFob { get; set; }
        public string StatusString { get; set; }
        public string FobAddStatus { get; set; }
        public int FobCount { get; set; }

        public long? ApplyNowID { get; set; }
        public long? PropertyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string ApplicantStatus { get; set; }
        public string UnitNo { get; set; }

        public long ApplicantID { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string DateOfBirthTxt { get; set; }
        public Nullable<int> Gender { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string Type { get; set; }
        public string Relationship { get; set; }
        public string Bedrooms { get; set; }
        public string Building { get; set; }
        public string SSNstring { get; set; }
        public string MoveInPercentage { get; set; }
        public string MoveInCharges { get; set; }
        public string MonthlyPercentage { get; set; }
        public string MonthlyCharges { get; set; }
        public string ESignatureDate { get; set; }
        public string ESignatureStatus { get; set; }

        public long MIID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public Nullable<System.DateTime> MoveInDate { get; set; }
        public string MoveInDateString { get; set; }
        public string MoveInTime { get; set; }
        public string PreMoveInDateString { get; set; }
        public string PreMoveInTime { get; set; }
        public string InsuranceDoc { get; set; }
        public string ElectricityDoc { get; set; }
        public Nullable<int> IsCheckPO { get; set; }
        public Nullable<int> IsCheckATT { get; set; }
        public Nullable<int> IsCheckWater { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedDateString { get; set; }

        public string OtherFirstName { get; set; }
        public string OtherLastName { get; set; }
        public long? OtherId { get; set; }

        public List<FobManagement> ApprovedApplicantList { get; set; }
        public List<FobManagement> AllApplicantList { get; set; }
        public List<FobManagement> LeaseTermList { get; set; }
        public List<FobManagement> MoveInDataList { get; set; }

        //Search
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
        public string SortFname { get; set; }
        public string SortLname { get; set; }
        public string SortUno { get; set; }
        public string SortModel { get; set; }
        public Nullable<int> Movers { get; set; }
        public Nullable<int> IsCheckSD { get; set; }
    }
    public class FobManagementModel : FobManagement
    {
        public FobManagementModel GetDetails()
        {
            FobManagementModel model = new FobManagementModel();
            model.ApprovedApplicantList = GetApprovedApplicantList();
            return model;
        }

        public FobManagementModel GetDetailsForAddEdit(long TenantID)
        {
            FobManagementModel model = new FobManagementModel();
            model.AllApplicantList = GetAllApplicantList(TenantID);
            model.LeaseTermList = GetLeaseTermData(TenantID);
            model.MoveInDataList = GetAllApplicantMovingCharges(TenantID);
            return model;
        }

        public List<FobManagement> GetApprovedApplicantList()
        {
            List<FobManagement> listApplicant = new List<FobManagement>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getApprovedApplicantList = db.tbl_ApplyNow.Where(co => co.Status == "Approved").OrderByDescending(co => co.CreatedDate).ToList();
            if (getApprovedApplicantList != null)
            {
                foreach (var item in getApprovedApplicantList)
                {
                    var getUnitNo = db.tbl_PropertyUnits.Where(co => co.UID == item.PropertyId).FirstOrDefault();
                    listApplicant.Add(new FobManagement()
                    {
                        ApplyNowID = item.ID,
                        PropertyID = item.PropertyId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        FullName = item.FirstName + " " + item.LastName,
                        Email = item.Email,
                        PhoneNo = item.Phone,
                        UnitNo = getUnitNo.UnitNo,
                        Building = getUnitNo.Building,
                        Bedrooms = getUnitNo.Bedroom.ToString(),
                    });
                }
            }
            db.Dispose();
            return listApplicant;
        }

        public List<FobManagement> GetAllApplicantList(long TenantID)
        {
            List<FobManagement> modelList = new List<FobManagement>();
            ShomaRMEntities db = new ShomaRMEntities();
            string relat = string.Empty;
            string unit = string.Empty;
            string Bed = string.Empty;
            var getAllApplicantList = db.tbl_Applicant.Where(co => co.TenantID == TenantID).ToList();
            if (getAllApplicantList != null)
            {

                foreach (var item in getAllApplicantList)
                {
                    string SSN = String.Empty;
                    if (item.Type == "Primary Applicant")
                    {
                        var getSSN = db.tbl_TenantOnline.Where(co => co.ProspectID == item.TenantID).FirstOrDefault();
                        SSN = new EncryptDecrypt().DecryptText(getSSN.SSN);
                        SSN = "***-**-" + SSN.Substring(5);
                    }
                    else
                    {
                        SSN = "None";
                    }

                    var getPropId = db.tbl_ApplyNow.Where(co => co.ID == item.TenantID).FirstOrDefault();
                    if (getPropId != null)
                    {
                        var getUnitNo = db.tbl_PropertyUnits.Where(co => co.UID == getPropId.PropertyId).FirstOrDefault();
                        if (getUnitNo != null)
                        {
                            unit = getUnitNo.UnitNo;
                            Bed = getUnitNo.Bedroom != null ? getUnitNo.Bedroom.ToString() : "0";
                        }
                    }

                    if (item.Type == "Primary Applicant")
                    {
                        relat = item.Relationship == "1" ? "Self" : "";
                    }
                    else if (item.Type == "Co-Applicant")
                    {
                        relat = item.Relationship == "1" ? "Spouse" : item.Relationship == "2" ? "Partner" : item.Relationship == "3" ? "Adult Child" : "";
                    }
                    else if (item.Type == "Minor")
                    {
                        relat = item.Relationship == "1" ? "Family Member" : item.Relationship == "2" ? "Child" : "";
                    }
                    else if (item.Type == "Guarantor")
                    {
                        relat = item.Relationship == "1" ? "Family" : item.Relationship == "2" ? "Friend" : "";
                    }
                    var getDataTenantFob = db.tbl_TenantFob.Where(co => co.ApplicantID == item.ApplicantID).FirstOrDefault();
                    if (getDataTenantFob != null)
                    {
                        modelList.Add(new FobManagement()
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Type = item.Type,
                            Relationship = relat,
                            ApplicantID = item.ApplicantID,
                            TenantID = item.TenantID,
                            UnitNo = unit,
                            Bedrooms = Bed,
                            FullName = item.FirstName + " " + item.LastName,
                            DateOfBirthTxt = item.DateOfBirth.HasValue ? item.DateOfBirth.Value.ToString("MM/dd/yyyy") : "",
                            Email = !string.IsNullOrWhiteSpace(item.Email) ? item.Email : "",
                            Gender = item.Gender,
                            MoveInPercentage = item.MoveInPercentage.HasValue ? item.MoveInPercentage.Value.ToString() : "0.00",
                            MoveInCharges = item.MoveInCharge.HasValue ? item.MoveInCharge.Value.ToString("0.00") : "0.00",
                            MonthlyPercentage = item.MonthlyPercentage.HasValue ? item.MonthlyPercentage.Value.ToString() : "0.00",
                            MonthlyCharges = item.MonthlyPayment.HasValue ? item.MonthlyPayment.Value.ToString("0.00") : "0.00",
                            SSNstring = SSN,
                            FobID = getDataTenantFob.FobID,
                            Status = getDataTenantFob.Status
                        });
                    }
                    else
                    {
                        modelList.Add(new FobManagement()
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Type = item.Type,
                            Relationship = relat,
                            ApplicantID = item.ApplicantID,
                            TenantID = item.TenantID,
                            UnitNo = unit,
                            Bedrooms = Bed,
                            FullName = item.FirstName + " " + item.LastName,
                            DateOfBirthTxt = item.DateOfBirth.HasValue ? item.DateOfBirth.Value.ToString("MM/dd/yyyy") : "",
                            Email = !string.IsNullOrWhiteSpace(item.Email) ? item.Email : "",
                            Gender = item.Gender,
                            MoveInPercentage = item.MoveInPercentage.HasValue ? item.MoveInPercentage.Value.ToString() : "0.00",
                            MoveInCharges = item.MoveInCharge.HasValue ? item.MoveInCharge.Value.ToString("0.00") : "0.00",
                            MonthlyPercentage = item.MonthlyPercentage.HasValue ? item.MonthlyPercentage.Value.ToString() : "0.00",
                            MonthlyCharges = item.MonthlyPayment.HasValue ? item.MonthlyPayment.Value.ToString("0.00") : "0.00",
                            SSNstring = SSN,
                            FobID = "",
                            Status = 0,
                        });
                    }
                }
            }
            db.Dispose();
            return modelList;
        }

        public List<FobManagement> GetAllApplicant(long TenantID)
        {
            List<FobManagement> modelList = new List<FobManagement>();
            ShomaRMEntities db = new ShomaRMEntities();
            string relat = string.Empty;
            string unit = string.Empty;
            string Bed = string.Empty;
            var getAllApplicantList = db.tbl_Applicant.Where(co => co.TenantID == TenantID).ToList();
            if (getAllApplicantList != null)
            {

                foreach (var item in getAllApplicantList)
                {
                    string SSN = String.Empty;
                    if (item.Type == "Primary Applicant")
                    {
                        var getSSN = db.tbl_TenantOnline.Where(co => co.ProspectID == item.TenantID).FirstOrDefault();
                        SSN = new EncryptDecrypt().DecryptText(getSSN.SSN);
                        SSN = "***-**-" + SSN.Substring(5);
                    }
                    else
                    {
                        SSN = "None";
                    }

                    var getPropId = db.tbl_ApplyNow.Where(co => co.ID == item.TenantID).FirstOrDefault();
                    if (getPropId != null)
                    {
                        var getUnitNo = db.tbl_PropertyUnits.Where(co => co.UID == getPropId.PropertyId).FirstOrDefault();
                        if (getUnitNo != null)
                        {
                            unit = getUnitNo.UnitNo;
                            Bed = getUnitNo.Bedroom != null ? getUnitNo.Bedroom.ToString() : "0";
                        }
                    }

                    if (item.Type == "Primary Applicant")
                    {
                        relat = item.Relationship == "1" ? "Self" : "";
                    }
                    else if (item.Type == "Co-Applicant")
                    {
                        relat = item.Relationship == "1" ? "Spouse" : item.Relationship == "2" ? "Partner" : item.Relationship == "3" ? "Adult Child" : "";
                    }
                    else if (item.Type == "Minor")
                    {
                        relat = item.Relationship == "1" ? "Family Member" : item.Relationship == "2" ? "Child" : "";
                    }
                    else if (item.Type == "Guarantor")
                    {
                        relat = item.Relationship == "1" ? "Family" : item.Relationship == "2" ? "Friend" : "";
                    }

                    var getDataTenantFob = db.tbl_TenantFob.Where(co => co.ApplicantID == item.ApplicantID).ToList();
                    if (getDataTenantFob != null)
                    {
                        if (getDataTenantFob.Count > 0)
                        {
                            foreach (var df in getDataTenantFob)
                            {
                                if (item.Type != "Guarantor")
                                {
                                    modelList.Add(new FobManagement()
                                    {
                                        FirstName = item.FirstName,
                                        LastName = item.LastName,
                                        Type = item.Type,
                                        Relationship = relat,
                                        ApplicantID = item.ApplicantID,
                                        TenantID = item.TenantID,
                                        UnitNo = unit,
                                        Bedrooms = Bed,
                                        FullName = item.FirstName + " " + item.LastName,
                                        DateOfBirthTxt = item.DateOfBirth.HasValue ? item.DateOfBirth.Value.ToString("MM/dd/yyyy") : "",
                                        Email = !string.IsNullOrWhiteSpace(item.Email) ? item.Email : "",
                                        Gender = item.Gender,
                                        MoveInPercentage = item.MoveInPercentage.HasValue ? item.MoveInPercentage.Value.ToString() : "0.00",
                                        MoveInCharges = item.MoveInCharge.HasValue ? item.MoveInCharge.Value.ToString("0.00") : "0.00",
                                        MonthlyPercentage = item.MonthlyPercentage.HasValue ? item.MonthlyPercentage.Value.ToString() : "0.00",
                                        MonthlyCharges = item.MonthlyPayment.HasValue ? item.MonthlyPayment.Value.ToString("0.00") : "0.00",
                                        SSNstring = SSN,
                                        FobID = df.FobID,
                                        Status = df.Status
                                    });
                                }
                            }
                        }
                        else
                        {
                            if (item.Type != "Guarantor")
                            {
                                modelList.Add(new FobManagement()
                                {
                                    FirstName = item.FirstName,
                                    LastName = item.LastName,
                                    Type = item.Type,
                                    Relationship = relat,
                                    ApplicantID = item.ApplicantID,
                                    TenantID = item.TenantID,
                                    UnitNo = unit,
                                    Bedrooms = Bed,
                                    FullName = item.FirstName + " " + item.LastName,
                                    DateOfBirthTxt = item.DateOfBirth.HasValue ? item.DateOfBirth.Value.ToString("MM/dd/yyyy") : "",
                                    Email = !string.IsNullOrWhiteSpace(item.Email) ? item.Email : "",
                                    Gender = item.Gender,
                                    MoveInPercentage = item.MoveInPercentage.HasValue ? item.MoveInPercentage.Value.ToString() : "0.00",
                                    MoveInCharges = item.MoveInCharge.HasValue ? item.MoveInCharge.Value.ToString("0.00") : "0.00",
                                    MonthlyPercentage = item.MonthlyPercentage.HasValue ? item.MonthlyPercentage.Value.ToString() : "0.00",
                                    MonthlyCharges = item.MonthlyPayment.HasValue ? item.MonthlyPayment.Value.ToString("0.00") : "0.00",
                                    SSNstring = SSN,
                                    FobID = "",
                                    Status = 0,
                                });
                            }
                        }
                    }
                    else
                    {
                        if (item.Type != "Guarantor")
                        {
                            modelList.Add(new FobManagement()
                            {
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Type = item.Type,
                                Relationship = relat,
                                ApplicantID = item.ApplicantID,
                                TenantID = item.TenantID,
                                UnitNo = unit,
                                Bedrooms = Bed,
                                FullName = item.FirstName + " " + item.LastName,
                                DateOfBirthTxt = item.DateOfBirth.HasValue ? item.DateOfBirth.Value.ToString("MM/dd/yyyy") : "",
                                Email = !string.IsNullOrWhiteSpace(item.Email) ? item.Email : "",
                                Gender = item.Gender,
                                MoveInPercentage = item.MoveInPercentage.HasValue ? item.MoveInPercentage.Value.ToString() : "0.00",
                                MoveInCharges = item.MoveInCharge.HasValue ? item.MoveInCharge.Value.ToString("0.00") : "0.00",
                                MonthlyPercentage = item.MonthlyPercentage.HasValue ? item.MonthlyPercentage.Value.ToString() : "0.00",
                                MonthlyCharges = item.MonthlyPayment.HasValue ? item.MonthlyPayment.Value.ToString("0.00") : "0.00",
                                SSNstring = SSN,
                                FobID = "",
                                Status = 0,
                            });
                        }
                    }
                    
                }
                var getDataTenantFobOther = db.tbl_TenantFob.Where(co => co.ApplicantID == 0 && co.TenantID == TenantID).ToList();
                if (getDataTenantFobOther != null)
                {
                    foreach (var ct in getDataTenantFobOther)
                    {
                        modelList.Add(new FobManagement()
                        {
                            FirstName = ct.OtherFirstName,
                            LastName = ct.OtherLastName,
                            Type = "Other",
                            Relationship = ct.OtherRelationship,
                            ApplicantID = 0,
                            TenantID = ct.TenantID,
                            UnitNo = unit,
                            Bedrooms = Bed,
                            FullName = ct.OtherFirstName + " " + ct.OtherLastName,
                            OtherId = ct.OtherId,
                            FobID = ct.FobID,
                            Status = ct.Status,
                        });
                    }
                }
            }
            db.Dispose();
            return modelList;
        }

        public FobManagementModel GetApplicantData(long ApplicantId)
        {
            FobManagementModel model = new FobManagementModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var getApplicantData = db.tbl_Applicant.Where(co => co.ApplicantID == ApplicantId).FirstOrDefault();
            if (getApplicantData != null)
            {
                string SSN = String.Empty;
                if (getApplicantData.Type == "Primary Applicant")
                {
                    var getSSN = db.tbl_TenantOnline.Where(co => co.ProspectID == getApplicantData.TenantID).FirstOrDefault();
                    SSN = new EncryptDecrypt().DecryptText(getSSN.SSN);
                }
                else
                {
                    SSN = "None";
                }
                model.ApplicantID = getApplicantData.ApplicantID;
                model.FullName = getApplicantData.FirstName + " " + getApplicantData.LastName;
                model.DateOfBirthTxt = getApplicantData.DateOfBirth.HasValue ? getApplicantData.DateOfBirth.Value.ToString("MM/dd/yyyy") : "";
                model.Email = !string.IsNullOrWhiteSpace(getApplicantData.Email) ? getApplicantData.Email : "";
                model.Gender = getApplicantData.Gender;
                model.SSNstring = SSN;
                model.Type = getApplicantData.Type;
                model.MoveInPercentage = getApplicantData.MoveInPercentage.HasValue ? getApplicantData.MoveInPercentage.Value.ToString() : "0.00";
                model.MoveInCharges = getApplicantData.MoveInCharge.HasValue ? getApplicantData.MoveInCharge.Value.ToString() : "0.00";
                model.MonthlyPercentage = getApplicantData.MonthlyPercentage.HasValue ? getApplicantData.MonthlyPercentage.Value.ToString() : "0.00";
                model.MonthlyCharges = getApplicantData.MonthlyPayment.HasValue ? getApplicantData.MonthlyPayment.Value.ToString() : "0.00";
            }
            db.Dispose();
            return model;
        }

        public List<FobManagement> GetLeaseTermData(long TenantID)
        {
            List<FobManagement> model = new List<FobManagement>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getAppData = db.tbl_Applicant.Where(co => co.TenantID == TenantID).ToList();
            if (getAppData!=null)
            {
                foreach (var item in getAppData)
                {
                    var getLeaseTerm = db.tbl_ESignatureKeys.Where(co => co.TenantID == item.TenantID && co.ApplicantID == item.ApplicantID).FirstOrDefault();
                    if (getLeaseTerm!=null)
                    {
                        model.Add(new FobManagement()
                        {
                            FullName = item.FirstName + " " + item.LastName,
                            Type = item.Type,
                            ApplicantFob = item.ApplicantID,
                            ESignatureDate = !string.IsNullOrWhiteSpace(getLeaseTerm.DateSigned) ? getLeaseTerm.DateSigned : "",
                            ESignatureStatus = !string.IsNullOrWhiteSpace(getLeaseTerm.DateSigned) ? "Signed" : "Not Signed",
                        });
                    }
                    else
                    {
                        model.Add(new FobManagement()
                        {
                            FullName = item.FirstName + " " + item.LastName,
                            Type = item.Type,
                            ApplicantFob = item.ApplicantID,
                            ESignatureDate = "",
                            ESignatureStatus = "Not Signed",
                        });
                    }
                }
                
            }
            db.Dispose();
            return model;
        }

        public string SaveTenantFob(long TenantID, long ApplicantId, int Status, string FobKey, int OtherId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var updateTenantFob = db.tbl_TenantFob.Where(co => co.ApplicantID == ApplicantId && co.FobID == FobKey).FirstOrDefault();
            
            if (updateTenantFob != null)
            {
                var updateTenantFobOther = db.tbl_TenantFob.Where(co => co.ApplicantID == 0 && co.OtherId == OtherId && co.FobID == FobKey).FirstOrDefault();
                if (updateTenantFobOther != null)
                {
                    if (Status == 1)
                    {
                        updateTenantFobOther.Status = 1;
                        msg = "FOB Activated successfully";
                    }
                    else if (Status == 2)
                    {
                        updateTenantFobOther.Status = 2;
                        msg = "FOB Deactivated successfully";
                    }
                    db.SaveChanges();

                    db.Dispose();
                    return msg;
                }
                else
                {
                    if (Status == 1)
                    {
                        updateTenantFob.Status = 1;
                        msg = "FOB Activated successfully";
                    }
                    else if (Status == 2)
                    {
                        updateTenantFob.Status = 2;
                        msg = "FOB Deactivated successfully";
                    }

                    db.SaveChanges();

                    db.Dispose();
                    return msg;
                }
            }
            else
            {
                var saveTenantFob = new tbl_TenantFob()
                {
                    TenantID = TenantID,
                    FobID = FobKey,
                    ApplicantID = ApplicantId,
                    Status = Status,
                };
                db.tbl_TenantFob.Add(saveTenantFob);
                db.SaveChanges();
                msg = "FOB Activated successfully";
                db.Dispose();
                return msg;
            }
        }

        public List<FobManagement> GetAllApplicantMovingCharges(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FobManagement> model = new List<FobManagement>();
            var getApplicantList = db.tbl_Applicant.Where(co => co.TenantID == TenantID).ToList();
            if (getApplicantList != null)
            {
                foreach (var item in getApplicantList)
                {
                    if (item.Type != "Guarantor" && item.Type != "Minor")
                    {
                        var getMoveInchecklist = db.tbl_MoveInChecklist.Where(co => co.ProspectID == item.TenantID).FirstOrDefault();
                        model.Add(new FobManagementModel()
                        {
                            ApplicantID = item.ApplicantID,
                            FullName = item.FirstName + " " + item.LastName,
                            Type = item.Type,
                            MoveInDateString = getMoveInchecklist == null ? "" : getMoveInchecklist.MoveInDate.HasValue ? getMoveInchecklist.MoveInDate.Value.ToString("MM/dd/yyyy") : "",
                            MoveInTime = getMoveInchecklist == null ? "" : !string.IsNullOrWhiteSpace(getMoveInchecklist.MoveInTime) ? getMoveInchecklist.MoveInTime : "",
                            PreMoveInDateString = getMoveInchecklist == null ? "" : getMoveInchecklist.PreMoveInDate.HasValue ? getMoveInchecklist.PreMoveInDate.Value.ToString("MM/dd/yyyy") : "",
                            PreMoveInTime = getMoveInchecklist == null ? "" : !string.IsNullOrWhiteSpace(getMoveInchecklist.PreMoveInTime) ? getMoveInchecklist.PreMoveInTime : "",
                            MoveInCharges = getMoveInchecklist == null ? "" : getMoveInchecklist.MoveInCharges.HasValue ? getMoveInchecklist.MoveInCharges.Value.ToString("0.00") : "",
                            InsuranceDoc = getMoveInchecklist == null ? "" : !string.IsNullOrWhiteSpace(getMoveInchecklist.InsuranceDoc) ? getMoveInchecklist.InsuranceDoc : "",
                            ElectricityDoc = getMoveInchecklist == null ? "" : !string.IsNullOrWhiteSpace(getMoveInchecklist.ElectricityDoc) ? getMoveInchecklist.ElectricityDoc : "",
                            IsCheckPO = getMoveInchecklist == null ? 0 : getMoveInchecklist.IsCheckPO.HasValue ? getMoveInchecklist.IsCheckPO : 0,
                            IsCheckATT = getMoveInchecklist == null ? 0 : getMoveInchecklist.IsCheckATT.HasValue ? getMoveInchecklist.IsCheckATT : 0,
                            IsCheckWater = getMoveInchecklist == null ? 0 : getMoveInchecklist.IsCheckWater.HasValue ? getMoveInchecklist.IsCheckWater : 0,
                            CreatedDateString = getMoveInchecklist == null ? "" : getMoveInchecklist.CreatedDate.HasValue ? getMoveInchecklist.CreatedDate.Value.ToString("MM/dd/yyyy") : "",
                            IsCheckSD = getMoveInchecklist == null ? 0 : getMoveInchecklist.IsCheckSD.HasValue ? getMoveInchecklist.IsCheckSD : 0,
                            Movers = getMoveInchecklist == null ? 0 : getMoveInchecklist.Movers.HasValue ? getMoveInchecklist.Movers : 0,
                        });
                    }
                }
            }

            return model;
        }

        public string DelTenantFob(long TenantID, long ApplicantId, int OtherId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var updateTenantFob = db.tbl_TenantFob.Where(co => co.ApplicantID == ApplicantId && co.TenantID == TenantID).FirstOrDefault();
            var updateTenantFobOther = db.tbl_TenantFob.Where(co => co.ApplicantID == 0 && co.TenantID == TenantID && co.OtherId == OtherId).FirstOrDefault();
            if (updateTenantFob!=null)
            {
                db.tbl_TenantFob.Remove(updateTenantFob);
                db.SaveChanges();
                db.Dispose();
                msg = "FOB Removed successfully";
            }
            else if (updateTenantFobOther!=null)
            {
                db.tbl_TenantFob.Remove(updateTenantFobOther);
                db.SaveChanges();
                db.Dispose();
                msg = "FOB Removed successfully";
            }
            return msg;
        }

        public List<FobManagementModel> GetApplicantNamesForFob(long TenantID)
        {
            List<FobManagementModel> list = new List<FobManagementModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getApplicantNames = db.tbl_Applicant.Where(co => co.TenantID == TenantID).ToList();
            if (getApplicantNames != null)
            {
                foreach (var item in getApplicantNames)
                {
                    if (item.Type != "Guarantor")
                    {
                        list.Add(new FobManagementModel()
                        {
                            ApplicantID = item.ApplicantID,
                            FullName = item.FirstName + " " + item.LastName,
                            TenantID = item.TenantID,
                        });
                    }
                }
            }
            return list;
        }

        public FobManagementModel GetTenantFobData(long ApplicantId, long TenantID)
        {
            FobManagementModel model = new FobManagementModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var checkAlreadyExist = db.tbl_TenantFob.Where(co => co.ApplicantID == ApplicantId && co.Status == 1).FirstOrDefault();
            var getPropId = db.tbl_ApplyNow.Where(co => co.ID == TenantID).FirstOrDefault();
            var getBed = db.tbl_PropertyUnits.Where(co => co.UID == getPropId.PropertyId).FirstOrDefault();
            
            string Relationshi = "";
            string UnitN = "";
            if (checkAlreadyExist == null)
            {
                var getApplicant = db.tbl_Applicant.Where(co => co.ApplicantID == ApplicantId).FirstOrDefault();
                if (getApplicant != null)
                {
                    if (getApplicant.Type == "Primary Applicant")
                    {
                        Relationshi = getApplicant.Relationship == "1" ? "Self" : "";
                    }
                    else if (getApplicant.Type == "Co-Applicant")
                    {
                        Relationshi = getApplicant.Relationship == "1" ? "Spouse" : getApplicant.Relationship == "2" ? "Partner" : getApplicant.Relationship == "3" ? "Adult Child"
                        : getApplicant.Relationship == "1" ? "Friend/Roommate" : "";
                    }
                    else if (getApplicant.Type == "Minor")
                    {
                        Relationshi = getApplicant.Relationship == "1" ? "Family Member" : getApplicant.Relationship == "2" ? "Child" : "";
                    }
                    var getPropertyId = db.tbl_ApplyNow.Where(co => co.ID == getApplicant.TenantID).FirstOrDefault();
                    if (getPropertyId != null)
                    {
                        var getUnitNo = db.tbl_PropertyUnits.Where(co => co.UID == getPropertyId.PropertyId).FirstOrDefault();
                        if (getUnitNo != null)
                        {
                            UnitN = getUnitNo.UnitNo;
                        }
                    }
                    model.UnitNo = UnitN;
                    model.Type = getApplicant.Type;
                    model.FullName = getApplicant.FirstName + " " + getApplicant.LastName;
                    model.Relationship = Relationshi;
                }
                else
                {
                    if (ApplicantId == -1)
                    {
                        var getPId = db.tbl_ApplyNow.Where(co => co.ID == TenantID).FirstOrDefault();
                        if (getPId != null)
                        {
                            var getUnitNo = db.tbl_PropertyUnits.Where(co => co.UID == getPId.PropertyId).FirstOrDefault();
                            if (getUnitNo!=null)
                            {
                                var getOtherId = db.tbl_TenantFob.Where(co => co.TenantID == TenantID && co.ApplicantID == 0).OrderByDescending(co => co.OtherId).FirstOrDefault();
                                model.OtherId = getOtherId == null ? 1 : getOtherId.OtherId == null ? 1 : getOtherId.OtherId + 1;
                                model.UnitNo = !string.IsNullOrWhiteSpace(getUnitNo.UnitNo) ? getUnitNo.UnitNo : "";
                                model.Type = "Other";
                                model.FullName = "";
                                model.Relationship = "Other";
                            }
                            else
                            {
                                model.UnitNo = "";
                                model.Type = "";
                                model.FullName = "";
                                model.Relationship = "";
                            }

                        }
                        else
                        {
                            model.UnitNo = "";
                            model.Type = "";
                            model.FullName = "";
                            model.Relationship = "";
                            model.StatusString = "InvalidId";
                        }
                        
                    }
                    else
                    {
                        model.UnitNo = "";
                        model.Type = "";
                        model.FullName = "";
                        model.Relationship = "";
                        model.StatusString = "InvalidId";
                    }
                }
            }
            else
            {
                model.StatusString = "Already Activated";
                model.UnitNo = "";
                model.Type = "";
                model.FullName = "";
                model.Relationship = "";
            }

            return model;
        }

        public string SaveOtherTenantFob(long ApplicantId, long TenantID, string Fname, string Lname, string Relationship, string FobKey, int Status,long OtherId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var saveOtherTenantFob = new tbl_TenantFob()
            {
                TenantID = TenantID,
                FobID = FobKey,
                ApplicantID = ApplicantId,
                Status = Status,
                OtherFirstName = Fname,
                OtherLastName = Lname,
                OtherRelationship = Relationship,
                OtherId = OtherId
            };
            db.tbl_TenantFob.Add(saveOtherTenantFob);
            db.SaveChanges();
            msg = "Saved Successfully";
            db.Dispose();
            return msg;
        }

        public int BuildPaganationPreMoving(FobManagementModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPreMovingPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //DbParameter param0 = cmd.CreateParameter();
                    //param0.ParameterName = "FromDate";
                    //param0.Value = model.FromDate;
                    //cmd.Parameters.Add(param0);

                    //DbParameter param1 = cmd.CreateParameter();
                    //param1.ParameterName = "ToDate";
                    //param1.Value = model.ToDate;
                    //cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

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

        public List<FobManagementModel> FillPreMovingSearchGrid(FobManagementModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FobManagementModel> lstPreMoving = new List<FobManagementModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPreMovingPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //DbParameter param0 = cmd.CreateParameter();
                    //param0.ParameterName = "FromDate";
                    //param0.Value = model.FromDate;
                    //cmd.Parameters.Add(param0);

                    //DbParameter param1 = cmd.CreateParameter();
                    //param1.ParameterName = "ToDate";
                    //param1.Value = model.ToDate;
                    //cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    FobManagementModel searchmodel = new FobManagementModel();
                    searchmodel.ApplyNowID = Convert.ToInt64(dr["ApplyNowID"].ToString());
                    searchmodel.PropertyID = Convert.ToInt64(dr["PropertyID"].ToString());
                    searchmodel.FirstName = dr["FirstName"].ToString();
                    searchmodel.LastName = dr["LastName"].ToString();
                    searchmodel.FullName = dr["FullName"].ToString();
                    searchmodel.Email = dr["Email"].ToString();
                    searchmodel.PhoneNo = dr["PhoneNo"].ToString();
                    searchmodel.UnitNo = dr["UnitNo"].ToString();
                    searchmodel.Building = dr["Building"].ToString();
                    searchmodel.Bedrooms = dr["Bedroom"].ToString();
                    searchmodel.CreatedDateString = dr["CreatedByDate"].ToString();
                    lstPreMoving.Add(searchmodel);
                }
                db.Dispose();
                return lstPreMoving.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public string saveMoveInTime(string MoveInTime, long TenantID)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var saveMovInTim = db.tbl_MoveInChecklist.Where(co => co.ProspectID == TenantID).FirstOrDefault();
            if (saveMovInTim != null)
            {
                saveMovInTim.MoveInTime = MoveInTime;
                db.SaveChanges();
                msg = "Move In Time Updated Successfully";
            }
            db.Dispose();
            return msg;
        }
    }
}