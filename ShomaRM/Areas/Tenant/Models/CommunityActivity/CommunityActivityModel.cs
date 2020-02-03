using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.IO;

namespace ShomaRM.Areas.Tenant.Models
{
    public class CommunityActivityModel
    {
        public long CID { get; set; }
        public Nullable<long> TenantId { get; set; }
        public string Details { get; set; }
        public string AttatchFile { get; set; }
        public string AttachFileOriginalName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string DateString { get; set; }
        public string TenantName { get; set; }
        public string ProfilePicture { get; set; }
        public string OrginalProfilePicture { get; set; }

        public string SaveUpdateCommunityPost(CommunityActivityModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.CID == 0)
            {
                var saveCommunityActivity = new tbl_CommunityActivity();
                {
                    saveCommunityActivity.TenantId = model.TenantId;
                    saveCommunityActivity.Details = model.Details;
                    saveCommunityActivity.AttatchFile = model.AttatchFile;
                    saveCommunityActivity.Date = DateTime.Now;
                    saveCommunityActivity.AttachFileOriginalName = model.AttachFileOriginalName;
                };

                db.tbl_CommunityActivity.Add(saveCommunityActivity);
                db.SaveChanges();
                msg = "Posted Successfully";
            }
            else
            {
                var updateCommunityActivity = db.tbl_CommunityActivity.Where(co => co.CID == model.CID).FirstOrDefault();

                if (updateCommunityActivity!=null)
                {
                    updateCommunityActivity.TenantId = model.TenantId;
                    updateCommunityActivity.Details = model.Details;
                    updateCommunityActivity.AttatchFile = model.AttatchFile;
                    updateCommunityActivity.Date = model.Date;
                    updateCommunityActivity.AttachFileOriginalName = model.AttachFileOriginalName;

                    db.SaveChanges();
                }
            }
            db.Dispose();
            return msg;
        }

        public CommunityActivityModel SaveUploadAttachFileCommunityActivity(HttpPostedFileBase fileBaseUpload, CommunityActivityModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            CommunityActivityModel _CommunityActivityModel = new CommunityActivityModel();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUpload != null && fileBaseUpload.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/CommunityPostFiles/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUpload.FileName;
                Extension = Path.GetExtension(fileBaseUpload.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload.FileName);
                fileBaseUpload.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUpload.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/CommunityPostFiles/") + "/" + sysFileName;

                }
                _CommunityActivityModel.AttatchFile = sysFileName;
                _CommunityActivityModel.AttachFileOriginalName = fileName;
            }

            return _CommunityActivityModel;
        }

        public List<CommunityActivityModel> GetCommunityActivityList(/*CommunityActivityModel model*/)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CommunityActivityModel> list = new List<CommunityActivityModel>();
            var getCommunityActivity = db.tbl_CommunityActivity.OrderByDescending(co=>co.Date).ToList();
            if (getCommunityActivity != null)
            {
               
                    foreach (var item in getCommunityActivity)
                    {
                    //    var tenantName = db.tbl_ApplyNow.Where(co => co.UserId == item.TenantId).FirstOrDefault();
                    //if (tenantName != null)
                    //{
                    var tenantProfile = db.tbl_TenantInfo.Where(co => co.TenantID == item.TenantId).FirstOrDefault();
                    if (tenantProfile != null)
                    {
                        list.Add(new CommunityActivityModel()
                            {
                                CID = item.CID,
                                TenantId = item.TenantId,
                                Details = item.Details,
                                AttatchFile = item.AttatchFile,
                                AttachFileOriginalName = item.AttachFileOriginalName,
                                DateString = item.Date.Value.ToString("MMMM dd"),
                                TenantName = tenantProfile.FirstName + " " + tenantProfile.LastName,
                                ProfilePicture = tenantProfile.ProfilePicture,
                                OrginalProfilePicture = tenantProfile.OrginalProfilePicture
                            });
                      }
                    //}
                }
            }
            db.Dispose();
            return list;
        }

        public List<CommunityActivityModel> GetCommunityActivityAdminList(CommunityActivityModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<CommunityActivityModel> list = new List<CommunityActivityModel>();
            var getCommunityActivity = db.tbl_CommunityActivity.Where(co => co.TenantId == model.TenantId).ToList();
            if (getCommunityActivity != null)
            {
                foreach (var item in getCommunityActivity)
                {
                        var tenantProfile = db.tbl_TenantInfo.Where(co => co.TenantID == item.TenantId).FirstOrDefault();
                        if (tenantProfile != null)
                        {
                            list.Add(new CommunityActivityModel()
                            {
                                CID = item.CID,
                                TenantId = item.TenantId,
                                Details = item.Details,
                                AttatchFile = item.AttatchFile,
                                AttachFileOriginalName = item.AttachFileOriginalName,
                                DateString = item.Date.Value.ToString("MM/dd/yyyy"),
                                TenantName = tenantProfile.FirstName + " " + tenantProfile.LastName,
                                ProfilePicture = tenantProfile.ProfilePicture,
                                OrginalProfilePicture = tenantProfile.OrginalProfilePicture
                            });
                        }
                }
            }
            db.Dispose();
            return list;
        }

        public void DeleteCommunityActivityPost(CommunityActivityModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var CommunityActivityPost = db.tbl_CommunityActivity.Where(p => p.CID == model.CID).FirstOrDefault();
            if (CommunityActivityPost != null)
            {
                db.tbl_CommunityActivity.Remove(CommunityActivityPost);
                db.SaveChanges();
            }

        }


    }
}