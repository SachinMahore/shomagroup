using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Areas.Admin.Models
{
    public class VendorModel
    {
        public long Vendor_ID { get; set; }
        public string Vendor_Name { get; set; }
        public string Mobile_Number { get; set; }
        public string Email_Id { get; set; }
        public string Address { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }

        public string SaveUpdateVendor(VendorModel model)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.Vendor_ID == 0)
            {
                var SaveVendor = new tbl_Vendor()
                {
                    Vendor_Name = model.Vendor_Name,
                    Mobile_Number = model.Mobile_Number,
                    Email_Id = model.Email_Id,
                    Address = model.Address,
                    State = model.State,
                    City = model.City,
                    Created_By = userid,
                    Created_Date = DateTime.Now.Date
                };
                db.tbl_Vendor.Add(SaveVendor);
                db.SaveChanges();
                msg = "Vendor Save Successfully";
            }
            else
            {
                var UpdateVendor = db.tbl_Vendor.Where(co => co.Vendor_ID == model.Vendor_ID).FirstOrDefault();
                if (UpdateVendor != null)
                {
                    UpdateVendor.Vendor_Name = model.Vendor_Name;
                    UpdateVendor.Mobile_Number = model.Mobile_Number;
                    UpdateVendor.Email_Id = model.Email_Id;
                    UpdateVendor.Address = model.Address;
                    UpdateVendor.State = model.State;
                    UpdateVendor.City = model.City;

                }
                db.SaveChanges();
                msg = "Vendor Updated Successfully";
            }
            db.Dispose();
            return msg;
        }

        public VendorModel GetVendorData(int Id)
        {
            VendorModel model = new VendorModel();
            ShomaRMEntities db = new ShomaRMEntities();

            var GetVendorData = db.tbl_Vendor.Where(co => co.Vendor_ID == Id).FirstOrDefault();

            if (GetVendorData != null)
            {
                model.Vendor_Name = GetVendorData.Vendor_Name;
                model.Mobile_Number = GetVendorData.Mobile_Number;
                model.Email_Id = GetVendorData.Email_Id;
                model.Address = GetVendorData.Address;
                model.State = GetVendorData.State;
                model.City = GetVendorData.City;
            }
            model.Vendor_ID = Id;
            return model;
        }
        public int BuildPaganationVendorList(VendorSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetVendorPaginationAndSearchData";
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
        public List<VendorSearchModel> GetVendorDataList(VendorSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<VendorSearchModel> lstVendor = new List<VendorSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetVendorPaginationAndSearchData";
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
                    VendorSearchModel searchmodel = new VendorSearchModel();
                    searchmodel.Vendor_ID = Convert.ToInt64(dr["Vendor_ID"].ToString());
                    searchmodel.Vendor_Name = dr["Vendor_Name"].ToString();
                    searchmodel.Mobile_Number = dr["Mobile_Number"].ToString();
                    searchmodel.Email_Id = dr["Email_Id"].ToString();
                    searchmodel.Address = dr["Address"].ToString();
                    searchmodel.State = dr["State"].ToString();
                    searchmodel.City = dr["City"].ToString();
                    lstVendor.Add(searchmodel);
                }
                db.Dispose();
                return lstVendor.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public class VendorSearchModel
        {
            public long Vendor_ID { get; set; }
            public string Vendor_Name { get; set; }
            public string Mobile_Number { get; set; }
            public string Email_Id { get; set; }
            public string Address { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfPages { get; set; }
            public int NumberOfRows { get; set; }
        }
        public List<VendorModel> VendorList()
        {
            List<VendorModel> list = new List<VendorModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var VendorList = db.tbl_Vendor.ToList();
            foreach (var item in VendorList)
            {
                list.Add(new VendorModel()
                {
                    Vendor_ID = item.Vendor_ID,
                    Vendor_Name = item.Vendor_Name
                });
            }
            return list;
        }
    }
}