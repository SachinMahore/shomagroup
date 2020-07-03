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
        public string pinSendBox = WebConfigurationManager.AppSettings["USEPayPinSandBox"];
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
            string pin = pinSendBox;   // pin assigned to source

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
            address.State = model.StateString;
            address.Zip = model.ZipHome;
            address.Country = model.CountryString;
            customer.BillingAddress = address;

            customer.Enabled = true;
            //customer.Amount = 5.00;
            //customer.Next = "2025-08-15";
            //customer.Schedule = "monthly";

            //usaepay.PaymentMethod[] payMethod = new usaepay.PaymentMethod[1];
            //payMethod[0] = new usaepay.PaymentMethod();
            //payMethod[0].CardExpiration = "1222";
            //payMethod[0].CardNumber = "4000100011112224";
            //payMethod[0].AvsStreet = "123 Main st.";
            //payMethod[0].AvsZip = "90046";
            //payMethod[0].CardType = "My Visa";

            //customer.PaymentMethods = payMethod;
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
        public string AddCustPaymentMethod(ApplyNowModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string CustNum = model.CustID;

            usaepay.PaymentMethod payMethod = new usaepay.PaymentMethod();
            if(model.PaymentMethod==2)
            {
                payMethod.MethodType = "CreditCard";
                payMethod.CardExpiration = model.CardMonth + model.CardYear;
                payMethod.CardNumber = model.CardNumber;
                
            }
            else
            {
                payMethod.MethodType = "ACH";
                payMethod.Routing = model.RoutingNumber;
                payMethod.Account = model.CardNumber;
            }
         
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
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token

            usaepay.TransactionRequestObject tran = new usaepay.TransactionRequestObject();

           
            tran.Details = new usaepay.TransactionDetail();
            tran.Details.Amount =Convert.ToDouble(model.Charge_Amount);
            tran.Details.AmountSpecified = true;
            tran.Details.Invoice = "1234";
            tran.Details.Description = model.Description;
            usaepay.TransactionResponse response = new usaepay.TransactionResponse();
            if (model.PaymentMethod == 2)
            {
                tran.Command = "cc:sale";
                tran.CreditCardData = new usaepay.CreditCardData();
                tran.CreditCardData.CardNumber = model.CardNumber;
                tran.CreditCardData.CardExpiration = model.CardMonth + model.CardYear;
                try
                {
                    response = client.runTransaction(token, tran);

                    if (response.ResultCode == "A")
                    {
                        transStatus = string.Concat(response.Result);
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
            }
            else if(model.PaymentMethod == 1)
            {
                
                tran.CheckData = new usaepay.CheckData();
                tran.CheckData.Account = model.AccountNumber;
                tran.CheckData.Routing = model.RoutingNumber;
               

                tran.AccountHolder = model.Name_On_Card;
                try
                {
                    response = client.runCheckSale(token, tran);

                    if (response.ResultCode == "A")
                    {
                        transStatus = string.Concat(response.Result);
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
            }
            

          
            return transStatus + "|" + response.AuthCode + "|" + response.RefNum;
        }
     
        public string PayUsingCustomerNum(string CustID, string PMID,decimal Amount,string Desc)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

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
            return transStatus + "|" + response.AuthCode + "|" + response.RefNum;
        }

        public string TransReport()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string start = "2020-06-01";
            string end = "2020-06-24";
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
        //Sachin M 24 june
        public string DeletePaymentMethod(string CustID, string PMID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string CustNum = CustID;
            string PaymentID =PMID;

            Boolean response;

            try
            {
                response = client.deleteCustomerPaymentMethod(token, CustNum, PaymentID);
              transStatus =string.Concat(response);
            }

            catch (Exception err)
            {
               transStatus= err.Message;
            }
            return transStatus;
        }

        public List<ApplyNowModel> GetCustPaymentMethod(string CustID, string PMID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string CustNum = CustID;
            List<ApplyNowModel> pmmodel = new List<ApplyNowModel>();
            try
            {
                usaepay.PaymentMethod[] Method = client.getCustomerPaymentMethods(token, CustNum);

               

                foreach(var pm in Method)
                {
                    ApplyNowModel mm = new ApplyNowModel();
                    mm.PAID =Convert.ToInt32(pm.MethodID);
                   
                    mm.Name_On_Card = pm.MethodName;
                    mm.CardYear = pm.CardExpiration;
                    mm.RoutingNumber = pm.Routing;
                    if(pm.MethodType=="ACH")
                    {
                        mm.CardNumber = pm.Routing;
                    }
                    else
                    {
                        mm.CardNumber = pm.CardNumber;
                    }
                    pmmodel.Add(mm);
                }
               //transStatus= string.Concat(Method[0],Method[1]);
            }

            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return pmmodel;
        }
        public string GetCustomerList(string CustID, string PMID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string CustNum = CustID;

            try
            {
                usaepay.PaymentMethod[] Method = client.getCustomerPaymentMethods(token, CustNum);
                transStatus = string.Concat(Method.Length);
            }

            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return transStatus;
        }

        public string RefundTrans(string RefNum,decimal Amount)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            usaepay.ueSoapServerPortTypeClient client = new usaepay.ueSoapServerPortTypeClient();
            usaepay.ueSecurityToken token = new usaepay.ueSecurityToken();

            token.SourceKey = sourceKeySendBox;
            token.ClientIP = "11.22.33.44";  // IP address of end user (if applicable)
            string pin = pinSendBox;   // pin assigned to source

            usaepay.ueHash hash = new usaepay.ueHash();
            hash.Type = "md5";  // Type of encryption 
            hash.Seed = Guid.NewGuid().ToString();  // unique encryption seed

            string prehashvalue = string.Concat(token.SourceKey, hash.Seed, pin);  // combine data into single string
            hash.HashValue = GenerateHash(prehashvalue); // generate hash

            token.PinHash = hash;   // add hash value to token
            string refnum = RefNum;
            double amount =Convert.ToDouble(Amount);

            usaepay.TransactionResponse response = new usaepay.TransactionResponse();

            try
            {
                response = client.refundTransaction(token, refnum, amount);

                if (response.ResultCode == "A")
                {
                  transStatus= string.Concat("Transaction Approved, RefNum: ",response.RefNum);
                }
                else
                {
                    transStatus= string.Concat("Transaction Failed: ",response.Error);
                }
            }

            catch (Exception err)
            {
                transStatus = err.Message;
            }
            return transStatus;
        }

    }
}