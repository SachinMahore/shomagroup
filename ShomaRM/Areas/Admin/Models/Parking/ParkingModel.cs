using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class ParkingModel
    {
        public long ParkingID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string ParkingName { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public string Description { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }
        public string Available { get; set; }
        public string UnitNo { get; set; }
        public string VehicleTag { get; set; }
        public string OwnerName { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }

        public List<ParkingModel> GetParkingList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ParkingModel> model = new List<ParkingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_Parking";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //DbParameter paramC = cmd.CreateParameter();
                    //paramC.ParameterName = "Criteria";
                    //paramC.Value = MarketSourceName;
                    //cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    //ParkingModel usm = new ParkingModel();
                    //usm.ParkingID = int.Parse(dr["ParkingID"].ToString());
                    //usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    //usm.ParkingName = dr["ParkingName"].ToString();
                    //usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    //usm.Description = dr["Description"].ToString();
                    //usm.Type = int.Parse(dr["Type"].ToString());
                    //usm.Status = int.Parse(dr["Status"].ToString());
                    //usm.Available = dr["Available"].ToString();
                    //usm.UnitNo = dr["UnitNo"].ToString();
                    //usm.VehicleTag = dr["VehicleTag"].ToString();
                    //usm.OwnerName = dr["TenantName"].ToString();
                    ParkingModel usm = new ParkingModel();
                    usm.ParkingID = int.Parse(dr["ParkingID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.ParkingName = dr["ParkingName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    usm.Type = int.Parse(dr["Type"].ToString());
                    model.Add(usm);
              
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<ParkingModel> GetUnitParkingList(int UID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ParkingModel> model = new List<ParkingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_UnitParking";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "UID";
                    paramC.Value = UID;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ParkingModel usm = new ParkingModel();
                    usm.ParkingID = int.Parse(dr["ParkingID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.ParkingName = dr["ParkingName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    usm.Type = int.Parse(dr["Type"].ToString());
                    model.Add(usm);
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public ParkingModel GetParkingInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ParkingModel model = new ParkingModel();
            model.ParkingID = 0;
            model.ParkingName = "";

            var ParkingInfo = db.tbl_Parking.Where(p => p.ParkingID == ParkingID).FirstOrDefault();
            if (ParkingInfo != null)
            {
                model.ParkingID = ParkingInfo.ParkingID;
                model.PropertyID = ParkingInfo.PropertyID;
                model.ParkingName = ParkingInfo.ParkingName;
                model.Charges = ParkingInfo.Charges;
                model.Description = ParkingInfo.Description;
            }

            return model;
        }
        public string SaveUpdateParking(ParkingModel model)
        {
            string msgtxt= "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.ParkingID != 0)
            {
                var ParkingInfo = db.tbl_Parking.Where(p => p.ParkingID == model.ParkingID).FirstOrDefault();
                if (ParkingInfo != null)
                {
                    ParkingInfo.PropertyID = model.PropertyID;
                    ParkingInfo.ParkingName = model.ParkingName;
                    ParkingInfo.Charges = model.Charges;
                    ParkingInfo.Description = model.Description;
                    db.SaveChanges();
                     msgtxt += "Parking information updated successfully.</br>";
                    var VehicleInfo = db.tbl_Vehicle.Where(p => p.ParkingID == model.ParkingID).FirstOrDefault();
                    if (VehicleInfo != null)
                    {
                        VehicleInfo.OwnerName = model.OwnerName;
                        VehicleInfo.Tag = model.VehicleTag;
                        VehicleInfo.Make = model.VehicleMake;
                        VehicleInfo.Model = model.VehicleModel;
                        db.SaveChanges();
                        msgtxt += "Vehicle information updated successfully.</br>";
                    }
                    else
                    {
                        msgtxt = "Vehicle information not updated due to invalid Parking information.";

                    }
                }
                else
                {
                    msgtxt = "Parking not updated successfully.";

                }

            }
            
            db.Dispose();
            return msgtxt;

        }
        public ParkingModel GetParkingData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ParkingModel model = new ParkingModel();

            var GetParkingData = db.tbl_Parking.Where(p => p.ParkingID == Id).FirstOrDefault();

            if (GetParkingData != null)
            {
                model.ParkingID = GetParkingData.ParkingID;
                model.PropertyID = GetParkingData.PropertyID;
                model.ParkingName = GetParkingData.ParkingName;
                model.Charges = GetParkingData.Charges;
                model.Description = GetParkingData.Description;

                 var GetUnitData = db.tbl_PropertyUnits.Where(p => p.UID == GetParkingData.PropertyID).FirstOrDefault();
                if (GetUnitData != null)
                {
                    model.UnitNo = GetUnitData.UnitNo;
                }
                var GetVehicleData = db.tbl_Vehicle.Where(p => p.ParkingID == GetParkingData.ParkingID).FirstOrDefault();
                if (GetVehicleData != null)
                {
                    model.OwnerName = GetVehicleData.OwnerName;
                    model.VehicleTag = GetVehicleData.Tag;
                    model.VehicleMake = GetVehicleData.Make;
                    model.VehicleModel = GetVehicleData.Model;

                }
               
            }
            model.ParkingID = Id;
            return model;
        }
        public List<ParkingModel> GetParkingSearchList(string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ParkingModel> model = new List<ParkingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_Parking_SearchList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "SearchText";
                    paramC.Value = SearchText;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ParkingModel usm = new ParkingModel();
                    usm.ParkingID = int.Parse(dr["ParkingID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.ParkingName = dr["ParkingName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    model.Add(usm);
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public int BuildPaganationParkingList(ParkingListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetParkingPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramCT = cmd.CreateParameter();
                    paramCT.ParameterName = "CriteriaByText";
                    paramCT.Value = model.CriteriaByText;
                    cmd.Parameters.Add(paramCT);

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
        public List<ParkingListModel> FillParkingSearchGrid(ParkingListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ParkingListModel> lstData = new List<ParkingListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetParkingPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramCT = cmd.CreateParameter();
                    paramCT.ParameterName = "CriteriaByText";
                    paramCT.Value = model.CriteriaByText;
                    cmd.Parameters.Add(paramCT);

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
                    ParkingListModel usm = new ParkingListModel();
                    usm.ParkingID = int.Parse(dr["ParkingID"].ToString());
                    usm.PropertyID = int.Parse(dr["PropertyID"].ToString());
                    usm.ParkingName = dr["ParkingName"].ToString();
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.Description = dr["Description"].ToString();
                    usm.Type = int.Parse(dr["Type"].ToString());
                    usm.Status = int.Parse(dr["Status"].ToString());
                    //usm.Available = dr["Available"].ToString();
                    usm.UnitNo = dr["UnitID"].ToString();
                    usm.VehicleTag = dr["VehicleTag"].ToString();
                    usm.OwnerName = dr["TenantName"].ToString();
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
    
    public class TenantParkingModel
    {
        public long TPID { get; set; }
        public Nullable<long> ParkingID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<decimal> Charges { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public List<TenantParkingModel> lstTParking { get; set; }
        public long UID { get; set; }

        public string  SaveUpdateTenantParking(TenantParkingModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            decimal totalParkingAmt = 0;
            int numOfParking = 0;
            var TenantParkingData = db.tbl_TenantParking.Where(p => p.TenantID == model.TenantID).ToList();
            db.tbl_TenantParking.RemoveRange(TenantParkingData);
            db.SaveChanges();
            if (model.lstTParking != null)
            {
                foreach (var cd in model.lstTParking)
                {
                    var parkingdata = db.tbl_Parking.Where(p => p.ParkingID == cd.ParkingID).FirstOrDefault();
                    var cdData = new tbl_TenantParking
                    {
                        ParkingID = cd.ParkingID,
                        TenantID = model.TenantID,
                        Charges = parkingdata.Charges,
                        CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))
                    };
                    db.tbl_TenantParking.Add(cdData);
                    db.SaveChanges();
                    numOfParking = numOfParking + parkingdata.Type ?? 0;
                    totalParkingAmt = totalParkingAmt+Convert.ToDecimal(parkingdata.Charges);

                    parkingdata.PropertyID = model.UID;
                    db.SaveChanges();
                }

            }
            else
            {
                var remparkingdata = db.tbl_Parking.Where(p => p.PropertyID == model.UID && p.Type==2).FirstOrDefault();
                if(remparkingdata!=null)
                {
                    remparkingdata.PropertyID = 0;
                    db.SaveChanges();
                }
            }


            return numOfParking.ToString() + "|" + totalParkingAmt.ToString();

        }


        public List<TenantParkingModel> GetTenantParkingList(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantParkingModel> model = new List<TenantParkingModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_Get_TenantParking";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "TenantId";
                    paramC.Value = TenantId;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantParkingModel usm = new TenantParkingModel();
                    usm.TPID = Convert.ToInt64(dr["TPID"].ToString());
                    usm.ParkingID = Convert.ToInt64(dr["ParkingID"].ToString());
                    usm.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    usm.Charges = Convert.ToDecimal(dr["Charges"].ToString());
                    usm.CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    model.Add(usm);
                }
                db.Dispose();
                return model.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public string DeleteParking(long ParkingID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (ParkingID != 0)
            {

                var parkingData = db.tbl_Parking.Where(p => p.ParkingID == ParkingID).FirstOrDefault();
                if (parkingData != null)
                {
                    db.tbl_Parking.Remove(parkingData);
                    db.SaveChanges();
                    msg = "Parking Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class ParkingListModel
    {
        public long ParkingID { get; set; }
        public long PropertyID { get; set; }
        public string ParkingName { get; set; }
        public decimal Charges { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string Criteria { get; set; }
        public string CriteriaByText { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
        public string Available { get; set; }
        public string UnitNo { get; set; }
        public string VehicleTag { get; set; }
        public string OwnerName { get; set; }
    }
}