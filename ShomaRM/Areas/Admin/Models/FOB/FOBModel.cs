using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class FOBModel
    {
        public long StorageID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string StorageName { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public string Description { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }

        public List<FOBModel> GetStorageList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FOBModel> model = new List<FOBModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_FOB";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //DbParameter paramC = cmd.CreateParameter();
                    //paramC.ParameterName = "Criteria";
                    //paramC.Value = MarketSourceName;
                    //cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    FOBModel usm = new FOBModel();
                    usm.StorageID = int.Parse(dr["StorageID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.StorageName = dr["StorageName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    model.Add(usm);
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public FOBModel GetStorageInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            FOBModel model = new FOBModel();
            model.StorageID = 0;
            model.StorageName = "";

            var StorageInfo = db.tbl_FOB.Where(p => p.StorageID == StorageID).FirstOrDefault();
            if (StorageInfo != null)
            {
                model.StorageID = StorageInfo.StorageID;
                model.PropertyID = StorageInfo.PropertyID;
                model.StorageName = StorageInfo.StorageName;
                model.Charges = StorageInfo.Charges;
                model.Description = StorageInfo.Description;
            }

            return model;
        }
        public long SaveUpdateStorage(FOBModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.StorageID == 0)
            {
                var StorageData = new tbl_FOB()
                {
                    PropertyID = model.PropertyID,
                    StorageName = model.StorageName,
                    Charges = model.Charges,
                    Description = model.Description
                };
                db.tbl_FOB.Add(StorageData);
                db.SaveChanges();
                model.StorageID = StorageData.StorageID;
            }
            else
            {
                var StorageInfo = db.tbl_FOB.Where(p => p.StorageID == model.StorageID).FirstOrDefault();
                if (StorageInfo != null)
                {
                    StorageInfo.PropertyID = model.PropertyID;
                    StorageInfo.StorageName = model.StorageName;
                    StorageInfo.Charges = model.Charges;
                    StorageInfo.Description = model.Description;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception(model.StorageName + " not exists in the system.");
                }
            }

            return model.StorageID;

        }
        public FOBModel GetStorageData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            FOBModel model = new FOBModel();

            var GetStorageData = db.tbl_FOB.Where(p => p.StorageID == Id).FirstOrDefault();

            if (GetStorageData != null)
            {
                model.StorageID = GetStorageData.StorageID;
                model.PropertyID = GetStorageData.PropertyID;
                model.StorageName = GetStorageData.StorageName;
                model.Charges = GetStorageData.Charges;
                model.Description = GetStorageData.Description;
            }
            model.StorageID = Id;
            return model;
        }
        public List<FOBModel> GetStorageSearchList(string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FOBModel> model = new List<FOBModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_Storage_SearchList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "SearchText";
                    paramC.Value = SearchText;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    FOBModel usm = new FOBModel();
                    usm.StorageID = int.Parse(dr["StorageID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.StorageName = dr["StorageName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    model.Add(usm);
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public int BuildPaganationStorageList(FOBListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetFOBPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

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
        public List<FOBListModel> FillStorageSearchGrid(FOBListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FOBListModel> lstData = new List<FOBListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetFOBPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    FOBListModel usm = new FOBListModel();
                    usm.StorageID = int.Parse(dr["StorageID"].ToString());
                    usm.StorageName = dr["StorageName"].ToString();
                    usm.PropertyID = long.Parse(dr["PropertyID"].ToString());
                    usm.Charges = decimal.Parse(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstData.Add(usm);
                }
                db.Dispose();
                return lstData.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public string DeleteFOB(long StorageID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (StorageID != 0)
            {

                var FOBData = db.tbl_FOB.Where(p => p.StorageID == StorageID).FirstOrDefault();
                if (FOBData != null)
                {
                    db.tbl_FOB.Remove(FOBData);
                    db.SaveChanges();
                    msg = "FOB Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }


    }
    public class FOBListModel
    {
        public long StorageID { get; set; }
        public long PropertyID { get; set; }
        public string StorageName { get; set; }
        public decimal Charges { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
    }
    public class TenantFOBModel
    {
        public long TSID { get; set; }
        public Nullable<long> StorageID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public List<TenantFOBModel> lstTStorage { get; set; }

        public string SaveUpdateTenantStorage(TenantFOBModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            decimal totalStorageAmt = 0;
            var TenantStorageData = db.tbl_TenantStorage.Where(p => p.TenantID == model.TenantID).ToList();
            db.tbl_TenantStorage.RemoveRange(TenantStorageData);
            db.SaveChanges();
            if (model.lstTStorage != null)
            {

                foreach (var cd in model.lstTStorage)
                {
                    var parkingdata = db.tbl_FOB.Where(p => p.StorageID == cd.StorageID).FirstOrDefault();
                    var cdData = new tbl_TenantStorage
                    {
                        StorageID = cd.StorageID,
                        TenantID = model.TenantID,
                        Charges = parkingdata.Charges,
                        CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
                    };
                    db.tbl_TenantStorage.Add(cdData);
                    db.SaveChanges();
                    totalStorageAmt = totalStorageAmt + Convert.ToDecimal(parkingdata.Charges);
                }

            }
            return totalStorageAmt.ToString();

        }
        public List<TenantFOBModel> GetTenantStorageList(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantFOBModel> model = new List<TenantFOBModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_TenantStorage";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "TenantId";
                    paramC.Value = TenantId;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantFOBModel usm = new TenantFOBModel();
                    usm.TSID = Convert.ToInt64(dr["TSID"].ToString());
                    usm.StorageID = Convert.ToInt64(dr["StorageID"].ToString());
                    usm.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    model.Add(usm);
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}