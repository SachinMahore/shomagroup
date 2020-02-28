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
    public class AmenitiesSlotModel
    {

        public long ID { get; set; }
        public long AmenityID { get; set; }
        public string Duration { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<decimal> Fees { get; set; }
        public string AmenityName { get; set; }
        public long DurationID { get; set; }

        public string SaveUpdateSlot(AmenitiesSlotModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.ID == 0)
            {
                var saveSlot = new tbl_AmenityPriceRange()
                {
                   AmenityID = model.AmenityID,
                   Duration = model.Duration,
                   Deposit = model.Deposit,
                   Fees = model.Fees,
                   DurationID = model.DurationID
                };
                db.tbl_AmenityPriceRange.Add(saveSlot);
                db.SaveChanges();
                msg = "Amenity Slot Save Successfully";
            }
            else
            {
                var GetSlotData = db.tbl_AmenityPriceRange.Where(p => p.ID == model.ID).FirstOrDefault();
                if (GetSlotData != null)
                {
                    GetSlotData.AmenityID = model.AmenityID;
                    GetSlotData.Duration = model.Duration;
                    GetSlotData.Deposit = model.Deposit;
                    GetSlotData.Fees = model.Fees;
                    GetSlotData.DurationID = model.DurationID;
                    db.SaveChanges();
                    msg = "Amenity Slot Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public AmenitiesSlotModel GetEventData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            AmenitiesSlotModel model = new AmenitiesSlotModel();

            var GetSlotData = db.tbl_AmenityPriceRange.Where(p => p.ID == Id).FirstOrDefault();
            if (GetSlotData != null)
            {
                model.AmenityID = GetSlotData.AmenityID;
                model.Duration = GetSlotData.Duration;
                model.Deposit = GetSlotData.Deposit;
                model.Fees = GetSlotData.Fees;
                model.DurationID = Convert.ToInt64(GetSlotData.DurationID);
            }
            model.ID = Id;
            return model;
        }
        public List<AmenitiesSlotModel> GetSlotListData(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenitiesSlotModel> lstpr = new List<AmenitiesSlotModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAmenitySlotList";
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
                    AmenitiesSlotModel pr = new AmenitiesSlotModel();
                   

                    pr.AmenityID = Convert.ToInt64(dr["AmenityID"].ToString());
                    pr.Duration = dr["Duration"].ToString();
                    pr.Deposit = Convert.ToDecimal(dr["Deposit"].ToString());
                    pr.Fees = Convert.ToDecimal(dr["Deposit"].ToString());
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
        public List<AmenitiesSlotModel> SlotList()
        {
            List<AmenitiesSlotModel> list = new List<AmenitiesSlotModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var SlotList = db.tbl_AmenityPriceRange.ToList();
            foreach (var item in SlotList)
            {
                list.Add(new AmenitiesSlotModel()
                {
                    ID = item.ID,
                    AmenityID = item.AmenityID,
                    Duration = item.Duration,
                    Deposit = item.Deposit,
                    Fees = item.Fees
                });
            }
            return list;
        }
        public class AmenitiesSlotSearchModel
        {
            public long ID { get; set; }
            public long AmenityID { get; set; }
            public string Duration { get; set; }
            public string Deposit { get; set; }
            public string Fees { get; set; }
            public string AmenityName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
            public int Type { get; set; }
            public string SortBy { get; set; }
            public string OrderBy { get; set; }
        }
        public int BuildPaganationSlotList(AmenitiesSlotSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetSlotPaginationAndSearchData";
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
        public List<AmenitiesSlotSearchModel> FillSlotSearchGrid(AmenitiesSlotSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenitiesSlotSearchModel> lstSlot = new List<AmenitiesSlotSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetSlotPaginationAndSearchData";
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
                    AmenitiesSlotSearchModel searchmodel = new AmenitiesSlotSearchModel();
                    searchmodel.ID = Convert.ToInt64(dr["ID"].ToString());
                    searchmodel.AmenityID = Convert.ToInt64(dr["AmenityID"].ToString());
                    searchmodel.Duration = dr["Duration"].ToString();
                    searchmodel.Deposit = dr["Deposit"].ToString();
                    searchmodel.Fees = dr["Fees"].ToString();
                    searchmodel.AmenityName = dr["AmenityName"].ToString();


                    lstSlot.Add(searchmodel);
                }
                db.Dispose();
                return lstSlot.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}