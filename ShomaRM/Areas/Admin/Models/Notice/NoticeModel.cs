using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class NoticeModel
    {
        public long NoticeID { get; set; }
        public Nullable<long> PID { get; set; }
        public Nullable<long> UID { get; set; }
        public Nullable<long> TID { get; set; }
        public Nullable<long> LeaseID { get; set; }
        public Nullable<int> Revision_Num { get; set; }
        public Nullable<System.DateTime> NoticeDate { get; set; }
        public string NoticeDateString { get; set; }
        public Nullable<System.DateTime> IntendedMoveOut { get; set; }
        public string TerminationReason { get; set; }
        public Nullable<System.DateTime> CancelNoticeDate { get; set; }
        public string TenantName { get; set; }
        public string PidString { get; set; }
        public string UidString { get; set; }

        public NoticeModel GetNoticeDeatails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            NoticeModel model = new NoticeModel();
            
                var getNoticeDet = db.tbl_Notice.Where(p => p.NoticeID == id).FirstOrDefault();
                if (getNoticeDet != null)
                {
                    
                    model.PID = getNoticeDet.PID;
                    model.UID = getNoticeDet.UID;
                    model.TID = getNoticeDet.TID;
                    model.LeaseID = getNoticeDet.LeaseID;
                    model.Revision_Num = getNoticeDet.Revision_Num;
                    model.NoticeDate = getNoticeDet.NoticeDate;
                    model.IntendedMoveOut = getNoticeDet.IntendedMoveOut;
                    model.CancelNoticeDate = getNoticeDet.CancelNoticeDate;
                    model.TerminationReason = getNoticeDet.TerminationReason;
                    model.NoticeID = id;
            }
           return model;
        }

        public string SaveUpdateNotice(NoticeModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            var leaseID = db.tbl_Lease.Where(p => p.TenantID == model.TID && p.Status == 1).FirstOrDefault();
            if (model.NoticeID == 0)
            {
                var saveNotice = new tbl_Notice()
                {
                    PID = model.PID,
                    UID = model.UID,
                    TID = model.TID,
                    LeaseID = leaseID.LID,
                    Revision_Num = model.Revision_Num,
                    NoticeDate = model.NoticeDate,
                    IntendedMoveOut = model.IntendedMoveOut,
                    CancelNoticeDate = model.CancelNoticeDate,
                    TerminationReason = model.TerminationReason,

                };
                db.tbl_Notice.Add(saveNotice);
                db.SaveChanges();


                msg = "Notice Saved Successfully";
            }
            else
            {
                var getNOdata = db.tbl_Notice.Where(p => p.NoticeID == model.NoticeID).FirstOrDefault();
                if (getNOdata != null)
                {

                    getNOdata.PID = model.PID;
                    getNOdata.UID = model.UID;
                    getNOdata.TID = model.TID;
                    getNOdata.LeaseID = leaseID.LID;
                    getNOdata.Revision_Num = model.Revision_Num;
                    getNOdata.NoticeDate = model.NoticeDate;
                    getNOdata.IntendedMoveOut = model.IntendedMoveOut;
                    getNOdata.CancelNoticeDate = model.CancelNoticeDate;
                    getNOdata.TerminationReason = model.TerminationReason;

                }
                db.SaveChanges();
                msg = "Notice Updated Successfully";
            }

            db.Dispose();
            return msg;
        }
        public List<NoticeModel> GetNoticeListData(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<NoticeModel> lstpr = new List<NoticeModel>();
            try
            {
                DataTable dtTable = new DataTable();
                dtTable.Clear();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetNoticeList";
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
                    NoticeModel pr = new NoticeModel();
                    DateTime? noticeDate = null;
                    try
                    {

                        noticeDate = Convert.ToDateTime(dr["NoticeDate"].ToString());
                    }
                    catch
                    {

                    }
                    pr.NoticeID = Convert.ToInt64(dr["NoticeID"].ToString());
                    pr.TenantName = dr["TenantName"].ToString();
                    pr.PidString = dr["PidString"].ToString();
                    pr.UidString = dr["UidString"].ToString();
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.NoticeDateString = noticeDate == null ? "" : noticeDate.Value.ToString("MM/dd/yyy");
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
        public List<NoticeModel> GetTenantNoticeList(DateTime FromDate, DateTime ToDate,long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<NoticeModel> lstpr = new List<NoticeModel>();
            try
            {
                DataTable dtTable = new DataTable();
                dtTable.Clear();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantNoticeList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);
                    
                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "TenantID";
                    paramTID.Value = TenantID;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    NoticeModel pr = new NoticeModel();
                    DateTime? noticeDate = null;
                    try
                    {

                        noticeDate = Convert.ToDateTime(dr["NoticeDate"].ToString());
                    }
                    catch
                    {

                    }
                    pr.NoticeID = Convert.ToInt64(dr["NoticeID"].ToString());
                    pr.TerminationReason = dr["TerminationReason"].ToString();                   
                    pr.Revision_Num = Convert.ToInt32(dr["Revision_Num"].ToString());
                    pr.NoticeDateString = noticeDate == null ? "" : noticeDate.Value.ToString("MM/dd/yyy");
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
        public int BuildPaganationNoticeList(NoticeSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetNoticePaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "FromDate";
                    paramDF.Value = model.FromDate;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "ToDate";
                    paramDT.Value = model.ToDate;
                    cmd.Parameters.Add(paramDT);

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
        public List<NoticeSearchModel> FillNoticeSearchGrid(NoticeSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<NoticeSearchModel> lstNotice = new List<NoticeSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetNoticePaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "FromDate";
                    paramDF.Value = model.FromDate;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "ToDate";
                    paramDT.Value = model.ToDate;
                    cmd.Parameters.Add(paramDT);

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
                    NoticeSearchModel searchmodel = new NoticeSearchModel();
                    searchmodel.NoticeID = Convert.ToInt64(dr["NoticeID"].ToString());
                    searchmodel.PropertyName = dr["PropertyName"].ToString();
                    searchmodel.TenantName = dr["TenantName"].ToString();
                    searchmodel.UnitName = dr["UnitName"].ToString();
                    searchmodel.Revision_Num = dr["Revision_Num"].ToString();
                    searchmodel.NoticeDate = dr["NoticeDate"].ToString();
                    lstNotice.Add(searchmodel);
                }
                db.Dispose();
                return lstNotice.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public class NoticeSearchModel
        {
            public long NoticeID { get; set; }
            public string PropertyName { get; set; }
            public string TenantName { get; set; }
            public string UnitName { get; set; }
            public string Revision_Num { get; set; }
            public string NoticeDate { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfPages { get; set; }
            public int NumberOfRows { get; set; }
        }
    }
}