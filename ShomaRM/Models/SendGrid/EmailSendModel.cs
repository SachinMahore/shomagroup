using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using ShomaRM.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Data.Common;

namespace ShomaRM.Models
{
    public class EmailSendModel
    {
        public string s_username = "nirvitech";
        public string s_password = "@17nirvi18#";


        public void SendEmail(string ToEmail, string Subject, string Body)
        {
            try
            {
                var myMessage = new SendGridMessage();
                myMessage.AddTo(ToEmail);
                myMessage.From = new MailAddress("admin@shomarm.com", "Administrator");
                myMessage.Subject = Subject;
                myMessage.Html = Body;
                myMessage.DisableOpenTracking();
                myMessage.DisableClickTracking();
                myMessage.EnableSpamCheck();
                try
                {
                    SendAsync(myMessage, s_username, s_password);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {

            }
        }
        void SendAsync(SendGridMessage message, string SGUserName, string SGPassword)
        {
            var credentials = new NetworkCredential(SGUserName, SGPassword);
            var transportWeb = new Web(credentials);
            try
            {
                transportWeb.Deliver(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}