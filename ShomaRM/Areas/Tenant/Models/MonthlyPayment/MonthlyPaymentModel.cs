using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Areas.Tenant.Models
{
    public class MonthlyPaymentModel
    {
        public long MPayID { get; set; }
        public Nullable<long> ApplyNowID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<decimal> MonthlyCharges { get; set; }
        public Nullable<decimal> AdditionalParking { get; set; }
        public Nullable<decimal> StorageCharges { get; set; }
        public Nullable<decimal> PetRent { get; set; }
        public Nullable<decimal> TrashRecycle { get; set; }
        public Nullable<decimal> PestControl { get; set; }
        public Nullable<decimal> ConvergentBilling { get; set; }
        public Nullable<decimal> TotalMonthlyCharges { get; set; }
        public Nullable<decimal> Prorated_Rent { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public Nullable<decimal> AdministrationFee { get; set; }
        public Nullable<decimal> VehicleRegistration { get; set; }

        public string Unit { get; set; }
        public string Model { get; set; }
        public string LeaseTerm { get; set; }

        public string SaveMonthlyPayment(MonthlyPaymentModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var checkMonthlyPayAlreadyExist = db.tbl_MonthlyPayment.Where(co => co.UserID == model.UserID).FirstOrDefault();
            if (checkMonthlyPayAlreadyExist == null)
            {
                var applyNow = db.tbl_ApplyNow.Where(co => co.UserId == model.UserID).FirstOrDefault();
                if (applyNow != null)
                {
                    var saveMonthlyPayment = new tbl_MonthlyPayment()
                    {
                        TenantID = applyNow.ID,
                        UserID = model.UserID,
                        MonthlyCharges = model.MonthlyCharges,
                        AdditionalParking = model.AdditionalParking,
                        StorageCharges = model.StorageCharges,
                        PetRent = model.PetRent,
                        TrashRecycle = model.TrashRecycle,
                        PestControl = model.PestControl,
                        ConvergentBilling = model.ConvergentBilling,
                        TotalMonthlyCharges = model.TotalMonthlyCharges,
                        Unit = model.Unit,
                        Model = model.Model,
                        LeaseTerm = model.LeaseTerm,
                    };
                    db.tbl_MonthlyPayment.Add(saveMonthlyPayment);
                    db.SaveChanges();

                    var saveMonthlyTransaction = new tbl_Transaction()
                    {

                        TenantID =Convert.ToInt64(model.UserID),
                       
                        PAID = "5",
                        Transaction_Date = DateTime.Now,
                       
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.TotalMonthlyCharges,
                        Description = "Monthly Payment",
                        Charge_Date = DateTime.Now,
                        Charge_Type = 3,
                       
                        Authcode="",
                        Charge_Amount = 0,
                        
                        Miscellaneous_Amount = 0,
                        Accounting_Date = DateTime.Now,
                      
                        Batch = "1",
                       
                        CreatedBy = Convert.ToInt32(model.UserID),
                       UserID = Convert.ToInt32(model.UserID),

                    };
                    db.tbl_Transaction.Add(saveMonthlyTransaction);
                    db.SaveChanges();

                    var saveMoveinPayment = new tbl_MoveInPayment()
                    {
                        TenantID = applyNow.ID,
                        UserID = model.UserID,
                        MoveInCharges = model.MoveInCharges,
                        SecurityDeposit = applyNow.Deposit,
                        PetFee = applyNow.PetDeposit,
                        Prorated_Rent = model.Prorated_Rent,
                        AdministrationFee = model.AdministrationFee,
                        VehicleRegistration = model.VehicleRegistration,
                     
                    };
                    db.tbl_MoveInPayment.Add(saveMoveinPayment);
                    db.SaveChanges();

                    var saveMoveInTransaction = new tbl_Transaction()
                    {

                        TenantID = Convert.ToInt64(model.UserID),
                   
                        PAID = "5",
                        Transaction_Date = DateTime.Now,
                      
                        CreatedDate = DateTime.Now,
                        Credit_Amount = model.MoveInCharges,
                        Description = "Monthly Payment",
                        Charge_Date = DateTime.Now,
                        Charge_Type = 2,
                      
                        Authcode="",
                        Charge_Amount = 0,

                        Miscellaneous_Amount = 0,
                        Accounting_Date = DateTime.Now,            
                        Batch = "1",                  
                        CreatedBy = Convert.ToInt32(model.UserID),             
                        UserID = Convert.ToInt32(model.UserID),
                    };
                    db.tbl_Transaction.Add(saveMoveInTransaction);
                    db.SaveChanges();
                }
            }
            else
            {
                var uApplyNow = db.tbl_ApplyNow.Where(co => co.UserId == model.UserID).FirstOrDefault();
                if (uApplyNow != null)
                {
                    var updateMonthlyPayment = db.tbl_MonthlyPayment.Where(co => co.UserID == model.UserID).FirstOrDefault();
                    if (updateMonthlyPayment != null)
                    {
                        updateMonthlyPayment.TenantID = uApplyNow.ID;
                        updateMonthlyPayment.UserID = model.UserID;
                        updateMonthlyPayment.MonthlyCharges = model.MonthlyCharges;
                        updateMonthlyPayment.AdditionalParking = model.AdditionalParking;
                        updateMonthlyPayment.StorageCharges = model.StorageCharges;
                        updateMonthlyPayment.PetRent = model.PetRent;
                        updateMonthlyPayment.TrashRecycle = model.TrashRecycle;
                        updateMonthlyPayment.PestControl = model.PestControl;
                        updateMonthlyPayment.ConvergentBilling = model.ConvergentBilling;
                        updateMonthlyPayment.TotalMonthlyCharges = model.TotalMonthlyCharges;
                        updateMonthlyPayment.Unit = model.Unit;
                        updateMonthlyPayment.Model = model.Model;
                        updateMonthlyPayment.LeaseTerm = model.LeaseTerm;
                    }
                    db.SaveChanges();

                    var updateMoveinPayment = db.tbl_MoveInPayment.Where(co => co.UserID == model.UserID).FirstOrDefault();
                    var saveMoveinPayment = new tbl_MoveInPayment()
                    {                       
                        MoveInCharges = model.MoveInCharges,
                        //SecurityDeposit = applyNow.Deposit,
                        //PetFee = applyNow.PetDeposit,
                        Prorated_Rent = model.Prorated_Rent,
                        AdministrationFee = model.AdministrationFee,
                        VehicleRegistration = model.VehicleRegistration,

                    };                   
                    db.SaveChanges();
                }
            }
            
            db.Dispose();
            return msg;
        }

        public MonthlyPaymentModel GetMonthlyPayment(long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            MonthlyPaymentModel model = new MonthlyPaymentModel();

            var getMonthlyPayment = db.tbl_MonthlyPayment.Where(co => co.UserID == UserId).FirstOrDefault();
            if (getMonthlyPayment!=null)
            {
                model.MPayID = getMonthlyPayment.MPayID;
                model.ApplyNowID = getMonthlyPayment.TenantID;
                model.UserID = getMonthlyPayment.UserID;
                model.MonthlyCharges = getMonthlyPayment.MonthlyCharges;
                model.AdditionalParking = getMonthlyPayment.AdditionalParking;
                model.StorageCharges = getMonthlyPayment.StorageCharges;
                model.PetRent = getMonthlyPayment.PetRent;
                model.TrashRecycle = getMonthlyPayment.TrashRecycle;
                model.PestControl = getMonthlyPayment.PestControl;
                model.ConvergentBilling = getMonthlyPayment.ConvergentBilling;
                model.TotalMonthlyCharges = getMonthlyPayment.TotalMonthlyCharges;
            }
            return model;
        }
    }
    public partial class MoveInPaymentModel
    {
        public long MIPayID { get; set; }
        public Nullable<long> ApplyNowID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public Nullable<decimal> AdministrationFee { get; set; }
        public Nullable<decimal> VehicleRegistration { get; set; }
        public Nullable<decimal> SecurityDeposit { get; set; }
        public Nullable<decimal> PetFee { get; set; }
        public Nullable<decimal> Prorated_Rent { get; set; }
    }
}