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
    
    public partial class tbl_AmenityReservation
    {
        public long ARID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<long> AmenityID { get; set; }
        public Nullable<System.DateTime> DesiredDate { get; set; }
        public string DesiredTime { get; set; }
        public Nullable<long> DurationID { get; set; }
        public string DepositFee { get; set; }
        public string ReservationFee { get; set; }
        public Nullable<int> Status { get; set; }
        public string Duration { get; set; }
        public string DesiredTimeFrom { get; set; }
        public string DesiredTimeTo { get; set; }
        public Nullable<long> Guest { get; set; }
    }
}
