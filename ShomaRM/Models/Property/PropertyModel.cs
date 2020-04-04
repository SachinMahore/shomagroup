using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Models
{
    public class PropertyModel
    {
        public long PID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string LocationGoogle { get; set; }
        public Nullable<int> Garages { get; set; }
        public string BuiltIn { get; set; }
        public string Parking { get; set; }
        public string Waterfront { get; set; }
        public string Amenities { get; set; }
        public string Picture { get; set; }
        public string YouTube { get; set; }
        public Nullable<int> NoOfUnits { get; set; }
        public Nullable<int> NoOfFloors { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
        public List<PropertyUnits> lstPropertyUnit { get; set; }
        public List<PropertyFloor> lstPropertyFloor { get; set; }
        public int Bedroom { get; set; }
        public DateTime MoveInDate { get; set; }
        public decimal MaxRent { get; set; }
        public int FromHome { get; set; }
        public List<PropertyModel> GetPropertyList(int City, string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyModel> lstProp = new List<PropertyModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "City";
                    paramF.Value = City;
                    cmd.Parameters.Add(paramF);

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
                    PropertyModel pr = new PropertyModel();
                   
                    pr.PID = Convert.ToInt32(dr["PID"].ToString());
                    pr.NoOfUnits = Convert.ToInt32(dr["NoOfUnits"].ToString());
                    pr.Title = dr["Title"].ToString();                
                    pr.Area = dr["Area"].ToString();
                    pr.NoOfFloors = Convert.ToInt32(dr["NoOfFloors"].ToString());
                    pr.Description = dr["Description"].ToString();
                    pr.Picture = dr["Picture"].ToString();
                    lstProp.Add(pr);
                }
                db.Dispose();
                return lstProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }

        }
        public PropertyModel GetPropertyDetails(int PID, DateTime AvailableDate, int Bedroom, decimal MaxRent)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PropertyModel model = new PropertyModel();
            List<PropertyFloor> listfloor = new List<PropertyFloor>();
            model.lstPropertyFloor = listfloor;
            var propDet = db.tbl_Properties.Where(p => p.PID == PID).FirstOrDefault();
            if(propDet !=null)
            {
                model.PID = propDet.PID;
                model.Title = propDet.Title;
                model.NoOfUnits = propDet.NoOfUnits;
                model.NoOfFloors = propDet.NoOfFloors;
                model.Description = propDet.Description;
                model.Area = propDet.Area;
                model.Location = propDet.Location;
                model.LocationGoogle = propDet.LocationGoogle;
                model.BuiltIn = propDet.BuiltIn;
                model.Waterfront = propDet.Waterfront;
                model.Parking = propDet.Parking;
                model.Picture = propDet.Picture;
                model.YouTube = propDet.YouTube;
                model.Status = propDet.Status;
                model.Amenities = propDet.Amenities;
            }
            
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyFloorCord";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = PID;
                    cmd.Parameters.Add(paramPID);


                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = AvailableDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = Bedroom;
                    cmd.Parameters.Add(paramB);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "MaxRent";
                    paramC.Value = MaxRent;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyFloor pr = new PropertyFloor();

                    pr.FloorID = Convert.ToInt32(dr["FloorID"].ToString());
                    //pr.FloorNo = dr["FloorNo"].ToString();
                    pr.Coordinates = dr["Coordinates"].ToString();

                    pr.IsAvail = Convert.ToInt32(dr["IsAvail"].ToString());

                    model.lstPropertyFloor.Add(pr);
                }
                db.Dispose();
                //return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
            return model;
        }
        public List<PropertyUnits> GetPropertyUnitList(long PID,DateTime AvailableDate, decimal Current_Rent, int Bedroom)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyUnits> lstUnitProp = new List<PropertyUnits>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetClientUnitList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = PID;
                    cmd.Parameters.Add(paramPID);

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = AvailableDate;
                    cmd.Parameters.Add(paramF);
                    if(Current_Rent==0)
                    {
                        Current_Rent = 10000;
                    }
                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Current_Rent";
                    paramC.Value = Current_Rent;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = Bedroom;
                    cmd.Parameters.Add(paramB);

                  
                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyUnits pr = new PropertyUnits();
                    DateTime? availableDate = null;
                    try
                    {
                        availableDate = Convert.ToDateTime(dr["AvailableDate"].ToString());
                    }
                    catch
                    {

                    }
                    
                    pr.PID = Convert.ToInt32(dr["PID"].ToString());
                    pr.UID = Convert.ToInt32(dr["UID"].ToString());
                    pr.UnitNo = dr["UnitNo"].ToString();
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.Hall = Convert.ToInt32(dr["Hall"].ToString());
                    pr.Deposit = Convert.ToDecimal(dr["Deposit"].ToString());
                    pr.Area = dr["Area"].ToString();
                    pr.FloorNoText = dr["FloorNo"].ToString();
                    pr.FloorPlan = dr["FloorPlan"].ToString();
                    pr.AvailableDateText = availableDate.Value.ToString("MM/dd/yyyy");
                    pr.Current_Rent = Convert.ToDecimal(dr["Current_Rent"].ToString());
                    lstUnitProp.Add(pr);
                }
                db.Dispose();
                return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }


        }
        public List<PropertyUnits> GetPropertyModelList(long PID, DateTime AvailableDate, decimal Current_Rent, int Bedroom, int SortOrder)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyUnits> lstUnitProp = new List<PropertyUnits>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetClientModelUnitList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = PID;
                    cmd.Parameters.Add(paramPID);

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = AvailableDate;
                    cmd.Parameters.Add(paramF);
                    if (Current_Rent == 0)
                    {
                        Current_Rent = 10000;
                    }
                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Current_Rent";
                    paramC.Value = Current_Rent;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = Bedroom;
                    cmd.Parameters.Add(paramB);

                    DbParameter paramSO = cmd.CreateParameter();
                    paramSO.ParameterName = "SortOrder";
                    paramSO.Value = SortOrder;
                    cmd.Parameters.Add(paramSO);


                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyUnits pr = new PropertyUnits();
                    //DateTime? availableDate = null;
                    //try
                    //{
                    //    availableDate = Convert.ToDateTime(dr["AvailableDate"].ToString());
                    //}
                    //catch
                    //{

                    //}

                    //pr.PID = Convert.ToInt32(dr["PID"].ToString());
                 
                    pr.Building = dr["ModelName"].ToString();
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.NoAvailable = Convert.ToInt32(dr["NoAvailable"].ToString());
                    pr.Area = dr["Area"].ToString();
               
                    pr.FloorPlan = dr["FloorPlan"].ToString();
                    //pr.AvailableDateText = availableDate.Value.ToString("MM/dd/yyyy");
                    pr.RentRange = dr["RentRange"].ToString();
                    lstUnitProp.Add(pr);
                }
                db.Dispose();
                return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }


        }
        public List<PropertyUnits> GetPropertyModelUnitList(string ModelName, DateTime AvailableDate, decimal Current_Rent, int Bedroom,int LeaseTermID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyUnits> lstUnitProp = new List<PropertyUnits>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyModelUnitList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "ModelName";
                    paramPID.Value = ModelName;
                    cmd.Parameters.Add(paramPID);

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = AvailableDate;
                    cmd.Parameters.Add(paramF);
                    if (Current_Rent == 0)
                    {
                        Current_Rent = 10000;
                    }
                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Current_Rent";
                    paramC.Value = Current_Rent;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = Bedroom;
                    cmd.Parameters.Add(paramB);

                    //DbParameter paramL = cmd.CreateParameter();
                    //paramL.ParameterName = "LeaseTermID";
                    //paramL.Value = LeaseTermID;
                    //cmd.Parameters.Add(paramL);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyUnits pr = new PropertyUnits();
                    DateTime? availableDate = null;
                    try
                    {
                        availableDate = Convert.ToDateTime(dr["AvailableDate"].ToString());
                    }
                    catch
                    {

                    }

                    pr.PID = Convert.ToInt32(dr["PID"].ToString());
                    pr.UID = Convert.ToInt32(dr["UID"].ToString());
                    pr.UnitNo = dr["UnitNo"].ToString();
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.Hall = Convert.ToInt32(dr["Hall"].ToString());
                    pr.Deposit = Convert.ToDecimal(dr["Deposit"].ToString());
                    pr.Area = dr["Area"].ToString();
                    pr.FloorNoText = dr["FloorNo"].ToString();
                    pr.FloorPlan = dr["FloorPlan"].ToString();
                    pr.AvailableDateText = availableDate.Value.ToString("MM/dd/yyyy");
                    var getRent = db.tbl_UnitLeasePrice.Where(p => p.LeaseID == LeaseTermID && p.UnitID == pr.UID).FirstOrDefault();
                    if(getRent!=null)
                    {
                        pr.Current_Rent = Convert.ToDecimal(getRent.Price);
                    }
                    else
                    {
                        pr.Current_Rent = Convert.ToDecimal("0.00");
                    }
                  
                    pr.Premium = dr["Premium"].ToString();
                    lstUnitProp.Add(pr);
                }
                db.Dispose();
                return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }


        }
       
        public PropertyUnits GetPropertyUnitDetails(long UID, int LeaseTermID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PropertyUnits model = new PropertyUnits();
            var unitDet = db.tbl_PropertyUnits.Where(p => p.UID == UID).FirstOrDefault();
            if(unitDet!=null)
            {
                DateTime? availableDate = null;
                try
                {
                    availableDate = Convert.ToDateTime(unitDet.AvailableDate.ToString());
                }
                catch
                {

                }
                DateTime? vacancylosssdate = null;
                try
                {
                    vacancylosssdate = Convert.ToDateTime(unitDet.VacancyLoss_Date.ToString());
                }
                catch
                {

                }
                DateTime? ocupancyDate = null;
                try
                {
                    ocupancyDate = Convert.ToDateTime(unitDet.OccupancyDate.ToString());
                }
                catch
                {

                }
                DateTime? actualmoveinDate = null;
                try
                {
                    actualmoveinDate = Convert.ToDateTime(unitDet.ActualMoveInDate.ToString());
                }
                catch
                {

                }
                model.PID = unitDet.PID;
                model.UID = unitDet.UID;
                model.UnitNo = unitDet.UnitNo;
                model.Rooms = unitDet.Rooms;
                model.Bedroom = unitDet.Bedroom;
                model.Bathroom = unitDet.Bathroom;

                model.Hall = unitDet.Hall;
                model.Deposit = Convert.ToDecimal(unitDet.Deposit);
                var getRent = db.tbl_UnitLeasePrice.Where(p => p.LeaseID == LeaseTermID && p.UnitID == UID).FirstOrDefault();
                if (getRent != null)
                {
                    model.Current_Rent = Convert.ToDecimal(getRent.Price);
                }
                else
                {
                    model.Current_Rent = Convert.ToDecimal("0.00");
                }
               // model.Current_Rent = unitDet.Current_Rent;
                model.Previous_Rent = unitDet.Previous_Rent;
                model.Market_Rent = unitDet.Market_Rent;
                model.Wing = unitDet.Wing;
                model.Building = unitDet.Building;
                model.Leased = unitDet.Leased;
                model.PetDetails = unitDet.PetDetails;
                model.FloorNo = unitDet.FloorNo;
                model.Area = unitDet.Area;
                model.FloorPlan = unitDet.FloorPlan;
                model.Carpet_Color = unitDet.Carpet_Color;
                model.Wall_Paint_Color = unitDet.Wall_Paint_Color;
                model.Furnished = unitDet.Furnished;
                model.Washer = unitDet.Washer;
                model.Refrigerator = unitDet.Refrigerator;
                model.Drapes = unitDet.Drapes;
                model.Dryer = unitDet.Dryer;
                model.Dishwasher = unitDet.Dishwasher;
                model.Disposal = unitDet.Disposal;
                model.Elec_Range = unitDet.Elec_Range;
                model.Gas_Range = unitDet.Gas_Range;
                model.Carpet = unitDet.Carpet;
                model.Air_Conditioning = unitDet.Air_Conditioning;
                model.Fireplace = unitDet.Fireplace;
                model.Den = unitDet.Den;
                //model.AvailableDateText = availableDate.Value.ToString("MM/dd/yyyy");
                //model.OccupancyDateText = ocupancyDate.Value.ToString("MM/dd/yyyy");
                //model.PendingMoveIn = unitDet.PendingMoveIn;
                //model.VacancyLoss_DateText = vacancylosssdate.Value.ToString("MM/dd/yyyy");
                //model.IntendedMoveIn_Date = unitDet.IntendedMoveIn_Date;
               // model.IntendMoveOutDate = unitDet.IntendMoveOutDate;
               // model.ActualMoveInDateText = actualmoveinDate.Value.ToString("MM/dd/yyyy");
                //model.ActualMoveOutDate = unitDet.ActualMoveOutDate;
                model.Coordinates = unitDet.Coordinates;
            }
         
            return model;
        }      
        public List<GalleryModel> GetPropertyGallary(long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<GalleryModel> lstGallary = new List<GalleryModel>();
            var propList = db.tbl_Gallery.Where(p => p.PID == PID).ToList();
            foreach (var pl in propList)
            {
                lstGallary.Add(new GalleryModel
                {
                    PID = pl.PID,
                    PhotoPath = pl.PhotoPath,
                });
            }
            return lstGallary;
        }

    }
    public partial class PropertyUnits
    {
        public long UID { get; set; }
        public Nullable<long> PID { get; set; }
        public string UnitNo { get; set; }
        public string Wing { get; set; }
        public string Building { get; set; }
        public Nullable<int> FloorNo { get; set; }
        public string FloorNoText { get; set; }
        public decimal Current_Rent { get; set; }
        public decimal Previous_Rent { get; set; }
        public decimal Market_Rent { get; set; }
        public decimal Deposit { get; set; }
        public string Leased { get; set; }
        public int Rooms { get; set; }
        public Nullable<int> Bedroom { get; set; }
        public Nullable<int> Bathroom { get; set; }
        public Nullable<int> Hall { get; set; }
        public int Den { get; set; }
        public int Furnished { get; set; }
        public int Fireplace { get; set; }
        public int Carpet { get; set; }
        public string Carpet_Color { get; set; }
        public string Wall_Paint_Color { get; set; }
        public int Drapes { get; set; }
        public int Air_Conditioning { get; set; }
        public int Range { get; set; }
        public int Gas_Range { get; set; }
        public int Elec_Range { get; set; }
        public int Washer_Hookup { get; set; }
        public int Dryer_Hookup { get; set; }
        public int Gas_Dryer_Hookup { get; set; }
        public int Elec_Dryer_Hookup { get; set; }
        public int Washer { get; set; }
        public int Dryer { get; set; }
        public int Refrigerator { get; set; }
        public int Dishwasher { get; set; }
        public int Disposal { get; set; }
        public string PetDetails { get; set; }
        public string Area { get; set; }
        public string FloorPlan { get; set; }
        public string Coordinates { get; set; }
        public Nullable<System.DateTime> AvailableDate { get; set; }
        public string AvailableDateText { get; set; }
        public int PendingMoveIn { get; set; }
        public Nullable<System.DateTime> IntendedMoveIn_Date { get; set; }
        public Nullable<System.DateTime> ActualMoveInDate { get; set; }
        public string ActualMoveInDateText { get; set; }
        public int PendingMoveOut { get; set; }
        public Nullable<System.DateTime> IntendMoveOutDate { get; set; }
        public Nullable<System.DateTime> ActualMoveOutDate { get; set; }
        public Nullable<System.DateTime> VacancyLoss_Date { get; set; }
        public string VacancyLoss_DateText { get; set; }
        public Nullable<System.DateTime> OccupancyDate { get; set; }
        public string OccupancyDateText { get; set; }
        public string RentRange { get; set; }
        public int IsAvail { get; set; }
        public int NoAvailable { get; set; }
        public string Premium { get; set; }
    }
    public partial class PropertyFloor
    {
        public long FloorID { get; set; }
        public Nullable<long> PID { get; set; }
        public string Coordinates { get; set; }
        public string FloorNo { get; set; }
        public string FloorPlan { get; set; }
        public int IsAvail { get; set; }
        public List<PropertyUnits> lstUnitFloor { get; set; }
        public List<PropertyFloor> GetFloorList(int PID,  DateTime AvailableDate, int Bedroom, decimal MaxRent)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyFloor> model = new List<PropertyFloor>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyFloorCord";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = PID;
                    cmd.Parameters.Add(paramPID);

                  
                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = AvailableDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = Bedroom;
                    cmd.Parameters.Add(paramB);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "MaxRent";
                    paramC.Value = MaxRent;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyFloor pr = new PropertyFloor();
                   
                    pr.FloorID = Convert.ToInt32(dr["FloorID"].ToString());
                    pr.FloorNo = dr["FloorID"].ToString();
                    pr.Coordinates = dr["Coordinates"].ToString();
                  
                    pr.IsAvail = Convert.ToInt32(dr["IsAvail"].ToString());

                    model.Add(pr);
                }
                db.Dispose();
                //return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
      
            return model.ToList();
        }
        public PropertyFloor GetPropertyFloorDetails(int FloorID, DateTime AvailableDate,  int Bedroom, decimal MaxRent, int LeaseTermID, string ModelName)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PropertyFloor model = new PropertyFloor();
            var unitDet = db.tbl_PropertyFloor.Where(p => p.FloorID == FloorID).FirstOrDefault();
            if (unitDet != null)
            {
                model.PID = unitDet.PID;             
                model.FloorPlan = unitDet.FloorPlan;
                model.FloorID = unitDet.FloorID;
                model.FloorNo = unitDet.FloorNo;
            }
            List<PropertyUnits> listfloor = new List<PropertyUnits>();
            model.lstUnitFloor = listfloor;
           

            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyUnitListCord";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = unitDet.PID;
                    cmd.Parameters.Add(paramPID);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "FloorNo";
                    paramC.Value = FloorID;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = AvailableDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = Bedroom;
                    cmd.Parameters.Add(paramB);

                    DbParameter paramM = cmd.CreateParameter();
                    paramM.ParameterName = "MaxRent";
                    paramM.Value = MaxRent;
                    cmd.Parameters.Add(paramM);

                    DbParameter paramMN = cmd.CreateParameter();
                    paramMN.ParameterName = "ModelName";
                    paramMN.Value = ModelName;
                    cmd.Parameters.Add(paramMN);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyUnits pr = new PropertyUnits();
                    DateTime? availableDate = null;
                    try
                    {
                        availableDate = Convert.ToDateTime(dr["AvailableDate"].ToString());
                    }
                    catch
                    {

                    }

                    //pr.PID = Convert.ToInt32(dr["PID"].ToString());
                    pr.UID = Convert.ToInt32(dr["UID"].ToString());
                    pr.UnitNo = dr["UnitNo"].ToString();
                    pr.Coordinates = dr["Coordinates"].ToString();
                   // pr.Area = dr["Area"].ToString();
                    pr.FloorNoText = dr["FloorNo"].ToString();
                    pr.IsAvail = Convert.ToInt32(dr["IsAvail"].ToString());
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.Area = dr["Area"].ToString();
                    pr.Premium = dr["Premium"].ToString();
                    pr.AvailableDateText = availableDate.Value.ToString("MM/dd/yyyy");
                    var getRent = db.tbl_UnitLeasePrice.Where(p => p.LeaseID == LeaseTermID && p.UnitID == pr.UID).FirstOrDefault();
                    if (getRent != null)
                    {
                        pr.Current_Rent = Convert.ToDecimal(getRent.Price);
                    }
                    else
                    {
                        pr.Current_Rent = Convert.ToDecimal("0.00");
                    }
                    listfloor.Add(pr);
                }
                db.Dispose();
                //return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
            return model;
        }
 
    }
}