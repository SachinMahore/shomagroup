using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;

namespace ShomaRM.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "property";
            return View();
        }
        public ActionResult AddEdit()
        {
            ViewBag.ActiveMenu = "property";
            return View();
        }
        public ActionResult Details()
        {
            ViewBag.ActiveMenu = "property";
            var id = 0;
            var  Bedroom = 0;
            var MoveInDate = DateTime.Now.AddDays(30);
            var MaxRent = 10000;
            var model = new PropertyModel().GetPropertyDetails(id, MoveInDate, Bedroom, MaxRent);
            return View("..\\Property\\Details",model);
        }
        public ActionResult GetPropertyList(int City,string SearchText)
        {
            try
            {
                return Json(new { model = (new PropertyModel().GetPropertyList(City,SearchText)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult GetPropertyDetails(int id)
        {
            ViewBag.ActiveMenu = "property";
            var Bedroom = 0;
            var MoveInDate = DateTime.Now.AddDays(30);
            var MaxRent = 10000;
            if (Session["Bedroom"] != null)
            {
                 Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                 MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                 MaxRent = Convert.ToInt32(Session["MaxRent"].ToString());
            }
            else
            {
                Bedroom = 0;
                MoveInDate = DateTime.Now.AddDays(30);
                MaxRent = 10000;
            }
             var model = new PropertyModel().GetPropertyDetails(id, MoveInDate, Bedroom, MaxRent);

            if (Session["Bedroom"] != null)
            {
                model.Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                model.MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                model.MaxRent = Convert.ToDecimal(Session["MaxRent"].ToString());
                model.FromHome = 1;
                Session.Remove("Bedroom");
                Session.Remove("MoveInDate");
                Session.Remove("MaxRent");
               
            }
            else
            {
                model.Bedroom = 0;
                model.MoveInDate = DateTime.Now.AddDays(30);
                //model.MaxRent = 0;
                model.FromHome = 0;
            }
            return View("..\\Property\\Details", model);
           
        }
        public ActionResult GetPropertyUnitList(long PID, DateTime AvailableDate, decimal Current_Rent, int Bedroom)
        {
            try
            {
                return Json(new { model = (new PropertyModel().GetPropertyUnitList(PID,AvailableDate, Current_Rent, Bedroom)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetPropertyModelUnitList(string ModelName, DateTime AvailableDate, decimal Current_Rent, int Bedroom, int LeaseTermID, long ProspectId)
        {
            try
            {
                return Json(new { model = (new PropertyModel().GetPropertyModelUnitList(ModelName, AvailableDate, Current_Rent, Bedroom, LeaseTermID, ProspectId)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

      
        public ActionResult GetPropertyUnitDetails(long UID, int LeaseTermID)
        {
            try
            {
                return Json(new { model = (new PropertyModel().GetPropertyUnitDetails(UID,LeaseTermID)) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPropertyGallary(int PID)
        {
            try
            {
                return Json(new { model = (new PropertyModel().GetPropertyGallary(PID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetFloorList(int PID, DateTime AvailableDate, int Bedroom,decimal MaxRent)
        {
            try
            {
                return Json((new PropertyFloor()).GetFloorList(PID, AvailableDate, Bedroom, MaxRent), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPropertyFloorDetails(int FloorID, DateTime AvailableDate, int Bedroom, decimal MaxRent, int LeaseTermID, string ModelName, long ProspectId)
        {
            try
            {
                return Json(new { model = (new PropertyFloor().GetPropertyFloorDetails(FloorID, AvailableDate, Bedroom, MaxRent, LeaseTermID, ModelName, ProspectId)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SetSelectedUnitId(long UnitID, string MoveInDate)
        {
            try
            {
                Session["UnitID"] = UnitID;
                Session["MoveInDate"] = MoveInDate;
                return Json(new { msg =1 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SetSearchFromHome(int Bedroom,DateTime MoveInDate, decimal MaxRent,int LeaseTerm)
        {
            try
            {
                Session["Bedroom"] = Bedroom;
                Session["MoveInDate"] = MoveInDate;
                Session["MaxRent"] = MaxRent;
                Session["LeaseTerm"] = LeaseTerm;
                
                return Json(new { msg = 1 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetPropertyModelList(long PID, DateTime AvailableDate, decimal Current_Rent, int Bedroom, int SortOrder)
        {
            try
            {
                return Json(new { model = (new PropertyModel().GetPropertyModelList(PID, AvailableDate, Current_Rent, Bedroom, SortOrder)) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UploadPetPolicyFile()
        {
            try
            {
                HttpPostedFileBase fileBaseUpload1 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload1 = Request.Files[i];

                }

                return Json(new { model = new PropertyModel().UploadPetPolicyFile(fileBaseUpload1) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}