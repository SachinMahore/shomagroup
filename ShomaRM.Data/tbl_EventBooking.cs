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
    using System.Collections.Generic;
    
    public partial class tbl_EventBooking
    {
        public long EventBookingID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<long> EventID { get; set; }
        public System.DateTime BookingDate { get; set; }
        public string BookingDetails { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> NoOfGuest { get; set; }
    
        public virtual tbl_Event tbl_Event { get; set; }
        public virtual tbl_Login tbl_Login { get; set; }
        public virtual tbl_Tenant tbl_Tenant { get; set; }
    }
}
