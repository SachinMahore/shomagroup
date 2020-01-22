using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class InvoiceModel
    {
        public long InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public string Vendor { get; set; }
        public string InvoiceDesc { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceDateString { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public string ReceivedDateString { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string PaymentDateString { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Route { get; set; }
        public Nullable<short> ExportNow { get; set; }
        public Nullable<System.DateTime> ExportedDate { get; set; }
        public string ExportedDateString { get; set; }
        public Nullable<int> ExportedBy { get; set; }
        public Nullable<System.DateTime> Approved { get; set; }
        public string ApprovedString { get; set; }
        public string ApprovedBy { get; set; }
        public string BatchID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string SaveUpdateInvoice(InvoiceModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.InvoiceID == 0)
            {
                var saveInvoice = new tbl_Invoice()
                {
                    InvoiceNumber = model.InvoiceNumber,
                    Vendor = model.Vendor,
                    InvoiceDesc = model.InvoiceDesc,
                    InvoiceDate = model.InvoiceDate,
                    ReceivedDate = model.ReceivedDate,
                    PaymentDate = model.PaymentDate,
                    TotalAmount = model.TotalAmount,
                    Route = model.Route,
                    ExportNow = model.ExportNow,
                    ExportedDate = model.ExportedDate,
                    ExportedBy = model.ExportedBy,
                    Approved = model.Approved,
                    ApprovedBy = model.ApprovedBy,
                    BatchID = model.BatchID,
                    CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                    CreatedDate = DateTime.Now.Date
                };
                db.tbl_Invoice.Add(saveInvoice);
                db.SaveChanges();
                msg = "Invoice Save Successfully";
            }
            else
            {
                var GetInvoiceData = db.tbl_Invoice.Where(p => p.InvoiceID == model.InvoiceID).FirstOrDefault();

                if (GetInvoiceData != null)
                {
                    GetInvoiceData.InvoiceNumber = model.InvoiceNumber;
                    GetInvoiceData.Vendor = model.Vendor;
                    GetInvoiceData.InvoiceDesc = model.InvoiceDesc;
                    GetInvoiceData.InvoiceDate = model.InvoiceDate;
                    GetInvoiceData.ReceivedDate = model.ReceivedDate;
                    GetInvoiceData.PaymentDate = model.PaymentDate;
                    GetInvoiceData.TotalAmount = model.TotalAmount;
                    GetInvoiceData.Route = model.Route;
                    GetInvoiceData.ExportNow = model.ExportNow;
                    GetInvoiceData.ExportedDate = model.ExportedDate;
                    GetInvoiceData.ExportedBy = model.ExportedBy;
                    GetInvoiceData.Approved = model.Approved;
                    GetInvoiceData.ApprovedBy = model.ApprovedBy;
                    GetInvoiceData.BatchID = model.BatchID;
                    GetInvoiceData.CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID;
                    GetInvoiceData.CreatedDate = DateTime.Now.Date;
                    db.SaveChanges();
                    msg = "Invoice Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public InvoiceModel GetInvoiceData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            InvoiceModel model = new InvoiceModel();

            var GetInvoiceData = db.tbl_Invoice.Where(p => p.InvoiceID == Id).FirstOrDefault();

            if (GetInvoiceData != null)
            {
                model.InvoiceNumber = GetInvoiceData.InvoiceNumber;
                model.Vendor = GetInvoiceData.Vendor;
                model.InvoiceDesc = GetInvoiceData.InvoiceDesc;
                model.InvoiceDate = GetInvoiceData.InvoiceDate;
                model.ReceivedDate = GetInvoiceData.ReceivedDate;
                model.PaymentDate = GetInvoiceData.PaymentDate;
                model.TotalAmount = GetInvoiceData.TotalAmount;
                model.Route = GetInvoiceData.Route;
                model.ExportNow = GetInvoiceData.ExportNow;
                model.ExportedDate = GetInvoiceData.ExportedDate;
                model.ExportedBy = GetInvoiceData.ExportedBy;
                model.Approved = GetInvoiceData.Approved;
                model.ApprovedBy = GetInvoiceData.ApprovedBy;
                model.BatchID = GetInvoiceData.BatchID;
            }
            model.InvoiceID = Id;
            return model;
        }

        public List<InvoiceModel> GetInvoiceListData(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<InvoiceModel> lstpr = new List<InvoiceModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetInvoiceList";
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
                    InvoiceModel pr = new InvoiceModel();

                    DateTime? invoiceDate = null;
                    try
                    {

                        invoiceDate = Convert.ToDateTime(dr["InvoiceDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? recievedDate = null;
                    try
                    {

                        recievedDate = Convert.ToDateTime(dr["ReceivedDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? paymentDate = null;
                    try
                    {

                        paymentDate = Convert.ToDateTime(dr["PaymentDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? approved = null;
                    try
                    {

                        approved = Convert.ToDateTime(dr["Approved"].ToString());
                    }
                    catch
                    {

                    }
                    pr.InvoiceID = Convert.ToInt64(dr["InvoiceID"].ToString());
                    pr.InvoiceNumber = dr["InvoiceNumber"].ToString();
                    pr.Vendor = dr["Vendor"].ToString();
                    pr.InvoiceDateString = invoiceDate == null ? "" : invoiceDate.Value.ToString("MM/dd/yyy");
                    pr.ReceivedDateString = recievedDate == null ? "" : recievedDate.Value.ToString("MM/dd/yyy");
                    pr.PaymentDateString = paymentDate == null ? "" : paymentDate.Value.ToString("MM/dd/yyy");
                    pr.TotalAmount = Convert.ToDecimal(dr["TotalAmount"].ToString());
                    pr.ApprovedString = approved == null ? "" : approved.Value.ToString("MM/dd/yyy");
                    pr.ApprovedBy = dr["ApprovedBy"].ToString();
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
        public class InvoiceSearchModel
        {
            public long InvoiceID { get; set; }
            public string InvoiceNumber { get; set; }
            public string Vendor { get; set; }
            public string InvoiceDate { get; set; }
            public string Approved { get; set; }
            public string TotalAmount { get; set; }
            public string ApprovedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ReceivedDate { get; set; }
            public string PaymentDate { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
        public int BuildPaganationInvoiceList(InvoiceSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetInvoicePaginationAndSearchData";
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
        public List<InvoiceSearchModel> FillInvoiceSearchGrid(InvoiceSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<InvoiceSearchModel> lstInvoice = new List<InvoiceSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetInvoicePaginationAndSearchData";
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
                    InvoiceSearchModel searchmodel = new InvoiceSearchModel();
                    searchmodel.InvoiceID = Convert.ToInt32(dr["InvoiceID"].ToString());
                    searchmodel.InvoiceNumber = dr["InvoiceNumber"].ToString();
                    searchmodel.Vendor = dr["Vendor"].ToString();
                    searchmodel.InvoiceDate = dr["InvoiceDate"].ToString();
                    searchmodel.Approved = dr["Approved"].ToString();
                    searchmodel.TotalAmount = Convert.ToDecimal(dr["TotalAmount"].ToString()).ToString("N2");
                    searchmodel.ApprovedBy = dr["ApprovedBy"].ToString();
                    searchmodel.ReceivedDate = dr["ReceivedDate"].ToString();
                    searchmodel.PaymentDate = dr["PaymentDate"].ToString();
                    searchmodel.CreatedDate = dr["CreatedDate"].ToString();
                    lstInvoice.Add(searchmodel);
                }
                db.Dispose();
                return lstInvoice.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}