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
    public class PromotionModel
    {
        public int PromotionID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyIDString { get; set; }
        public string PromotionTitle { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string StartDateText { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string EndDateText { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedDateText { get; set; }
        public Nullable<int> CreatedById { get; set; }

        public List<PromotionModel> GetPromotionList(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PromotionModel> lstpr = new List<PromotionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPromotionList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PromotionModel pr = new PromotionModel();

                    DateTime? startDate = null;
                    try
                    {
                        startDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? endDate = null;
                    try
                    {
                        endDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    }
                    catch
                    {

                    }

                    pr.PromotionID = Convert.ToInt32(dr["PromotionID"].ToString());
                    pr.PropertyIDString = dr["PropertyID"].ToString();
                    pr.PromotionTitle = dr["PromotionTitle"].ToString();
                    pr.StartDateText = startDate == null ? "" : startDate.ToString();
                    pr.EndDateText = endDate == null ? "" : endDate.ToString();
                    lstpr.Add(pr);
                }
                db.Dispose();
                return lstpr.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public List<PromotionModel> GetPromotionListDetails(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PromotionModel> lstpr = new List<PromotionModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {

                    FromDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    ToDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));

                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPromotionList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PromotionModel pr = new PromotionModel();
                    pr.PromotionTitle = dr["PromotionTitle"].ToString();
                    lstpr.Add(pr);
                }
                db.Dispose();
                return lstpr.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public PromotionModel GetPromotionDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PromotionModel model = new PromotionModel();
            var getPromotiondata = db.tbl_Promotion.Where(p => p.PromotionID == id).FirstOrDefault();
            if (getPromotiondata != null)
            {
                model.PropertyID = getPromotiondata.PropertyID;
                model.PromotionTitle = getPromotiondata.PromotionTitle;
                model.StartDate = getPromotiondata.StartDate;
                model.EndDate = getPromotiondata.EndDate;
            }
            model.PromotionID = id;
            return model;
        }


        public string SaveUpdatePromotion(PromotionModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.PromotionID == 0)
            {
                var savePromotion = new tbl_Promotion()
                {
                    PromotionID = model.PromotionID,
                    PropertyID = model.PropertyID,
                    PromotionTitle = model.PromotionTitle,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")),
                    CreatedById = userid
                };
                db.tbl_Promotion.Add(savePromotion);
                db.SaveChanges();
                msg = "Promotion Details Saved Successfully";
            }
            else
            {
                var getNOdata = db.tbl_Promotion.Where(p => p.PromotionID == model.PromotionID).FirstOrDefault();
                if (getNOdata != null)
                {
                    getNOdata.PromotionID = model.PromotionID;
                    getNOdata.PropertyID = model.PropertyID;
                    getNOdata.PromotionTitle = model.PromotionTitle;
                    getNOdata.StartDate = model.StartDate;
                    getNOdata.EndDate = model.EndDate;
                    getNOdata.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    getNOdata.CreatedById = userid;
                }
                db.SaveChanges();
                msg = "Promotion Details Updated Successfully";
            }
            db.Dispose();
            return msg;
        }

        public int BuildPaganationPromotionList(PromotionListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPromotionPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramSD = cmd.CreateParameter();
                    paramSD.ParameterName = "StartDate";
                    paramSD.Value = !string.IsNullOrEmpty(model.StartDate) ? model.StartDate : DateTime.Now.ToString("MM/dd/yyyy");
                    cmd.Parameters.Add(paramSD);

                    DbParameter paramED = cmd.CreateParameter();
                    paramED.ParameterName = "EndDate";
                    paramED.Value =!string.IsNullOrEmpty( model.EndDate)?model.EndDate:DateTime.Now.ToString("MM/dd/yyyy");
                    cmd.Parameters.Add(paramED);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows==0?25:model.NumberOfRows;
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
        public List<PromotionListModel> FillPromotionSearchGrid(PromotionListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PromotionListModel> lstData = new List<PromotionListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPromotionPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramSD = cmd.CreateParameter();
                    paramSD.ParameterName = "StartDate";
                    paramSD.Value = !string.IsNullOrEmpty(model.StartDate) ? model.StartDate : DateTime.Now.ToString("MM/dd/yyyy");
                    cmd.Parameters.Add(paramSD);

                    DbParameter paramED = cmd.CreateParameter();
                    paramED.ParameterName = "EndDate";
                    paramED.Value = !string.IsNullOrEmpty(model.EndDate) ? model.EndDate : DateTime.Now.ToString("MM/dd/yyyy");
                    cmd.Parameters.Add(paramED);

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
                    PromotionListModel searchmodel = new PromotionListModel();

                    DateTime? startDate = null;
                    try
                    {
                        startDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? endDate = null;
                    try
                    {
                        endDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    }
                    catch
                    {

                    }

                    searchmodel.PromotionID = Convert.ToInt32(dr["PromotionID"].ToString());
                    searchmodel.PropertyID =long.Parse( dr["PropertyID"].ToString());
                    searchmodel.PropertyName = dr["PropertyName"].ToString();
                    searchmodel.PromotionTitle = dr["PromotionTitle"].ToString();
                    searchmodel.StartDate = startDate == null ? "" : startDate.Value.ToString("MM/dd/yyyy");
                    searchmodel.EndDate = endDate == null ? "" : endDate.Value.ToString("MM/dd/yyyy");
                    lstData.Add(searchmodel);
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
    public class PromotionListModel
    {
        public int PromotionID { get; set; }
        public long PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string PromotionTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
    }
}
