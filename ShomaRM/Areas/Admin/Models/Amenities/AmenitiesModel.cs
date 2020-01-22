using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Net;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class AmenitiesModel
    {
        public long ID { get; set; }
        public string Amenity { get; set; }
        public string AmenityDetails { get; set; }
        public List<AmenityList> GetAmenityList(string Amenity)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenityList> model = new List<AmenityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAmenityList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = Amenity;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    AmenityList usm = new AmenityList();
                    usm.ID = int.Parse(dr["ID"].ToString());
                    usm.Amenity = dr["Amenity"].ToString();
                    usm.AmenityDetails = dr["AmenityDetails"].ToString();
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
        public int BuildPaganationAmenityList(AmenityList model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenityList> lstAmenity = new List<AmenityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAmenityPaginationAndSearchData";
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

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                if (dtTable.Rows.Count>0)
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
        public List<AmenityList> FillAmenitySearchGrid(AmenityList model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<AmenityList> lstAmenity = new List<AmenityList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetAmenityPaginationAndSearchData";
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

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    AmenityList usm = new AmenityList();
                    usm.ID = int.Parse(dr["ID"].ToString());
                    usm.Amenity = dr["Amenity"].ToString();
                    usm.AmenityDetails = dr["AmenityDetails"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstAmenity.Add(usm);
                }
                db.Dispose();
                return lstAmenity.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public AmenitiesModel GetAmenityInfo(int AmenityID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            AmenitiesModel model = new AmenitiesModel();
            model.ID = 0;
            model.Amenity = "";
            model.AmenityDetails = "";

            var AmenityInfo = db.tbl_Amenities.Where(p => p.ID == AmenityID).FirstOrDefault();
            if (AmenityInfo != null)
            {
                model.ID = AmenityInfo.ID;
                model.Amenity = AmenityInfo.Amenity;
                model.AmenityDetails = AmenityInfo.AmenityDetails;
            }
            return model;
        }
        public long SaveUpdateAmenity(AmenitiesModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var amenityExists = db.tbl_Amenities.Where(p => p.ID != model.ID && p.Amenity == model.Amenity).FirstOrDefault();
            if (amenityExists == null)
            {
                if (model.ID == 0)
                {
                    var data = new tbl_Amenities()
                    {
                        Amenity = model.Amenity,
                        AmenityDetails = model.AmenityDetails,
                    };
                    db.tbl_Amenities.Add(data);
                    db.SaveChanges();
                    model.ID = data.ID;
                }
                else
                {
                    var AmenityInfo = db.tbl_Amenities.Where(p => p.ID == model.ID).FirstOrDefault();
                    if (AmenityInfo != null)
                    {
                        AmenityInfo.Amenity = model.Amenity;
                        AmenityInfo.AmenityDetails = model.AmenityDetails;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.Amenity + " not exists in the system.");
                    }
                }
                return model.ID;
            }
            else
            {
                throw new Exception(model.Amenity + " already exists in the system.");
            }
        }


        public string DeleteAmenities(long AmenitiesID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (AmenitiesID != 0)
            {

                var amenitiesData = db.tbl_Amenities.Where(p => p.ID == AmenitiesID).FirstOrDefault();
                if (amenitiesData != null)
                {
                    db.tbl_Amenities.Remove(amenitiesData);
                    db.SaveChanges();
                    msg = "Amenities Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
    }
    public class AmenityList
    {
        public long ID { get; set; }
        public string Amenity { get; set; }
        public string AmenityDetails { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfRows { get; set; }
        public String Criteria { get; set; }
    }
}