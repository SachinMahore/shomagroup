using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class TenantHomeController : Controller
    {
        // GET: Tenant/TenantHome
        public ActionResult Index()
        {
            return View();
        }
    }
}