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
        public long EID { get; set; }

        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string ServerURL = WebConfigurationManager.AppSettings["ServerURL"];

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
                    string accName = "";
                    string credaccount = "";
                    if(dr["PayMethod"].ToString()=="1")
                    {
                        credaccount = new EncryptDecrypt().DecryptText(dr["CardNumber"].ToString());
                        if (credaccount.Length > 3)
                        {
                            accName = dr["AccountName"].ToString() + "(" + credaccount.Substring(credaccount.Length - 4, 4) + ")";
                        }
                        else
                        {
                            accName = dr["AccountName"].ToString() + "(" + credaccount + ")";
                        }
                    }else
                    {
                        credaccount = new EncryptDecrypt().DecryptText(dr["AccountNumber"].ToString());
                        if (credaccount.Length > 3)
                        {
                            accName = dr["AccountName"].ToString() + "(" + credaccount.Substring(credaccount.Length - 4, 4) + ")";
                        }
                        else
                        {
                            accName = dr["AccountName"].ToString() + "(" + credaccount + ")";
                        }
                    }

                    pr.PAID = Convert.ToInt32(dr["PAID"].ToString());
                    pr.TransID = Convert.ToInt32(dr["TMPID"].ToString());
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.Value.ToString("MM/dd/yyyy");
                    pr.TAccCardName = accName;
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
           
                model.Transaction_Type = getTransdata.PAID;
                model.Transaction_Date = getTransdata.Transaction_Date;
              
                model.CreatedDate = getTransdata.CreatedDate;
                model.Credit_Amount = getTransdata.Credit_Amount;
                model.Description = getTransdata.Description;
                model.Charge_DateString = chargedate.Value != null ? chargedate.Value.ToString("MM/dd/yyyy") : "";
                model.Charge_Date = getTransdata.Charge_Date;
                model.Charge_Type = getTransdata.Charge_Type.ToString();
              
                model.AuthCode = getTransdata.Authcode;
                model.Charge_Amount = getTransdata.Charge_Amount;
                model.Miscellaneous_Amount = getTransdata.Miscellaneous_Amount;
                model.Accounting_Date = getTransdata.Accounting_Date;
          
                model.Batch = getTransdata.Batch;
           
                model.CreatedBy = getTransdata.CreatedBy;
             

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
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == model.PAID).FirstOrDefault();
            var propertyDet = db.tbl_Properties.Where(co => co.PID ==8).FirstOrDefault();
            ApplyNowModel mm = new ApplyNowModel();
            string transStatus = "";

            string decryptedCardNumber = string.IsNullOrWhiteSpace(editPaymentAccounts.CardNumber) ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.CardNumber);
            string decryptedAccountNumber = string.IsNullOrWhiteSpace(editPaymentAccounts.AccountNumber) ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.AccountNumber);
            string decrytpedCardMonth = string.IsNullOrWhiteSpace(editPaymentAccounts.Month) ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.Month);
            string decrytpedCardYear = string.IsNullOrWhiteSpace(editPaymentAccounts.Year) ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.Year);
            string decrytpedRoutingNumber = string.IsNullOrWhiteSpace(editPaymentAccounts.RoutingNumber) ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.RoutingNumber);

            if (editPaymentAccounts != null)
            {
                if(propertyDet!=null)
                {
                    mm.ProcessingFees = propertyDet.ProcessingFees ?? 0;
                }
                mm.Name_On_Card = editPaymentAccounts.NameOnCard;
                mm.CardNumber = decryptedCardNumber;
                mm.CardMonth = decrytpedCardMonth;
                mm.CardYear = decrytpedCardYear;
                //mm.CCVNumber = model.CCVNumber;
                mm.ProspectId = model.TenantID;
                mm.PaymentMethod = 1;
                mm.Charge_Amount = model.Charge_Amount;
                mm.AccountNumber = decryptedAccountNumber;
                mm.RoutingNumber = decrytpedRoutingNumber;
                mm.BankName = editPaymentAccounts.BankName;
                var GetTenData = db.tbl_TenantOnline.Where(p => p.ID == model.TenantID).FirstOrDefault();
                mm.Email = GetTenData.Email;
                if (!string.IsNullOrWhiteSpace(mm.CardNumber))
                {
                    mm.Name_On_Card = editPaymentAccounts.NameOnCard;
                    transStatus = new UsaePayModel().ChargeCard(mm);
                }
                else if (!string.IsNullOrWhiteSpace(mm.RoutingNumber))
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
                        PAID = model.PAID.ToString(),
                        Transaction_Date = Convert.ToDateTime(model.Charge_Date),
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.Charge_Amount,
                        Description = model.Description + " | TransID: " + strlist[2],
                        Charge_Date = model.Charge_Date,
                        Charge_Type = Convert.ToInt32(model.Charge_Type),
                        Authcode = strlist[1],
                        Charge_Amount = 0,
                        Miscellaneous_Amount = model.Miscellaneous_Amount,
                        Accounting_Date = DateTime.Now,
                        Batch = "0",
                        CreatedBy = userid,
                        UserID = userid,
                        RefNum= strlist[2]
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

            if (model.IsAllUpdate == 1)
            {
                var monthPayList = db.tbl_TenantMonthlyPayments.Where(p => p.TenantID == model.TenantID && p.IsRecurring != 3).ToList();
                if (monthPayList != null)
                {
                    foreach (var recPay in monthPayList)
                    {
                        recPay.Transaction_Date = Convert.ToDateTime(model.Charge_Date).AddMonths(recPay.Revision_Num - 1);
                        recPay.Charge_Amount = model.Charge_Amount;
                        recPay.PAID = Convert.ToInt32(model.PAID);
                        recPay.Description = "Monthly Charges - " + Convert.ToDateTime(model.Charge_Date).AddMonths(recPay.Revision_Num - 1);
                        db.SaveChanges();
                    }

                }
                msg = "All Recurring Payment Updated Successfully";
            }
            else
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
                msg = "Recurring Payment Set Up Successfully";
            }
            
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
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var transDetails = db.tbl_Transaction.Where(p => p.TransID == TransID).FirstOrDefault();
            if (transDetails != null)
            {
                string transStatus = new UsaePayModel().RefundCharge(transDetails.RefNum, Convert.ToDecimal(transDetails.Charge_Amount));
                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = transDetails.TenantID,

                        PAID = transDetails.PAID,
                        Transaction_Date = DateTime.Now,

                        CreatedDate = DateTime.Now,
                        Credit_Amount = transDetails.Charge_Amount,
                        Description = transDetails.Description + " | TransID: " + strlist[2],
                        Charge_Date = transDetails.Charge_Date,
                        Charge_Type = 10,
                        Authcode = strlist[1],
                        Charge_Amount = Convert.ToDecimal(transDetails.Charge_Amount),
                        Miscellaneous_Amount = transDetails.Miscellaneous_Amount,
                        Accounting_Date = DateTime.Now,
                        CreatedBy = userid,
                        RefNum= strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();
                }
            }

            db.Dispose();
            return msg;
        }
        public string VoidRRCharges(long TransID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var transDetails = db.tbl_Transaction.Where(p => p.TransID == TransID).FirstOrDefault();
            if (transDetails != null)
            {
                string transStatus = new UsaePayModel().VoidCharge(transDetails.RefNum, Convert.ToDecimal(transDetails.Charge_Amount));
                String[] spearator = { "|" };
                String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    var saveTransaction = new tbl_Transaction()
                    {
                        TenantID = transDetails.TenantID,
                        PAID = transDetails.PAID,
                        Transaction_Date = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        Credit_Amount = transDetails.Charge_Amount,
                        Description = transDetails.Description + " | TransID: " + strlist[2],
                        Charge_Date = transDetails.Charge_Date,
                        Charge_Type = 10,
                        Authcode = strlist[1],
                        Charge_Amount = Convert.ToDecimal(transDetails.Charge_Amount),
                        Miscellaneous_Amount = transDetails.Miscellaneous_Amount,
                        Accounting_Date = DateTime.Now,
                        CreatedBy = userid,
                        RefNum= strlist[2],
                    };
                    db.tbl_Transaction.Add(saveTransaction);
                    db.SaveChanges();
                }

            }

            db.Dispose();
            return msg;
        }
        public void ScheduleRecurring()
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
                    int isFailed = 0;
                    long TransId = Convert.ToInt64(dr["TransID"].ToString());
                    long PAID = Convert.ToInt32(dr["Transaction_Type"].ToString());
                    long TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    var accountDet = db.tbl_PaymentAccounts.Where(p => p.PAID == PAID).FirstOrDefault();
                    var transData = db.tbl_Transaction.Where(p => p.TransID == TransId).FirstOrDefault();
                    var tenantData = db.tbl_TenantInfo.Where(p => p.TenantID == TenantID).FirstOrDefault();
                    var propertyDet = db.tbl_Properties.Where(co => co.PID == 8).FirstOrDefault();
                    ApplyNowModel mm = new ApplyNowModel();
                    string transStatus = "";
                    string cardaccnum = "";

                    string decryptedCardNumber = string.IsNullOrWhiteSpace(accountDet.CardNumber) ? "" : new EncryptDecrypt().DecryptText(accountDet.CardNumber);
                    string decryptedAccountNumber = string.IsNullOrWhiteSpace(accountDet.AccountNumber) ? "" : new EncryptDecrypt().DecryptText(accountDet.AccountNumber);
                    string decrytpedCardMonth = string.IsNullOrWhiteSpace(accountDet.Month) ? "" : new EncryptDecrypt().DecryptText(accountDet.Month);
                    string decrytpedCardYear = string.IsNullOrWhiteSpace(accountDet.Year) ? "" : new EncryptDecrypt().DecryptText(accountDet.Year);
                    string decrytpedRoutingNumber = string.IsNullOrWhiteSpace(accountDet.RoutingNumber) ? "" : new EncryptDecrypt().DecryptText(accountDet.RoutingNumber);

                    if (accountDet != null)
                    {
                        if (propertyDet != null)
                        {
                            mm.ProcessingFees = propertyDet.ProcessingFees ?? 0;
                        }
                        mm.Name_On_Card = accountDet.NameOnCard;
                        mm.CardNumber = decryptedCardNumber;
                        mm.CardMonth = decrytpedCardMonth;
                        mm.CardYear = decrytpedCardYear;
                        mm.ProspectId = TenantID;
                        mm.PaymentMethod = 1;
                        mm.Charge_Amount = transData.Charge_Amount;
                        mm.AccountNumber = decryptedAccountNumber;
                        mm.RoutingNumber = decrytpedRoutingNumber;
                        mm.BankName = accountDet.BankName;
                        if (!string.IsNullOrWhiteSpace( mm.CardNumber))
                        {
                            mm.Name_On_Card = accountDet.NameOnCard;
                            cardaccnum = mm.CardNumber.Substring(mm.CardNumber.Length - 4);
                            transStatus = new UsaePayModel().ChargeCard(mm);
                        }
                        else if (!string.IsNullOrWhiteSpace(mm.RoutingNumber))
                        {
                            mm.Name_On_Card = accountDet.AccountName;
                            cardaccnum = mm.AccountNumber.Substring(mm.AccountNumber.Length - 4);
                            transStatus = new UsaePayModel().ChargeACH(mm);
                        }

                        String[] spearator = { "|" };
                        String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                        if (strlist[1] != "000000")
                        {
                            transData.Transaction_Date = DateTime.Now;
                            transData.Credit_Amount = transData.Charge_Amount;
                            transData.Description = transData.Description + " | TransID: " + strlist[2];
                            transData.Authcode = strlist[1];
                            transData.Accounting_Date = DateTime.Now;
                            transData.RefNum= strlist[2];
                            db.SaveChanges();
                            msg = transStatus.ToString();
                        }
                        else
                        {
                            isFailed = 1;
                            var saveTransaction = new tbl_Transaction()
                            {
                                TenantID = TenantID,
                                PAID = PAID.ToString(),
                                Transaction_Date = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                Credit_Amount = 0,
                                Description = transData.Description,
                                Charge_Date = DateTime.Now,
                                Charge_Type = 11,
                                Authcode = "",
                                Charge_Amount = transData.Charge_Amount,
                                Accounting_Date = DateTime.Now,
                                Batch = TransId.ToString(),                               
                                CreatedBy = 1,
                                RefNum=""
                            };
                            db.tbl_Transaction.Add(saveTransaction);
                            db.SaveChanges();
                        }

                        string reportHTML = "";
                        string body = "";
                        string subject = "";
                        string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                        reportHTML = reportHTML.Replace("[%ServerURL%]", ServerURL);
                        reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));
                        string message = "";
                        string phonenumber = tenantData.Mobile;

                        if (isFailed == 0)
                        {
                            subject = "Sanctuary Payment Confirmation";
                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + tenantData.FirstName + " " + tenantData.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal, this is not a confirmation that the payment has been processed at your bank. It may take 2-3 days before the funds have been debited from you account. Please review the payment information below and keep this email for your personal records</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment confirmation# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (mm.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + (mm.ProcessingFees??0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((mm.Charge_Amount ?? 0) + (mm.ProcessingFees??0)).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">*The service fee is collected by the payment agent not the property management company and will not display your ledger. Service fee is non Refundable.</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);
                           
                            message = "Sanctuary Payment Confirmation. Please check the email for detail.";
                            
                            //sachin m 01 may 2020

                            //message = "Sanctuary Payment Confirmation. Please check the email for detail.";
                            //subject = "Monthly charges received -" + DateTime.Now.ToString("MM/dd/yyyy");
                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Monthly charges received -" + DateTime.Now.ToString("MM/dd/yyyy"));
                            //reportHTML = reportHTML.Replace("[%TenantName%]", tenantData.FirstName + " " + tenantData.LastName);
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", "<p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your monthly charges received $" + transData.Charge_Amount.Value.ToString("0.00") + " on " + DateTime.Now + ". Transaction ID : " + strlist[2] + ".” </p>");
                            //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                        }
                        else
                        {
                            subject = "Sanctuary Payment Failed";

                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear: " + tenantData.FirstName + " " + tenantData.LastName + " this email confirmation is a notice that you have submitted a payment in the resident portal and transaction is falied. Please review the payment information below and keep this email for your personal records</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">PAYMENT INFORMATION</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment failed# " + strlist[2] + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment account:XXXX-" + cardaccnum + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment date:" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Payment amount:$" + (mm.Charge_Amount ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Service fee:$" + (mm.ProcessingFees ?? 0).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Total payment:$" + ((mm.Charge_Amount ?? 0) + (mm.ProcessingFees ?? 0)).ToString("0.00") + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">In meantime, if you have any questions about the application process please contact us</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                            message = "Sanctuary Payment Failed. Please check the email for detail.";

                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Monthly charges payment failed -" + DateTime.Now.ToString("MM/dd/yyyy"));
                            //reportHTML = reportHTML.Replace("[%TenantName%]", tenantData.FirstName + " " + tenantData.LastName);
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", "<p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your monthly charges failed $" + transData.Charge_Amount.Value.ToString("0.00") + " on " + DateTime.Now + ". Transaction Details : " + strlist[0] + ".” </p>");
                            //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                        }

                        body = reportHTML;

                        new EmailSendModel().SendEmail(tenantData.Email, subject, body);

                        //message = subject + ". Please check the email for detail.";
                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(phonenumber))
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            db.Dispose();
        }
        public string GenerateMonthlyRent()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
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
                    
                        PAID = dr["PAID"].ToString(),
                        Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                       
                        CreatedDate = DateTime.Now,
                        Credit_Amount = 0,
                        Description = dr["Description"].ToString(),
                        Charge_Date = Convert.ToDateTime(dr["Transaction_Date"]),
                        Charge_Type = 3,
                        
                        Authcode = "",
                        Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString()),
                        Accounting_Date = DateTime.Now,
                        Batch = Batch,
                        
                        CreatedBy = userid,
                        UserID= Convert.ToInt64(dr["UserID"].ToString()),
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
        public string GenerateLateFee()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            //decimal dueAmt = 0;
            //List<TenantMonthlyPayments> lsttmp = new List<TenantMonthlyPayments>();
            //try
            //{
            //    DataTable dtTable = new DataTable();
            //    using (var cmd = db.Database.Connection.CreateCommand())
            //    {
            //        db.Database.Connection.Open();
            //        cmd.CommandText = "usp_GetMonthlyPayLists";
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
            //        da.SelectCommand = cmd;
            //        da.Fill(dtTable);
            //        db.Database.Connection.Close();
            //    }
            //    foreach (DataRow dr in dtTable.Rows)
            //    {

            //        var saveTransaction = new tbl_Transaction()
            //        {

            //            TenantID = Convert.ToInt64(dr["TenantID"].ToString()),
            //            Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString()),
            //            Transaction_Type = dr["PAID"].ToString(),
            //            Transaction_Date = Convert.ToDateTime(dr["Transaction_Date"]),
            //            Run = Convert.ToInt32(dr["TMPID"].ToString()),
            //            CreatedDate = DateTime.Now,
            //            Credit_Amount = 0,
            //            Description = "Late fees",
            //            Charge_Date = Convert.ToDateTime(dr["Transaction_Date"]),
            //            Charge_Type = 8,
            //            Payment_ID = null,
            //            Authcode="",
            //            Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString()),
            //            Accounting_Date = DateTime.Now,
            //            Batch = Batch,
            //            Batch_Source = "",
            //            CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,

            //        };
            //        db.tbl_Transaction.Add(saveTransaction);
            //        db.SaveChanges();
            //        var TransId = saveTransaction.TransID;

            //        CreateTransBill(TransId, Convert.ToDecimal(dueAmt), "Late fees");

            //    }
            //    db.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    db.Database.Connection.Close();
            //    throw ex;
            //}


            //msg = "Monthly Payment Generated Successfully";


            //db.Dispose();
            return msg;
        }

        public string SaveAmenityTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            // var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
            var tenentUID = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var GetApplicantData = db.tbl_Applicant.Where(c => c.UserID == tenentUID).FirstOrDefault();
            var GePropertyData = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
           
            var UserData = db.tbl_TenantOnline.Where(p => p.ParentTOID == tenentUID).FirstOrDefault();


            var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ID == model.PAID).FirstOrDefault();

            string decrytpedPMID = new EncryptDecrypt().DecryptText(GetPayDetails.PaymentID);


            decimal processingFees = 0;

            if (GePropertyData != null)
            {
                processingFees = GePropertyData.ProcessingFees ?? 0;
            }

            string transStatus = "";
            //string cardaccnum = "";
            //model.Email = GetApplicantData.Email;
            //model.ProcessingFees = processingFees;

            transStatus = new UsaePayWSDLModel().PayUsingCustomerNum(GetApplicantData.CustID, decrytpedPMID, Convert.ToDecimal(model.Charge_Amount), model.Description);


            String[] spearator = { "|" };
            String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            if (strlist[0] == "Approved")
            {
                var saveTransaction = new tbl_Transaction()
                {
                    TenantID = model.TenantID,
                    PAID = model.Transaction_Type,
                    Transaction_Date = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Credit_Amount = model.Charge_Amount,
                    Description = model.Description + " | TransID: " + strlist[2],
                    Charge_Date = DateTime.Now,
                    Charge_Type = 4,
                    Authcode = strlist[1],
                    Charge_Amount = model.Charge_Amount,
                    Accounting_Date = DateTime.Now,
                    Batch = model.Batch,
                    CreatedBy = Convert.ToInt32(model.UserId),
                    UserID = Convert.ToInt32(model.UserId),
                    RefNum= strlist[2],
                };
                db.tbl_Transaction.Add(saveTransaction);
                db.SaveChanges();
                var TransId = saveTransaction.TransID;

                var ameDet = db.tbl_AmenityReservation.Where(p => p.ARID == model.ARID).FirstOrDefault();
                var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == ameDet.TenantID).FirstOrDefault();

                string reportHTML = "";
                string body = "";

                string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                //reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");

                reportHTML = reportHTML.Replace("[%ServerURL%]", ServerURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                string message = "";
                string phonenumber = GetTenantData.Mobile;

                if (model.IsAmeDepoPay == 1)
                {
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.ReservationFee), model.Description + " Fees");
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.DepositFee), model.Description + " Deposit");
                    ameDet.Status = 4;
                    db.SaveChanges();

                    string emailBody = "";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Amenity Reservation Fees & Deposit Paid</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName + "</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Your Reservation Fee payment in the Amount of $" + ameDet.ReservationFee + "and your Deposit Fee in the Amount of $" + ameDet.DepositFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate.Value.ToString("MM/dd/yyyy") + " from " + ameDet.DesiredTimeFrom + " to " + ameDet.DesiredTimeTo + " has been received. Your Reservation is now confirmed. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded. </p>";
                    reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Fees & Deposit Paid");
                    //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Reservation Fee payment in the Amount of $" + ameDet.ReservationFee + "and your Deposit Fee in the Amount of $" + ameDet.DepositFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate.Value.ToString("MM/dd/yyyy") + " from " + ameDet.DesiredTimeFrom + " to " + ameDet.DesiredTimeTo + " has been received. Your Reservation is now confirmed. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.” </p>");
                    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                    body = reportHTML;

                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Fees & Deposit Paid", body);

                    message = "Your Reservation Fee payment has been received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
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

                    string emailBody = "";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Amenity Reservation Fees & Deposit Paid</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName + "</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Your Reservation Fee payment in the Amount of $" + ameDet.ReservationFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate.Value.ToString("MM/dd/yyyy") + " from " + ameDet.DesiredTimeFrom + " to " + ameDet.DesiredTimeTo + " has been received. Your Reservation is now confirmed subject to the payment of the Security Deposit at least 3 business days prior to the scheduled event. If we do 	not receive the Security Deposit fee prior to the deadline, management reserves the right to cancel the event and the Reservation fee will not be refunded. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.</p>";
                    reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Fees Paid");
                    //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Reservation Fee payment in the Amount of $" + ameDet.ReservationFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate.Value.ToString("MM/dd/yyyy") + " from " + ameDet.DesiredTimeFrom + " to " + ameDet.DesiredTimeTo + " has been received. Your Reservation is now confirmed subject to the payment of the Security Deposit at least 3 business days prior to the scheduled event. If we do 	not receive the Security Deposit fee prior to the deadline, management reserves the right to cancel the event and the Reservation fee will not be refunded. Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.” </p>");
                    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                    body = reportHTML;

                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Fees Paid", body);
                    message = "Your Reservation Fee payment has been received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
                    }
                }
                else if (model.IsAmeDepoPay == 3)
                {
                    CreateTransBill(TransId, Convert.ToDecimal(ameDet.DepositFee), model.Description + " Deposit");
                    ameDet.Status = 3;
                    db.SaveChanges();

                    string emailBody = "";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Amenity Reservation Fees & Deposit Paid</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName + "</p>";
                    emailBody += "<p style=\"margin-bottom: 0px;\">Your Reservation Deposit payment in the Amount of $" + ameDet.DepositFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate.Value.ToString("MM/dd/yyyy") + " from " + ameDet.DesiredTimeFrom + " to " + ameDet.DesiredTimeTo + "  has been received. of $(Reservation Fee Amount) and your Deposit Fee in the Amount of $(Deposit Fee Amount) for your reservation of the (Reservation) on (Reservation Date) from (Reservation Time From) to (Reservation Time To) has been received. Your Reservation is now confirmed. Please print the attached “Clubhouse/Licensed Space Agreement” for your records. Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your refund will be processed within 7-10 days. After the 3-day deadline, your reservation fee will not be refunded.” . Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.</p>";
                    reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                    //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Amenity Reservation Deposit Paid");
                    //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                    //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Reservation Deposit payment in the Amount of $" + ameDet.DepositFee + " for your reservation of the " + model.Description + " on " + ameDet.DesiredDate.Value.ToString("MM/dd/yyyy") + " from " + ameDet.DesiredTimeFrom + " to "+ameDet.DesiredTimeTo+ "  has been received. of $(Reservation Fee Amount) and your Deposit Fee in the Amount of $(Deposit Fee Amount) for your reservation of the (Reservation) on (Reservation Date) from (Reservation Time From) to (Reservation Time To) has been received. Your Reservation is now confirmed. Please print the attached “Clubhouse/Licensed Space Agreement” for your records. Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your refund will be processed within 7-10 days. After the 3-day deadline, your reservation fee will not be refunded.” . Please print the attached 	“Clubhouse/Licensed Space Agreement” for your records.   Please note you can cancel your reservation online free of charge up to 3 Business Days prior to the date of your event. Your 	refund will be processed within 7-10 days.  After the 3-day deadline, your reservation fee will not be refunded.” </p>");
                    //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                    body = reportHTML;

                    new EmailSendModel().SendEmail(GetTenantData.Email, "Amenity Reservation Deposit Paid", body);
                    message = "Your Reservation Deposit payment has been received. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(phonenumber))
                        {
                            new TwilioService().SMS(phonenumber, message);
                        }
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
        public string SaveServiceTransaction(MyTransactionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            // var GetProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
            var tenentUID = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var GetApplicantData = db.tbl_Applicant.Where(c => c.UserID == tenentUID).FirstOrDefault();
            var GePropertyData = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();

            var UserData = db.tbl_TenantOnline.Where(p => p.ParentTOID == tenentUID).FirstOrDefault();


            var GetPayDetails = db.tbl_OnlinePayment.Where(P => P.ID == model.PAID).FirstOrDefault();

            string decrytpedPMID = new EncryptDecrypt().DecryptText(GetPayDetails.PaymentID);


            decimal processingFees = 0;

            if (GePropertyData != null)
            {
                processingFees = GePropertyData.ProcessingFees ?? 0;
            }

            string transStatus = "";
            //string cardaccnum = "";
            //model.Email = GetApplicantData.Email;
            //model.ProcessingFees = processingFees;

            transStatus = new UsaePayWSDLModel().PayUsingCustomerNum(GetApplicantData.CustID, decrytpedPMID, Convert.ToDecimal(model.Charge_Amount), model.Description);



            String[] spearator = { "|" };
            String[] strlist = transStatus.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            if (strlist[1] != "000000")
            {
                var saveTransaction = new tbl_Transaction()
                {
                    TenantID = model.TenantID,
                    PAID = model.Transaction_Type,
                    Transaction_Date = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Credit_Amount = model.Charge_Amount,
                    Description = model.Description + " | TransID: " + strlist[2],
                    Charge_Date = DateTime.Now,
                    Charge_Type = 4,
                    Authcode = strlist[1],
                    Charge_Amount = model.Charge_Amount,
                    Accounting_Date = DateTime.Now,
                    Batch = model.Batch,
                    CreatedBy = Convert.ToInt32(model.UserId),
                    UserID = Convert.ToInt32(model.UserId),
                    RefNum = strlist[2],
                };
                db.tbl_Transaction.Add(saveTransaction);
                db.SaveChanges();
                var TransId = saveTransaction.TransID;

                var serEstDet = db.tbl_Estimate.Where(p => p.EID == model.EID).FirstOrDefault();
                var GetTenantData = db.tbl_TenantInfo.Where(p => p.TenantID == model.TenantID).FirstOrDefault();

                string reportHTML = "";
                string body = "";

                string filePath = HttpContext.Current.Server.MapPath("~/Content/Template/");
                //reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");

                reportHTML = reportHTML.Replace("[%ServerURL%]", ServerURL);
                reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                string message = "";
                string phonenumber = GetTenantData.Mobile;


                CreateTransBill(TransId, Convert.ToDecimal(serEstDet.Amount), model.Description + " Charges");

                serEstDet.Status = "4";
                db.SaveChanges();

                string emailBody = "";
                emailBody += "<p style=\"margin-bottom: 0px;\">Service Repair Charges Paid</p>";
                emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + GetTenantData.FirstName + " " + GetTenantData.LastName + "</p>";
                emailBody += "<p style=\"margin-bottom: 0px;\">Your Service Repair charges in the Amount of $" + serEstDet.Amount + " for your service of the " + serEstDet.Description + " on " + serEstDet.CreatedDate.Value.ToString("MM/dd/yyyy") + " has been received. Your Service Repair is now confirmed.</p>";
                reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);


                //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Service Repair Charges Paid");
                //reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantData.FirstName + " " + GetTenantData.LastName);
                //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Your Service Repair charges in the Amount of $" + serEstDet.Amount + " for your service of the " + serEstDet.Description + " on " + serEstDet.CreatedDate.Value.ToString("MM/dd/yyyy") + " has been received. Your Service Repair is now confirmed.” </p>");
                //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");
                body = reportHTML;

                new EmailSendModel().SendEmail(GetTenantData.Email, "Service Repair Charges Paid", body);

                message = "Service Repair payment has been received. Please check the email for detail.";
                if (SendMessage == "yes")
                {
                    if (!string.IsNullOrWhiteSpace(phonenumber))
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