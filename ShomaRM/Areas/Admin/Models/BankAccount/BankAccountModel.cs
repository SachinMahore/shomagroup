using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Models
{
    public class BankAccountModel
    {
        public int BAID { get; set; }
        public string Bank_Name { get; set; }
        public string Account_Number { get; set; }
        public Nullable<int> Account_Type { get; set; }
        public string Account_TypeText { get; set; }
        public int BuildPaganationBankAccountList(BankAccountSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetBankAccountPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "Filter";
                    param0.Value = model.Filter;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "Criteria";
                    param1.Value = string.IsNullOrEmpty(model.Criteria) ? "" : model.Criteria;
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
        public List<BankAccountSearchModel> GetBankAccountList(BankAccountSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<BankAccountSearchModel> lstBAL = new List<BankAccountSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetBankAccountPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "Filter";
                    param0.Value = model.Filter;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "Criteria";
                    param1.Value = string.IsNullOrEmpty(model.Criteria) ? "" : model.Criteria;
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
                    BankAccountSearchModel searchmodel = new BankAccountSearchModel();
                    searchmodel.BAID = Convert.ToInt32(dr["BAID"].ToString());
                    searchmodel.Bank_Name = dr["Bank_Name"].ToString();
                    searchmodel.Account_Number = dr["Account_Number"].ToString();
                    searchmodel.Account_TypeText = dr["Account_Type"].ToString();
                    lstBAL.Add(searchmodel);
                }
                db.Dispose();
                return lstBAL.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public class BankAccountSearchModel
        {
            public long BAID { get; set; }
            public string Bank_Name { get; set; }
            public string Account_TypeText { get; set; }
            public string Account_Number { get; set; }
            public string Filter { get; set; }
            public string Criteria { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
        public string SaveUpdateBankDetails(BankAccountModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            if (model.BAID == 0)
            {
                var saveBankDetails = new tbl_BankAccount()
                {
                    Bank_Name= model.Bank_Name,
                    Account_Number = model.Account_Number,
                    Account_Type = model.Account_Type,
                };
                db.tbl_BankAccount.Add(saveBankDetails);
                db.SaveChanges();
                msg = "Bank Details Saved Successfully";
            }
            else
            {
                var getBDdata = db.tbl_BankAccount.Where(p => p.BAID == model.BAID).FirstOrDefault();
                if (getBDdata != null)
                {
                    getBDdata.Bank_Name = model.Bank_Name;
                    getBDdata.Account_Number = model.Account_Number;
                    getBDdata.Account_Type = model.Account_Type;
                }
                db.SaveChanges();
                msg = "Bank Details Updated Successfully";
            }
            db.Dispose();
            return msg;
        }
        public BankAccountModel GetBankDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            BankAccountModel model = new BankAccountModel();

            var getBankdata = db.tbl_BankAccount.Where(p => p.BAID == id).FirstOrDefault();
            if (getBankdata != null)
            {
                model.Bank_Name = getBankdata.Bank_Name;
                model.Account_Number = getBankdata.Account_Number;
                model.Account_Type = getBankdata.Account_Type;
            }
            model.BAID = id;
            return model;
        }
        public List<BankAccountModel> FillBankAccountDropDownList(long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<BankAccountModel> lstBankAccount = new List<BankAccountModel>();
            var BankAccountList = db.tbl_BankAccount.Where(p => p.Account_Type == PID).ToList();
            foreach (var tl in BankAccountList)
            {
                lstBankAccount.Add(new BankAccountModel
                {
                    BAID = tl.BAID,
                    Bank_Name = tl.Bank_Name,
                    Account_Number = tl.Account_Number,
                });
            }
            return lstBankAccount;
        }
    }
}