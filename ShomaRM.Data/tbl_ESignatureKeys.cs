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
    
    public partial class tbl_ESignatureKeys
    {
        public long ESID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<long> ApplicantID { get; set; }
        public string Key { get; set; }
        public Nullable<long> EsignatureId { get; set; }
        public string DateSigned { get; set; }
    }
}