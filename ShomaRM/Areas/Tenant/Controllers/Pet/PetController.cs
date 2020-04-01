using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class PetController : Controller
    {
        // GET: Tenant/Pet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveUpdatePet(PetModel model)
        {
            try
            {
                string msg = new PetModel().SaveUpdatePet(model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPetList(long TenantID)
        {
            try
            {
                return Json(new { model = new PetModel().GetPetList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetPetDetails(int id)
        {

            try
            {
                return Json(new { model = new PetModel().GetPetDetails(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteTenantPet(long PetID)
        {
            try
            {
                return Json(new { model = new PetModel().DeleteTenantPet(PetID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProfilePetList(long TenantID)
        {
            try
            {
                return Json(new { model = new PetModel().GetProfilePetList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SaveUpdateTenanatPet(PetModel model, long UserId)
        {
            try
            {
                return Json(new { Msg = (new PetModel().SaveUpdateTenanatPet(model, UserId)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckPetRegistration(long UserId)
        {
            try
            {
                return Json(new { model = new PetModel().CheckPetRegistration(UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}