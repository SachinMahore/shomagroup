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


        public long SaveUpdateServiceCausingIssue(ServiceCausingIssueModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_CausingIssue.Where(p => p.CausingIssueID != model.CausingIssueID && p.CausingIssue == model.CausingIssue).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.CausingIssueID == 0)
                {
                    var CausingData = new tbl_CausingIssue()
                    {
                        CausingIssue = model.CausingIssue,
                        ServiceIssueID = model.ServiceIssueID,
                    };
                    db.tbl_CausingIssue.Add(CausingData);
                    db.SaveChanges();
                    model.CausingIssueID = CausingData.CausingIssueID;
                }
                else
                {
                    var ScausingData = db.tbl_CausingIssue.Where(p => p.CausingIssueID == model.CausingIssueID).FirstOrDefault();
                    if (ScausingData != null)
                    {
                        ScausingData.CausingIssue = model.CausingIssue;
                        ScausingData.ServiceIssueID = model.ServiceIssueID;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.CausingIssue + " not exists in the system.");
                    }
                }

                return model.CausingIssueID;
            }
            else
            {
                throw new Exception(model.CausingIssue + " already exists in the system.");
            }
        }

        public ServiceCausingIssueModel GetServiceCausingInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServiceCausingIssueModel model = new ServiceCausingIssueModel();


            var servicecausing = db.tbl_CausingIssue.Where(p => p.CausingIssueID == ID).FirstOrDefault();
            if (servicecausing != null)
            {
                model.CausingIssueID = servicecausing.CausingIssueID;
                model.ServiceIssueID = servicecausing.ServiceIssueID;
                model.CausingIssue = servicecausing.CausingIssue;
            }

            return model;
        }
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