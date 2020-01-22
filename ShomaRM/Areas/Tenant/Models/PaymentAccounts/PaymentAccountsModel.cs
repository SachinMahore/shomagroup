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

        public string SaveUpdatePaymentsAccounts(PaymentAccountsModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            
            if (model.PAID == 0)
            {
                var savePaymentAccounts = new tbl_PaymentAccounts()
                {
                    TenantId = model.TenantId,
                    NickName = model.NickName,
                    NameOnCard = model.NameOnCard,
                    CardNumber = model.CardNumber,
                    Month = model.Month,
                    Year = model.Year,
                    CardType = model.CardType
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
                    updatePaymentAccounts.NickName = model.NickName;
                    updatePaymentAccounts.NameOnCard = model.NameOnCard;
                    updatePaymentAccounts.CardNumber = model.CardNumber;
                    updatePaymentAccounts.Month = model.Month;
                    updatePaymentAccounts.Year = model.Year;
                    updatePaymentAccounts.CardType = model.CardType;
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
            var getPaymentAccounts = db.tbl_PaymentAccounts.Where(co => co.TenantId == TenantId).ToList();

            if (getPaymentAccounts!=null)
            {
                foreach (var item in getPaymentAccounts)
                {
                    listPaymentAccounts.Add(new PaymentAccountsModel()
                    {
                        PAID = item.PAID,
                        TenantId = item.TenantId,
                        NickName = item.NickName,
                        NameOnCard = item.NameOnCard,
                        CardNumber = item.CardNumber,
                        Month = item.Month,
                        Year = item.Year,
                        CardType = item.CardType,
                        CardTypeString = item.CardType == 1 ? "Visa" : item.CardType == 2 ? "MasterCard" : "",
                        Default = item.Default,
                        DefaultString = item.Default == 0 ? "Not a Default" : item.Default == 1 ? "Default" : ""
                    });
                }
                
            }
            return listPaymentAccounts;
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
                model.NickName = editPaymentAccounts.NickName;
                model.NameOnCard = editPaymentAccounts.NameOnCard;
                model.CardNumber = editPaymentAccounts.CardNumber;
                model.Month = editPaymentAccounts.Month;
                model.Year = editPaymentAccounts.Year;
                model.CardType = editPaymentAccounts.CardType;
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
    }
}