using ShomaRM.Data;
using ShomaRM.Models;
using ShomaRM.Models.Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShomaRM.Controllers
{
    public class CheckListController : Controller
    {
        // GET: CheckList
        public ActionResult Index()
        {
            ViewBag.UID = "0";
            if (ShomaGroupWebSession.CurrentUser != null)
            {
                ViewBag.UID = ShomaGroupWebSession.CurrentUser.UserID.ToString();
            }

            return View();
        }
        public ActionResult SaveMoveInCheckList(CheckListModel model)
        {
            try
            {
                return Json(new { msg = (new CheckListModel().SaveMoveInCheckList(model)) }, JsonRequestBehavior.AllowGet);
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
                return Json(new { Msg = (new CheckListModel().SaveMoveInPayment(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async System.Threading.Tasks.Task<ActionResult> LeaseBlumoon()
        {
          
            var data = await LeaseBlumoonAsync();
            if (data != null)
            {

            }

            return File(data.leasePdf, "application/pdf", $"LeaseDocument_{0}.pdf");
        }

        public async System.Threading.Tasks.Task<LeaseResponseModel> LeaseBlumoonAsync()
        {

            var test = new BluemoonService();
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
            var GetCoappDet = db.tbl_Applicant.Where(c => c.TenantID == appData.ID).ToList();
            var GetVehicleList = db.tbl_Vehicle.Where(c => c.TenantID == appData.ID).ToList();
            var GetPetList = db.tbl_TenantPet.Where(c => c.TenantID == appData.ID).ToList();

            leaseRequestModel.UNIT_NUMBER ="Unit-"+appData.PropertyId.ToString();
            leaseRequestModel.ADDRESS = tenantdata.HomeAddress1 + " " + tenantdata.CityHome;
            //leaseRequestModel.LEASE_BEGIN_DATE = appData.MoveInDate;
            //leaseRequestModel.LEASE_END_DATE = appData.MoveInDate.Value.AddMonths(Convert.ToInt32(appData.LeaseTerm));
            leaseRequestModel.RENT = (float)Convert.ToDecimal(appData.Rent);
            leaseRequestModel.PRORATED_RENT = (float)Convert.ToDecimal(appData.Prorated_Rent);
            leaseRequestModel.SECURITY_DEPOSIT = (float)Convert.ToDecimal(appData.Deposit);
            leaseRequestModel.UTILITY_ADDENDUM_PEST_CONTROL_RATE = (float)Convert.ToDecimal(appData.PestAmt);
            leaseRequestModel.PARKING_MONTHLY_CHARGE = (float)Convert.ToDecimal(appData.ParkingAmt);
            leaseRequestModel.PET_ONE_TIME_FEE = (float)Convert.ToDecimal(appData.PetDeposit);
            leaseRequestModel.UTILITY_ADDENDUM_ADMINISTRATION_FEE = (float)Convert.ToDecimal(appData.AdministrationFee);

            if (GetCoappDet.Count==1)
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
            if (GetCoappDet.Count ==3)
            {
                leaseRequestModel.OCCUPANT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.RESIDENT_1 = GetCoappDet[0].FirstName + " " + GetCoappDet[0].LastName;
                leaseRequestModel.OCCUPANT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.RESIDENT_2 = GetCoappDet[1].FirstName + " " + GetCoappDet[1].LastName;
                leaseRequestModel.OCCUPANT_3 = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName;
                leaseRequestModel.RESIDENT_3 = GetCoappDet[2].FirstName + " " + GetCoappDet[2].LastName;
            }
            if (GetVehicleList.Count == 1)
            {
                leaseRequestModel.VEHICLE_MAKE_1 = GetVehicleList[0].Make;
                leaseRequestModel.VEHICLE_MODEL_YEAR_1 = GetVehicleList[0].Year;
                leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 = GetVehicleList[0].License;          
            }
            if (GetVehicleList.Count == 2)
            {
                leaseRequestModel.VEHICLE_MAKE_1 = GetVehicleList[0].Make;
                leaseRequestModel.VEHICLE_MODEL_YEAR_1 = GetVehicleList[0].Year;
                leaseRequestModel.VEHICLE_LICENSE_NUMBER_1 = GetVehicleList[0].License;
                leaseRequestModel.VEHICLE_MAKE_2 = GetVehicleList[1].Make;
                leaseRequestModel.VEHICLE_MODEL_YEAR_2 = GetVehicleList[1].Year;
                leaseRequestModel.VEHICLE_LICENSE_NUMBER_2 = GetVehicleList[1].License;
              
            }
            if (GetPetList.Count == 1)
            {
                leaseRequestModel.PET_NAME = GetPetList[0].PetName;
                leaseRequestModel.PET_TYPE = GetPetList[0].PetType==1?"":"";
                leaseRequestModel.PET_WEIGHT = GetPetList[0].Weight;
                leaseRequestModel.PET_DR_NAME = GetPetList[0].VetsName;
                leaseRequestModel.PET_BREED = GetPetList[0].Breed;
            }
            if (GetPetList.Count == 2)
            {
                leaseRequestModel.PET_2_NAME = GetPetList[0].PetName;
                leaseRequestModel.PET_2_TYPE = GetPetList[0].PetType == 1 ? "" : "";
                leaseRequestModel.PET_2_WEIGHT = GetPetList[0].Weight;
             
                leaseRequestModel.PET_2_BREED = GetPetList[0].Breed;
            }
            LeaseResponseModel authenticateData = await test.CreateSession();
            LeaseResponseModel leaseCreateResponse = await test.CreateLease(leaseRequestModel: leaseRequestModel, PropertyId: "112154", sessionId: authenticateData.SessionId);
            LeaseResponseModel leaseEditResponse = await test.EditLease(leaseRequestModel: leaseRequestModel, leaseId: leaseCreateResponse.LeaseId, sessionId: authenticateData.SessionId);
            LeaseResponseModel leasePdfResponse = await test.GenerateLeasePdf(sessionId: authenticateData.SessionId, leaseId: leaseCreateResponse.LeaseId);
            await test.CloseSession(sessionId: authenticateData.SessionId);
            return leasePdfResponse;


        }

    }

}