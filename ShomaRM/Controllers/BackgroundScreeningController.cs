using Newtonsoft.Json;
using ShomaRM.Models;
using ShomaRM.Models.Acutraq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ShomaRM.Controllers
{
    public class BackgroundScreeningController : Controller
    {

      
        [HttpPost, ValidateInput(false)]
        public ActionResult ReceiveRequest()
        {
            try
            {
				var result = Request.Form["request"];

                var xDoc = XDocument.Parse(result.ToString());

                // for attribute
                var resultOrderDetail = xDoc
                 .Descendants("OrderXML")
                 .Descendants("Order")
                 .Descendants("OrderDetail")
                 .ToList();
               
            }
            catch (Exception ex)
            {
            }
			return null;
        }
    }
}