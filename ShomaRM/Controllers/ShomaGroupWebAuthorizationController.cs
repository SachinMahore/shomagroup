using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Data;
using ShomaRM.Models;

namespace ShomaRM.Controllers
{
    public class ShomaGroupWebAuthorizationController : AuthorizeAttribute
    {
        public string LoginPage { get; set; }
        public string AccessDeniedPage { get; set; }
        //public Enums.UserRole UserRole { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    //Ajax request doesn't return to login page, it just returns 403 error.
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new HttpUnauthorizedResult();
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/Login");
                    //filterContext.HttpContext.Response.Redirect("~/Home");
                }
            }
            else
            {
                ShomaRMEntities db = new ShomaRMEntities();
                var user = db.tbl_Login.Where(p => p.Username == filterContext.HttpContext.User.Identity.Name).FirstOrDefault();

                ShomaGroupWebSession _WebSession = new ShomaGroupWebSession();
                var currentUser = new CurrentUser();
                currentUser.UserID = user.UserID;
                currentUser.Username = user.Username;
                currentUser.FullName = user.FirstName + " " + user.LastName;
                currentUser.EmailAddress = user.Email;
                currentUser.IsAdmin = (user.IsSuperUser.HasValue ? user.IsSuperUser.Value : 0);
                currentUser.EmailAddress = user.Email;

                currentUser.LoggedInUser = user.FirstName;

                HttpContext.Current.Session["CurrentUser"] = currentUser;

                db.Dispose();
                _WebSession.SetWebSession(currentUser);
            }

            base.OnAuthorization(filterContext);
        }
    }
}