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
    
    public partial class tbl_SalesAgent
    {
        public int AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> City { get; set; }
        public Nullable<long> PID { get; set; }
        public string Designation { get; set; }
        public string Photo { get; set; }
    
        public virtual tbl_City tbl_City { get; set; }
        public virtual tbl_State tbl_State { get; set; }
        public virtual tbl_Properties tbl_Properties { get; set; }
    }
}
