using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.TwilioApi
{
    public class SMSListViewModel
    {
        public SMSListViewModel()
        {
            SMSMessagesModel = new List<SMSMessagesModel>();
        }
        public IList<SMSMessagesModel> SMSMessagesModel { get; set; }
    }
}