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
using ShomaRM.Models;

namespace ShomaRM.Areas.Admin.Models
{
    public class UsersModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> IsSuperUser { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateID { get; set; }
        public string ZipCode { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public Nullable<int> IsActive { get; set; }
        public string Extension { get; set; }
        public Nullable<int> UserType { get; set; }
        public string UserCode { get; set; }
        public string Timezone { get; set; }
        public string ExtWorkPhone { get; set; }
        public string ExtCellPhone { get; set; }
        public int HasRight { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public int BuildPaganationUserList(UserSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUserPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "Filter";
                    paramDF.Value = model.Filter;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "Criteria";
                    paramDT.Value = !string.IsNullOrEmpty(model.Criteria) ? model.Criteria : "";
                    cmd.Parameters.Add(paramDT);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

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
        public List<UserSearchModel> FillUserSearchGrid(UserSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<UserSearchModel> lstUser = new List<UserSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUserPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "Filter";
                    paramDF.Value = model.Filter;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "Criteria";
                    paramDT.Value = !string.IsNullOrEmpty(model.Criteria) ? model.Criteria : "";
                    cmd.Parameters.Add(paramDT);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    UserSearchModel searchmodel = new UserSearchModel();
                    searchmodel.UserID = Convert.ToInt64(dr["UserID"].ToString());
                    searchmodel.FirstName = dr["FirstName"].ToString();
                    searchmodel.LastName = dr["LastName"].ToString();
                    searchmodel.Username = dr["Username"].ToString();
                    searchmodel.Status = dr["Status"].ToString();
                    searchmodel.UserType = dr["UserType"].ToString();
                    lstUser.Add(searchmodel);
                }
                db.Dispose();
                return lstUser.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public class UserSearchModel
        {
            public long UserID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string Status { get; set; }
            public string Filter { get; set; }
            public string Criteria { get; set; }
            public string UserType { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfPages { get; set; }
            public int NumberOfRows { get; set; }
            public string SortBy { get; set; }
            public string OrderBy { get; set; }
        }
        public List<UserList> GetUserListByType(int UserType)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<UserList> model = new List<UserList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUserListByType";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "UserType";
                    paramF.Value = UserType;
                    cmd.Parameters.Add(paramF);
                    
                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    UserList usm = new UserList();
                    usm.UserID = int.Parse(dr["UserID"].ToString());
                    usm.FirstName = dr["FirstName"].ToString();
                    usm.LastName = dr["LastName"].ToString();
                    usm.Username = dr["Username"].ToString();
                    usm.Status = dr["Status"].ToString();
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
        public string GetUserType(int UserType)
        {
            if (UserType == 1)
            {
                return "Administrator";
            }
            else if (UserType == 2)
            {
                return "Applicant";
            }
            else if (UserType == 3)
            {
                return "Tenant";
            }
            else if (UserType == 4)
            {
                return "Property Manager";
            }
            else if (UserType == 5)
            {
                return "Service Manager";
            }
            else if (UserType == 6)
            {
                return "Sales Agent";
            }
            else if (UserType == 7)
            {
                return "Service Person";
            }
            return "";
        }
        public UsersModel GetUserInfo(int UserID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            UsersModel model = new UsersModel();
            model.UserID = 0;
            model.FirstName = "";
            model.LastName = "";
            model.Address1 = "";
            model.Address2 = "";
            model.City = "";
            model.StateID = "";
            model.ZipCode = "";
            model.WorkPhone = "";
            model.Extension = "";
            model.CellPhone = "";
            model.Email = "";
            model.UserType = 0;
            model.Username = "";
            model.Password = "";
            model.IsActive = 1;
            if (UserID > 0)
            {
                var userInfo = db.tbl_Login.Where(p => p.UserID == UserID).FirstOrDefault();
                if (userInfo != null)
                {
                    model.UserID = userInfo.UserID;
                    model.FirstName = userInfo.FirstName;
                    model.LastName = userInfo.LastName;
                  
                    model.CellPhone = userInfo.CellPhone;
                    model.Email = userInfo.Email;
                
                    model.Username = userInfo.Username;
                    model.Password = userInfo.Password;
                    model.IsActive = (userInfo.IsActive.HasValue ? userInfo.IsActive.Value : 0);
                    model.UserType = userInfo.UserType;
                }
            }
            return model;
        }
        public int SaveUpdateUser(UsersModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_Login.Where(p => p.UserID != model.UserID && p.Username == model.Username).FirstOrDefault();
            string encpassword = new EncryptDecrypt().EncryptText(model.Password);
            if (userNameExists == null)
            {
                if (model.UserID == 0)
                {
                    
                    var userData = new tbl_Login()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                      
                        CellPhone = model.CellPhone,
                        Email = model.Email,
                        Username = model.Username,
                        Password = encpassword,
                        IsActive = model.IsActive,
                        IsSuperUser = 0,
                        UserType = model.UserType
                    };
                    db.tbl_Login.Add(userData);
                    db.SaveChanges();
                    model.UserID = userData.UserID;
                }
                else
                {
                    var userInfo = db.tbl_Login.Where(p => p.UserID == model.UserID).FirstOrDefault();
                    if (userInfo != null)
                    {
                        userInfo.FirstName = model.FirstName;
                        userInfo.LastName = model.LastName;
                      
                        userInfo.CellPhone = model.CellPhone;
                        userInfo.Email = model.Email;
                      
                        userInfo.Username = model.Username;
                        userInfo.Password = encpassword;
                        userInfo.IsActive = model.IsActive;
                        userInfo.UserType = model.UserType;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.Username + " not exists in the system.");
                    }
                }

                return model.UserID;
            }
            else
            {
                throw new Exception(model.Username + " already exists in the system.");
            }
        }

        public string UpdatePasswordUser(UsersModel model)
        {
          
            string encryptedNewPassword = new EncryptDecrypt().EncryptText(model.NewPassword);
            string encryptedOldPassword = new EncryptDecrypt().EncryptText(model.OldPassword);
            string msg = "";
            long uid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0; 
            ShomaRMEntities db = new ShomaRMEntities();
           
            var userNameExists = db.tbl_Login.Where(p => p.UserID == uid && p.Password == encryptedOldPassword).FirstOrDefault();
           

            if (userNameExists != null)
            {
                tbl_Login userData = new tbl_Login();

                userNameExists.Password = encryptedNewPassword;
                db.SaveChanges();
                msg = "Password Change Successfully";

            }
            else
            {
                msg = "Password / User Does Not Match with old";
            }
            return msg;
        }
    }
    public class UserList
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public string Username { get; set; }
        public string Status { get; set; }
    }
}