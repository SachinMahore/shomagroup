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
    
    public partial class tbl_ServiceRequest
    {
        public long ServiceID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<int> ProblemCategory { get; set; }
        public string Details { get; set; }
        public Nullable<int> PermissionEnterApartment { get; set; }
        public Nullable<System.DateTime> PermissionComeDate { get; set; }
        public Nullable<int> PetInforChange { get; set; }
        public Nullable<int> AlarmCodeChange { get; set; }
        public string Notes { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Location { get; set; }
        public Nullable<int> ItemCaussing { get; set; }
        public Nullable<int> ItemIssue { get; set; }
        public string OtherItemCaussing { get; set; }
        public string OtherItemIssue { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public string OriginalServiceFile { get; set; }
        public string TempServiceFile { get; set; }
        public string EmergencyMobile { get; set; }
        public string CompletedPicture { get; set; }
        public string TempCompletedPicture { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<int> ServicePerson { get; set; }
        public string PermissionComeTime { get; set; }
        public string ClosingNotes { get; set; }
        public Nullable<int> UrgentStatus { get; set; }

        public Nullable<System.DateTime> ClosingDate { get; set; }

    }
}
