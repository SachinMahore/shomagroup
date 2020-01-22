using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class PetManagementModel
    {
        public long PetPlaceID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PetPlace { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public string Description { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }

        public List<PetManagementModel> GetPetPlaceList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PetManagementModel> model = new List<PetManagementModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_PetPlace";
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
                    PetManagementModel usm = new PetManagementModel();
                    usm.PetPlaceID = int.Parse(dr["PetPlaceID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.PetPlace = dr["PetPlace"].ToString();
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
        public PetManagementModel GetPetPlaceInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PetManagementModel model = new PetManagementModel();
            model.PetPlaceID = 0;
            model.PetPlace = "";

            var PetPlaceInfo = db.tbl_PetPlace.Where(p => p.PetPlaceID == PetPlaceID).FirstOrDefault();
            if (PetPlaceInfo != null)
            {
                model.PetPlaceID = PetPlaceInfo.PetPlaceID;
                model.PropertyID = PetPlaceInfo.PropertyID;
                model.PetPlace = PetPlaceInfo.PetPlace;
                model.Charges = PetPlaceInfo.Charges;
                model.Description = PetPlaceInfo.Description;
            }

            return model;
        }
        public long SaveUpdatePetPlace(PetManagementModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            
                if (model.PetPlaceID == 0)
                {
                    var PetPlaceData = new tbl_PetPlace()
                    {
                        PropertyID = model.PropertyID,
                        PetPlace = model.PetPlace,
                        Charges = model.Charges,
                        Description = model.Description
                    };
                    db.tbl_PetPlace.Add(PetPlaceData);
                    db.SaveChanges();
                    model.PetPlaceID = PetPlaceData.PetPlaceID;
                }
                else
                {
                    var PetPlaceInfo = db.tbl_PetPlace.Where(p => p.PetPlaceID == model.PetPlaceID).FirstOrDefault();
                    if (PetPlaceInfo != null)
                    {
                        PetPlaceInfo.PropertyID = model.PropertyID;
                        PetPlaceInfo.PetPlace = model.PetPlace;
                        PetPlaceInfo.Charges = model.Charges;
                        PetPlaceInfo.Description = model.Description;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.PetPlace + " not exists in the system.");
                    }
                }

                return model.PetPlaceID;
            
        }
        public PetManagementModel GetPetPlaceData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PetManagementModel model = new PetManagementModel();

            var GetPetPlaceData = db.tbl_PetPlace.Where(p => p.PetPlaceID == Id).FirstOrDefault();

            if (GetPetPlaceData != null)
            {
                model.PetPlaceID = GetPetPlaceData.PetPlaceID;
                model.PropertyID = GetPetPlaceData.PropertyID;
                model.PetPlace = GetPetPlaceData.PetPlace;
                model.Charges = GetPetPlaceData.Charges;
                model.Description = GetPetPlaceData.Description;
            }
            model.PetPlaceID = Id;
            return model;
        }
        public List<PetManagementModel> GetPetPlaceSearchList(string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PetManagementModel> model = new List<PetManagementModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_PetPlace_SearchList";
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
                    PetManagementModel usm = new PetManagementModel();
                    usm.PetPlaceID = int.Parse(dr["PetPlaceID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.PetPlace = dr["PetPlace"].ToString();
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
        public int BuildPaganationPetPlaceList(PetPlaceListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPetPlacePaginationAndSearchData";
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
        public List<PetPlaceListModel> FillPetPlaceSearchGrid(PetPlaceListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PetPlaceListModel> lstData = new List<PetPlaceListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPetPlacePaginationAndSearchData";
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
                    PetPlaceListModel usm = new PetPlaceListModel();
                    usm.PetPlaceID = int.Parse(dr["PetPlaceID"].ToString());
                    usm.PetPlace = dr["PetPlace"].ToString();
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
        public void DeletePetPlacesDetails(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var PetPlaceInfo = db.tbl_PetPlace.Where(p => p.PetPlaceID == PetPlaceID).FirstOrDefault();
            if (PetPlaceInfo != null)
            {
                db.tbl_PetPlace.Remove(PetPlaceInfo);
                db.SaveChanges();
            }

        }
    }
    public class PetPlaceListModel
    {
        public long PetPlaceID { get; set; }
        public long PropertyID { get; set; }
        public string PetPlace { get; set; }
        public decimal Charges { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
    }

    public class TenantPetPlaceModel
    {
        public long TPetID { get; set; }
        public Nullable<long> PetPlaceID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public List<TenantPetPlaceModel> lstTPetPlace{ get; set; }

        public string SaveUpdateTenantPetPlace(TenantPetPlaceModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            decimal totalPetPlaceAmt = 0;
            var TenantStorageData = db.tbl_TenantPetPlace.Where(p => p.TenantID == model.TenantID).ToList();
            db.tbl_TenantPetPlace.RemoveRange(TenantStorageData);
            db.SaveChanges();
            if (model.lstTPetPlace != null)
            {
              
                foreach (var cd in model.lstTPetPlace)
                {
                    var parkingdata = db.tbl_PetPlace.Where(p => p.PetPlaceID == cd.PetPlaceID).FirstOrDefault();
                    var cdData = new tbl_TenantPetPlace
                    {
                        PetPlaceID = cd.PetPlaceID,
                        TenantID = model.TenantID,
                        Charges = parkingdata.Charges,
                        CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
                    };
                    db.tbl_TenantPetPlace.Add(cdData);
                    db.SaveChanges();
                    totalPetPlaceAmt = totalPetPlaceAmt + Convert.ToDecimal(parkingdata.Charges);
                }

            }
            return totalPetPlaceAmt.ToString();

        }
        public List<TenantPetPlaceModel> GetTenantPetPlaceList(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantPetPlaceModel> model = new List<TenantPetPlaceModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_TenantPetPlace";
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
                    TenantPetPlaceModel usm = new TenantPetPlaceModel();
                    usm.TPetID = Convert.ToInt64(dr["TPetID"].ToString());
                    usm.PetPlaceID = Convert.ToInt64(dr["PetPlaceID"].ToString());
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