using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Razor;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using ShomaRM.Models;
using ShomaRM.Areas.Admin.Models;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using ShomaRM.Data;
using System.Linq;

namespace ShomaRM.Controllers
{
    public class EmbeddedSigningController : Controller
    {
        // GET: EmbeddedSigning
        public ActionResult Index()
        {
            // var ur = new EmbeddedSigning().OnPostAsync();
            return View();
        }
        //string ApiLoginID = ConfigurationManager.AppSettings["ApiLoginID"];
         string accessToken = ConfigurationManager.AppSettings["DocuApiID"];
        private const string accountId = "a84fdfd7-3785-4fc2-b53d-7b21d5ce2535";
        private const string signerName = "AMIT PAR";
        private const string signerEmail = "amit.partakke@gmail.com";

        private const string signerClientId = "1000";

        private const string basePath = "https://demo.docusign.net/restapi";

        public string envid="";
        //string returnUrl = "http://52.4.251.162:8086/ApplyNow/Index/45";
        public async Task<ActionResult> OnPostAsync(string TenantID)

        {
            ShomaRMEntities db = new ShomaRMEntities();
            long tid = Convert.ToInt64(TenantID);
            var tenantdata = db.tbl_TenantOnline.Where(p => p.ProspectID == tid).FirstOrDefault();
            Session["StepNo"] = 15;
            string docName = new ProspectVerificationModel().PDFBuilder(TenantID);
           
            Document document = new Document

            {
                DocumentBase64 = Convert.ToBase64String(ReadContent(docName)),

                Name = "LeaseAgreement-" + TenantID,
                FileExtension = "pdf",
                DocumentId = "1"

            };

            Document[] documents = new Document[] { document };

            

            // Create the signer recipient object 

            Signer signer = new Signer

            {
                Email = tenantdata.Email,
                Name = tenantdata.FirstName+" "+tenantdata.LastName,
                ClientUserId = TenantID,

                RecipientId = "1",
                RoutingOrder = "1"

            };



            // Create the sign here tab (signing field on the document)

            SignHere signHereTab = new SignHere

            {
                DocumentId = "1",
                PageNumber = "3",
                RecipientId = "1",

                TabLabel = "Sign Here Tab",
                XPosition = "160",
                YPosition = "590"

            };

            SignHere[] signHereTabs = new SignHere[] { signHereTab };



            // Add the sign here tab array to the signer object.

            signer.Tabs = new Tabs { SignHereTabs = new List<SignHere>(signHereTabs) };

            // Create array of signer objects

            Signer[] signers = new Signer[] { signer };

            // Create recipients object

            Recipients recipients = new Recipients { Signers = new List<Signer>(signers) };

            // Bring the objects together in the EnvelopeDefinition

            EnvelopeDefinition envelopeDefinition = new EnvelopeDefinition

            {
                EmailSubject = "Please sign the document",

                Documents = new List<Document>(documents),

                Recipients = recipients,

                Status = "sent"

            };
            // 2. Use the SDK to create and send the envelope

            ApiClient apiClient = new ApiClient(basePath);

            apiClient.Configuration.AddDefaultHeader("Authorization", "Bearer " + accessToken);

            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient.Configuration);

            EnvelopeSummary results = envelopesApi.CreateEnvelope(accountId, envelopeDefinition);



            // 3. Create Envelope Recipient View request obj

            string envelopeId = results.EnvelopeId;
         
            string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            RecipientViewRequest viewOptions = new RecipientViewRequest

            {
               // ReturnUrl = "http://localhost:53680/ApplyNow/Index/" + uid,
               ReturnUrl = "http://52.4.251.162:8086/ApplyNow/Index/" + uid,
                ClientUserId = TenantID,

                AuthenticationMethod = "none",

                UserName = tenantdata.FirstName + " " + tenantdata.LastName,
                Email = tenantdata.Email,

            };



            // 4. Use the SDK to obtain a Recipient View URL

            ViewUrl viewUrl = envelopesApi.CreateRecipientView(accountId, envelopeId, viewOptions);
            envid = envelopeId;
            var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();
            if (onlineProspectData != null)
            {
                onlineProspectData.EnvelopeID = envelopeId;
               
                db.SaveChanges();
            }
            // 5. Redirect the user's browser to the URL

            return Redirect(viewUrl.Url);

        }
        public async Task<ActionResult> OnPostAsyncLease()

