using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models.Calender
{
    public class CalenderEvents
    {
        public string id { get; set; }
        public string subject { get; set; }
        public DateTimeModel start { get; set; }
        public DateTimeModel end { get; set; }
    }

    public class DateTimeModel
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

}