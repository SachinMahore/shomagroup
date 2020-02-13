using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class UsersController : Controller
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
        public ActionResult BuildPaganationUserList(UsersModel.UserSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new UsersModel()).BuildPaganationUserList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillUserSearchGrid(UsersModel.UserSearchModel model)
        {
            try
            {
                return Json((new UsersModel()).FillUserSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetUserListByType(int UserType)
        {
            try
            {
                return Json((new UsersModel()).GetUserListByType(UserType), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult New()
        {
            ViewBag.ActiveMenu = "admin";
            ViewBag.UserID = 0;
            //var model = new UsersModel().GetUserInfo(0);
            return View("AddEdit");
        }
        public ActionResult Edit(Int32 ID)
        {
            ViewBag.ActiveMenu = "admin";
            ViewBag.UserID = ID;
            //var model = new UsersModel().GetUserInfo(ID);
            return View("AddEdit");
        }
        public ActionResult GetUserInfo(int UserID = 0)
        {
            try
            {
                return Json((new UsersModel()).GetUserInfo(UserID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateUser(UsersModel model)
        {
            try
            {
                return Json(new { result = 1, ID = model.SaveUpdateUser(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdatePasswordUser(UsersModel model)
        {
            try
            {
                return Json(new { model = new UsersModel().UpdatePasswordUser(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}