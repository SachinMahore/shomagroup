using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class MarketSourceModel
    {
        public int AdID { get; set; }
        public string Advertiser { get; set; }

        public List<MarketSourceModel> GetMarketSourceList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MarketSourceModel> model = new List<MarketSourceModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_Advertiser";
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
                    MarketSourceModel usm = new MarketSourceModel();
                    usm.AdID = int.Parse(dr["AdID"].ToString());
                    usm.Advertiser = dr["Advertiser"].ToString();
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
        public MarketSourceModel GetMarketSourceInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MarketSourceModel model = new MarketSourceModel();
            model.AdID = 0;
            model.Advertiser = "";

            var MarketSourceInfo = db.tbl_Advertiser.Where(p => p.AdID == AdID).FirstOrDefault();
            if (MarketSourceInfo != null)
            {
                model.AdID = MarketSourceInfo.AdID;
                model.Advertiser = MarketSourceInfo.Advertiser;
            }

            return model;
        }
        public long SaveUpdateMarketSource(MarketSourceModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_Advertiser.Where(p => p.AdID != model.AdID && p.Advertiser == model.Advertiser).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.AdID == 0)
                {
                    var MarketSourceData = new tbl_Advertiser()
                    {
                        Advertiser = model.Advertiser
                    };
                    db.tbl_Advertiser.Add(MarketSourceData);
                    db.SaveChanges();
                    model.AdID = MarketSourceData.AdID;
                }
                else
                {
                    var MarketSourceInfo = db.tbl_Advertiser.Where(p => p.AdID == model.AdID).FirstOrDefault();
                    if (MarketSourceInfo != null)
                    {
                        MarketSourceInfo.Advertiser = model.Advertiser;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.Advertiser + " not exists in the system.");
                    }
                }

                return model.AdID;
            }
            else
            {
                throw new Exception(model.Advertiser + " already exists in the system.");
            }
        }
        public MarketSourceModel GetMarketSourceData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MarketSourceModel model = new MarketSourceModel();

            var GetMarketSourceData = db.tbl_Advertiser.Where(p => p.AdID == Id).FirstOrDefault();

            if (GetMarketSourceData != null)
            {
                model.AdID = GetMarketSourceData.AdID;
                model.Advertiser = GetMarketSourceData.Advertiser;
            }
            model.AdID = Id;
            return model;
        }
        public List<MarketSourceModel> GetMarketSourceSearchList(string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MarketSourceModel> model = new List<MarketSourceModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_Advertiser_SearchList";
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
                    MarketSourceModel usm = new MarketSourceModel();
                    usm.AdID = int.Parse(dr["AdID"].ToString());
                    usm.Advertiser = dr["Advertiser"].ToString();
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
        public int BuildPaganationMSList(MSListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenityList> lstAmenity = new List<AmenityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAdvertiserPaginationAndSearchData";
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
        public List<MSListModel> FillMSSearchGrid(MSListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<MSListModel> lstData = new List<MSListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAdvertiserPaginationAndSearchData";
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
                    MSListModel usm = new MSListModel();
                    usm.AdID = int.Parse(dr["AdID"].ToString());
                    usm.Advertiser = dr["Advertiser"].ToString();
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

        public string DeleteMarketSource(long ADID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (ADID != 0)
            {

                var advertiserData = db.tbl_Advertiser.Where(p => p.AdID == ADID).FirstOrDefault();
                if (advertiserData != null)
                {
                    db.tbl_Advertiser.Remove(advertiserData);
                    db.SaveChanges();
                    msg = "Marketing Source Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class MSListModel
    {
        public int AdID { get; set; }
        public string Advertiser { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
    }
}