using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class ChargeTypeModel
    {
        public long CTID { get; set; }
        public string Charge_Type { get; set; }
        public string Summary_Charge_Type { get; set; }
        public string Charge_Description { get; set; }
        public string Charge_Reference { get; set; }
        public string Payment_Description { get; set; }
        public string Payment_Reference { get; set; }
        public string Revenue_Account { get; set; }
        public string Receivable_Account { get; set; }
        public string Prepayment_Account { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Time_Stamp { get; set; }
        public short Concession { get; set; }
        public List<ChargeTypeModel> GetChargeTypeList(string ChargeType)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ChargeTypeModel> model = new List<ChargeTypeModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetChargeTypeList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = ChargeType;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ChargeTypeModel usm = new ChargeTypeModel();
                    usm.CTID = int.Parse(dr["CTID"].ToString());
                    usm.Charge_Type = dr["Charge_Type"].ToString();
                    usm.Charge_Description = dr["Charge_Description"].ToString();
                    model.Add(usm);
                }
                db.Dispose();
                return model;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public ChargeTypeModel GetChargeTypeInfo(int CTID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ChargeTypeModel model = new ChargeTypeModel();
            model.CTID = 0;
            model.Charge_Type = "";
            model.Charge_Description = "";

            var ChargeTypeInfo = db.tbl_ChargeType.Where(p => p.CTID == CTID).FirstOrDefault();
            if (ChargeTypeInfo != null)
            {
                model.CTID = ChargeTypeInfo.CTID;
                model.Charge_Type = ChargeTypeInfo.Charge_Type;
                model.Charge_Description = ChargeTypeInfo.Charge_Description;
                model.Summary_Charge_Type = ChargeTypeInfo.Summary_Charge_Type;
                model.Revenue_Account = ChargeTypeInfo.Revenue_Account;
                model.Payment_Description = ChargeTypeInfo.Payment_Description;
            }

            return model;
        }
        public long SaveUpdateChargeType(ChargeTypeModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var ChargeTypeExists = db.tbl_ChargeType.Where(p => p.CTID != model.CTID && p.Charge_Type == model.Charge_Type).FirstOrDefault();
            if (ChargeTypeExists == null)
            {
                if (model.CTID == 0)
                {
                    var data = new tbl_ChargeType()
                    {
                        Charge_Type = model.Charge_Type,
                        Charge_Description = model.Charge_Description,
                    };
                    db.tbl_ChargeType.Add(data);
                    db.SaveChanges();
                    model.CTID = data.CTID;
                }
                else
                {
                    var ChargeTypeInfo = db.tbl_ChargeType.Where(p => p.CTID == model.CTID).FirstOrDefault();
                    if (ChargeTypeInfo != null)
                    {
                        ChargeTypeInfo.Charge_Type = model.Charge_Type;
                        ChargeTypeInfo.Charge_Description = model.Charge_Description;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.Charge_Type + " not exists in the system.");
                    }
                }

                return model.CTID;
            }
            else
            {
                throw new Exception(model.Charge_Type + " already exists in the system.");
            }
        }

        public List<ChargeTypeModel> GetCTypeList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ChargeTypeModel> lstCharge = new List<ChargeTypeModel>();
            var chargeList = db.tbl_ChargeType.ToList();
            foreach (var pl in chargeList)
            {
                lstCharge.Add(new ChargeTypeModel
                {
                    CTID = pl.CTID,
                    Charge_Type = pl.Charge_Type,

                });

            }
            return lstCharge;
        }
    }
}