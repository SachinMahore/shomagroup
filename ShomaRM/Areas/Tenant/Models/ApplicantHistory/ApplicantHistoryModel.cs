using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Models
{
    public class ApplicantHistoryModel
    {

        public long AHID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string Country { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public Nullable<long> StateHome { get; set; }
        public string StateHomeTxt { get; set; }

        public string CityHome { get; set; }
        public string ZipHome { get; set; }
        public Nullable<int> RentOwn { get; set; }
        public Nullable<System.DateTime> MoveInDateFrom { get; set; }
        public Nullable<System.DateTime> MoveInDateTo { get; set; }
        public string MonthlyPayment { get; set; }
        public string Reason { get; set; }

        public string MoveInDateFromTxt { get; set; }
        public string MoveInDateToTxt { get; set; }
        public string StateString { get; set; }
        public string CountryString { get; set; }
        public string RentOwnString { get; set; }
        public int? MonthsApplicantHistory { get; set; }
        public int? TotalMonthsApplicantHistory { get; set; }

        public string SaveUpdateApplicantHistory(ApplicantHistoryModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.AHID == 0)
            {
                var saveApplicantHistory = new tbl_ApplicantHistory()
                {
                    Country = model.Country,
                    HomeAddress1 = model.HomeAddress1,
                    HomeAddress2 = model.HomeAddress2,
                    StateHome = model.StateHome,
                    CityHome = model.CityHome,
                    ZipHome = model.ZipHome,
                    RentOwn = model.RentOwn,
                    MoveInDateFrom = model.MoveInDateFrom,
                    MoveInDateTo = model.MoveInDateTo,
                    MonthlyPayment = model.MonthlyPayment,
                    Reason = model.Reason,
                    TenantID = model.TenantID
                };
                db.tbl_ApplicantHistory.Add(saveApplicantHistory);
                db.SaveChanges();


                msg = "Applicant History Saved Successfully";
            }
            else
            {
                var getApplicantHistorydata = db.tbl_ApplicantHistory.Where(p => p.AHID == model.AHID).FirstOrDefault();
                if (getApplicantHistorydata != null)
                {
                    getApplicantHistorydata.Country = model.Country;
                    getApplicantHistorydata.HomeAddress1 = model.HomeAddress1;
                    getApplicantHistorydata.HomeAddress2 = model.HomeAddress2;
                    getApplicantHistorydata.StateHome = model.StateHome;
                    getApplicantHistorydata.CityHome = model.CityHome;
                    getApplicantHistorydata.ZipHome = model.ZipHome;
                    getApplicantHistorydata.RentOwn = model.RentOwn;
                    getApplicantHistorydata.MoveInDateFrom = model.MoveInDateFrom;
                    getApplicantHistorydata.MoveInDateTo = model.MoveInDateTo;
                    getApplicantHistorydata.MonthlyPayment = model.MonthlyPayment;
                    getApplicantHistorydata.Reason = model.Reason;
                }
                db.SaveChanges();
                msg = "Applicant History Updated Successfully";
            }

            db.Dispose();
            return msg;


        }

        public List<ApplicantHistoryModel> GetApplicantHistoryListOLD(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplicantHistoryModel> lstProp = new List<ApplicantHistoryModel>();

            var applicantHistoryList = db.tbl_ApplicantHistory.Where(p => p.TenantID == TenantID).ToList();
            foreach (var ahl in applicantHistoryList)
            {
                DateTime? moveInFrom = null;
                try

                {

                    moveInFrom = Convert.ToDateTime(ahl.MoveInDateFrom);
                }
                catch
                {

                }
                DateTime? moveInTo = null;
                try
                {

                    moveInTo = Convert.ToDateTime(ahl.MoveInDateTo);
                }
                catch
                {

                }
                lstProp.Add(new ApplicantHistoryModel
                {

                    AHID = ahl.AHID,
                    Country = ahl.Country,
                    HomeAddress1 = ahl.HomeAddress1,
                    HomeAddress2 = ahl.HomeAddress2,
                    StateHome = ahl.StateHome,
                    CityHome = ahl.CityHome,
                    ZipHome = ahl.ZipHome,
                    RentOwn = ahl.RentOwn,
                    MoveInDateFromTxt = moveInFrom == null ? "" : moveInFrom.Value.ToString("MM/dd/yyy"),
                    MoveInDateToTxt = moveInFrom == null ? "" : moveInFrom.Value.ToString("MM/dd/yyy"),
                    MonthlyPayment = ahl.MonthlyPayment,
                    Reason = ahl.Reason
                });

            }
            return lstProp;
        }

        public List<ApplicantHistoryModel> GetApplicantHistoryList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ApplicantHistoryModel> lstProp = new List<ApplicantHistoryModel>();

            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_ApplicantHistoryList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "TenantID";
                    paramF.Value = TenantID;
                    cmd.Parameters.Add(paramF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    DateTime? moveInFrom = null;
                    try

                    {

                        moveInFrom = Convert.ToDateTime(dr["MoveInDateFrom"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveInTo = null;
                    try
                    {

                        moveInTo = Convert.ToDateTime(dr["MoveInDateTo"].ToString());
                    }
                    catch
                    {

                    }

                    lstProp.Add(new ApplicantHistoryModel
                    {
                        AHID = Convert.ToInt64(dr["AHID"].ToString()),
                        Country = dr["Country"].ToString(),
                        HomeAddress1 = dr["HomeAddress1"].ToString(),
                        HomeAddress2 = dr["HomeAddress2"].ToString(),
                        StateHomeTxt = dr["StateHome"].ToString(),
                        CityHome = dr["CityHome"].ToString(),
                        ZipHome = dr["ZipHome"].ToString(),
                        RentOwn = Convert.ToInt32(dr["RentOwn"].ToString()),
                        MoveInDateFromTxt = moveInFrom == null ? "" : moveInFrom.Value.ToString("MM/dd/yyy"),
                        MoveInDateToTxt = moveInTo == null ? "" : moveInTo.Value.ToString("MM/dd/yyy"),
                        MonthlyPayment = dr["MonthlyPayment"].ToString(),
                        Reason = dr["Reason"].ToString()
                    });
                }
                db.Dispose();
                return lstProp;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public ApplicantHistoryModel GetApplicantHistoryDetails(long AHID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantHistoryModel model = new ApplicantHistoryModel();

            var getAHRdata = db.tbl_ApplicantHistory.Where(p => p.AHID == AHID).FirstOrDefault();
            if (getAHRdata != null)
            {
                DateTime? moveInFrom = null;
                try
                {

                    moveInFrom = Convert.ToDateTime(getAHRdata.MoveInDateFrom);
                }
                catch
                {

                }
                DateTime? moveInTo = null;
                try
                {

                    moveInTo = Convert.ToDateTime(getAHRdata.MoveInDateTo);
                }
                catch
                {

                }
                model.Country = getAHRdata.Country;
                model.HomeAddress1 = getAHRdata.HomeAddress1;
                model.HomeAddress2 = getAHRdata.HomeAddress2;
                model.StateHome = getAHRdata.StateHome;
                model.CityHome = getAHRdata.CityHome;
                model.ZipHome = getAHRdata.ZipHome;
                model.RentOwn = getAHRdata.RentOwn;
                model.MoveInDateFromTxt = moveInFrom == null ? "" : moveInFrom.Value.ToString("MM/dd/yyy");
                model.MoveInDateToTxt = moveInTo == null ? "" : moveInTo.Value.ToString("MM/dd/yyy");
                model.MonthlyPayment = getAHRdata.MonthlyPayment;
                model.Reason = getAHRdata.Reason;
                var stateStr = db.tbl_State.Where(co => co.ID == getAHRdata.StateHome).FirstOrDefault();
                model.StateString = stateStr.StateName;
                int ctryString = Convert.ToInt32(model.Country);
                var countryStr = db.tbl_Country.Where(co => co.ID == ctryString).FirstOrDefault();
                model.CountryString = countryStr.CountryName;
                model.RentOwnString = model.RentOwn == 1 ? "Rent" : model.RentOwn == 2 ? "Own" : "";
            }
            model.AHID = AHID;

            return model;
        }

        public string DeleteApplicantHistory(long AHID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (AHID != 0)
            {

                var ApplicantHistoryData = db.tbl_ApplicantHistory.Where(p => p.AHID == AHID).FirstOrDefault();
                if (ApplicantHistoryData != null)
                {
                    db.tbl_ApplicantHistory.Remove(ApplicantHistoryData);
                    db.SaveChanges();
                    msg = "Applicant History Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public ApplicantHistoryModel GetMonthsFromApplicantHistory(long TenantId, string FromDateAppHis, string ToDateAppHis)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ApplicantHistoryModel model = new ApplicantHistoryModel();

            try
            {
                int monthsCount = 0;
                string fromDateDB = string.Empty;
                string toDateDB = string.Empty;
                var moveInApplHist = db.tbl_ApplicantHistory.Where(co => co.TenantID == TenantId).OrderBy(co => co.MoveInDateFrom).ToList();
                if (moveInApplHist != null)
                {
                    foreach (var item in moveInApplHist)
                    {
                        fromDateDB = Convert.ToString(item.MoveInDateFrom);
                        break;
                    }
                }
                var moveOutApplHist = db.tbl_ApplicantHistory.Where(co => co.TenantID == TenantId).OrderByDescending(co => co.MoveInDateTo).ToList();
                if (moveOutApplHist != null)
                {
                    foreach (var item in moveOutApplHist)
                    {
                        toDateDB = Convert.ToString(item.MoveInDateTo);
                        break;
                    }
                }
                if (fromDateDB != string.Empty && toDateDB != string.Empty)
                {
                    DateTime dt1FromDB = Convert.ToDateTime(fromDateDB);
                    DateTime dt2ToDB = Convert.ToDateTime(toDateDB);
                    if (FromDateAppHis != string.Empty && ToDateAppHis != string.Empty)
                    {
                        DateTime dt1Form = Convert.ToDateTime(FromDateAppHis);
                        DateTime dt2Form = Convert.ToDateTime(ToDateAppHis);
                        bool fDate = dt1FromDB < dt1Form;
                        bool tDate = dt2ToDB > dt2Form;

                        DateTime finalDt1 = Convert.ToDateTime(fDate == true ? dt1FromDB : dt1Form);
                        DateTime finalDt2 = Convert.ToDateTime(tDate == true ? dt2ToDB : dt2Form);

                        int givenDate = ((finalDt2.Year - finalDt1.Year) * 12) + finalDt2.Month - finalDt1.Month;
                        monthsCount = monthsCount + givenDate;
                    }
                }
                else if (FromDateAppHis != string.Empty && ToDateAppHis != string.Empty)
                {
                    DateTime dt1 = Convert.ToDateTime(FromDateAppHis);
                    DateTime dt2 = Convert.ToDateTime(ToDateAppHis);
                    int givenDate = ((dt2.Year - dt1.Year) * 12) + dt2.Month - dt1.Month;
                    monthsCount = monthsCount + givenDate;
                }
                model.TotalMonthsApplicantHistory = monthsCount;

                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

    }
}