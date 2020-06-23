using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace ShomaRM.Models
{
    public class UsaePayWSDLModel
    {
        public string sourceKeySendBox = WebConfigurationManager.AppSettings["USEPaySourceKeySandBox"];
        public string sourceKeyLive = WebConfigurationManager.AppSettings["USEPaySourceKeyLive"];
        public bool useSandBox = Convert.ToBoolean(WebConfigurationManager.AppSettings["USEPayUseSandbox"]);

        public string CreateUsaePayAccount(TenantOnlineModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = "4987";   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token

            usaepay.CustomerObject customer = new usaepay.CustomerObject();
            usaepay.Address address = new usaepay.Address();
            address.FirstName =model.FirstName ;
            address.LastName = model.LastName;
            address.Company = model.EmployerName;
            address.Street = model.HomeAddress1+" "+model.HomeAddress2;
            address.City =model.CityHome;
            address.State = model.StateHomeString;
            address.Zip = model.ZipHome;
            address.Country = model.CountryString;
            customer.BillingAddress = address;

            customer.Enabled = true;
            customer.Amount = 5.00;
            customer.Next = "2025-08-15";
            customer.Schedule = "monthly";

            usaepay.PaymentMethod[] payMethod = new usaepay.PaymentMethod[1];
            payMethod[0] = new usaepay.PaymentMethod();
            payMethod[0].CardExpiration = "1222";
            payMethod[0].CardNumber = "4000100011112224";
            payMethod[0].AvsStreet = "123 Main st.";
            payMethod[0].AvsZip = "90046";
            payMethod[0].CardType = "My Visa";

            customer.PaymentMethods = payMethod;
            string response;

            try
            {
                response = client.addCustomer(token, customer);
                transStatus = string.Concat(response);
            }

            catch (Exception err)
            {

            }


            return transStatus;
        }
        public string AddCustPaymentMethodCC(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = "4987";   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string CustNum = model.CCVNumber;

            usaepay.PaymentMethod payMethod = new usaepay.PaymentMethod();
            payMethod.CardExpiration = model.CardMonth+model.CardYear;
            payMethod.CardNumber =model.CardNumber;
            payMethod.AvsStreet = "Nagpur";
            payMethod.AvsZip = "90046";
            payMethod.MethodName = model.Name_On_Card;

            string response;

            try
            {
                response = client.addCustomerPaymentMethod(token, CustNum, payMethod, false, true);
                transStatus = string.Concat(response);
            }

            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return transStatus;
        }
        //public string CreateUsaePayAccount(string RefNo, decimal Credit_Amount)
        //{
        //    ShomaRMEntities db = new ShomaRMEntities();
        //    string transStatus = "";
        //    usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
        //    usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

        //    token.SourceKey = sourceKeySendBox;
        //    token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
        //    string pin = "7894";   // pin assigned to source

        //    usaepay.ueHash hash = new usaepay.ueHash();
        //    hash.Type = "md5";  // Type of encryption 
        //    hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

        //    string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
        //    hash.HashValue = GenerateHash(prehashvalue); // generate hash

        //    token.PinHash = hash;   // add hash value to token

        //    usaepay.CustomerObject customer = new usaepay.CustomerObject();
        //    usaepay.Address address = new usaepay.Address();
        //    address.FirstName = "Sachin";
        //    address.LastName = "Mahore";
        //    address.Company = "NirviTech";
        //    address.Street = "Matamypura.";
        //    address.City = "Nerpinglai";
        //    address.State = "MH";
        //    address.Zip = "4447047";
        //    address.Country = "IND";
        //    customer.BillingAddress = address;

        //    customer.Enabled = true;
        //    customer.Amount = 5.00;
        //    customer.Next = "2010-08-15";
        //    customer.Schedule = "monthly";

        //    usaepay.PaymentMethod[] payMethod = new usaepay.PaymentMethod[1];
        //    payMethod[0] = new usaepay.PaymentMethod();
        //    payMethod[0].CardExpiration = "1222";
        //    payMethod[0].CardNumber = "4444555566667779";
        //    payMethod[0].AvsStreet = "123 Main st.";
        //    payMethod[0].AvsZip = "90046";
        //    payMethod[0].CardType = "My Visa";

        //    customer.PaymentMethods = payMethod;
        //    string response;

        //    try
        //    {
        //        response = client.addCustomer(token, customer);
        //        transStatus = string.Concat(response);
        //    }

        //    catch (Exception err)
        //    {

        //    }


        //    return transStatus;
        //}
        private static string GenerateHash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
      
        public string ChargeCardSoap(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = "5577";   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token

            usaepay.TransactionRequestObject tran = new usaepay.TransactionRequestObject();

            tran.Command = "cc:sale";
            tran.Details = new usaepay.TransactionDetail();
            tran.Details.Amount = 1.00;
            tran.Details.AmountSpecified = true;
            tran.Details.Invoice = "1234";
            tran.Details.Description = "Example Transaction";

            tran.CreditCardData = new usaepay.CreditCardData();
            tran.CreditCardData.CardNumber = "4444555566667779";
            tran.CreditCardData.CardExpiration = "0919";

            usaepay.TransactionResponse response = new usaepay.TransactionResponse();

            try
            {
                response = client.runTransaction(token, tran);

                if (response.ResultCode == "A")
                {
                    transStatus = string.Concat("Transaction Approved, RefNum: ", response.RefNum);
                }
                else
                {
                    transStatus = string.Concat("Transaction Failed: ", response.Error);
                }
            }
            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return transStatus;
        }
        //public string AddCustPaymentMethodCC(ApplyNowModel model)
        //{
        //    ShomaRMEntities db = new ShomaRMEntities();
        //    string transStatus = "";
        //    usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
        //    usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

        //    token.SourceKey = sourceKeySendBox;
        //    token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
        //    string pin = "7894";   // pin assigned to source

        //    usaepay.ueHash hash = new usaepay.ueHash();
        //    hash.Type = "md5";  // Type of encryption 
        //    hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

        //    string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
        //    hash.HashValue = GenerateHash(prehashvalue); // generate hash

        //    token.PinHash = hash;   // add hash value to token
        //    string CustNum = "11248650";

        //    usaepay.PaymentMethod payMethod = new usaepay.PaymentMethod();
        //    payMethod.CardExpiration = "1222";
        //    payMethod.CardNumber = "4000100011112224";
        //    payMethod.AvsStreet = "Nagpur";
        //    payMethod.AvsZip = "90046";
        //    payMethod.MethodName = "My Visa";

        //    string response;

        //    try
        //    {
        //        response = client.addCustomerPaymentMethod(token, CustNum, payMethod, false, true);
        //        transStatus = string.Concat(response);
        //    }

        //    catch (Exception err)
        //    {
        //        transStatus = err.Message;
        //    }
        //    return transStatus;
        //}
        public string AddCustPaymentMethodACH(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = "7894";   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string CustNum = "11248650";

            usaepay.PaymentMethod payMethod = new usaepay.PaymentMethod();
            
            payMethod.Routing = "234329098";
            payMethod.Account = "676867862126";
            //payMethod.AvsZip = "90046";
            payMethod.MethodName = "SachinSBIACH";

            string response;

            try
            {
                response = client.addCustomerPaymentMethod(token, CustNum, payMethod, false, true);
                transStatus = string.Concat(response);
            }

            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return transStatus;
        }
        public string PayUsingCustomerNum(string CustID, string PMID,decimal Amount,string Desc)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = "4987";   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string custNum = CustID;
            string paymentMethodID = PMID;
            usaepay.CustomerTransactionRequest tran = new usaepay.CustomerTransactionRequest();
            tran.Details = new usaepay.TransactionDetail();

            tran.Details.Invoice = "123456";
            tran.Details.Description = Desc;
            tran.Details.Amount =Convert.ToDouble(Amount);
            tran.Details.AmountSpecified = true;


            usaepay.TransactionResponse response = new usaepay.TransactionResponse();

            try
            {
                response = client.runCustomerTransaction(token, custNum, paymentMethodID, tran);
                transStatus = string.Concat(response.Result);
            }

            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return transStatus;
        }

        public string TransReport()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = "4987";   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string start = "2020-06-01";
            string end = "2020-06-23";
            string format = "html";
            string report = "CreditCard:Sales By Date";

            try
            {
                string response = client.getTransactionReport(token, start, end, report, format);
                byte[] decbuff = Convert.FromBase64String(response);
               transStatus= Encoding.UTF8.GetString(decbuff);
            }

            catch (Exception err)
            {
                transStatus= err.Message;
            }
            return transStatus;
        }
        
    }
}