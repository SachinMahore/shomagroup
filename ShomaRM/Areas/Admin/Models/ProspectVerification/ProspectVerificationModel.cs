using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;
using ShomaRM.Models;
using System.Xml.Serialization;
using System.Web.Configuration;
using ShomaRM.Models.TwilioApi;
using ShomaRM.Models.Bluemoon;

namespace ShomaRM.Areas.Admin.Models
{
    public class ProspectVerificationModel
    {
        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];
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
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CCVNumber { get; set; }
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

        public Nullable<decimal> MonthlyCharges { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public Nullable<decimal> Prorated_Rent { get; set; }
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
        public string MarketsourceString { get; set; }
        public List<PropertyUnits> lstPropertyUnit { get; set; }
        public List<PropertyFloor> lstPropertyFloor { get; set; }

        public List<EmailData> lstemailsend { get; set; }

        public long MIID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public string MoveInTime { get; set; }
     
        public string InsuranceDoc { get; set; }
        public string ElectricityDoc { get; set; }
        public Nullable<int> IsCheckPO { get; set; }
        public Nullable<int> IsCheckATT { get; set; }
        public Nullable<int> IsCheckWater { get; set; }
        public int LeaseTerm { get; set; }
        public string EsignatureID { get; set; }
        public Nullable<decimal> PetDNAAmt { get; set; }
        public int LeaseTermID { get; set; }
    

        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];

