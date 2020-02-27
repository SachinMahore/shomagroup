using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class ServiceIssueModel
    {
        public long IssueID { get; set; }
        public Nullable<long> ServiceIssueID { get; set; }
        public Nullable<long> CausingIssueID { get; set; }
        public string Issue { get; set; }


        public long SaveUpdateServiceIssue(ServiceIssueModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_Issue.Where(p => p.IssueID != model.IssueID && p.Issue == model.Issue).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.IssueID == 0)
                {
                    var IssueData = new tbl_Issue()
                    {
                        CausingIssueID = model.CausingIssueID,
                        ServiceIssueID = model.ServiceIssueID,
                        Issue = model.Issue,
                    };
                    db.tbl_Issue.Add(IssueData);
                    db.SaveChanges();
                    model.IssueID = IssueData.IssueID;
                }
                else
                {
                    var SIssueData = db.tbl_Issue.Where(p => p.IssueID == model.IssueID).FirstOrDefault();
                    if (SIssueData != null)
                    {
                        SIssueData.CausingIssueID = model.CausingIssueID;
                        SIssueData.ServiceIssueID = model.ServiceIssueID;
                        SIssueData.Issue = model.Issue;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.Issue + " not exists in the system.");
                    }
                }

                return model.IssueID;
            }
            else
            {
                throw new Exception(model.Issue + " already exists in the system.");
            }
        }
        public ServiceIssueModel GetServiceIssueInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServiceIssueModel model = new ServiceIssueModel();


            var Serissue = db.tbl_Issue.Where(p => p.IssueID == ID).FirstOrDefault();
            if (Serissue != null)
            {   model.IssueID = Serissue.IssueID;
                model.CausingIssueID = Serissue.CausingIssueID;
                model.ServiceIssueID = Serissue.ServiceIssueID;
                model.Issue = Serissue.Issue;
            }

            return model;
        }
        public int BuildPaganationSIList(SIListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<SIListModel> lstSCI = new List<SIListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServiceIssuePaginationAndSearchData";
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
        public List<SIListModel> FillSISSearchGrid(SIListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<SIListModel> lstData = new List<SIListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServiceIssuePaginationAndSearchData";
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
                    SIListModel usm = new SIListModel();
                    usm.IssueID = int.Parse(dr["IssueID"].ToString());
                    usm.CausingIssueID = int.Parse(dr["CausingIssueID"].ToString());
                    usm.CausingIssue = dr["CausingIssue"].ToString();
                    usm.ServiceIssueID = int.Parse(dr["ServiceIssueID"].ToString());
                    usm.ServiceIssue = dr["ServiceIssue"].ToString();
                    usm.Issue = dr["Issue"].ToString();
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
        public string DeleteService(long IssueID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (IssueID != 0)
            {
                var IssueIDData = db.tbl_Issue.Where(p => p.IssueID == IssueID).FirstOrDefault();
                if (IssueIDData != null)
                {
                    db.tbl_Issue.Remove(IssueIDData);
                    db.SaveChanges();
                    msg = "Service  Issue  Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    
}
    public class SIListModel
    {
        public int IssueID { get; set; }
        public int CausingIssueID { get; set; }
        public string CausingIssue { get; set; }
        public int ServiceIssueID { get; set; }
        public string ServiceIssue { get; set; }
        public string Issue { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
    }

}