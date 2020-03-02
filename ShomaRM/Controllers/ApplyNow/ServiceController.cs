using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers.ApplyNow
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index(int EID, int Status)
        {
            ViewBag.UID = "0";
            ViewBag.Status = Status;
            ViewBag.EID = EID;
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var GetServiceData = db.tbl_Estimate.Where(p => p.EID == EID).FirstOrDefault();
            if(GetServiceData!=null)
            {
                if (GetServiceData.Status == "0")
                {
                    GetServiceData.Status = Status.ToString();
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Status = 3;
                }
            }
            return View();
        }
    }
}