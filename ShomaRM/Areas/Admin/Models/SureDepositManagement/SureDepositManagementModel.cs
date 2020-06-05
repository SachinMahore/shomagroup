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
        public DateTime ExpirationDate { get; set; }
        public string ExpirationDateString { get; set; }
        public string StatusString { get; set; }
        public int Status { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Bond { get; set; }
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
                    var getSDdata = db.tbl_SureDeposit.Where(p => p.ProspectID == item.ProspectID).FirstOrDefault();
                    string sdnum = "";
                    string stat = "Not Uploaded";
                    string bond = "";
                    if (getSDdata != null)
                    {
                        sdnum = getSDdata.SDNumber;
                        stat = getSDdata.Status==1? "Not Uploaded" : getSDdata.Status==2? "Uploaded" : "Expired";
                        bond = getSDdata.SDUploadName;
                    }
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
                       SDNumber=sdnum,
                       StatusString=stat,
                       Bond=bond,
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
                    ProspectID = model.ApplyNowID,
                    ParentTOID = model.TenantID,
                    UploadDate = DateTime.Now,
                    ExpirationDate = model.ExpirationDate,
                    Status = model.Status,
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
                getSDdata.ExpirationDate = model.ExpirationDate;
                getSDdata.Status = model.Status;
                db.SaveChanges();
                msg = "Sure Deposit Saved Successfully";
            }

            db.Dispose();
            return msg;
        }
        public SureDepositManagementModel GetTenantSDDetails(int id, long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            SureDepositManagementModel sddet = new SureDepositManagementModel();
            var getSDdata = db.tbl_SureDeposit.Where(p => p.ProspectID == id).FirstOrDefault();
            var getAppInfo = db.tbl_TenantOnline.Where(co => co.ParentTOID ==TenantID).FirstOrDefault();

            if(getAppInfo!=null)
            {
                sddet.FirstName = getAppInfo.FirstName;
                sddet.LastName = getAppInfo.LastName;
                sddet.Email = getAppInfo.Email;
                sddet.PhoneNo = getAppInfo.Mobile;
                var getstatename = db.tbl_State.Where(f => f.ID == getAppInfo.State).FirstOrDefault();
                sddet.State = getstatename.StateName;
                sddet.City = getAppInfo.CityHome;
                sddet.Address1 = getAppInfo.HomeAddress1;
                sddet.Address2 = getAppInfo.HomeAddress2;
                sddet.Zip = getAppInfo.ZipHome;
              if(getSDdata!=null)
                {
                    sddet.SDNumber = getSDdata.SDNumber;
                    sddet.ExpirationDateString = getSDdata.ExpirationDate.HasValue ? getSDdata.ExpirationDate.Value.ToString("MM/dd/yyyy") : "";
                    sddet.Status =Convert.ToInt32(getSDdata.Status);
                }
            }
            return sddet;
        }

    }
}