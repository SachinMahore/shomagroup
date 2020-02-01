using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers.CommunityClub
{
    public class CommunityClubController : Controller
    {
        // GET: Tenant/CommunityClub
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "communityclub";
            return View();
        }

        public ViewResult createCommunityClub()
        {
            ViewBag.ActiveMenu = "communityclub";
            return View("CommunityClubJoin");
        }
    }
}