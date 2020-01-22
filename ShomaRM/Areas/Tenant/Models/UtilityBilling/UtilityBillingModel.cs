using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Models;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Tenant.Models
{
    public class UtilityBillingModel
    {
        public Nullable<int> UBID { get; set; }
        public Nullable<int> UtilityID { get; set; }
        public Nullable<long> LeaseID { get; set; }
        public Nullable<int> Revision_Num { get; set; }
        public string Unit { get; set; }
        public string ChargeType { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public string EffectiveDateString { get; set; }
        public Nullable<decimal> MeterReading { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public Nullable<System.DateTime> Posted { get; set; }
        public long TenantID { get; set; }
        public string UtilityTitle { get; set; }
        public UtilityBillingModel GetUBData(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            UtilityBillingModel model =new UtilityBillingModel();
            var getUBDta = db.tbl_UtilityBilling.Where(p => p.UBID == id).FirstOrDefault();
            if (getUBDta != null)
            {
               
                model.UtilityID = getUBDta.UtilityID;
                model.LeaseID = getUBDta.LeaseID;
                model.Revision_Num = getUBDta.Revision_Num;
                model.Unit = getUBDta.Unit;
                model.ChargeType = getUBDta.ChargeType;
                model.EffectiveDate = getUBDta.EffectiveDate;
                model.MeterReading = getUBDta.MeterReading;
                model.PricePerUnit = getUBDta.PricePerUnit;
                model.Posted = getUBDta.Posted;
                model.TenantID = getUBDta.TenantID;
            }
            model.UBID = id;
            return model;
        }
        public string SaveUpdateUtilityBilling(UtilityBillingModel model)
        {

            string result = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.UBID == 0)
            {
                var saveWorkorder = new tbl_UtilityBilling()
                {
                    UtilityID = model.UtilityID,
                    LeaseID = model.LeaseID,
                    Revision_Num = model.Revision_Num,
                    Unit = model.Unit,
                    ChargeType = model.ChargeType,
                    EffectiveDate = model.EffectiveDate,
                    MeterReading = model.MeterReading,
                    PricePerUnit = model.PricePerUnit,
                    Posted = model.Posted,
                    TenantID = ShomaGroupWebSession.CurrentUser.TenantID,

                };
                db.tbl_UtilityBilling.Add(saveWorkorder);
                db.SaveChanges();
                result = "Utility Billing Saved Successfully";
            }
            else
            {
                var getUBDta = db.tbl_UtilityBilling.Where(p => p.UBID == model.UBID).FirstOrDefault();
                if (getUBDta != null)
                {
                    getUBDta.UtilityID = model.UtilityID;
                    getUBDta.LeaseID = model.LeaseID;
                    getUBDta.Revision_Num = model.Revision_Num;
                    getUBDta.Unit = model.Unit;
                    getUBDta.ChargeType = model.ChargeType;
                    getUBDta.EffectiveDate = model.EffectiveDate;
                    getUBDta.MeterReading = model.MeterReading;
                    getUBDta.PricePerUnit = model.PricePerUnit;
                    getUBDta.Posted = model.Posted;
                    getUBDta.TenantID = model.TenantID;
                }
                db.SaveChanges();
                result = "Utility Billing Updated Successfully";
            }

            db.Dispose();

            return result;
        }

        public List<UtilityBillingModel> GetUtilityBillingList(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<UtilityBillingModel> lstpr = new List<UtilityBillingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUtilityBillingList";
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
                    UtilityBillingModel pr = new UtilityBillingModel();

                    DateTime? effectiveDate = null;
                    try
                    {
                        
                        effectiveDate = Convert.ToDateTime(dr["EffectiveDate"].ToString());
                    }
                    catch
                    {

                    }
                    pr.UBID = Convert.ToInt32(dr["UBID"].ToString());
                    pr.UtilityTitle = dr["UtilityTitle"].ToString();
                    pr.UtilityID = Convert.ToInt32(dr["UtilityID"].ToString());
                    pr.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.Unit = dr["Unit"].ToString();
                    pr.ChargeType = dr["ChargeType"].ToString();
                    pr.EffectiveDateString = effectiveDate == null ? "" : effectiveDate.Value.ToString("MM/dd/yyy");
                    pr.MeterReading = Convert.ToDecimal(dr["MeterReading"].ToString());
                    pr.PricePerUnit = Convert.ToDecimal(dr["PricePerUnit"].ToString());
                    pr.Posted = Convert.ToDateTime(dr["Posted"].ToString());
                    pr.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
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
    }
}