using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Areas.Tenant.Models;
using System.Data;
using System.Data.Common;
using ShomaRM.Models;

namespace ShomaRM.Areas.Tenant.Models
{
    public class MyTransactionModel
    {
        public int TransID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyIDString { get; set; }
        public Nullable<long> UnitID { get; set; }
        public string UnitIDString { get; set; }
        public long TenantID { get; set; }
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
        public Nullable<decimal> Balance { get; set; }
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
        public Nullable<long> ProspectID { get; set; }
        public decimal? CurrentUpcomingCharges { get; set; }
        public string CurrentUpcomingChargesString { get; set; }

        public string NameOnCardString { get; set; }
        public string NumberOnCardString { get; set; }
        public string ExpirationMonthOnCardString { get; set; }
        public string ExpirationYearOnCardString { get; set; }
        public string BankName { get; set; }
        public string RoutingNumber { get; set; }

        public List<MyTransactionModel> GetTenantTransactionList(long TenantID, int AccountHistoryDDL)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyTransactionModel> lstpr = new List<MyTransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantTransactionList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramAccountDDLValue = cmd.CreateParameter();
                    paramAccountDDLValue.ParameterName = "AccountDDL";
                    paramAccountDDLValue.Value = AccountHistoryDDL;
                    cmd.Parameters.Add(paramAccountDDLValue);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    MyTransactionModel pr = new MyTransactionModel();

