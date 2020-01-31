﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;

namespace ShomaRM.Controllers
{
    public class PayLinkController : Controller
    {
        // GET: PayLink
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LeaseNow()
        {
            ViewBag.UID = "0";
            if(ShomaGroupWebSession.CurrentUser!=null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }
          
            return View();
        }
        
        public ActionResult GetProspectData(long UID)
        {
            try
            {
                return Json(new { model = (new OnlineProspectModule().GetProspectData(UID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PayAmenityCharges(int ARID)
        {
            try
            {
                Session["ARID"] = ARID;
               
                return RedirectToAction("../Account/Login");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}