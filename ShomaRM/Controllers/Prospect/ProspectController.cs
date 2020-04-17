using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using ShomaRM.ApiService;
using ShomaRM.Models;

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
                var currenttime = DateTime.Now.ToString("hh:mm:ss");
                var addtime = DateTime.Now.AddHours(1).ToString("HH:mm:ss");

                Service _Services = new Service();

                string body= "{'subject': '" + model.FirstName + " " + model.LastName + "','body': { 'contentType': 'HTML', 'content': 'Call link: https://aka.ms/mmkv1b Submit a question: https://aka.ms/ybuw2i' }, 'start': { 'dateTime': '" + Convert.ToDateTime(model.VisitDateTime).ToString("yyyy-MM-dd") + "T" + currenttime.Replace(".",":") + "','timeZone': 'Pacific Standard Time'  },  'end': { 'dateTime': '" + Convert.ToDateTime(model.VisitDateTime).ToString("yyyy-MM-dd") + "T" + addtime.Replace(".",":") + "', 'timeZone': 'Pacific Standard Time' }, 'location': {'displayName': '" + model.FirstName + " " + model.LastName + "'},'recurrence': {'pattern': {'type': 'relativeMonthly','interval': 1,'daysOfWeek': ['Tuesday'],'index': 'first'},'range': {'type': 'noEnd', 'startDate': '2017-08-29' }}}";
              
                var details= await _Services.CrmRequest(HttpMethod.Post, "https://graph.microsoft.com/v1.0/me/events", body);
                if (details.IsSuccessStatusCode == true)
                {
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