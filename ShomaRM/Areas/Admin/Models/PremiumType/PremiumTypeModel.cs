using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Admin.Models
{
    public class PremiumTypeModel
    {
        public int PTID { get; set; }
        public string PremiumType { get; set; }
        public string Details { get; set; }

        public PremiumTypeModel SaveUpdatePremiumType(PremiumTypeModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PTID == 0)
            {
                var SavePremiumType = new tbl_PremiumType()
                {
                    PremiumType = model.PremiumType,
                    Details = model.Details
                };
                db.tbl_PremiumType.Add(SavePremiumType);
                db.SaveChanges();
            }
            else
            {
                var UpdatePremiumType = db.tbl_PremiumType.Where(co => co.PTID == model.PTID).FirstOrDefault();
                if (UpdatePremiumType != null)
                {
                    UpdatePremiumType.PremiumType = model.PremiumType;
                    UpdatePremiumType.Details = model.Details;

                    db.SaveChanges();
                }
            }
            db.Dispose();

            return model;
        }

        public List<PremiumTypeModel> GetPremiumTypeList(string SearchText)
        {
            List<PremiumTypeModel> listPremiumType = new List<PremiumTypeModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            if (SearchText.Trim() == string.Empty)
            {
                var PremiumTypeList = db.tbl_PremiumType.ToList();
                foreach (var item in PremiumTypeList)
                {
                    listPremiumType.Add(new PremiumTypeModel()
                    {
                        PTID = item.PTID,
                        PremiumType = item.PremiumType,
                        Details = item.Details,
                    });
                }
            }
            else
            {
                var PremiumTypeList = db.tbl_PremiumType.Where(co => co.PremiumType.Contains(SearchText)).ToList();
                foreach (var item in PremiumTypeList)
                {

                    listPremiumType.Add(new PremiumTypeModel()
                    {
                        PTID = item.PTID,
                        PremiumType = item.PremiumType,
                        Details = item.Details,
                    });
                }
            }

            return listPremiumType;
        }

        public PremiumTypeModel GetPremiumTypeDetails(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PremiumTypeModel model = new PremiumTypeModel();

            var PremiumTypeDetails = db.tbl_PremiumType.Where(co => co.PTID == Id).FirstOrDefault();

            if (PremiumTypeDetails != null)
            {
                model.PTID = PremiumTypeDetails.PTID;
                model.PremiumType = PremiumTypeDetails.PremiumType;
                model.Details = PremiumTypeDetails.Details;
            }
            return model;
        }
        public void DeletePremiumTypeDetails(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var PremiumTypeDetails = db.tbl_PremiumType.Where(co => co.PTID == Id).FirstOrDefault();

            if (PremiumTypeDetails != null)
            {
                db.tbl_PremiumType.Remove(PremiumTypeDetails);
                db.SaveChanges();
            }
        }
    }
}