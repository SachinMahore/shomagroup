using ShomaRM.Data;
using ShomaRM.Models;
using ShomaRM.Models.Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;

namespace ShomaRM.Controllers
{
    public class CheckListController : Controller
    {
        // GET: CheckList
        public ActionResult Index()
        {
            ViewBag.UID = "0";
            ViewBag.ProcessingFees = new CheckListModel().GetProcessingFees();
            if (ShomaGroupWebSession.CurrentUser != null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }

            return View();
        }
        public ActionResult GetProspectMoveInData(long UID)
        {
            try
            {
                var tenantData = (new OnlineProspectModule().GetProspectData(UID));
                var moveinData = (new CheckListModel().GetMoveInData(tenantData.ProspectId ?? 0));

                return Json(new { model = tenantData, moveindata = moveinData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveMoveInCheckList(CheckListModel model)
        {
            try
            {
                string result = (new CheckListModel().SaveMoveInCheckList(model));
                Session.RemoveAll();
                FormsAuthentication.SignOut();
                (new ShomaGroupWebSession()).RemoveWebSession();
                return Json(new { msg = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadInsurenceDoc(CheckListModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadInsurenceDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadInsurenceDoc = Request.Files[i];

                }

                return Json(new { model = new CheckListModel().UploadInsurenceDoc(fileBaseUploadInsurenceDoc, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadProofOfElectricityDoc(CheckListModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadProofOfElectricityDoc = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadProofOfElectricityDoc = Request.Files[i];

                }

                return Json(new { model = new CheckListModel().UploadProofOfElectricityDoc(fileBaseUploadProofOfElectricityDoc, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveMoveInPayment(ApplyNowModel model)
        {
            try
            {
                string paymentDone = "0";
                string result = (new CheckListModel().SaveMoveInPayment(model));
                String[] spearator = { "|" };
                String[] strlist = result.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                if (strlist[1] != "000000")
                {
                    paymentDone = "1";
                }
                return Json(new { Msg = result, PaymentDone = paymentDone }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> LeaseBlumoon()
        {

            try
            {
                var data = await LeaseBlumoonAsync();
                if (data != null)
                {
                    System.IO.File.WriteAllBytes(Server.MapPath("/Content/assets/img/Document/LeaseDocument_" + data.LeaseId + ".pdf"), data.leasePdf);
                }
                return Json(new { LeaseId = data.LeaseId, EsignatureId = data.EsignatureId, EsignatureKey = data.EsignatureKey }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { LeaseId = "0", EsignatureId = "0", EsignatureKey = "" }, JsonRequestBehavior.AllowGet);
            }
            //https://www-new.bluemoonforms.com/esignature/

            //return File(data.leasePdf, "application/pdf", "LeaseDocument_" + data.LeaseId + ".pdf");
        }

        public async System.Threading.Tasks.Task<ActionResult> LeaseDocument()
        {
            try
            {
                var data = await LeaseDocumentAsync();
                if (data != null)
                {
                    System.IO.File.WriteAllBytes(Server.MapPath("/Content/assets/img/Document/LeaseDocument_" + data.LeaseId + ".pdf"), data.leasePdf);
                }
                return Json(new { LeaseId = data.LeaseId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { LeaseId = "0" }, JsonRequestBehavior.AllowGet);
            }

            //return File(data.leasePdf, "application/pdf", "LeaseDocument_" + data.LeaseId + ".pdf");
        }

        public async System.Threading.Tasks.Task<LeaseResponseModel> LeaseBlumoonAsync()
        {
            var bmservice = new BluemoonService();
            LeaseRequestModel leaseRequestModel = new LeaseRequestModel();

            ShomaRMEntities db = new ShomaRMEntities();
            string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            long UserID = Convert.ToInt64(uid);
            var appData = db.tbl_ApplyNow.Where(p => p.UserId == UserID).FirstOrDefault();
            long tid = 0;

            if (appData != null)
            {
                tid = appData.ID;
            }

            var tenantdata = db.tbl_TenantOnline.Where(p => p.ProspectID == tid).FirstOrDefault();
            var GetCoappDet = db.tbl_Applicant.Where(c => c.TenantID == appData.ID && c.Type != "Guarantor").ToList();
            var GetVehicleList = db.tbl_Vehicle.Where(c => c.TenantID == appData.ID).ToList();
            var GetPetList = db.tbl_TenantPet.Where(c => c.TenantID == appData.ID).ToList();

            leaseRequestModel.UNIT_NUMBER = "Unit-" + appData.PropertyId.ToString();
            leaseRequestModel.ADDRESS = "9400 NW 41st Street,Doral, FL 33178, USA";
            leaseRequestModel.DATE_OF_LEASE = appData.CreatedDate.Value.ToString("MM-dd-yyyy");
            leaseRequestModel.LEASE_BEGIN_DATE = appData.MoveInDate.Value.ToString("MM-dd-yyyy");
            leaseRequestModel.LEASE_END_DATE = appData.MoveInDate.Value.AddMonths(Convert.ToInt32(appData.LeaseTerm)).ToString("MM-dd-yyyy");
            leaseRequestModel.RENT = (float)Convert.ToDecimal(appData.Rent);
            leaseRequestModel.PRORATED_RENT = (float)Convert.ToDecimal(appData.Prorated_Rent);
            leaseRequestModel.SECURITY_DEPOSIT = (float)Convert.ToDecimal(appData.Deposit);
            leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_RATE = (float)Convert.ToDecimal(appData.PestAmt);
            leaseRequestModel.PARKING_MONTHLY_CHARGE = (float)Convert.ToDecimal(appData.ParkingAmt);
            leaseRequestModel.PET_ONE_TIME_FEE = (float)Convert.ToDecimal(appData.PetDeposit);
            leaseRequestModel.UTILITY_ADDENDUM_ADMINISTRATION_FEE = (float)Convert.ToDecimal(appData.AdministrationFee);
            leaseRequestModel.PARKING_ONE_TIME_FEE = (float)Convert.ToDecimal(appData.VehicleRegistration);
            leaseRequestModel.RENTERS_INSURANCE_PROVIDER = "";


            if (GetCoappDet.Count == 1)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;

            }
            if (GetCoappDet.Count == 2)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.OCCUPANT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.RESIDENT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
            }
            if (GetCoappDet.Count == 3)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.OCCUPANT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.RESIDENT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.OCCUPANT_3 = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName;
                leaseRequestModel.RESIDENT_3 = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName;
            }
            if (GetVehicleList.Count > 0)
            {
                long StateId1 = Convert.ToInt64(GetVehicleList[0].State);
                var vehState1 = db.tbl_State.Where(p => p.ID == StateId1).FirstOrDefault();

                if (GetVehicleList.Count == 1)
                {


                    leaseRequestModel.VEHICLE_MAKE_1 = GetVehicleList[0].Make;
                    leaseRequestModel.VEHICLE_MODEL_YEAR_1 = GetVehicleList[0].Year;
                    leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 = GetVehicleList[0].License;
                    leaseRequestModel.VEHICLE_STATE_1 = vehState1.StateName;
                }
                if (GetVehicleList.Count == 2)
                {
                    long StateId2 = Convert.ToInt64(GetVehicleList[1].State);
                    var vehState2 = db.tbl_State.Where(p => p.ID == StateId2).FirstOrDefault();

                    leaseRequestModel.VEHICLE_MAKE_1 = GetVehicleList[0].Make;
                    leaseRequestModel.VEHICLE_MODEL_YEAR_1 = GetVehicleList[0].Year;
                    leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 = GetVehicleList[0].License;
                    leaseRequestModel.VEHICLE_STATE_1 = vehState1.StateName;
                    leaseRequestModel.VEHICLE_MAKE_2 = GetVehicleList[1].Make;
                    leaseRequestModel.VEHICLE_MODEL_YEAR_2 = GetVehicleList[1].Year;
                    leaseRequestModel.VEHICLE_LICENSE_NUMBER_2 = GetVehicleList[1].License;
                    leaseRequestModel.VEHICLE_STATE_2 = vehState2.StateName;
                }
            }
            if (GetPetList.Count > 0)
            {
                if (GetPetList.Count == 1)
                {
                    leaseRequestModel.PET_NAME = GetPetList[0].PetName;
                    leaseRequestModel.PET_TYPE = GetPetList[0].PetType == 1 ? "" : "";
                    leaseRequestModel.PET_WEIGHT = GetPetList[0].Weight;
                    leaseRequestModel.PET_DR_NAME = GetPetList[0].VetsName;
                    leaseRequestModel.PET_BREED = GetPetList[0].Breed;
                }
                if (GetPetList.Count == 2)
                {
                    leaseRequestModel.PET_NAME = GetPetList[0].PetName;
                    leaseRequestModel.PET_TYPE = GetPetList[0].PetType == 1 ? "" : "";
                    leaseRequestModel.PET_WEIGHT = GetPetList[0].Weight;
                    leaseRequestModel.PET_DR_NAME = GetPetList[0].VetsName;
                    leaseRequestModel.PET_BREED = GetPetList[0].Breed;

                    leaseRequestModel.PET_2_NAME = GetPetList[1].PetName;
                    leaseRequestModel.PET_2_TYPE = GetPetList[1].PetType == 1 ? "" : "";
                    leaseRequestModel.PET_2_WEIGHT = GetPetList[1].Weight;
                    leaseRequestModel.PET_2_BREED = GetPetList[1].Breed;
                }
            }
            LeaseResponseModel authenticateData = await bmservice.CreateSession();


            string leaseid = "";
            string esignatureid = "";
            string esignaturekey = "";
            if (!string.IsNullOrWhiteSpace(appData.EnvelopeID))
            {
                leaseid = appData.EnvelopeID;
            }

            if (leaseid == "")
            {
                LeaseResponseModel leaseCreateResponse = await bmservice.CreateLease(leaseRequestModel: leaseRequestModel, PropertyId: "112154", sessionId: authenticateData.SessionId);
                var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();

                onlineProspectData.EnvelopeID = leaseCreateResponse.LeaseId;
                db.SaveChanges();

                leaseid = leaseCreateResponse.LeaseId;
            }
            else
            {
                LeaseResponseModel leaseEditResponse = await bmservice.EditLease(leaseRequestModel: leaseRequestModel, leaseId: leaseid, sessionId: authenticateData.SessionId);
            }

            List<EsignatureParty> esignatureParties = new List<EsignatureParty>();

            // please provide the list with the correct data from lease methods. below is some static details I provided - Sachin Mahore upadated dynamic
            // Note. For owmer please set IsOwner true and other will ve residents which will be set to false.
            esignatureParties.Add(new EsignatureParty()
            {
                Email = "info@sanctuarydoral.com",
                IsOwner = true,
                Name = "Sanctuary Doral",
                Phone = "786-437-8658"
            });

            // add the residents details who will sign the document
            // Note 1. email should be valid email . on this email , the residents will get the esignature request
            //      2. name should match exact as per resisdent detail in create lease otherwise we will not get any response

            if (GetCoappDet.Count == 1)
            {
                esignatureParties.Add(new EsignatureParty()
                {
                    Email = GetCoappDet[0].Email,
                    IsOwner = false,
                    Name = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName,
                    Phone = GetCoappDet[0].Phone,
                });
            }
            if (GetCoappDet.Count == 2)
            {
                esignatureParties.Add(new EsignatureParty()
                {
                    Email = GetCoappDet[0].Email,
                    IsOwner = false,
                    Name = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName,
                    Phone = GetCoappDet[0].Phone,
                });
                esignatureParties.Add(new EsignatureParty()
                {
                    Email = GetCoappDet[1].Email,
                    IsOwner = false,
                    Name = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName,
                    Phone = GetCoappDet[1].Phone,
                });
            }
            if (GetCoappDet.Count == 3)
            {
                esignatureParties.Add(new EsignatureParty()
                {
                    Email = GetCoappDet[0].Email,
                    IsOwner = false,
                    Name = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName,
                    Phone = GetCoappDet[0].Phone,
                });
                esignatureParties.Add(new EsignatureParty()
                {
                    Email = GetCoappDet[1].Email,
                    IsOwner = false,
                    Name = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName,
                    Phone = GetCoappDet[1].Phone,
                });
                esignatureParties.Add(new EsignatureParty()
                {
                    Email = GetCoappDet[2].Email,
                    IsOwner = false,
                    Name = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName,
                    Phone = GetCoappDet[2].Phone,
                });
            }

            LeaseResponseModel EsignatureResponse = await bmservice.RequestEsignature(leaseId: leaseid, sessionId: authenticateData.SessionId, esignatureParties: esignatureParties);

            // this will not give pdf with signature right away because this will need to be called after residents will sign the esignature. 
            // so please call it on any download lease document button 
            // Note. please save the esignature id for downloading document 

            LeaseResponseModel leaseDocumentWithEsignature = await bmservice.GetLeaseDocumentWithEsignature(SessionId: authenticateData.SessionId, EsignatureId: EsignatureResponse.EsignatureId);

            if (leaseid != "")
            {
                esignatureid = EsignatureResponse.EsignatureId;
                var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();
                onlineProspectData.EsignatureID = EsignatureResponse.EsignatureId;
                db.SaveChanges();

                LeaseResponseModel leaseKeys = await bmservice.GetEsignnatureDetails(SessionId: authenticateData.SessionId, EsignatureId: EsignatureResponse.EsignatureId);

                foreach (var apptdata in GetCoappDet)
                {
                    var keydata = leaseKeys.EsigneResidents.Where(p => p.Email == apptdata.Email).FirstOrDefault();
                    if (keydata != null)
                    {
                        if (apptdata.Type == "Primary Applicant")
                        {
                            esignaturekey = keydata.Key;
                        }
                        var EsignatureData = new tbl_ESignatureKeys()
                        {
                            TenantID = tid,
                            ApplicantID = apptdata.ApplicantID,
                            Key = keydata.Key,
                            EsignatureId = Convert.ToInt64(EsignatureResponse.EsignatureId),
                            DateSigned = keydata.DateSigned
                        };
                        db.tbl_ESignatureKeys.Add(EsignatureData);
                        db.SaveChanges();
                    }
                    db.Dispose();
                }
            }
            //LeaseResponseModel leasePdfResponse = await bmservice.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: leaseid);
            await bmservice.CloseSession(sessionId: authenticateData.SessionId);
            leaseDocumentWithEsignature.LeaseId = leaseid;
            leaseDocumentWithEsignature.EsignatureId = esignatureid;
            leaseDocumentWithEsignature.EsignatureKey = esignaturekey;
            return leaseDocumentWithEsignature;
        }
        public async System.Threading.Tasks.Task<LeaseResponseModel> LeaseDocumentAsync()
        {
            var bmservice = new BluemoonService();
            LeaseRequestModel leaseRequestModel = new LeaseRequestModel();

            ShomaRMEntities db = new ShomaRMEntities();
            string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            long UserID = Convert.ToInt64(uid);
            var appData = db.tbl_ApplyNow.Where(p => p.UserId == UserID).FirstOrDefault();
            long tid = 0;

            if (appData != null)
            {
                tid = appData.ID;
            }

            var tenantdata = db.tbl_TenantOnline.Where(p => p.ProspectID == tid).FirstOrDefault();
            var GetCoappDet = db.tbl_Applicant.Where(c => c.TenantID == appData.ID && c.Type != "Guarantor").ToList();
            var GetVehicleList = db.tbl_Vehicle.Where(c => c.TenantID == appData.ID).ToList();
            var GetPetList = db.tbl_TenantPet.Where(c => c.TenantID == appData.ID).ToList();

            leaseRequestModel.UNIT_NUMBER = "Unit-" + appData.PropertyId.ToString();
            leaseRequestModel.ADDRESS = " 9400 NW 41st Street,Doral, FL 33178, USA";
            leaseRequestModel.DATE_OF_LEASE = appData.CreatedDate.Value.ToString("MM-dd-yyyy");
            leaseRequestModel.LEASE_BEGIN_DATE = appData.MoveInDate.Value.ToString("MM-dd-yyyy");
            leaseRequestModel.LEASE_END_DATE = appData.MoveInDate.Value.AddMonths(Convert.ToInt32(appData.LeaseTerm)).ToString("MM-dd-yyyy");
            leaseRequestModel.RENT = (float)Convert.ToDecimal(appData.Rent);
            leaseRequestModel.PRORATED_RENT = (float)Convert.ToDecimal(appData.Prorated_Rent);
            leaseRequestModel.SECURITY_DEPOSIT = (float)Convert.ToDecimal(appData.Deposit);
            leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_RATE = (float)Convert.ToDecimal(appData.PestAmt);
            leaseRequestModel.PARKING_MONTHLY_CHARGE = (float)Convert.ToDecimal(appData.ParkingAmt);
            leaseRequestModel.PET_ONE_TIME_FEE = (float)Convert.ToDecimal(appData.PetDeposit);
            leaseRequestModel.UTILITY_ADDENDUM_ADMINISTRATION_FEE = (float)Convert.ToDecimal(appData.AdministrationFee);
            leaseRequestModel.PARKING_ONE_TIME_FEE = (float)Convert.ToDecimal(appData.VehicleRegistration);
            leaseRequestModel.RENTERS_INSURANCE_PROVIDER = "";


            if (GetCoappDet.Count == 1)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;

            }
            if (GetCoappDet.Count == 2)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.OCCUPANT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.RESIDENT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
            }
            if (GetCoappDet.Count == 3)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.OCCUPANT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.RESIDENT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.OCCUPANT_3 = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName;
                leaseRequestModel.RESIDENT_3 = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName;
            }
            if (GetVehicleList.Count > 0)
            {
                long StateId1 = Convert.ToInt64(GetVehicleList[0].State);
                var vehState1 = db.tbl_State.Where(p => p.ID == StateId1).FirstOrDefault();

                if (GetVehicleList.Count == 1)
                {


                    leaseRequestModel.VEHICLE_MAKE_1 = GetVehicleList[0].Make;
                    leaseRequestModel.VEHICLE_MODEL_YEAR_1 = GetVehicleList[0].Year;
                    leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 = GetVehicleList[0].License;
                    leaseRequestModel.VEHICLE_STATE_1 = vehState1.StateName;
                }
                if (GetVehicleList.Count == 2)
                {
                    long StateId2 = Convert.ToInt64(GetVehicleList[1].State);
                    var vehState2 = db.tbl_State.Where(p => p.ID == StateId2).FirstOrDefault();

                    leaseRequestModel.VEHICLE_MAKE_1 = GetVehicleList[0].Make;
                    leaseRequestModel.VEHICLE_MODEL_YEAR_1 = GetVehicleList[0].Year;
                    leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 = GetVehicleList[0].License;
                    leaseRequestModel.VEHICLE_STATE_1 = vehState1.StateName;
                    leaseRequestModel.VEHICLE_MAKE_2 = GetVehicleList[1].Make;
                    leaseRequestModel.VEHICLE_MODEL_YEAR_2 = GetVehicleList[1].Year;
                    leaseRequestModel.VEHICLE_LICENSE_NUMBER_2 = GetVehicleList[1].License;
                    leaseRequestModel.VEHICLE_STATE_2 = vehState2.StateName;
                }
            }
            if (GetPetList.Count > 0)
            {
                if (GetPetList.Count == 1)
                {
                    leaseRequestModel.PET_NAME = GetPetList[0].PetName;
                    leaseRequestModel.PET_TYPE = GetPetList[0].PetType == 1 ? "" : "";
                    leaseRequestModel.PET_WEIGHT = GetPetList[0].Weight;
                    leaseRequestModel.PET_DR_NAME = GetPetList[0].VetsName;
                    leaseRequestModel.PET_BREED = GetPetList[0].Breed;
                }
                if (GetPetList.Count == 2)
                {
                    leaseRequestModel.PET_NAME = GetPetList[0].PetName;
                    leaseRequestModel.PET_TYPE = GetPetList[0].PetType == 1 ? "" : "";
                    leaseRequestModel.PET_WEIGHT = GetPetList[0].Weight;
                    leaseRequestModel.PET_DR_NAME = GetPetList[0].VetsName;
                    leaseRequestModel.PET_BREED = GetPetList[0].Breed;

                    leaseRequestModel.PET_2_NAME = GetPetList[1].PetName;
                    leaseRequestModel.PET_2_TYPE = GetPetList[1].PetType == 1 ? "" : "";
                    leaseRequestModel.PET_2_WEIGHT = GetPetList[1].Weight;
                    leaseRequestModel.PET_2_BREED = GetPetList[1].Breed;
                }
            }
            LeaseResponseModel authenticateData = await bmservice.CreateSession();


            string leaseid = "";
            if (!string.IsNullOrWhiteSpace(appData.EnvelopeID))
            {
                leaseid = appData.EnvelopeID;
            }

            if (leaseid == "")
            {
                LeaseResponseModel leaseCreateResponse = await bmservice.CreateLease(leaseRequestModel: leaseRequestModel, PropertyId: "112154", sessionId: authenticateData.SessionId);
                var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();
                onlineProspectData.EnvelopeID = leaseCreateResponse.LeaseId;
                db.SaveChanges();

                leaseid = leaseCreateResponse.LeaseId;
            }
            else
            {
                LeaseResponseModel leaseEditResponse = await bmservice.EditLease(leaseRequestModel: leaseRequestModel, leaseId: leaseid, sessionId: authenticateData.SessionId);
            }


            LeaseResponseModel leasePdfResponse = await bmservice.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: leaseid);

            if (leaseid != "")
            {
                var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == tid).FirstOrDefault();
                db.SaveChanges();
            }
            
            await bmservice.CloseSession(sessionId: authenticateData.SessionId);
            leasePdfResponse.LeaseId = leaseid;
            return leasePdfResponse;
        }
        public async System.Threading.Tasks.Task<ActionResult> GetLeaseDocBlumoon()
        {
            try
            {
                var data = await GetLeaseDocBlumoonAsync();
                if (data != null)
                {
                    System.IO.File.WriteAllBytes(Server.MapPath("/Content/assets/img/Document/LeaseDocument_" + data.LeaseId + ".pdf"), data.leasePdf);
                }
                return Json(new { LeaseId = data.LeaseId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { LeaseId = "0" }, JsonRequestBehavior.AllowGet);
            }

        }
        public async System.Threading.Tasks.Task<LeaseResponseModel> GetLeaseDocBlumoonAsync()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string uid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID.ToString();
            long UserID = Convert.ToInt64(uid);
            var tenantData = db.tbl_ApplyNow.Where(p => p.UserId == UserID).FirstOrDefault();
            string LeaseId = tenantData.EnvelopeID;
            string esignatureId = tenantData.EsignatureID;
            var bmservice = new BluemoonService();

            LeaseResponseModel authenticateData = await bmservice.CreateSession();
            if (!string.IsNullOrWhiteSpace(tenantData.EsignatureID))
            {
                LeaseResponseModel leaseDocumentWithEsignature = await bmservice.GetLeaseDocumentWithEsignature(SessionId: authenticateData.SessionId, EsignatureId: esignatureId);
                await bmservice.CloseSession(sessionId: authenticateData.SessionId);
                leaseDocumentWithEsignature.LeaseId = LeaseId;
                return leaseDocumentWithEsignature;
            }
            else
            {
                LeaseResponseModel leasePdfResponse = await bmservice.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: LeaseId);
                await bmservice.CloseSession(sessionId: authenticateData.SessionId);
                leasePdfResponse.LeaseId = LeaseId;
                return leasePdfResponse;
            }
        }
    }
}