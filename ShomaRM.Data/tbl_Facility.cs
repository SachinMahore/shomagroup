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
    
    public partial class tbl_Facility
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Facility()
        {
            this.tbl_FacilityBooking = new HashSet<tbl_FacilityBooking>();
        }
    
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_FacilityBooking> tbl_FacilityBooking { get; set; }
        public virtual tbl_Properties tbl_Properties { get; set; }
        public virtual tbl_Login tbl_Login { get; set; }
    }
}
