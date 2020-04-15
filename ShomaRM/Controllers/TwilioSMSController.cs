using ShomaRM.Models.TwilioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers
{
    public class TwilioSMSController : Controller
    {
        [HttpPost]
        public void ReceiveSMS()
        {
            try
            {
                SMSMessagesModel model = new SMSMessagesModel();
                model.PhoneNumber = Request.Form["From"];
                model.Message = Request.Form["Body"];
                model.ReceivedDate = DateTime.UtcNow;
                string result = (new SMSMessagesModel().SaveSMS(model));
            }
            catch(Exception ex)
            {

            }
        
        }

    }
}