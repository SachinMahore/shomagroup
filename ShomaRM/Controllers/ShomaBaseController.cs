using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoggerEngine;

namespace ShomaRM.Controllers
{
    public abstract partial class ShomaBaseController : Controller
    {
        public void LogError(string message, System.Diagnostics.TraceLevel traceLevel = System.Diagnostics.TraceLevel.Info, object obj = null, string methodName = "", string fileName = "", int lineNumber = 0)
        {
            LoggingHelper.LogMessage(message, traceLevel, obj, methodName, fileName, lineNumber);
        }
        public void LogError(Exception exception, System.Diagnostics.TraceLevel traceLevel = System.Diagnostics.TraceLevel.Info, object obj = null)
        {
            LoggingHelper.LogMessage(exception, traceLevel, obj);
        }
    }
}