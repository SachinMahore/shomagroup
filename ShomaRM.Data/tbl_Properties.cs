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
    
    public partial class tbl_Properties
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Properties()
        {
            this.tbl_CashReceipts = new HashSet<tbl_CashReceipts>();
            this.tbl_CashReceipts1 = new HashSet<tbl_CashReceipts>();
            this.tbl_Event = new HashSet<tbl_Event>();
            this.tbl_Facility = new HashSet<tbl_Facility>();
            this.tbl_FOB = new HashSet<tbl_FOB>();
            this.tbl_Gallery = new HashSet<tbl_Gallery>();
            this.tbl_Lease = new HashSet<tbl_Lease>();
            this.tbl_Notice = new HashSet<tbl_Notice>();
            this.tbl_Parking = new HashSet<tbl_Parking>();
            this.tbl_PetPlace = new HashSet<tbl_PetPlace>();
            this.tbl_Promotion = new HashSet<tbl_Promotion>();
            this.tbl_PropertyFloor = new HashSet<tbl_PropertyFloor>();
            this.tbl_PropertyUnits = new HashSet<tbl_PropertyUnits>();
            this.tbl_Prospect = new HashSet<tbl_Prospect>();
            this.tbl_PurchaseOrder = new HashSet<tbl_PurchaseOrder>();
            this.tbl_SalesAgent = new HashSet<tbl_SalesAgent>();
            this.tbl_Storage = new HashSet<tbl_Storage>();
            this.tbl_Tenant = new HashSet<tbl_Tenant>();
            this.tbl_Tenant1 = new HashSet<tbl_Tenant>();
            this.tbl_WorkOrder = new HashSet<tbl_WorkOrder>();
        }
    
        public long PID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> Status { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string LocationGoogle { get; set; }
        public Nullable<int> Garages { get; set; }
        public string BuiltIn { get; set; }
        public string Parking { get; set; }
        public string Waterfront { get; set; }
        public string Amenities { get; set; }
        public Nullable<int> NoOfUnits { get; set; }
        public Nullable<int> NoOfFloors { get; set; }
        public Nullable<int> AgentID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }
        public string Picture { get; set; }
        public string YouTube { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> City { get; set; }
        public Nullable<decimal> ApplicationFees { get; set; }
        public Nullable<decimal> GuarantorFees { get; set; }
        public Nullable<decimal> PestControlFees { get; set; }
        public Nullable<decimal> TrashFees { get; set; }
        public Nullable<decimal> ConversionBillFees { get; set; }
        public Nullable<decimal> AdminFees { get; set; }
        public Nullable<decimal> PetDNAAmt { get; set; }
        public Nullable<decimal> ProcessingFees { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CashReceipts> tbl_CashReceipts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CashReceipts> tbl_CashReceipts1 { get; set; }
        public virtual tbl_City tbl_City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Event> tbl_Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Facility> tbl_Facility { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_FOB> tbl_FOB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Gallery> tbl_Gallery { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Lease> tbl_Lease { get; set; }
        public virtual tbl_Login tbl_Login { get; set; }
        public virtual tbl_Login tbl_Login1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Notice> tbl_Notice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Parking> tbl_Parking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PetPlace> tbl_PetPlace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Promotion> tbl_Promotion { get; set; }
        public virtual tbl_Properties tbl_Properties1 { get; set; }
        public virtual tbl_Properties tbl_Properties2 { get; set; }
        public virtual tbl_Properties tbl_Properties11 { get; set; }
        public virtual tbl_Properties tbl_Properties3 { get; set; }
        public virtual tbl_State tbl_State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PropertyFloor> tbl_PropertyFloor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PropertyUnits> tbl_PropertyUnits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Prospect> tbl_Prospect { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PurchaseOrder> tbl_PurchaseOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SalesAgent> tbl_SalesAgent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Storage> tbl_Storage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Tenant> tbl_Tenant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Tenant> tbl_Tenant1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_WorkOrder> tbl_WorkOrder { get; set; }
    }
}
