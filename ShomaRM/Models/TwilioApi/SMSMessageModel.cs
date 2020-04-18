using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.TwilioApi
{


    public partial class SMSMessagesModel
    {
        public long MsgID { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }



        public string SaveSMS(SMSMessagesModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            var saveSMS = new tbl_SMSMessages()
            {
                Message = model.Message,
                PhoneNumber = model.PhoneNumber,
                ReceivedDate = model.ReceivedDate
            };
            db.tbl_SMSMessages.Add(saveSMS);
            db.SaveChanges();
            msg = "SMS Save Successfully";


            db.Dispose();
            return msg;
        }

    }
}