                    DateTime? transactiondateString = null;
                    try
                    {
                        transactiondateString = Convert.ToDateTime(dr["Transaction_Date"].ToString());
                    }
                    catch
                    {

                    }
                    
                    
                    pr.TransID = Convert.ToInt32(dr["TransID"].ToString());
                    
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.Value.ToString("MM/dd/yyyy");
                    pr.Description = dr["Description"].ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    
                    //pr.Balance = Convert.ToDecimal(dr["Balance"].ToString());
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                   
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
        public string GetTotalDue(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MyTransactionModel lstpr = new MyTransactionModel();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTotalDue";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach(DataRow dr in dtTable.Rows)
                {
                    lstpr.Balance = Convert.ToDecimal(dr["Balance"]);
                }
               

                db.Dispose();
                return lstpr.Balance.ToString();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<MyTransactionModel> GetTenantUpTransactionList(long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyTransactionModel> lstpr = new List<MyTransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantUpTransactionList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "UserId";
                    paramTID.Value = UserId;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    MyTransactionModel pr = new MyTransactionModel();

                    DateTime? transactiondateString = null;
                    try
                    {
                        transactiondateString = Convert.ToDateTime(dr["Charge_Date"].ToString());
                    }
                    catch
                    {

                    }
                   
               
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.Value.ToString("MM/dd/yyyy");
                  
                    pr.Credit_Amount = Convert.ToDecimal(dr["MonthlyPayment"].ToString());
                    pr.Description = dr["Description"].ToString();
                 
                    pr.Charge_Amount = Convert.ToDecimal(dr["MonthlyPayment"].ToString());
                    pr.CurrentUpcomingCharges = Convert.ToDecimal(dr["MonthlyPayment"].ToString());
                    pr.CurrentUpcomingChargesString = pr.CurrentUpcomingCharges.Value.ToString("0.00");
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
        public MyTransactionModel GetTransactionDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MyTransactionModel model = new MyTransactionModel();
            
            model.Charge_Amount = 0;
            model.Credit_Amount = 0;
            model.Miscellaneous_Amount = 0;
            model.Batch_Source = "";
            model.Tax_Sequence = 0;
            model.Batch = "";

            var getTransdata = db.tbl_Transaction.Where(p => p.TransID == id).FirstOrDefault();
            if (getTransdata != null)
            {
                DateTime? chargedate = null;
                try
                {
                    chargedate = Convert.ToDateTime(getTransdata.Charge_Date.ToString());
                }
                catch
                {

                }
              
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
                model.Charge_DateString = chargedate.Value!=null ? chargedate.Value.ToString("MM/dd/yyyy") :"";
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
        public List<MyTransactionModel> GetAccountHistoryListByDateRange(long TenantID, DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyTransactionModel> lstpr = new List<MyTransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAccountHistoryByDateRange";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantId";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramFromDate = cmd.CreateParameter();
                    paramFromDate.ParameterName = "FromDate";
                    paramFromDate.Value = FromDate;
                    cmd.Parameters.Add(paramFromDate);

                    DbParameter paramToDate = cmd.CreateParameter();
                    paramToDate.ParameterName = "ToDate";
                    paramToDate.Value = ToDate;
                    cmd.Parameters.Add(paramToDate);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    MyTransactionModel pr = new MyTransactionModel();

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
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.Value.ToString("MM/dd/yyyy");
                    pr.Run = Convert.ToInt32(dr["Run"].ToString());
                    pr.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    pr.Reference = dr["Reference"].ToString();
                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
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

        public string SaveUpdateTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            ApplyNowModel mm = new ApplyNowModel();
             mm.Name_On_Card=model.NameOnCardString;
            mm.CardNumber = model.TAccCardNumber;
            mm.CardMonth =Convert.ToInt32(model.ExpirationMonthOnCardString);
            mm.CardYear = Convert.ToInt32(model.ExpirationYearOnCardString);
            mm.CCVNumber = 123;
            mm.ProspectId = model.TenantID;
            mm.PaymentMethod =1;

            string transStatus = new UsaePayModel().ChargeCard(mm);
            String[] spearator = { "|" };
            String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            if (strlist[1] != "000000")
            {
                var saveTransaction = new tbl_Transaction()
                {
                   
                    TenantID = model.TenantID,
                    Revision_Num = model.Revision_Num,
                    Transaction_Type = model.Transaction_Type,
                    Transaction_Date = DateTime.Now,
                    Run = 1,
                    LeaseID = model.LeaseID,
                    Reference = model.Reference,
                    CreatedDate = DateTime.Now,
                    Credit_Amount = model.Charge_Amount,
                    Description = model.Description + "| TransID: " + Convert.ToInt32(strlist[1]),
                    Charge_Date = model.Charge_Date,
                    Charge_Type = Convert.ToInt32(model.Charge_Type),
                    Payment_ID = Convert.ToInt32(strlist[1]),
                    Charge_Amount = 0,
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
                    CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                    GL_Trans_Reference_1 = model.PropertyID.ToString(),
                    GL_Trans_Reference_2 = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.FullName.ToString(),
                    GL_Entries_Created =1,
                    GL_Trans_Description = transStatus.ToString(),
                    TAccCardName = model.NameOnCardString,
                    TAccCardNumber = model.NumberOnCardString,
                    TCardExpirationMonth = model.ExpirationMonthOnCardString,
                    TCardExpirationYear = model.ExpirationYearOnCardString,
                    TBankName = model.BankName,
                    TRoutingNumber = model.RoutingNumber
                    
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

                }
                db.SaveChanges();
                msg = "Transaction Updated Successfully";
            }

            db.Dispose();
            return msg;
        }

        public List<MyTransactionModel> GetTenantAccountHistoryList(long TenantID)
        {
            List<MyTransactionModel> listTenantHistory = new List<MyTransactionModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getTenantAccountHistory = db.tbl_Transaction.Where(co => co.TenantID == TenantID).ToList();
            if (getTenantAccountHistory!=null)
            {
                foreach (var item in getTenantAccountHistory)
                {
                    listTenantHistory.Add(new MyTransactionModel()
                    {
                        TransID = item.TransID,
                        Transaction_Date = item.Transaction_Date,
                        Transaction_DateString = item.Transaction_Date.ToString("MM/dd/yyyy"),
                        Description = item.Description,
                        Charge_Amount = item.Charge_Amount,
                    });
                }
            }
            return listTenantHistory;
        }
    }
}