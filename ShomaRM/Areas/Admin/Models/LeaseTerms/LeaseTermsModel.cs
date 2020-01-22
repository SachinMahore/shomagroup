using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class LeaseTermsModel
    {
        public int LTID { get; set; }
        public Nullable<int> LeaseTerms { get; set; }
        public Nullable<bool> FormAgent { get; set; }
        public Nullable<int> OfferTerms { get; set; }
        public string FormAgentString { get; set; }

        public LeaseTermsModel SaveUpdateLeaseTerms(LeaseTermsModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.LTID == 0)
            {
                var SaveLeaseTerms = new tbl_LeaseTerms()
                {
                    LeaseTerms = model.LeaseTerms,
                    FormAgent = model.FormAgent,
                    OfferTerms = model.OfferTerms == null ? 0 : model.OfferTerms
                };

                db.tbl_LeaseTerms.Add(SaveLeaseTerms);
                db.SaveChanges();
            }
            else
            {
                var UpdateLeaseTerms = db.tbl_LeaseTerms.Where(co => co.LTID == model.LTID).FirstOrDefault();
                if (UpdateLeaseTerms != null)
                {
                    UpdateLeaseTerms.LeaseTerms = model.LeaseTerms;
                    UpdateLeaseTerms.FormAgent = model.FormAgent;
                    UpdateLeaseTerms.OfferTerms = model.OfferTerms == null ? 00 : model.OfferTerms;

                    db.SaveChanges();
                }
            }
            db.Dispose();

            return model;
        }

        public List<LeaseTermsModel> GetLeaseTermsList()
        {
            List<LeaseTermsModel> listLeaseTerms = new List<LeaseTermsModel>();
            ShomaRMEntities db = new ShomaRMEntities();

            var LeaseTermsList = db.tbl_LeaseTerms.ToList();
            foreach (var item in LeaseTermsList)
            {
                listLeaseTerms.Add(new LeaseTermsModel()
                {
                    LTID = item.LTID,
                    LeaseTerms = item.LeaseTerms,
                    FormAgent = item.FormAgent,
                    OfferTerms = item.OfferTerms,
                    FormAgentString = item.FormAgent == true ? "Yes" : "False"
                });
            }

            return listLeaseTerms;
        }

        public LeaseTermsModel GetLeaseTermsDetails(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            LeaseTermsModel model = new LeaseTermsModel();

            var LeaseTermsDetails = db.tbl_LeaseTerms.Where(co => co.LTID == Id).FirstOrDefault();

            if (LeaseTermsDetails != null)
            {
                model.LTID = LeaseTermsDetails.LTID;
                model.LeaseTerms = LeaseTermsDetails.LeaseTerms;
                model.FormAgent = LeaseTermsDetails.FormAgent;
                model.OfferTerms = LeaseTermsDetails.OfferTerms;
            }
            return model;
        }
        public void DeleteLeaseTermsDetails(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var LeaseTermsDetails = db.tbl_LeaseTerms.Where(co => co.LTID == Id).FirstOrDefault();

            if (LeaseTermsDetails != null)
            {
                db.tbl_LeaseTerms.Remove(LeaseTermsDetails);
                db.SaveChanges();
            }
        }
    }
}