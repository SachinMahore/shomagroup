using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShomaRM.ApiService;
using ShomaRM.Areas.Admin.Models;

namespace ShomaRM.Areas.Admin.Controllers
{
    public class ProspectManagementController : Controller
    {
        // GET: Admin/ProspectManagement
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "prospect";
            return View();
        }
        public ActionResult AddEdit(int id)
        {
            ViewBag.ActiveMenu = "prospect";
            if (id !=0)
            {
                var model = new ProspectManagementModel().GetProspectDetails(id);
                return View("..\\ProspectManagement\\AddEdit", model);
            }
            else
            {
                return RedirectToAction("../ProspectManagement/Index");
                //return View("..\\ProspectManagement\\Index");
            }
        }
        public ActionResult GetProspectList(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().GetProspectList(FromDate, ToDate) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> SaveProspectFormAsync(ProspectManagementModel model)
        {
            var ProspectModel = new ProspectManagementModel().GetProspectDetails(Convert.ToInt32(model.PID));
            var agentdetail = new UsersModel().GetUserInfo(Convert.ToInt32(model.AssignAgentID));
            try
            {

                var currenttime = DateTime.UtcNow.ToString("hh:mm:ss");
                var addtime = DateTime.UtcNow.AddHours(1).ToString("HH:mm:ss");

                Service _Services = new Service();

                string body = "{'subject': '" + ProspectModel.FirstName + " " + ProspectModel.LastName + "','body': { 'contentType': 'HTML', 'content': 'Call link: https://aka.ms/mmkv1b Submit a question: https://aka.ms/ybuw2i' }, 'start': { 'dateTime': '" + Convert.ToDateTime(model.VisitDateTime).ToString("yyyy-MM-dd") + "T" + currenttime.Replace(".", ":") + "','timeZone': 'Pacific Standard Time'  },  'end': { 'dateTime': '" + Convert.ToDateTime(model.VisitDateTime).ToString("yyyy-MM-dd") + "T" + addtime.Replace(".", ":") + "', 'timeZone': 'Pacific Standard Time' }, 'location': {'displayName': '" + ProspectModel.Message + " '},'attendees': [ { 'emailAddress': {'address':'" + agentdetail.Email + "','name': '" + ProspectModel.FirstName + " " + ProspectModel.LastName + "'},'type': 'required'}]}";

                var details = await _Services.CrmRequest(new HttpMethod("PATCH"), "https://graph.microsoft.com/v1.0/me/calendars/" + ConfigurationManager.AppSettings["CalendarId"] + "/events/" + ProspectModel.OutlookID, body);
                if (details.IsSuccessStatusCode == true)
                {
                    string contactsJson = await details.Content.ReadAsStringAsync();
                    var odataresponse = JsonConvert.DeserializeObject<RootObject>(contactsJson);
                    model.OutlookID = odataresponse.id.ToString();
                    return Json(new { model = new ProspectManagementModel().SaveProspectForm(model) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { msg = "Outlook Event is not update.." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProspectDetails(int id)
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().GetProspectDetails(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult SaveUpdateVisit(VisitModel model)
        {
            try
            {
                return Json(new { Msg = (new VisitModel().SaveUpdateVisit(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetVisitDetails(int id)
        {
            try
            {
                return Json(new { model = new VisitModel().GetVisitDetails(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult GetVisitList(long ProspectID)
        {
            try
            {
                return Json(new { model = new VisitModel().GetVisitList(ProspectID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetDdlMarketSourceList()
        {
            try
            {
                return Json(new { model = new ProspectManagementModel().GetDdlMarketSourceList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult BuildPaganationProspectList(ProspectSearchModel model)
        {
            try
            {
                return Json(new { NOP = (new ProspectManagementModel()).BuildPaganationProspectList(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillProspectSearchGrid(ProspectSearchModel model)
        {
            try
            {
                return Json((new ProspectManagementModel()).FillProspectSearchGrid(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}