using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models.Club
{
    public class ClubMappingModel
    {
        public long Id { get; set; }
        public long ClubId { get; set; }
        public long UserId { get; set; }

        public ClubMappingModel GetClubMappingByClubId(long ClubId, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            return db.tbl_ClubMapping.Where(a => a.ClubId == ClubId && a.UserId == UserId).Select(a => new ClubMappingModel()
            {
                Id = a.Id,
                ClubId = a.ClubId,
                UserId = a.UserId

            }).FirstOrDefault();
        }


        public bool RemoveMappingByClubIdandUserId(long ClubId, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                var Data = db.tbl_ClubMapping.Where(a => a.ClubId == ClubId && a.UserId == UserId).FirstOrDefault();
                if (Data != null)
                {
                    if(db.tbl_Club.Where(a=>a.UserId== UserId && a.Id== ClubId).Count()>0)
                    {
                        int Value = db.Database.ExecuteSqlCommand("delete from tbl_ClubMapping where ClubId = " + ClubId + " and UserId=" + UserId + "");
                        int CLubValue = db.Database.ExecuteSqlCommand("delete from tbl_Club where Id = " + ClubId + " and UserId=" + UserId + "");
                    }
                    else
                    {
                        db.tbl_ClubMapping.Remove(Data);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var CreateClubMap = new tbl_ClubMapping()
                    {
                        Id = 0,
                        ClubId = ClubId,
                        UserId = UserId
                    };
                    db.tbl_ClubMapping.Add(CreateClubMap);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            
        }

    }
}