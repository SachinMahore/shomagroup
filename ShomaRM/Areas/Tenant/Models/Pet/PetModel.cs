using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ShomaRM.Models;


namespace ShomaRM.Areas.Tenant.Models
{
    public class PetModel
    {
        public int PetID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<int> PetType { get; set; }
        public string Breed { get; set; }
        public string Weight { get; set; }
        public string Age { get; set; }
        public string Photo { get; set; }
        public string PetVaccinationCertificate { get; set; }
        public string PetName { get; set; }
        public string VetsName { get; set; }
        public string TempPetNameFile { get; set; }
        public string TempPetVaccinationCertificateFile { get; set; }
        public string OriginalPetNameFile { get; set; }
        public string OriginalPetVaccinationCertificateFile { get; set; }
        public long? TenantPetPlace { get; set; }
        public int? PetCount { get; set; }
        public int TenantPetPlaceIDInt { get; set; }

        public string SaveUpdatePet(PetModel model)
        {

            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PetID == 0)
            {
                var savePet = new tbl_TenantPet()
                {
                    PetID = model.PetID,
                    TenantID = model.TenantID,
                    PetType = model.PetType,
                    Breed = model.Breed,
                    Weight = model.Weight,
                    Age = model.Age,
                    Photo = model.Photo,
                    PetVaccinationCert = model.PetVaccinationCertificate,
                    OriginalPhoto = model.OriginalPetNameFile,
                    OriginalVaccinationCert = model.OriginalPetVaccinationCertificateFile,
                    PetName = model.PetName,
                    VetsName = model.VetsName,
                    AddedBy = ShomaGroupWebSession.CurrentUser.UserID
                };
                db.tbl_TenantPet.Add(savePet);
                db.SaveChanges();
                msg = savePet.TenantID.ToString();

                msg += ",Pet Saved Successfully";
            }
            else
            {
                var getPetdata = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();
                if (getPetdata != null)
                {
                    getPetdata.PetID = model.PetID;
                    getPetdata.TenantID = model.TenantID;
                    getPetdata.PetType = model.PetType;
                    getPetdata.Breed = model.Breed;
                    getPetdata.Weight = model.Weight;
                    getPetdata.Age = model.Age;
                    getPetdata.Photo = model.Photo;
                    getPetdata.PetVaccinationCert = model.PetVaccinationCertificate;
                    getPetdata.OriginalPhoto = model.OriginalPetNameFile;
                    getPetdata.OriginalVaccinationCert = model.OriginalPetVaccinationCertificateFile;
                    getPetdata.PetName = model.PetName;
                    getPetdata.VetsName = model.VetsName;
                }
                db.SaveChanges();
                msg = "Pet Updated Successfully";
            }

            db.Dispose();
            return msg;


        }

        public List<PetModel> GetPetList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PetModel> lstProp = new List<PetModel>();

