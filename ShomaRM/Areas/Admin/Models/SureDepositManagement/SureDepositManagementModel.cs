using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Areas.Admin.Models;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Data;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class SureDepositManagementModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string ApplicantStatus { get; set; }
        public string UnitNo { get; set; }
        public string Building { get; set; }
        public long? ApplyNowID { get; set; }
        public long? PropertyID { get; set; }
        public long? TenantID { get; set; }
        public long SDID { get; set; }
        public string SDNumber { get; set; }
        public List<SureDepositManagementModel> SureDepoAppList { get; set; }

        public SureDepositManagementModel GetDetails()
        {
            SureDepositManagementModel model = new SureDepositManagementModel();
            model.SureDepoAppList = GetSureDepoAppList();
            return model;
        }
        public List<SureDepositManagementModel> GetSureDepoAppList()
        {
            List<SureDepositManagementModel> listSDApplicant = new List<SureDepositManagementModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var getSDApplicantList = db.tbl_MoveInChecklist.Where(co => co.IsCheckSD == 1).OrderByDescending(co => co.CreatedDate).ToList();
            if (getSDApplicantList != null)
            {
                foreach (var item in getSDApplicantList)
                {
                    var getAppInfo = db.tbl_TenantOnline.Where(co => co.ParentTOID == item.ParentTOID).FirstOrDefault();
                    var getPropdet = db.tbl_ApplyNow.Where(co => co.ID == item.ProspectID).FirstOrDefault();
                    var getUnitNo = db.tbl_PropertyUnits.Where(co => co.UID == getPropdet.PropertyId).FirstOrDefault();
                    listSDApplicant.Add(new SureDepositManagementModel()
                    {
                        ApplyNowID = getPropdet.ID,
                        PropertyID = getPropdet.PropertyId,
                        FirstName = getAppInfo.FirstName,
                        LastName = getAppInfo.LastName,
                        FullName = getAppInfo.FirstName + " " + getAppInfo.LastName,
                        Email = getAppInfo.Email,
                        Building=getUnitNo.Building,
                        UnitNo = getUnitNo.UnitNo,
                       TenantID=getAppInfo.ParentTOID,
                    });
                }
            }
            db.Dispose();
            return listSDApplicant;
        }
        public string UploadSureDeposit(HttpPostedFileBase fileBaseUploadInsurenceDoc, long ProspectId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUploadInsurenceDoc != null && fileBaseUploadInsurenceDoc.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/SureDeposit/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = Path.GetFileNameWithoutExtension(fileBaseUploadInsurenceDoc.FileName);
                Extension = Path.GetExtension(fileBaseUploadInsurenceDoc.FileName);
                sysFileName = fileName + ProspectId + Path.GetExtension(fileBaseUploadInsurenceDoc.FileName);
                fileBaseUploadInsurenceDoc.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUploadInsurenceDoc.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/SureDeposit/") + "/" + sysFileName;

                }

                var moveInDet = db.tbl_SureDeposit.Where(p => p.ProspectID == ProspectId).FirstOrDefault();
                if (moveInDet == null)
                {
                    var saveSD = new tbl_SureDeposit()
                    {
                        ProspectID = ProspectId,
                        SDUploadName = sysFileName.ToString(),
                    };
                    db.tbl_SureDeposit.Add(saveSD);
                    db.SaveChanges();
                }
                else
                {
                    moveInDet.SDUploadName = sysFileName.ToString();
                    db.SaveChanges();
                }
                msg = "File Uploaded Successfully |" + sysFileName.ToString();
            }
            else
            {
                msg = "Something went wrong file not Uploaded";
            }
            return msg;
        }
        public string SaveUpdateSureDeposit(SureDepositManagementModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var getSDdata = db.tbl_SureDeposit.Where(p => p.ProspectID == model.ApplyNowID).FirstOrDefault();

            if (getSDdata == null)
                {
                    var saveSD = new tbl_SureDeposit()
                    {
                        SDNumber = model.SDNumber,  
                        ProspectID=model.ApplyNowID,
                        ParentTOID=model.TenantID,
                        UploadDate=DateTime.Now,
                    };
                    db.tbl_SureDeposit.Add(saveSD);
                    db.SaveChanges();
                    
                    msg = "Sure Deposit Saved Successfully";
                }
          
           else
            {
                getSDdata.ParentTOID = model.TenantID;
                getSDdata.SDNumber = model.SDNumber;
                getSDdata.UploadDate = DateTime.Now;
                db.SaveChanges();
                msg = "Sure Deposit Saved Successfully";
            }

            db.Dispose();
            return msg;
        }
    }
}