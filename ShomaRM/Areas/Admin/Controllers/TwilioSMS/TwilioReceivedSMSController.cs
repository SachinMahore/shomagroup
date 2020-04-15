using ShomaRM.Data;
using ShomaRM.Models.TwilioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Areas.Admin.Controllers.TwilioSMS
{
    public class TwilioReceivedSMSController : Controller
    {
        // GET: Admin/TwilioSMS
        public ActionResult Index()
        {
            ViewBag.ActiveMenu = "TwilioSMS";
            SMSListViewModel SMSListData = new SMSListViewModel();
            IList<SMSMessagesModel> model = new List<SMSMessagesModel>();
            ShomaRMEntities entities = new ShomaRMEntities();
            var multipledata = entities.tbl_SMSMessages.ToList().OrderByDescending(a=>a.MsgID);
            foreach (var singledata in multipledata) {
                var smsmessage = new SMSMessagesModel();
                smsmessage.MsgID = singledata.MsgID;
                smsmessage.PhoneNumber = singledata.PhoneNumber;
                smsmessage.ReceivedDate = singledata.ReceivedDate;
                smsmessage.Message = singledata.Message;
                model.Add(smsmessage);
            }
            SMSListData.SMSMessagesModel = model;
            return View(SMSListData);
        }
    }
}