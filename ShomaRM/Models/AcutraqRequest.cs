using Org.BouncyCastle.Asn1.Ocsp;
using ShomaRM.Data;
using ShomaRM.Models.Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ShomaRM.Models
{
    public class AcutraqRequest
    {

        public async Task<OrderXML>   PostAqutraqRequest(TenantOnlineModel data)
        {
            var _objAcqutraqOrder = new OrderXML();
            _objAcqutraqOrder.Method = "SEND ORDER";
            var Authentication = new Authentication();

            Authentication.Username = "pward";
            Authentication.Password = "Password1234!";
            _objAcqutraqOrder.Authentication = Authentication;
            _objAcqutraqOrder.TestMode = "Yes";
            _objAcqutraqOrder.ReturnResultURL = "http://52.4.251.162:8086/";
            var _objorder = new Order();
            _objorder.BillingReferenceCode = "000-0000";
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
            CurrentAddress.State =data.StateHomeString;
            CurrentAddress.Zipcode = data.ZipHome;
            CurrentAddress.Country = data.Country;
            _objsubject.CurrentAddress = CurrentAddress;
            _objorder.Subject = _objsubject;
            _objorder.PackageServiceCode = "CCEE";

            //employee
            var _objorderdetails = new OrderDetailEMP();
            _objorderdetails.ServiceCode = "EMP";
            _objorderdetails.OrderId = data.ProspectID.ToString();
            _objorderdetails.CompanyName =data.EmployerName;
            _objorderdetails.Position = data.JobTitle;
            _objorderdetails.Salary =data.Income.ToString();
            _objorderdetails.Manager = data.SupervisorName;
            _objorderdetails.Telephone = data.SupervisorPhone;
            _objorderdetails.EmployerCity = data.OfficeCity;
            _objorderdetails.EmployerState = data.OfficeState.ToString();
             var _objEmploymentDates = new EmploymentDates();
            _objEmploymentDates.StartDate = data.StartDateTxt;

            _objEmploymentDates.EndDate = "10/10/2020";
            _objorderdetails.EmploymentDates = _objEmploymentDates;
            _objorderdetails.ReasonForLeaving = "Test";
            //_objorderdetails.ReasonForLeaving = data.Reason;
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
            catch (Exception e) {
                var er = e;
            }
            if (result.Count() != 0)
            {
                var saveresult = "";
                foreach (var item in result)
                {
                    BackgroundScreeningModel backgroundscreening = new BackgroundScreeningModel();
                    backgroundscreening.Type = item.FirstAttribute.Value;
                    backgroundscreening.OrderID = Convert.ToInt32(item.LastAttribute.Value);
                    backgroundscreening.TenantId = Convert.ToInt32(data.ProspectID);
                    saveresult = (new BackgroundScreeningModel().SaveBackgroundScreening(backgroundscreening));
                }
            }
            return null;
        }
        public async Task<OrderXML> PostAqutraqRequestOld(object data)

        {
            var _objAcqutraqOrder = new OrderXML();
            _objAcqutraqOrder.Method = "SEND ORDER";
            var Authentication = new Authentication();

            Authentication.Username = "IntegrationUser";
            Authentication.Password = "Shom@Group2019!!";
            _objAcqutraqOrder.Authentication = Authentication;
            _objAcqutraqOrder.TestMode = "Yes";
            _objAcqutraqOrder.ReturnResultURL = "http://thinkersteps.com/contact.html";
            var _objorder = new Order();
            _objorder.BillingReferenceCode = "000-0000";
            var _objsubject = new Subject();
            _objsubject.FirstName = "Sachin";
            _objsubject.MiddleName = "Vijayrao";
            _objsubject.LastName = "Mahore";
            _objsubject.DOB = "07/08/1988";
            _objsubject.SSN = "111-22-3333";
            _objsubject.Gender = "Male";
            _objsubject.DLNumber = "222";
            _objsubject.ApplicantPosition = "Director";
            _objorder.Subject = _objsubject;
            _objAcqutraqOrder.Order = _objorder;
            var CurrentAddress = new CurrentAddress();
            CurrentAddress.StreetAddress = "Gopal Nagar";
            CurrentAddress.City = "Nagpur";
            CurrentAddress.State = "Maharashtra";
            CurrentAddress.Zipcode = "440016";
            CurrentAddress.Country = "India";
            _objsubject.CurrentAddress = CurrentAddress;
            _objorder.Subject = _objsubject;
            _objorder.PackageServiceCode = "CCEE";

            //employee
            var _objorderdetails = new OrderDetailEMP();
            _objorderdetails.ServiceCode = "EMPVR";
            _objorderdetails.OrderId = "123456";
            _objorderdetails.CompanyName = "NirviTech";
            _objorderdetails.Position = "Developer";
            _objorderdetails.Salary = "50000";
            _objorderdetails.Manager = "Test";
            _objorderdetails.Telephone = "(123)456-7890";
            _objorderdetails.EmployerCity = "Nagpur";
            _objorderdetails.EmployerState = "Nagpur";
            var _objEmploymentDates = new EmploymentDates();

            _objEmploymentDates.StartDate = "01/01/2018";
            _objEmploymentDates.EndDate = "10/10/2020";

            _objorderdetails.EmploymentDates = _objEmploymentDates;
            _objorderdetails.ReasonForLeaving = "Test";
            _objorder.OrderDetailEMP = _objorderdetails;

            var _objCriminal = new OrderDetailCriminal();
            _objCriminal.state = "Maharahtra";
            _objCriminal.ServiceCode = "MULTISTATEEVICT";
            _objCriminal.OrderId = "123456";
            _objorder.OrderDetailCriminal = _objCriminal;


            var _objCredit = new OrderDetailCredit();
            _objCredit.ServiceCode = "CREDITTUVANT";
            _objCredit.OrderId = "123456";
            _objorder.OrderDetailCredit = _objCredit;

            string Serialisexml = AquatraqHelper.Serialize(_objAcqutraqOrder);
            Serialisexml = AquatraqHelper.SetAttributeValue(Serialisexml, "123456");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("request", Serialisexml));

            var result = await AquatraqHelper.PostFormUrlEncoded<List<XElement>>("https://screen.acutraq.com/webservice/default.cfm", keyValues);
            if (result != null)
            {
                foreach (var item in result)
                {
                    string serviceCode = item.Attribute("EMPVR").Value;
                    string orderID = item.Attribute("orderID").Value;
                    string CRAorderID = item.Attribute("CRAorderID").Value;

                }
            }
            return null;
        }

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
    }






}