using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models
{
    public class BackgroundScreeningModel
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Type { get; set; }
        public int OrderID { get; set; }
        public string Status { get; set; }
        public string PDFUrl { get; set; }
    }
}