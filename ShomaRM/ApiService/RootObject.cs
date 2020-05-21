using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.ApiService
{
    public class RootObject
    {
        public string content { get; set; }
        public List<object> Value { get; set; }
        public object id { get; set; }
    }
}