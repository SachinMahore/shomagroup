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
        public int ApplyNowId { get; set; }
        public int ApplicantId { get; set; }
        public string ReportType { get; set; }
        public string SSN { get; set; }
        public string TransactionNumber { get; set; }
        public string ReportDate { get; set; }
        public string ApplicantDecision { get; set; }
        public string ApplicationDecision { get; set; }
        public string ApplicantScore { get; set; }
        public string TenantName { get; set; }
        public string Notes { get; set; }
       
        public string SaveBackgroundScreening(BackgroundScreeningModel model)
        {
            string msg = "";
            //ShomaRMEntities db = new ShomaRMEntities();
            //try{
            //    var saveSMS = new tbl_BackgroundScreening()
            //    {
            //        TenantId = model.TenantId,
            //        OrderID = model.OrderID,
            //        Status = model.Status,
            //        PDFUrl = model.PDFUrl,
            //        Type = model.Type

            //    };
            //    db.tbl_BackgroundScreening.Add(saveSMS);
            //    db.SaveChanges();
            //}
            //catch (Exception e) { }
            //msg = "Data has been Save Successfully";


            //db.Dispose();
            return msg;
        }
    }
}