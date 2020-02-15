﻿using ShomaRM.Models;
using ShomaRM.Models.Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ShomaRM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["DelDatAll"] !=null)
            {
                if (Session["DelDatAll"].ToString() == "Del")
                {
                    ViewBag.DelAData = Session["DelDatAll"].ToString();
                }
                else
                {
                    ViewBag.DelAData = "";
                }
            }
            else
            {
                ViewBag.DelAData = "";
            }
            Session["DelDatAll"] = null;

            
            ViewBag.ActiveMenu = "home";
            return View();
        }

        public ActionResult About()
        
        
        {
            ViewBag.Message = "Your application description page.";
           
            var data = testAsync();
            if (data != null)
            {

            }

            return View();
        }

        public async System.Threading.Tasks.Task<bool> testAsync()
        {
            
            var test = new BluemoonService();
            LeaseRequestModel leaseRequestModel = new LeaseRequestModel();
            leaseRequestModel.UNIT_NUMBER = "Apurva";
            string leaseId = await test.CreateLease(leaseRequestModel: leaseRequestModel,PropertyId : "112154");
            Session["LeaseId"] = leaseId;
            return true;
        }

        public ActionResult Contact()
        {
            ViewBag.ActiveMenu = "contact";
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FAQ()
        {
            ViewBag.Message = "Your FAQ's page.";

            return View();
        }
        public ActionResult Terms()
        {
            ViewBag.Message = "Your Terms page.";

            return View();
        }
        public ActionResult ScheduleEmail()
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().ScheduleEmail() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}