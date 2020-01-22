using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Net;
using System.Data;
using System.Data.Common;
using System.IO;
using ShomaRM.Areas.Tenant.Models;


namespace ShomaRM.Areas.Tenant.Models
{
    public class RatingModel
    {
        public long RID { get; set; }
        public Nullable<long> TenantId { get; set; }
        public Nullable<int> Rating { get; set; }


        public RatingModel GetRatings(long Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            RatingModel model = new RatingModel();

            var getRatings = db.tbl_Rating.Where(p => p.TenantId == Id).FirstOrDefault();

            if (getRatings != null)
            {
                model.TenantId = getRatings.TenantId;
                model.Rating = getRatings.Rating;
            }
            model.TenantId = TenantId;
            return model;
        }

        public string SaveRatings(RatingModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.TenantId != 0)
            {
                var saveRatings = new tbl_Rating()
                {
                    TenantId = model.TenantId,
                    Rating = model.Rating
                };
                db.tbl_Rating.Add(saveRatings);
                db.SaveChanges();

                msg = "Ratings Saved Successfully";
            }
            db.Dispose();
            return msg;


        }
    }
}