using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models.Bluemoon
{
    public class EsignatureParty
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool IsOwner { get; set; }
    }
}