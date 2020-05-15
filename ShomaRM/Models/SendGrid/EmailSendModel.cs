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
using LoggerEngine;
using System.Web.Configuration;

namespace ShomaRM.Models
{
    public class EmailSendModel
    {
        public string s_username = WebConfigurationManager.AppSettings["SendGridUserName"];
        public string s_password = WebConfigurationManager.AppSettings["SendGridPassword"];
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
                    LoggingHelper.LogMessage("Success", System.Diagnostics.TraceLevel.Info);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogMessage(ex, System.Diagnostics.TraceLevel.Error);
                    throw ex;
                    
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogMessage(ex, System.Diagnostics.TraceLevel.Error);
            }
        }
        public void SendEmailWithAttachment(string ToEmail, string Subject, string Body, List<string> FilePath)
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

                foreach(string filepath in FilePath)
                {
                    myMessage.AddAttachment(filepath);
                }

                try
                {
                    SendAsync(myMessage, s_username, s_password);
                    LoggingHelper.LogMessage("Success", System.Diagnostics.TraceLevel.Info);
                }
                catch (Exception ex)
                {
                    LoggingHelper.LogMessage(ex, System.Diagnostics.TraceLevel.Error);
                    throw ex;

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogMessage(ex, System.Diagnostics.TraceLevel.Error);
            }
        }
        void SendAsync(SendGridMessage message, string SGUserName, string SGPassword)
        {
            var credentials = new NetworkCredential(SGUserName, SGPassword);
            var transportWeb = new Web(credentials);
            try
            {
                transportWeb.Deliver(message);
                LoggingHelper.LogMessage("Success", System.Diagnostics.TraceLevel.Info);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogMessage(ex, System.Diagnostics.TraceLevel.Error);
                throw ex;
            }
        }
    }
}