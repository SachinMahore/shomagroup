using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ShomaRM.Models.TwilioApi
{
    public class TwilioService
    {
        public void SMS(string To, string MsgText)
        {
            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSID"];
            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            var twilioNumber = ConfigurationManager.AppSettings["TwilioNumber"];

            TwilioClient.Init(accountSid, authToken);

            try
            {
                var message = MessageResource.Create(body: MsgText,
                    from: new Twilio.Types.PhoneNumber(twilioNumber),//+1 202 866 1345
                    to: new Twilio.Types.PhoneNumber("+1" + To), forceDelivery: true
                );
            }
            catch (Exception ex)
            {

            }
        }


        public void Call(string To)
        {
            var accountSid = ConfigurationManager.AppSettings["TwilioAccountSID"];
            var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
            var twilioNumber = ConfigurationManager.AppSettings["TwilioNumber"];

            TwilioClient.Init(accountSid, authToken);

            var call = CallResource.Create(
             method: Twilio.Http.HttpMethod.Get,

         statusCallbackMethod: Twilio.Http.HttpMethod.Post,
         url: new Uri("http://shoma.thinkersteps.com/VoiceMessage.xml"),
              to: new Twilio.Types.PhoneNumber(To),
              from: new Twilio.Types.PhoneNumber(twilioNumber)
            );
        }

    }
}