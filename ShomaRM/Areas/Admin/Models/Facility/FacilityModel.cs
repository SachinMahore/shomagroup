using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class FacilityModel
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string SaveUpdateFacility(HttpPostedFileBase fb, FacilityModel model)
        {
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Facility/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/Facility/") + "/" + sysFileName;
                }
            }
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.FacilityID == 0)
            {
                var saveFacility = new tbl_Facility()
                {
                    FacilityName = model.FacilityName,
                    PropertyID = model.PropertyID,
                    Photo = sysFileName,
                    Description = model.Description,
                    CreatedByID = userid,
                    CreatedDate = DateTime.Now.Date
                };
                db.tbl_Facility.Add(saveFacility);
                db.SaveChanges();
                msg = "Facility Save Successfully";
            }
            else
            {
                string PhotoName = "";
                var GetFacilityData = db.tbl_Facility.Where(p => p.FacilityID == model.FacilityID).FirstOrDefault();
                PhotoName = GetFacilityData.Photo;
                if (PhotoName != sysFileName && sysFileName != "")
                {
                    PhotoName = sysFileName;
                }
                if (GetFacilityData != null)
                {
                    GetFacilityData.FacilityName = model.FacilityName;
                    GetFacilityData.PropertyID = model.PropertyID;
                    GetFacilityData.Photo = PhotoName;
                    GetFacilityData.Description = model.Description;
                    //GetFacilityData.CreatedByID = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID;
                    //GetFacilityData.CreatedDate = DateTime.Now.Date;
                    db.SaveChanges();
                    msg = "Facility Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }
        public FacilityModel GetFacilityData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            FacilityModel model = new FacilityModel();
            var GetFacilityData = db.tbl_Facility.Where(p => p.FacilityID == Id).FirstOrDefault();
            if (GetFacilityData != null)
            {
                model.FacilityName = GetFacilityData.FacilityName;
                model.PropertyID = GetFacilityData.PropertyID;
                model.Photo = GetFacilityData.Photo;
                model.Description = GetFacilityData.Description;
            }
            model.FacilityID = Id;
            return model;
        }
        public List<FacilityModel> FacilityList()
        {
            List<FacilityModel> list = new List<FacilityModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var FacilityList = db.tbl_Facility.ToList();
            foreach (var item in FacilityList)
            {
                list.Add(new FacilityModel()
                {
                    FacilityID = item.FacilityID,
                    FacilityName = item.FacilityName
                });
            }
            return list;
        }
        public class FacilitySearchModel
        {
            public long FacilityID { get; set; }
            public string FacilityName { get; set; }
            public string PropertyName { get; set; }
            public string Photo { get; set; }
            public string Description { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
        }
        public int BuildPaganationFacilityList(FacilitySearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetFacilityPaginationAndSearchData";
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
        public List<FacilitySearchModel> FillFacilitySearchGrid(FacilitySearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<FacilitySearchModel> lstFacility = new List<FacilitySearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetFacilityPaginationAndSearchData";
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
                    FacilitySearchModel searchmodel = new FacilitySearchModel();
                    searchmodel.FacilityID = Convert.ToInt64(dr["FacilityID"].ToString());
                    searchmodel.FacilityName = dr["FacilityName"].ToString();
                    searchmodel.PropertyName = dr["PropertyName"].ToString();
                    searchmodel.Photo = dr["Photo"].ToString();
                    searchmodel.Description = dr["Description"].ToString();
                    lstFacility.Add(searchmodel);
                }
                db.Dispose();
                return lstFacility.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
    }
}