using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.Bluemoon
{
    public class LeaseResponseModel
    {
        public string SessionId { get; set; }
        public string LeaseId { get; set; }

        public bool Success { get; set; }

        public byte[] leasePdf { get; set; }
    }

    public class AuthenticationResponseModel
    {
        public bool AuthenticateUserResult { get; set; }
    }
}