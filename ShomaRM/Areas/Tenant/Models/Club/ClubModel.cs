using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models.Club
{
    public class ClubModel
    {
        public long Id { get; set; }
        public string ClubTitle { get; set; }
        public long ActivityId { get; set; }

        public DateTime StartDate { get; set; }
        public string Venue { get; set; }
        public long DayId { get; set; }
        public string Time { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneCheck { get; set; }
        public bool EmailCheck { get; set; }
        public long LevelId { get; set; }
        public string SpecialInstruction { get; set; }
        public string Description { get; set; }
        public string BriefDescription { get; set; }
        public bool TermsAndCondition { get; set; }
        public long? TenantID { get; set; }
        public long UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        //for Mapping Status do Not Add In Table
        public bool ClubJoinStatus { get; set; }
        public long SearchId { get; set; }

        public string SaveclubEvent(ClubModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (db.tbl_Club.Where(a=>a.ClubTitle== model.ClubTitle)==null)
            {
                var ClubCreate = new tbl_Club()
                {
                    Id = model.Id,
                    ClubTitle = model.ClubTitle,
                    ActivityId = model.ActivityId,
                    StartDate = model.StartDate,
                    Venue = model.Venue,
                    DayId = model.DayId,
                    Time = model.Time,
                    Contact = model.Contact,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PhoneCheck = model.PhoneCheck,
                    EmailCheck = model.EmailCheck,
                    LevelId = model.LevelId,
                    SpecialInstruction = model.SpecialInstruction,
                    Description = model.Description,
                    BriefDescription = model.BriefDescription,
                    TermsAndCondition = model.TermsAndCondition,
                    TenantID = model.TenantID,
                    UserId = model.UserId,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow
                };
                db.tbl_Club.Add(ClubCreate);
                db.SaveChanges();
                var CreateClubMap = new tbl_ClubMapping()
                {
                    Id = 0,
                    ClubId = ClubCreate.Id,
                    UserId = model.UserId
                };
                db.tbl_ClubMapping.Add(CreateClubMap);
                db.SaveChanges();
                msg = "Progress Saved";
            }
            else
            {
                msg = "You Already Registered For This Event";
            }
            db.Dispose();
            return msg;
        }

        public List<ClubModel> GetClubList(long UserId=0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var Clublist = db.tbl_Club.ToList();
            if (UserId != 0)
            {
                Clublist = Clublist.Where(a => a.UserId == UserId).ToList();
            }
           return Clublist.Select(a=>new ClubModel()
           {
               Id = a.Id,
               ClubTitle = a.ClubTitle,
               ActivityId = a.ActivityId,
               StartDate = a.StartDate,
               Venue = a.Venue,
               DayId = a.DayId,
               Time = a.Time,
               Contact = a.Contact,
               Email = a.Email,
               PhoneNumber = a.PhoneNumber,
               PhoneCheck = a.PhoneCheck,
               EmailCheck = a.EmailCheck,
               LevelId = a.LevelId,
               SpecialInstruction = a.SpecialInstruction,
               Description = a.Description,
               BriefDescription = a.BriefDescription,
               TermsAndCondition = a.TermsAndCondition,
               TenantID = a.TenantID,
               UserId = a.UserId,
               IsDeleted = false,
               CreatedDate = DateTime.UtcNow,
               LastUpdatedDate = DateTime.UtcNow

           }).ToList();

        }

        public ClubModel GetClubbyId(long ClubId,long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            return db.tbl_Club.ToList().Where(a => a.Id == ClubId).Select(a => new ClubModel()
            {
                Id = a.Id,
                ClubTitle = a.ClubTitle,
                ActivityId = a.ActivityId,
                StartDate = a.StartDate,
                Venue = a.Venue,
                DayId = a.DayId,
                Time = a.Time,
                Contact = a.Contact,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                PhoneCheck = a.PhoneCheck,
                EmailCheck = a.EmailCheck,
                LevelId = a.LevelId,
                SpecialInstruction = a.SpecialInstruction,
                Description = a.Description,
                BriefDescription = a.BriefDescription,
                TermsAndCondition = a.TermsAndCondition,
                TenantID = a.TenantID,
                UserId = a.UserId,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                ClubJoinStatus = (db.tbl_ClubMapping.Where(b => b.UserId == a.UserId) == null ? false : true)
            }).FirstOrDefault();
          

        }

    }
}