        public List<ProspectVerificationModel> GetProspectVerificationList(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ProspectVerificationModel> lstpr = new List<ProspectVerificationModel>();
            try
            {
              
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectVerificationList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "FromDate";
                    paramF.Value = FromDate;
                    cmd.Parameters.Add(paramF);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "ToDate";
                    paramC.Value = ToDate;
                    cmd.Parameters.Add(paramC);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ProspectVerificationModel pr = new ProspectVerificationModel();
                    pr.ID = Convert.ToInt64(dr["ID"].ToString());
                    pr.DocID =Convert.ToInt64(dr["DocID"].ToString());
                    pr.FirstName = dr["FirstName"].ToString();
                    pr.LastName = dr["LastName"].ToString();
                    pr.Phone = dr["Phone"].ToString();
                    pr.Email = dr["Email"].ToString();
                    //pr.DocumentTypeText = GetDocumentType(dr["DocumentType"].ToString());
                    pr.DocumentName = dr["DocumentName"].ToString();
                    pr.VerificationStatus = Convert.ToInt32(dr["VerificationStatus"].ToString());
                    lstpr.Add(pr);
                }
                db.Dispose();
                return lstpr.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public int BuildPaganationProspectVerifyList(ProspectVerifySearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectVerifyPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "FromDate";
                    paramDF.Value = model.FromDate;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "ToDate";
                    paramDT.Value = model.ToDate;
                    cmd.Parameters.Add(paramDT);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramUn = cmd.CreateParameter();
                    paramUn.ParameterName = "UnitNo";
                    paramUn.Value = model.UnitNo == null ? model.UnitNo : "Unit " + model.UnitNo;
                    cmd.Parameters.Add(paramUn);


                    DbParameter paramAs = cmd.CreateParameter();
                    paramAs.ParameterName = "Astatus";
                    paramAs.Value = model.ApplicationStatus;
                    cmd.Parameters.Add(paramAs);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = model.SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = model.OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                if (dtTable.Rows.Count > 0)
                {
                    NOP = int.Parse(dtTable.Rows[0]["NOP"].ToString());
                }
                db.Dispose();
                return NOP;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        //Get BackgroundScreening data list by userId
        public List<BackgroundScreeningModel> FillProspectBackgroundScreening(long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var GetTenantDet = db.tbl_ApplyNow.Where(p => p.UserId == UserId).FirstOrDefault();
            var bgscrData = db.tbl_BackgroundScreening.Where(a => a.TenantId == GetTenantDet.ID).ToList();

            List<BackgroundScreeningModel> bgScrLIst = new List<BackgroundScreeningModel>();
            try
            {                
                foreach (var bgscr in bgscrData)
                {
                    BackgroundScreeningModel bgScr = new BackgroundScreeningModel();
                    bgScr.TenantId = bgscr.TenantId;
                    bgScr.OrderID = bgscr.OrderID;
                    bgScr.Status = bgscr.Status;
                    bgScr.PDFUrl = bgscr.PDFUrl;
                    bgScr.Type = bgscr.Type;
                    bgScrLIst.Add(bgScr);
                }
                db.Dispose();
                return bgScrLIst;
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public List<ProspectVerifySearchModel> FillProspectVerifySearchGrid(ProspectVerifySearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ProspectVerifySearchModel> lstProspectVerify = new List<ProspectVerifySearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectVerifyPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "FromDate";
                    paramDF.Value = model.FromDate;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "ToDate";
                    paramDT.Value = model.ToDate;
                    cmd.Parameters.Add(paramDT);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramUn = cmd.CreateParameter();
                    paramUn.ParameterName = "UnitNo";
                    paramUn.Value = model.UnitNo == null ? model.UnitNo : "Unit " + model.UnitNo;
                    cmd.Parameters.Add(paramUn);


                    DbParameter paramAs = cmd.CreateParameter();
                    paramAs.ParameterName = "Astatus";
                    paramAs.Value = model.ApplicationStatus;
                    cmd.Parameters.Add(paramAs);

                    DbParameter param5 = cmd.CreateParameter();
                    param5.ParameterName = "SortBy";
                    param5.Value = model.SortBy;
                    cmd.Parameters.Add(param5);

                    DbParameter param6 = cmd.CreateParameter();
                    param6.ParameterName = "OrderBy";
                    param6.Value = model.OrderBy;
                    cmd.Parameters.Add(param6);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    ProspectVerifySearchModel searchmodel = new ProspectVerifySearchModel();
                    searchmodel.ID = Convert.ToInt64(dr["ID"].ToString());
                    searchmodel.UserID = Convert.ToInt64(dr["UserID"].ToString());
                    searchmodel.DocID = Convert.ToInt64(dr["DocID"].ToString());
                    searchmodel.FirstName = dr["FirstName"].ToString();
                    searchmodel.LastName = dr["LastName"].ToString();
                    searchmodel.Phone = dr["Phone"].ToString();
                    searchmodel.Email = dr["Email"].ToString();
                    searchmodel.DocumentTypeText = dr["DocumentType"].ToString();
                    searchmodel.DocumentName = dr["DocumentName"].ToString();
                    searchmodel.VerificationStatus = Convert.ToInt32(dr["VerificationStatus"].ToString());
                    searchmodel.CreatedDate = dr["CreatedDate"].ToString();
                    searchmodel.PropertyId = Convert.ToInt64(dr["PropertyId"].ToString());
                    searchmodel.UnitNo = dr["UnitNo"].ToString();
                    searchmodel.ApplicationStatus = dr["ApplicationStatus"].ToString();
                    lstProspectVerify.Add(searchmodel);
                }
                db.Dispose();
                return lstProspectVerify.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public string SaveScreeningStatus(string Email, long ProspectId, string Status)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            //var tenantData = db.tbl_TenantOnline.Where(p => p.ProspectID == ProspectId).FirstOrDefault();
            ShomaRM.Models.TenantOnlineModel model = new ShomaRM.Models.TenantOnlineModel();

            var tenantData = model.GetTenantOnlineList(Convert.ToInt32(ProspectId));
            var GetTenantDet = db.tbl_ApplyNow.Where(p => p.ID == ProspectId).FirstOrDefault();

            model.FirstName = tenantData.FirstName;
            model.MiddleInitial = tenantData.MiddleInitial;
            model.LastName = tenantData.LastName;
            model.DateOfBirthTxt = tenantData.DateOfBirthTxt;
            model.SSN = tenantData.SSN;
            model.Gender = tenantData.Gender;
            model.IDNumber = tenantData.IDNumber;
            model.JobTitle = tenantData.JobTitle;
            model.HomeAddress1 = tenantData.HomeAddress1;
            model.CityHome = tenantData.CityHome;
            model.StateHomeString = tenantData.StateHomeString;
            model.ZipHome = tenantData.ZipHome;
            model.Country = tenantData.Country;
            model.EmployerName = tenantData.EmployerName;
            model.JobTitle = tenantData.JobTitle;
            model.Income = tenantData.Income;
            model.SupervisorName = tenantData.SupervisorName;
            model.SupervisorPhone = tenantData.SupervisorPhone;            
            model.OfficeCity = tenantData.OfficeCity;
            model.OfficeState = tenantData.OfficeState;
            model.StartDateTxt = tenantData.StartDateTxt;
            //model.Reason = tenantData.Reason;
            model.ProspectID = ProspectId;


            if(GetTenantDet!=null)
            {
                if ((GetTenantDet.IsApplyNow ?? 0) == 2)
                {
                    var test = new AcutraqRequest();
                    var acuResult = test.PostAqutraqRequest(model);
                }
            }

            string msg = "";
            string reportHTML = "";
            string reportHTMLCoapp = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect3.html");
            reportHTMLCoapp = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect3.html");
            string message = "";


            if (Email != null)
            {
                
                if (GetTenantDet != null)
                {
                    GetTenantDet.IsApplyNow = 5;
                    GetTenantDet.Status = Status;
                    db.SaveChanges();
                }
                var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == GetTenantDet.PropertyId).FirstOrDefault();

                var GetCoappDet = db.tbl_Applicant.Where(c => c.TenantID == ProspectId && c.Type == "Primary Applicant").FirstOrDefault();
                var GetCoappList = db.tbl_Applicant.Where(c => c.TenantID == ProspectId && c.Type != "Primary Applicant" && c.Type != "Guarantor").ToList();
                string phonenumber = GetTenantDet.Phone;

                reportHTML = reportHTML.Replace("[%EmailHeader%]", "Online Application Status");
                reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + GetTenantDet.FirstName + " " + GetTenantDet.LastName + "</b>,<br/>Your Online application submitted successfully. Please click below to Pay Application fees. <br/><br/><u><b>Payment Link :<a href=''></a> </br></b></u>  </br>");
                reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantDet.FirstName + " " + GetTenantDet.LastName);
                var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();
                string sub = "Online Application Status";
                if (Status == "Approved")
                {
                   
                    string payid = new EncryptDecrypt().EncryptText(GetCoappDet.ApplicantID.ToString() + ",3," + propertDet.AdminFees.Value.ToString("0.00"));
                    reportHTML = reportHTML.Replace("[%Status%]", "Congratulations ! Your Application is Approved and Pay your Administration Fees");
                    reportHTML = reportHTML.Replace("[%StatusDet%]", "Good news!You have been approved.We welcome you to our community.Your next step is to pay the Administration fee of $350.00 to ensure your unit is reserved until you move -in. Once you process your payment, you will be directed to prepare your lease.  ");
                    // reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\""+ serverURL+"/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\""+ serverURL+ "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Review & Sign Document</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                    message = "Notification: Your Application is Approved and pay your Administration Fees. Please check the email for detail.";

                   
                }
                else if (Status == "Denied")
                {
                    reportHTML = reportHTML.Replace("[%Status%]", "Sorry ! Your Application is Denied");
                    reportHTML = reportHTML.Replace("[%StatusDet%]", "We are sorry that your application has been denied.  If your situation changes in the future, we would love the opportunity to welcome you into our community.");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");

                    message = "Sorry ! Your Application is Denied. Please check the email for detail.";
                }
                else if (Status == "Signed")
                {
                    sub = "Lease has been finalized : Pay your Move In Charges & Accept Move In Checklist";
                   
                    reportHTML = reportHTML.Replace("[%Status%]", "Lease has been finalized : Pay your Move In Charges & Accept Move In Checklist");
                    reportHTML = reportHTML.Replace("[%StatusDet%]", "Your Application is Signed by All Applicants and Pay your Move In Charges. Your next step is to pay the Move In Charges and Accept Move in Checklist  ");
                   reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\""+ serverURL+"/Account/Login\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\""+ serverURL+ "/Account/Login\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Accept Move In Checklist & Pay Charges</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                 
                    message = "Notification: Pay your Move In Charges and Accept Move In Checklist.";

                    if (GetCoappDet != null)
                    {
                        foreach (var app in GetCoappList)
                        {
                            if (app.MoveInPercentage >= 0)
                            {
                                string mpayid = new EncryptDecrypt().EncryptText(app.ApplicantID.ToString() + ",2," + (((GetTenantDet.MoveInCharges * app.MoveInPercentage) / 100).Value.ToString("0.00")));

                                reportHTMLCoapp = reportHTMLCoapp.Replace("[%Status%]", "Congratulations ! Your Application is Approved");
                                reportHTMLCoapp = reportHTMLCoapp.Replace("[%EmailHeader%]", app.MoveInPercentage + "% Move In charges Payment Link");
                                reportHTMLCoapp = reportHTMLCoapp.Replace("[%StatusDet%]", "Hi <b>" + app.FirstName + " " + app.LastName + "</b>,<br/>Your Online application is Approved. Please click below to pay " + app.MoveInPercentage + "% Move In Charges $" + (((GetTenantDet.MoveInCharges * app.MoveInPercentage) / 100).Value.ToString("0.00")) + ". <br/><br/><u><b>Payment Link :<a href=''></a> </b></u> ");
                                reportHTMLCoapp = reportHTMLCoapp.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + app.ApplicantID + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + mpayid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                                reportHTMLCoapp = reportHTMLCoapp.Replace("[%TenantName%]", app.FirstName + " " + app.LastName);
                                string bodyCoapp = reportHTMLCoapp;
                                new EmailSendModel().SendEmail(app.Email, "Move In charges Payment Link", bodyCoapp);
                                if (SendMessage == "yes")
                                {
                                    new TwilioService().SMS(app.Phone, "Congratulations ! Your Application is Approved. Please check the email for Move In charges Payment Link.");
                                }
                            }
                        }
                    }

                }
                else
                {
                    reportHTML = reportHTML.Replace("[%Status%]", "Congratulations ! Your Application is Approved with Condition");
                    reportHTML = reportHTML.Replace("[%StatusDet%]", "Your application has been approved with conditions.  Kindly click here to call our office or schedule an appointment to discuss your options.  We look forward to assisting you in becoming a member of our community.");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");

                    message = "Congratulations ! Your Application is Approved with Condition. Please check the email for detail.";
                }
            
                reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString("0.00"));
                reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString("0.00"));
                reportHTML = reportHTML.Replace("[%TenantEmail%]", Email);

