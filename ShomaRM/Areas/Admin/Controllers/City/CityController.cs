using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Data;

namespace ShomaRM.Areas.Admin.Controllers.City
{
    public class CityController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "admin";
            UsersModel model = new UsersModel();
            if (model.HasRight == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult FillStateDropDownList()
        {
            try
            {
                return Json((new CityModel()).FillStateDropDownList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCityList(CityList model)
        {
            try
            {
                return Json((new CityModel()).GetCityList(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationCityList(CityList model)
        {
            try
            {
                return Json(new { NOP = (new CityModel()).BuildPaganationCityList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCityListbyState(int StateID)
        {
            try
            {
                return Json((new CityModel()).GetCityListbyState(StateID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCityInfo(int CityID = 0)
        {
            try
            {
                return Json((new CityModel()).GetCityInfo(CityID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateCity(CityModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateCity(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Aug 17, 2019
        public ActionResult FillStateDropDownListByCountryID(long CID)
        {
            try
            {
                return Json((new CityModel()).FillStateDropDownListByCountryID(CID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillCountryDropDownList()
        {
            try
            {
                return Json((new CityModel()).FillCountryDropDownList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Aug 17, 2019

        public ActionResult DeleteCity(long CityID)
        {
            try
            {
                return Json(new { model = new CityModel().DeleteCity(CityID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}