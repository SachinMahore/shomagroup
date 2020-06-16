using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ShomaRM.Models
{
    public class UsaePayModel
    {
        public string sourceKeySendBox = WebConfigurationManager.AppSettings["USEPaySourceKeySandBox"];
        public string sourceKeyLive = WebConfigurationManager.AppSettings["USEPaySourceKeyLive"];
        public bool useSandBox = Convert.ToBoolean(WebConfigurationManager.AppSettings["USEPayUseSandbox"]);
        public string ChargeCard(ApplyNowModel model)
        {
            string transStatus = "";
            USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
            usaepay.SourceKey = (useSandBox == true ? sourceKeySendBox : sourceKeyLive);
            usaepay.Amount = Convert.ToDecimal(model.Charge_Amount ?? 0) + Convert.ToDecimal(model.ProcessingFees ?? 0);
            usaepay.Description = model.Description;
            usaepay.CardHolder = model.Name_On_Card;
            usaepay.CardNumber =model.CardNumber;
            usaepay.CardExp = model.CardMonth.ToString()+model.CardYear.ToString();
            usaepay.CustEmail = model.Email;
            usaepay.UseSandbox = useSandBox;

            try
            {
                usaepay.Sale();
                if (usaepay.ResultCode == "A")
                {
                    transStatus = "Transaction approved\n" +
                        "Auth Code: " + usaepay.AuthCode + "\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "AVS: " + usaepay.AvsResult + "\n" +
                        "CVV: " + usaepay.Cvv2Result;
                }
                else if (usaepay.ResultCode == "D")
                {
                    transStatus = "Transaction Declined\n" +
                        "Ref Num: " + usaepay.ResultRefNum;
                }
                else
                {
                    transStatus = "Transaction Error\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "Error: " + usaepay.ErrorMesg + "\n" +
                        "Error Code: " + usaepay.ErrorCode;
                }
            }
            catch (Exception x)
            {
                transStatus = "ERROR: " + x.Message;
            }
            return transStatus+"|"+usaepay.AuthCode+"|"+usaepay.ResultRefNum;
        }
        public string RefundCharge(string TransID,decimal Credit_Amount)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
            usaepay.SourceKey = (useSandBox == true ? sourceKeySendBox : sourceKeyLive);
            usaepay.UseSandbox = useSandBox;

            usaepay.Amount =Credit_Amount;
            string RefNo = TransID;
            try
            {
                usaepay.Refund(RefNo);
                if (usaepay.ResultCode == "A")
                {
                    transStatus = "Transaction approved\n" +
                        "Auth Code: " + usaepay.AuthCode + "\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "AVS: " + usaepay.AvsResult + "\n" +
                        "CVV: " + usaepay.Cvv2Result;
                }
                else if (usaepay.ResultCode == "D")
                {
                    transStatus = "Transaction Declined\n" +
                        "Ref Num: " + usaepay.ResultRefNum;
                }
                else
                {
                    transStatus = "Transaction Error\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "Error: " + usaepay.ErrorMesg + "\n" +
                        "Error Code: " + usaepay.ErrorCode;
                }
            }
            catch (Exception x)
            {
                transStatus = "ERROR: " + x.Message;
            }
            return transStatus + "|" + usaepay.AuthCode + "|" + usaepay.ResultRefNum;
        }
        public string VoidCharge(string TransID, decimal Credit_Amount)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string transStatus = "";
            USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
            usaepay.SourceKey = (useSandBox == true ? sourceKeySendBox : sourceKeyLive);
            usaepay.UseSandbox = useSandBox;

            usaepay.Amount = Credit_Amount;
            string RefNo = TransID;
            try
            {
                usaepay.Void(RefNo);
                if (usaepay.ResultCode == "A")
                {
                    transStatus = "Transaction approved\n" +
                        "Auth Code: " + usaepay.AuthCode + "\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "AVS: " + usaepay.AvsResult + "\n" +
                        "CVV: " + usaepay.Cvv2Result;
                }
                else if (usaepay.ResultCode == "D")
                {
                    transStatus = "Transaction Declined\n" +
                        "Ref Num: " + usaepay.ResultRefNum;
                }
                else
                {
                    transStatus = "Transaction Error\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "Error: " + usaepay.ErrorMesg + "\n" +
                        "Error Code: " + usaepay.ErrorCode;
                }
            }
            catch (Exception x)
            {
                transStatus = "ERROR: " + x.Message;
            }
            return transStatus + "|" + usaepay.AuthCode + "|" + usaepay.ResultRefNum;
        }
        public string ChargeACH(ApplyNowModel model)
        {
            string transStatus = "";
            USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
            usaepay.SourceKey = (useSandBox == true ? sourceKeySendBox : sourceKeyLive);
            usaepay.Pin = model.CCVNumber==null ? "" : model.CCVNumber.ToString();
            usaepay.Amount = Convert.ToDecimal(model.Charge_Amount ?? 0) + Convert.ToDecimal(model.ProcessingFees ?? 0);
            usaepay.Description = model.Description;
            usaepay.CardHolder = model.Name_On_Card;
            
            usaepay.CheckAccount = model.AccountNumber;
            usaepay.CheckRouting = model.RoutingNumber;
            usaepay.CustEmail = model.Email;
            usaepay.CustReceipt = true;
            usaepay.Command = "check";
            usaepay.UseSandbox = useSandBox;

            try
            {
                usaepay.Process();
                if (usaepay.ResultCode == "A")
                {
                    transStatus = "Transaction approved\n" +
                        "Auth Code: " + usaepay.AuthCode + "\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "AVS: " + usaepay.AvsResult + "\n" +
                        "CVV: " + usaepay.Cvv2Result;
                }
                else if (usaepay.ResultCode == "D")
                {
                    transStatus = "Transaction Declined\n" +
                        "Ref Num: " + usaepay.ResultRefNum;
                }
                else
                {
                    transStatus = "Transaction Error\n" +
                        "Ref Num: " + usaepay.ResultRefNum + "\n" +
                        "Error: " + usaepay.ErrorMesg + "\n" +
                        "Error Code: " + usaepay.ErrorCode;
                }
            }
            catch (Exception x)
            {
                transStatus = "ERROR: " + x.Message;
            }
            return transStatus + "|" + usaepay.AuthCode + "|" + usaepay.ResultRefNum;
        }

        //public string SaveAccountInfo(string RefNo, decimal Credit_Amount)
        //{
        //    ShomaRMEntities db = new ShomaRMEntities();
        //    string transStatus = "";
        //    USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
        //    usaepay.SourceKey = (useSandBox == true ? sourceKeySendBox : sourceKeyLive);
        //    usaepay.UseSandbox = useSandBox;

        //    usaepay.usaepayService client = new usaepayService();

        //    usaepay.Amount = Credit_Amount;
           
        //    try
        //    {
                
        //        usaepay.FieldValue[] update = new usaepay.FieldValue[2];
        //        for (int i = 0; i < 2; i++)
        //        {

        //            update[i] = new usaepay.FieldValue();

        //        }

        //        update[0].Field = "Schedule"; update[0].Value = "weekly";
        //        update[1].Field = "NumLeft"; update[1].Value = "7";

        //        string response;

        //        try
        //        {
        //            response = client.convertTranToCust(token, refnum, update);

        //            MessageBox.Show(string.Concat("Customer Number: ",
        //                        response));

        //        }


        //        catch (Exception err)
        //        {
        //            MessageBox.Show(err.Message);
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //        transStatus = "ERROR: " + x.Message;
        //    }
        //    return transStatus + "|" + usaepay.AuthCode + "|" + usaepay.ResultRefNum;
        //}
        //private static usaepay.usaepayService getClient()
        //{
        //    USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
        //    usaepay.SourceKey = (useSandBox == true ? sourceKeySendBox : sourceKeyLive);
        //    usaepay.UseSandbox = useSandBox;
        //    usaepay. client = new usaepay.usaepayService();

        //    return client;

        //}
    }
}