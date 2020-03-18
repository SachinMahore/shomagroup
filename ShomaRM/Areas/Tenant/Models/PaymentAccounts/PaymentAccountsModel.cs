using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Models;

namespace ShomaRM.Areas.Tenant.Models
{
    public class PaymentAccountsModel
    {
        public long PAID { get; set; }
        public Nullable<long> TenantId { get; set; }
        public string NickName { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public Nullable<int> CardType { get; set; }
        public string CardTypeString { get; set; }
        public int? Default { get; set; }
        public string DefaultString { get; set; }
        public Nullable<int> PayMethod { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }

        public bool IsLessThanSevenDays { get; set; }

        public string SaveUpdatePaymentsAccounts(PaymentAccountsModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            string encrytpedCardNumber = model.CardNumber == null? "" : new EncryptDecrypt().EncryptText(model.CardNumber);
            string encrytpedAccountNumber = model.AccountNumber== null ? "" : new EncryptDecrypt().EncryptText(model.AccountNumber);
            string encrytpedCardMonth = new EncryptDecrypt().EncryptText(model.Month);
            string encrytpedCardYear = new EncryptDecrypt().EncryptText(model.Year);
            string encrytpedRoutingNumber = model.RoutingNumber == null? "" : new EncryptDecrypt().EncryptText(model.RoutingNumber);

            if (model.PAID == 0)
            {
                var savePaymentAccounts = new tbl_PaymentAccounts()
                {
                    TenantId = model.TenantId,
                    NameOnCard = model.NameOnCard,
                    CardNumber = encrytpedCardNumber,
                    Month = encrytpedCardMonth,
                    Year = encrytpedCardYear,
                    CardType = model.CardType,
                    PayMethod=model.PayMethod,
                    BankName = model.BankName,
                    AccountName=model.AccountName,
                    AccountNumber= encrytpedAccountNumber,
                    RoutingNumber= encrytpedRoutingNumber
                };
                db.tbl_PaymentAccounts.Add(savePaymentAccounts);
                db.SaveChanges();
                msg = "Payment Account Saved Successfully";
            }
            else
            {
                var updatePaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == model.PAID).FirstOrDefault();
                if (updatePaymentAccounts != null)
                {
                    updatePaymentAccounts.TenantId = model.TenantId;
                    if (model.PayMethod == 1)
                    {
                        updatePaymentAccounts.AccountName = model.AccountName;
                        updatePaymentAccounts.NameOnCard = model.NameOnCard;
                        updatePaymentAccounts.CardNumber = encrytpedCardNumber;
                        updatePaymentAccounts.Month = encrytpedCardMonth;
                        updatePaymentAccounts.Year = encrytpedCardYear;
                        updatePaymentAccounts.CardType = model.CardType;
                        updatePaymentAccounts.PayMethod = model.PayMethod;

                        updatePaymentAccounts.BankName = "";
                        updatePaymentAccounts.AccountNumber = "";
                        updatePaymentAccounts.RoutingNumber = "";
                    }
                    else if (model.PayMethod == 2)
                    {
                        updatePaymentAccounts.NameOnCard = "";
                        updatePaymentAccounts.CardNumber = "";
                        updatePaymentAccounts.Month = "0";
                        updatePaymentAccounts.Year = "0";
                        updatePaymentAccounts.CardType = 0;

                        updatePaymentAccounts.PayMethod = model.PayMethod;
                        updatePaymentAccounts.BankName = model.BankName;
                        updatePaymentAccounts.AccountName = model.AccountName;
                        updatePaymentAccounts.AccountNumber = encrytpedAccountNumber;
                        updatePaymentAccounts.RoutingNumber = encrytpedRoutingNumber;
                    }
                    db.SaveChanges();
                }
                msg = "Progress Update Successfully";
            }
            db.Dispose();
            return msg;
        }

        public List<PaymentAccountsModel> GetPaymentsAccountsList(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PaymentAccountsModel> listPaymentAccounts = new List<PaymentAccountsModel>();
            string dt = DateTime.Now.ToString("dd/MM/yyyy");
            int noOfDaysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var day = dt.Split('/');
            int remainingDay = noOfDaysInCurrentMonth - Convert.ToInt32(day[0]);

            var getPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.TenantId == TenantId && co.PayMethod == 1).ToList();

            if (getPaymentAccounts != null)
            {
                foreach (var item in getPaymentAccounts)
                {
                    string decryptedCardNumber = new EncryptDecrypt().DecryptText(item.CardNumber);
                    string decrytpedCardMonth = new EncryptDecrypt().DecryptText(item.Month);
                    string decrytpedCardYear = new EncryptDecrypt().DecryptText(item.Year);


                    listPaymentAccounts.Add(new PaymentAccountsModel()
                    {
                        PAID = item.PAID,
                        TenantId = item.TenantId,
                        AccountName = item.AccountName,
                        NameOnCard = item.NameOnCard,
                        CardNumber = decryptedCardNumber,
                        Month = decrytpedCardMonth,
                        Year = decrytpedCardYear,
                        CardType = item.CardType,
                        CardTypeString = item.CardType == 1 ? "Visa" : item.CardType == 2 ? "MasterCard" : "",
                        Default = item.Default,
                        DefaultString = item.Default == 0 ? "Not a Default" : item.Default == 1 ? "Default" : "",
                        IsLessThanSevenDays = remainingDay <= 7 ? true : false,
                    });
                }

            }
            return listPaymentAccounts;
        }

