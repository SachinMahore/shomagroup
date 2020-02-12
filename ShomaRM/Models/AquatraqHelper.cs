using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static ShomaRM.Models.AcutraqRequest;

namespace ShomaRM.Models
{
    public static class AquatraqHelper
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static string SetAttributeValue(string defaultXML, string ordernumber)
        {

            try
            {
                defaultXML = defaultXML.Replace("<CompanyName", "<CompanyName CurrentEmployer = \"Yes\"");

                defaultXML = defaultXML.Replace("<PackageServiceCode", "<PackageServiceCode OrderId=\"" + ordernumber + "\"");
                defaultXML = defaultXML.Replace("<Salary", "<Salary period=\"Yearly\"");
               
                defaultXML = defaultXML.Replace("<OrderDetailEMP", "<OrderDetail");
                defaultXML = defaultXML.Replace("</OrderDetailEMP", "</OrderDetail");

                defaultXML = defaultXML.Replace("<OrderDetailCredit", "<OrderDetail");
                defaultXML = defaultXML.Replace("</OrderDetailCredit ", "</OrderDetail");


                defaultXML = defaultXML.Replace("<OrderDetailCriminal", "<OrderDetail");
                defaultXML = defaultXML.Replace("</OrderDetailCriminal", "</OrderDetail");


                return defaultXML;



            }
            catch (Exception e)
            {
                return defaultXML;
                Console.WriteLine(e);
            }



        }
      
        public static async Task<List<XElement>> PostFormUrlEncoded<TResult>(string url, List<KeyValuePair<string, string>> postData)
        {
            using (var httpClient = new  HttpClient())
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    string serializedString = await response.Content.ReadAsStringAsync();
                    var xDoc = XDocument.Parse(serializedString);
                  
                        // for attribute
                        var resultOrderDetail = xDoc
                         .Descendants("OrderXML")
                         .Descendants("Order")
                         .Descendants("OrderDetail")
                         .ToList();
                        return resultOrderDetail;
                  
                }
            }
        }

       
    }
   
}	    

    