using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class ServiceCategoryModel
    {
        public int ServiceIssueID { get; set; }
        public string ServiceIssue { get; set; }

        public int BuildPaganationSCList(SCListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<SLListModel> lstAmenity = new List<SLListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_TestSpSearchData";
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

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = model.SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = model.OrderBy;
                    cmd.Parameters.Add(param6);

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
        public List<SCListModel> FillSCSearchGrid(SCListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<SCListModel> lstData = new List<SCListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_TestSpSearchData";
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

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = model.SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = model.OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    SCListModel usm = new SCListModel();
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
        public long SaveUpdateServiceCategory(ServiceCategoryModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_ServiceIssue.Where(p => p.ServiceIssueID != model.ServiceIssueID && p.ServiceIssue == model.ServiceIssue).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.ServiceIssueID == 0)
                {
                    var ServiceData = new tbl_ServiceIssue()
                    {
                        ServiceIssue = model.ServiceIssue
                    };
                    db.tbl_ServiceIssue.Add(ServiceData);
                    db.SaveChanges();
                    model.ServiceIssueID = ServiceData.ServiceIssueID;
                }
                else
                {
                    var ServiceIssueData = db.tbl_ServiceIssue.Where(p => p.ServiceIssueID == model.ServiceIssueID).FirstOrDefault();
                    if (ServiceIssueData != null)
                    {
                        ServiceIssueData.ServiceIssue = model.ServiceIssue;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.ServiceIssue + " not exists in the system.");
                    }
                }

                return model.ServiceIssueID;
            }
            else
            {
                throw new Exception(model.ServiceIssue + " already exists in the system.");
            }
        }
        public ServiceCategoryModel GetServiceCategoryInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServiceCategoryModel model = new ServiceCategoryModel();


            var ServiceCategory = db.tbl_ServiceIssue.Where(p => p.ServiceIssueID == ID).FirstOrDefault();
            if (ServiceCategory != null)
            {
                model.ServiceIssueID = ServiceCategory.ServiceIssueID;
                model.ServiceIssue = ServiceCategory.ServiceIssue;
            }

            return model;
        }
        public string DeleteServiceCategory(long ServiceIssueID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (ServiceIssueID != 0)
            {
                var serviceData = db.tbl_ServiceIssue.Where(p => p.ServiceIssueID == ServiceIssueID).FirstOrDefault();
                if (serviceData != null)
                {
                    db.tbl_ServiceIssue.Remove(serviceData);
                    db.SaveChanges();
                    msg = "Service Category Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class SCListModel
    {
        public int ServiceIssueID { get; set; }
        public string ServiceIssue { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}