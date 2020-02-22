using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Models
{
    public class OnlineProspectModule
    {
        public long DocID { get; set; }
        public Nullable<long> ProspectusID { get; set; }
        public string Address { get; set; }
        public Nullable<int> DocumentType { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> VerificationStatus { get; set; }
        public string VerificationStatusString { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DocumentState { get; set; }
        public string DocumentIDNumber { get; set; }
        public long TenantID { get; set; }
        public long ID { get; set; }
        public Nullable<long> PropertyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public Nullable<int> IsApplyNow { get; set; }
        public string IsApplyNowStatus { get; set; }
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string AnnualIncome { get; set; }
        public string AddiAnnualIncome { get; set; }
        public Nullable<decimal> Charge_Amount { get; set; }
        public int PaymentID { get; set; }
        public int IsRentalPolicy { get; set; }
        public int IsRentalQualification { get; set; }
        
        public string Name_On_Card { get; set; }
        public string CardNumber { get; set; }
        public Nullable<int> CardMonth { get; set; }
        public Nullable<int> CardYear { get; set; }
        public Nullable<int> CCVNumber { get; set; }
        public Nullable<long> ProspectId { get; set; }
        public long SUID { get; set; }
        public int Marketsource { get; set; }
        public DateTime MoveInDate { get; set; }
        public string MoveInDateTxt { get; set; }
        public Nullable<decimal> ParkingAmt { get; set; }
        public Nullable<decimal> StorageAmt { get; set; }
        public Nullable<decimal> FOBAmt { get; set; }
        public Nullable<decimal> PetPlaceAmt { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<decimal> Rent { get; set; }
        public Nullable<decimal> PetDeposit { get; set; }
        public Nullable<decimal> TrashAmt { get; set; }
        public Nullable<decimal> PestAmt { get; set; }
        public Nullable<decimal> ConvergentAmt { get; set; }
        public Nullable<decimal> AdminFees { get; set; }
        public Nullable<decimal> ApplicationFees { get; set; }
        public Nullable<decimal> GuarantorFees { get; set; }
        public Nullable<decimal> VehicleRegistration { get; set; }
        public Nullable<decimal> Prorated_Rent { get; set; }
        public int LeaseTerm { get; set; }

        public Nullable<decimal> MonthlyCharges { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public string PetPlaceID { get; set; }
        public string ParkingSpaceID { get; set; }
        public string StorageSpaceID { get; set; }
        public int Bedroom { get; set; }
        public string Picture { get; set; }
        public decimal MaxRent { get; set; }
        public int FromHome { get; set; }
        public string ExpireDate { get; set; }
        public int StepNo { get; set; }
        public string EnvelopeID { get; set; }
        public List<PropertyUnits> lstPropertyUnit { get; set; }
        public List<PropertyFloor> lstPropertyFloor { get; set; }
      
        public List<EmailData> lstemailsend { get; set; }
        public string Building { get; set; }
        public int FloorID { get; set; }
        public string SaveOnlineProspect(OnlineProspectModule model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            long Uid = 0;
       
            if (model.ID == 0)
            {
                var loginDet = db.tbl_Login.Where(p => p.Email == model.Email).FirstOrDefault();
                if (loginDet == null)
                {
                    var saveUserNamePassword = new tbl_Login()
                    {
                        Username = model.Email,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        IsActive = 1,
                        TenantID = 0,
                        UserType = 3
                    };
                    db.tbl_Login.Add(saveUserNamePassword);
                    db.SaveChanges();
                    Uid = saveUserNamePassword.UserID;

                }
                else
                {
                    Uid = loginDet.UserID;
                }

                var saveOnlineProspect = new tbl_ApplyNow()
                {
                    PropertyId = model.PropertyId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Date = DateTime.Now,
                    Status = model.Status,
                    Address = model.Address,
                    Password = model.Password,
                    IsApplyNow = 1,
                    //CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                    CreatedDate = DateTime.Now,
                    DateofBirth = model.DateofBirth,
                    AnnualIncome = model.AnnualIncome,
                    AddiAnnualIncome = model.AddiAnnualIncome,
                    Marketsource = model.Marketsource,
                    UserId = Uid,
                    MoveInDate = model.MoveInDate,
                    LeaseTerm=model.LeaseTerm,
                };

                db.tbl_ApplyNow.Add(saveOnlineProspect);
                db.SaveChanges();
                model.ID = saveOnlineProspect.ID;

                var saveApplicant = new tbl_Applicant()
                {
                 
                    TenantID = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone,
                    Email = model.Email,
                    DateOfBirth = model.DateofBirth,
                    Gender = 0,
                    Type= "Primary Applicant",

                };
                db.tbl_Applicant.Add(saveApplicant);
                db.SaveChanges();

                var getAppldata = new tbl_TenantOnline()
                {

                    ProspectID = model.ID,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateofBirth,
                    Email = model.Email,
                    Mobile = model.Phone,
                    CreatedDate = DateTime.Now,
                    IsInternational=0,
                };
                db.tbl_TenantOnline.Add(getAppldata);
                db.SaveChanges();


                var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == model.PropertyId).FirstOrDefault();
                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                if (model != null)
                {
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                    reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'></br>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;  Thank you for registering on our fast and easy Leasing Portal!  Your account has been successfully created as follows:</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'></br>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;Username : [%TenantEmail%]</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'></br>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;We are excited you are considering us as your place to live.  If you need any assistance in completing your online application or have any questions about our community, </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'></br>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Please feel free to contact us at your convenience.  Our contact information with some highlights about our property is shown below. We look forward to serving you.</p>");

                    reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);
                    reportHTML = reportHTML.Replace("[%TenantAddress%]", model.Address);
                    reportHTML = reportHTML.Replace("[%LeaseStartDate%]", DateTime.Now.ToString());
                    reportHTML = reportHTML.Replace("[%LeaseEndDate%]", DateTime.Now.AddMonths(13).ToString());
                    reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                    reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                    reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%TenantEmail%]", model.Email);
                    reportHTML = reportHTML.Replace("[%TenantPassword%]", model.Password);
                    reportHTML = reportHTML.Replace("[%QuoteNo%]", model.ID.ToString());
                    reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
                }
                string body = reportHTML;
                new EmailSendModel().SendEmail(model.Email, "Application Submission", body);
            }
            msg = model.ID.ToString() + "|Online Prospect Save Successfully|" +Uid;

            var currentUser = new CurrentUser();
            currentUser.UserID =Convert.ToInt32(Uid);
            (new ShomaGroupWebSession()).SetWebSession(currentUser);
            db.Dispose();
            return msg;
        }
        public string UpdateOnlineProspect(OnlineProspectModule model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            long Uid = 0;
            DateTime? moveindate = null;
            if (model.MoveInDate != DateTime.MinValue)
            {
                moveindate = model.MoveInDate;
            }
            if (model.ID != 0)
            {
                var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ID).FirstOrDefault();
                if (onlineProspectData != null)
                {
                    onlineProspectData.ParkingAmt = model.ParkingAmt;
                    onlineProspectData.StorageAmt = model.StorageAmt;
                    onlineProspectData.PetPlaceAmt = model.PetPlaceAmt;
                    onlineProspectData.PestAmt = model.PestAmt;
                    onlineProspectData.ConvergentAmt = model.ConvergentAmt;
                    onlineProspectData.TrashAmt = model.TrashAmt;
                    //onlineProspectData.MoveInDate = moveindate;
                    onlineProspectData.MonthlyCharges = model.MonthlyCharges;
                    onlineProspectData.MoveInCharges = model.MoveInCharges;
                    onlineProspectData.PetDeposit = model.PetDeposit;
                    onlineProspectData.FOBAmt = model.FOBAmt;
                    onlineProspectData.Deposit = model.Deposit;
                    onlineProspectData.Rent = model.Rent;
                    onlineProspectData.VehicleRegistration = model.VehicleRegistration;
                    onlineProspectData.Prorated_Rent = model.Prorated_Rent;
                    onlineProspectData.AdministrationFee = model.AdminFees;
                    onlineProspectData.LeaseTerm = model.LeaseTerm;
                    db.SaveChanges();
                }
                
                
            }
            msg = model.ID.ToString() + "|Quote Updated Successfully";
            db.Dispose();
            return msg;
        }
        public string SaveCheckPolicy(OnlineProspectModule model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            long Uid = 0;
            DateTime? moveindate = null;
            if (model.MoveInDate != DateTime.MinValue)
            {
                moveindate = model.MoveInDate;
            }
            if (model.ID != 0)
            {
                var onlineProspectData = db.tbl_ApplyNow.Where(p => p.ID == model.ID).FirstOrDefault();
            
                if (onlineProspectData.IsRentalPolicy ==null)
                {
                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    if (model != null)
                    {
                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", "  <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We noticed you begun your application process.  Please note for your convenience, the application remains active for 48 hours; however, if the application is  not completed and submitted within 48 hours, you will need to start over.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;   If you have any questions or need assistance in completing the application, please do not hesitate to call us.  We are here to assist you!  </p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;We know you will love your new home and are excited to have you reside here. </p>");

                        reportHTML = reportHTML.Replace("[%TenantName%]", model.FirstName + " " + model.LastName);

                        reportHTML = reportHTML.Replace("[%TenantEmail%]", model.Email);

                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(model.Email, "Application Submission", body);
                }
                if (onlineProspectData != null)
                {
                    onlineProspectData.IsRentalPolicy = model.IsRentalPolicy;
                    onlineProspectData.IsRentalQualification = model.IsRentalQualification;

                    db.SaveChanges();
                }
            }
            msg = "Policy Checked Successfully";
            db.Dispose();
            return msg;
        }
        public OnlineProspectModule GetApplyNowList(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            OnlineProspectModule lstpr = new OnlineProspectModule();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_ApplyNowList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "id";
                    paramF.Value = id;
                    cmd.Parameters.Add(paramF);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    lstpr.FirstName = dr["FirstName"].ToString();
                    lstpr.LastName = dr["LastName"].ToString();
                    lstpr.Email = dr["Email"].ToString();
                    lstpr.Phone = dr["Phone"].ToString();
                    lstpr.Address = dr["Address"].ToString();

                }
                db.Dispose();
                return lstpr;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public OnlineProspectModule GetProspectData(long Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            OnlineProspectModule model = new OnlineProspectModule();
            model.ProspectId = 0;
            model.IsApplyNow = 1;
            model.IsApplyNowStatus = "New";
            model.ParkingAmt = 0;
            model.PetPlaceAmt = 0;
            model.StorageAmt = 0;
            model.PetPlaceID = "0";
            model.ParkingSpaceID = "0";
            model.StorageSpaceID = "0";
            model.FOBAmt = 0;
            model.EnvelopeID = "";
            model.LeaseTerm = 12;
            if (Id != 0)
            {
                var GetProspectData = db.tbl_ApplyNow.Where(p => p.UserId == Id).FirstOrDefault();
               
                if (GetProspectData != null)
                {
                    var GetPaymentProspectData = db.tbl_OnlinePayment.Where(p => p.ProspectId == GetProspectData.ID).FirstOrDefault();
                    var GetDocumentVerificationData = db.tbl_DocumentVerification.Where(p => p.ProspectusID == GetProspectData.ID).FirstOrDefault();

                    model.FirstName = GetProspectData.FirstName;
                    model.LastName = GetProspectData.LastName;
                    model.Email = GetProspectData.Email;
                    model.Phone = GetProspectData.Phone.ToString();
                    model.Date = GetProspectData.Date;
                    model.Status = GetProspectData.Status;
                    model.Address = GetProspectData.Address;
                    model.IsApplyNow = GetProspectData.IsApplyNow;
                    model.DateofBirth = GetProspectData.DateofBirth;
                    model.AnnualIncome = GetProspectData.AnnualIncome;
                    model.AddiAnnualIncome = GetProspectData.AddiAnnualIncome;
                    model.IsApplyNowStatus = GetProspectData.IsApplyNow == 1 ? "Pending" : GetProspectData.IsApplyNow == 2 ? "Approved" : "Rejected";
                    model.PropertyId = GetProspectData.PropertyId;
                    model.ProspectId = GetProspectData.ID;
                    model.TenantID = Convert.ToInt64(GetProspectData.UserId);
                    model.Password = GetProspectData.Password;
                    model.Marketsource = Convert.ToInt32(GetProspectData.Marketsource);
                    model.CreatedDate = Convert.ToDateTime(GetProspectData.CreatedDate);
                    model.IsRentalQualification = Convert.ToInt32(GetProspectData.IsRentalQualification);
                    model.IsRentalPolicy = Convert.ToInt32(GetProspectData.IsRentalPolicy);
                    model.ParkingAmt = GetProspectData.ParkingAmt;
                    model.PetPlaceAmt = GetProspectData.PetPlaceAmt;
                    model.StorageAmt = GetProspectData.StorageAmt;
                    model.MoveInCharges = GetProspectData.MoveInCharges;
                    model.MonthlyCharges = GetProspectData.MonthlyCharges;
                    model.PetDeposit = GetProspectData.PetDeposit;
                    model.FOBAmt = 0;
                    model.EnvelopeID = GetProspectData.EnvelopeID==null?"" : GetProspectData.EnvelopeID;
                    model.LeaseTerm = Convert.ToInt32(GetProspectData.LeaseTerm);

                    DateTime? dateExpire = null;
                    try
                    {

                        dateExpire = Convert.ToDateTime(GetProspectData.CreatedDate.ToString());
                    }
                    catch
                    {
                    }

                    model.MoveInDateTxt = GetProspectData.MoveInDate.Value.ToString("MM/dd/yyyy");
                    model.ExpireDate = Convert.ToDateTime(GetProspectData.CreatedDate).AddHours(72).ToString("MM/dd/yyyy hh:mm tt");


                    if (GetPaymentProspectData != null)
                    {
                        model.Name_On_Card = GetPaymentProspectData.Name_On_Card;
                        model.CardNumber = GetPaymentProspectData.CardNumber;
                        model.CardMonth = GetPaymentProspectData.CardMonth;
                        model.CardYear = GetPaymentProspectData.CardYear;
                        model.CCVNumber = GetPaymentProspectData.CCVNumber;
                    }
                    if (GetDocumentVerificationData != null)
                    {
                        model.DocumentName = GetDocumentVerificationData.DocumentName;
                    }

                    var getPetPlace = db.tbl_TenantPetPlace.Where(p => p.TenantID == GetProspectData.ID).FirstOrDefault();
                    if(getPetPlace!=null)
                    {
                        model.PetPlaceID = getPetPlace.PetPlaceID.ToString();

                    }
                    var getParkingPlace = db.tbl_TenantParking.Where(p => p.TenantID == GetProspectData.ID).FirstOrDefault();
                    if(getParkingPlace!=null)
                    {
                        model.ParkingSpaceID = getParkingPlace.ParkingID.ToString();
                    }
                    var getStoragePlace = db.tbl_TenantStorage.Where(p => p.TenantID == GetProspectData.ID).FirstOrDefault();
                    if (getStoragePlace != null)
                    {
                        model.StorageSpaceID = getStoragePlace.StorageID.ToString();
                    }

                    var getUnitDet = db.tbl_PropertyUnits.Where(p => p.UID == GetProspectData.PropertyId).FirstOrDefault();
                    if(getUnitDet!=null)
                    {
                        model.Building = getUnitDet.Building;
                        model.FloorID = Convert.ToInt32(getUnitDet.FloorNo);
                    }
                   
                }
            }

            List<PropertyFloor> listfloor = new List<PropertyFloor>();
            model.lstPropertyFloor = listfloor;
            var propDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
            if (propDet != null)
            {
                
                model.Picture = propDet.Picture;
                model.ConvergentAmt = propDet.ConversionBillFees;
                model.PestAmt = propDet.PestControlFees;
                model.TrashAmt = propDet.TrashFees;
                model.AdminFees = propDet.AdminFees;
                model.ApplicationFees = propDet.ApplicationFees;

                model.GuarantorFees = propDet.GuarantorFees;
            }
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetPropertyFloorCord";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramPID = cmd.CreateParameter();
                    paramPID.ParameterName = "PID";
                    paramPID.Value = 8;
                    cmd.Parameters.Add(paramPID);


                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "AvailableDate";
                    paramF.Value = DateTime.Now;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramB = cmd.CreateParameter();
                    paramB.ParameterName = "Bedroom";
                    paramB.Value = 0;
                    cmd.Parameters.Add(paramB);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "MaxRent";
                    paramC.Value = 10000;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    PropertyFloor pr = new PropertyFloor();

                    pr.FloorID = Convert.ToInt32(dr["FloorID"].ToString());
                    //pr.FloorNo = dr["FloorNo"].ToString();
                    pr.Coordinates = dr["Coordinates"].ToString();

                    pr.IsAvail = Convert.ToInt32(dr["IsAvail"].ToString());

                    model.lstPropertyFloor.Add(pr);
                }
                db.Dispose();
                //return lstUnitProp.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
            model.ID = Id;
            return model;
        }
        public string ScheduleEmail()
        {
            string msg = "";
            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            string body = "";
            ShomaRMEntities db = new ShomaRMEntities();
          
                DateTime nn = DateTime.Now.AddHours(-50);
                DateTime dd = DateTime.Now.AddHours(-48);
                var GetTenantDet = db.tbl_ApplyNow.Where(p => p.CreatedDate >= nn && p.CreatedDate <= dd).ToList();
                
                if (GetTenantDet != null)
                {

                    foreach (var cd in GetTenantDet)
                    {
                        body = "";
                        reportHTML = "";
                        reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                        reportHTML = reportHTML.Replace("[%TenantName%]", cd.FirstName + " " + cd.LastName);
                        reportHTML = reportHTML.Replace("[%TenantEmail%]", cd.Email);

                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Final Notification to complete your Application");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", "<p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;                                           This is your final notification that your application has not been completed.  Unless the application is submitted today, it will be deleted and you will be kindly asked to reenter the information.</p><p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We know you will love your new home in our project, so don’t delay and send us your application now.  We are eager to welcome you into our community.</p>");
                        body = reportHTML;

                        new EmailSendModel().SendEmail(cd.Email, "Final Notification to complete your Application", body);

                    }
                }
          
                DateTime nnn = DateTime.Now.AddHours(-26);
                DateTime ddd = DateTime.Now.AddHours(-24);
                var GetTenantDetn = db.tbl_ApplyNow.Where(p => p.CreatedDate >= nnn && p.CreatedDate <= ddd).ToList();
               
                if (GetTenantDetn != null)
                {

                    foreach (var cd in GetTenantDetn)
                    {
                        body = "";
                        reportHTML = "";
                        reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                        reportHTML = reportHTML.Replace("[%TenantName%]", cd.FirstName + " " + cd.LastName);
                        reportHTML = reportHTML.Replace("[%TenantEmail%]", cd.Email);

                        reportHTML = reportHTML.Replace("[%EmailHeader%]", "Reminder to complete your Application");
                        reportHTML = reportHTML.Replace("[%EmailBody%]", "<p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; We are sending you a kindly reminder that your application has not been completed.  Finishing is fast and easy, so log in and get started.  You are just a few steps away from submitting your application.  We are here to help, so if you need any assistance, please do not hesitate to contact us.”</p>");

                        body = reportHTML;
                        new EmailSendModel().SendEmail(cd.Email, "Reminder to complete your Application", body);


                    }
                }
           
      

            msg = "Email Send Successfully";
            return msg;

        }
        public string SendCoappEmail(OnlineProspectModule model)
        {
            string msg = "";
            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect2.html");
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.lstemailsend != null)
            {
                var GetTenantDet = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();
                
                var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == GetTenantDet.PropertyId).FirstOrDefault();
                foreach (var cd in model.lstemailsend)
                {
                    var GetCoappDet = db.tbl_Applicant.Where(c => c.Email ==cd.AppEmail).FirstOrDefault();
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                    reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + GetCoappDet.FirstName + " " + GetCoappDet.LastName + "</b>,<br/>Your Online application submitted successfully. Please login to see status. <br/><br/><u><b>User Credentials</br></b></u> </br> </br> User ID :" + model.Email + " </br>Password :" + model.Password);

                    reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);
                   
                    reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                    reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                    reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%TenantEmail%]", cd.AppEmail);
                 
                    reportHTML = reportHTML.Replace("[%QuoteNo%]", model.ID.ToString());
                    reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(cd.AppEmail, "Application Submission", body);
                }
            }

            msg = "Email Send Successfully";
            return msg;

        }
        public string SendPayLinkEmail(OnlineProspectModule model)
        {
            string msg = "";
            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect2.html");
            ShomaRMEntities db = new ShomaRMEntities();
            if (model.Email != null)
            {
                var GetTenantDet = db.tbl_ApplyNow.Where(p => p.ID == model.ProspectId).FirstOrDefault();

                var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == GetTenantDet.PropertyId).FirstOrDefault();
              
                var GetCoappDet = db.tbl_Applicant.Where(c => c.Email ==model.Email).FirstOrDefault();
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Fee Payment Link");
                    reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + GetCoappDet.FirstName + " " + GetCoappDet.LastName + "</b>,<br/>Your Online application submitted successfully. Please click below to Pay Application fees. <br/><br/><u><b>Payment Link :<a href=''></a> </br></b></u>  </br>");

                    reportHTML = reportHTML.Replace("[%TenantName%]", GetCoappDet.FirstName + " " + GetCoappDet.LastName);

                    reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                    reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                    reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%TenantEmail%]", model.Email);

                    reportHTML = reportHTML.Replace("[%QuoteNo%]", model.ID.ToString());
                    reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(model.Email, "Application Fee Payment Link", body);
               
            }

            msg = "Email Send Successfully";
            return msg;

        }
        public string DeleteApplicantTenantID(long TenantID, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (TenantID != 0)
            {

                var appliTenantData = db.tbl_Applicant.Where(p => p.TenantID == TenantID).ToList();
                if (appliTenantData != null)
                {
                    var testData = appliTenantData.Where(p => p.Type != "Primary Applicant").ToList();
                    if (testData != null)
                    {
                        db.tbl_Applicant.RemoveRange(testData);
                        db.SaveChanges();
                    }

                }

                var appliHistoryTenantData = db.tbl_ApplicantHistory.Where(p => p.TenantID == TenantID).ToList();
                if (appliHistoryTenantData != null)
                {
                    db.tbl_ApplicantHistory.RemoveRange(appliHistoryTenantData);
                    db.SaveChanges();
                }

                var appliTenantParkingData = db.tbl_TenantParking.Where(p => p.TenantID == TenantID).ToList();
                if (appliTenantParkingData != null)
                {
                    db.tbl_TenantParking.RemoveRange(appliTenantParkingData);
                    db.SaveChanges();
                }


                var appliTenantPetData = db.tbl_TenantPet.Where(p => p.TenantID == TenantID).ToList();
                if (appliTenantPetData != null)
                {
                    db.tbl_TenantPet.RemoveRange(appliTenantPetData);
                    db.SaveChanges();
                }

                var appliTenantPetPlaceData = db.tbl_TenantPetPlace.Where(p => p.TenantID == TenantID).ToList();
                if (appliTenantPetPlaceData != null)
                {
                    db.tbl_TenantPetPlace.RemoveRange(appliTenantPetPlaceData);
                    db.SaveChanges();
                }

                var appliTenantStorageData = db.tbl_TenantStorage.Where(p => p.TenantID == TenantID).ToList();
                if (appliTenantStorageData != null)
                {
                    db.tbl_TenantStorage.RemoveRange(appliTenantStorageData);
                    db.SaveChanges();
                }

                var appliTenantVehicleData = db.tbl_Vehicle.Where(p => p.TenantID == TenantID).ToList();
                if (appliTenantVehicleData != null)
                {
                    db.tbl_Vehicle.RemoveRange(appliTenantVehicleData);
                    db.SaveChanges();
                }

                var appliTenantOnlineData = db.tbl_TenantOnline.Where(p => p.ProspectID == TenantID).FirstOrDefault();
                if (appliTenantOnlineData != null)
                {
                    db.tbl_TenantOnline.Remove(appliTenantOnlineData);
                    db.SaveChanges();
                }
                var appliApplyNowData = db.tbl_ApplyNow.Where(p => p.ID == TenantID).FirstOrDefault();
                if (appliApplyNowData != null)
                {
                    db.tbl_ApplyNow.Remove(appliApplyNowData);
                    db.SaveChanges();
                }
                var appliLoginData = db.tbl_Login.Where(p => p.UserID == UserId).FirstOrDefault();
                if (appliLoginData != null)
                {
                    db.tbl_Login.Remove(appliLoginData);
                    db.SaveChanges();
                }
                msg = "Applicant Removed Successfully";

            }
            db.Dispose();
            return msg;
        }
    }

    public class DocumentVerificationModule
    {
         public long DocID { get; set; }
        public Nullable<long> ProspectusID { get; set; }
        public string Address { get; set; }
        public Nullable<int> DocumentType { get; set; }
        public string DocumentName { get; set; }
        public Nullable<int> VerificationStatus { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string DocumentState { get; set; }
        public string DocumentIDNumber { get; set; }    

        public string SaveDocumentVerifications(HttpPostedFileBase fb, DocumentVerificationModule model)
        {
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            if (fb != null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/") + "/" + sysFileName;

                }
            }


            if (model.DocID == 0)
            {
                var saveDocumentVerification = new tbl_DocumentVerification()
                {
                    ProspectusID = model.ProspectusID,
                    DocumentType = model.DocumentType,
                    DocumentName = sysFileName,
                    DocumentState = model.DocumentState,
                    DocumentIDNumber = model.DocumentIDNumber,
                    VerificationStatus = 0,
                    //CreatedBy = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID,
                    CreatedDate = DateTime.Now.Date
                };
                db.tbl_DocumentVerification.Add(saveDocumentVerification);
                db.SaveChanges();
                msg = "Document Verification Save Successfully";
            }
            db.Dispose();
            return msg;
        }

    }
    public class EmailData
    {
        public string AppEmail { get; set; }
    }
}