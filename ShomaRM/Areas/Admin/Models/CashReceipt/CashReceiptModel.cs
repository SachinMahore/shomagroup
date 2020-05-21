using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Models
{
    public class CashReceiptModel
    {
        public long CRID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string TenantIDString { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> PrePayment { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string PaymentDateString { get; set; }
        public string CheckNumber { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyIDString { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string UnitIDString { get; set; }
        public Nullable<long> LeaseID { get; set; }
        public Nullable<int> Revision_Num { get; set; }
        public string PaymentType { get; set; }
        public Nullable<int> IsApplicant { get; set; }
        public Nullable<int> Status { get; set; }
        public string Description { get; set; }
        public string Batch { get; set; }
        public Nullable<int> UserID { get; set; }
        public string BankAccount { get; set; }
        public Nullable<System.DateTime> DateStamp { get; set; }
        public string DateStampString { get; set; }
        public Nullable<System.DateTime> DepositAcctDate { get; set; }
        public string DepositAcctDateString { get; set; }

        public List<CashReceiptModel> GetCashReceiptList(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CashReceiptModel> lstpr = new List<CashReceiptModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCashReceiptList";
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
                    CashReceiptModel pr = new CashReceiptModel();

                    DateTime? paymentDate = null;
                    try
                    {
                        paymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? dateStampString = null;
                    try
                    {
                        dateStampString = Convert.ToDateTime(dr["DateStamp"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? depositAcctDateString = null;
                    try
                    {
                        depositAcctDateString = Convert.ToDateTime(dr["DepositAcctDate"].ToString());
                    }
                    catch
                    {
                    }
                    pr.CRID = Convert.ToInt32(dr["CRID"].ToString());
                    pr.PropertyIDString = dr["PropertyID"].ToString();
                    pr.UnitIDString = dr["UnitID"].ToString();
                    pr.TenantIDString = dr["TenantID"].ToString();
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Status = Convert.ToInt32(dr["Status"].ToString());
                    pr.Batch = dr["Batch"].ToString();
                    pr.Description = dr["Description"].ToString();
                    pr.Balance = Convert.ToDecimal(dr["Balance"].ToString());
                    pr.PrePayment = Convert.ToDecimal(dr["PrePayment"].ToString());
                    pr.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"].ToString());
                    pr.CheckNumber = dr["CheckNumber"].ToString();
                    pr.PaymentDateString = paymentDate == null ? "" : paymentDate.ToString();
                    pr.PaymentType = dr["PaymentType"].ToString();
                    pr.BankAccount = dr["BankAccount"].ToString();
                    pr.DateStampString = dateStampString == null ? "" : dateStampString.ToString();
                    pr.IsApplicant = Convert.ToInt32(dr["IsApplicant"].ToString());
                    pr.DepositAcctDateString = depositAcctDateString == null ? "" : depositAcctDateString.ToString();
                    lstpr.Add(pr);
                }
                db.Dispose();
                return lstpr.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public CashReceiptModel GetCashReceiptDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CashReceiptModel model = new CashReceiptModel();
            var getCashRdata = db.tbl_CashReceipts.Where(p => p.CRID == id).FirstOrDefault();
            if (getCashRdata != null)
            {
                model.PropertyID = getCashRdata.PropertyID;
                model.UnitID = getCashRdata.UnitID;
                model.TenantID = getCashRdata.TenantID;
                model.Revision_Num = getCashRdata.Revision_Num;
                model.Status = getCashRdata.Status;
                model.Batch = getCashRdata.Batch;
                model.Description = getCashRdata.Description;
                model.Balance = getCashRdata.Balance;
                model.PrePayment = getCashRdata.PrePayment;
                model.PaymentAmount = getCashRdata.PaymentAmount;
                model.CheckNumber = getCashRdata.CheckNumber;
                model.PaymentDate = getCashRdata.PaymentDate;
                model.PaymentType = getCashRdata.PaymentType;
                model.BankAccount = getCashRdata.BankAccount;
                model.DateStamp = getCashRdata.DateStamp;
                model.IsApplicant = getCashRdata.IsApplicant;
                model.DepositAcctDate = getCashRdata.DepositAcctDate;
            }
            model.CRID = id;
            return model;
        }
        public string SaveUpdateCashReceipt(CashReceiptModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.CRID == 0)
            {
                var saveCashReceipt = new tbl_CashReceipts()
                {
                    PropertyID = model.PropertyID,
                    UnitID = model.UnitID,
                    TenantID = model.TenantID,
                    Revision_Num = model.Revision_Num,
                    Status = model.Status,
                    Batch = model.Batch,
                    Description = model.Description,
                    Balance = model.Balance,
                    PrePayment = model.PrePayment,
                    PaymentAmount = model.PaymentAmount,
                    CheckNumber = model.CheckNumber,
                    PaymentDate = model.PaymentDate,
                    PaymentType = model.PaymentType,
                    BankAccount = model.BankAccount,
                    DateStamp = model.DateStamp,
                    IsApplicant = model.IsApplicant,
                    DepositAcctDate = model.DepositAcctDate,
                    UserID = userid,
                };
                db.tbl_CashReceipts.Add(saveCashReceipt);
                db.SaveChanges();
                msg = "Cash Receipt Saved Successfully";
            }
            else
            {
                var getNOdata = db.tbl_CashReceipts.Where(p => p.CRID == model.CRID).FirstOrDefault();
                if (getNOdata != null)
                {
                    getNOdata.PropertyID = model.PropertyID;
                    getNOdata.PropertyID = model.PropertyID;
                    getNOdata.UnitID = model.UnitID;
                    getNOdata.TenantID = model.TenantID;
                    getNOdata.Revision_Num = model.Revision_Num;
                    getNOdata.Status = model.Status;
                    getNOdata.Batch = model.Batch;
                    getNOdata.Description = model.Description;
                    getNOdata.Balance = model.Balance;
                    getNOdata.PrePayment = model.PrePayment;
                    getNOdata.PaymentAmount = model.PaymentAmount;
                    getNOdata.CheckNumber = model.CheckNumber;
                    getNOdata.PaymentDate = model.PaymentDate;
                    getNOdata.PaymentType = model.PaymentType;
                    getNOdata.BankAccount = model.BankAccount;
                    getNOdata.DateStamp = model.DateStamp;
                    getNOdata.IsApplicant = model.IsApplicant;
                    getNOdata.DepositAcctDate = model.DepositAcctDate;
                }
                db.SaveChanges();
                msg = "Cash Receipt Updated Successfully";
            }
            db.Dispose();
            return msg;
        }
        public class CashReceiptSearchModel
        {
            public long CRID { get; set; }
            public string PropertyTitle { get; set; }
            public string UnitName { get; set; }
            public string TenantName { get; set; }
            public string Revision_Num { get; set; }
            public string Status { get; set; }
            public string Batch { get; set; }
            public string Description { get; set; }
            public decimal Balance { get; set; }
            public decimal PrePayment { get; set; }
            public decimal PaymentAmount { get; set; }
            public string CheckNumber { get; set; }
            public string PaymentDate { get; set; }
            public string PaymentType { get; set; }
            public string BankAccount { get; set; }
            public string DateStamp { get; set; }
            public string IsApplicant { get; set; }
            public string DepositAcctDate { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
        public int BuildPaganationCashReceiptList(CashReceiptSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCashReceiptPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
                    cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

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
        public List<CashReceiptSearchModel> FillCashReceiptSearchGrid(CashReceiptSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CashReceiptSearchModel> lstCashReceipt = new List<CashReceiptSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCashReceiptPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate; 
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
                    cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    CashReceiptSearchModel searchmodel = new CashReceiptSearchModel();
                    searchmodel.CRID = Convert.ToInt32(dr["CRID"].ToString());
                    searchmodel.PropertyTitle = dr["PropertyID"].ToString();
                    searchmodel.UnitName = dr["UnitID"].ToString();
                    searchmodel.TenantName = dr["TenantID"].ToString();
                    searchmodel.Revision_Num =dr["Revision_Num"].ToString();
                    searchmodel.Status = dr["Status"].ToString();
                    searchmodel.Batch = dr["Batch"].ToString();
                    searchmodel.Description = dr["Description"].ToString();
                    searchmodel.Balance = Convert.ToDecimal(dr["Balance"].ToString());
                    searchmodel.PrePayment = Convert.ToDecimal(dr["PrePayment"].ToString());
                    searchmodel.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"].ToString());
                    searchmodel.CheckNumber = dr["CheckNumber"].ToString();
                    searchmodel.PaymentDate = dr["PaymentDate"].ToString(); 
                    searchmodel.PaymentType = dr["PaymentType"].ToString();
                    searchmodel.BankAccount = dr["BankAccount"].ToString();
                    searchmodel.DateStamp = dr["DateStamp"].ToString();
                    searchmodel.DepositAcctDate= dr["DepositAcctDate"].ToString();
                    lstCashReceipt.Add(searchmodel);
                }
                db.Dispose();
                return lstCashReceipt.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}