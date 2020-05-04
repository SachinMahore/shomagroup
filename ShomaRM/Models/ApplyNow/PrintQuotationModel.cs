using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models
{
    public class PrintQuotationModel
    {
        public string TenantID { get; set; }
        public string QuoteDate { get; set; }
        public string ApplicantName { get; set; }
        public string QuoteExpires { get; set; }
        public string PhoneNumber    { get; set; }
        public string Email { get; set; }
        public string DesiredMoveIn { get; set; }
        public string UnitNo { get; set; }
        public string ModelName { get; set; }
        public string LeaseTerm { get; set; }
        public string AssignParkingSpace { get; set; }
        public string ApplicationFees { get; set; }
        public string SecurityDeposit { get; set; }
        public string GuarantorFees { get; set; }
        public string PetNonRefundableFee { get; set; }
        public string AdministratorFee { get; set; }
        public string PetDNAFee { get; set; }
        public string VehicleRegistration { get; set; }
        public string MonthlyRent { get; set; }
        public string ProratedMonthlyRent { get; set; }
        public string TrashFee { get; set; }
        public string ProratedTrashFee { get; set; }
        public string PestControlFee { get; set; }
        public string ProratedPestControlFee { get; set; }
        public string ConvergentBillingFee { get; set; }
        public string ProratedConvergentBillingFee { get; set; }
        public string AdditionalParking { get; set; }
        public string ProratedAdditionalParking { get; set; }
        public string StorageAmount { get; set; }
        public string ProratedStorageAmount { get; set; }
        public string PetFee { get; set; }
        public string ProratedPetFee { get; set; }
        public string MonthlyCharges { get; set; }
        public string ProratedMonthlyCharges { get; set; }
    }
}