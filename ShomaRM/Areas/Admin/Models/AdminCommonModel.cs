using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class AdminCommonModel
    {
        public static string ScriptVersion()
        {
            return ConfigurationManager.AppSettings["ScriptVer"];
        }
    }
}