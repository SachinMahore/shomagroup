using ShomaRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers
{
    public class CheckListController : Controller
    {
        // GET: CheckList
        public ActionResult Index()
        {
            ViewBag.UID = "0";
            if (ShomaGroupWebSession.CurrentUser != null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }

            return View();
        }
        public ActionResult SaveMoveInCheckList(CheckListModel model)
        {
            try
            {
                return Json(new { msg = (new CheckListModel().SaveMoveInCheckList(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
    
}