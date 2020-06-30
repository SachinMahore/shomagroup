using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShomaRM.Models;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;
using System.Data;

using iText;
using iText.Html2pdf;
using iText.Layout.Element;
using iText.Kernel.Pdf;
using iText.Layout;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace ShomaRM.Controllers
{
    public class ApplyNowController : Controller
    {
        //
        // GET: /ApplyNow/
        public ActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    ViewBag.PID = Convert.ToInt32(id);
                }
                catch
                {
                    ViewBag.PID = 0;
                    id = "0";
                }
            }
            else
            {
                id = "0";
                return Redirect("/Home");
            }
            if (ShomaGroupWebSession.CurrentUser == null && id!="0")
            {
                return Redirect("/Account/Login");
            }

            var model = new OnlineProspectModule().GetProspectData(Convert.ToInt64(id));

            if (Session["Bedroom"] != null)
            {
                model.Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                model.MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                model.MaxRent = Convert.ToDecimal(Session["MaxRent"].ToString());
                model.LeaseTermID = Convert.ToInt32(Session["LeaseTerm"].ToString());
                var leaseDet = new Areas.Admin.Models.LeaseTermsModel().GetLeaseTermsDetails(model.LeaseTermID);
                model.LeaseTerm =Convert.ToInt32(leaseDet.LeaseTerms);
                model.FromHome = 1;
                Session.Remove("Bedroom");
                Session.Remove("MoveInDate");
                Session.Remove("MaxRent");
                Session.Remove("LeaseTerm");
            } else if (Session["Model"] != null)
            {
                model.Bedroom = 0;
                model.MoveInDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                model.MaxRent = 1000000;
                ShomaRMEntities db = new ShomaRMEntities();
                var leaseDet = db.tbl_LeaseTerms.Where(p => p.LeaseTerms == 12).FirstOrDefault();
                if (leaseDet != null)
                {
                    model.LeaseTermID = leaseDet.LTID;
                }
                else
                {
                    model.LeaseTermID = 0;
                }
                model.LeaseTermID = leaseDet.LTID;
                model.LeaseTerm = Convert.ToInt32(leaseDet.LeaseTerms);
                model.Building = Session["Model"].ToString();
                model.FromHome = 3;
                model.StepCompleted = 2;
                Session.Remove("Model");
            }
            else
            {
                model.Bedroom = 0;
                model.MoveInDate = DateTime.Now.AddDays(30);
                //model.MaxRent = 0;
                model.FromHome = 0;
                if (model.LeaseTermID == 0)
                {
                    ShomaRMEntities db = new ShomaRMEntities();
                    var leaseDet = db.tbl_LeaseTerms.Where(p => p.LeaseTerms == 12).FirstOrDefault();
                    if (leaseDet != null)
                    {
                        model.LeaseTermID = leaseDet.LTID;
                    }
                    else
                    {
                        model.LeaseTermID = 0;
                    }
                }
               
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

        public async System.Threading.Tasks.Task<ActionResult> SavePaymentDetails(ApplyNowModel model)
        {
            try
            {
                return Json(new { Msg = await (new ApplyNowModel().SavePaymentDetails(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SaveCoGuPaymentDetails(ApplyNowModel model)
        {
            try
            {
                return Json(new { Msg = (new ApplyNowModel().SaveCoGuPaymentDetails(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async System.Threading.Tasks.Task<ActionResult> saveCoAppPayment(ApplyNowModel model)
        {
            try
            {
                string msg = await (new ApplyNowModel()).saveCoAppPayment(model);
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Sachin M 09 June 2020
        public async System.Threading.Tasks.Task<ActionResult> saveListPayment(ApplyNowModel model)
        {
            try
            {
                string msg = await (new ApplyNowModel().saveListPayment(model));
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Sachin M 25 June 2020
        public async System.Threading.Tasks.Task<ActionResult>  saveListPaymentFinalStep(ApplyNowModel model)
        {
            try
            {
                string msg =await (new ApplyNowModel().saveListPaymentFinalStep(model));
                return Json(new { Msg = msg }, JsonRequestBehavior.AllowGet);
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
            string encryptedPassword = new EncryptDecrypt().EncryptText(Password);
            ShomaRMEntities db = new ShomaRMEntities();
            string userid = "";
            var user = db.tbl_Login.Where(p => p.Username == UserName && p.Password == encryptedPassword && p.IsActive == 1).FirstOrDefault();
            if (user != null)
            {
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
                    userid += "|ad|admin";
                    SignInFormAuth(UserName, false);
                }
                else if (currentUser.TenantID != 0)
                {
                    //tenant site
                    userid += "|te|tenant";
                    SignInFormAuth(UserName, false);
                }
                else
                {
                    var checkExpiry = db.tbl_ApplyNow.Where(co => co.UserId == currentUser.UserID).FirstOrDefault();
                    checkExpiry.Status = (!string.IsNullOrWhiteSpace(checkExpiry.Status) ? checkExpiry.Status : "");
                    if ((checkExpiry.StepCompleted ?? 0) == 18 && checkExpiry.Status.Trim() == "")
                    {
                        userid += "|as|" + (new EncryptDecrypt().EncryptText("In Progress"));
                        //return RedirectToAction("../ApplicationStatus/" + (new EncryptDecrypt().EncryptText("In Progress")));
                    }
                    else if (checkExpiry.Status.Trim() == "Approved")
                    {
                        checkExpiry.StepCompleted = 18;
                        db.SaveChanges();
                        userid += "|as|" + (new EncryptDecrypt().EncryptText("Approved"));
                        //return RedirectToAction("../ApplicationStatus/" + (new EncryptDecrypt().EncryptText("Approved")));
                    }
                    else if (checkExpiry.Status.Trim() == "Signed")
                    {
                        userid += "|cl|checklist";
                        //return RedirectToAction("../Checklist/");
                    }
                    else
                    {
                        checkExpiry.Status = (!string.IsNullOrWhiteSpace(checkExpiry.Status) ? checkExpiry.Status : "");
                        if (checkExpiry != null)
                        {
                            DateTime expDate = Convert.ToDateTime(DateTime.Now.AddHours(-72).ToString("MM/dd/yyyy") + " 23:59:59");

                            if (checkExpiry.CreatedDate < expDate)
                            {
                                new ApplyNowController().DeleteApplicantTenantID(checkExpiry.ID, currentUser.UserID);
                                Session["DelDatAll"] = "Del";
                                userid = "-1|hp|homepage";
                            }
                            else
                            {
                                Session["DelDatAll"] = null;
                                userid += "|an|applynow";
                            }
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
        public ActionResult SaveCoGuTenantOnline(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveCoGuTenantOnline(model)) }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetSSNIdNumberPassportNumber(int id, int vid)
        {
            try
            {
                return Json(new { ssn = new TenantOnlineModel().GetSSNIdNumberPassportNumber(id, vid) }, JsonRequestBehavior.AllowGet);
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
        //Sachin M 12 may
        public ActionResult GetApplicantHistoryListPV(long TenantID, long UserID)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetApplicantHistoryListPV(TenantID,UserID) }, JsonRequestBehavior.AllowGet);
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

        public ActionResult SendPayLinkEmailApplyNow(long ProspectId, long ApplicationID, decimal ChargeAmount, int ChargeType, string Email)
        {
            try
            {
                return Json(new { model = new OnlineProspectModule().SendPayLinkEmailApplyNow(ProspectId, ApplicationID, ChargeAmount, ChargeType, Email) }, JsonRequestBehavior.AllowGet);
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


        //sachin m 11 may
        public ActionResult TaxFileUpload6(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload6 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload6 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload6(fileBaseUpload6, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TaxFileUpload7(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload7 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload7 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload7(fileBaseUpload7, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TaxFileUpload8(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload8 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload8 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload8(fileBaseUpload8, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TaxFileUpload4(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload4 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload4 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload4(fileBaseUpload4, model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TaxFileUpload5(TenantOnlineModel model)
        {
            try
            {
                HttpPostedFileBase fileBaseUpload5 = null;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileBaseUpload5 = Request.Files[i];

                }

                return Json(new { model = new TenantOnlineModel().SaveTaxUpload5(fileBaseUpload5, model) }, JsonRequestBehavior.AllowGet);
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
        public ActionResult ChangePassword(string uid)
        {
            string[] uidd = new EncryptDecrypt().DecryptText(uid).Split(',');
            ViewBag.UID = uidd[0];
            if (uidd[1] == "0")
            {
                var model = new ApplyNowModel().ExpireChangePassword(Convert.ToInt64(uidd[0]));
                ViewBag.LinkExp = model;
                ViewBag.IsTempPass = "0";
            }
            else
            {
                var email= new ApplyNowModel().GetEmailAddress(Convert.ToInt64(uidd[0]));
                ViewBag.Email = email;
                ViewBag.LinkExp = "No";
                ViewBag.IsTempPass = "1";
            }
            return View();
        }
        public JsonResult SaveChangePassword(long UserID,string EmailId, string NewPassword)
        {
            try
            {
                return Json(new { model = new ApplyNowModel().SaveChangePassword(UserID,EmailId, NewPassword) }, JsonRequestBehavior.AllowGet);
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

        public JsonResult DeleteVehicleListOnCheck(long TenantId)
        {
            try
            {
                return Json(new { model = new VehicleModel().DeleteVehicleListOnCheck(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdateSSN(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveUpdateSSN(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdateIDNumber(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveUpdateIDNumber(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdatePassportNumber(TenantOnlineModel model)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().SaveUpdatePassportNumber(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveUpdateEmployerHistory(EmployerHistoryModel model )
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().saveUpdateEmployerHistory(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       
        public JsonResult GetEmployerHistory(long TenantId)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().GetEmployerHistory(TenantId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteEmployerHistory(long HEIID)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().DeleteEmployerHistory(HEIID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMonthsFromEmployerHistory(long TenantId, string EmpStartDate, string EmpTerminationDate)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().GetMonthsFromEmployerHistory(TenantId, EmpStartDate, EmpTerminationDate) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditEmployerHistory(long HEIID)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().EditEmployerHistory(HEIID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
               
        public JsonResult SaveUpdateStep(long ID, int StepCompleted)
        {
            try
            {
                return Json(new { result = new ApplyNowModel().SaveUpdateStep(ID, StepCompleted) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult SaveUpdateOnlineProspect(OnlineProspectModule model)
        {
            try
            {
                return Json(new { msg = (new OnlineProspectModule().SaveUpdateOnlineProspect(model)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckUnitAvailable(long UnitID, long ProspectID)
        {
            try
            {
                return Json(new { result = (new ApplyNowModel().CheckUnitAvailable(UnitID, ProspectID)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getPreviousEmployementInfo(int id)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().GetPriousEmploymentInfo(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //sachin m 13 may
        public ActionResult UpdateResStatus(long ID, int ResidenceStatus,string ResidenceNotes)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().UpdateResStatus(ID,ResidenceStatus,ResidenceNotes)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateEmpStatus(long ID, int EmpStatus, string EmpNotes)
        {
            try
            {
                return Json(new { msg = (new TenantOnlineModel().UpdateEmpStatus(ID, EmpStatus, EmpNotes)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //sachin m 12 may
        public JsonResult getPreviousEmployementInfoPV(int id, long UserID)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().GetPriousEmploymentInfoPV(id,UserID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getPreviousAddressInfo(int id)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetPreviousAddressInfo(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //sachin M 12 may
        public JsonResult getPreviousAddressInfoPV(int id, long UserID)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetPreviousAddressInfoPV(id,UserID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //sachin m 13 may
        public JsonResult GetAppResidenceHistory(int id)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().GetAppResidenceHistory(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAppEmpHistory(int id)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().GetAppEmpHistory(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Sachin Mahore 21 Apr 2020
        public ActionResult CoApplicantDet(string id)
        {
            string[] ids = id.Split('-');
            ShomaRMEntities db = new ShomaRMEntities();
            string pid = ids[0].ToString();
            long uid = Convert.ToInt64(ids[1].ToString());

            ViewBag.PTOID = uid.ToString();
            if (ShomaGroupWebSession.CurrentUser == null && pid != "0")
            {
                return Redirect("/Account/Login");
            }

            var model = new OnlineProspectModule().GetProspectData(Convert.ToInt64(pid));

            if (Session["Bedroom"] != null)
            {
                model.Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                model.MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                model.MaxRent = Convert.ToDecimal(Session["MaxRent"].ToString());
                model.LeaseTermID = Convert.ToInt32(Session["LeaseTerm"].ToString());
                var leaseDet = new Areas.Admin.Models.LeaseTermsModel().GetLeaseTermsDetails(model.LeaseTermID);
                model.LeaseTerm = Convert.ToInt32(leaseDet.LeaseTerms);

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
                if (model.LeaseTermID == 0)
                {

                    var leaseDet = db.tbl_LeaseTerms.Where(p => p.LeaseTerms == 12).FirstOrDefault();
                    if (leaseDet != null)
                    {
                        model.LeaseTermID = leaseDet.LTID;
                    }
                    else
                    {
                        model.LeaseTermID = 0;
                    }
                }

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
        //Sachin Mahore 24 Apr 2020 2:30 PM
        public ActionResult GuarantorDet(string id)
        {
            string[] ids = id.Split('-');
            ShomaRMEntities db = new ShomaRMEntities();
            string pid = ids[0].ToString();
            long uid = Convert.ToInt64(ids[1].ToString());

            ViewBag.PTOID = uid.ToString();
            if (ShomaGroupWebSession.CurrentUser == null && pid != "0")
            {
                return Redirect("/Account/Login");
            }

            var model = new OnlineProspectModule().GetProspectData(Convert.ToInt64(pid));

            if (Session["Bedroom"] != null)
            {
                model.Bedroom = Convert.ToInt32(Session["Bedroom"].ToString());
                model.MoveInDate = Convert.ToDateTime(Session["MoveInDate"].ToString());
                model.MaxRent = Convert.ToDecimal(Session["MaxRent"].ToString());
                model.LeaseTermID = Convert.ToInt32(Session["LeaseTerm"].ToString());
                var leaseDet = new Areas.Admin.Models.LeaseTermsModel().GetLeaseTermsDetails(model.LeaseTermID);
                model.LeaseTerm = Convert.ToInt32(leaseDet.LeaseTerms);

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
                if (model.LeaseTermID == 0)
                {

                    var leaseDet = db.tbl_LeaseTerms.Where(p => p.LeaseTerms == 12).FirstOrDefault();
                    if (leaseDet != null)
                    {
                        model.LeaseTermID = leaseDet.LTID;
                    }
                    else
                    {
                        model.LeaseTermID = 0;
                    }
                }

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
        public ActionResult PrintApplicationForm(long TenantID)
        {
            try
            {
                return Json(new { filename =new ApplyNowModel().PrintApplicationForm(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { filename = "" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult PrintGuarantorForm(long TenantID)
        {
            try
            {
                return Json(new { filename = new ApplyNowModel().PrintGuarantorForm(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { filename = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintCoapplicantForm(long TenantID)
        {
            try
            {
                return Json(new { filename = new ApplyNowModel().PrintCoapplicantForm(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { filename = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintQuotation(PrintQuotationModel model)
        {
            try
            {
                return Json(new { filename = new ApplyNowModel().PrintQuotation(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { filename = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetEncDecSSNPassportIDNum(string EncDecVal, int EncDec)
        {
            try
            {
                return Json(new { result = new TenantOnlineModel().GetEncDecSSNPassportIDNum(EncDecVal, EncDec) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CheckApplicationStatus(long TenantID)
        {
            try
            {
                return Json(new { result = new TenantOnlineModel().CheckApplicationStatus(TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetTenantOnlineListProspectVerification(int id, long TenantID)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().GetTenantOnlineListProspectVerification(id, TenantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult VerifyQuotationNo(string QuotationNo)
        {
            try
            {
                return Json(new ApplyNowModel().VerifyQuotationNo(QuotationNo), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SignInUsingQuotationNo(string QuotationNo, string UserName, string Password, int IsTempPass)
        {
            try
            {
                return Json(new ApplyNowModel().SignInUsingQuotationNo(QuotationNo, UserName, Password, IsTempPass), JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetApplicantHistoryListForAdmin(long TenantID, long ApplicantUserId)
        {
            try
            {
                return Json(new { model = new ApplicantHistoryModel().GetApplicantHistoryListForAdmin(TenantID, ApplicantUserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult GetEmployerHistoryByAdmin(long TenantId, long ApplicantUserId)
        {
            try
            {
                return Json(new { model = new EmployerHistoryModel().GetEmployerHistoryByAdmin(TenantId, ApplicantUserId) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckUserNameAndPassword(long UserID, string EmailId, string OldPassword)
        {
            try
            {
                return Json(new { model = new ApplyNowModel().CheckUserNameAndPassword(UserID, EmailId, OldPassword) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintMonthlySummary(PrintMonthlySummary model)
        {
            try
            {
                return Json(new { filename = new ApplyNowModel().PrintMonthlySummary(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { filename = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Model(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (new ApplyNowModel().CheckModelExist(id.ToUpper()))
                {
                    Session["Model"] = id.ToUpper();
                }
            }
            return Redirect("/ApplyNow/Index/0");
        }


        //Sachin Mahore 08 June 2020
        public ActionResult GetBankCCList(long ApplicantID)
        {
            try
            {
                return Json(new { model = new BankCCModel().GetBankCCList(ApplicantID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult GetStaticApplicantValues(long id, long UID)
        {
            try
            {
                return Json(new { model = new StaticApplicantValues().GetStaticApplicantValues(id, UID) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { model = Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveUpdateAgreeSummary(long CurrentUserId,int isAgreeSummary)
        {
            try
            {
                return Json(new { model = new TenantOnlineModel().SaveUpdateAgreeSummary(CurrentUserId, isAgreeSummary) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { model = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}