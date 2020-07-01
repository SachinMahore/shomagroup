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
            //aptlyHelper aptlyHelper = new aptlyHelper();
            //aptlyModel aptlyModel = new aptlyModel();
            //aptlyModel.name = "Sanctuary Doral -ganesh";
            //aptlyModel.Email = "ganesh@gmail.com";
            //aptlyModel.FirstName = "ganesh bokde";
            //aptlyModel.LastName = "bokde";
            //aptlyModel.Phone = "+14152344159";
            //aptlyModel.Building = "Sanctuary Doral";
            //aptlyModel.Unit = "104";
            //aptlyModel.UnitNumber = "104";
            //aptlyModel.FloorPlan = "A1";
            //aptlyModel.Stage = "Applicants";
            //aptlyModel.SubStage = "Applicants";
            //aptlyModel.LeaseTerm = "12";
            //aptlyModel.MoveInDate = "2020-05-25";
            //aptlyModel.QuoteExpires = "2020-05-01 24:00:00";
            //aptlyModel.Pets = "Dog(s)";

            //aptlyModel.PortalURL = "http://52.4.251.162:8086/Admin/ProspectVerification/EditProspect/1727";
            //aptlyModel.CreditPaid = "False";
            //aptlyModel.BackgroundCheckPaid = "true";
            //RelatedContacts _objrelatedContacts = new RelatedContacts();
            //_objrelatedContacts.FirstName = "Sanctuary Doral -ganesh";
            //_objrelatedContacts.LastName = "Sanctuary Doral -ganesh";
            //_objrelatedContacts.Email = "ganesh@gmail.com";
            //_objrelatedContacts.Phone = "+14152344159";
            //RelatedContacts _objrelatedContacts1 = new RelatedContacts();
            //_objrelatedContacts1.FirstName = "Sanctuary Doral -ganesh1";
            //_objrelatedContacts1.LastName = "Sanctuary Doral -ganesh1";
            //_objrelatedContacts1.Email = "ganesh1@gmail.com";
            //_objrelatedContacts1.Phone = "+919960239121";
            //aptlyModel.RelatedContacts = new List<RelatedContacts>();
            //aptlyModel.RelatedContacts.Add(_objrelatedContacts);
            //      aptlyModel.RelatedContacts.Add(_objrelatedContacts1);
            //var test = aptlyHelper.PostAptlyAsync(aptlyModel);

            //   CoreLogicHelper _corelogichelper = new CoreLogicHelper();
            //   var LeaseTermsModel = new LeaseTermsModel();
            //   LeaseTermsModel.MonthlyRent = "25.00";
            //   LeaseTermsModel.LeaseMonths = "12";
            //   LeaseTermsModel.SecurityDeposit = "25.00";
            //   var applicant=new  Applicant();
            //   applicant.CurrentRent = "50";
            //   applicant.CustomerID = "111223";
            //   applicant.ConsentObtained = "Yes";
            //   applicant.EmploymentGrossIncome = "25";
            //   applicant.ApplicantIdentifier = "abc123";
            //   applicant.ApplicantType = "Applicant";
            //   applicant.Birthdate = "1960-06-25";
            //   applicant.SocSecNumber = "880544745";
            //   applicant.FirstName = "Edwin";
            //   applicant.LastName = "Avila";
            //   applicant.Address1 = "85 Rebecca Ln";
            //   applicant.City = "Youngsville";
            //   applicant.State = "NC";
            //   applicant.PostalCode = "27596";
            //   applicant.UnparsedAddress = "85 Rebecca Ln";

            //   string strxml = _corelogichelper.PostCoreLogicData(LeaseTermsModel, applicant);
            //   var keyValues = new List<KeyValuePair<string, string>>();
            //   keyValues.Add(new KeyValuePair<string, string>("XMLPost", strxml));
            //var data=   _corelogichelper.PostFormUrlEncoded<List<XElement>>("https://vendors.residentscreening.net/b2b/demits.aspx", keyValues);



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