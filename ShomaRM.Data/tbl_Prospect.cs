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
    
    public partial class tbl_Prospect
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Prospect()
        {
            this.tbl_Document = new HashSet<tbl_Document>();
            this.tbl_Visit = new HashSet<tbl_Visit>();
        }
    
        public long PID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> City { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public Nullable<int> HavingPets { get; set; }
        public string PetsDetails { get; set; }
        public string VehicleDetails { get; set; }
        public Nullable<int> MinBedroom { get; set; }
        public Nullable<int> MinBathroom { get; set; }
        public Nullable<decimal> MinRent { get; set; }
        public Nullable<int> MaxBedroom { get; set; }
        public Nullable<int> MaxBathroom { get; set; }
        public Nullable<decimal> MaxRent { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public Nullable<int> Term { get; set; }
        public string ReasonForMovingIn { get; set; }
        public Nullable<System.DateTime> VisitDateTime { get; set; }
        public Nullable<int> LeasingAgent { get; set; }
        public string MarketSource { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<long> PropertyID { get; set; }
    
        public virtual tbl_City tbl_City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Document> tbl_Document { get; set; }
        public virtual tbl_Login tbl_Login { get; set; }
        public virtual tbl_Login tbl_Login1 { get; set; }
        public virtual tbl_Properties tbl_Properties { get; set; }
        public virtual tbl_State tbl_State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Visit> tbl_Visit { get; set; }
    }
}
