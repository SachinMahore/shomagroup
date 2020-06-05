using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Controllers;

namespace ShomaRM.Areas.Admin.Controllers
{
 
    public class PropertyManagementController : Controller
    {
        // GET: Admin/PropertyManagement
     
        public ActionResult Index()
        {
            Session["UNOR"] = 10;
            ViewBag.ActiveMenu = "property";
            return View();
        }
        public ActionResult GetPropertyList()
        {
            try
            {
                return Json(new { model = (new PropertyManagementModel().GetPropertyList()) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SearchPropertyList(int City, string SearchText)
        {
            try
            {
                return Json(new { model = (new PropertyManagementModel().SearchPropertyList(City, SearchText)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AddProperty()
        {
            Session["UNOR"] = 10;
            ViewBag.ActiveMenu = "property";
            int id = 0;
            var model = new PropertyManagementModel().GetPropertyDet(id);
            return View("..\\PropertyManagement\\EditProperty", model);
        }
        public ActionResult EditProperty(int id)
        {
            ViewBag.ActiveMenu = "property";
            var model = new PropertyManagementModel().GetPropertyDet(id);
            return View("..\\PropertyManagement\\EditProperty", model);

        }
        public ActionResult GetPropertyUnitList(long PID)
        {
            try
            {
                return Json(new { model = (new PropertyManagementModel().GetPropertyUnitList(PID)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult BuildPaganationPUList(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            Session["UNOR"] = NOR;
            try
            {
                
                return Json(new { NOP = (new PropertyManagementModel()).BuildPaganationPUList(PID, PN, NOR, SortBy, OrderBy) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillPUSearchGrid(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            try
            {
                Session["UNOR"] = NOR;
                return Json((new PropertyManagementModel()).FillPUSearchGrid(PID, PN, NOR, SortBy, OrderBy), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PropUnitList()
        {
            return View();
        }
        public ActionResult EditPropUnit(int id)
        {
            Session["UNOR"] = 10;
            ViewBag.ActiveMenu = "property";
            var model = new PropertyUnits().GetPropertyUnitDetails(id);
            return View("..\\PropertyManagement\\_EditPropUnit", model);

        }
        public ActionResult EditPropModel(int id)
        {
            ViewBag.ActiveMenu = "Models";
            var model = new ModelsModel().GetModelsData(id);
            return View("..\\PropertyManagement\\_EditPropModel", model);

        }
        public ActionResult SaveUpdateProperty(PropertyManagementModel model)
        {

            try
            {
                string msg = new PropertyManagementModel().SaveUpdateProperty(model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdatePropertyUnit(PropertyUnits model)
        {

            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                long msg = new PropertyUnits().SaveUpdatePropertyUnit(fb, model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveUpdateFloor(PropertyFloor model)
        {
            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = new PropertyFloor().SaveUpdateFloor(fb, model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        //public ActionResult GetFloorList(int PID)
        //{
        //    try
        //    {
        //        return Json((new PropertyFloor()).GetFloorList(PID), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public ActionResult GetPropertyFloorDetails(int FloorID)
        {
            try
            {
                return Json(new { model = (new PropertyFloor().GetPropertyFloorDetails(FloorID)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult BuildPaganationPropList(PropertyManagementModel.PropertySearch model)
        {
            try
            {
                return Json(new { NOP = (new PropertyManagementModel()).BuildPaganationPropList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillPropertySearchGrid(PropertyManagementModel.PropertySearch model)
        {
            try
            {
                return Json((new PropertyManagementModel()).FillPropertySearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteModel(int ModelID)
        {
            try
            {
                return Json(new { model = new ModelsModel().DeleteModel(ModelID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult DeleteUnit(long UID)
        {
            try
            {
                return Json(new { model = new PropertyUnits().DeleteUnit(UID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult UpdateAvailDate(PropertyUnits model)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateAvailDate(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult UpdateMoveInDate(PropertyUnits model)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateMoveInDate(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult UpdateMoveOutDate(PropertyUnits model)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateMoveOutDate(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult UpdateRent(PropertyUnits model)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateRent(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult UpdateUnitNotes(PropertyUnits model)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateUnitNotes(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult PropertyFileUpload(PropertyManagementModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload1 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload1 = Request.Files[i];

                }

                return Json(new { model = new PropertyManagementModel().PropertyFileUpload(fileBaseUpload1, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetUnitLeasePrice(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            try
            {
                return Json((new PropertyManagementModel()).GetUnitLeasePrice(PID, PN, NOR, SortBy, OrderBy), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult BuildPaganationUnitLeasePrice(long PID, int PN, int NOR, string SortBy, string OrderBy)
        {
            try
            {
                return Json(new { NOP = (new PropertyManagementModel()).BuildPaganationUnitLeasePrice(PID, PN, NOR, SortBy, OrderBy) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateUnitLeasePrice(long ULPID, decimal Price)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateUnitLeasePrice(ULPID, Price) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateUnitAvailability(PropertyUnits model)
        {
            try
            {
                return Json(new { model = new PropertyUnits().UpdateUnitAvailability(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}