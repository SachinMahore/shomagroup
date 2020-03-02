using ShomaRM.Data;
using ShomaRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        //for Mapping Status do Not Add In Table
        public bool ClubJoinStatus { get; set; }
        public long SearchId { get; set; }
        [NotMapped]
        public string StringStartDate { get; set; }

        public ResponseModel SaveclubEvent(ClubModel model)
        {
            ResponseModel _respnse = new ResponseModel();
            ShomaRMEntities db = new ShomaRMEntities();

            if (db.tbl_Club.Where(a => a.ClubTitle.ToLower() == model.ClubTitle.ToLower()).ToList().Count() == 0)
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
                    Active=true,
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
                _respnse.Status = true;
                _respnse.msg = "Saved Successfully..";
            }
            else
            {
                _respnse.Status = false;
                _respnse.msg = "You Already Registered For This Event";
            }
            db.Dispose();
            return _respnse;
        }

        public ResponseModel EditclubEvent(ClubModel model)
        {
            ResponseModel _respnse = new ResponseModel();
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                if (db.tbl_Club.Where(a => a.Id == model.Id).FirstOrDefault().ClubTitle.ToLower() == model.ClubTitle.ToLower())
                {
                    var club = db.tbl_Club.Where(a => a.Id == model.Id).FirstOrDefault();

                    club.Id = model.Id;
                    club.ClubTitle = model.ClubTitle;
                    club.ActivityId = model.ActivityId;
                    club.StartDate = model.StartDate;
                    club.Venue = model.Venue;
                    club.DayId = model.DayId;
                    club.Time = model.Time;
                    club.Contact = model.Contact;
                    club.Email = model.Email;
                    club.PhoneNumber = model.PhoneNumber;
                    club.PhoneCheck = model.PhoneCheck;
                    club.EmailCheck = model.EmailCheck;
                    club.LevelId = model.LevelId;
                    club.SpecialInstruction = model.SpecialInstruction;
                    club.Description = model.Description;
                    club.BriefDescription = model.BriefDescription;
                    club.TermsAndCondition = model.TermsAndCondition;
                    club.TenantID = model.TenantID;
                    club.UserId = model.UserId;
                    club.CreatedDate = DateTime.UtcNow;
                    club.LastUpdatedDate = DateTime.UtcNow;
                   
                    db.Entry(club).State = EntityState.Modified; 
                    db.SaveChanges();
                   
                    _respnse.Status = true;
                    _respnse.msg = "Update Successfully..";
                }
                else
                {
                    _respnse.Status = false;
                    _respnse.msg = "Club Not Found..";
                }
            }
            catch(Exception ex)
            {
                _respnse.Status = false;
                _respnse.msg = ex.ToString();
            }
            db.Dispose();
            return _respnse;
        }

        public List<ClubModel> GetClubList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var Clublist = db.tbl_Club.ToList();
            if (UserId != 0)
            {
                Clublist = Clublist.ToList();
            }
            return Clublist.Select(a => new ClubModel()
            {
                Id = a.Id,
                ClubTitle = a.ClubTitle,
                ActivityId = a.ActivityId,
                StartDate =a.StartDate.ToLocalTime(),
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
                IsDeleted = a.IsDeleted,
                Active=a.Active,
                CreatedDate =a.CreatedDate,
                LastUpdatedDate = a.LastUpdatedDate

            }).ToList();

        }

        public List<ClubModel> GetJoiningClubList(long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var listitem= db.tbl_Club.SqlQuery("Select * FROM tbl_club where (SELECT count(*) FROM tbl_ClubMapping where tbl_ClubMapping.ClubId = tbl_club.Id and tbl_ClubMapping.UserId = "+ UserId + ")< 1").ToList<tbl_Club>().Select(a=>new ClubModel
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
                IsDeleted = a.IsDeleted,
                Active=a.Active,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow

            }).ToList();


            return listitem;
        }

        public List<ClubModel> GetJoinClubAndMyClubList(long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var DataList = db.tbl_Club.SqlQuery("Select * FROM tbl_club where (SELECT count(*) FROM tbl_ClubMapping where tbl_ClubMapping.ClubId = tbl_club.Id and tbl_ClubMapping.UserId = " + UserId + ")= 1 AND (tbl_club.Active='1') ").ToList<tbl_Club>().Select(data=> new ClubModel()
                            {
                                Id = data.Id,
                                ClubTitle = data.ClubTitle,
                                ActivityId = data.ActivityId,
                                StartDate = data.StartDate,
                                Venue = data.Venue,
                                DayId = data.DayId,
                                Time = data.Time,
                                Contact = data.Contact,
                                Email = data.Email,
                                PhoneNumber = data.PhoneNumber,
                                PhoneCheck = data.PhoneCheck,
                                EmailCheck = data.EmailCheck,
                                LevelId = data.LevelId,
                                SpecialInstruction = data.SpecialInstruction,
                                Description = data.Description,
                                BriefDescription = data.BriefDescription,
                                TermsAndCondition = data.TermsAndCondition,
                                TenantID = data.TenantID,
                                UserId = data.UserId,
                                IsDeleted = data.IsDeleted,
                                Active=data.Active,
                                CreatedDate = DateTime.UtcNow,
                                LastUpdatedDate = DateTime.UtcNow

                            }).ToList();

            var InactiveList = db.tbl_Club.Where(a => a.Active == false && a.UserId==UserId).Select(data=>new ClubModel() {
                Id = data.Id,
                ClubTitle = data.ClubTitle,
                ActivityId = data.ActivityId,
                StartDate = data.StartDate,
                Venue = data.Venue,
                DayId = data.DayId,
                Time = data.Time,
                Contact = data.Contact,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                PhoneCheck = data.PhoneCheck,
                EmailCheck = data.EmailCheck,
                LevelId = data.LevelId,
                SpecialInstruction = data.SpecialInstruction,
                Description = data.Description,
                BriefDescription = data.BriefDescription,
                TermsAndCondition = data.TermsAndCondition,
                TenantID = data.TenantID,
                UserId = data.UserId,
                IsDeleted = data.IsDeleted,
                Active = data.Active,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow
            }).ToList();
            foreach(var item in InactiveList)
            {
                DataList.Add(item);
            }
            

            return DataList;
        }

        public ClubModel GetClubbyId(long ClubId, long UserId)
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
                IsDeleted = a.IsDeleted,
                Active=a.Active,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                ClubJoinStatus = (db.tbl_ClubMapping.Where(b => b.UserId == a.UserId) == null ? false : true)
            }).FirstOrDefault();


        }

        public ClubMappingModel GetClubJoinStatusId(long ClubId, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
             var Data=db.tbl_ClubMapping.ToList().Where(a => a.ClubId == ClubId && a.UserId == UserId).Select(a => new ClubMappingModel()
            {
                Id = a.Id,
                ClubId = a.ClubId,
                UserId = a.UserId

            }).FirstOrDefault();

            return Data;
        }

        public bool UpdateClubActiveDeactive(long Id,bool Active)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var data = db.tbl_Club.Where(a => a.Id == Id).FirstOrDefault();
            try
            {
                data.Active = Active;
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}