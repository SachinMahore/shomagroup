using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ShomaRM.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class CommunityActivityModel
    {
        public long CID { get; set; }
        public Nullable<long> TenantId { get; set; }
        public string Details { get; set; }
        public string AttatchFile { get; set; }
        public string AttachFileOriginalName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public void DeleteCommunityPost(int CID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var CommunityPost = db.tbl_CommunityActivity.Where(p => p.CID == CID).FirstOrDefault();
            if (CommunityPost != null)
            {
                db.tbl_CommunityActivity.Remove(CommunityPost);
                db.SaveChanges();
            }

        }

        public class CommunityActivitySearchModel
        {

            public long CID { get; set; }
            public long TenantId { get; set; }
            public string Details { get; set; }
            public string AttatchFile { get; set; }
            public string AttachFileOriginalName { get; set; }
            public string PostedDate { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
            public string PostedBy { get; set; }
            public string NumberOfPages { get; set; }
        }

        public int BuildPaganationCommunityList(CommunityActivitySearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCommunityPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
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
        public List<CommunityActivitySearchModel> FillCommunitySearchGrid(CommunityActivitySearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CommunityActivitySearchModel> lstCommunity = new List<CommunityActivitySearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCommunityPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
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
                    CommunityActivitySearchModel searchmodel = new CommunityActivitySearchModel();

                    searchmodel.CID = Convert.ToInt64(dr["CID"].ToString());
                    searchmodel.TenantId = Convert.ToInt64(dr["TenantId"].ToString());
                    searchmodel.Details = dr["Details"].ToString();
                    searchmodel.AttatchFile = dr["AttatchFile"].ToString();
                    searchmodel.AttachFileOriginalName = dr["AttachFileOriginalName"].ToString();
                    searchmodel.PostedBy = dr["PostedBy"].ToString();
                    searchmodel.PostedDate = dr["PostedDate"].ToString();
                    searchmodel.NumberOfPages = dr["NumberOfPages"].ToString();

                    lstCommunity.Add(searchmodel);
                }
                db.Dispose();
                return lstCommunity.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}