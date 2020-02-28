using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class CountryModel
    {
        public long ID { get; set; }
        public string CountryName { get; set; }

        public string SaveUpdateCountry(CountryModel model)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.ID == 0)
            {
                var saveCountry = new tbl_Country()
                {
                    CountryName = model.CountryName,
                };
                db.tbl_Country.Add(saveCountry);
                db.SaveChanges();
                msg = "Progress Saved Successfully";
            }
            else
            {
                var updateCountry = db.tbl_Country.Where(co => co.ID == model.ID).FirstOrDefault();
                if (updateCountry != null)
                {
                    updateCountry.CountryName = model.CountryName;

                    db.SaveChanges();
                    msg = "Progress Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public int BuildPaganationCountryList(CountryList model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<CountryList> lstCountry = new List<CountryList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCountryPaginationAndSearchData";
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

        public List<CountryList> GetCountryList(CountryList model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CountryList> lstCountry = new List<CountryList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCountryPaginationAndSearchData";
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
                    CountryList usm = new CountryList();
                    usm.ID = int.Parse(dr["ID"].ToString());
                    usm.CountryName = dr["CountryName"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstCountry.Add(usm);
                }
                db.Dispose();
                return lstCountry.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public CountryModel GetCountryInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CountryModel model = new CountryModel();
            model.ID = 0;
            model.CountryName = "";

            var countryInfo = db.tbl_Country.Where(p => p.ID == ID).FirstOrDefault();
            if (countryInfo != null)
            {
                model.ID = countryInfo.ID;
                model.CountryName = countryInfo.CountryName;
            }
            return model;
        }

        public string DeleteCountry(long CountryId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (CountryId != 0)
            {

                var countryData = db.tbl_Country.Where(p => p.ID == CountryId).FirstOrDefault();
                if (countryData != null)
                {
                    db.tbl_Country.Remove(countryData);
                    db.SaveChanges();
                    msg = "Progress Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }

    public class CountryList
    {
        public long ID { get; set; }
        public string CountryName { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfRows { get; set; }
        public string Criteria { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}