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
    
    public partial class tbl_TenantFob
    {
        public long TFobID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string FobID { get; set; }
        public Nullable<long> ApplicantID { get; set; }
        public Nullable<int> Status { get; set; }
        public string OtherFirstName { get; set; }
        public string OtherLastName { get; set; }
        public string OtherRelationship { get; set; }
        public Nullable<long> OtherId { get; set; }
    }
}
