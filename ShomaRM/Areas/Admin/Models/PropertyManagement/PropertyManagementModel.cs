using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Models
{
    public class PropertyManagementModel
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
        public string OriginalPicture { get; set; }
        public string YouTube { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> City { get; set; }
        public Nullable<int> NoOfUnits { get; set; }
        public Nullable<int> NoOfFloors { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
        public List<PropertyUnits> lstPropertyUnit { get; set; }
        public List<PropertyFloor> lstPropertyFloor { get; set; }
        public Nullable<decimal> ApplicationFees { get; set; }
        public Nullable<decimal> GuarantorFees { get; set; }
        public Nullable<decimal> PestControlFees { get; set; }
        public Nullable<decimal> TrashFees { get; set; }
        public Nullable<decimal> ConversionBillFees { get; set; }
        public Nullable<decimal> AdminFees { get; set; }
        public Nullable<decimal> DNAPetFees { get; set; }
        public Nullable<decimal> ProcessingFees { get; set; }

        public List<UnitLeasePriceHeader> lstUnitLeasePriceHeader { get; set; }
        public decimal ColumnWidth { get; set; }

        public string SaveUpdateProperty(PropertyManagementModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            
            if (model.PID == 0)
            {
                var saveProp = new tbl_Properties()
                {
                    Title = model.Title,
                    Location = model.Location,
                    LocationGoogle = model.LocationGoogle,
                    BuiltIn = model.BuiltIn,
                    Waterfront = model.Waterfront,
                    Description = model.Description,
                    NoOfFloors = model.NoOfFloors,
                    NoOfUnits = model.NoOfUnits,
                    Parking = model.Parking,
                    YouTube = model.YouTube,
                    State = model.State,
                    City = model.City,
                    AgentID = 1,
                    Picture = model.Picture,
                    Status = model.Status,
                    Amenities = model.Amenities,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = 1,
                    LastModifiedeDate = DateTime.Now,
                    ApplicationFees = model.ApplicationFees,
                    GuarantorFees = model.GuarantorFees,
                    PestControlFees = model.PestControlFees,
                    TrashFees = model.TrashFees,
                    ConversionBillFees = model.ConversionBillFees,
                    AdminFees = model.AdminFees,
                    PetDNAAmt = model.DNAPetFees,
                    ProcessingFees = model.ProcessingFees
                };
                db.tbl_Properties.Add(saveProp);
                db.SaveChanges();
                msg = "Property Details Saved Successfully";
            }
            else
            {
                var propUpdate = db.tbl_Properties.Where(p => p.PID == model.PID).FirstOrDefault();
                if (propUpdate != null)
                {
                    propUpdate.Title = model.Title;
                    propUpdate.Location = model.Location;
                    propUpdate.LocationGoogle = model.LocationGoogle;
                    propUpdate.BuiltIn = model.BuiltIn;
                    propUpdate.Waterfront = model.Waterfront;
                    propUpdate.Description = model.Description;
                    propUpdate.NoOfFloors = model.NoOfFloors;
                    propUpdate.NoOfUnits = model.NoOfUnits;
                    propUpdate.Parking = model.Parking;
                    propUpdate.State = model.State;
                    propUpdate.City = model.City;
                    propUpdate.YouTube = model.YouTube;
                    propUpdate.Area = model.Area;
                    propUpdate.AgentID = 1;
                    if (!string.IsNullOrWhiteSpace( model.Picture))
                    {
                        propUpdate.Picture = model.Picture;
                    }
                    propUpdate.Status = model.Status;
                    if (model.Amenities != "0")
                    {
                        propUpdate.Amenities = model.Amenities;
                    }

                    propUpdate.CreatedBy = 1;
                    propUpdate.CreatedDate = DateTime.Now;
                    propUpdate.LastModifiedBy = 1;
                    propUpdate.LastModifiedeDate = DateTime.Now;
                    propUpdate.ApplicationFees = model.ApplicationFees;
                    propUpdate.GuarantorFees = model.GuarantorFees;
                    propUpdate.PestControlFees = model.PestControlFees;
                    propUpdate.TrashFees = model.TrashFees;
                    propUpdate.ConversionBillFees = model.ConversionBillFees;
                    propUpdate.AdminFees = model.AdminFees;
                    propUpdate.PetDNAAmt = model.DNAPetFees;
                    propUpdate.ProcessingFees = model.ProcessingFees;
                }
                db.SaveChanges();
                msg = "Property Details Updated Successfully";
            }

            return msg;
        }
        public List<PropertyManagementModel> GetPropertyList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyManagementModel> lstProp = new List<PropertyManagementModel>();
            var propList = db.tbl_Properties.ToList();
            foreach (var pl in propList)
            {
                lstProp.Add(new PropertyManagementModel
                {
                    PID = pl.PID,
                    Title = pl.Title,
                    Description = pl.Description,
                    Area = pl.Area,
                    NoOfFloors = pl.NoOfFloors,
                    NoOfUnits = pl.NoOfUnits,
                    Picture = pl.Picture,

                });

            }
            return lstProp;
        }
        public List<PropertyManagementModel> SearchPropertyList(int City, string SearchText)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyManagementModel> lstProp = new List<PropertyManagementModel>();
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
                    PropertyManagementModel pr = new PropertyManagementModel();

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
        public PropertyManagementModel GetPropertyDet(int id)
        {
            PropertyManagementModel model = new PropertyManagementModel();
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyFloor> listfloor = new List<PropertyFloor>();
            model.lstPropertyFloor = listfloor;
            var propDet = db.tbl_Properties.Where(p => p.PID == id).FirstOrDefault();
            if (propDet != null)
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
                model.City = propDet.City;
                model.State = propDet.State;
                model.ApplicationFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.ApplicationFees));
                model.GuarantorFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.GuarantorFees));
                model.PestControlFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.PestControlFees));
                model.TrashFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.TrashFees));
                model.ConversionBillFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.ConversionBillFees));
                model.AdminFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.AdminFees));
                model.DNAPetFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.PetDNAAmt));
                model.ProcessingFees = Convert.ToDecimal(String.Format("{0:0.00}", propDet.ProcessingFees));
            }

            var leaseTerms = db.tbl_LeaseTerms.ToList().OrderBy(p => p.LeaseTerms ?? 0);

            decimal rowCount = leaseTerms.Count();
            if(rowCount==0)
            {
                rowCount = 1;
            }
            decimal width = 80 / rowCount;

            List<UnitLeasePriceHeader> ulph = new List<UnitLeasePriceHeader>();
            ulph.Add(new UnitLeasePriceHeader() { HeaderName = "Unit No", SortName = "UnitNo", Width = (rowCount == 1 ? "50" : "5"), DefaultSort = "1", HasSorting="1" });
            ulph.Add(new UnitLeasePriceHeader() { HeaderName = "Model", SortName = "Building", Width = (rowCount == 1 ? "50" : "5"), DefaultSort = "0", HasSorting = "1" });
            foreach(var lt in leaseTerms)
            {
                ulph.Add(new UnitLeasePriceHeader() { HeaderName = (lt.LeaseTerms ?? 0).ToString() + " Months", SortName = (lt.LeaseTerms ?? 0).ToString() + "Months", Width = width.ToString("0.00"), DefaultSort = "0" , HasSorting="0"});
            }

            model.lstUnitLeasePriceHeader = ulph;

            var floorlist = db.tbl_PropertyFloor.Where(p => p.PID == id).ToList();
            if (floorlist != null)
            {
                foreach (var fl in floorlist)
                {
                    model.lstPropertyFloor.Add(new PropertyFloor()
                    {
                        FloorNo = fl.FloorNo,
                        Coordinates = fl.Coordinates,
                        FloorID = fl.FloorID,
                        FloorPlan=fl.FloorPlan
                    });


                }
            }
            return model;
        }

        public List<PropertyUnits> GetPropertyUnitList(long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyUnits> lstUnitProp = new List<PropertyUnits>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyUnitDDL";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = PID;
                    cmd.Parameters.Add(paramPID);


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
                    pr.FloorNoText = dr["FloorNoText"].ToString();
                    pr.Current_Rent = Convert.ToDecimal(dr["Current_Rent"].ToString());
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.Building =dr["Building"].ToString();
                    pr.Area = dr["Area"].ToString();
                    pr.AvailableDateText = dr["AvailableDate"].ToString();

                    //FloorNo, Current_Rent, Bathroom, Bedroom, Hall, Area, AvailableDate
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
        public int BuildPaganationPUList(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyUnitPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "PropertyID";
                    param0.Value = PID;
                    cmd.Parameters.Add(param0);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = PN;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = NOR;
                    cmd.Parameters.Add(param4);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                if (dtTable.Rows.Count > 0)
                {
                    NOP = int.Parse(dtTable.Rows[0]["NOP"].ToString());
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
        public List<PropertyUnits> FillPUSearchGrid(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyUnits> lstPropertyUnit = new List<PropertyUnits>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyUnitPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "PropertyID";
                    param0.Value = PID;
                    cmd.Parameters.Add(param0);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = PN;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = NOR;
                    cmd.Parameters.Add(param4);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyUnits searchmodel = new PropertyUnits();
                    DateTime? availableDate = null;
                    try
                    {
                        availableDate = Convert.ToDateTime(dr["AvailableDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveinDate = null;
                    try
                    {
                        moveinDate = Convert.ToDateTime(dr["MoveInDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? moveoutDate = null;
                    try
                    {
                        moveoutDate = Convert.ToDateTime(dr["MoveOutDate"].ToString());
                    }
                    catch
                    {

                    }
                    searchmodel.PID = Convert.ToInt32(dr["PID"].ToString());
                    searchmodel.UID = Convert.ToInt32(dr["UID"].ToString());
                    searchmodel.UnitNo = dr["UnitNo"].ToString();
                    searchmodel.FloorNoText = dr["FloorNoText"].ToString();
                    searchmodel.Current_Rent = Convert.ToDecimal(dr["Current_Rent"].ToString());
                    searchmodel.ULRID = Convert.ToInt64(dr["ULRID"].ToString());
                    searchmodel.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    searchmodel.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    searchmodel.Building = dr["Building"].ToString();
                    searchmodel.Area = dr["Area"].ToString();
                    searchmodel.BalconyArea = dr["BalconyArea"].ToString();
                    searchmodel.InteriorArea = dr["InteriorArea"].ToString();
                    searchmodel.AvailableDateText = dr["AvailableDate"].ToString();
                    searchmodel.MoveInDateText = dr["MoveInDate"].ToString();
                    searchmodel.MoveOutDateText = dr["MoveOutDate"].ToString();
                    searchmodel.PremiumType = dr["PremiumType"].ToString();
                    searchmodel.Notes = dr["Notes"].ToString();
                    lstPropertyUnit.Add(searchmodel);
                }
                db.Dispose();
                return lstPropertyUnit.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
       
        public int BuildPaganationPropList(PropertySearch model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "SearchText";
                    param0.Value = model.Title;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "City";
                    param1.Value = model.City == "0" ? null : model.City;
                    cmd.Parameters.Add(param1);

                    DbParameter param2 = cmd.CreateParameter();
                    param2.ParameterName = "State";
                    param2.Value = model.State == "0" ? null : model.State;
                    cmd.Parameters.Add(param2);

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
        public List<PropertySearch> FillPropertySearchGrid(PropertySearch model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertySearch> lstTenant = new List<PropertySearch>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "SearchText";
                    param0.Value = model.Title;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "City";
                    param1.Value = model.City == "0" ? null : model.City;
                    cmd.Parameters.Add(param1);

                    DbParameter param2 = cmd.CreateParameter();
                    param2.ParameterName = "State";
                    param2.Value = model.State == "0" ? null : model.State;
                    cmd.Parameters.Add(param2);

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
                    PropertySearch usm = new PropertySearch();
                    usm.PID = long.Parse(dr["PID"].ToString());
                    usm.Title = dr["Title"].ToString();
                    usm.NoOfUnits = int.Parse(dr["NoOfUnits"].ToString());
                    usm.NoOfFloors = int.Parse(dr["NoOfFloors"].ToString());
                    usm.State = dr["State"].ToString();
                    usm.City = dr["City"].ToString();
                    usm.Location = dr["Location"].ToString();
                    usm.NumberOfRows = int.Parse(dr["NumberOfPages"].ToString());
                    lstTenant.Add(usm);
                }
                db.Dispose();
                return lstTenant.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public PropertyManagementModel PropertyFileUpload(HttpPostedFileBase fileBaseUpload1, PropertyManagementModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PropertyManagementModel propertyManagementModel = new PropertyManagementModel();
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUpload1 != null && fileBaseUpload1.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/demo/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUpload1.FileName;
                Extension = Path.GetExtension(fileBaseUpload1.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload1.FileName);
                fileBaseUpload1.SaveAs(filePath + "//" + sysFileName);
                fileBaseUpload1.SaveAs(filePath + "//" + fileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUpload1.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/demo/") + "/" + sysFileName;

                }
                //propertyManagementModel.Picture = sysFileName;
                //propertyManagementModel.OriginalPicture = fileName;
                propertyManagementModel.Picture = fileName;
                propertyManagementModel.OriginalPicture = fileName;
            }
            return propertyManagementModel;
        }
        public List<UnitLeaseWisePriceList> GetUnitLeasePrice(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<UnitLeaseWisePriceList> lstUnitLeasePrice = new List<UnitLeaseWisePriceList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUnitLeasePrice";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "PropertyId";
                    param0.Value = PID;
                    cmd.Parameters.Add(param0);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = PN;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = NOR;
                    cmd.Parameters.Add(param4);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    UnitLeaseWisePriceList searchmodel = new UnitLeaseWisePriceList();
                    searchmodel.UID = Convert.ToInt64(dr["UID"].ToString());
                    searchmodel.UnitNo = dr["UnitNo"].ToString();
                    searchmodel.Building = dr["Building"].ToString();
                    searchmodel.ULPID = Convert.ToInt64(dr["ULPID"].ToString());
                    searchmodel.LeaseID = Convert.ToInt32(dr["LeaseID"].ToString());
                    searchmodel.Price = Convert.ToDecimal(dr["Price"].ToString());
                    lstUnitLeasePrice.Add(searchmodel);
                }
                db.Dispose();
                return lstUnitLeasePrice.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public int BuildPaganationUnitLeasePrice(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUnitLeasePrice";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "PropertyId";
                    param0.Value = PID;
                    cmd.Parameters.Add(param0);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = PN;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = NOR;
                    cmd.Parameters.Add(param4);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                if (dtTable.Rows.Count > 0)
                {
                    NOP = int.Parse(dtTable.Rows[0]["NOP"].ToString());
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
        public class PropertySearch
        {
            public long PID { get; set; }
            public string Title { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public long NoOfFloors { get; set; }
            public long NoOfUnits { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
            public string Location { get; set; }
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
        public long ULRID { get; set; }
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
        public int Premium { get; set; }
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
        public string MoveInDateText { get; set; }
        public string MoveOutDateText { get; set; }
        public string ActualMoveInDateText { get; set; }
        public int PendingMoveIn { get; set; }
        public Nullable<System.DateTime> IntendedMoveIn_Date { get; set; }
        public Nullable<System.DateTime> ActualMoveInDate { get; set; }
        public int PendingMoveOut { get; set; }
        public Nullable<System.DateTime> IntendMoveOutDate { get; set; }
        public Nullable<System.DateTime> ActualMoveOutDate { get; set; }
        public Nullable<System.DateTime> VacancyLoss_Date { get; set; }
        public Nullable<System.DateTime> OccupancyDate { get; set; }
        public long PreviousID { get; set; }
        public long NextID { get; set; }
        public string BalconyArea { get; set; }
        public string InteriorArea { get; set; }
        public string Notes { get; set; }
        public string PremiumType { get; set; }
        public List<UnitLeasePrice> UnitWiseRent { get; set; }
        public List<PropertyFloor> Floors { get; set; }
        public List<ModelsModel> ModelsNumber { get; set; }
        public string UnitWiseRentData { get; set; }
        public List<PropertyUnits> lstPropertyUnit { get; set; }
        public List<PremiumTypeModel> PremiumTypeList { get; set; }
        public PropertyUnits GetPropertyUnitDetails(long UID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PropertyUnits model = new PropertyUnits();
            var unitDet = db.tbl_PropertyUnits.Where(p => p.UID == UID).FirstOrDefault();
            //var NextData = db.tbl_PropertyUnits.Where(p => p.UID > UID).OrderBy(p => p.UID).FirstOrDefault();
            //var PreviousData = db.tbl_PropertyUnits.Where(p => p.UID < UID).OrderByDescending(p => p.UID).FirstOrDefault();

            long nextUID = 0;
            long previousUID = 0;

            GetNextPreviousUnitIDs(UID, ref nextUID, ref previousUID);

            List<UnitLeasePrice> unitWiseRent = GetUnitLeasePrice(UID);
            model.UnitWiseRent = unitWiseRent.ToList();

            if (unitDet != null)
            {
                DateTime? availableDate = null;
                try
                {
                    availableDate = Convert.ToDateTime(unitDet.AvailableDate.ToString());
                }
                catch
                {

                }
                DateTime? actmovein = null;
                try
                {
                    actmovein = Convert.ToDateTime(unitDet.ActualMoveInDate.ToString());
                }
                catch
                {

                }
                model.PID = unitDet.PID;
                long? pid = unitDet.PID;
                List<PropertyFloor> propFloorPlan = GetFloorList(unitDet.PID ?? 0);
                model.Floors = propFloorPlan;
                List<ModelsModel> modelsNum = GetModelsListDetail();
                model.ModelsNumber = modelsNum;
                List<PremiumTypeModel> premiumlist = new PremiumTypeModel(). GetPremiumTypeList("", "PremiumType", "ASC");
                model.PremiumTypeList = premiumlist;
                model.UID = unitDet.UID;
                model.UnitNo = unitDet.UnitNo;
                model.Rooms = unitDet.Rooms;
                model.Bedroom = unitDet.Bedroom;
                model.Bathroom = unitDet.Bathroom;
                model.Hall = unitDet.Hall;
                model.Deposit = Convert.ToDecimal(unitDet.Deposit);
                model.Current_Rent = unitDet.Current_Rent;
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
                model.AvailableDate = unitDet.AvailableDate;
                model.AvailableDateText = availableDate == null ? "" : availableDate.Value.ToString("MM/dd/yyyy");
                model.ActualMoveInDateText = actmovein==null?"": actmovein.Value.ToString("MM/dd/yyyy");
                model.OccupancyDate = unitDet.OccupancyDate;
                model.PendingMoveIn = unitDet.PendingMoveIn;
                model.VacancyLoss_Date = unitDet.VacancyLoss_Date;
                model.IntendedMoveIn_Date = unitDet.IntendedMoveIn_Date;
                model.IntendMoveOutDate = unitDet.IntendMoveOutDate;
               // model.ActualMoveInDate = unitDet.ActualMoveInDate;
                model.ActualMoveOutDate = unitDet.ActualMoveOutDate;
                model.Coordinates = unitDet.Coordinates;
                model.Premium =Convert.ToInt32(unitDet.Premium);
                //model.PreviousID = PreviousData != null ? PreviousData.UID : 0;
                //model.NextID = NextData != null ? NextData.UID : 0;
                model.PreviousID = previousUID;
                model.NextID = nextUID;
                model.InteriorArea = unitDet.InteriorArea == null?"0":unitDet.InteriorArea;
                model.BalconyArea = unitDet.BalconyArea == null ? "0" : unitDet.BalconyArea;
                model.Notes = unitDet.Notes;
                
            }

            return model;
        }
        public void GetNextPreviousUnitIDs(long currentUID, ref long nextUID, ref long previousUID)
        {
            nextUID = 0;
            previousUID = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<ModelsModel> lstpr = new List<ModelsModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetNextPreviousUnitID";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramCUID = cmd.CreateParameter();
                    paramCUID.ParameterName = "CurrentUnitID";
                    paramCUID.Value = currentUID;
                    cmd.Parameters.Add(paramCUID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                if(dtTable.Rows.Count>0)
                {
                    nextUID = Convert.ToInt64(dtTable.Rows[0]["NextUnitID"].ToString());
                    previousUID = Convert.ToInt64(dtTable.Rows[0]["PreviousUnitID"].ToString());
                }


                db.Dispose();
            }
            catch (Exception ex)
            {
                
            }
        }
        public long SaveUpdatePropertyUnit(HttpPostedFileBase fb, PropertyUnits model)
        {
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/");
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/") + "/" + sysFileName;

                }
            }
            if (model.UID == 0)
            {
                var saveProp = new tbl_PropertyUnits()
                {
                    PID = model.PID,
                    UID = model.UID,
                    UnitNo = model.UnitNo,
                    Rooms = model.Rooms,
                    Bedroom = model.Bedroom,
                    Bathroom = model.Bathroom,
                    Hall = model.Hall,
                    Deposit = Convert.ToDecimal(model.Deposit),
                    Current_Rent = model.Current_Rent,
                    Previous_Rent = model.Previous_Rent,
                    Market_Rent = model.Market_Rent,
                    Wing = model.Wing,
                    Building = model.Building,
                    Leased = model.Leased,
                    PetDetails = model.PetDetails,
                    FloorNo = model.FloorNo,
                    Area = model.Area,
                    FloorPlan = model.Building+".jpg",
                    Carpet_Color = model.Carpet_Color,
                    Wall_Paint_Color = model.Wall_Paint_Color,
                    Furnished = model.Furnished,
                    Washer = model.Washer,
                    Refrigerator = model.Refrigerator,
                    Drapes = model.Drapes,
                    Dryer = model.Dryer,
                    Dishwasher = model.Dishwasher,
                    Disposal = model.Disposal,
                    Elec_Range = model.Elec_Range,
                    Gas_Range = model.Gas_Range,
                    Carpet = model.Carpet,
                    Air_Conditioning = model.Air_Conditioning,
                    Fireplace = model.Fireplace,
                    Den = model.Den,
                    AvailableDate = model.AvailableDate,
                    OccupancyDate = model.OccupancyDate,
                    PendingMoveIn = model.PendingMoveIn,
                    VacancyLoss_Date = model.VacancyLoss_Date,
                    IntendedMoveIn_Date = model.IntendedMoveIn_Date,
                    IntendMoveOutDate = model.IntendMoveOutDate,
                    ActualMoveInDate = model.ActualMoveInDate,
                    ActualMoveOutDate = model.ActualMoveOutDate,
                    Coordinates = model.Coordinates,
                    Premium=model.Premium,
                    BalconyArea=model.BalconyArea,
                    InteriorArea=model.InteriorArea,
                    Notes=model.Notes,
                };
                db.tbl_PropertyUnits.Add(saveProp);
                db.SaveChanges();
                model.UID = saveProp.UID;

                msg = "Property Unit Details Saved Successfully";
            }
            else
            {
                var propUnitUpdate = db.tbl_PropertyUnits.Where(p => p.UID == model.UID).FirstOrDefault();
                if (propUnitUpdate != null)
                {
                    propUnitUpdate.PID = model.PID;
                    propUnitUpdate.UID = model.UID;
                    propUnitUpdate.UnitNo = model.UnitNo;
                    propUnitUpdate.Rooms = model.Rooms;
                    propUnitUpdate.Bedroom = model.Bedroom;
                    propUnitUpdate.Bathroom = model.Bathroom;
                    propUnitUpdate.Hall = model.Hall;
                    propUnitUpdate.Deposit = Convert.ToDecimal(model.Deposit);
                    propUnitUpdate.Current_Rent = model.Current_Rent;
                    propUnitUpdate.Previous_Rent = model.Previous_Rent;
                    propUnitUpdate.Market_Rent = model.Market_Rent;
                    propUnitUpdate.Wing = model.Wing;
                    propUnitUpdate.Building = model.Building;
                    propUnitUpdate.Leased = model.Leased;
                    propUnitUpdate.PetDetails = model.PetDetails;
                    propUnitUpdate.FloorNo = model.FloorNo;
                    propUnitUpdate.Area = model.Area;
                    if (sysFileName != "")
                    {
                        propUnitUpdate.FloorPlan = sysFileName;
                    }

                    propUnitUpdate.Carpet_Color = model.Carpet_Color;
                    propUnitUpdate.Wall_Paint_Color = model.Wall_Paint_Color;
                    propUnitUpdate.Furnished = model.Furnished;
                    propUnitUpdate.Washer = model.Washer;
                    propUnitUpdate.Refrigerator = model.Refrigerator;
                    propUnitUpdate.Drapes = model.Drapes;
                    propUnitUpdate.Dryer = model.Dryer;
                    propUnitUpdate.Dishwasher = model.Dishwasher;
                    propUnitUpdate.Disposal = model.Disposal;
                    propUnitUpdate.Elec_Range = model.Elec_Range;
                    propUnitUpdate.Gas_Range = model.Gas_Range;
                    propUnitUpdate.Carpet = model.Carpet;
                    propUnitUpdate.Air_Conditioning = model.Air_Conditioning;
                    propUnitUpdate.Fireplace = model.Fireplace;
                    propUnitUpdate.Den = model.Den;
                    propUnitUpdate.AvailableDate = model.AvailableDate;
                    propUnitUpdate.OccupancyDate = model.OccupancyDate;
                    propUnitUpdate.PendingMoveIn = model.PendingMoveIn;
                    propUnitUpdate.VacancyLoss_Date = model.VacancyLoss_Date;
                    propUnitUpdate.IntendedMoveIn_Date = model.IntendedMoveIn_Date;
                    propUnitUpdate.IntendMoveOutDate = model.IntendMoveOutDate;
                    propUnitUpdate.ActualMoveInDate = model.ActualMoveInDate;
                    propUnitUpdate.ActualMoveOutDate = model.ActualMoveOutDate;
                    propUnitUpdate.Coordinates = model.Coordinates;
                    propUnitUpdate.Premium = model.Premium;
                    propUnitUpdate.BalconyArea = model.BalconyArea;
                    propUnitUpdate.InteriorArea = model.InteriorArea;
                    propUnitUpdate.Notes = model.Notes;
                }
                db.SaveChanges();
                msg = "Property Unit Details Updated Successfully";
            }

            // Unit Wise Rent //
            var unitWiseRent = db.tbl_UnitLeasePrice.Where(p => p.UnitID == model.UID).ToList();
            if(unitWiseRent.Count>0)
            {
                db.tbl_UnitLeasePrice.RemoveRange(unitWiseRent);
            }

            string[] unitWiseRentData = model.UnitWiseRentData.Split('|');
            foreach(string uwrd in unitWiseRentData)
            {
                string[] leaseRent = uwrd.Split(',');
                var saveUWR = new tbl_UnitLeasePrice() {
                    UnitID = model.UID,
                    LeaseID = Convert.ToInt32(leaseRent[0]),
                    Price=Convert.ToDecimal(leaseRent[1]),
                    Deposit=0
                };

                db.tbl_UnitLeasePrice.Add(saveUWR);
                db.SaveChanges();
            }
            // Unit Wise Rent //

            return model.UID;
        }
        public long UpdateAvailDate(PropertyUnits model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.UID != 0)
            {
                var propUnitUpdate = db.tbl_PropertyUnits.Where(p => p.UID == model.UID).FirstOrDefault();
                if (propUnitUpdate != null)
                {
                    propUnitUpdate.AvailableDate = model.AvailableDate;
                }
                db.SaveChanges();
                msg = "Property Unit Details Updated Successfully";
            }
            return model.UID;
        }
        public long UpdateMoveInDate(PropertyUnits model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.UID != 0)
            {
                var propUnitUpdate = db.tbl_PropertyUnits.Where(p => p.UID == model.UID).FirstOrDefault();
                if (propUnitUpdate != null)
                {
                    propUnitUpdate.ActualMoveInDate = model.ActualMoveInDate;
                }
                db.SaveChanges();
                msg = "Property Unit Details Updated Successfully";
            }
            return model.UID;
        }
        public long UpdateMoveOutDate(PropertyUnits model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.UID != 0)
            {
                var propUnitUpdate = db.tbl_PropertyUnits.Where(p => p.UID == model.UID).FirstOrDefault();
                if (propUnitUpdate != null)
                {
                    propUnitUpdate.ActualMoveOutDate = model.ActualMoveOutDate;
                }
                db.SaveChanges();
                msg = "Property Unit Details Updated Successfully";
            }
            return model.UID;
        }
        public long UpdateRent(PropertyUnits model)
        {
            //string msg = "";
            //ShomaRMEntities db = new ShomaRMEntities();
            //if (model.UID != 0)
            //{
            //    var propUnitUpdate = db.tbl_PropertyUnits.Where(p => p.UID == model.UID).FirstOrDefault();
            //    if (propUnitUpdate != null)
            //    {
            //        propUnitUpdate.Current_Rent = model.Current_Rent;
            //    }
            //    db.SaveChanges();
            //    msg = "Property Unit Details Updated Successfully";
            //}
            //else
            //{

            //}
            //return model.UID;
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.ULRID != 0)
            {
                var propUnitLeaseUpdate = db.tbl_UnitLeasePrice.Where(p => p.ULPID == model.ULRID).FirstOrDefault();
                if (propUnitLeaseUpdate != null)
                {
                    propUnitLeaseUpdate.Price    = model.Current_Rent;
                    db.SaveChanges();
                    msg = "Property Unit Details Updated Successfully";
                }
            }
            else
            {
                var leaseTerm = db.tbl_LeaseTerms.ToList();
                foreach(var lt in leaseTerm)
                {
                    var saveULP = new tbl_UnitLeasePrice()
                    {
                        LeaseID=lt.LTID,
                        UnitID=model.UID,
                        Price=model.Current_Rent,
                        Deposit=0
                    };
                    db.tbl_UnitLeasePrice.Add(saveULP);
                    db.SaveChanges();
                }

            }
            db.Dispose();
            return model.UID;
        }
        public long UpdateUnitNotes(PropertyUnits model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.UID != 0)
            {
                var propUnitUpdate = db.tbl_PropertyUnits.Where(p => p.UID == model.UID).FirstOrDefault();
                if (propUnitUpdate != null)
                {
                    propUnitUpdate.Notes = model.Notes;
                }
                db.SaveChanges();
                msg = "Property Unit Details Updated Successfully";
            }
            return model.UID;
        }
        public string DeleteUnit(long UID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (UID != 0)
            {
                var appnowData = db.tbl_ApplyNow.Where(p => p.PropertyId == UID).FirstOrDefault();
                if (appnowData == null)
                {

                    var unitData = db.tbl_PropertyUnits.Where(p => p.UID == UID).FirstOrDefault();
                    if (unitData != null)
                    {
                        db.tbl_PropertyUnits.Remove(unitData);
                        db.SaveChanges();
                        msg = "Unit Removed Successfully";

                    }
                }
                else
                {
                    msg = "Unit Unable to remove";
                }
            }
            db.Dispose();
            return msg;
        }
        public List<UnitLeasePrice> GetUnitLeasePrice(long UID)
        {
            List<UnitLeasePrice> unitListPrice = new List<UnitLeasePrice>();
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUnitLeasewiseRent";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramCUID = cmd.CreateParameter();
                    paramCUID.ParameterName = "UnitId";
                    paramCUID.Value = UID;
                    cmd.Parameters.Add(paramCUID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    UnitLeasePrice pr = new UnitLeasePrice();

                    pr.LTID = Convert.ToInt32(dr["LTID"].ToString());
                    pr.LeaseTerms = Convert.ToInt32(dr["LeaseTerms"].ToString());
                    pr.Price = Convert.ToDecimal(dr["Price"].ToString());
                    pr.Deposit = Convert.ToDecimal(dr["Deposit"].ToString());
                    unitListPrice.Add(pr);
                }
                db.Dispose();
            }
            catch (Exception ex)
            {

            }
            return unitListPrice;
        }
        public List<PropertyFloor> GetFloorList(long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PropertyFloor> model = new List<PropertyFloor>();
            var floorlist = db.tbl_PropertyFloor.Where(p => p.PID == PID).ToList().OrderBy(p => p.FloorNo);
            if (floorlist != null)
            {
                foreach (var fl in floorlist)
                {
                    model.Add(new PropertyFloor()
                    {
                        FloorNo = fl.FloorNo,
                        Coordinates = fl.Coordinates,
                        FloorID = fl.FloorID,
                    });

                }
            }
            return model;
        }
        public List<ModelsModel> GetModelsListDetail()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ModelsModel> lstpr = new List<ModelsModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetModelsList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ModelsModel pr = new ModelsModel();
                    pr.ModelID = Convert.ToInt32(dr["ModelID"].ToString());
                    pr.BalconyArea = dr["BalconyArea"].ToString();
                    pr.InteriorArea = dr["InteriorArea"].ToString();
                    pr.Area = dr["Area"].ToString();
                    pr.RentRange = "$" + Convert.ToDecimal(dr["MinRent"].ToString()).ToString("0.00") + "-$" + Convert.ToDecimal(dr["MaxRent"].ToString()).ToString("0.00");
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.FloorPlan = dr["FloorPlan"].ToString();
                    pr.ModelName = dr["ModelName"].ToString();
                    pr.BalconyArea = dr["BalconyArea"].ToString() != null ? dr["BalconyArea"].ToString() : "0";
                    pr.InteriorArea = dr["InteriorArea"].ToString() != null ? dr["InteriorArea"].ToString() : "0";
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
        public long UpdateUnitLeasePrice(long ULPID, decimal Price)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (ULPID != 0)
            {
                var propUnitLeaseUpdate = db.tbl_UnitLeasePrice.Where(p => p.ULPID == ULPID).FirstOrDefault();
                if (propUnitLeaseUpdate != null)
                {
                    propUnitLeaseUpdate.Price = Price;
                    db.SaveChanges();
                    msg = "Property Unit Details Updated Successfully";
                }
            }
            
            db.Dispose();
            return ULPID;
        }
    }
    public partial class PropertyFloor
    {
        public long FloorID { get; set; }
        public Nullable<long> PID { get; set; }
        public string Coordinates { get; set; }
        public string FloorNo { get; set; }
        public string FloorPlan { get; set; }
        public int IsAvail { get; set; }
        public List<PropertyUnits> lstPropertyUnit { get; set; }
        public string FileName { get; set; }

        public string SaveUpdateFloor(HttpPostedFileBase fb, PropertyFloor model)
        {
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/");
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);

                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/") + "/" + sysFileName;

                }
            }
            var utilityExists = db.tbl_PropertyFloor.Where(p => p.PID == model.PID && p.FloorNo == model.FloorNo).FirstOrDefault();
            if (utilityExists == null)
            {

                var data = new tbl_PropertyFloor()
                {
                    PID = model.PID,
                    FloorNo = model.FloorNo,
                    Coordinates = model.Coordinates,
                    FloorPlan = sysFileName,
                };
                db.tbl_PropertyFloor.Add(data);
                db.SaveChanges();
                model.FloorID = data.FloorID;

                return msg;
            }
            else
            {
                utilityExists.Coordinates = model.Coordinates;
                if (sysFileName != "")
                {
                    utilityExists.FloorPlan = sysFileName;
                }
                db.SaveChanges();
                throw new Exception("Floor Updated Successfully.");
            }
        }
        
        public PropertyFloor GetPropertyFloorDetails(int FloorID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PropertyFloor model = new PropertyFloor();
            List<PropertyUnits> listunit = new List<PropertyUnits>();
            model.lstPropertyUnit = listunit;
            var unitDet = db.tbl_PropertyFloor.Where(p => p.FloorID == FloorID).FirstOrDefault();
            if (unitDet != null)
            {
                model.PID = unitDet.PID;
                model.FloorPlan = unitDet.FloorPlan;
                model.FloorID = unitDet.FloorID;
                model.FloorNo = unitDet.FloorNo;
            }
            var unitlist = db.tbl_PropertyUnits.Where(p => p.FloorNo == FloorID).ToList();
            if (unitlist != null)
            {
                foreach (var fl in unitlist)
                {
                    model.lstPropertyUnit.Add(new PropertyUnits()
                    {
                        Coordinates = fl.Coordinates,
                        UnitNo = fl.UnitNo,
                        UID = fl.UID,
                    });


                }
            }
            return model;
        }
    }
    public class ModelsModel
    {
        public int ModelID { get; set; }
        public string Area { get; set; }
        public Nullable<decimal> Rent { get; set; }
        public Nullable<int> Bedroom { get; set; }
        public Nullable<int> Bathroom { get; set; }
        public string FloorPlan { get; set; }
        public string ModelName { get; set; }
        public string TempFileName { get; set; }
        public string FloorPlanDetails { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<decimal> MinRent { get; set; }
        public Nullable<decimal> MaxRent { get; set; }
        public string RentRange { get; set; }
        public string TempFloorPlanDetailsFileName { get; set; }
        public string BalconyArea { get; set; }
        public string InteriorArea { get; set; }
        public int PreviousID { get; set; }
        public int NextID { get; set; }

        public ModelsModel SaveUpdateModels(ModelsModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.ModelID == 0)
            {
                var SaveModels = new tbl_Models()
                {
                    Area = model.Area,
                    Rent = model.Rent,
                    Bedroom = model.Bedroom,
                    Bathroom = model.Bathroom,
                    FloorPlan = model.FloorPlan,
                    ModelName = model.ModelName,
                    MaxRent=model.MaxRent,
                    MinRent=model.MinRent,
                    Deposit=model.Deposit,
                    BalconyArea = model.BalconyArea,
                    InteriorArea = model.InteriorArea,
                };
                db.tbl_Models.Add(SaveModels);
                db.SaveChanges();
            }
            else
            {
                var UpdateModels = db.tbl_Models.Where(co => co.ModelID == model.ModelID).FirstOrDefault();

                if (UpdateModels != null)
                {
                    string oldmodelName = UpdateModels.ModelName;
                    decimal olddeposit = UpdateModels.Deposit??0;


                    UpdateModels.Area = model.Area;
                    UpdateModels.Rent = model.Rent;
                    UpdateModels.Bedroom = model.Bedroom;
                    UpdateModels.Bathroom = model.Bathroom;
                    UpdateModels.FloorPlan = model.FloorPlan;
                    UpdateModels.ModelName = model.ModelName;
                    UpdateModels.MaxRent = model.MaxRent;
                    UpdateModels.MinRent = model.MinRent;
                    UpdateModels.Deposit = model.Deposit;
                    UpdateModels.BalconyArea = model.BalconyArea;
                    UpdateModels.InteriorArea = model.InteriorArea;
                    db.SaveChanges();

                    if (oldmodelName != model.ModelName || olddeposit != (model.Deposit ?? 0))
                    {
                        DataTable dtTable = new DataTable();
                        using (var cmd = db.Database.Connection.CreateCommand())
                        {
                            db.Database.Connection.Open();
                            cmd.CommandText = "usp_UpdateUnitDepostiteAndModelName";
                            cmd.CommandType = CommandType.StoredProcedure;

                            DbParameter paramOMN = cmd.CreateParameter();
                            paramOMN.ParameterName = "OldModelName";
                            paramOMN.Value = oldmodelName;
                            cmd.Parameters.Add(paramOMN);

                            DbParameter paramNMN = cmd.CreateParameter();
                            paramNMN.ParameterName = "NewModelName";
                            paramNMN.Value = model.ModelName;
                            cmd.Parameters.Add(paramNMN);

                            DbParameter paramDA = cmd.CreateParameter();
                            paramDA.ParameterName = "Deposit";
                            paramDA.Value = model.Deposit ?? 0;
                            cmd.Parameters.Add(paramDA);

                            DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(dtTable);
                            db.Database.Connection.Close();
                        }

                    }
                }
            }
            return model;
        }

        public ModelsModel GetModelsData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ModelsModel model = new ModelsModel();

            var GetModelsData = db.tbl_Models.Where(p => p.ModelID == Id).OrderBy(p => p.ModelName).FirstOrDefault();

            //var NextData = db.tbl_Models.Where(p => p.ModelID > Id).OrderBy(p => p.ModelName).FirstOrDefault();
            //var PreviousData = db.tbl_Models.Where(p => p.ModelID < Id).OrderByDescending(p => p.ModelName).FirstOrDefault();
            var i = db.tbl_Models.ToList();
            var NextData = i.SkipWhile(co => co.ModelID != Id).Skip(1).FirstOrDefault();
            var PreviousData = i.TakeWhile(co => co.ModelID != Id).Take(1).FirstOrDefault();

            if (GetModelsData != null)
            {
                model.ModelID = GetModelsData.ModelID;
                model.Area = GetModelsData.Area;
                model.Rent = GetModelsData.Rent;
                model.Bedroom = GetModelsData.Bedroom;
                model.Bathroom = GetModelsData.Bathroom;
                model.Deposit = GetModelsData.Deposit;
                model.MaxRent = GetModelsData.MaxRent;
                model.MinRent = GetModelsData.MinRent;
                model.FloorPlan = GetModelsData.FloorPlan;
                model.ModelName = GetModelsData.ModelName;
                model.FloorPlanDetails = GetModelsData.FloorPlanDetails;
                model.BalconyArea = GetModelsData.BalconyArea;
                model.InteriorArea = GetModelsData.InteriorArea;
                model.PreviousID = PreviousData!=null? PreviousData.ModelID:0;
                model.NextID = NextData != null ? NextData.ModelID:0;
            }
            model.ModelID = Id;
            return model;
        }
        public ModelsModel GetPropertyModelDetails(string ModelName)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ModelsModel model = new ModelsModel();

            var GetModelsData = db.tbl_Models.Where(p => p.ModelName == ModelName).FirstOrDefault();

            if (GetModelsData != null)
            {
                model.ModelID = GetModelsData.ModelID;
                model.Area = GetModelsData.Area;
                model.Rent = GetModelsData.Rent;
                model.Bedroom = GetModelsData.Bedroom;
                model.Bathroom = GetModelsData.Bathroom;
                model.FloorPlan = GetModelsData.FloorPlan;
                model.ModelName = GetModelsData.ModelName;
                model.Deposit = GetModelsData.Deposit;
                model.MaxRent = GetModelsData.MaxRent;
                model.MinRent = GetModelsData.MinRent;
                model.BalconyArea = GetModelsData.BalconyArea;
                model.InteriorArea = GetModelsData.InteriorArea;
            }
           
            return model;
        }

        public ModelsModel UploadModelsFloorPlan(HttpPostedFileBase fb, ModelsModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";

            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fb.FileName;
                sysFileName = model.ModelName + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/") + "/" + sysFileName;

                }
                model.TempFileName = sysFileName;

            }
            return model;
        }

        public List<ModelsModel> GetModelsListDetail()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ModelsModel> lstpr = new List<ModelsModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetModelsList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ModelsModel pr = new ModelsModel();
                    pr.ModelID = Convert.ToInt32(dr["ModelID"].ToString());
                    pr.BalconyArea = dr["BalconyArea"].ToString();
                    pr.InteriorArea = dr["InteriorArea"].ToString();
                    pr.Area = dr["Area"].ToString();
                    pr.RentRange = "$" + Convert.ToDecimal(dr["MinRent"].ToString()).ToString("0.00") + "-$" + Convert.ToDecimal(dr["MaxRent"].ToString()).ToString("0.00");
                    pr.Bedroom = Convert.ToInt32(dr["Bedroom"].ToString());
                    pr.Bathroom = Convert.ToInt32(dr["Bathroom"].ToString());
                    pr.FloorPlan = dr["FloorPlan"].ToString();
                    pr.ModelName = dr["ModelName"].ToString();
                    pr.BalconyArea = dr["BalconyArea"].ToString() != null ? dr["BalconyArea"].ToString() : "0";
                    pr.InteriorArea = dr["InteriorArea"].ToString() != null ? dr["InteriorArea"].ToString() : "0";
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
        public ModelsModel UploadModelsFloorPlanDetails(HttpPostedFileBase fb, ModelsModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";

            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fb.FileName;
                sysFileName = model.ModelName + "Det" + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/plan/") + "/" + sysFileName;

                }
                model.TempFloorPlanDetailsFileName = sysFileName;

            }
            return model;
        }
        public string DeleteModel(int ModelID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (ModelID != 0)
            {

                var vehData = db.tbl_Models.Where(p => p.ModelID == ModelID).FirstOrDefault();
                if (vehData != null)
                {
                    db.tbl_Models.Remove(vehData);
                    db.SaveChanges();
                    msg = "Model Removed Successfully";

                }


            }

            db.Dispose();
            return msg;


        }
    }

    public class UnitLeasePrice
    {
        public int LTID { get; set; }
        public int? LeaseTerms { get; set; }
        public decimal? Price { get; set; }
        public decimal? Deposit { get; set; }
    }

    public class UnitLeaseWisePriceList
    {
        public long UID { get; set; }
        public string UnitNo { get; set; }
        public string Building { get; set; }
        public long ULPID { get; set; }
        public long LeaseID { get; set; }
        public decimal Price { get; set; }
    } 

    public class UnitLeasePriceHeader
    {
        public string HeaderName { get; set; }
        public string SortName { get; set; }
        public string Width { get; set; }
        public string DefaultSort { get; set; }
        public string HasSorting { get; set; }
    }
}