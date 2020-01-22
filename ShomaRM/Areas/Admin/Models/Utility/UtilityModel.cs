using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class UtilityModel
    {
        public int UtilityID { get; set; }
        public string UtilityTitle { get; set; }
        public string UtilityDetails { get; set; }
        public int BuildPaganationUtilityList(UtilityList model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<UtilityList> lstUtility = new List<UtilityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUtilityPaginationAndSearchData";
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
                if (dtTable.Rows.Count>0)
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
        public List<UtilityList> GetUtilityList(UtilityList model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<UtilityList> lstUtility = new List<UtilityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUtilityPaginationAndSearchData";
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
                    UtilityList usm = new UtilityList();
                    usm.UtilityID = int.Parse(dr["UtilityID"].ToString());
                    usm.UtilityTitle = dr["UtilityTitle"].ToString();
                    usm.UtilityDetails = dr["UtilityDetails"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstUtility.Add(usm);
                }
                db.Dispose();
                return lstUtility.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<UtilityList> GetUtilityDDLList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<UtilityList> lstUtility = new List<UtilityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUtilityPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = "";
                    cmd.Parameters.Add(paramC);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = 1;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value =2;
                    cmd.Parameters.Add(paramNOR);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    UtilityList usm = new UtilityList();
                    usm.UtilityID = int.Parse(dr["UtilityID"].ToString());
                    usm.UtilityTitle = dr["UtilityTitle"].ToString();
                    usm.UtilityDetails = dr["UtilityDetails"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstUtility.Add(usm);
                }
                db.Dispose();
                return lstUtility.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public UtilityModel GetUtilityInfo(int UtilityID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            UtilityModel model = new UtilityModel();
            model.UtilityID = 0;
            model.UtilityTitle = "";
            model.UtilityDetails = "";

            var UtilityInfo = db.tbl_Utility.Where(p => p.UtilityID == UtilityID).FirstOrDefault();
            if (UtilityInfo != null)
            {
                model.UtilityID = UtilityInfo.UtilityID;
                model.UtilityTitle = UtilityInfo.UtilityTitle;
                model.UtilityDetails = UtilityInfo.UtilityDetails;
            }

            return model;
        }
        public long SaveUpdateUtility(UtilityModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var utilityExists = db.tbl_Utility.Where(p => p.UtilityID != model.UtilityID && p.UtilityTitle == model.UtilityTitle).FirstOrDefault();
            if (utilityExists == null)
            {
                if (model.UtilityID == 0)
                {
                    var data = new tbl_Utility()
                    {
                        UtilityTitle = model.UtilityTitle,
                        UtilityDetails = model.UtilityDetails,
                    };
                    db.tbl_Utility.Add(data);
                    db.SaveChanges();
                    model.UtilityID = data.UtilityID;
                }
                else
                {
                    var utilityInfo = db.tbl_Utility.Where(p => p.UtilityID == model.UtilityID).FirstOrDefault();
                    if (utilityInfo != null)
                    {
                        utilityInfo.UtilityTitle = model.UtilityTitle;
                        utilityInfo.UtilityDetails = model.UtilityDetails;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.UtilityTitle + " not exists in the system.");
                    }
                }

                return model.UtilityID;
            }
            else
            {
                throw new Exception(model.UtilityTitle + " already exists in the system.");
            }
        }

        public string DeleteUtility(long UtilityID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (UtilityID != 0)
            {

                var utilityData = db.tbl_Utility.Where(p => p.UtilityID == UtilityID).FirstOrDefault();
                if (utilityData != null)
                {
                    db.tbl_Utility.Remove(utilityData);
                    db.SaveChanges();
                    msg = "Utility Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class UtilityList
    {
        public long UtilityID { get; set; }
        public string UtilityTitle { get; set; }
        public string UtilityDetails { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfRows { get; set; }
        public String Criteria { get; set; }
    }
}