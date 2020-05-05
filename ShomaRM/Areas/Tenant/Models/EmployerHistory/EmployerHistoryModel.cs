using ShomaRM.Data;
using ShomaRM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models
{
    public class EmployerHistoryModel
    {
        public long HEIID { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> JobType { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> TerminationDate { get; set; }
        public Nullable<decimal> AnnualIncome { get; set; }
        public Nullable<decimal> AddAnnualIncome { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorPhone { get; set; }
        public string SupervisorEmail { get; set; }
        public Nullable<int> Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Nullable<int> State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string TaxReturn1 { get; set; }
        public string TaxReturn2 { get; set; }
        public string TaxReturn3 { get; set; }
        public string TaxReturn1OrigName { get; set; }
        public string TaxReturn2OrigName { get; set; }
        public string TaxReturn3OrigName { get; set; }
        public Nullable<int> PaystubValue { get; set; }
        public long TenantId { get; set; }
        public string JobTypeName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string StartDateString { get; set; }
        public string TerminationDateString { get; set; }
        public string TerminationReason { get; set; }
        public int TotalMonthsEmployerHistory { get; set; }
        //Sachin Mahore 23 Apr
        public long ParentTOID { get; set; }
        public string saveUpdateEmployerHistory(EmployerHistoryModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.HEIID == 0)
            {
                var saveEmployerHistory = new tbl_EmployerHistory()
                {
                    HEIID = model.HEIID,
                    EmployerName = model.EmployerName,
                    JobTitle = model.JobTitle,
                    JobType = model.JobType,
                    StartDate = model.StartDate,
                    TerminationDate = model.TerminationDate,
                    AnnualIncome = model.AnnualIncome,
                    AddAnnualIncome = model.AddAnnualIncome,
                    SupervisorName = model.SupervisorName,
                    SupervisorPhone = model.SupervisorPhone,
                    SupervisorEmail = model.SupervisorEmail,
                    Country = model.Country,
                    Address1 = model.Address1,
                    Address2 = model.Address2,
                    State = model.State,
                    City = model.City,
                    Zip = model.Zip,
                    TenantId = model.TenantId,
                    TerminationReason = model.TerminationReason,
                    ParentTOID= userid,
                };
                db.tbl_EmployerHistory.Add(saveEmployerHistory);
                db.SaveChanges();
                msg = "Progress Saved";
            }
            else
            {
                var updateEmployerHistory = db.tbl_EmployerHistory.Where(co => co.HEIID == model.HEIID).FirstOrDefault();
                if (updateEmployerHistory != null)
                {
                    updateEmployerHistory.HEIID = model.HEIID;
                    updateEmployerHistory.EmployerName = model.EmployerName;
                    updateEmployerHistory.JobTitle = model.JobTitle;
                    updateEmployerHistory.JobType = model.JobType;
                    updateEmployerHistory.StartDate = model.StartDate;
                    updateEmployerHistory.TerminationDate = model.TerminationDate;
                    updateEmployerHistory.AnnualIncome = model.AnnualIncome;
                    updateEmployerHistory.AddAnnualIncome = model.AddAnnualIncome;
                    updateEmployerHistory.SupervisorName = model.SupervisorName;
                    updateEmployerHistory.SupervisorPhone = model.SupervisorPhone;
                    updateEmployerHistory.SupervisorEmail = model.SupervisorEmail;
                    updateEmployerHistory.Country = model.Country;
                    updateEmployerHistory.Address1 = model.Address1;
                    updateEmployerHistory.Address2 = model.Address2;
                    updateEmployerHistory.State = model.State;
                    updateEmployerHistory.City = model.City;
                    updateEmployerHistory.Zip = model.Zip;
                    updateEmployerHistory.TenantId = model.TenantId;
                    updateEmployerHistory.TerminationReason = model.TerminationReason;
                    db.SaveChanges();
                    msg = "Progress Updated";
                }
            }
            db.Dispose();
            return msg;
        }

        public List<EmployerHistoryModel> GetEmployerHistory(long TenantId)
        {
            List<EmployerHistoryModel> model = new List<EmployerHistoryModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            long addedby = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var getEmployerHistorydata = db.tbl_EmployerHistory.Where(co => co.TenantId == TenantId && co.ParentTOID == addedby).ToList();
            if (getEmployerHistorydata != null)
            {
                foreach (var item in getEmployerHistorydata)
                {
                    var countryName = db.tbl_Country.Where(co => co.ID == item.Country).FirstOrDefault();
                    var StateName = db.tbl_State.Where(co => co.ID == item.State).FirstOrDefault();
                    model.Add(new EmployerHistoryModel()
                    {
                        HEIID = item.HEIID,
                        EmployerName = item.EmployerName,
                        JobTitle = !string.IsNullOrWhiteSpace(item.JobTitle) ? item.JobTitle : "",
                        JobType = item.JobType,
                        StartDate = item.StartDate,
                        TerminationDate = item.TerminationDate,
                        AnnualIncome = item.AnnualIncome,
                        AddAnnualIncome = item.AddAnnualIncome,
                        SupervisorName = item.SupervisorName,
                        SupervisorPhone = item.SupervisorPhone,
                        SupervisorEmail = item.SupervisorEmail,
                        Country = item.Country,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        State = item.State,
                        City = item.City,
                        Zip = item.Zip,
                        CountryName = countryName != null ? countryName.CountryName : "",
                        StateName = StateName != null ? StateName.StateName : "",
                        JobTypeName = item.JobType == 1 ? "Permanent" : item.JobType == 2 ? "Contract Basis" : "",
                        StartDateString = item.StartDate != null ? item.StartDate.Value.ToString("MM/dd/yyyy") : "",
                        TerminationDateString = item.TerminationDate != null ? item.TerminationDate.Value.ToString("MM/dd/yyyy") : "",
                        TerminationReason = !string.IsNullOrWhiteSpace(item.TerminationReason) ? item.TerminationReason : ""
                    });
                }

            }
            return model;
        }

        public string DeleteEmployerHistory(long HEIID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var deleteEmployerHistory = db.tbl_EmployerHistory.Where(co => co.HEIID == HEIID).FirstOrDefault();

            if (deleteEmployerHistory != null)
            {
                db.tbl_EmployerHistory.Remove(deleteEmployerHistory);
                db.SaveChanges();

                msg = "Removed Successfully";
            }
            db.Dispose();

            return msg;
        }

     
        public EmployerHistoryModel EditEmployerHistory(long HEIID)
        {
            EmployerHistoryModel model = new EmployerHistoryModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var editEmployerHistorydata = db.tbl_EmployerHistory.Where(co => co.HEIID == HEIID).FirstOrDefault();
            if (editEmployerHistorydata != null)
            {
                var countryName = db.tbl_Country.Where(co => co.ID == editEmployerHistorydata.Country).FirstOrDefault();
                var StateName = db.tbl_State.Where(co => co.ID == editEmployerHistorydata.State).FirstOrDefault();

                model.HEIID = editEmployerHistorydata.HEIID;
                model.EmployerName = !string.IsNullOrWhiteSpace(editEmployerHistorydata.EmployerName) ? editEmployerHistorydata.EmployerName : "";
                model.JobTitle = editEmployerHistorydata.JobTitle;
                model.JobType = editEmployerHistorydata.JobType;
                model.StartDate = editEmployerHistorydata.StartDate;
                model.TerminationDate = editEmployerHistorydata.TerminationDate;
                model.AnnualIncome = editEmployerHistorydata.AnnualIncome;
                model.AddAnnualIncome = editEmployerHistorydata.AddAnnualIncome;
                model.SupervisorName = !string.IsNullOrWhiteSpace(editEmployerHistorydata.SupervisorName) ? editEmployerHistorydata.SupervisorName : "";
                model.SupervisorPhone = editEmployerHistorydata.SupervisorPhone;
                model.SupervisorEmail = !string.IsNullOrWhiteSpace(editEmployerHistorydata.SupervisorEmail) ? editEmployerHistorydata.SupervisorEmail : "";
                model.Country = editEmployerHistorydata.Country;
                model.Address1 = !string.IsNullOrWhiteSpace(editEmployerHistorydata.Address1) ? editEmployerHistorydata.Address1 : "";
                model.Address2 = !string.IsNullOrWhiteSpace(editEmployerHistorydata.Address2) ? editEmployerHistorydata.Address2 : "";
                model.State = editEmployerHistorydata.State;
                model.City = !string.IsNullOrWhiteSpace(editEmployerHistorydata.City) ? editEmployerHistorydata.City : "";
                model.Zip = !string.IsNullOrWhiteSpace(editEmployerHistorydata.Zip) ? editEmployerHistorydata.Zip : "";
                model.StartDateString = editEmployerHistorydata.StartDate != null ? editEmployerHistorydata.StartDate.Value.ToString("MM/dd/yyyy") : "";
                model.TerminationDateString = editEmployerHistorydata.TerminationDate != null ? editEmployerHistorydata.TerminationDate.Value.ToString("MM/dd/yyyy") : "";
                model.TerminationReason = !string.IsNullOrWhiteSpace(editEmployerHistorydata.TerminationReason) ? editEmployerHistorydata.TerminationReason : "";
                model.CountryName = countryName != null ? countryName.CountryName : "";
                model.StateName = StateName != null ? StateName.StateName : "";
                model.JobTypeName = editEmployerHistorydata.JobType == 1 ? "Permanent" : editEmployerHistorydata.JobType == 2 ? "Contract Basis" : "";
            }
            return model;
        }

        public EmployerHistoryModel GetMonthsFromEmployerHistory(long TenantId, string EmpStartDate, string EmpTerminationDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EmployerHistoryModel model = new EmployerHistoryModel();

            try
            {
                int monthsCount = 0;
                string fromDateDB = string.Empty;
                string toDateDB = string.Empty;
                long ptoid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
                var empHist = db.tbl_EmployerHistory.Where(co => co.TenantId == TenantId && co.ParentTOID == ptoid).ToList();
                if (empHist != null)
                {
                    foreach (var item in empHist)
                    {
                        fromDateDB = Convert.ToString(item.StartDate.Value.ToString("MM/dd/yyyy"));
                        toDateDB = Convert.ToString(item.TerminationDate.Value.ToString("MM/dd/yyyy"));

                        DateTime dtFromDB = Convert.ToDateTime(fromDateDB);
                        DateTime dtToDB = Convert.ToDateTime(toDateDB);
                        int givenDate = ((dtToDB.Year - dtFromDB.Year) * 12) + dtToDB.Month - dtFromDB.Month;

                        monthsCount += givenDate+1;
                    }
                }

                DateTime dtCurrentFromDB = Convert.ToDateTime(EmpStartDate);
                DateTime dtCurrentToDB = Convert.ToDateTime(EmpTerminationDate);

                int givenCurrentDate = ((dtCurrentToDB.Year - dtCurrentFromDB.Year) * 12) + dtCurrentToDB.Month - dtCurrentFromDB.Month;
                monthsCount += givenCurrentDate+1;


                model.TotalMonthsEmployerHistory = monthsCount;

                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public EmployerHistoryModel GetPriousEmploymentInfo(int id)
        {
            EmployerHistoryModel model = new EmployerHistoryModel();
            ShomaRMEntities db = new ShomaRMEntities();
            long ptoid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var PriousEmploymentdata = db.tbl_EmployerHistory.Where(co => co.TenantId == id && co.ParentTOID == ptoid).OrderByDescending(s => s.HEIID).FirstOrDefault();

            if (PriousEmploymentdata != null)
            {
                var countryName = db.tbl_Country.Where(co => co.ID == PriousEmploymentdata.Country).FirstOrDefault();
                var StateName = db.tbl_State.Where(co => co.ID == PriousEmploymentdata.State).FirstOrDefault();

                model.HEIID = PriousEmploymentdata.HEIID;
                model.EmployerName = !string.IsNullOrWhiteSpace(PriousEmploymentdata.EmployerName) ? PriousEmploymentdata.EmployerName : "";
                model.JobTitle = !string.IsNullOrWhiteSpace(PriousEmploymentdata.JobTitle) ? PriousEmploymentdata.JobTitle : "";
                model.JobType = PriousEmploymentdata.JobType;
                model.StartDate = PriousEmploymentdata.StartDate;
                model.TerminationDate = PriousEmploymentdata.TerminationDate;
                model.AnnualIncome = PriousEmploymentdata.AnnualIncome;
                model.AddAnnualIncome = PriousEmploymentdata.AddAnnualIncome;
                model.SupervisorName = !string.IsNullOrWhiteSpace(PriousEmploymentdata.SupervisorName) ? PriousEmploymentdata.SupervisorName : "";
                model.SupervisorPhone = PriousEmploymentdata.SupervisorPhone;
                model.SupervisorEmail = !string.IsNullOrWhiteSpace(PriousEmploymentdata.SupervisorEmail) ? PriousEmploymentdata.SupervisorEmail : "";
                model.Country = PriousEmploymentdata.Country;
                model.Address1 = !string.IsNullOrWhiteSpace(PriousEmploymentdata.Address1) ? PriousEmploymentdata.Address1 : "";
                model.Address2 = !string.IsNullOrWhiteSpace(PriousEmploymentdata.Address2) ? PriousEmploymentdata.Address2 : "";
                model.State = PriousEmploymentdata.State;
                model.City = !string.IsNullOrWhiteSpace(PriousEmploymentdata.City) ? PriousEmploymentdata.City : "";
                model.Zip = !string.IsNullOrWhiteSpace(PriousEmploymentdata.Zip) ? PriousEmploymentdata.Zip : "";
                model.StartDateString = PriousEmploymentdata.StartDate != null ? PriousEmploymentdata.StartDate.Value.ToString("MM/dd/yyyy") : "";
                model.TerminationDateString = PriousEmploymentdata.TerminationDate != null ? PriousEmploymentdata.TerminationDate.Value.ToString("MM/dd/yyyy") : "";
                model.TerminationReason = !string.IsNullOrWhiteSpace(PriousEmploymentdata.TerminationReason) ? PriousEmploymentdata.TerminationReason : "";
                model.CountryName = countryName != null ? countryName.CountryName : "";
                model.StateName = StateName != null ? StateName.StateName : "";
                model.JobTypeName = PriousEmploymentdata.JobType == 1 ? "Permanent" : PriousEmploymentdata.JobType == 2 ? "Contract Basis" : "";
            }
            return model;
        }
    }
}