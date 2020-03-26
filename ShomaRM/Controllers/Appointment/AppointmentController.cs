using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;
using ShomaRM.Data;

namespace ShomaRM.Controllers.Appointment
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index(string id)
        {
            string appid = new EncryptDecrypt().DecryptText(id);
            long PID = Convert.ToInt64(appid);
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var appointment = db.tbl_Prospect.Where(p => p.PID == PID).FirstOrDefault();
            if (appointment != null)
            {
                string apptDate= appointment.VisitDateTime.HasValue ? appointment.VisitDateTime.Value.ToString("MM/dd/yyyy") : "";

                if ((appointment.AppointmentStatus??0) != 3)
                {
                    appointment.AppointmentStatus = 3;
                    db.SaveChanges();
                    ViewBag.ApptMsg = "Your appointment " + (apptDate != "" ? "on " + apptDate : "") + " is cancelled.";
                }
                else
                {
                    ViewBag.ApptMsg = "Your appointment " + (apptDate != "" ? "on " + apptDate : "") + " is already cancelled.";
                }
            }

            return View();
        }
    }
}