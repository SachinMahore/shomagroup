using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.Corelogic
{
    public class LeaseTermsModel
    {
        public string MonthlyRent { get; set; }
        public string LeaseMonths { get; set; }
        public string SecurityDeposit { get; set; } 
    }
    public class Applicant
    {
        public string CurrentRent { get; set; }
        public string ConsentObtained { get; set; }
        public string EmploymentGrossIncome { get; set; }
        public string ApplicantIdentifier { get; set; }
        public string ApplicantType { get; set; }
        public string Birthdate { get; set; }

        public string SocSecNumber { get; set; }
        public string CustomerID { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string UnparsedAddress { get; set; }
       
    }
}
