using ShomaRM.Data;
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
        public string TenantName { get; set; }
        public string Notes { get; set; }
        public int OrderID { get; set; }
        public string Status { get; set; }
        public string PDFUrl { get; set; }
        public string SaveBackgroundScreening(BackgroundScreeningModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            try{
                var saveSMS = new tbl_BackgroundScreening()
                {
                    TenantId = model.TenantId,
                    OrderID = model.OrderID,
                    Status = model.Status,
                    PDFUrl = model.PDFUrl,
                    Type = model.Type

                };
                db.tbl_BackgroundScreening.Add(saveSMS);
                db.SaveChanges();
            }
            catch (Exception e) { }
            msg = "Data has been Save Successfully";


            db.Dispose();
            return msg;
        }
    }
}