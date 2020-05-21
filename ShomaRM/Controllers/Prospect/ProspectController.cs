using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShomaRM.ApiService;
using ShomaRM.Models;
using System.IO;

namespace ShomaRM.Controllers
{
    public class ProspectController : Controller
    {
        // GET: Tenant
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "prospect";
            return View();
        }

        public async System.Threading.Tasks.Task<ActionResult> SaveProspectFormAsync(ProspectModel model)
        {
            try
            {
                var GetProspectusList = new ProspectModel().GetProspectusList().Where(a=>a.EmailId!= model.EmailId);
                var currenttime = DateTime.UtcNow.ToString("hh:mm:ss");
                var addtime = DateTime.UtcNow.AddHours(1).ToString("HH:mm:ss");

                Service _Services = new Service();
                string Attendees = "'attendees': [";
                //foreach(var item in GetProspectusList)
                //{
                //    Attendees = Attendees + "{ 'emailAddress': {'address':'" + item.EmailId + "','name': '" + item.FirstName + " " + item.LastName + "'},'type': 'required'},";
                //}
                
                Attendees = Attendees + "{ 'emailAddress': {'address':'" + model.EmailId + "','name': '" + model.FirstName + " " + model.LastName + "'},'type': 'required'}";
                Attendees = Attendees + "]";


                string body= "{'subject': '" + model.FirstName + " " + model.LastName + "','body': { 'contentType': 'HTML', 'content': 'Call link: https://aka.ms/mmkv1b Submit a question: https://aka.ms/ybuw2i' }, 'start': { 'dateTime': '" + Convert.ToDateTime(model.VisitDateTime).ToString("yyyy-MM-dd") + "T" + currenttime.Replace(".",":") + "','timeZone': 'Pacific Standard Time'  },  'end': { 'dateTime': '" + Convert.ToDateTime(model.VisitDateTime).ToString("yyyy-MM-dd") + "T" + addtime.Replace(".",":") + "', 'timeZone': 'Pacific Standard Time' }, 'location': {'displayName': '" + model.Message + "'},"+ Attendees + "}";
              
                var details= await _Services.CrmRequest(HttpMethod.Post, "https://graph.microsoft.com/v1.0/me/calendars/" + ConfigurationManager.AppSettings["CalendarId"] +"/events", body);
                if (details.IsSuccessStatusCode == true)
                {
                    string contactsJson = await details.Content.ReadAsStringAsync();
                    var odataresponse = JsonConvert.DeserializeObject<RootObject>(contactsJson);
                    model.OutlookID = odataresponse.id.ToString();
                    return Json(new { model = new ProspectModel().SaveProspectForm(model) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { model = model }, JsonRequestBehavior.AllowGet);
                }
               
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}