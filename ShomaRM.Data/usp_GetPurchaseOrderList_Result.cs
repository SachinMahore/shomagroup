//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShomaRM.Data
{
    using System;
    
    public partial class usp_GetPurchaseOrderList_Result
    {
        public long POID { get; set; }
        public string PropertyID { get; set; }
        public string OrderNumber { get; set; }
        public string Vendor { get; set; }
        public string PODesc { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string Route { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> CanceledDate { get; set; }
        public Nullable<int> CanceledBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
