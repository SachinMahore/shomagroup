using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Models
{
    public class TransactionModel
    {
        public int TransID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyIDString { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string UnitIDString { get; set; }
        public long TenantID { get; set; }
        public long ProspectID { get; set; }
        public string TenantIDString { get; set; }
        public long LeaseID { get; set; }
        public int Revision_Num { get; set; }
        public System.DateTime Transaction_Date { get; set; }
        public string Transaction_DateString { get; set; }
        public int Run { get; set; }
        public Nullable<int> Tax_Sequence { get; set; }
        public string Transaction_Type { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Charge_Date { get; set; }
        public string Charge_DateString { get; set; }
        public string Charge_Type { get; set; }
        public string Summary_Charge_Type { get; set; }
        public Nullable<int> Payment_ID { get; set; }
        public string Reference { get; set; }
        public Nullable<decimal> Charge_Amount { get; set; }
        public Nullable<decimal> Credit_Amount { get; set; }
        public Nullable<decimal> Miscellaneous_Amount { get; set; }
        public Nullable<System.DateTime> Accounting_Date { get; set; }
        public string Accounting_DateString { get; set; }
        public Nullable<int> Journal { get; set; }
        public string Accrual_Debit_Acct { get; set; }
        public string Accrual_Credit_Acct { get; set; }
        public string Cash_Debit_Account { get; set; }
        public string Cash_Credit_Account { get; set; }
        public string Appl_of_Origin { get; set; }
        public string Batch { get; set; }
        public string Batch_Source { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedByText { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public string GL_Trans_Description { get; set; }
        public string GL_Trans_Reference_1 { get; set; }
        public string GL_Trans_Reference_2 { get; set; }
        public int GL_Entries_Created { get; set; }
        public string TBankName { get; set; }
        public string TAccCardName { get; set; }
        public string TAccCardNumber { get; set; }
        public string TRoutingNumber { get; set; }
        public string TSecurityNumber { get; set; }
        public string TCardExpirationMonth { get; set; }
        public string TCardExpirationYear { get; set; }
        public string TransactionDateString { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantType { get; set; }

        public int BuildPaganationTransactionList(TransactionSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTransactionPaginationAndSearchData";
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
        public List<TransactionSearchModel> GetTransactionList(TransactionSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TransactionSearchModel> lstTransaction = new List<TransactionSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTransactionPaginationAndSearchData";
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
                    TransactionSearchModel searchmodel = new TransactionSearchModel();
                    DateTime? dtTransDate = null;
                    DateTime? dtCreatedDate = null;
                    DateTime? dtChargeDate = null;
                    try
                    {
                        dtTransDate = Convert.ToDateTime(dr["Transaction_Date"].ToString());
                    }
                    catch
                    {
                    }
                    try
                    {
                        dtCreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    }
                    catch
                    {
                    }
                    try
                    {
                        dtChargeDate = Convert.ToDateTime(dr["Charge_Date"].ToString());
                    }
                    catch
                    {
                    }
                    DateTime? accounting_DateString = null;
                    try
                    {
                        accounting_DateString = Convert.ToDateTime(dr["Accounting_Date"].ToString());
                    }
                    catch
                    {
                    }
                    searchmodel.TransID = Convert.ToInt32(dr["TransID"].ToString());
                    searchmodel.TenantIDString = dr["TenantID"].ToString();
                    searchmodel.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    searchmodel.Transaction_Type = dr["Transaction_Type"].ToString();
                    searchmodel.Run = Convert.ToInt32(dr["Run"].ToString());
                    searchmodel.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    searchmodel.Reference = dr["Reference"].ToString();
                    searchmodel.TransactionDate = dtTransDate.HasValue ? dtTransDate.Value.ToString("MM/dd/yyyy") : "";
                    searchmodel.CreatedDate = dtCreatedDate.HasValue ? dtCreatedDate.Value.ToString("MM/dd/yyyy") : "";
                    searchmodel.ChargeDate = dtChargeDate.HasValue ? dtChargeDate.Value.ToString("MM/dd/yyyy") : "";
                    searchmodel.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    searchmodel.Description = dr["Description"].ToString();
                    searchmodel.Charge_Type = dr["Charge_Type"].ToString();
                    searchmodel.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    searchmodel.CreatedByText = dr["CreatedByText"].ToString();
                    lstTransaction.Add(searchmodel);
                }
                db.Dispose();
                return lstTransaction.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<TransactionModel> GetTenantTransactionList(DateTime FromDate, DateTime ToDate, long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TransactionModel> lstpr = new List<TransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantTransactionList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TransactionModel pr = new TransactionModel();

                    DateTime? transactiondateString = null;
                    try
                    {
                        transactiondateString = Convert.ToDateTime(dr["Transaction_Date"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? createdDateString = null;
                    try
                    {
                        createdDateString = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    }
                    catch
                    {

                    }

                    DateTime? charge_DateString = null;
                    try
                    {
                        charge_DateString = Convert.ToDateTime(dr["Charge_Date"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? accounting_DateString = null;
                    try
                    {
                        accounting_DateString = Convert.ToDateTime(dr["Accounting_Date"].ToString());
                    }
                    catch
                    {

                    }
                    pr.TransID = Convert.ToInt32(dr["TransID"].ToString());

                    pr.TenantIDString = dr["TenantID"].ToString();
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Transaction_Type = dr["Transaction_Type"].ToString();
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();
                    pr.Run = Convert.ToInt32(dr["Run"].ToString());
                    pr.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    pr.Reference = dr["Reference"].ToString();
                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
                    pr.Charge_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.CreatedByText = dr["CreatedByText"].ToString();

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
        public List<TransactionModel> GetProspectTransactionList(long ProspectID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TransactionModel> lstpr = new List<TransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectTransactionList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "ProspectID";
                    paramTID.Value = ProspectID;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TransactionModel pr = new TransactionModel();

                    DateTime? transactiondateString = null;
                    try
                    {
                        transactiondateString = Convert.ToDateTime(dr["Transaction_Date"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? createdDateString = null;
                    try
                    {
                        createdDateString = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    }
                    catch
                    {

                    }

                    DateTime? charge_DateString = null;
                    try
                    {
                        charge_DateString = Convert.ToDateTime(dr["Charge_Date"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? accounting_DateString = null;
                    try
                    {
                        accounting_DateString = Convert.ToDateTime(dr["Accounting_Date"].ToString());
                    }
                    catch
                    {

                    }
                    pr.TransID = Convert.ToInt32(dr["TransID"].ToString());

                    // pr.TenantIDString = dr["TenantID"].ToString();
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Transaction_Type = dr["Transaction_Type"].ToString();
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();
                    pr.Run = Convert.ToInt32(dr["Run"].ToString());
                    pr.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    pr.Reference = dr["Reference"].ToString();
                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    pr.TBankName = dr["TBankName"].ToString();

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
        public List<TransactionModel> GetOnlineTransactionList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TransactionModel> lstpr = new List<TransactionModel>();

            int userid= ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var appData = db.tbl_Applicant.Where(p => p.UserID == userid).FirstOrDefault();
            long appid = 0;
            if(appData!=null)
            {
                appid = appData.ApplicantID;
            }

            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetOnlineTransactionList";
                    cmd.CommandType = CommandType.StoredProcedure;


                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramAID = cmd.CreateParameter();
                    paramAID.ParameterName = "AppID";
                    paramAID.Value = appid;
                    cmd.Parameters.Add(paramAID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TransactionModel pr = new TransactionModel();

                    DateTime? transactiondateString = null;
                    try
                    {
                        transactiondateString = Convert.ToDateTime(dr["Transaction_Date"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? createdDateString = null;
                    try
                    {
                        createdDateString = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    }
                    catch
                    {

                    }

                    DateTime? charge_DateString = null;
                    try
                    {
                        charge_DateString = Convert.ToDateTime(dr["Charge_Date"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? accounting_DateString = null;
                    try
                    {
                        accounting_DateString = Convert.ToDateTime(dr["Accounting_Date"].ToString());
                    }
                    catch
                    {

                    }
                    pr.TransID = Convert.ToInt32(dr["TransID"].ToString());

                    // pr.TenantIDString = dr["TenantID"].ToString();
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Transaction_Type = dr["Transaction_Type"].ToString();
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();
                    pr.Run = Convert.ToInt32(dr["Run"].ToString());
                    pr.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    pr.Reference = dr["Reference"].ToString();
                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    pr.TBankName = dr["TBankName"].ToString();
                    pr.ApplicantName =dr["ApplicantName"].ToString();
                    pr.ApplicantType = dr["ApplicantType"].ToString();

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
        
        public TransactionModel GetTransactionDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            TransactionModel model = new TransactionModel();
            model.Accounting_Date = DateTime.Now;
            model.CreatedDate = DateTime.Now;
            model.Charge_Date = DateTime.Now;
            model.Transaction_Date = DateTime.Now;
            model.Charge_Amount = 0;
            model.Credit_Amount = 0;
            model.Miscellaneous_Amount = 0;
            model.Batch_Source = "";
            model.Tax_Sequence = 0;
            model.Batch = "";

            var getTransdata = db.tbl_Transaction.Where(p => p.TransID == id).FirstOrDefault();
            if (getTransdata != null)
            {

              
                model.TenantID = getTransdata.TenantID;
                model.Revision_Num = getTransdata.Revision_Num;
                model.Transaction_Type = getTransdata.Transaction_Type;
                model.Transaction_Date = getTransdata.Transaction_Date;
                model.Run = getTransdata.Run;
                model.LeaseID = getTransdata.LeaseID;
                model.Reference = getTransdata.Reference;
                model.CreatedDate = getTransdata.CreatedDate;
                model.Credit_Amount = getTransdata.Credit_Amount;
                model.Description = getTransdata.Description;
                model.Charge_Date = getTransdata.Charge_Date;
                model.Charge_Type = getTransdata.Charge_Type.ToString();
                model.Payment_ID = getTransdata.Payment_ID;
                model.Charge_Amount = getTransdata.Charge_Amount;
                model.Miscellaneous_Amount = getTransdata.Miscellaneous_Amount;
                model.Accounting_Date = getTransdata.Accounting_Date;
                model.Journal = getTransdata.Journal;
                model.Accrual_Debit_Acct = getTransdata.Accrual_Debit_Acct;
                model.Accrual_Credit_Acct = getTransdata.Accrual_Credit_Acct;
                model.Cash_Debit_Account = getTransdata.Cash_Debit_Account;
                model.Cash_Credit_Account = getTransdata.Cash_Credit_Account;
                model.Appl_of_Origin = getTransdata.Appl_of_Origin;
                model.Batch = getTransdata.Batch;
                model.Batch_Source = getTransdata.Batch_Source;
                model.CreatedBy = getTransdata.CreatedBy;
                model.GL_Trans_Reference_1 = getTransdata.GL_Trans_Reference_1;
                model.GL_Trans_Reference_2 = getTransdata.GL_Trans_Reference_1;
                //model.GL_Entries_Created = getTransdata.GL_Entries_Created;
                model.GL_Trans_Description = getTransdata.GL_Trans_Description;

            }
            model.TransID = id;

            return model;
        }
        public string SaveUpdateTransaction(TransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            string fullname= ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.FullName : "";
            if (model.TransID == 0)
            {
                var saveTransaction = new tbl_Transaction()
                {
                   
                    TenantID = 0,
                    Revision_Num = model.Revision_Num,
                    Transaction_Type = model.Transaction_Type,
                    Transaction_Date = DateTime.Now,
                    Run = 1,
                    LeaseID = 0,
                    Reference = model.Reference,
                    CreatedDate = DateTime.Now,
                    Credit_Amount = 0,
                    Description = model.Description,
                    Charge_Date = model.Charge_Date,
                    Charge_Type = Convert.ToInt32(model.Charge_Type),
                    Payment_ID = 13322244,
                    Charge_Amount = model.Charge_Amount,
                    Miscellaneous_Amount = model.Miscellaneous_Amount,
                    Accounting_Date = DateTime.Now,
                    Journal = 0,
                    Accrual_Debit_Acct = "400-5000-10500",
                    Accrual_Credit_Acct = "400-5000-40030",
                    Cash_Debit_Account = "400-5100-10011",
                    Cash_Credit_Account = "400-5100-40085",
                    Appl_of_Origin = "SRM",
                    Batch = "1",
                    Batch_Source = "",
                    CreatedBy = userid,
                    GL_Trans_Reference_1 = model.PropertyID.ToString(),
                    GL_Trans_Reference_2 = fullname,
                    GL_Entries_Created = 1,
                    GL_Trans_Description = model.Description,
                    ProspectID = model.ProspectID,
                    //TAccCardName = model.TAccCardName,
                    //TAccCardNumber = model.TAccCardNumber,
                    //TBankName = model.TBankName,
                    //TRoutingNumber = model.TRoutingNumber,
                    //TCardExpirationMonth = model.TCardExpirationMonth,
                    //TCardExpirationYear = model.TCardExpirationYear,
                    //TSecurityNumber = model.TSecurityNumber,

                };
                db.tbl_Transaction.Add(saveTransaction);
                db.SaveChanges();


                msg = "Transaction Saved Successfully";
            }
            else
            {
                var getNOdata = db.tbl_Transaction.Where(p => p.TransID == model.TransID).FirstOrDefault();
                if (getNOdata != null)
                {

                   
                    getNOdata.TenantID = model.TenantID;
                    getNOdata.Revision_Num = model.Revision_Num;
                    getNOdata.Transaction_Type = model.Transaction_Type;
                    getNOdata.Transaction_Date = model.Transaction_Date;
                    getNOdata.Run = model.Run;
                    getNOdata.LeaseID = model.LeaseID;
                    getNOdata.Reference = model.Reference;
                    getNOdata.CreatedDate = model.CreatedDate;
                    getNOdata.Credit_Amount = model.Credit_Amount;
                    getNOdata.Description = model.Description;
                    getNOdata.Charge_Date = model.Charge_Date;
                    getNOdata.Charge_Type = Convert.ToInt32(model.Charge_Type);
                    getNOdata.Payment_ID = model.Payment_ID;
                    getNOdata.Charge_Amount = model.Charge_Amount;
                    getNOdata.Miscellaneous_Amount = model.Miscellaneous_Amount;
                    getNOdata.Accounting_Date = model.Accounting_Date;
                    getNOdata.Journal = model.Journal;
                    getNOdata.Accrual_Debit_Acct = model.Accrual_Debit_Acct;
                    getNOdata.Accrual_Credit_Acct = model.Accrual_Credit_Acct;
                    getNOdata.Cash_Debit_Account = model.Cash_Debit_Account;
                    getNOdata.Cash_Credit_Account = model.Cash_Credit_Account;
                    getNOdata.Appl_of_Origin = model.Appl_of_Origin;
                    getNOdata.Batch = model.Batch;
                    getNOdata.Batch_Source = model.Batch_Source;
                    // getNOdata.CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID;
                    getNOdata.GL_Trans_Reference_1 = model.GL_Trans_Reference_1;
                    getNOdata.GL_Trans_Reference_2 = model.GL_Trans_Reference_2;
                    getNOdata.GL_Entries_Created = model.GL_Entries_Created;
                    getNOdata.GL_Trans_Description = model.GL_Trans_Description;
                    getNOdata.TAccCardName = model.TAccCardName;
                    getNOdata.TAccCardNumber = model.TAccCardNumber;
                    getNOdata.TBankName = model.TBankName;
                    getNOdata.TRoutingNumber = model.TRoutingNumber;
                    getNOdata.TCardExpirationMonth = model.TCardExpirationMonth;
                    getNOdata.TCardExpirationYear = model.TCardExpirationYear;
                    getNOdata.TSecurityNumber = model.TSecurityNumber;
                }
                db.SaveChanges();
                msg = "Transaction Updated Successfully";
            }

            db.Dispose();
            return msg;
        }
        public List<TransactionModel> GetAccountHistory(long TenantId)
        {
            List<TransactionModel> listAccountHistory = new List<TransactionModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getAccountHistory = db.tbl_Transaction.Where(co => co.TenantID == TenantId).OrderByDescending(co => co.TransID).ToList();

            if (getAccountHistory!=null)
            {
                foreach (var item in getAccountHistory)
                {
                    listAccountHistory.Add(new TransactionModel()
                    {
                        TransID = item.TransID,
                        Transaction_Date = item.Transaction_Date,
                        Description = item.Description,
                        Charge_Amount = item.Charge_Amount,
                        TransactionDateString = item.Transaction_Date.ToString("MM/dd/yyyy")
                    });
                }
            }
            return listAccountHistory;
        }

        public class TransactionSearchModel
        {
            public long TransID { get; set; }
            public string TenantIDString { get; set; }
            public int Revision_Num { get; set; }
            public string Transaction_Type { get; set; }
            public string TransactionDate { get; set; }
            public int Run { get; set; }
            public int LeaseID { get; set; }
            public string Reference { get; set; }
            public string CreatedDate { get; set; }
            public decimal Credit_Amount { get; set; }
            public string Description { get; set; }
            public string ChargeDate { get; set; }
            public string Charge_Type { get; set; }
            public decimal Charge_Amount { get; set; }
            public string CreatedByText { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
    }
}