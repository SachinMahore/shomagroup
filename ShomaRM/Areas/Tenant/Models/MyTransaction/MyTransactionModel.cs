using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Areas.Tenant.Models;
using System.Data;
using System.Data.Common;
using ShomaRM.Models;
using System.Web.Configuration;
using ShomaRM.Models.TwilioApi;

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
        public int LeaseTerm { get; set; }

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
        public string AuthCode { get; set; }
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
        public Nullable<int> CCVNumber { get; set; }
        public Nullable<int> PAID { get; set; }
        public List<BillModel> lstpr { get; set; }
        public long UserId { get; set; }
        public int TMPID { get; set; }
        public int IsAllUpdate { get; set; }
        public int IsAmeDepoPay { get; set; }
        public long ARID { get; set; }
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];

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
                    pr.Description = dr["Description"].ToString()+" [" + dr["AccountName"].ToString()+"]";
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
        public List<MyTransactionModel> GetRecurringPayLists(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyTransactionModel> lstpr = new List<MyTransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetRecurringPayLists";
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

                    pr.PAID = Convert.ToInt32(dr["PAID"].ToString());
                    pr.TransID = Convert.ToInt32(dr["TMPID"].ToString());
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.Value.ToString("MM/dd/yyyy");
                    pr.TAccCardName = dr["TAccCardName"].ToString();
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
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
                foreach (DataRow dr in dtTable.Rows)
                {
                    lstpr.Balance = Convert.ToDecimal(dr["Balance"]);
                    //lstpr.TransID = Convert.ToInt32(dr["TMPID"]);
                }


                db.Dispose();
                return lstpr.Balance.ToString() + "|" + 0;
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
        public List<MyTransactionModel> GetUpTransationLists(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MyTransactionModel> lstpr = new List<MyTransactionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUpTransationLists";
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


                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.Value.ToString("MM/dd/yyyy");

                    pr.Credit_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();

                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    pr.CurrentUpcomingCharges = Convert.ToDecimal(dr["Charge_Amount"].ToString());
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
                model.Charge_DateString = chargedate.Value != null ? chargedate.Value.ToString("MM/dd/yyyy") : "";
                model.Charge_Date = getTransdata.Charge_Date;
                model.Charge_Type = getTransdata.Charge_Type.ToString();
                model.Payment_ID = getTransdata.Payment_ID;
                model.AuthCode = getTransdata.Authcode;
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
        public string SaveUpdateTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == model.PAID).FirstOrDefault();
            ApplyNowModel mm = new ApplyNowModel();
            string transStatus = "";
            if (editPaymentAccounts != null)
            {
                mm.Name_On_Card = editPaymentAccounts.NameOnCard;
                mm.CardNumber = editPaymentAccounts.CardNumber;
                mm.CardMonth = Convert.ToInt32(editPaymentAccounts.Month);
                mm.CardYear = Convert.ToInt32(editPaymentAccounts.Year);
                mm.CCVNumber = model.CCVNumber;
                mm.ProspectId = model.TenantID;
                mm.PaymentMethod = 1;
                mm.Charge_Amount = model.Charge_Amount;
                mm.AccountNumber = editPaymentAccounts.AccountNumber;
                mm.RoutingNumber = editPaymentAccounts.RoutingNumber;
                mm.BankName = editPaymentAccounts.BankName;
                if (mm.CardNumber != null)
                {
                    mm.Name_On_Card = editPaymentAccounts.NameOnCard;
                    transStatus = new UsaePayModel().ChargeCard(mm);
                }
                else if (mm.RoutingNumber != null)
                {
                    mm.Name_On_Card = editPaymentAccounts.AccountName;
                    transStatus = new UsaePayModel().ChargeACH(mm);
                }
            }

           
                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = model.TenantID,
                        Revision_Num = model.Revision_Num,
                        Transaction_Type = "3",
                        Transaction_Date = Convert.ToDateTime(model.Charge_Date),
                        Run = 1,
                        LeaseID = model.LeaseID,
                        Reference = model.Reference,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + " | TransID: " + strlist[1],
                        Charge_Date = model.Charge_Date,
                        Charge_Type = Convert.ToInt32(model.Charge_Type),
                        Authcode = strlist[1],
                        Charge_Amount =0,
                        Miscellaneous_Amount = model.Miscellaneous_Amount,
                        Accounting_Date = DateTime.Now,
                     
                        Batch = "0",
                        Batch_Source = "",
                        CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                       
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
                    var TransId = saveTransaction.TransID;
              
                    msg = transStatus.ToString();
                }
           
            db.Dispose();
            return msg;
        }
        public string SaveUpdateRecurringTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (model.IsAllUpdate == 0)
            {
                var recPay = db.tbl_TenantMonthlyPayments.Where(co => co.TMPID == model.TransID).FirstOrDefault();
                if (recPay != null)
                {
                    recPay.Transaction_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(1);
                    recPay.Charge_Amount = model.Charge_Amount;
                    recPay.PAID = Convert.ToInt32(model.PAID);
                    recPay.Description = "Monthly Charges - " + Convert.ToDateTime(model.Charge_Date).AddMonths(1);
                    db.SaveChanges();

                    msg = "Recurring Payment Updated Successfully";
                }
                else
                {
                    msg = "";
                }
            }
            else
            {
                var monthPayList = db.tbl_TenantMonthlyPayments.Where(p => p.TenantID ==model.TenantID && p.IsRecurring != 3).ToList();
                if (monthPayList != null)
                {
                    foreach (var recPay in monthPayList)
                    {
                        recPay.Transaction_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(recPay.Revision_Num-1);
                        recPay.Charge_Amount = model.Charge_Amount;
                        recPay.PAID = Convert.ToInt32(model.PAID);
                        recPay.Description = "Monthly Charges - " + Convert.ToDateTime(model.Charge_Date).AddMonths(recPay.Revision_Num-1);
                        db.SaveChanges();
                    }
                    
                }
                msg = "All Recurring Payment Updated Successfully";
            }
            db.Dispose();
            return msg;
        }
        public string SetUpRecurringTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            var monthPayList = db.tbl_TenantMonthlyPayments.Where(p => p.TenantID == model.TenantID && p.IsRecurring!=3).ToList();
            if (monthPayList != null)
            {
                foreach (var tl in monthPayList)
                {
                    tl.IsRecurring = 1;
                    tl.Transaction_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(tl.Revision_Num - 1);
                    tl.Charge_Amount = model.Charge_Amount;
                    tl.PAID = Convert.ToInt32(model.PAID);
                    tl.Description = "Monthly Charges - " + Convert.ToDateTime(model.Charge_Date).AddMonths(tl.Revision_Num - 1);
                }
                db.SaveChanges();
            }
            msg = "Recurring Payment Set Up Successfully";
            //var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == model.PAID).FirstOrDefault();

            //if (model.TransID == 0)
            //{
            //    for (int i= model.Revision_Num; i < model.LeaseTerm;i++)
            //    {
            //        var saveTransaction = new tbl_Transaction()
            //        {

            //            TenantID = model.TenantID,
            //            Revision_Num =i+1,
            //            Transaction_Type = "1",
            //            Transaction_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(i-1),
            //            Run = 1,

            //            CreatedDate = DateTime.Now,
            //            Credit_Amount = 0,
            //            Description = "Monthly Charges-" + Convert.ToDateTime(model.Charge_Date).AddMonths(i - 1),
            //            Charge_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(i-1),
            //            Charge_Type = 3,

            //            Charge_Amount = model.Charge_Amount,
            //            Accounting_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(i-1),

            //            CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,

            //            TAccCardName = editPaymentAccounts.NameOnCard,
            //            TAccCardNumber = editPaymentAccounts.CardNumber,
            //            TCardExpirationMonth = editPaymentAccounts.Month,
            //            TCardExpirationYear = editPaymentAccounts.Year,

            //        };
            //        db.tbl_Transaction.Add(saveTransaction);
            //        db.SaveChanges();
            //    }


            //    msg = "Recurring Payment Set Up Successfully";
            //}
            //else
            //{
            //    msg = "Recurring Payment Updated Successfully";
            //}

            db.Dispose();
            return msg;
        }
        public string DeleteRecurringTransaction(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            var monthPayList = db.tbl_TenantMonthlyPayments.Where(p => p.TenantID == TenantID && p.IsRecurring!=3).ToList();
            if (monthPayList != null)
            {
                foreach (var tl in monthPayList)
                {
                    tl.IsRecurring = 0;

                }
                db.SaveChanges();
            }
            msg = "Recurring Payment Cancelled Successfully";


            db.Dispose();
            return msg;
        }
        public List<MyTransactionModel> GetTenantAccountHistoryList(long TenantID)
        {
            List<MyTransactionModel> listTenantHistory = new List<MyTransactionModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getTenantAccountHistory = db.tbl_Transaction.Where(co => co.TenantID == TenantID).ToList();
            if (getTenantAccountHistory != null)
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
        public List<MyTransactionModel> GetReservationPaymentList(long ARID)
        {
            List<MyTransactionModel> listReservationPayment = new List<MyTransactionModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getReservationPayment = db.tbl_Transaction.Where(co => co.Batch == ARID.ToString() && co.Charge_Type == 4).ToList();
            if (getReservationPayment != null)
            {
                foreach (var item in getReservationPayment)
                {
                    listReservationPayment.Add(new MyTransactionModel()
                    {
                        TransID = item.TransID,
                        Transaction_Date = item.Transaction_Date,
                        Transaction_DateString = item.Transaction_Date.ToString("MM/dd/yyyy"),
                        Description = item.Description,
                        Charge_Amount = item.Charge_Amount,
                    });
                }
            }
            return listReservationPayment;
        }
        public string CreateTransBill(long TransID, decimal Amount, string Description)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var saveTransBill = new tbl_Bill()
            {
                TransID = TransID,
                Amount = Amount,
                Description = Description,

            };
            db.tbl_Bill.Add(saveTransBill);
            db.SaveChanges();
            return "";
        }
        public MyTransactionModel GetTenantBillList(long TransID)
        {
            MyTransactionModel model = new MyTransactionModel();
            ShomaRMEntities db = new ShomaRMEntities();
            List<BillModel> listbill = new List<BillModel>();
            model.lstpr = listbill;
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantBillList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TransID";
                    paramTID.Value = TransID;
                    cmd.Parameters.Add(paramTID);


                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {


                    model.lstpr.Add(new BillModel()
                    {
                        BillID = Convert.ToInt32(dr["BillID"].ToString()),
                        Description = dr["Description"].ToString(),
                        Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                    });

                }

                var invDet = db.tbl_Transaction.Where(p => p.TransID == TransID).FirstOrDefault();
                model.Credit_Amount = invDet.Credit_Amount;
                model.Charge_Amount = invDet.Charge_Amount;
                model.Description = invDet.Description;
                model.Transaction_DateString = invDet.Transaction_Date.ToString("MM/dd/yyyy");

                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public string RefundRRCharges(long TransID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var transDetails = db.tbl_Transaction.Where(p => p.TransID == TransID).FirstOrDefault();

            string transStatus = new UsaePayModel().RefundCharge(TransID);
            String[] spearator = { "|" };
            String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            if (strlist[1] != "000000")
            {
                var saveTransaction = new tbl_Transaction()
                {

                    TenantID = transDetails.TenantID,
                    Revision_Num = transDetails.Revision_Num,
                    Transaction_Type = transDetails.Transaction_Type,
                    Transaction_Date = DateTime.Now,
                    Run = 1,
                    LeaseID = transDetails.LeaseID,
                    Reference = transDetails.Reference,
                    CreatedDate = DateTime.Now,
                    Credit_Amount = transDetails.Charge_Amount,
                    Description = transDetails.Description + " | TransID: " + strlist[1],
                    Charge_Date = transDetails.Charge_Date,
                    Charge_Type = 10,
                    Authcode = strlist[1],
                    Charge_Amount = Convert.ToDecimal(transDetails.Charge_Amount),
                    Miscellaneous_Amount = transDetails.Miscellaneous_Amount,
                    Accounting_Date = DateTime.Now,

                    CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,


                };
                db.tbl_Transaction.Add(saveTransaction);
                db.SaveChanges();


            }

            db.Dispose();
            return msg;
        }
        public string ScheduleRecurring()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            List<TenantMonthlyPayments> lsttmp = new List<TenantMonthlyPayments>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetScheduleRecurringLists";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantMonthlyPayments pr = new TenantMonthlyPayments();
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    pr.PAID = Convert.ToInt32(dr["PAID"].ToString());
                    pr.TMPID = Convert.ToInt32(dr["TMPID"].ToString());
                    pr.Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]);
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    lsttmp.Add(pr);

                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = Convert.ToInt64(dr["TenantID"].ToString()),
                        Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString()),
                        Transaction_Type = dr["PAID"].ToString(),
                        Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Run = Convert.ToInt32(dr["TMPID"].ToString()),
                        CreatedDate = DateTime.Now,
                        Credit_Amount = 0,
                        Description = dr["Description"].ToString(),
                        Charge_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Charge_Type = 3,
                        Payment_ID = null,
                        Authcode = "",
                        Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString()),
                        Accounting_Date = DateTime.Now,
                        Batch = Batch,
                        Batch_Source = "",
                        CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,

                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();
                    var TransId = saveTransaction.TransID;

                    long tmpid = Convert.ToInt64(dr["TMPID"].ToString());
                    var RecuuRingData = db.tbl_TenantMonthlyPayments.Where(p => p.TMPID == tmpid).FirstOrDefault();
                    if (RecuuRingData != null)
                    {
                        RecuuRingData.IsRecurring = 3;
                        db.SaveChanges();
                    }
                    long uid = Convert.ToInt64(dr["UserID"].ToString());
                    var prospectDet = db.tbl_ApplyNow.Where(p => p.UserId == uid).FirstOrDefault();

                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.Rent), "Monthly Rent");
                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.TrashAmt), "Trash/Recycle charges");
                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.ConvergentAmt), "Convergent Billing charges");
                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.PestAmt), "Pest Control charges");

                    if (prospectDet.ParkingAmt != 0)
                    {
                        CreateTransBill(TransId, Convert.ToDecimal(prospectDet.ParkingAmt), "Additional Parking charges");
                    }
                    if (prospectDet.PetPlaceAmt != 0)
                    {
                        CreateTransBill(TransId, Convert.ToDecimal(prospectDet.PetPlaceAmt), "Pet charges");
                    }
                    if (prospectDet.StorageAmt != 0)
                    {
                        CreateTransBill(TransId, Convert.ToDecimal(prospectDet.StorageAmt), "Storage Charges");
                    }


                }
                db.Dispose();

            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }


            msg = "Monthly Payment Generated Successfully";


            db.Dispose();
            return msg;
        }
        public string GenerateMonthlyRent()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            List<TenantMonthlyPayments> lsttmp = new List<TenantMonthlyPayments>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetMonthlyPayLists";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantMonthlyPayments pr = new TenantMonthlyPayments();
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    pr.PAID = Convert.ToInt32(dr["PAID"].ToString());
                    pr.TMPID = Convert.ToInt32(dr["TMPID"].ToString());
                    pr.Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]);
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    lsttmp.Add(pr);

                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = Convert.ToInt64(dr["TenantID"].ToString()),
                        Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString()),
                        Transaction_Type = dr["PAID"].ToString(),
                        Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Run = Convert.ToInt32(dr["TMPID"].ToString()),
                        CreatedDate = DateTime.Now,
                        Credit_Amount = 0,
                        Description = dr["Description"].ToString(),
                        Charge_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Charge_Type = 3,
                        Payment_ID = null,
                        Authcode="",
                        Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString()),
                        Accounting_Date = DateTime.Now,
                        Batch = Batch,
                        Batch_Source = "",
                        CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,

                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();
                    var TransId = saveTransaction.TransID;

                    long tmpid = Convert.ToInt64(dr["TMPID"].ToString());
                    var RecuuRingData = db.tbl_TenantMonthlyPayments.Where(p => p.TMPID == tmpid).FirstOrDefault();
                    if (RecuuRingData != null)
                    {
                        RecuuRingData.IsRecurring = 3;
                        db.SaveChanges();
                    }
                    long uid = Convert.ToInt64(dr["UserID"].ToString());
                    var prospectDet = db.tbl_ApplyNow.Where(p => p.UserId ==uid).FirstOrDefault();

                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.Rent), "Monthly Rent");
                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.TrashAmt), "Trash/Recycle charges");
                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.ConvergentAmt), "Convergent Billing charges");
                    CreateTransBill(TransId, Convert.ToDecimal(prospectDet.PestAmt), "Pest Control charges");

                    if (prospectDet.ParkingAmt != 0)
                    {
                        CreateTransBill(TransId, Convert.ToDecimal(prospectDet.ParkingAmt), "Additional Parking charges");
                    }
                    if (prospectDet.PetPlaceAmt != 0)
                    {
                        CreateTransBill(TransId, Convert.ToDecimal(prospectDet.PetPlaceAmt), "Pet charges");
                    }
                    if (prospectDet.StorageAmt != 0)
                    {
                        CreateTransBill(TransId, Convert.ToDecimal(prospectDet.StorageAmt), "Storage Charges");
                    }


                }
                db.Dispose();
             
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }

    
            msg = "Monthly Payment Generated Successfully";
            

            db.Dispose();
            return msg;
        }
        public string GenerateLateFee()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            decimal dueAmt = 0;
            List<TenantMonthlyPayments> lsttmp = new List<TenantMonthlyPayments>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetMonthlyPayLists";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                  
                    var saveTransaction = new tbl_Transaction()
                    {

                        TenantID = Convert.ToInt64(dr["TenantID"].ToString()),
                        Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString()),
                        Transaction_Type = dr["PAID"].ToString(),
                        Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Run = Convert.ToInt32(dr["TMPID"].ToString()),
                        CreatedDate = DateTime.Now,
                        Credit_Amount = 0,
                        Description = "Late fees",
                        Charge_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Charge_Type = 3,
                        Payment_ID = null,
                        Authcode="",
                        Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString()),
                        Accounting_Date = DateTime.Now,
                        Batch = Batch,
                        Batch_Source = "",
                        CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,

                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();
                    var TransId = saveTransaction.TransID;

                    CreateTransBill(TransId, Convert.ToDecimal(dueAmt), "Late fees");
                   
                }
                db.Dispose();

            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }


            msg = "Monthly Payment Generated Successfully";


            db.Dispose();
            return msg;
        }

        public string SaveAmenityTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == model.PAID).FirstOrDefault();
            ApplyNowModel mm = new ApplyNowModel();
            string transStatus = "";
            if (editPaymentAccounts != null)
            {

                mm.CardNumber = editPaymentAccounts.CardNumber;
                mm.CardMonth = Convert.ToInt32(editPaymentAccounts.Month);
                mm.CardYear = Convert.ToInt32(editPaymentAccounts.Year);
                mm.CCVNumber = model.CCVNumber;
                mm.ProspectId = model.TenantID;
                mm.PaymentMethod = 1;
                mm.Charge_Amount = model.Charge_Amount;

                mm.AccountNumber = editPaymentAccounts.AccountNumber;
                mm.RoutingNumber = editPaymentAccounts.RoutingNumber;
                mm.BankName = editPaymentAccounts.BankName;

                if (mm.CardNumber != null)
                {
                    mm.Name_On_Card = editPaymentAccounts.NameOnCard;
                    transStatus = new UsaePayModel().ChargeCard(mm);
                }
                else if (mm.RoutingNumber != null)
                {
                    mm.Name_On_Card = editPaymentAccounts.AccountName;
                    transStatus = new UsaePayModel().ChargeACH(mm);
                }
            }


            String[] spearator = { "|" };
            String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            if (strlist[1] != "000000")
            {
                var saveTransaction = new tbl_Transaction()
                {

                    TenantID = model.TenantID,
                    Revision_Num = 1,
                    Transaction_Type = model.Transaction_Type,
                    Transaction_Date = DateTime.Now,
                    Run = 1,
                    LeaseID = model.LeaseID,
                    Reference = "AR" + model.Batch,
                    CreatedDate = DateTime.Now,
                    Credit_Amount = model.Charge_Amount,
                    Description = model.Description + " | TransID: " + strlist[1],
                    Charge_Date = DateTime.Now,
                    Charge_Type = 4,
                    Authcode = strlist[1],
                    Charge_Amount = model.Charge_Amount,

                    Accounting_Date = DateTime.Now,

                    Batch = model.Batch,
                    Batch_Source = "",
                    CreatedBy = Convert.ToInt32(model.UserId),

                    GL_Trans_Description = transStatus.ToString(),

                };
                db.tbl_Transaction.Add(saveTransaction);
                db.SaveChanges();
                var TransId = saveTransaction.TransID;

                var ameDet = db.tbl_AmenityReservation.Where(p => p.ARID == model.ARID).FirstOrDefault();
                var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == ameDet.TenantID).FirstOrDefault();

                string reportHTML = "";
                string body = "";

                string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");

                string message = "";
                string phonenumber = GetTenantData.Mobile;

                if (model.IsAmeDepoPay == 1)
                {
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.ReservationFee), model.Description + " Fees");
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.DepositFee), model.Description + " Deposit");
                    ameDet.Status = 4;
                    db.SaveChanges();

                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Fees & Deposit Paid");
                    reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Reservation Fee payment in the Amount of $" + ameDet.ReservationFee + "and your Deposit Fee in the Amount of $" + ameDet.DepositFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate + " at " + ameDet.DesiredTime + " has been received. Your Reservation is now confirmed. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.” </p>");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                    body = reportHTML;

                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Fees & Deposit Paid", body);

                    message = "Your Reservation Fee payment has been received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }
                else if (model.IsAmeDepoPay == 0)
                {
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.ReservationFee), model.Description + " Fees");
                    if (Convert.ToDecimal(ameDet.DepositFee) == 0)
                    {
                        ameDet.Status = 4;
                    }
                    else
                    {
                        ameDet.Status = 2;
                    }

                    db.SaveChanges();
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Fees Paid");
                    reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Reservation Fee payment in the Amount of $" + ameDet.ReservationFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate + " at " + ameDet.DesiredTime + " has been received. Your Reservation is now confirmed subject to the payment of the Security Deposit at least 3 business days prior to the scheduled event. If we do 	not receive the Security Deposit fee prior to the deadline, management reserves the right to cancel the event and the Reservation fee will not be refunded. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.” </p>");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                    body = reportHTML;

                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Fees Paid", body);
                    message = "Your Reservation Fee payment has been received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }
                else if (model.IsAmeDepoPay == 3)
                {
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.DepositFee), model.Description + " Deposit");
                    ameDet.Status = 3;
                    db.SaveChanges();
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Deposit Paid");
                    reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Reservation Deposit payment in the Amount of $" + ameDet.DepositFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate + " at " + ameDet.DesiredTime + " has been received. Your Reservation is now completed. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.” </p>");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                    body = reportHTML;

                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Deposit Paid", body);
                    message = "Your Reservation Deposit payment has been received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }

                msg = "1";

            }
            else
            {
                msg = "0";
            }

            db.Dispose();
            return msg;
        }
    }
}

public partial class BillModel
{
    public long BillID { get; set; }
    public long TransID { get; set; }
    public string Description { get; set; }
    public Nullable<decimal> Amount { get; set; }
}
public partial class TenantMonthlyPayments
{
    public long TMPID { get; set; }
    public long TenantID { get; set; }
    public int Revision_Num { get; set; }
    public System.DateTime Transaction_Date { get; set; }
    public string Description { get; set; }
    public Nullable<decimal> Charge_Amount { get; set; }
    public int PAID { get; set; }
    public int IsRecurring { get; set; }
}