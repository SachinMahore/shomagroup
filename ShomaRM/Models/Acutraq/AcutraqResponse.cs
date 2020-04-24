using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using static ShomaRM.Models.AcutraqRequest;

namespace ShomaRM.Models.Acutraq
{
    public class OrderXML
    {
        public string Method { get; set; }
        [AllowHtml]
        public Authentication Authentication { get; set; }
        [AllowHtml]
        public string TestMode { get; set; }
        [AllowHtml]
        public string ReturnResultURL { get; set; }
        [AllowHtml]
        public string OrderingUser { get; set; }
        [AllowHtml]
        public Order Order { get; set; }

    }
    public class Authentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TestMode { get; set; }
    }
    public class Order
    {
        public string BillingReferenceCode { get; set; }
     
        public string PackageServiceCode { get; set; }


        [XmlArray("OrderDetail")]
        public List<OrderDetailResponse> OrderDetail { get; set; }
    }
    public class OrderDetailResponse
    {

        [XmlAttribute("ServiceCode")]
        public string ServiceCode { get; set; }

        [XmlAttribute("OrderId")]
        public string OrderId { get; set; }
        [XmlAttribute("CRAorderID")]
        public string CRAorderID { get; set; }
    }
}