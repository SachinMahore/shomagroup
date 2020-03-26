using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.Bluemoon
{
    public class LeaseResponseModel
    {
        public LeaseResponseModel()
        {
            leasePdfWithEsignatures = new List<byte[]>();
            EsigneResidents = new List<KeyModel>();
        }
        public string SessionId { get; set; }

        public string EsignatureId { get; set; }
        public string LeaseId { get; set; }

        public bool Success { get; set; }

        public byte[] leasePdf { get; set; }

        public List<byte[]> leasePdfWithEsignatures { get; set; }

        public List<KeyModel> EsigneResidents { get; set; }
    }

    public class KeyModel
    {
        public string Key { get; set; }
        public string Email { get; set; }
        public string DateSigned { get; set; }
    }

    public class AuthenticationResponseModel
    {
        public bool AuthenticateUserResult { get; set; }
    }
}