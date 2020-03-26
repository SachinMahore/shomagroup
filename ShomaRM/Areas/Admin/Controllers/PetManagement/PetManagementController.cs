using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class PetManagementController : Controller
    {
        // GET: Admin/PetPlace
        public ActionResult Index(int Id)
        {
            ViewBag.ActiveMenu = "admin";
            var model = new PetManagementModel().GetPetPlaceData(Id);
            return View("..\\PetManagement\\Index", model);
        }
        public ActionResult GetPetPlaceList()
        {
            try
            {
                return Json((new PetManagementModel()).GetPetPlaceList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPetPlaceInfo(int PetPlaceID = 0)
        {
            try
            {
                return Json((new PetManagementModel()).GetPetPlaceInfo(PetPlaceID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdatePetPlace(PetManagementModel model)
        {
            try
            {
                return Json(new { result = 1, PetPlaceID = model.SaveUpdatePetPlace(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPetPlaceSearchList(string SearchText)
        {
            try
            {
                return Json((new PetManagementModel()).GetPetPlaceSearchList(SearchText), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //For TenantPetPlaceModel

        public ActionResult GetTenantPetPlaceList(long TenantId)
        {
            try
            {
                return Json((new TenantPetPlaceModel()).GetTenantPetPlaceList(TenantId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateTenantPetPlace(TenantPetPlaceModel model)
        {
            try
            {
                string[] petDetails = model.SaveUpdateTenantPetPlace(model).Split('|');
                return Json(new { result = 1, numOfPet= petDetails[0], totalPetPlaceAmt = petDetails[1], petDNAFees= petDetails[2] }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillPetPlaceSearchGrid(PetPlaceListModel model)
        {
            try
            {
                return Json((new PetManagementModel()).FillPetPlaceSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationPetPlaceList(PetPlaceListModel model)
        {
            try
            {
                return Json(new { NOP = (new PetManagementModel()).BuildPaganationPetPlaceList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeletePetPlacesDetails(long PetPlaceID = 0)
        {
            try
            {
                (new PetManagementModel()).DeletePetPlacesDetails(PetPlaceID);
                return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}