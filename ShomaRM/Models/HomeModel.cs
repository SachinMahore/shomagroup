using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;

namespace ShomaRM.Models
{
    public class HomeModel
    {
        public int LTID { get; set; }
        public string LeaseTerms { get; set; }
        public List<HomeModel> leaseTerms { get; set; }

        public HomeModel GetLeaseTerms()
        {
           HomeModel model = new HomeModel();
            List<HomeModel> listLeaseTerms = new List<HomeModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var leaseTerms = db.tbl_LeaseTerms.OrderBy(co => co.LeaseTerms ?? 0).ToList();
            foreach (var lt in leaseTerms)
            {
                listLeaseTerms.Add(new HomeModel() { LTID = lt.LTID, LeaseTerms = ((lt.LeaseTerms??0)!=0? (lt.LeaseTerms??0).ToString() : "") });
            }
            if(listLeaseTerms.Count()==0)
            {
                listLeaseTerms.Add(new HomeModel() { LTID = 0, LeaseTerms = "No Lease Term" });
            }
            model.leaseTerms = listLeaseTerms;
            return model;
        }
    }
}