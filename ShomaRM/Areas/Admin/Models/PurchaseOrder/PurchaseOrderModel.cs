using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class PurchaseOrderModel
    {
        public long POID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string OrderNumber { get; set; }
        public string Vendor { get; set; }
        public string PODesc { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public string PODateString { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Route { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedDateString { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> CanceledDate { get; set; }
        public string CanceledDateString { get; set; }
        public Nullable<int> CanceledBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedDateString { get; set; }

        public string SaveUpdatePurchaseOrder(PurchaseOrderModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.POID == 0)
            {
                var SavePurchaseOrder = new tbl_PurchaseOrder()
                {
                    PropertyID = model.PropertyID,
                    OrderNumber = model.OrderNumber,
                    Vendor = model.Vendor,
                    PODesc = model.PODesc,
                    PODate = model.PODate,
                    TotalAmount = model.TotalAmount,
                    Route = model.Route,
                    ApprovedDate = model.ApprovedDate,
                    ApprovedBy = model.ApprovedBy,
                    CanceledDate = model.CanceledDate,
                    CanceledBy = model.CanceledBy,
                    CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                    CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
                };
                db.tbl_PurchaseOrder.Add(SavePurchaseOrder);
                db.SaveChanges();
                msg = "Purchase Order Save Successfully";
            }
            else
            {
                var UpdatePurchaseOrder = db.tbl_PurchaseOrder.Where(co => co.POID == model.POID).FirstOrDefault();

                if (UpdatePurchaseOrder != null)
                {
                    UpdatePurchaseOrder.PropertyID = model.PropertyID;
                    UpdatePurchaseOrder.OrderNumber = model.OrderNumber;
                    UpdatePurchaseOrder.Vendor = model.Vendor;
                    UpdatePurchaseOrder.PODesc = model.PODesc;
                    UpdatePurchaseOrder.TotalAmount = model.TotalAmount;
                    UpdatePurchaseOrder.Route = model.Route;
                    UpdatePurchaseOrder.ApprovedDate = model.ApprovedDate;
                    UpdatePurchaseOrder.ApprovedBy = model.ApprovedBy;
                    UpdatePurchaseOrder.CanceledDate = model.CanceledDate;
                    UpdatePurchaseOrder.CanceledBy = model.CanceledBy;

                    db.SaveChanges();
                    msg = "Purchase Order Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
        public PurchaseOrderModel GetPurchaseOrderData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PurchaseOrderModel model = new PurchaseOrderModel();

            var GetPurchaseOrderData = db.tbl_PurchaseOrder.Where(co => co.POID == Id).FirstOrDefault();

            if (GetPurchaseOrderData != null)
            {
                model.PropertyID = GetPurchaseOrderData.PropertyID;
                model.OrderNumber = GetPurchaseOrderData.OrderNumber;
                model.Vendor = GetPurchaseOrderData.Vendor;
                model.PODesc = GetPurchaseOrderData.PODesc;
                model.PODate = GetPurchaseOrderData.PODate;
                model.TotalAmount = GetPurchaseOrderData.TotalAmount;
                model.Route = GetPurchaseOrderData.Route;
                model.ApprovedDate = GetPurchaseOrderData.ApprovedDate;
                model.ApprovedBy = GetPurchaseOrderData.ApprovedBy;
                model.CanceledDate = GetPurchaseOrderData.CanceledDate;
                model.CanceledBy = GetPurchaseOrderData.CanceledBy;
            }
            model.POID = Id;
            return model;
        }
        public class PurchaseOrderSearchModel
        {
            public long POID { get; set; }
            public string PropertyID { get; set; }
            public string OrderNumber { get; set; }
            public string Vendor { get; set; }
            public string PODesc { get; set; }
            public string PODate { get; set; }
            public string TotalAmount { get; set; }
            public string Route { get; set; }
            public string ApprovedDate { get; set; }
            public string ApprovedBy { get; set; }
            public string CanceledDate { get; set; }
            public string CanceledBy { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
        public int BuildPaganationPurchaseOrderList(PurchaseOrderSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPurchaseOrderPaginationAndSearchData";
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
        public List<PurchaseOrderSearchModel> FillPurchaseOrderSearchGrid(PurchaseOrderSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PurchaseOrderSearchModel> lstPurchaseOrder = new List<PurchaseOrderSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPurchaseOrderPaginationAndSearchData";
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
                    PurchaseOrderSearchModel searchmodel = new PurchaseOrderSearchModel();
                    searchmodel.POID = Convert.ToInt32(dr["POID"].ToString());
                    searchmodel.PropertyID = dr["PropertyID"].ToString();
                    searchmodel.OrderNumber = dr["OrderNumber"].ToString();
                    searchmodel.Vendor = dr["Vendor"].ToString();
                    searchmodel.PODesc = dr["PODesc"].ToString();
                    searchmodel.PODate = dr["PODate"].ToString();
                    searchmodel.TotalAmount = Convert.ToDecimal(dr["TotalAmount"].ToString()).ToString("N2");
                    searchmodel.Route = dr["Route"].ToString();
                    searchmodel.ApprovedDate = dr["ApprovedDate"].ToString();
                    searchmodel.ApprovedBy = dr["ApprovedBy"].ToString();
                    searchmodel.CanceledDate = dr["CanceledDate"].ToString();
                    searchmodel.CanceledBy = dr["CanceledBy"].ToString();
                    searchmodel.CreatedBy = dr["CreatedBy"].ToString();
                    searchmodel.CreatedDate = dr["CreatedDate"].ToString();
                    lstPurchaseOrder.Add(searchmodel);
                }
                db.Dispose();
                return lstPurchaseOrder.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}