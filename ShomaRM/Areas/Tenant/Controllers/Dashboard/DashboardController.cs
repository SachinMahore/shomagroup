using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Areas.Admin.Models;
using System.Net.Http;
using ShomaRM.ApiService;
using Newtonsoft.Json;
using ShomaRM.Areas.Tenant.Models.Calender;

namespace ShomaRM.Areas.Tenant.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Tenant/Dashboard
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "dashboard";
            
            return View();
        }

        public async System.Threading.Tasks.Task<JsonResult> GetCalenderEventAsync()
        {
            List<EventModel> listEvent = new List<EventModel>();
            Service _Services = new Service();
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            List<CalenderEvents> Items = new List<CalenderEvents>();
            var details = await _Services.CrmRequest(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me/calendarview?startdatetime="+ startDate.ToString("yyyy-MM-dd")+ "&enddatetime="+ endDate.ToString("yyyy-MM-dd") + "", null);
            if (details.IsSuccessStatusCode == true)
            {
                try
                {
                    string contactsJson = await details.Content.ReadAsStringAsync();
                    var odataresponse = JsonConvert.DeserializeObject<RootObject>(contactsJson);
                    Items = JsonConvert.DeserializeObject<List<CalenderEvents>>(JsonConvert.SerializeObject(odataresponse.Value));

                    foreach (var item in Items)
                    {
                        listEvent.Add(new EventModel()
                        {
                            EventID = 0,
                            EventDate = item.start.dateTime,
                            EventName = item.subject,
                            EventDateString = item.start.dateTime.ToString("dd"),
                            Type = 1
                        });
                    }
                }
                catch(Exception ex)
                {
                    
                }
                return Json(new { model = listEvent }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SetSessionMakePayments(int StepId, int PayStepId)
        {
            try
            {
                Session["StepIdMakePayment1"] = StepId.ToString();
                Session["PayStepIdMakePayment"] = PayStepId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SetSessionSetRecurringPayments(int StepId, int PayStepId)
        {
            try
            {
                Session["StepIdMakePayment2"] = StepId.ToString();
                Session["PayStepIdRecurringPayment"] = PayStepId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SetSessionSetReserveAmenities(int StepId, int PayStepId)
        {
            try
            {
                Session["StepReserveAmenities"] = StepId.ToString();
                Session["PayStepIdReserveAmenities"] = PayStepId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SetSessionSetRegisterGuest(int StepId)
        {
            try
            {
                Session["StepRegisterGuest"] = StepId.ToString();
              
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SetSessionSetSubmitServiceRequest(int StepId, int ServiceRequestId)
        {
            try
            {
                Session["StepIdServiceRequest2"] = StepId.ToString();
                Session["ServiceStepIdSubmitServiceRequest"] = ServiceRequestId.ToString();
                long TenantId = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.TenantID;
                return Json(new { model = TenantId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEventList()
        {
            try
            {
                return Json(new { model = new EventModel().GetEventList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetCalEventList(DateTime dt)
        {
            try
            {
                return Json(new { model = new EventModel().GetCalEventList(dt) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDateEventList()
        {
            try
            {
                return Json(new { model = new EventModel().GetDateEventList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetNewDateEventList()
        {
            try
            {
                return Json(new { model = new EventModel().GetNewDateEventList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetRatings(long UserId)
        {
            try
            {
                return Json(new { model = new RatingModel().GetRatings(UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveRatings(RatingModel model)
        {
            try
            {
                return Json(new { model = new RatingModel().SaveRatings(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEventDetail(long EventID)
        {
            try
            {
                return Json(new { model = new EventModel().GetEventDetail(EventID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveTenantEventJoin (TenantEventJoinModel model)
        {
            try
            {
                return Json(new { model = new TenantEventJoinModel().SaveTenantEventJoin(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEventJoiningStatus(TenantEventJoinModel model)
        {
            try
            {
                return Json(new { model = new TenantEventJoinModel().AlreadyJoinTenantEvent(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveTenantEventUnJoin(TenantEventJoinModel model)
        {
            try
            {
                return Json(new { model = new TenantEventJoinModel().SaveTenantEventUnJoin(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}