using ShomaRM.Data;
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

        public string saveUpdateEmployerHistory(EmployerHistoryModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
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
                    TaxReturn1 = model.TaxReturn1,
                    TaxReturn2 = model.TaxReturn2,
                    TaxReturn3 = model.TaxReturn3,
                    TaxReturn1OrigName = model.TaxReturn1OrigName,
                    TaxReturn2OrigName = model.TaxReturn2OrigName,
                    TaxReturn3OrigName = model.TaxReturn3OrigName,
                    PaystubValue = model.PaystubValue,
                    TenantId = model.TenantId
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
                    updateEmployerHistory.TaxReturn1 = model.TaxReturn1;
                    updateEmployerHistory.TaxReturn2 = model.TaxReturn2;
                    updateEmployerHistory.TaxReturn3 = model.TaxReturn3;
                    updateEmployerHistory.TaxReturn1OrigName = model.TaxReturn1OrigName;
                    updateEmployerHistory.TaxReturn2OrigName = model.TaxReturn2OrigName;
                    updateEmployerHistory.TaxReturn3OrigName = model.TaxReturn3OrigName;
                    updateEmployerHistory.PaystubValue = model.PaystubValue;
                    updateEmployerHistory.TenantId = model.TenantId;
                    db.SaveChanges();
                    msg = "Progress Updated";
                }
            }
            db.Dispose();
            return msg;
        }

        public EmployerHistoryModel SaveTaxUpload1HEI(HttpPostedFileBase fileBaseUpload1, EmployerHistoryModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EmployerHistoryModel HEImodel1 = new EmployerHistoryModel();
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
                HEImodel1.TaxReturn1 = sysFileName;
                HEImodel1.TaxReturn1OrigName = fileName;
            }
            return HEImodel1;
        }
        public EmployerHistoryModel SaveTaxUpload2HEI(HttpPostedFileBase fileBaseUpload2, EmployerHistoryModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EmployerHistoryModel HEImodel2 = new EmployerHistoryModel();
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
                HEImodel2.TaxReturn2 = sysFileName.ToString();
                HEImodel2.TaxReturn2OrigName = fileName;
            }
            return HEImodel2;
        }
        public EmployerHistoryModel SaveTaxUpload3HEI(HttpPostedFileBase fileBaseUpload3, EmployerHistoryModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EmployerHistoryModel HEImodel3 = new EmployerHistoryModel();
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
                HEImodel3.TaxReturn3 = sysFileName.ToString();
                HEImodel3.TaxReturn3OrigName = fileName;
            }
            return HEImodel3;
        }

        public List<EmployerHistoryModel> GetEmployerHistory(long TenantId)
        {
            List<EmployerHistoryModel> model = new List<EmployerHistoryModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getEmployerHistorydata = db.tbl_EmployerHistory.Where(co => co.TenantId == TenantId).ToList();
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
                        JobTitle = item.JobTitle,
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
                        TaxReturn1 = item.TaxReturn1,
                        TaxReturn2 = item.TaxReturn2,
                        TaxReturn3 = item.TaxReturn3,
                        TaxReturn1OrigName = item.TaxReturn1OrigName,
                        TaxReturn2OrigName = item.TaxReturn2OrigName,
                        TaxReturn3OrigName = item.TaxReturn3OrigName,
                        PaystubValue = item.PaystubValue,
                        CountryName = countryName != null ? countryName.CountryName : "",
                        StateName = StateName != null ? StateName.StateName : "",
                        JobTypeName = item.JobType == 1 ? "Permanent" : item.JobType == 2 ? "Contract Basis" : "",
                        StartDateString = item.StartDate!=null ? item.StartDate.Value.ToString("MM/dd/yyyy"):"",
                        TerminationDateString = item.TerminationDate!= null ? item.TerminationDate .Value.ToString("MM/dd/yyyy"):"",
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
    }
}