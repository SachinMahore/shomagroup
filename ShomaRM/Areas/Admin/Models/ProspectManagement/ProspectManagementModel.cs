using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Web.Configuration;
using ShomaRM.Models.TwilioApi;
using System.Web.UI.WebControls;

namespace ShomaRM.Areas.Admin.Models
{
    public class ProspectManagementModel
    {
        public long PID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public Nullable<long> State { get; set; }
        public Nullable<long> City { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public Nullable<int> HavingPets { get; set; }
        public string PetsDetails { get; set; }
        public string VehicleDetails { get; set; }
        public Nullable<int> MinBedroom { get; set; }
        public Nullable<int> MinBathroom { get; set; }
        public Nullable<decimal> MinRent { get; set; }
        public Nullable<int> MaxBedroom { get; set; }
        public Nullable<int> MaxBathroom { get; set; }
        public Nullable<decimal> MaxRent { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<long> PropertyID { get; set; }
        public Nullable<System.DateTime> RequiredDate { get; set; }
        public string RequiredDateText { get; set; }
        public Nullable<int> Term { get; set; }
        public string ReasonForMovingIn { get; set; }
        public Nullable<System.DateTime> VisitDateTime { get; set; }
        public string VisitDateTimeText { get; set; }
        public Nullable<int> LeasingAgent { get; set; }
        public string MarketSource { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedDateText { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedeDate { get; set; }

        public int AdID { get; set; }
        public string Advertiser { get; set; }
        public long AssignAgentID { get; set; }
        public string AssignAgentName { get; set; }
        public int AppointmentStatus { get; set; }
        public string OutlookID { get; set; }
        string message = "";
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        string ServerURL = WebConfigurationManager.AppSettings["ServerURL"];
        public List<ProspectManagementModel> GetProspectList(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ProspectManagementModel> lstpr = new List<ProspectManagementModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectList";
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
                    ProspectManagementModel pr = new ProspectManagementModel();
                    DateTime? createdDate = null;
                    try
                    {
                        createdDate = Convert.ToDateTime(dr["CreatedDate"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? visitDate = null;
                    try
                    {
                        visitDate = Convert.ToDateTime(dr["VisitDateTime"].ToString());
                    }
                    catch
                    {

                    }
                    DateTime? requiredDate = null;
                    try
                    {
                        requiredDate = Convert.ToDateTime(dr["RequiredDate"].ToString());
                    }
                    catch
                    {

                    }
                    pr.PID = Convert.ToInt32(dr["PID"].ToString());
                    pr.FirstName = dr["FirstName"].ToString();
                    pr.LastName = dr["LastName"].ToString();
                    pr.PhoneNo = dr["PhoneNo"].ToString();
                    pr.EmailId = dr["EmailId"].ToString();
                    pr.StateName = dr["StateName"].ToString();
                    pr.CityName = dr["CityName"].ToString();
                    pr.Address = dr["Address"].ToString();
                    pr.Message = dr["Message"].ToString();
                    pr.HavingPets = Convert.ToInt32(dr["HavingPets"].ToString());
                    pr.UnitID = Convert.ToInt32(dr["UnitID"].ToString());
                    pr.CreatedDateText = createdDate == null ? "" : createdDate.Value.ToString("MM/dd/yyy");
                    pr.VisitDateTimeText = visitDate == null ? "" : visitDate.Value.ToString("MM/dd/yyy");
                    pr.RequiredDateText = requiredDate == null ? "" : requiredDate.Value.ToString("MM/dd/yyy");
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
        public int BuildPaganationProspectList(ProspectSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
                    cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

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
                    NOP = int.Parse(dtTable.Rows[0]["NumberOfPages"].ToString());
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
        public List<ProspectSearchModel> FillProspectSearchGrid(ProspectSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<ProspectSearchModel> lstProspect = new List<ProspectSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter param0 = cmd.CreateParameter();
                    param0.ParameterName = "FromDate";
                    param0.Value = model.FromDate;
                    cmd.Parameters.Add(param0);

                    DbParameter param1 = cmd.CreateParameter();
                    param1.ParameterName = "ToDate";
                    param1.Value = model.ToDate;
                    cmd.Parameters.Add(param1);

                    DbParameter param3 = cmd.CreateParameter();
                    param3.ParameterName = "PageNumber";
                    param3.Value = model.PageNumber;
                    cmd.Parameters.Add(param3);

                    DbParameter param4 = cmd.CreateParameter();
                    param4.ParameterName = "NumberOfRows";
                    param4.Value = model.NumberOfRows;
                    cmd.Parameters.Add(param4);

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
                    ProspectSearchModel psmodel = new ProspectSearchModel();
                    psmodel.PID = Convert.ToInt64(dr["PID"].ToString());
                    psmodel.FirstName = dr["FirstName"].ToString();
                    psmodel.LastName = dr["LastName"].ToString();
                    psmodel.PhoneNo = dr["PhoneNo"].ToString();
                    psmodel.EmailId = dr["EmailId"].ToString();
                    psmodel.StateName = dr["StateName"].ToString();
                    psmodel.CityName = dr["CityName"].ToString();
                    psmodel.Address = dr["Address"].ToString();
                    psmodel.Message = dr["Message"].ToString();
                    psmodel.HavingPets = dr["HavingPets"].ToString();
                    //psmodel.UnitID = dr["UnitID"].ToString();
                    psmodel.CreatedDate = dr["CreatedDate"].ToString();
                    psmodel.VisitDateTime = dr["VisitDateTime"].ToString();
                    psmodel.AssignAgentID = Convert.ToInt64(dr["AssignAgentId"].ToString());
                    psmodel.AppointmentStatusString = dr["AppointmentStatus"].ToString();
                    var salesAgentName = db.tbl_Login.Where(co => co.UserID == psmodel.AssignAgentID).FirstOrDefault();
                    if (salesAgentName != null)
                    {
                        psmodel.AssignAgentName = salesAgentName.FirstName + " " + salesAgentName.LastName;
                    }
                    else
                    {
                        psmodel.AssignAgentName = "Agent not yet assign";
                    }
                    lstProspect.Add(psmodel);
                }
                db.Dispose();
                return lstProspect.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        
        public string SaveProspectForm(ProspectManagementModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            var prospData = db.tbl_Prospect.Where(p => p.PID == model.PID).FirstOrDefault();
            string message = "";
            string phonenumber = prospData.PhoneNo;
            long assignAgeID = prospData.AssignAgentId != null ? Convert.ToInt64(prospData.AssignAgentId) : 0;
            if (model.PID != 0)
            {
                if (prospData != null)
                {
                    prospData.AssignAgentId = model.AssignAgentID;
                    prospData.AppointmentStatus = model.AppointmentStatus;
                    prospData.VisitDateTime = model.VisitDateTime;
                    db.SaveChanges();
                    msg = "Progress Saved";

                    if (model.AppointmentStatus != 3)
                    {
                        if (assignAgeID != model.AssignAgentID)
                        {
                            var info = db.tbl_Login.Where(p => p.UserID == prospData.AssignAgentId).FirstOrDefault();

                            string reportHTML = "";
                            string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                            reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");
                            reportHTML = reportHTML.Replace("[%ServerURL%]", ServerURL);
                            reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                            string emailBody = "";
                            emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + prospData.FirstName + " " + prospData.LastName + "</p>";
                            emailBody += "<p style=\"margin-bottom: 0px;\">This is a confirmation email to your appointment with  <b>" + info.FirstName + " " + info.LastName + " </b> Dated on <b>" + model.RequiredDateText + "</b> at office. If you have queries or require any clarifications or any assistance in finding the location  please do not hesitate to contact me at <i>" + info.CellPhone + "</i>, " + info.Email + ". I genuinely appreciate a prompt confirmation from your side. Looking forward to meeting you there.</p>";
                            reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                            //reportHTML = reportHTML.Replace("[%TenantName%]", prospData.FirstName + " " + prospData.LastName);
                            //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; This is a confirmation email to your appointment with  <b>" + info.FirstName + " " + info.LastName + " </b> Dated on <b>" + model.RequiredDateText + "</b> at office. If you have queries or require any clarifications or any assistance in finding the location  please do not hesitate to contact me at <i>" + info.CellPhone + "</i>, " + info.Email + ". I genuinely appreciate a prompt confirmation from your side. Looking forward to meeting you there. </p>");
                            //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");

                            string body = reportHTML;
                            new EmailSendModel().SendEmail(prospData.EmailId, "Your Appointment is Confirmed", body);

                            message = "This is a confirmation message for your appointment. Please check the email for detail.";
                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message);
                                }
                            }

                            string reportHTMLAgent = "";
                            string filePathAg = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                            reportHTMLAgent = System.IO.File.ReadAllText(filePathAg + "EmailTemplateAmenity.html");
                            reportHTMLAgent = reportHTMLAgent.Replace("[%ServerURL%]", ServerURL);
                            reportHTMLAgent = reportHTMLAgent.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                            string emailBodyAgent = "";
                            emailBodyAgent += "<p style=\"margin-bottom: 0px;\">Dear " + info.FirstName + " " + info.LastName + "</p>";
                            emailBodyAgent += "<p style=\"margin-bottom: 0px;\">Please be informed that a meeting has been scheduled  with <b> " + prospData.FirstName + " " + prospData.LastName + " </b> Dated on  <b>" + model.RequiredDateText + "</b>.We shall meet at office . Please inform me if you'd like to add anything to list above. All suggestions and questions are highly welcomed.Kindly signal that you received this email and confirm your attendance.Please make sure to be on time as you always do.Looking forward to seeing you there.</p>";
                            reportHTMLAgent = reportHTMLAgent.Replace("[%EmailBody%]", emailBodyAgent);

                            //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Application Submission");
                            //reportHTMLAgent = reportHTMLAgent.Replace("[%TenantName%]", info.FirstName + " " + info.LastName);
                            //reportHTMLAgent = reportHTMLAgent.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Please be informed that a meeting has been scheduled  with <b> " + prospData.FirstName + " " + prospData.LastName + " </b> Dated on  <b>" + model.RequiredDateText + "</b>.We shall meet at office . Please inform me if you'd like to add anything to list above. All suggestions and questions are highly welcomed.Kindly signal that you received this email and confirm your attendance.Please make sure to be on time as you always do.Looking forward to seeing you there.</p>");
                            //reportHTMLAgent = reportHTMLAgent.Replace("[%LeaseNowButton%]", "");

                            string bodyAg = reportHTMLAgent;
                            new EmailSendModel().SendEmail(info.Email, "Appointment for " + prospData.FirstName + " " + prospData.LastName + " on " + model.RequiredDateText, bodyAg);

                            string message1 = "Please to be informed that a meeting has been scheduled. Please check the email for detail.";

                            if (SendMessage == "yes")
                            {
                                if (!string.IsNullOrWhiteSpace(phonenumber))
                                {
                                    new TwilioService().SMS(phonenumber, message1);
                                }
                            }
                        }
                    }
                    else
                    {
                        var info = db.tbl_Login.Where(p => p.UserID == prospData.AssignAgentId).FirstOrDefault();

                        string reportHTML = "";
                        string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                        reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateAmenity.html");

                        reportHTML = reportHTML.Replace("[%ServerURL%]", ServerURL);
                        reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + prospData.FirstName + " " + prospData.LastName + "</p>";
                        emailBody += "<p style=\"margin-bottom: 0px;\">" + info.FirstName + " " + info.LastName + " was unfortunately not able to make the meeting, please contact us back so we can schedule another tour at your earliest convenience.</p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                        //reportHTML = reportHTML.Replace("[%TenantName%]", prospData.FirstName + " " + prospData.LastName);
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; " + info.FirstName + " " + info.LastName + " was unfortunately not able to make the meeting, please contact us back so we can schedule another tour at your earliest convenience. </p>");
                        //reportHTML = reportHTML.Replace("[%LeaseNowButton%]", "");

                        string body = reportHTML;
                        new EmailSendModel().SendEmail(prospData.EmailId, "Your Appointment is Cancelled", body);

                        message = "This is a cancellation message for your shoma appointment. Please check the email for detail.";
                        if (SendMessage == "yes")
                        {
                            if (!string.IsNullOrWhiteSpace(phonenumber))
                            {
                                new TwilioService().SMS(phonenumber, message);
                            }
                        }
                    }
                };
            }
            return msg;
        }
        public ProspectManagementModel GetProspectDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            ProspectManagementModel model = new ProspectManagementModel();
            model.CreatedDate = DateTime.Now;
            var prospectData = db.tbl_Prospect.Where(p => p.PID == id).FirstOrDefault();
            if (prospectData != null)
            {
                model.PID = prospectData.PID;
                model.FirstName = prospectData.FirstName;
                model.LastName = prospectData.LastName;
                model.PhoneNo = prospectData.PhoneNo;
                model.EmailId = prospectData.EmailId;
                model.State = prospectData.State;
                model.City = prospectData.City;
                model.Address = prospectData.Address;
                model.Message = prospectData.Message;
                model.HavingPets = prospectData.HavingPets;
                model.PropertyID = prospectData.PropertyID;
                model.UnitID = prospectData.UnitID;
                model.CreatedBy = 1;
                model.CreatedDate = prospectData.CreatedDate;
                model.VisitDateTime = prospectData.VisitDateTime;
                model.RequiredDate = prospectData.RequiredDate;
                model.MarketSource = prospectData.MarketSource;
                model.PetsDetails = prospectData.PetsDetails;
                model.AssignAgentID = prospectData.AssignAgentId != null ? prospectData.AssignAgentId.Value : 0;
                model.AppointmentStatus = prospectData.AppointmentStatus != null ? prospectData.AppointmentStatus.Value : 0;
                model.OutlookID = prospectData.OutlookID;
            }

            return model;
        }
        public List<ProspectManagementModel> GetDdlMarketSourceList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var Advertiser = db.tbl_Advertiser.OrderBy(co => co.Advertiser).ToList();
            List<ProspectManagementModel> li = new List<ProspectManagementModel>();

            foreach (var item in Advertiser)
            {
                li.Add(new ProspectManagementModel
                {
                    AdID=item.AdID,
                    Advertiser=item.Advertiser
                });
            }

            return li.ToList();
        }
    }
    public class ProspectSearchModel
    {
        public long PID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string HavingPets { get; set; }
        public string UnitID { get; set; }
        public string CreatedDate { get; set; }
        public string VisitDateTime { get; set; }
        public string RequiredDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public long AssignAgentID { get; set; }
        public string AssignAgentName { get; set; }
        public string AppointmentStatusString { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
    public partial class VisitModel
    {
        public long VisitID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public Nullable<int> VisitNumber { get; set; }
        public Nullable<System.DateTime> VisitDateTime { get; set; }
        public string VisitDateTimeText { get; set; }
        public string ResultCode { get; set; }
        public string Details { get; set; }
        public Nullable<int> Activity { get; set; }
        public Nullable<int> VisitIncharge { get; set; }
        public Nullable<int> VisitSlot { get; set; }
        public string   VisitInchargeText { get; set; }
        public VisitModel GetVisitDetails(int id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            VisitModel model = new VisitModel();

            var getVisitDet = db.tbl_Visit.Where(p => p.VisitID == id).FirstOrDefault();
            if (getVisitDet != null)
            {
                DateTime? visitDateTime = null;
                try
                {

                    visitDateTime = Convert.ToDateTime(getVisitDet.VisitDateTime);
                }
                catch
                {

                }

                model.VisitID = getVisitDet.VisitID;
                model.ProspectID = getVisitDet.ProspectID;
                model.VisitDateTimeText = visitDateTime == null ? "" : visitDateTime.Value.ToString("MM/dd/yyy");
                model.ResultCode = getVisitDet.ResultCode;
                model.VisitNumber = getVisitDet.VisitNumber;
                model.Details = getVisitDet.Details;
                model.Activity = getVisitDet.Activity;
                model.VisitIncharge = getVisitDet.VisitIncharge;
                
                model.VisitSlot = getVisitDet.VisitSlot;
            }
           return model;
        }

        public string SaveUpdateVisit(VisitModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            var visitID = db.tbl_Visit.Where(p => p.VisitID==model.VisitID).FirstOrDefault();
            if (model.VisitID == 0)
            {
                var saveVisit = new tbl_Visit()
                {
                    VisitID = model.VisitID,
                    ProspectID = model.ProspectID,
                    VisitDateTime = model.VisitDateTime,
                    ResultCode = model.ResultCode,
                    VisitNumber = model.VisitNumber,
                    Details = model.Details,
                    Activity = model.Activity,
                    VisitIncharge = model.VisitIncharge,
                VisitSlot = model.VisitSlot,
            };
                db.tbl_Visit.Add(saveVisit);
                db.SaveChanges();


                msg = "Visit Saved Successfully";
            }
            else
            {
                var visitData = db.tbl_Visit.Where(p => p.VisitID == model.VisitID).FirstOrDefault();
                if (visitData != null)
                {

                    visitData.VisitID = model.VisitID;
                    visitData.ProspectID = model.ProspectID;
                    visitData.VisitDateTime = model.VisitDateTime;
                    visitData.ResultCode = model.ResultCode;
                    visitData.VisitNumber = model.VisitNumber;
                    visitData.Details = model.Details;
                    visitData.Activity = model.Activity;
                    visitData.VisitIncharge = model.VisitIncharge;
                    visitData.VisitSlot = model.VisitSlot;
                }
                db.SaveChanges();
                msg = "Visit Updated Successfully";
            }

            db.Dispose();
            return msg;


        }

        public List<VisitModel> GetVisitList(long ProspectID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<VisitModel> lstpr = new List<VisitModel>();
            try
            {
                DataTable dtTable = new DataTable();
                dtTable.Clear();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetVisitList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramF = cmd.CreateParameter();
                    paramF.ParameterName = "ProspectID";
                    paramF.Value = ProspectID;
                    cmd.Parameters.Add(paramF);                    

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    VisitModel pr = new VisitModel();
                    DateTime? visitDateTime = null;
                    try
                    {

                        visitDateTime = Convert.ToDateTime(dr["VisitDateTime"].ToString());
                    }
                    catch
                    {

                    }
                    pr.VisitID = Convert.ToInt64(dr["VisitID"].ToString());
                    pr.ProspectID = Convert.ToInt64(dr["ProspectID"].ToString());
                    pr.ResultCode = dr["ResultCode"].ToString();
                    pr.Details= dr["Details"].ToString();                 
                    pr.Activity = Convert.ToInt32(dr["Activity"].ToString());
                    pr.VisitNumber = Convert.ToInt32(dr["VisitNumber"].ToString());
                    pr.VisitSlot = Convert.ToInt32(dr["VisitSlot"].ToString());
                    pr.VisitInchargeText =dr["VisitInchargeText"].ToString();
                    pr.VisitDateTimeText = visitDateTime == null ? "" : visitDateTime.Value.ToString("MM/dd/yyy");
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
    }
}