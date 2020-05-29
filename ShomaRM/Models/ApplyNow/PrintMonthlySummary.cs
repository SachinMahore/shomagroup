using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models
{
    public class PrintMonthlySummary
    {
        public string ModelImage { get; set; }
        public string ModelName { get; set; }
        public string UnitNo { get; set; }
        public string LeaseTerm { get; set; }
        public string MoveInDate { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string SqFt { get; set; }
        public string Occupancy { get; set; }
        public string Deposit { get; set; }
        public string BaseRent { get; set; }
        public string Premium { get; set; }
        public string Promotion { get; set; }
        public string Subtotal { get; set; }
        public string PestControl { get; set; }
        public string TrashRecycle { get; set; }
        public string ConvergentBillingFee { get; set; }
        public string AdditionalSubtotal { get; set; }
        public string TotalMonthlyCharges { get; set; }
    }
}