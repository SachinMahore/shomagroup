﻿using ShomaRM.Areas.Tenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class GuestManagementController : Controller
    {
        // GET: Admin/GuestManagement
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetGuestRegistrationList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new GuestRegistrationModel().GetGuestRegistrationList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteGuestRegistrationList(long GuestID)
        {
            try
            {
                return Json(new { model = new GuestRegistrationModel().DeleteGuestRegistrationList(GuestID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetGuestList(GuestRegistrationModel model)
        {
            try
            {
                return Json(new { model = new GuestRegistrationModel().GetGuestList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}