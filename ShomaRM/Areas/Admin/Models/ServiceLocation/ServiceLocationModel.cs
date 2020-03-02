using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class ServiceLocationModel
    {
        public int LocationID { get; set; }
        public string Location { get; set; }

        
        public ServiceLocationModel GetServiceLocationInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServiceLocationModel model = new ServiceLocationModel();
           

            var ServiceLocation = db.tbl_ServiceLocation.Where(p => p.LocationID == ID).FirstOrDefault();
            if (ServiceLocation != null)
            {
                model.LocationID = ServiceLocation.LocationID;
                model.Location = ServiceLocation.Location;
            }

            return model;
        }
        public long SaveUpdateServiceLocation(ServiceLocationModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_ServiceLocation.Where(p => p.LocationID != model.LocationID && p.Location == model.Location).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.LocationID == 0)
                {
                    var LocationData = new tbl_ServiceLocation()
                    {
                        Location = model.Location
                    };
                    db.tbl_ServiceLocation.Add(LocationData);
                    db.SaveChanges();
                    model.LocationID = LocationData.LocationID;
                }
                else
                {
                    var LocationData = db.tbl_ServiceLocation.Where(p => p.LocationID == model.LocationID).FirstOrDefault();
                    if (LocationData != null)
                    {
                        LocationData.Location = model.Location;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.Location + " not exists in the system.");
                    }
                }

                return model.LocationID;
            }
            else
            {
                throw new Exception(model.Location + " already exists in the system.");
            }
        }
        public ServiceLocationModel GetLocationSourceData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ServiceLocationModel model = new ServiceLocationModel();

            var GetLocationData = db.tbl_ServiceLocation.Where(p => p.LocationID == Id).FirstOrDefault();

            if (GetLocationData != null)
            {
                model.LocationID = GetLocationData.LocationID;
                model.Location = GetLocationData.Location;
            }
            model.LocationID = Id;
            return model;
        }
       
        public int BuildPaganationSLList(SLListModel model)
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
                    cmd.CommandText = "usp_GetServiceLocationPaginationAndSearchData";
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
        public List<SLListModel> FillLSSearchGrid(SLListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<SLListModel> lstData = new List<SLListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetServiceLocationPaginationAndSearchData";
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
                    SLListModel usm = new SLListModel();
                    usm.LocationID = int.Parse(dr["LocationID"].ToString());
                    usm.Location = dr["Location"].ToString();
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

        public string DeleteServiceLocation(long LocationID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (LocationID != 0)
            {
                var locationData = db.tbl_ServiceLocation.Where(p => p.LocationID == LocationID).FirstOrDefault();
                if (locationData != null)
                {
                    db.tbl_ServiceLocation.Remove(locationData);
                    db.SaveChanges();
                    msg = "Service Location  Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class SLListModel
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
        public string Criteria { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}