        public List<PaymentAccountsModel> GetPaymentsBankAccountsList(long TenantId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PaymentAccountsModel> listPaymentBankAccounts = new List<PaymentAccountsModel>();
            string dt = DateTime.Now.ToString("dd/MM/yyyy");
            int noOfDaysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var day = dt.Split('/');
            int remainingDay = noOfDaysInCurrentMonth - Convert.ToInt32(day[0]);
            var getPaymentBankAccounts = db.tbl_PaymentAccounts.Where(co => co.TenantId == TenantId && co.PayMethod == 2).ToList();

            if (getPaymentBankAccounts != null)
            {
                foreach (var item in getPaymentBankAccounts)
                {
                    string decryptedAccountNumber = new EncryptDecrypt().DecryptText(item.AccountNumber);
                    string decrytpedRoutingNumber = new EncryptDecrypt().DecryptText(item.RoutingNumber);


                    listPaymentBankAccounts.Add(new PaymentAccountsModel()
                    {
                        PAID = item.PAID,
                        TenantId = item.TenantId,
                        PayMethod = item.PayMethod,
                        BankName = item.BankName,
                        AccountName = item.AccountName,
                        AccountNumber = decryptedAccountNumber,
                        RoutingNumber = decrytpedRoutingNumber,
                        Default = item.Default,
                        DefaultString = item.Default == 0 ? "Not a Default" : item.Default == 1 ? "Default" : "",
                        IsLessThanSevenDays = remainingDay <= 7 ? true : false,
                    });
                }

            }
            return listPaymentBankAccounts;
        }


        public PaymentAccountsModel EditPaymentsAccounts(long PAID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PaymentAccountsModel model = new PaymentAccountsModel();
            var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == PAID).FirstOrDefault();

            string decryptedCardNumber = editPaymentAccounts.CardNumber== null ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.CardNumber);
            string decryptedAccountNumber = editPaymentAccounts.AccountNumber == "" ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.AccountNumber);
            string decrytpedCardMonth = new EncryptDecrypt().DecryptText(editPaymentAccounts.Month);
            string decrytpedCardYear = new EncryptDecrypt().DecryptText(editPaymentAccounts.Year);
            string decrytpedRoutingNumber = editPaymentAccounts.RoutingNumber == "" ? "" : new EncryptDecrypt().DecryptText(editPaymentAccounts.RoutingNumber);

            if (editPaymentAccounts != null)
            {
                model.PAID = editPaymentAccounts.PAID;
                model.TenantId = editPaymentAccounts.TenantId;
                model.AccountName = editPaymentAccounts.AccountName;
                model.NameOnCard = editPaymentAccounts.NameOnCard;
                model.CardNumber = decryptedCardNumber;
                model.Month = decrytpedCardMonth;
                model.Year = decrytpedCardYear;
                model.CardType = editPaymentAccounts.CardType;
                model.PayMethod = editPaymentAccounts.PayMethod;
                model.BankName = editPaymentAccounts.BankName;
                model.AccountNumber = decryptedAccountNumber;
                model.RoutingNumber = decrytpedRoutingNumber;
            }
            return model;
        }

        public PaymentAccountsModel EditPaymentsBankAccounts(long PAID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PaymentAccountsModel model = new PaymentAccountsModel();
            var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == PAID).FirstOrDefault();

            string encrytpedAccountNumber = new EncryptDecrypt().EncryptText(editPaymentAccounts.AccountNumber);
            string encrytpedRoutingNumber = new EncryptDecrypt().EncryptText(editPaymentAccounts.RoutingNumber);

            string decryptedAccountNumber = new EncryptDecrypt().DecryptText(encrytpedAccountNumber);
            string decrytpedRoutingNumber = new EncryptDecrypt().DecryptText(encrytpedRoutingNumber);

            if (editPaymentAccounts != null)
            {
                model.PAID = editPaymentAccounts.PAID;
                model.TenantId = editPaymentAccounts.TenantId;
                model.AccountName = editPaymentAccounts.AccountName;
                model.BankName = editPaymentAccounts.BankName;
                model.AccountNumber = decryptedAccountNumber;
                model.RoutingNumber = decrytpedRoutingNumber;
                model.PayMethod = editPaymentAccounts.PayMethod;
            }
            return model;
        }

        public string DeletePaymentsAccounts(long PAID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            var deletePaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == PAID).FirstOrDefault();

            if (deletePaymentAccounts != null)
            {
                db.tbl_PaymentAccounts.Remove(deletePaymentAccounts);
                db.SaveChanges();
                msg = "Record Deleted Successfully";
            }
            db.Dispose();
            return msg;
        }

        public string MakeDefaultPaymentSystem(long TenantId, long PAID)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var paymnets = db.tbl_PaymentAccounts.Where(co => co.TenantId == TenantId).ToList();
            if (paymnets!=null)
            {
                foreach (var item in paymnets)
                {
                    item.Default = 0;
                    db.SaveChanges();
                }

                var makeDefaultPaymentSystems = db.tbl_PaymentAccounts.Where(co => co.PAID == PAID).FirstOrDefault();
                if (makeDefaultPaymentSystems!=null)
                {
                    makeDefaultPaymentSystems.Default = 1;

                    db.SaveChanges();
                    msg = makeDefaultPaymentSystems.NickName + " Selected as a Default Payment System";
                }
                
            }
            db.Dispose();
            return msg;
        }

        public List<PaymentAccountsModel> GetPaymentMethods(long TenantId)
        {
            List<PaymentAccountsModel> listPaymentAccount = new List<PaymentAccountsModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var paymentMethod = db.tbl_PaymentAccounts.Where(co => co.TenantId == TenantId).ToList();
            if (paymentMethod!=null)
            {
                foreach (var item in paymentMethod)
                {
                    listPaymentAccount.Add(new PaymentAccountsModel()
                    {
                        PAID = item.PAID,
                        AccountName = item.AccountName,
                        Default = item.Default,
                        PayMethod = item.PayMethod
                    });
                }
            }
            return listPaymentAccount;
        }
    }
}