        {
            ShomaRMEntities db = new ShomaRMEntities();
            string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            long UserID = Convert.ToInt64(uid);
            var tenantData = db.tbl_ApplyNow.Where(p => p.UserId == UserID).FirstOrDefault();
            long tid = 0;

            if (tenantData != null)
            {
                tid = tenantData.ID;
            }

            string TenantID = tid.ToString();


            var tenantdata = db.tbl_TenantOnline.Where(p => p.ProspectID == tid).FirstOrDefault();
            Session["StepNo"] = 15;
            string docName = new ProspectVerificationModel().PDFBuilder(TenantID);

            Document document = new Document

            {
                DocumentBase64 = Convert.ToBase64String(ReadContent(docName)),

                Name = "LeaseAgreement-" + TenantID,
                FileExtension = "pdf",
                DocumentId = "1"

            };

            Document[] documents = new Document[] { document };



            // Create the signer recipient object 

            Signer signer = new Signer

            {
                Email = tenantdata.Email,
                Name = tenantdata.FirstName + " " + tenantdata.LastName,
                ClientUserId = TenantID,

                RecipientId = "1",
                RoutingOrder = "1"

            };



            // Create the sign here tab (signing field on the document)

            SignHere signHereTab = new SignHere

            {
                DocumentId = "1",
                PageNumber = "3",
                RecipientId = "1",

                TabLabel = "Sign Here Tab",
                XPosition = "160",
                YPosition = "590"

            };

            SignHere[] signHereTabs = new SignHere[] { signHereTab };



            // Add the sign here tab array to the signer object.

            signer.Tabs = new Tabs { SignHereTabs = new List<SignHere>(signHereTabs) };

            // Create array of signer objects

            Signer[] signers = new Signer[] { signer };

            // Create recipients object

            Recipients recipients = new Recipients { Signers = new List<Signer>(signers) };

            // Bring the objects together in the EnvelopeDefinition

            EnvelopeDefinition envelopeDefinition = new EnvelopeDefinition

            {
                EmailSubject = "Please sign the document",

                Documents = new List<Document>(documents),

                Recipients = recipients,

                Status = "sent"

            };
            // 2. Use the SDK to create and send the envelope

            ApiClient apiClient = new ApiClient(basePath);

            apiClient.Configuration.AddDefaultHeader("Authorization", "Bearer " + accessToken);

            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient.Configuration);

            EnvelopeSummary results = envelopesApi.CreateEnvelope(accountId, envelopeDefinition);



            // 3. Create Envelope Recipient View request obj

            string envelopeId = results.EnvelopeId;

            //string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            RecipientViewRequest viewOptions = new RecipientViewRequest

            {
                //ReturnUrl = "http://localhost:53673/Checklist",
                ReturnUrl = "http://52.4.251.162:8086/Checklist",

                ClientUserId = TenantID,

                AuthenticationMethod = "none",

                UserName = tenantdata.FirstName + " " + tenantdata.LastName,
                Email = tenantdata.Email,

            };



            // 4. Use the SDK to obtain a Recipient View URL

            ViewUrl viewUrl = envelopesApi.CreateRecipientView(accountId, envelopeId, viewOptions);
            envid = envelopeId;
            var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();
            if (onlineProspectData != null)
            {
                onlineProspectData.EnvelopeID = envelopeId;

                db.SaveChanges();
            }
            // 5. Redirect the user's browser to the URL

            return Redirect(viewUrl.Url);

        }
        internal static byte[] ReadContent(string fileName)
        {
            byte[] buff = null;
            //string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", fileName);
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/" + fileName);


            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    long numBytes = new FileInfo(path).Length;
                    buff = br.ReadBytes((int)numBytes);
                }
            }
            return buff;
        }
        public ActionResult GetDocuDoc(string EnvelopeID)
        {
            
            EnvelopesApi envelopesApi = new EnvelopesApi();
            EnvelopeDocumentsResult docsList = envelopesApi.ListDocuments(accountId, EnvelopeID);

            String filePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            string filename = Path.GetRandomFileName();
            FileStream fs = null;
            try
            {
                // GetDocument() API call returns a MemoryStream
                MemoryStream docStream = (MemoryStream)envelopesApi.GetDocument(accountId, docsList.EnvelopeId, docsList.EnvelopeDocuments[0].DocumentId);
                // let's save the document to local file system
                filePath = filePath +  filename +".pdf";
                fs = new FileStream(filePath, FileMode.Create);
                docStream.Seek(0, SeekOrigin.Begin);
                docStream.CopyTo(fs);
                fs.Close();

                return Json(new { result = filename + ".pdf" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult GetDocuDocLease(long UserID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
        
            var tenantData = db.tbl_ApplyNow.Where(p => p.UserId == UserID).FirstOrDefault();
            EnvelopesApi envelopesApi = new EnvelopesApi();
            EnvelopeDocumentsResult docsList = envelopesApi.ListDocuments(accountId, tenantData.EnvelopeID);

            String filePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            string filename = Path.GetRandomFileName();
            FileStream fs = null;
            try
            {
                // GetDocument() API call returns a MemoryStream
                MemoryStream docStream = (MemoryStream)envelopesApi.GetDocument(accountId, docsList.EnvelopeId, docsList.EnvelopeDocuments[0].DocumentId);
                // let's save the document to local file system
                filePath = filePath + filename + ".pdf";
                fs = new FileStream(filePath, FileMode.Create);
                docStream.Seek(0, SeekOrigin.Begin);
                docStream.CopyTo(fs);
                fs.Close();

                return Json(new { result = filename + ".pdf" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}