            //var petList = db.tbl_TenantPet.Where(p => p.TenantID == TenantID).ToList();
            var petList = db.tbl_TenantPet.Where(p => p.TenantID == TenantID && p.AddedBy == ShomaGroupWebSession.CurrentUser.UserID).ToList();
            foreach (var pl in petList)
            {
                lstProp.Add(new PetModel
                {
                    PetID = pl.PetID,
                    TenantID = pl.TenantID,
                    PetType = pl.PetType,
                    Breed = pl.Breed,
                    Weight = string.IsNullOrWhiteSpace(pl.Weight) ? "" : pl.Weight,
                    Age = string.IsNullOrWhiteSpace(pl.Age) ? "" : pl.Age,
                    Photo = pl.Photo,
                    PetVaccinationCertificate = pl.PetVaccinationCert,
                    PetName = pl.PetName,
                    VetsName = !string.IsNullOrWhiteSpace(pl.VetsName) ? pl.VetsName : ""
                });

            }
            return lstProp;
        }


        public PetModel GetPetDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PetModel model = new PetModel();

            var getPetdata = db.tbl_TenantPet.Where(p => p.PetID == id).FirstOrDefault();
            if (getPetdata != null)
            {
                model.PetID = getPetdata.PetID;
                model.PetName = getPetdata.PetName;
                model.Breed = getPetdata.Breed;
                model.Weight = getPetdata.Weight == null ? "" : getPetdata.Weight;
                model.VetsName = getPetdata.VetsName;
                model.Photo = getPetdata.Photo;
                model.PetVaccinationCertificate = getPetdata.PetVaccinationCert;
                model.OriginalPetNameFile = getPetdata.OriginalPhoto;
                model.OriginalPetVaccinationCertificateFile = getPetdata.OriginalVaccinationCert;
            }
            model.PetID = id;

            return model;
        }

        public string DeleteTenantPet(long PetID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (PetID != 0)
            {

                var petData = db.tbl_TenantPet.Where(p => p.PetID == PetID && p.AddedBy == ShomaGroupWebSession.CurrentUser.UserID).FirstOrDefault();
                if (petData != null)
                {
                    db.tbl_TenantPet.Remove(petData);
                    db.SaveChanges();
                    msg = "Pet Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public PetModel SaveUploadPetPhoto(HttpPostedFileBase fileBaseUploadPetPhoto, PetModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PetModel petModelPetPhoto = new PetModel();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUploadPetPhoto != null && fileBaseUploadPetPhoto.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/pet/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUploadPetPhoto.FileName;
                Extension = Path.GetExtension(fileBaseUploadPetPhoto.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUploadPetPhoto.FileName);
                fileBaseUploadPetPhoto.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUploadPetPhoto.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/pet/") + "/" + sysFileName;

                }
                petModelPetPhoto.TempPetNameFile = sysFileName.ToString();
                petModelPetPhoto.OriginalPetNameFile = fileName;
            }

            return petModelPetPhoto;
        }
        public PetModel SaveUploadPetVaccinationC(HttpPostedFileBase fileBaseUploadPetVaccinationCer, PetModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PetModel petModelPetVaccination = new PetModel();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUploadPetVaccinationCer != null && fileBaseUploadPetVaccinationCer.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/pet/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUploadPetVaccinationCer.FileName;
                Extension = Path.GetExtension(fileBaseUploadPetVaccinationCer.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUploadPetVaccinationCer.FileName);
                fileBaseUploadPetVaccinationCer.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUploadPetVaccinationCer.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/pet/") + "/" + sysFileName;

                }
                petModelPetVaccination.TempPetVaccinationCertificateFile = sysFileName.ToString();
                petModelPetVaccination.OriginalPetVaccinationCertificateFile = fileName;
            }

            return petModelPetVaccination;
        }

        public List<PetModel> GetProfilePetList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<PetModel> lstProp = new List<PetModel>();
            var appPet = db.tbl_TenantInfo.Where(p => p.TenantID == TenantID).FirstOrDefault();
            if (appPet != null)
            {
                var petList = db.tbl_TenantPet.Where(p => p.TenantID == appPet.ProspectID).ToList();
                if (petList != null)
                {
                    foreach (var pl in petList)
                    {
                        lstProp.Add(new PetModel
                        {
                            PetID = pl.PetID,
                            TenantID = pl.TenantID,
                            PetType = pl.PetType,
                            Breed = pl.Breed,
                            Weight = pl.Weight == null ? "" : pl.Weight,
                            Age = pl.Age == null ? "" : pl.Age,
                            Photo = pl.Photo,
                            PetVaccinationCertificate = pl.PetVaccinationCert,
                            PetName = pl.PetName,
                            VetsName = pl.VetsName
                        });

                    }
                }
            }
            return lstProp;
        }

        public string SaveUpdateTenanatPet(PetModel model, long UserId)
        {

            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var applyNow = db.tbl_ApplyNow.Where(co => co.UserId == UserId).FirstOrDefault();
            if (applyNow != null)
            {

                if (model.PetID == 0)
                {
                    var savePet = new tbl_TenantPet()
                    {
                        PetID = model.PetID,
                        TenantID = applyNow.ID,
                        PetType = model.PetType,
                        Breed = model.Breed,
                        Weight = model.Weight,
                        Age = model.Age,
                        Photo = model.Photo,
                        PetVaccinationCert = model.PetVaccinationCertificate,
                        OriginalPhoto = model.OriginalPetNameFile,
                        OriginalVaccinationCert = model.OriginalPetVaccinationCertificateFile,
                        PetName = model.PetName,
                        VetsName = model.VetsName
                    };
                    db.tbl_TenantPet.Add(savePet);
                    db.SaveChanges();
                    //msg = savePet.TenantID.ToString();

                    msg += "Pet Saved Successfully";
                }
                else
                {
                    var getPetdata = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();
                    if (getPetdata != null)
                    {
                        getPetdata.PetID = model.PetID;
                        getPetdata.TenantID = applyNow.ID;
                        getPetdata.PetType = model.PetType;
                        getPetdata.Breed = model.Breed;
                        getPetdata.Weight = model.Weight;
                        getPetdata.Age = model.Age;
                        getPetdata.Photo = model.Photo;
                        getPetdata.PetVaccinationCert = model.PetVaccinationCertificate;
                        getPetdata.OriginalPhoto = model.OriginalPetNameFile;
                        getPetdata.OriginalVaccinationCert = model.OriginalPetVaccinationCertificateFile;
                        getPetdata.PetName = model.PetName;
                        getPetdata.VetsName = model.VetsName;
                    }
                    db.SaveChanges();
                    msg = "Pet Updated Successfully";
                }
            }

            db.Dispose();
            return msg;


        }

        public PetModel CheckPetRegistration(long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            PetModel model = new PetModel();
            var getTenatId = db.tbl_ApplyNow.Where(co => co.UserId == UserId).FirstOrDefault();
            if (getTenatId!=null)
            {
                var getTenantPetInfoForReg = db.tbl_TenantPetPlace.Where(co => co.TenantID == getTenatId.ID).FirstOrDefault();
                if (getTenantPetInfoForReg!=null)
                {
                    TenantPetPlace = getTenantPetInfoForReg.PetPlaceID;
                    TenantPetPlaceIDInt = Convert.ToInt32(TenantPetPlace);
                    int getPetCount = db.tbl_TenantPet.Where(co => co.TenantID == getTenatId.ID).Count();
                    PetCount = getPetCount;
                }
            }
            return model;
        }
        public void CheckAndDeletePet(long ProspectID, int NoOfPet)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var petData = db.tbl_TenantPet.Where(co => co.TenantID == ProspectID).ToList().OrderByDescending(p=>p.PetID);
            var petDataCount = petData.Count();
            if (NoOfPet == 3)
            {
                db.tbl_TenantPet.RemoveRange(petData);
                db.SaveChanges();
            }
            else if (petDataCount > NoOfPet)
            {
                var petDelete = petData.FirstOrDefault();
                db.tbl_TenantPet.Remove(petDelete);
                db.SaveChanges();
            }
        }
    }
}