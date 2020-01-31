using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;

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
            
            if (model.PAID == 0)
            {
                var savePaymentAccounts = new tbl_PaymentAccounts()
                {
                    TenantId = model.TenantId,
                    NameOnCard = model.NameOnCard,
                    CardNumber = model.CardNumber,
                    Month = model.Month,
                    Year = model.Year,
                    CardType = model.CardType,
                    PayMethod=model.PayMethod,
                    BankName = model.BankName,
                    AccountName=model.AccountName,
                    AccountNumber=model.AccountNumber,
                    RoutingNumber=model.RoutingNumber
                };
                db.tbl_PaymentAccounts.Add(savePaymentAccounts);
                db.SaveChanges();
                msg = "Progress Saved Successfully";
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
                        updatePaymentAccounts.CardNumber = model.CardNumber;
                        updatePaymentAccounts.Month = model.Month;
                        updatePaymentAccounts.Year = model.Year;
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
                        updatePaymentAccounts.Month = "";
                        updatePaymentAccounts.Year = "";
                        updatePaymentAccounts.CardType = 0;

                        updatePaymentAccounts.PayMethod = model.PayMethod;
                        updatePaymentAccounts.BankName = model.BankName;
                        updatePaymentAccounts.AccountName = model.AccountName;
                        updatePaymentAccounts.AccountNumber = model.AccountNumber;
                        updatePaymentAccounts.RoutingNumber = model.RoutingNumber;
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
                    listPaymentAccounts.Add(new PaymentAccountsModel()
                    {
                        PAID = item.PAID,
                        TenantId = item.TenantId,
                        AccountName = item.AccountName,
                        NameOnCard = item.NameOnCard,
                        CardNumber = item.CardNumber,
                        Month = item.Month,
                        Year = item.Year,
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
                    listPaymentBankAccounts.Add(new PaymentAccountsModel()
                    {
                        PAID = item.PAID,
                        TenantId = item.TenantId,
                        PayMethod = item.PayMethod,
                        BankName = item.BankName,
                        AccountName = item.AccountName,
                        AccountNumber = item.AccountNumber,
                        RoutingNumber = item.RoutingNumber,
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

            if (editPaymentAccounts != null)
            {
                model.PAID = editPaymentAccounts.PAID;
                model.TenantId = editPaymentAccounts.TenantId;
                model.AccountName = editPaymentAccounts.AccountName;
                model.NameOnCard = editPaymentAccounts.NameOnCard;
                model.CardNumber = editPaymentAccounts.CardNumber;
                model.Month = editPaymentAccounts.Month;
                model.Year = editPaymentAccounts.Year;
                model.CardType = editPaymentAccounts.CardType;
                model.PayMethod = editPaymentAccounts.PayMethod;
                model.BankName = editPaymentAccounts.BankName;
                model.AccountNumber = editPaymentAccounts.AccountNumber;
                model.RoutingNumber = editPaymentAccounts.RoutingNumber;
            }
            return model;
        }

        public PaymentAccountsModel EditPaymentsBankAccounts(long PAID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PaymentAccountsModel model = new PaymentAccountsModel();
            var editPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.PAID == PAID).FirstOrDefault();

            if (editPaymentAccounts != null)
            {
                model.PAID = editPaymentAccounts.PAID;
                model.TenantId = editPaymentAccounts.TenantId;
                model.AccountName = editPaymentAccounts.AccountName;
                model.BankName = editPaymentAccounts.BankName;
                model.AccountNumber = editPaymentAccounts.AccountNumber;
                model.RoutingNumber = editPaymentAccounts.RoutingNumber;
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