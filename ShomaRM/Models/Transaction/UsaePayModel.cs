﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShomaRM.Models
{
    public class UsaePayModel
    {
        public string ChargeCard(ApplyNowModel model)
        {
            string transStatus = "";
            USAePayAPI.USAePay usaepay = new USAePayAPI.USAePay();
            usaepay.SourceKey = "_y8h5x1TGONQjE491cj9mb8bRdA57u32";
            usaepay.Pin = model.CCVNumber.ToString();
            usaepay.Amount =Convert.ToDecimal(model.Charge_Amount);
            usaepay.Description = model.Description;
            usaepay.CardHolder = model.Name_On_Card;
            usaepay.CardNumber = model.CardNumber;
            usaepay.CardExp = model.CardMonth.ToString()+model.CardYear.ToString();
          
            usaepay.UseSandbox = true;


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
            return transStatus+"|"+usaepay.AuthCode;
        }
    }
}