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
                  
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();
                  
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

                   
                    pr.Transaction_Type = dr["Transaction_Type"].ToString();
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();
                
                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
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
                 
                    pr.Transaction_Type = dr["Transaction_Type"].ToString();
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();
                  
                   
                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());
                
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
        public List<TransactionModel> GetAllTransactionListOP(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TransactionModel> lstpr = new List<TransactionModel>();

            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var appData = db.tbl_Applicant.Where(p => p.UserID == TenantID).FirstOrDefault();
            long appid = 0;
            if (appData != null)
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

                    pr.Transaction_Type = dr["Transaction_Type"].ToString();
                    pr.Transaction_DateString = transactiondateString == null ? "" : transactiondateString.ToString();


                    pr.CreatedDateString = createdDateString == null ? "" : createdDateString.ToString();
                    pr.Credit_Amount = Convert.ToDecimal(dr["Credit_Amount"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Charge_DateString = charge_DateString == null ? "" : charge_DateString.ToString();
                    pr.Charge_Type = dr["Charge_Type"].ToString();
                    pr.Charge_Amount = Convert.ToDecimal(dr["Charge_Amount"].ToString());

                    pr.ApplicantName = dr["ApplicantName"].ToString();
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
              
                model.Transaction_Type = getTransdata.PAID;
                model.Transaction_Date = getTransdata.Transaction_Date;
         
                model.CreatedDate = getTransdata.CreatedDate;
                model.Credit_Amount = getTransdata.Credit_Amount;
                model.Description = getTransdata.Description;
                model.Charge_Date = getTransdata.Charge_Date;
                model.Charge_Type = getTransdata.Charge_Type.ToString();
   
                model.Charge_Amount = getTransdata.Charge_Amount;
                model.Miscellaneous_Amount = getTransdata.Miscellaneous_Amount;
                model.Accounting_Date = getTransdata.Accounting_Date;
       
                model.Batch = getTransdata.Batch;

                model.CreatedBy = getTransdata.CreatedBy;
              

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
                
                    Transaction_Date = DateTime.Now,
              
                    CreatedDate = DateTime.Now,
                    Credit_Amount = 0,
                    Description = model.Description,
                    Charge_Date = model.Charge_Date,
                    Charge_Type = Convert.ToInt32(model.Charge_Type),
                 
                    Charge_Amount = model.Charge_Amount,
                    Miscellaneous_Amount = model.Miscellaneous_Amount,
                    Accounting_Date = DateTime.Now,
             
                    Batch = "1",
              
                    CreatedBy = userid,
                
                   UserID=userid,
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
           
                    getNOdata.PAID = model.Transaction_Type;
                    getNOdata.Transaction_Date = model.Transaction_Date;
             
                    getNOdata.CreatedDate = model.CreatedDate;
                    getNOdata.Credit_Amount = model.Credit_Amount;
                    getNOdata.Description = model.Description;
                    getNOdata.Charge_Date = model.Charge_Date;
                    getNOdata.Charge_Type = Convert.ToInt32(model.Charge_Type);
                  
                    getNOdata.Charge_Amount = model.Charge_Amount;
                    getNOdata.Miscellaneous_Amount = model.Miscellaneous_Amount;
                    getNOdata.Accounting_Date = model.Accounting_Date;
                 
                    getNOdata.Batch = model.Batch;
                   
         
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