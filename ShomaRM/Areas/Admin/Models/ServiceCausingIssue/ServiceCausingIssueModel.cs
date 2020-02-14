using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class ServiceCausingIssueModel
    {
        public long CausingIssueID { get; set; }
        public string CausingIssue { get; set; }
        public Nullable<long> ServiceIssueID { get; set; }
        public string ServiceIssue { get; set; }


        public int BuildPaganationSLList(SCIListModel model)
    {
        int NOP = 0;
        ShomaRMEntities db = new ShomaRMEntities();
        List<SCIListModel> lstSCI = new List<SCIListModel>();
        try
        {
            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = "usp_GetServiceCausingIssuePaginationAndSearchData";
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
        public List<SCIListModel> FillSCISSearchGrid(SCIListModel model)
    {
        ShomaRMEntities db = new ShomaRMEntities();
        List<SCIListModel> lstData = new List<SCIListModel>();
        try
        {
            DataTable dtTable = new DataTable();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = "usp_GetServiceCausingIssuePaginationAndSearchData";
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
                SCIListModel usm = new SCIListModel();
                usm.CausingIssueID = int.Parse(dr["CausingIssueID"].ToString());
                usm.CausingIssue = dr["CausingIssue"].ToString();
                usm.ServiceIssueID = int.Parse(dr["ServiceIssueID"].ToString());
                usm.ServiceIssue = dr["ServiceIssue"].ToString();
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
    }
    public class SCIListModel
    {
        public int CausingIssueID { get; set; }
        public string CausingIssue { get; set; }
        public int ServiceIssueID { get; set; }
        public string ServiceIssue { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
    }

}