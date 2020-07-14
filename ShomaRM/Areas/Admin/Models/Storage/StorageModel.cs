using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class StorageModel
    {
        public long StorageID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string StorageName { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public string Description { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }

        public List<StorageModel> GetStorageList(long TenantID, string OrderBy, string SortBy)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<StorageModel> model = new List<StorageModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_Storage";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbParameter paramOB = cmd.CreateParameter();
                    paramOB.ParameterName = "OrderBy";
                    paramOB.Value = OrderBy;
                    cmd.Parameters.Add(paramOB);

                    DbParameter paramSB = cmd.CreateParameter();
                    paramSB.ParameterName = "SortBy";
                    paramSB.Value = SortBy;
                    cmd.Parameters.Add(paramSB);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    StorageModel usm = new StorageModel();
                    usm.StorageID = int.Parse(dr["StorageID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.StorageName = dr["StorageName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    usm.Type = int.Parse(dr["Type"].ToString());
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
        
        public StorageModel GetStorageInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            StorageModel model = new StorageModel();
            model.StorageID = 0;
            model.StorageName = "";

            var StorageInfo = db.tbl_Storage.Where(p => p.StorageID == StorageID).FirstOrDefault();
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
        public long SaveUpdateStorage(StorageModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
                if (model.StorageID != 0)
                {
                    var StorageInfo = db.tbl_Storage.Where(p => p.StorageID == model.StorageID).FirstOrDefault();
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
        public StorageModel GetStorageData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            StorageModel model = new StorageModel();

            var GetStorageData = db.tbl_Storage.Where(p => p.StorageID == Id).FirstOrDefault();

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
        public List<StorageModel> GetStorageSearchList(string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<StorageModel> model = new List<StorageModel>();
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
                    StorageModel usm = new StorageModel();
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
        public int BuildPaganationStorageList(StorageListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetStoragePaginationAndSearchData";
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

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

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
        public List<StorageListModel> FillStorageSearchGrid(StorageListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<StorageListModel> lstData = new List<StorageListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetStoragePaginationAndSearchData";
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

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    StorageListModel usm = new StorageListModel();
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


        public string DeleteStorage(long StorageID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (StorageID != 0)
            {

                var StorageData = db.tbl_Storage.Where(p => p.StorageID == StorageID).FirstOrDefault();
                if (StorageData != null)
                {
                    db.tbl_Storage.Remove(StorageData);
                    db.SaveChanges();
                    msg = "Storage Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class StorageListModel
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
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
    public class TenantStorageModel
    {
        public long TSID { get; set; }
        public Nullable<long> StorageID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public List<TenantStorageModel> lstTStorage { get; set; }

        public string SaveUpdateTenantStorage(TenantStorageModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            decimal totalStorageAmt = 0;
            string result = "";
            if (model.lstTStorage != null)

            {
                // check storage available //
                long storageid = model.lstTStorage[0].StorageID ?? 0;

                var storagedata = db.tbl_Storage.Where(p => p.StorageID == storageid).FirstOrDefault();
                var isStorageAvailable = db.tbl_TenantStorage.Where(p => p.StorageID == storageid && TenantID != model.TenantID).FirstOrDefault();
                if (isStorageAvailable != null)
                {
                    result = "0|" + storagedata.StorageName + " - " + storagedata.Description + " is not available.<br/>Please select other storage unit.|0.00";
                }
                // check storage available //
                else
                {
                    var TenantStorageData = db.tbl_TenantStorage.Where(p => p.TenantID == model.TenantID).ToList();
                    db.tbl_TenantStorage.RemoveRange(TenantStorageData);
                    db.SaveChanges();

                    foreach (var cd in model.lstTStorage)
                    {
                        var cdData = new tbl_TenantStorage
                        {
                            StorageID = cd.StorageID,
                            TenantID = model.TenantID,
                            Charges = storagedata.Charges,
                            CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
                        };
                        db.tbl_TenantStorage.Add(cdData);
                        db.SaveChanges();
                        totalStorageAmt = totalStorageAmt + Convert.ToDecimal(storagedata.Charges);
                    }

                    result = "1|Progress saved|" + totalStorageAmt.ToString();
                }
            }
            else
            {
                var TenantStorageData = db.tbl_TenantStorage.Where(p => p.TenantID == model.TenantID).ToList();
                db.tbl_TenantStorage.RemoveRange(TenantStorageData);
                db.SaveChanges();
                result = "1|Progress saved|0.00";
            }
            return result; 
            
        }
        public List<TenantStorageModel> GetTenantStorageList(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantStorageModel> model = new List<TenantStorageModel>();
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
                    TenantStorageModel usm = new TenantStorageModel();
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