using LoggerEngine;
using Microsoft.Graph;
using ShomaRM.Models;
using ShomaRM.Models.Bluemoon;
using ShomaRM.Models.Corelogic;
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
using System.Xml.Linq;
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
        public async System.Threading.Tasks.Task<ActionResult> CreateESignPolicyAgreement(int uid, bool AppAgree)
        {
            var bmservice = new BluemoonService();
            LeaseResponseModel authenticateData = await bmservice.CreateSession();
            LeaseRequestModel leaseRequestModel = new LeaseRequestModel();

            leaseRequestModel.LEASE_BEGIN_DATE = "";
            leaseRequestModel.LEASE_END_DATE = "";
            leaseRequestModel.RESIDENT_1 = "Lalut";

            LeaseResponseModel leaseCreateResponse = await bmservice.CreateLeaseCustomForm(leaseRequestModel: leaseRequestModel, PropertyId: "112154", sessionId: authenticateData.SessionId);
            //var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();

            //onlineProspectData.EnvelopeID = leaseCreateResponse.LeaseId;
            //db.SaveChanges();

            var leaseid = leaseCreateResponse.LeaseId;
            List<EsignatureParty> esignatureParties = new List<EsignatureParty>();
            esignatureParties.Add(new EsignatureParty()
            {
                Email = "info@sanctuarydoral.com",
                IsOwner = true,
                Name = "Sanctuary Doral",
                Phone = "786-437-8658"
            });

            //for true =SHOMA_APPAGREE  AND FALSE SHOMA_RULESPOL
            LeaseResponseModel EsignatureResponse = await bmservice.RequestCustomEsignature(leaseId: leaseid, sessionId: authenticateData.SessionId, esignatureParties: esignatureParties, AppAgree: true);

            // this will not give pdf with signature right away because this will need to be called after residents will sign the esignature. 
            // so please call it on any download lease document button 
            // Note. please save the esignature id for downloading document 

            LeaseResponseModel leaseDocumentWithEsignature = await bmservice.GetLeaseDocumentWithEsignature(SessionId: authenticateData.SessionId, EsignatureId: EsignatureResponse.EsignatureId);
            
            return View();
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


        //Sachin M 23 June

        public ActionResult CreateUsaePayAccount(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().CreateUsaePayAccount(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddCustPaymentMethod(ApplyNowModel model)
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().AddCustPaymentMethod(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddCustPaymentMethodACH(ApplyNowModel mm)
        {
            UsaePayWSDLModel model = new UsaePayWSDLModel();
            model.AddCustPaymentMethodACH(mm);
            return View();
        }
        public ActionResult PayUsingCustomerNum(string CustID, string PMID, decimal Amount, string Desc)
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().PayUsingCustomerNum(CustID,PMID,Amount,Desc)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PayFromUsae()
        {
            
            return View();
        }
        public ActionResult TransReport()
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().TransReport()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetCustPaymentMethod(string CustID, string PMID)
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().GetCustPaymentMethod(CustID, PMID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeletePaymentMethod(string CustID, string PMID)
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().DeletePaymentMethod(CustID, PMID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RefundTrans( string RefNum, decimal Amount)
        {
            try
            {
                return Json(new { msg = (new UsaePayWSDLModel().RefundTrans(RefNum, Amount)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}