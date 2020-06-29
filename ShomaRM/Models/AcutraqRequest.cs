using Org.BouncyCastle.Asn1.Ocsp;
using ShomaRM.Data;
using ShomaRM.Models.Bluemoon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ShomaRM.Models
{
    public class AcutraqRequest
    {
        #region PostAll
        public async Task<OrderXML> PostAqutraqRequest(TenantOnlineModel data, bool isTest = true)
        {

            var _objAcqutraqOrder = new OrderXML();
            _objAcqutraqOrder.Method = "SEND ORDER";
            var Authentication = new Authentication();

            Authentication.Username = "pward";
            Authentication.Password = "Password1234!";
            _objAcqutraqOrder.Authentication = Authentication;
            if (isTest)
            {
                _objAcqutraqOrder.TestMode = "Yes";
            }
            _objAcqutraqOrder.ReturnResultURL = WebConfigurationManager.AppSettings["ServerURL"] + "BackgroundScreening/ReceiveRequest";
            var _objorder = new Order();
            _objorder.BillingReferenceCode = data.ID.ToString();
            var _objsubject = new Subject();
            _objsubject.FirstName = data.FirstName;
            _objsubject.MiddleName = data.MiddleInitial;
            _objsubject.LastName = data.LastName;
            _objsubject.DOB = data.DateOfBirthTxt;
            _objsubject.SSN = data.SSN;
            _objsubject.Gender = data.Gender == 1 ? "Male" : "Female";
            _objsubject.DLNumber = data.IDNumber;
            _objsubject.ApplicantPosition = data.JobTitle;
            _objorder.Subject = _objsubject;
            _objAcqutraqOrder.Order = _objorder;

            var CurrentAddress = new CurrentAddress();
            CurrentAddress.StreetAddress = data.HomeAddress1;
            CurrentAddress.City = data.CityHome;
            CurrentAddress.State = data.StateHomeString;
            CurrentAddress.Zipcode = data.ZipHome;
            CurrentAddress.Country = data.Country;
            _objsubject.CurrentAddress = CurrentAddress;
            _objorder.Subject = _objsubject;


            //employee
            var _objorderdetails = new OrderDetailEMP();
            _objorderdetails.ServiceCode = "EMP";
            _objorderdetails.OrderId = data.ProspectID.ToString();
            _objorderdetails.CompanyName = data.EmployerName;
            _objorderdetails.Position = data.JobTitle;
            _objorderdetails.Salary = data.Income.ToString();
            _objorderdetails.Manager = data.SupervisorName;
            _objorderdetails.Telephone = data.SupervisorPhone;
            _objorderdetails.EmployerCity = data.OfficeCity;
            _objorderdetails.EmployerState = data.OfficeState.ToString();
            var _objEmploymentDates = new EmploymentDates();
            _objEmploymentDates.StartDate = data.StartDateTxt;

            _objEmploymentDates.EndDate = data.DateExpireTxt == "" ? "00/00/0000" : data.DateExpireTxt;
            _objorderdetails.EmploymentDates = _objEmploymentDates;
            _objorderdetails.ReasonForLeaving = data.Reason; ;

            _objorder.OrderDetailEMP = _objorderdetails;

            var _objCriminal = new OrderDetailCriminal();
            _objCriminal.state = data.StateHomeString;
            _objCriminal.ServiceCode = "MULTISTATE";
            _objCriminal.OrderId = data.ProspectID.ToString();
            _objorder.OrderDetailCriminal = _objCriminal;

            var _objEVISEA = new OrderDetailEVISEA();
            _objEVISEA.ServiceCode = "EVISEA";
            _objEVISEA.OrderId = data.ProspectID.ToString();
            _objorder.OrderDetailEVISEA = _objEVISEA;

            var _objCredit = new OrderDetailCredit();
            _objCredit.ServiceCode = "TENTCREDIT";
            _objCredit.OrderId = data.ProspectID.ToString();
            _objorder.OrderDetailCredit = _objCredit;

            string Serialisexml = AquatraqHelper.Serialize(_objAcqutraqOrder);
            Serialisexml = AquatraqHelper.SetAttributeValue(Serialisexml, data.ProspectID.ToString());
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("request", Serialisexml));
            var result = new List<XElement>();
            try
            {
                result = await AquatraqHelper.PostFormUrlEncoded<List<XElement>>("https://orders.dciresources.com/webservice/default.cfm", keyValues);
            }
            catch (Exception e)
            {
                var er = e;
            }
            if (result.Count() != 0)
            {
                var saveresult = "";
                //Saving CRAOrderId and tenant id result in database 
                foreach (var item in result)
                {
                    //BackgroundScreeningModel backgroundscreening = new BackgroundScreeningModel();
                    //backgroundscreening.Type = item.FirstAttribute.Value;
                    //backgroundscreening.OrderID = Convert.ToInt32(item.LastAttribute.Value);
                    //backgroundscreening.TenantId = Convert.ToInt32(data.ProspectID);
                    //saveresult = (new BackgroundScreeningModel().SaveBackgroundScreening(backgroundscreening));
                }
            }
            return null;
        }
        #endregion

        #region PostEvisea
        public async Task<OrderXML> PostAqutraqEVISEA(TenantOnlineModel data, bool isTest = true)
        {
            var _objAcqutraqOrder = new OrderXMLEVISEA();
            _objAcqutraqOrder.Method = "SEND ORDER";
            var Authentication = new Authentication();

            Authentication.Username = "pward";
            Authentication.Password = "Password1234!";
            _objAcqutraqOrder.Authentication = Authentication;
            if (isTest)
            {
                _objAcqutraqOrder.TestMode = "Yes";
            }
            _objAcqutraqOrder.ReturnResultURL = WebConfigurationManager.AppSettings["ServerURL"] + "BackgroundScreening/ReceiveRequest";
            var _objorder = new OrderEVISEA();
            _objorder.BillingReferenceCode = data.ID.ToString();
            var _objsubject = new Subject();
            _objsubject.FirstName = data.FirstName;
            _objsubject.MiddleName = data.MiddleInitial;
            _objsubject.LastName = data.LastName;
            _objsubject.DOB = data.DateOfBirthTxt;
            _objsubject.SSN = data.SSN;
            _objsubject.Gender = data.Gender == 1 ? "Male" : "Female";
            _objsubject.DLNumber = data.IDNumber;
            _objsubject.ApplicantPosition = data.JobTitle;
            _objorder.Subject = _objsubject;
            _objAcqutraqOrder.Order = _objorder;

            var CurrentAddress = new CurrentAddress();
            CurrentAddress.StreetAddress = data.HomeAddress1;
            CurrentAddress.City = data.CityHome;
            CurrentAddress.State = data.StateHomeString;
            CurrentAddress.Zipcode = data.ZipHome;
            CurrentAddress.Country = data.Country;
            _objsubject.CurrentAddress = CurrentAddress;
            _objorder.Subject = _objsubject;

            var _objEVISEA = new OrderDetailEVISEA();
            _objEVISEA.ServiceCode = "EVISEA";
            _objEVISEA.OrderId = data.ProspectID.ToString();
            _objorder.OrderDetailEVISEA = _objEVISEA;



            string Serialisexml = AquatraqHelper.Serialize(_objAcqutraqOrder);
            Serialisexml = AquatraqHelper.SetAttributeValue(Serialisexml, data.ProspectID.ToString());
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("request", Serialisexml));
            var result = new List<XElement>();
            try
            {
                result = await AquatraqHelper.PostFormUrlEncoded<List<XElement>>("https://orders.dciresources.com/webservice/default.cfm", keyValues);
            }
            catch (Exception e)
            {
                var er = e;
            }
            if (result.Count() != 0)
            {
                var saveresult = "";
                //Saving CRAOrderId and tenant id result in database 
                foreach (var item in result)
                {
                    //BackgroundScreeningModel backgroundscreening = new BackgroundScreeningModel();
                    //backgroundscreening.Type = item.FirstAttribute.Value;
                    //backgroundscreening.OrderID = Convert.ToInt32(item.LastAttribute.Value);
                    //backgroundscreening.TenantId = Convert.ToInt32(data.ProspectID);
                    //saveresult = (new BackgroundScreeningModel().SaveBackgroundScreening(backgroundscreening));
                }
            }
            return null;
        }
        #endregion
        #region PostMULTISTATE
        public async Task<OrderXML> PostAqutraqRequestMULTISTATE(TenantOnlineModel data, bool isTest = true)
        {

            var _objAcqutraqOrder = new OrderXMLCriminal();
            _objAcqutraqOrder.Method = "SEND ORDER";
            var Authentication = new Authentication();

            Authentication.Username = "pward";
            Authentication.Password = "Password1234!";
            _objAcqutraqOrder.Authentication = Authentication;
            if (isTest)
            {
                _objAcqutraqOrder.TestMode = "Yes";
            }
            _objAcqutraqOrder.ReturnResultURL = WebConfigurationManager.AppSettings["ServerURL"] + "BackgroundScreening/ReceiveRequest";
            var _objorder = new OrderCriminal();
            _objorder.BillingReferenceCode = data.ID.ToString();
            var _objsubject = new Subject();
            _objsubject.FirstName = data.FirstName;
            _objsubject.MiddleName = data.MiddleInitial;
            _objsubject.LastName = data.LastName;
            _objsubject.DOB = data.DateOfBirthTxt;
            _objsubject.SSN = data.SSN;
            _objsubject.Gender = data.Gender == 1 ? "Male" : "Female";
            _objsubject.DLNumber = data.IDNumber;
            _objsubject.ApplicantPosition = data.JobTitle;
            _objorder.Subject = _objsubject;
            _objAcqutraqOrder.Order = _objorder;

            var CurrentAddress = new CurrentAddress();
            CurrentAddress.StreetAddress = data.HomeAddress1;
            CurrentAddress.City = data.CityHome;
            CurrentAddress.State = data.StateHomeString;
            CurrentAddress.Zipcode = data.ZipHome;
            CurrentAddress.Country = data.Country;
            _objsubject.CurrentAddress = CurrentAddress;
            _objorder.Subject = _objsubject;




            var _objCriminal = new OrderDetailCriminal();
            _objCriminal.state = data.StateHomeString;
            _objCriminal.ServiceCode = "MULTISTATE";
            _objCriminal.OrderId = data.ProspectID.ToString();
            _objorder.OrderDetailCriminal = _objCriminal;



            string Serialisexml = AquatraqHelper.Serialize(_objAcqutraqOrder);
            Serialisexml = AquatraqHelper.SetAttributeValue(Serialisexml, data.ProspectID.ToString());
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("request", Serialisexml));
            var result = new List<XElement>();
            try
            {
                result = await AquatraqHelper.PostFormUrlEncoded<List<XElement>>("https://orders.dciresources.com/webservice/default.cfm", keyValues);
            }
            catch (Exception e)
            {
                var er = e;
            }
            if (result.Count() != 0)
            {
                var saveresult = "";
                //Saving CRAOrderId and tenant id result in database 
                foreach (var item in result)
                {
                    //BackgroundScreeningModel backgroundscreening = new BackgroundScreeningModel();
                    //backgroundscreening.Type = item.FirstAttribute.Value;
                    //backgroundscreening.OrderID = Convert.ToInt32(item.LastAttribute.Value);
                    //backgroundscreening.TenantId = Convert.ToInt32(data.ProspectID);
                    //saveresult = (new BackgroundScreeningModel().SaveBackgroundScreening(backgroundscreening));
                }
            }
            return null;
        }
        #endregion

        #region Tenant
        public async Task<string> PostAqutraqTenant(TenantOnlineModel data, bool isTest = true)
        {
            string bgresult = "0";
            var _objAcqutraqOrder = new OrderXMLTenant();
            _objAcqutraqOrder.Method = "SEND ORDER";
            var Authentication = new Authentication();

            Authentication.Username = "pward";
            Authentication.Password = "Password1234!";
            _objAcqutraqOrder.Authentication = Authentication;
            if (isTest)
            {
                _objAcqutraqOrder.TestMode = "Yes";
            }

            _objAcqutraqOrder.ReturnResultURL = WebConfigurationManager.AppSettings["ServerURL"] + "BackgroundScreening/ReceiveRequest";
            
            var _objorder = new OrderTenant();
            _objorder.BillingReferenceCode = data.ID.ToString();
            var _objsubject = new Subject();
            _objsubject.FirstName = data.FirstName;
            _objsubject.MiddleName = data.MiddleInitial;
            _objsubject.LastName = data.LastName;
            _objsubject.DOB = data.DateOfBirthTxt;
            _objsubject.SSN = data.SSN;
            _objsubject.Gender = data.Gender == 1 ? "Male" : "Female";
            _objsubject.DLNumber = data.IDNumber;
            _objsubject.ApplicantPosition = data.JobTitle;
            _objorder.Subject = _objsubject;
            _objAcqutraqOrder.Order = _objorder;

            var CurrentAddress = new CurrentAddress();
            CurrentAddress.StreetAddress = data.HomeAddress1;
            CurrentAddress.City = data.CityHome;
            CurrentAddress.State = data.StateHomeString;
            CurrentAddress.Zipcode = data.ZipHome;
            CurrentAddress.Country = data.CountryString;
            _objsubject.CurrentAddress = CurrentAddress;
            _objorder.Subject = _objsubject;

            var _objCredit = new OrderDetailCredit();
            _objCredit.ServiceCode = "TENTCREDIT";
            _objCredit.OrderId =data.ID.ToString();
            _objorder.OrderDetailCredit = _objCredit;

            string Serialisexml = AquatraqHelper.Serialize(_objAcqutraqOrder);
            Serialisexml = AquatraqHelper.SetAttributeValue(Serialisexml, data.ProspectID.ToString());
            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("request", Serialisexml));
            var result = new List<XElement>();
            try
            {
                result = await AquatraqHelper.PostFormUrlEncoded<List<XElement>>("https://orders.dciresources.com/webservice/default.cfm", keyValues);
            }
            catch (Exception e)
            {
                var er = e;
            }
            if (result.Count() != 0)
            {
                bgresult = "1";
                var saveresult = "";
                foreach (var item in result)
                {
                    //BackgroundScreeningModel backgroundscreening = new BackgroundScreeningModel();
                    //backgroundscreening.Type = item.FirstAttribute.Value;
                    //backgroundscreening.OrderID = Convert.ToInt32(item.LastAttribute.Value);
                    //backgroundscreening.TenantId = Convert.ToInt32(data.ID);
                    //saveresult = (new BackgroundScreeningModel().SaveBackgroundScreening(backgroundscreening));
                }
            }
            return bgresult;
        }
        #endregion
        //All
        #region All

        public class OrderXML
        {
            public string Method { get; set; }
            public Authentication Authentication { get; set; }
            public string TestMode { get; set; }
            public string ReturnResultURL { get; set; }
            public string OrderingUser { get; set; }

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
            public Subject Subject { get; set; }
            public string PackageServiceCode { get; set; }
            public OrderDetailEMP OrderDetailEMP { get; set; }
            public OrderDetailCriminal OrderDetailCriminal { get; set; }
            public OrderDetailCredit OrderDetailCredit { get; set; }
            public OrderDetailEVISEA OrderDetailEVISEA { get; set; }

            public OrderDetailResponse OrderDetail { get; set; }
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
        public class Subject
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Generation { get; set; }
            public string DOB { get; set; }
            public string SSN { get; set; }
            public string Gender { get; set; }
            public string Ethnicity { get; set; }
            public string DLNumber { get; set; }
            public string ApplicantPosition { get; set; }

            public CurrentAddress CurrentAddress { get; set; }
            public Aliases Aliases { get; set; }
        }

        public class Aliases
        {
            public Alias Alias { get; set; }
        }
        public class Alias
        {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }

        }

        public class EmploymentDates
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        public class CurrentAddress
        {
            public string StreetAddress { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zipcode { get; set; }
            public string Country { get; set; }
        }

        //employeee
        public class OrderDetailEMP
        {

            [XmlAttribute("ServiceCode")]
            public string ServiceCode { get; set; }

            [XmlAttribute("OrderId")]
            public string OrderId { get; set; }
            [XmlAttribute("CRAorderID")]
            public string CRAorderID { get; set; }

            public string CompanyName { get; set; }
            public string Position { get; set; }
            public string Salary { get; set; }

            public string Manager { get; set; }
            public string Telephone { get; set; }
            public string EmployerCity { get; set; }
            public string EmployerState { get; set; }
            public EmploymentDates EmploymentDates { get; set; }

            public string ReasonForLeaving { get; set; }

        }

        //Credit
        public class OrderDetailCredit
        {

            [XmlAttribute("ServiceCode")]
            public string ServiceCode { get; set; }

            [XmlAttribute("OrderId")]
            public string OrderId { get; set; }
            [XmlAttribute("CRAorderID")]
            public string CRAorderID { get; set; }

        }

        //criminal
        public class OrderDetailCriminal
        {
            [XmlAttribute("ServiceCode")]
            public string ServiceCode { get; set; }

            [XmlAttribute("OrderId")]

            public string OrderId { get; set; }

            [XmlAttribute("CRAorderID")]
            public string CRAorderID { get; set; }

            public string state { get; set; }
        }
        //criminal
        public class OrderDetailEVISEA
        {
            [XmlAttribute("ServiceCode")]
            public string ServiceCode { get; set; }

            [XmlAttribute("OrderId")]

            public string OrderId { get; set; }


        }
        #endregion
        #region EVISEA
        //EVISEA
        public class OrderXMLEVISEA
        {
            public string Method { get; set; }
            public Authentication Authentication { get; set; }
            public string TestMode { get; set; }
            public string ReturnResultURL { get; set; }
            public string OrderingUser { get; set; }

            public OrderEVISEA Order { get; set; }

        }
        public class OrderEVISEA
        {
            public string BillingReferenceCode { get; set; }
            public Subject Subject { get; set; }
            public string PackageServiceCode { get; set; }

            public OrderDetailEVISEA OrderDetailEVISEA { get; set; }

            public OrderDetailResponse OrderDetail { get; set; }
        }
        #endregion

        #region Multistate
        //Multistate
        public class OrderXMLCriminal
        {
            public string Method { get; set; }
            public Authentication Authentication { get; set; }
            public string TestMode { get; set; }
            public string ReturnResultURL { get; set; }
            public string OrderingUser { get; set; }

            public OrderCriminal Order { get; set; }

        }
        public class OrderCriminal
        {
            public string BillingReferenceCode { get; set; }
            public Subject Subject { get; set; }
            public string PackageServiceCode { get; set; }

            public OrderDetailCriminal OrderDetailCriminal { get; set; }

            public OrderDetailResponse OrderDetail { get; set; }
        }
        #endregion
        #region Tenant
        //Tenanat
        public class OrderXMLTenant
        {
            public string Method { get; set; }
            public Authentication Authentication { get; set; }
            public string TestMode { get; set; }
            public string ReturnResultURL { get; set; }
            public string OrderingUser { get; set; }

            public OrderTenant Order { get; set; }

        }
        public class OrderTenant
        {
            public string BillingReferenceCode { get; set; }
            public Subject Subject { get; set; }
            public string PackageServiceCode { get; set; }


            public OrderDetailCredit OrderDetailCredit { get; set; }
            public OrderDetailResponse OrderDetail { get; set; }
        }
        #endregion
    }
}