using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;

namespace ShomaRM.Controllers
{
    public class ApplyNowController : Controller
    {
        //
        // GET: /ApplyNow/
        public ActionResult Index(string id)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.PID = Convert.ToInt32(id);
            }
            else
            {
                return Redirect("/Property");
            }

            var model = new OnlineProspectModule().GetProspectData(Convert.ToInt64(id));
            if (Session["Bedroom"] != null)
            {
                model.Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                model.MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                model.MaxRent = Convert.ToDecimal(Session["MaxRent"].ToString());
                model.LeaseTerm = Convert.ToInt32(Session["LeaseTerm"].ToString());
                model.FromHome = 1;
                Session.Remove("Bedroom");
                Session.Remove("MoveInDate");
                Session.Remove("MaxRent");
                Session.Remove("LeaseTerm");
            }
            else
            {
                model.Bedroom = 0;
                model.MoveInDate = DateTime.Now.AddDays(30);
                //model.MaxRent = 0;
                model.FromHome = 0;
            }
            if (Session["StepNo"] != null)
            {
                model.StepNo = Convert.ToInt32(Session["StepNo"].ToString());
                Session.Remove("StepNo");
            }
            else
            {
                model.StepNo = 0;

            }
            return View(model);
        }

        public ActionResult SaveOnlineProspect(OnlineProspectModule model)
        {
            try
            {
                return Json(new { msg = (new OnlineProspectModule().SaveOnlineProspect(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateOnlineProspect(OnlineProspectModule model)
        {
            try
            {
                return Json(new { msg = (new OnlineProspectModule().UpdateOnlineProspect(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveCheckPolicy(OnlineProspectModule model)
        {
            try
            {
                return Json(new { msg = (new OnlineProspectModule().SaveCheckPolicy(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveDocumentVerification(DocumentVerificationModule model)
        {
            try
            {
                // long AccountID = formData.AccountID;
                HttpPostedFileBase fb = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fb = Request.Files[i];

                }
                string msg = new DocumentVerificationModule().SaveDocumentVerifications(fb, model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SavePaymentDetails(ApplyNowModel model)
        {
            try
            {
                return Json(new { Msg = (new ApplyNowModel().SavePaymentDetails(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult saveCoAppPayment(ApplyNowModel model)
        {
            try
            {
                return Json(new { Msg = (new ApplyNowModel().saveCoAppPayment(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetApplyNowList(int id)
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().GetApplyNowList(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult SignIn(string UserName, string Password)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string userid = "";
            var user = db.tbl_Login.Where(p => p.Username == UserName && p.Password == Password && p.IsActive == 1).FirstOrDefault();
            if (user != null)
            {
                //var currentUser = new CurrentUser();
                //currentUser.TenantID = user.TenantID == 0 ? 0 : Convert.ToInt64(user.TenantID);
                //currentUser.UserID = user.UserID;
                var currentUser = new CurrentUser();
                currentUser.UserID = user.UserID;
                currentUser.Username = user.Username;
                currentUser.FullName = user.FirstName + " " + user.LastName;
                currentUser.EmailAddress = user.Email;
                currentUser.IsAdmin = (user.IsSuperUser.HasValue ? user.IsSuperUser.Value : 0);
                currentUser.EmailAddress = user.Email;
                currentUser.UserType = Convert.ToInt32(user.UserType == null ? 0 : user.UserType);
                currentUser.LoggedInUser = user.FirstName;
                currentUser.TenantID = user.TenantID == 0 ? 0 : Convert.ToInt64(user.TenantID);
                currentUser.UserType = Convert.ToInt32((user.UserType).ToString());

                (new ShomaGroupWebSession()).SetWebSession(currentUser);
                userid = user.UserID.ToString();

                if (currentUser.TenantID == 0 && currentUser.UserType != 3)
                {
                    //admin site 
                    userid += "|ad";
                    SignInFormAuth(UserName, false);
                }
                else if (currentUser.TenantID != 0)
                {
                    //tenant site
                    userid += "|te";
                    SignInFormAuth(UserName, false);
                }
                else
                {
                    var checkExpiry = db.tbl_ApplyNow.Where(co => co.UserId == currentUser.UserID).FirstOrDefault();

                    checkExpiry.Status = (!string.IsNullOrWhiteSpace(checkExpiry.Status) ? checkExpiry.Status : "");

                    if (checkExpiry.Status.Trim() == "Approved")
                    {
                        return RedirectToAction("../Checklist/");
                    }
                    if (checkExpiry != null)
                    {
                        if (checkExpiry.CreatedDate < DateTime.Now.AddHours(-72))
                        {
                            new ApplyNowController().DeleteApplicantTenantID(checkExpiry.ID, currentUser.UserID);
                            Session["DelDatAll"] = "Del";
                            userid = "-1|hp";
                        }
                        else
                        {
                            Session["DelDatAll"] = null;
                            userid += "|an";
                        }
                    }
                }
            }
            else
            {
                user = db.tbl_Login.Where(p => p.Username == UserName).FirstOrDefault();
                if (user == null)
                {
                    userid = "Email ID not registerd.|er";
                }
                else if (user == null)
                {
                    userid = "Invalid password.|er";
                }
                else
                {
                    userid = "User is not active.|er";
                }
                userid = "0|er";
            }
            return Json(new { UserData = userid }, JsonRequestBehavior.AllowGet);
        }
        public void SignInFormAuth(string userName, bool rememberMe)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(userName, rememberMe);
        }
        public ActionResult SaveTenantOnline(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveTenantOnline(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTenantOnlineList(int id)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().GetTenantOnlineList(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        //Amit's Work

        public ActionResult SaveUpdateApplicantHistory(ApplicantHistoryModel model)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().SaveUpdateApplicantHistory(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetApplicantHistoryList(long TenantID)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetApplicantHistoryList(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GetApplicantHistoryDetails(long AHID)
        {

            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetApplicantHistoryDetails(AHID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteApplicantHistory(long AHID)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().DeleteApplicantHistory(AHID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult SendCoappEmail(OnlineProspectModule model)
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().SendCoappEmail(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult SendPayLinkEmail(OnlineProspectModule model)
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().SendPayLinkEmail(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult PrintSummary()
        {
            return View();
        }

        public ActionResult HaveVehicle(long id, bool vehicleValue)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().SaveHaveVehicle(id, vehicleValue) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HavePet(long id, bool PetValue)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().SaveHavePet(id, PetValue) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //New Upload Code

        public ActionResult TaxFileUpload1(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload1 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload1 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload1(fileBaseUpload1, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TaxFileUpload2(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload2 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload2 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload2(fileBaseUpload2, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TaxFileUpload3(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload3 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload3 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload3(fileBaseUpload3, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadPassport(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadPassport = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadPassport = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveUploadPassport(fileBaseUploadPassport, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadIdentity(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadIdentity = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadIdentity = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveUploadIdentity(fileBaseUploadIdentity, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UploadPetPhoto(PetModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadPetName = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadPetName = Request.Files[i];

                }

                return Json(new { model = new PetModel().SaveUploadPetPhoto(fileBaseUploadPetName, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UploadPetVaccinationCertificate(PetModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadPetVaccinationCer = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadPetVaccinationCer = Request.Files[i];

                }

                return Json(new { model = new PetModel().SaveUploadPetVaccinationC(fileBaseUploadPetVaccinationCer, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UploadVehicleRegistation(VehicleModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUploadVehicleRegistation = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUploadVehicleRegistation = Request.Files[i];

                }

                return Json(new { model = new VehicleModel().SaveUploadVehicleRegistation(fileBaseUploadVehicleRegistation, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTenantPetPlaceData(long id)
        {
            try
            {
                return Json(new { model = new TenantPetPlace().GetTenantPetPlaceData(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteApplicantTenantID(long TenantID,long UserId)
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().DeleteApplicantTenantID(TenantID, UserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult ForgetPassword(ApplyNowModel model)
        {
            try
            {
                return Json(new { model = new ApplyNowModel().ForgetPassword(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckEmailAreadyExist(string EmailId)
        {
            try
            {
                return Json(new { model = new ApplyNowModel().CheckEmailAreadyExist(EmailId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMonthsFromApplicantHistory(long TenantId , string FromDateAppHis , string ToDateAppHis)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetMonthsFromApplicantHistory(TenantId, FromDateAppHis, ToDateAppHis) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}