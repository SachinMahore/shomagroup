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
    
    public partial class usp_GetAssigmentAuditList_Result
    {
        public int AHID { get; set; }
        public string TableName { get; set; }
        public string EventName { get; set; }
        public int EventRows { get; set; }
        public System.DateTime EventDate { get; set; }
        public string UserName { get; set; }
        public int AHDID { get; set; }
        public int AHID1 { get; set; }
        public string PKColumnValue { get; set; }
        public string FKColumnValue { get; set; }
        public string AuditDetails { get; set; }
    }
}