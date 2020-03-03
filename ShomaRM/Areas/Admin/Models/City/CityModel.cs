using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Net;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class CityModel
    {
        public long ID { get; set; }
        public string CityName { get; set; }
        public long StateID { get; set; }

        public List<StateList> FillStateDropDownList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<StateList> model = new List<StateList>();
            var stateData = db.tbl_State.OrderBy(p => p.StateName);
            foreach (var state in stateData)
            {
                StateList sl = new StateList();
                sl.StateName = state.StateName;
                sl.ID = state.ID;
                model.Add(sl);
            }
            db.Dispose();
            return model.ToList();
        }
        public int BuildPaganationCityList(CityList model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<CityList> lstCity = new List<CityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCityPaginationAndSearchData";
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
        public List<CityList> GetCityList(CityList model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CityList> lstCity = new List<CityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCityPaginationAndSearchData";
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
                    CityList usm = new CityList();
                    usm.ID = int.Parse(dr["ID"].ToString());
                    usm.CityName = dr["CityName"].ToString();
                    usm.StateName = dr["StateName"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstCity.Add(usm);
                }
                db.Dispose();
                return lstCity.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<CityList> GetCityListbyState(int StateID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CityList> model = new List<CityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCityListbyState";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "StateID";
                    paramC.Value = StateID;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    CityList usm = new CityList();
                    usm.ID = int.Parse(dr["ID"].ToString());
                    usm.CityName = dr["CityName"].ToString();
                   
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
        public CityModel GetCityInfo(int CityID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CityModel model = new CityModel();
            model.ID = 0;
            model.CityName = "";
            model.StateID = 0;

            var cityInfo = db.tbl_City.Where(p => p.ID == CityID).FirstOrDefault();
            if (cityInfo != null)
            {
                model.ID = cityInfo.ID;
                model.CityName = cityInfo.CityName;
                model.StateID = cityInfo.StateID.HasValue ? cityInfo.StateID.Value : 0;
            }

            return model;
        }
        public long SaveUpdateCity(CityModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_City.Where(p => p.ID != model.ID && p.CityName == model.CityName).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.ID == 0)
                {
                    var userData = new tbl_City()
                    {
                        CityName = model.CityName,
                        StateID = model.StateID,
                    };
                    db.tbl_City.Add(userData);
                    db.SaveChanges();
                    model.ID = userData.ID;
                }
                else
                {
                    var cityInfo = db.tbl_City.Where(p => p.ID == model.ID).FirstOrDefault();
                    if (cityInfo != null)
                    {
                        cityInfo.CityName = model.CityName;
                        cityInfo.StateID = model.StateID;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.CityName + " not exists in the system.");
                    }
                }
                return model.ID;
            }
            else
            {
                throw new Exception(model.CityName + " already exists in the system.");
            }
        }
        public List<StateList> FillStateDropDownListByCountryID(long CID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<StateList> lstData = new List<StateList>();
            DataTable dtTable = new DataTable();
            try
            {
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_FillStateDropDownListByCountryID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "CID";
                    param4.Value = CID;
                    cmd.Parameters.Add(param4);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (System.Data.DataRow dr in dtTable.Rows)
                {
                    StateList model = new StateList();
                    model.ID = Convert.ToInt64(dr["ID"].ToString());
                    model.StateName = dr["StateName"].ToString();
                    lstData.Add(model);
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
        public List<CountyList> FillCountryDropDownList()
        {



            ShomaRMEntities db = new ShomaRMEntities();
            List<CountyList> lstData = new List<CountyList>();
            System.Data.DataTable dtTable = new System.Data.DataTable();
            try
            {
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCountryList";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    System.Data.Common.DbDataAdapter da = System.Data.Common.DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (System.Data.DataRow dr in dtTable.Rows)
                {
                    CountyList model = new CountyList();
                    model.ID = Convert.ToInt64(dr["ID"].ToString());
                    model.CountryName = dr["CountryName"].ToString();
                    lstData.Add(model);
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


        public string DeleteCity(long CityID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (CityID != 0)
            {

                var cityData = db.tbl_City.Where(p => p.ID == CityID).FirstOrDefault();
                if (cityData != null)
                {
                    db.tbl_City.Remove(cityData);
                    db.SaveChanges();
                    msg = "City Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class CityList
    {
        public long ID { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfRows { get; set; }
        public string Criteria { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
    public class CountyList
    {
        public long ID { get; set; }
        public string CountryName { get; set; }
    }
}