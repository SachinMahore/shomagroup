using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models
{
    public class ApplicantModel
    {
        public long ApplicantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string DateOfBirthTxt { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string Type { get; set; }
        public string Relationship { get; set; }

        public Nullable<decimal> MoveInPercentage { get; set; }
        public Nullable<decimal> MoveInCharge { get; set; }
        public Nullable<decimal> MonthlyPercentage { get; set; }
        public Nullable<decimal> MonthlyPayment { get; set; }
        public string ComplStatus { get; set; }
        public string OtherGender { get; set; }
        public string RelationshipString { get; set; }
        public string GenderString { get; set; }
        public int StepCompleted { get; set; }
        public long ProspectID { get; set; }

        public string SaveUpdateApplicant(ApplicantModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
            {
                model.DateOfBirth = null;
            }
            else
            {
                model.DateOfBirth = model.DateOfBirth;
            }
            if (model.ApplicantID == 0)
            {
                var saveApplicant = new tbl_Applicant()
                {
                    ApplicantID = model.ApplicantID,
                    TenantID = model.TenantID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Type = model.Type,
                    Relationship = model.Relationship,
                    OtherGender = model.OtherGender
                };
                if (model.Type == "Primary Applicant")
                {
                    var updateTenantOnline = db.tbl_TenantOnline.Where(co => co.ProspectID == TenantID).FirstOrDefault();
                    if (updateTenantOnline != null)
                    {
                        updateTenantOnline.DateOfBirth = model.DateOfBirth;
                        updateTenantOnline.Gender = model.Gender;
                        updateTenantOnline.OtherGender = model.OtherGender;
                        db.SaveChanges();
                    }
                }

                db.tbl_Applicant.Add(saveApplicant);
                db.SaveChanges();

                msg = "Applicant Saved Successfully";
            }
            else
            {
                var getAppldata = db.tbl_Applicant.Where(p => p.ApplicantID == model.ApplicantID).FirstOrDefault();
                if (getAppldata != null)
                {

                    getAppldata.FirstName = model.FirstName;
                    getAppldata.LastName = model.LastName;
                    getAppldata.Phone = model.Phone;
                    getAppldata.Email = model.Email;
                    if (model.DateOfBirth == Convert.ToDateTime("01/01/0001 12:00:00 AM"))
                    {
                        getAppldata.DateOfBirth = null;
                    }
                    else
                    {
                        getAppldata.DateOfBirth = model.DateOfBirth;
                    }

                    getAppldata.Gender = model.Gender;
                    getAppldata.Type = model.Type;
                    getAppldata.Relationship = model.Relationship;
                    getAppldata.OtherGender = model.OtherGender;

                    if (model.Type == "Primary Applicant")
                    {
                        var updateTenantOnline = db.tbl_TenantOnline.Where(co => co.ProspectID == model.TenantID).FirstOrDefault();
                        if (updateTenantOnline != null)
                        {
                            updateTenantOnline.DateOfBirth = model.DateOfBirth;
                            updateTenantOnline.Gender = model.Gender;
                            updateTenantOnline.OtherGender = model.OtherGender;

                            db.SaveChanges();
                        }
                    }
                }
                db.SaveChanges();
                msg = "Applicant Updated Successfully";
            }

            db.Dispose();
            return msg;


        }

        public List<ApplicantModel> GetApplicantList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplicantModel> lstProp = new List<ApplicantModel>();
            var vehList = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
            foreach (var ap in vehList)
            {
                string compl = "";
                string Rel = "";
                if (ap.Phone == null && ap.Email == null && ap.DateOfBirth == null)
                {
                    compl = "Unstarted";
                }
                else if (ap.Phone != null && ap.Email != null && ap.DateOfBirth != null)
                {
                    compl = "Completed";
                }
                else
                {
                    compl = "Pending";
                }
                DateTime? dobDateTime = null;
                try
                {
                    dobDateTime = Convert.ToDateTime(ap.DateOfBirth);
                }
                catch { }
                if (ap.Type == "Primary Applicant")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Self" : "";
                }
                else if (ap.Type == "Co-Applicant")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Spouse" : ap.Relationship == "2" ? "Partner" : ap.Relationship == "3" ? "Adult Child" : ap.Relationship == "4" ? "Friend/Roommate" : "";
                }
                else if (ap.Type == "Minor")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Family Member" : ap.Relationship == "2" ? "Child" : "";
                }
                else if (ap.Type == "Guarantor")
                {
                    Rel = ap.Relationship == null ? "" : ap.Relationship == "1" ? "Family Member" : ap.Relationship == "2" ? "Friend" : "";
                }
                lstProp.Add(new ApplicantModel
                {
                    
                    ApplicantID = ap.ApplicantID,
                    FirstName = ap.FirstName,
                    LastName = ap.LastName,
                    Phone = ap.Phone,
                    Email = ap.Email,
                    Type = ap.Type,
                    Gender = ap.Gender,
                    MoveInPercentage = ap.MoveInPercentage != null ? ap.MoveInPercentage : 0,
                    MoveInCharge = ap.MoveInCharge != null ? ap.MoveInCharge : 0,
                    MonthlyPercentage = ap.MonthlyPercentage != null ? ap.MonthlyPercentage : 0,
                    MonthlyPayment = ap.MonthlyPayment != null ? ap.MonthlyPayment : 0,
                    ComplStatus = compl,
                    OtherGender = ap.OtherGender != null ? OtherGender : "",
                    
                    RelationshipString = Rel,
                    DateOfBirth = ap.DateOfBirth,
                    DateOfBirthTxt = dobDateTime == null ? "" : dobDateTime.Value.ToString("MM/dd/yyyy"),
                    GenderString = ap.Gender == 1 ? "Male" : ap.Gender == 2 ? "Female" : ap.Gender == 3 ? "Other" : ""
                });
            }
            return lstProp;
        }
        public ApplicantModel GetApplicantDetails(int id,int chargetype)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantModel model = new ApplicantModel();

            var getApplicantDet = db.tbl_Applicant.Where(p => p.ApplicantID == id).FirstOrDefault();
            var getTenantDet = db.tbl_ApplyNow.Where(p => p.ID == getApplicantDet.TenantID).FirstOrDefault();
            var getAppliTransDet = db.tbl_Transaction.Where(p => p.TenantID ==getTenantDet.UserId  && p.Batch==id.ToString() && p.Charge_Type== chargetype).FirstOrDefault();
            if (getAppliTransDet == null)
            {
                DateTime? dobDateTime = null;
                try
                {

                    dobDateTime = Convert.ToDateTime(getApplicantDet.DateOfBirth);
                }
                catch
                {

                }

                model.DateOfBirthTxt = dobDateTime == null ? "" : dobDateTime.Value.ToString("MM/dd/yyyy");
                model.FirstName = getApplicantDet.FirstName;
                model.LastName = getApplicantDet.LastName;
                model.Phone = getApplicantDet.Phone;
                model.Email = getApplicantDet.Email;
                model.Gender = getApplicantDet.Gender;
                model.Type = getApplicantDet.Type;
                model.Relationship = getApplicantDet.Relationship;
                model.MoveInPercentage = getApplicantDet.MoveInPercentage;
                model.MoveInCharge = getApplicantDet.MoveInCharge;
                model.MonthlyPercentage = getApplicantDet.MonthlyPercentage;
                model.MonthlyPayment = getApplicantDet.MonthlyPayment;
                model.OtherGender = getApplicantDet.OtherGender;
                model.TenantID = getApplicantDet.TenantID;
            }
            else
            {
                model.FirstName = getApplicantDet.FirstName;
                model.LastName = getApplicantDet.LastName;
                model.Type = "5";
            }
            return model;
        }

        public string DeleteApplicant(long AID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (AID != 0)
            {

                var appliData = db.tbl_Applicant.Where(p => p.ApplicantID == AID).FirstOrDefault();
                if (appliData != null)
                {
                    db.tbl_Applicant.Remove(appliData);
                    db.SaveChanges();
                    msg = "Applicant Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public string SaveUpdatePaymentResponsibility(List<ApplicantModel> model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            long prospectID = 0;
            int stepcompModel = 0;
            foreach (ApplicantModel item in model)
            {
                prospectID = item.ProspectID;
                stepcompModel = item.StepCompleted;
                if (item.ApplicantID == 0)
                {
                    var saveResponsibility = new tbl_Applicant()
                    {
                        MoveInPercentage = item.MoveInPercentage,
                        MoveInCharge = item.MoveInCharge,
                        MonthlyPercentage = item.MonthlyPercentage,
                        MonthlyPayment = item.MonthlyPayment
                    };
                    db.tbl_Applicant.Add(saveResponsibility);
                    db.SaveChanges();


                    msg = "Applicant Saved Successfully";
                }
                else
                {
                    var getAppldata = db.tbl_Applicant.Where(p => p.ApplicantID == item.ApplicantID).FirstOrDefault();
                    if (getAppldata != null)
                    {
                        getAppldata.MoveInPercentage = item.MoveInPercentage;
                        getAppldata.MoveInCharge = item.MoveInCharge;
                        getAppldata.MonthlyPercentage = item.MonthlyPercentage;
                        getAppldata.MonthlyPayment = item.MonthlyPayment;

                    }
                    db.SaveChanges();
                    msg = "Applicant Updated Successfully";
                }
            }

            var applyNow = db.tbl_ApplyNow.Where(p => p.ID == prospectID).FirstOrDefault();

            if(applyNow!=null)
            {
                int stepcomp = 0;
                stepcomp = applyNow.StepCompleted ?? 0;
                if (stepcomp < stepcompModel)
                {
                    stepcomp = stepcompModel;
                    db.SaveChanges();
                }
            }

            db.Dispose();
            return msg;
        }
    }
}