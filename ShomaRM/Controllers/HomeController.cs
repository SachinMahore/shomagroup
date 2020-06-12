using LoggerEngine;
using ShomaRM.Models;
using ShomaRM.Models.Bluemoon;
using ShomaRM.Models.TwilioApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using Twilio;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;

namespace ShomaRM.Controllers
{
    public class HomeController : TwilioController
    {
        
        public ActionResult Index()
        {
            //aptlyHelper aptlyHelper = new aptlyHelper();
            //aptlyModel aptlyModel = new aptlyModel();
            //aptlyModel.FirstName = "ganesh bokde";
            //aptlyModel.LastName = "bokde";

            //aptlyModel.name = "Sanctuary Doral -ganesh";
            //aptlyModel.Email = "ganesh@gmail.com";
            //aptlyModel.FirstName = "ganesh bokde";
            //aptlyModel.Building = "Sanctuary Doral";
            //aptlyModel.Unit = "104";

            //aptlyModel.Phone = "+14152344159";
            //aptlyModel.Stage = "Registration";
            //aptlyModel.LeaseTerm = "12";
            //aptlyModel.MoveInDate = "2020-05-25";
            //aptlyModel.DateSigned = "2020-05-02";
            //aptlyModel.CreditPaid = "true";
            //aptlyModel.BackgroundCheckPaid = "true";
            //aptlyModel.QuoteCreated = "2020-05-0";
            //var test= aptlyHelper.PostAptlyAsync(aptlyModel);

            if (Session["DelDatAll"] != null)
            {
                if (Session["DelDatAll"].ToString() == "Del")
                {
                    ViewBag.DelAData = Session["DelDatAll"].ToString();
                }
                else
                {
                    ViewBag.DelAData = "";
                }
            }
            else
            {
                ViewBag.DelAData = "";
            }
            Session["DelDatAll"] = null;
            var model = new HomeModel().GetLeaseTerms();
            ////To get Server url            
            //var serverURL = Request.Url;
            ////Change key in WebConfig file
            //Configuration AppConfigSettings = WebConfigurationManager.OpenWebConfiguration("~");
            //AppConfigSettings.AppSettings.Settings["ServerURL"].Value = serverURL.ToString();
            //AppConfigSettings.Save();
            ViewBag.ActiveMenu = "home";
            return View(model);
        }


        public async System.Threading.Tasks.Task<ActionResult> About()   

        {
            ViewBag.Message = "Your application description page.";

            //TwilioService twilioService = new TwilioService();
            //twilioService.SMS("+9547908408","Message Text");

            //twilioService.Call("+9547908408");

            return View();



            //var data = await testAsync();
            //if (data != null)
            //{


            //}

            //return File(data.leasePdfWithEsignatures[0], "application/pdf", $"LeaseDocument_{0}.pdf");
        }

        //public async System.Threading.Tasks.Task<LeaseResponseModel> testAsync()
        //{

        //    var test = new BluemoonService();
        //    LeaseRequestModel leaseRequestModel = new LeaseRequestModel();
        //    leaseRequestModel.UNIT_NUMBER = "Apurva";
        //    LeaseResponseModel authenticateData = await test.CreateSession();
        //    LeaseResponseModel leaseCreateResponse = await test.CreateLease(leaseRequestModel: leaseRequestModel, PropertyId: "112154", sessionId: authenticateData.SessionId);
           
        //    //LeaseResponseModel leaseEditResponse = await test.EditLease(leaseRequestModel: leaseRequestModel, leaseId: leaseCreateResponse.LeaseId, sessionId: authenticateData.SessionId);
        //    //LeaseResponseModel leasePdfResponse = await test.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: leaseCreateResponse.LeaseId);
        //    //await test.CloseSession(sessionId: authenticateData.SessionId);
        
        //    //return leaseResponseModel;
        //    //LeaseResponseModel leasePdfResponse = await test.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: leaseCreateResponse.LeaseId);
        //    await test.CloseSession(sessionId: authenticateData.SessionId);
        //    return leaseResponseModel;


        //}

        public ActionResult Contact()
        {
            ViewBag.ActiveMenu = "contact";
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult FAQ()
        {
            ViewBag.Message = "Your FAQ's page.";

            return View();
        }
        public ActionResult Terms()
        {
            ViewBag.Message = "Your Terms page.";

            return View();
        }
        public ActionResult ScheduleEmail()
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().ScheduleEmail() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        #region Create Calender Event
        public ActionResult CreateCalenderEvent()
        {
            return Json("");
        }
        #endregion
    }
}