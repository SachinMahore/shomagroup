using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using ShomaRM.Data;

namespace ShomaRM.Models
{
    public class ShomaGroupWebSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionStateTokenRepository"/> class.
        /// </summary>
        /// <param name="sessionState">State of the session.</param>
        public ShomaGroupWebSession()
        {

        }

       

      

        public static CurrentUser CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUser"] == null)
                {
                    if (HttpContext.Current.Request.IsAuthenticated)
                    {
                        ShomaRMEntities db = new ShomaRMEntities();
                        var user = db.tbl_Login.Where(p => p.Username == HttpContext.Current.User.Identity.Name).First();
                        ShomaGroupWebSession _WebSession = new ShomaGroupWebSession();
                        var currentUser = new CurrentUser();
                        currentUser.UserID = user.UserID;
                        currentUser.Username = user.Username;
                        currentUser.FullName = user.FirstName + " " + user.LastName;
                        currentUser.EmailAddress = user.Email;
                        currentUser.IsAdmin = (user.IsSuperUser.HasValue ? user.IsSuperUser.Value : 0);
                        currentUser.EmailAddress = user.Email;
                        currentUser.UserType = Convert.ToInt32((user.UserType).ToString());
                        currentUser.LoggedInUser = user.FirstName;
                       
                        HttpContext.Current.Session["CurrentUser"] = currentUser;
                        db.Dispose();
                    }
                }
                return (HttpContext.Current.Session["CurrentUser"] == null) ? null : (CurrentUser)(HttpContext.Current.Session["CurrentUser"]);
            }
        }

        public void SetWebSession(CurrentUser user)
        {
            HttpContext.Current.Session["CurrentUser"] = user;
        }

        public void RemoveWebSession()
        {
            HttpContext.Current.Session["CurrentUser"] = null;
        }

        public UserAccessRights UserAccess(string ControllerName)
        {
            string specialRights = "";
            DataSet dsUserRights = GetUserAccessRightByUserIDAndController(CurrentUser.UserID, ControllerName);
            //DataSet dsUserRights = GetUserAccessRightByUserIDAndController(1, ControllerName);
            UserAccessRights userAccessRights = new UserAccessRights();
            userAccessRights.HasRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["HasRight"].ToString());
            userAccessRights.EditRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["EditRight"].ToString());
            userAccessRights.AddRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["AddRight"].ToString());
            userAccessRights.DeleteRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["DeleteRight"].ToString());
            userAccessRights.DispalyRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["DispalyRight"].ToString());
            foreach (DataRow drSR in dsUserRights.Tables["SpecialRights"].Rows)
            {
                if (specialRights == "")
                {
                    specialRights = "{\"RightName\":\"" + drSR["RightName"].ToString() + "\",\"SpecialRight\":\"" + drSR["SpecialRight"].ToString() + "\"}";
                }
                else
                {
                    specialRights += ",{\"RightName\":\"" + drSR["RightName"].ToString() + "\",\"SpecialRight\":\"" + drSR["SpecialRight"].ToString() + "\"}";
                }
            }
            if (specialRights == "")
            {
                specialRights = "{}";
            }
            userAccessRights.SpecialRights = specialRights;

            return userAccessRights;
        }
        public DataSet GetUserAccessRightByUserIDAndController(int UserID, string Controller)
        {
            DataSet dsRights = new DataSet();
            ShomaRMEntities db = new ShomaRMEntities();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUserAccess";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter paramUserID = cmd.CreateParameter();
                    paramUserID.ParameterName = "UserID";
                    paramUserID.Value = UserID;
                    cmd.Parameters.Add(paramUserID);

                    DbParameter paramController = cmd.CreateParameter();
                    paramController.ParameterName = "Controller";
                    paramController.Value = Controller;
                    cmd.Parameters.Add(paramController);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsRights);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }

            }

            dsRights.Tables[0].TableName = "AccessRights";
            dsRights.Tables[1].TableName = "SpecialRights";
            db.Dispose();
            return dsRights;
        }
        public static DataSet GetUserSpecialRightByUserIDAndController(int UserID, string Controller)
        {
            DataSet dsRights = new DataSet();
            ShomaRMEntities db = new ShomaRMEntities();
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                try
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetUserAccess";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DbParameter paramUserID = cmd.CreateParameter();
                    paramUserID.ParameterName = "UserID";
                    paramUserID.Value = UserID;
                    cmd.Parameters.Add(paramUserID);

                    DbParameter paramController = cmd.CreateParameter();
                    paramController.ParameterName = "Controller";
                    paramController.Value = Controller;
                    cmd.Parameters.Add(paramController);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dsRights);
                    db.Database.Connection.Close();
                }
                catch
                {
                    db.Database.Connection.Close();
                }

            }
            db.Dispose();
            dsRights.Tables[0].TableName = "AccessRights";
            dsRights.Tables[1].TableName = "SpecialRights";

            return dsRights;
        }
        public static UserAccessRights UserSpecialRights(string ControllerName)
        {
            string specialRights = "";
            DataSet dsUserRights = GetUserSpecialRightByUserIDAndController(CurrentUser.UserID, ControllerName);
            UserAccessRights userAccessRights = new UserAccessRights();
            userAccessRights.HasRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["HasRight"].ToString());
            userAccessRights.EditRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["EditRight"].ToString());
            userAccessRights.AddRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["AddRight"].ToString());
            userAccessRights.DeleteRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["DeleteRight"].ToString());
            userAccessRights.DispalyRight = Convert.ToInt32(dsUserRights.Tables["AccessRights"].Rows[0]["DispalyRight"].ToString());
            foreach (DataRow drSR in dsUserRights.Tables["SpecialRights"].Rows)
            {
                if (specialRights == "")
                {
                    specialRights = "{\"RightName\":\"" + drSR["RightName"].ToString() + "\",\"SpecialRight\":\"" + drSR["SpecialRight"].ToString() + "\"}";
                }
                else
                {
                    specialRights += ",{\"RightName\":\"" + drSR["RightName"].ToString() + "\",\"SpecialRight\":\"" + drSR["SpecialRight"].ToString() + "\"}";
                }
            }
            if (specialRights == "")
            {
                specialRights = "{}";
            }
            userAccessRights.SpecialRights = specialRights;

            return userAccessRights;
        }
    }
}

[Serializable]
public class CurrentUser
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public int IsAdmin { get; set; }
    public int UserType { get; set; }
    public string LoggedInUser { get; set; }
    public string Timezone { get; set; }
    public string Extension { get; set; }
    public string SMPTUserName { get; set; }
    public string SMTPPassword { get; set; }
    public long TenantID { get; set; }
}

[Serializable]
public class UserAccessRights
{
    public int HasRight { get; set; }
    public int EditRight { get; set; }
    public int AddRight { get; set; }
    public int DeleteRight { get; set; }
    public int DispalyRight { get; set; }
    public string SpecialRights { get; set; }
}