                reportHTML = reportHTML.Replace("[%QuoteNo%]", ID.ToString());
                reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Sanctuary Doral");
                string body = reportHTML;
                new EmailSendModel().SendEmail(Email,sub , body);
                if (SendMessage == "yes")
                {
                    new TwilioService().SMS(phonenumber, message);
                }

            }

            msg = "Email Send Successfully";
            return msg;

        }

        public string SendReminderEmail(long ProspectId, int RemType, long ApplicantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();

            ShomaRM.Models.TenantOnlineModel model = new ShomaRM.Models.TenantOnlineModel();

            var tenantData = model.GetTenantOnlineList(Convert.ToInt32(ProspectId));
            var GetTenantDet = db.tbl_ApplyNow.Where(p => p.ID == ProspectId).FirstOrDefault();
            string msg = "";
            string reportHTML = "";

            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect3.html");
            string message = "";
            var GetCoappDet = db.tbl_Applicant.Where(c => c.TenantID == ProspectId && c.Type == "Primary Applicant").FirstOrDefault();

            string phonenumber = GetTenantDet.Phone;

            if (RemType == 1)
            {
                reportHTML = reportHTML.Replace("[%EmailHeader%]", "Reminder to Pay Administration Fees");
                reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + GetTenantDet.FirstName + " " + GetTenantDet.LastName + "</b>,<br/>Your Online application submitted successfully. Please click below to Pay Application fees. <br/><br/><u><b>Payment Link :<a href=''></a> </br></b></u>  </br>");
                reportHTML = reportHTML.Replace("[%TenantName%]", GetTenantDet.FirstName + " " + GetTenantDet.LastName);
                var propertDet = db.tbl_Properties.Where(p => p.PID == 8).FirstOrDefault();

                string payid = new EncryptDecrypt().EncryptText(GetCoappDet.ApplicantID.ToString() + ",3," + propertDet.AdminFees.Value.ToString("0.00"));
                reportHTML = reportHTML.Replace("[%Status%]", "Congratulations ! Your Application is Approved and Pay your Administration Fees");
                reportHTML = reportHTML.Replace("[%StatusDet%]", "Good news!You have been approved.We welcome you to our community.Your next step is to pay the Administration fee of $350.00 to ensure your unit is reserved until you move -in. Once you process your payment, you will be directed to prepare your lease.  ");
                reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/PayLink/?pid=" + payid + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">PAY NOW</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                message = "Notification: Your Application is Approved and pay your Administration Fees. Please check the email for detail.";
                string body = reportHTML;
                new EmailSendModel().SendEmail(GetTenantDet.Email, "Reminder to Pay Administration Fees", body);
                if (SendMessage == "yes")
                {
                    new TwilioService().SMS(phonenumber, message);
                }
            }
            else if (RemType == 2)
            {
                List<tbl_ESignatureKeys> GetCoappList = new List<tbl_ESignatureKeys>();
                if (ApplicantID == 0)
                {
                    GetCoappList = db.tbl_ESignatureKeys.Where(c => c.TenantID == ProspectId && c.DateSigned == "").ToList();
                }
                else
                {
                    GetCoappList = db.tbl_ESignatureKeys.Where(c => c.TenantID == ProspectId && c.DateSigned == "" && c.ApplicantID == ApplicantID).ToList();
                }
                foreach (var app in GetCoappList)
                {
                    var apptdata = db.tbl_Applicant.Where(c => c.ApplicantID == app.ApplicantID).FirstOrDefault();

                    string payid = app.Key.ToString();
                    reportHTML = reportHTML.Replace("[%Status%]", "Reminder Review and Sign your application");
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Reminder Review and Sign your application");
                    reportHTML = reportHTML.Replace("[%StatusDet%]", "Hi <b>" + apptdata.FirstName + " " + apptdata.LastName + "</b>,<br/>Your Online application is Approved. Please click below to sign the lease </u> ");
                    reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "<!--[if mso]><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing: 0; border-collapse: collapse; mso-table-lspace:0pt; mso-table-rspace:0pt;\"><tr><td style=\"padding-top: 25px; padding-right: 10px; padding-bottom: 10px; padding-left: 10px\" align=\"center\"><v:roundrect xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w=\"urn:schemas-microsoft-com:office:word\" href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"height:46.5pt; width:168.75pt; v-text-anchor:middle;\" arcsize=\"7%\" stroke=\"false\" fillcolor=\"#a8bf6f\"><w:anchorlock/><v:textbox inset=\"0,0,0,0\"><center style=\"color:#ffffff; font-family:'Trebuchet MS', Tahoma, sans-serif; font-size:16px\"><![endif]--> <a href=\"" + serverURL + "/CheckList/SignLease?key=" + app.Key.ToString() + "\" style=\"-webkit-text-size-adjust: none; text-decoration: none; display: inline-block; color: #ffffff; background-color: #a8bf6f; border-radius: 4px; -webkit-border-radius: 4px; -moz-border-radius: 4px; width: auto; width: auto; border-top: 1px solid #a8bf6f; border-right: 1px solid #a8bf6f; border-bottom: 1px solid #a8bf6f; border-left: 1px solid #a8bf6f; padding-top: 15px; padding-bottom: 15px; font-family: 'Montserrat', 'Trebuchet MS', 'Lucida Grande', 'Lucida Sans Unicode', 'Lucida Sans', Tahoma, sans-serif; text-align: center; mso-border-alt: none; word-break: keep-all;\" target=\"_blank\"><span style=\"padding-left:15px;padding-right:15px;font-size:16px;display:inline-block;\"><span style=\"font-size: 16px; line-height: 32px;\">Review and Sign</span></span></a><!--[if mso]></center></v:textbox></v:roundrect></td></tr></table><![endif]-->");
                    reportHTML = reportHTML.Replace("[%TenantName%]", apptdata.FirstName + " " + apptdata.LastName);

                    string body = reportHTML;
                    new EmailSendModel().SendEmail(apptdata.Email, "Reminder Review and Sign your application", body);
                    if (SendMessage == "yes")
                    {
                        new TwilioService().SMS(phonenumber, message);
                    }
                }
            }

            msg = "Email Send Successfully";
            return msg;
        }
        public void SaveScreeningStatusList(string Email, long UserId, string Status)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var GetTenantDet = db.tbl_ApplyNow.Where(p => p.UserId == UserId).FirstOrDefault();
            string result = SaveScreeningStatus(Email, GetTenantDet.ID, Status);
            db.Dispose();

        }
        public class ProspectVerifySearchModel
        {
            public long ID { get; set; }
            public long UserID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public long DocID { get; set; }
            public int VerificationStatus { get; set; }
            public string DocumentTypeText { get; set; }
            public string DocumentName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfPages { get; set; }
            public int NumberOfRows { get; set; }
            public long PropertyId { get; set; }
            public string CreatedDate { get; set; }
            public string SortBy { get; set; }
            public string OrderBy { get; set; }
            public string UnitNo { get; set; }
            public string ApplicationStatus { get; set; }
        }
    
        public string GetDocumentType(string DocType)
        {
            string docType = "";
            if (DocType == "1")
            {
                docType = "Driving Licence";
            }
            else if (DocType == "2")
            {
                docType = "SSN";
            }
            else if (DocType == "3")
            {
                docType = "PAN";
            }
            else if (DocType == "4")
            {
                docType = "Passport";
            }
            return docType;
        }

        public ProspectVerificationModel GetProspectData(long Id)
        {
            //var test = new AcutraqRequest();
            //var data=     test.PostAqutraqRequestOld(null);
            ShomaRMEntities db = new ShomaRMEntities();

            ProspectVerificationModel model = new ProspectVerificationModel();
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
            model.EsignatureID = "";
            model.LeaseTerm = 12;
            model.PetDNAAmt = 0;
            model.LeaseTermID = 0;
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
                    model.Status = (!string.IsNullOrWhiteSpace(GetProspectData.Status) ? GetProspectData.Status.Trim() : "0");  ;
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
                    model.Prorated_Rent = GetProspectData.Prorated_Rent;
                    model.PetDeposit = GetProspectData.PetDeposit;
                    model.FOBAmt = 0;
                    model.EnvelopeID = (!string.IsNullOrWhiteSpace(GetProspectData.EnvelopeID) ? GetProspectData.EnvelopeID : "");
                    model.EsignatureID = (!string.IsNullOrWhiteSpace(GetProspectData.EsignatureID) ? GetProspectData.EsignatureID : "");
                    model.LeaseTerm = GetProspectData.LeaseTerm ?? 12;
                    model.PetDNAAmt = GetProspectData.PetDNAAmt;
                    model.LeaseTermID = Convert.ToInt32(GetProspectData.LeaseTerm);
                    DateTime? dateExpire = null;
                    try
                    {

                        dateExpire = Convert.ToDateTime(GetProspectData.MoveInDate.ToString());
                    }
                    catch
                    {
                    }

                    model.MoveInDateTxt = dateExpire.Value.ToString("MM/dd/yyyy");
                    model.ExpireDate = Convert.ToDateTime(GetProspectData.CreatedDate).AddHours(48).ToString("MM/dd/yyyy") + " 23:59:59";

                    //string decryptedCardNumber = new EncryptDecrypt().DecryptText(GetPaymentProspectData.CardNumber);
                    //string decryptedCardMonth = new EncryptDecrypt().DecryptText(GetPaymentProspectData.CardMonth);
                    //string decryptedCardYear = new EncryptDecrypt().DecryptText(GetPaymentProspectData.CardYear);
                    //string decryptedCCVNumber = new EncryptDecrypt().DecryptText(GetPaymentProspectData.CCVNumber);

                    //if (GetPaymentProspectData != null)
                    //{
                    //    model.Name_On_Card = GetPaymentProspectData.Name_On_Card;
                    //    model.CardNumber = decryptedCardNumber;
                    //    model.CardMonth = decryptedCardMonth;
                    //    model.CardYear = decryptedCardYear;
                    //    model.CCVNumber = decryptedCCVNumber;
                    //}
                    //if (GetDocumentVerificationData != null)
                    //{
                    //    model.DocumentName = GetDocumentVerificationData.DocumentName;
                    //}

                    var getPetPlace = db.tbl_TenantPetPlace.Where(p => p.TenantID == GetProspectData.ID).FirstOrDefault();
                    if (getPetPlace != null)
                    {
                        model.PetPlaceID = getPetPlace.PetPlaceID.ToString();

                    }
                    var getParkingPlace = db.tbl_TenantParking.Where(p => p.TenantID == GetProspectData.ID).FirstOrDefault();
                    if (getParkingPlace != null)
                    {
                        model.ParkingSpaceID = getParkingPlace.ParkingID.ToString();
                    }
                    var getStoragePlace = db.tbl_TenantStorage.Where(p => p.TenantID == GetProspectData.ID).FirstOrDefault();
                    if (getStoragePlace != null)
                    {
                        model.StorageSpaceID = getStoragePlace.StorageID.ToString();
                    }

                    var MarketSourceString = db.tbl_Advertiser.Where(co => co.AdID == model.Marketsource).FirstOrDefault();
                    if (MarketSourceString != null)
                    {
                        model.MarketsourceString = MarketSourceString.Advertiser;
                    }

                    DateTime? timeMoveIn = null;
                    try
                    {

                        timeMoveIn = Convert.ToDateTime(GetProspectData.CreatedDate.ToString());
                    }
                    catch
                    {
                    }

                    model.MoveInTime = timeMoveIn.Value.ToString("hh:mm tt");

                    //model.MoveInDateTxt = "";
                    model.MoveInTime = "";
                   // model.MoveInCharges = 0;
                    model.IsCheckATT = 0;
                    model.IsCheckPO = 0;
                    model.IsCheckWater = 0;
                    model.InsuranceDoc = "";
                    model.ElectricityDoc = "";
                    var MoveInData = db.tbl_MoveInChecklist.Where(co => co.ProspectID == model.ProspectId).FirstOrDefault();
                    if (MoveInData != null)
                    {
                        model.MoveInDateTxt = MoveInData.MoveInDate.HasValue ? MoveInData.MoveInDate.Value.ToString("MM/dd/yyyy") : "";
                        model.MoveInTime = MoveInData.MoveInTime;
                       // model.MoveInCharges = MoveInData.MoveInCharges ?? 0;
                        model.IsCheckATT = MoveInData.IsCheckATT ?? 0;
                        model.IsCheckPO = MoveInData.IsCheckPO ?? 0;
                        model.IsCheckWater = MoveInData.IsCheckWater ?? 0;
                        model.InsuranceDoc = MoveInData.InsuranceDoc;
                        model.ElectricityDoc = MoveInData.ElectricityDoc;
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

        public void SaveProspectVerification(long DocId, int VerificationStatus, long PID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var docInfo = db.tbl_DocumentVerification.Where(p => p.DocID == DocId).FirstOrDefault();
            if (docInfo != null)
            {
                docInfo.VerificationStatus = VerificationStatus;
                db.SaveChanges();
            }
            if (VerificationStatus != 0)
            {
                var prosInfo = db.tbl_ApplyNow.Where(p => p.ID == PID).FirstOrDefault();
                if (VerificationStatus == 1)
                {
                    prosInfo.IsApplyNow = 2;
                    db.SaveChanges();
                }
                else if (VerificationStatus == 2)
                {
                    prosInfo.IsApplyNow = 1;
                    db.SaveChanges();
                }


                var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == prosInfo.PropertyId).FirstOrDefault();
                string reportHTML = "";
                string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplate.html");
                string message = "";
                string phonenumber = prosInfo.Phone;

                if (prosInfo != null)
                {
                    reportHTML = reportHTML.Replace("[%EmailHeader%]", "Background Verification");
                    reportHTML = reportHTML.Replace("[%EmailBody%]", "Hi <b>" + prosInfo.FirstName + " " + prosInfo.LastName + "</b>,<br/>Your result for background verification is " + (VerificationStatus == 1 ? "qualified" : "disqualified") + "<br/>If you have any query?<br/>Please contact Administrator.");
                    reportHTML = reportHTML.Replace("[%TenantName%]", prosInfo.FirstName + " " + prosInfo.LastName);
                    reportHTML = reportHTML.Replace("[%TenantAddress%]", prosInfo.Address);
                    reportHTML = reportHTML.Replace("[%LeaseDate%]", DateTime.Now.ToString());
                    reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                    reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                    reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString());
                    reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString());
                    reportHTML = reportHTML.Replace("[%EmailFooter%]", "<br/>Regards,<br/>Administrator<br/>Shoma RM");

                    message = "Prospect Background Verification is done. Please check the email for detail.";
                }
                string body = reportHTML;
                new EmailSendModel().SendEmail(prosInfo.Email, "Background verification status", body);

                if (SendMessage == "yes")
                {
                    new TwilioService().SMS(phonenumber, message);
                }
            }
            db.Dispose();
        }
        public string PDFBuilder(string TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ProspectVerificationModel model = new ProspectVerificationModel();
            long tid = Convert.ToInt64(TenantID);
            var GetProspectData = db.tbl_TenantOnline.Where(cp => cp.ProspectID == tid).FirstOrDefault();
            // long uid = GetProspectData.PropertyId;
          
            var petList = db.tbl_TenantPet.Where(pp => pp.TenantID == tid).ToList();
            var coappliList = db.tbl_Applicant.Where(pp => pp.TenantID == tid && pp.Type=="Co-Applicant").ToList();

            var appn = db.tbl_ApplyNow.Where(an => an.ID == tid).FirstOrDefault();

            var advName = db.tbl_Advertiser.Where(ap => ap.AdID == appn.Marketsource).FirstOrDefault();
            var GetUnitDet = db.tbl_PropertyUnits.Where(up => up.UID == appn.PropertyId).FirstOrDefault();

            string reportHTML = "";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
            reportHTML = System.IO.File.ReadAllText(filePath + "LeaseDoc.html");
            string Pets = "";
            string OtherResidents = "";

            if (GetProspectData != null)
            {
                reportHTML = reportHTML.Replace("[%TenantName%]", GetProspectData.FirstName + " " + GetProspectData.LastName);
                reportHTML = reportHTML.Replace("[%TenantAddress%]", GetProspectData.HomeAddress1 + GetProspectData.HomeAddress2);
               
                reportHTML = reportHTML.Replace("[%SiteAddress%]", GetProspectData.HomeAddress1 + GetProspectData.HomeAddress2);
                reportHTML = reportHTML.Replace("[%LeaseStart%]", appn.MoveInDate == null ? "" : Convert.ToDateTime(appn.MoveInDate).ToString("MM/dd/yyyy"));
                reportHTML = reportHTML.Replace("[%LeaseEnd%]", appn.MoveInDate == null ? "" : Convert.ToDateTime(appn.MoveInDate).AddMonths(12).ToString("MM/dd/yyyy"));
                reportHTML = reportHTML.Replace("[%AdvertisingSource%]", advName.Advertiser);
                reportHTML = reportHTML.Replace("[%Referredby%]", "");
                reportHTML = reportHTML.Replace("[%DateofBirth%]", GetProspectData.DateOfBirth==null?"": Convert.ToDateTime(GetProspectData.DateOfBirth).ToString("MM/dd/yyyy"));
                reportHTML = reportHTML.Replace("[%SSNITIN%]", GetProspectData.SSN);
                reportHTML = reportHTML.Replace("[%AnnualIncome%]", GetProspectData.Income.ToString());
                reportHTML = reportHTML.Replace("[%OtherAnnualIncome%]", GetProspectData.AdditionalIncome.ToString());
                reportHTML = reportHTML.Replace("[%IdProof%]", GetProspectData.IDNumber);
                reportHTML = reportHTML.Replace("[%HomePhone%]", GetProspectData.Mobile);
                reportHTML = reportHTML.Replace("[%WorkPhone%]", GetProspectData.Mobile);
                reportHTML = reportHTML.Replace("[%WorkPhoneExt%]", GetProspectData.Mobile);
                reportHTML = reportHTML.Replace("[%Mobile%]", GetProspectData.Mobile);
                reportHTML = reportHTML.Replace("[%Email%]", GetProspectData.Email);
                reportHTML = reportHTML.Replace("[%ECName%]", GetProspectData.EmergencyFirstName + " " + GetProspectData.EmergencyLastName);
                reportHTML = reportHTML.Replace("[%ECRelationship%]", GetProspectData.Relationship);
                reportHTML = reportHTML.Replace("[%ECAddress%]", GetProspectData.EmergencyAddress1 + " " + GetProspectData.EmergencyAddress2);
                reportHTML = reportHTML.Replace("[%ECHomePhone%]", GetProspectData.EmergencyHomePhone);
                reportHTML = reportHTML.Replace("[%ECWorkPhone%]", GetProspectData.EmergencyWorkPhone);
                reportHTML = reportHTML.Replace("[%ECMobile%]", GetProspectData.EmergencyMobile);
                reportHTML = reportHTML.Replace("[%ECFax%]", "");
                reportHTML = reportHTML.Replace("[%ECEmail%]", GetProspectData.EmergencyEmail);
                reportHTML = reportHTML.Replace("[%CRCompany%]", "");
                reportHTML = reportHTML.Replace("[%CRRentOwn%]", GetProspectData.RentOwn == 1 ? "Rent" : "Own");
                reportHTML = reportHTML.Replace("[%CRAddress%]", GetProspectData.HomeAddress1 + " " + GetProspectData.HomeAddress2);
                reportHTML = reportHTML.Replace("[%CRRentAmount%]", GetProspectData.MonthlyPayment);
                reportHTML = reportHTML.Replace("[%CRManagerContact%]", "");
                reportHTML = reportHTML.Replace("[%CRMoveInDate%]", GetProspectData.MoveInDateFrom.ToString());
                reportHTML = reportHTML.Replace("[%CRReasonforleaving%]", GetProspectData.Reason);
                reportHTML = reportHTML.Replace("[%CRPhone%]", GetProspectData.Mobile);
                reportHTML = reportHTML.Replace("[%CRFax%]", "");
                reportHTML = reportHTML.Replace("[%CREmail%]", GetProspectData.Email);
                reportHTML = reportHTML.Replace("[%CEEmployerName%]", GetProspectData.EmployerName);
                reportHTML = reportHTML.Replace("[%CEAddress%]", GetProspectData.OfficeAddress1 + " " + GetProspectData.OfficeAddress2);
                reportHTML = reportHTML.Replace("[%CEJobTitle%]", GetProspectData.JobTitle);
                reportHTML = reportHTML.Replace("[%CEJobType%]", GetProspectData.JobType == 1 ? "Permanant" : "Contract Basis");
                reportHTML = reportHTML.Replace("[%CEAnnualIncome%]", GetProspectData.Income.ToString());
                reportHTML = reportHTML.Replace("[%CEStartDate%]", GetProspectData.StartDate.ToString());
                reportHTML = reportHTML.Replace("[%CESupervisorName%]", GetProspectData.SupervisorName);
                reportHTML = reportHTML.Replace("[%CEPhone%]", GetProspectData.SupervisorPhone);
                reportHTML = reportHTML.Replace("[%CEFax%]", "");
                reportHTML = reportHTML.Replace("[%CEEmail%]", GetProspectData.SupervisorEmail);

                reportHTML = reportHTML.Replace("[%LeaseDate%]", DateTime.Now.ToString());
                reportHTML = reportHTML.Replace("[%PropertyName%]", "Sanctury");
                reportHTML = reportHTML.Replace("[%UnitName%]", GetUnitDet.UnitNo);
                reportHTML = reportHTML.Replace("[%Deposit%]", GetUnitDet.Deposit.ToString());
                reportHTML = reportHTML.Replace("[%MonthlyRent%]", GetUnitDet.Current_Rent.ToString());

                if (OtherResidents != null)
                {
                    int cappcount = coappliList.Count;
                    foreach (var cal in coappliList)
                    {
                        if (OtherResidents == "")
                        {
                            OtherResidents = cal.FirstName + " " + cal.LastName;
                        }
                        else
                        {
                            OtherResidents += ", " + cal.FirstName + " " + cal.LastName;
                        }
                    }
                    reportHTML = reportHTML.Replace("[%OtherResidents%]", OtherResidents);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%OtherResidents%]", "");
                }

                if (Pets != null)
                {
                    Pets += "<table width='70%' border='1' cellpadding='7' cellspacing='0'><tr><th style='text-align:left; color: black'>Pet Name</th><th style='text-align:left; color: black'>Breed</th> <th style='text-align:left; color: black'>Weight</th></tr>";

                    foreach (var pl in petList)
                    {
                        Pets += "<tr>";
                        Pets += string.Format("<td style=\"text-align:left;\">{0}</td>", pl.PetName);
                        Pets += string.Format("<td  style=\"text-align:left;\">{0}</td>", pl.Breed);
                        Pets += string.Format("<td  style=\"text-align:left;\">{0}</td>", pl.Weight);
                        Pets += "</tr>";
                    }
                    Pets += "</table>";
                    reportHTML = reportHTML.Replace("[%PetList%]", Pets);
                }
                else
                {
                    reportHTML = reportHTML.Replace("[%PetList%]", "I do not have pets.");
                }

            }
            // reportHTML = Regex.Replace(reportHTML, @"\[%[^%]+%\]", "", RegexOptions.IgnoreCase);

            string reportPath = HttpContext.Current.Server.MapPath("~/ReportTemplates/");
            string reportName = filePath + TenantID.ToString() + ".html";
            System.IO.File.WriteAllText(reportName, reportHTML);

            string FileName = System.IO.Path.GetFileName(reportName);

            string path = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/"); ;
            FileName = System.IO.Path.Combine(path, FileName);

            var filePathHTMLToImage = HttpContext.Current.Server.MapPath("~/wkhtmltopdf/bin/");
            var psi = new System.Diagnostics.ProcessStartInfo();
            //psi.Arguments = "" + FileName + " " + System.IO.Path.ChangeExtension(FileName, "pdf");
            psi.Arguments = string.Format("{0}{1}{0}", '"', FileName) + " " + string.Format("{0}{1}{0}", '"', System.IO.Path.ChangeExtension(FileName, "pdf"));
            psi.FileName = filePathHTMLToImage + "wkhtmltopdf.exe";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            var p = System.Diagnostics.Process.Start(psi);
            p.WaitForExit(60000);
            reportName = System.IO.Path.Combine(path, System.IO.Path.GetFileName(System.IO.Path.ChangeExtension(FileName, "pdf")));

            SaveReportToFolder(reportName, "pdf", TenantID);

            return System.IO.Path.GetFileName(System.IO.Path.ChangeExtension(FileName, "pdf"));
        }

        public void SaveReportToFolder(string ReportSource, string ReportType, string TenantID)
        {
            try
            {
                //CampaignName = Regex.Replace(CampaignName, @"&quot;|[\s+|'"",&?%\.*:#/\\-]", "").Trim();
                string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Document/");
                string fileName = TenantID + "." + ReportType;
                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                string ReportDestination = System.IO.Path.Combine(filePath, fileName);

                System.IO.File.Copy(ReportSource, ReportDestination, true);

                byte[] bytes;
                using (var stream = new FileStream(ReportDestination, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        bytes = reader.ReadBytes((int)stream.Length);
                    }
                }
                string contentType = "application/octet-stream";
                if (ReportType == "html")
                {
                    contentType = "text/html";
                }
                else if (ReportType == "pdf")
                {
                    contentType = "application/pdf";
                }
                else if (ReportType == "xlsx")
                {
                    contentType = "application/x-msexcel";
                }

                //var document = new UploadDocument();
                //document.CampaignID = Convert.ToInt64(TenantID);
                //document.DocumentFolder = "ReportFile";
                //document.DocumentName = fileName;
                //document.ContentType = contentType;
                //document.Data = bytes;
                //document.IsDownloadedMS = 1;
                //document.IsDownloadedCS = 0;
                //document.IsDeletedMS = 0;
                //document.IsDeletedCS = 0;
                //document.SaveUploadedDocument();
            }
            catch (Exception ex)
            {

            }


        }
    }
    public class EmailData
    {
        public string AppEmail { get; set; }
    }

    public class EmpAqutraqModel
    {
        public OrderXML OrderXML { get; set; }
    }

    public class OrderXML
    {
        public string Method { get; set; }
        public  Authentication Authentication { get; set; }
        public  string TestMode { get; set; }
        public string   ReturnResultURL { get; set; }
        public string OrderingUser { get; set; }
        public Order Order { get; set; }
       
    }
    public class Authentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TestMode { get; set; }
    }
    public class Order
    {
        public string BillingReferenceCode { get; set; }
        public Subject Subject { get; set; }
        public string PackageServiceCode { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
    public class Subject
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Generation { get; set; }
        public string DOB {  get; set; }
        public string SSN { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string DLNumber { get; set; }
        public string ApplicantPosition { get; set; }

        public CurrentAddress CurrentAddress { get;set; }
        public Aliases Aliases { get; set; }
    }

    public class Aliases
    {
        public Alias Alias { get; set; }
    }
    public class Alias
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

    }
 
    public class OrderDetail
    {
        public string  CompanyName { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }

        public string Manager { get; set; }
        public string Telephone { get; set; }
        public string EmployerCity { get; set; }
        public string EmployerState { get; set; }
        public EmploymentDates EmploymentDates { get; set; }
      
        public string ReasonForLeaving { get; set; }

    }

    public class EmploymentDates
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class CurrentAddress
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
    }
}
