using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShomaRM.Data;
using ShomaRM.Models;
using LoggerEngine;

namespace ShomaRM.Controllers
{
    [Authorize]
    public class AccountController : ShomaBaseController
    {
        private ShomaRMEntities db = new ShomaRMEntities();
        //public AccountController()
        //    : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        //{
        //}

        //public AccountController(UserManager<ApplicationUser> userManager)
        //{
        //    UserManager = userManager;
        //}

        //public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (returnUrl.ToString().ToLower().Contains("logoff"))
                    returnUrl = null;

            }
            catch
            {
                returnUrl = null;
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //string dec = new EncryptDecrypt().DecryptText("NCM2B4dAzCFueiZKQytVtg");
            string encryptedPassword = new EncryptDecrypt().EncryptText(model.Password);
            if (ModelState.IsValid)
            {
                var user = db.tbl_Login.Where(p => p.Username == model.UserName && p.Password == encryptedPassword && p.IsActive == 1).FirstOrDefault();
                if (user != null)
                {
                    
                    //LogError("Vijay Ramteke");
                    SignIn(model.UserName, model.RememberMe);
                    // Set Current User
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
                    // Store the Log.
                    var loginHistory = new tbl_LoginHistory
                    {
                        UserID = user.UserID,
                        IPAddress = Request.UserHostAddress,
                        PageName = "Home",
                        LoginDateTime = DateTime.Now,
                        SessionID = Session.SessionID.ToString()
                    };

                    db.tbl_LoginHistory.Add(loginHistory);
                    db.SaveChanges();

                    if (currentUser.TenantID == 0 && currentUser.UserType != 3 && currentUser.UserType != 33 && currentUser.UserType != 34)
                    {
                        return RedirectToAction("../Admin/AdminHome");
                    }
                    else if (currentUser.TenantID != 0)
                    {
                        return RedirectToAction("../Tenant/Dashboard");
                    }
                    else if (user.ParentUserID == null)
                    {
                        var checkExpiry = db.tbl_ApplyNow.Where(co => co.UserId == currentUser.UserID).FirstOrDefault();
                        checkExpiry.Status = (!string.IsNullOrWhiteSpace(checkExpiry.Status) ? checkExpiry.Status : "");
                        if ((checkExpiry.StepCompleted??0)==18 && checkExpiry.Status.Trim()=="")
                        {
                            return RedirectToAction("../ApplicationStatus/Index/"+(new EncryptDecrypt().EncryptText("In Progress")));
                        }
                        else if (checkExpiry.Status.Trim() == "Approved")
                        {
                            checkExpiry.StepCompleted = 18;
                            db.SaveChanges();
                            return RedirectToAction("../ApplicationStatus/Index/" + (new EncryptDecrypt().EncryptText("Approved")));
                        }
                        else if (checkExpiry.Status.Trim() == "Signed")
                        {
                            return RedirectToAction("../Checklist/");
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
                                    return RedirectToAction("../Home");
                                }
                                else
                                {
                                    if ((user.IsTempPass ?? 0) == 1)
                                    {
                                        string uide = new EncryptDecrypt().EncryptText(currentUser.UserID.ToString()+",1");
                                        return RedirectToAction("../ApplyNow/ChangePassword", new { uid = uide });
                                    }
                                    else
                                    {
                                        Session["DelDatAll"] = null;
                                        return RedirectToAction("../ApplyNow/Index/" + currentUser.UserID);
                                    }
                                }
                            }
                        }
                    }
                    else if (user.ParentUserID != null)
                    {
                        string uidd = new EncryptDecrypt().EncryptText(user.UserID.ToString() + "," + (user.IsTempPass ?? 0).ToString());
                        if (currentUser.UserType == 33)
                        {
                            var chkstatus = db.tbl_TenantOnline.Where(v => v.ParentTOID == user.UserID && v.ResidenceStatus==1 && v.EmpStatus==1).FirstOrDefault();
                            if (chkstatus!=null)
                            {
                                return RedirectToAction("../Checklist/CoApplicant");
                            }
                            else
                            {
                                if ((user.IsTempPass ?? 0) == 1)
                                {

                                    return RedirectToAction("../ApplyNow/ChangePassword", new { uid = uidd });
                                }
                                else
                                {
                                    return RedirectToAction("../ApplyNow/CoApplicantDet/" + user.ParentUserID + "-" + currentUser.UserID);
                                }
                            }
                        }
                        else if (currentUser.UserType == 34)
                        {
                            if ((user.IsTempPass ?? 0) == 1)
                            {
                                return RedirectToAction("../ApplyNow/ChangePassword", new { uid = uidd });
                            }
                            else
                            {
                                return RedirectToAction("../ApplyNow/GuarantorDet/" + user.ParentUserID + "-" + currentUser.UserID);
                            }
                        }
                    }
                    // return RedirectToLocal(returnUrl);
                }
                else
                {
                    user = db.tbl_Login.Where(p => p.Username == model.UserName).FirstOrDefault();
                    if (user == null)
                    {
                        ModelState.AddModelError("", "Invalid User Name OR Password OR Your quote has expired please register again.");
                        ViewBag.Error = 1;
                    }
                    else if (user != null && (user.IsActive??0)==1)
                    {
                        ModelState.AddModelError("", "Invalid password");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User is not active.");
                    }
                }
            }
            return View(model);
        }
        
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ForgotPasswordConfirmation

        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
       
        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ResetPasswordConfirmation

        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin



        //
        // POST: /Account/LogOff

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            (new ShomaGroupWebSession()).RemoveWebSession();
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            new CommonModel().AddPageLoginHistory("");
            try
            {
                var loginHistory = db.tbl_LoginHistory.Where(p => p.UserID == userid && p.SessionID == Session.SessionID.ToString() && p.LogoutDateTime == null).FirstOrDefault();

                if (loginHistory != null)
                {
                    loginHistory.LogoutDateTime = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {

            }
           

            return RedirectToAction("Login", "Account");
        }

        public ActionResult IsSession()
        {
            if (Session["CurrentUser"] != null)
            {
                return Json(new { IsLogOut = "0" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { IsLogOut = "1" }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult KeepLive()
        {
            return Json(new { result = "OK" }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public void SignIn(string userName, bool rememberMe)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(userName, rememberMe);
        }
    }
}