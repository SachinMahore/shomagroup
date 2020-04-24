using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;

namespace ShomaRM.Controllers
{
    public class ApplicationStatusController : Controller
    {
        // GET: ApplicationStatus
        public ActionResult Index(string id)
        {
            string status = "in process";
            try { status = new EncryptDecrypt().DecryptText(id).ToLower();
                if(status== "approved")
                {
                    status = "Your application is approve. Please check you mail.";
                }
                else
                {
                    status = "Your application is in-process. We will contact you shortly.";
                }
            }
            catch { }
            ViewBag.Status = status;
            return View();
        }
    }
}