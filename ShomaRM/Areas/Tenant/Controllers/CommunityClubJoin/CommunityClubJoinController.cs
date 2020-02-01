using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers.CommunityClubJoin
{
    public class CommunityClubJoinController : Controller
    {
        // GET: Tenant/CommunityClubJoin
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "communityclub";
            return View();
        }
    }
}