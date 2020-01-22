using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models
{
    public class GalleryModel
    {
        public long GID { get; set; }
        public string PhotoPath { get; set; }
        public Nullable<System.DateTime> UploadedDate { get; set; }
        public Nullable<long> PID { get; set; }
        public string ToolTip { get; set; }
    }
}