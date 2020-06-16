using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using ShomaRM.Areas.Tenant.Models;
using System.IO;
using System.Web.Configuration;
using ShomaRM.Models.TwilioApi;

namespace ShomaRM.Areas.Admin.Models
{
    public class TenantPetManagementModel
    {

    }

    public class TenantPetSearchListModel
    {
        public int PetID { get; set; }
        public long TenantID { get; set; }
        public string PetType { get; set; }
        public string Breed { get; set; }
        public string Weight { get; set; }
        public string Age { get; set; }
        public string Photo { get; set; }
        public string PetVaccinationCert { get; set; }
        public string PetName { get; set; }
        public string VetsName { get; set; }
        public string OriginalPhoto { get; set; }
        public string OriginalVaccinationCert { get; set; }
        public string UniqID { get; set; }
        public string ExpiryDate { get; set; }
        public string UnitNo { get; set; }
        public string TenantName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Criteria { get; set; }
        public string CriteriaByText { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfPages { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
        public string FilePath { get; set; }

        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];

        public int BuildPaganationTenantPetList(TenantPetSearchListModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantPetPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "FromDate";
                    paramDF.Value = model.FromDate;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "ToDate";
                    paramDT.Value = model.ToDate;
                    cmd.Parameters.Add(paramDT);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramCT = cmd.CreateParameter();
                    paramCT.ParameterName = "CriteriaByText";
                    paramCT.Value = model.CriteriaByText;
                    cmd.Parameters.Add(paramCT);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

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
        public List<TenantPetSearchListModel> FillTenantPetSearchGrid(TenantPetSearchListModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<TenantPetSearchListModel> lstData = new List<TenantPetSearchListModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetTenantPetPaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramDF = cmd.CreateParameter();
                    paramDF.ParameterName = "FromDate";
                    paramDF.Value = model.FromDate;
                    cmd.Parameters.Add(paramDF);

                    DbParameter paramDT = cmd.CreateParameter();
                    paramDT.ParameterName = "ToDate";
                    paramDT.Value = model.ToDate;
                    cmd.Parameters.Add(paramDT);

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

                    DbParameter paramCT = cmd.CreateParameter();
                    paramCT.ParameterName = "CriteriaByText";
                    paramCT.Value = model.CriteriaByText;
                    cmd.Parameters.Add(paramCT);

                    DbParameter paramPN = cmd.CreateParameter();
                    paramPN.ParameterName = "PageNumber";
                    paramPN.Value = model.PageNumber;
                    cmd.Parameters.Add(paramPN);

                    DbParameter paramNOR = cmd.CreateParameter();
                    paramNOR.ParameterName = "NumberOfRows";
                    paramNOR.Value = model.NumberOfRows;
                    cmd.Parameters.Add(paramNOR);

                    DbParameter paramSortBy = cmd.CreateParameter();
                    paramSortBy.ParameterName = "SortBy";
                    paramSortBy.Value = model.SortBy;
                    cmd.Parameters.Add(paramSortBy);

                    DbParameter paramOrderBy = cmd.CreateParameter();
                    paramOrderBy.ParameterName = "OrderBy";
                    paramOrderBy.Value = model.OrderBy;
                    cmd.Parameters.Add(paramOrderBy);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    TenantPetSearchListModel usm = new TenantPetSearchListModel();

                    usm.PetID = Convert.ToInt32(dr["PetID"].ToString());
                    //usm.TenantID = Convert.ToInt64(dr["TenantID"].ToString());
                    usm.PetType = dr["PetType"].ToString();
                    usm.Breed = dr["Breed"].ToString();
                    usm.Weight = dr["Weight"].ToString();
                    usm.Age = dr["Age"].ToString();
                    usm.Photo = dr["Photo"].ToString();
                    usm.PetVaccinationCert = dr["PetVaccinationCert"].ToString();
                    usm.PetName = dr["PetName"].ToString();
                    usm.VetsName = dr["VetsName"].ToString();
                    usm.OriginalPhoto = dr["OriginalPhoto"].ToString();
                    usm.OriginalVaccinationCert = dr["OriginalVaccinationCert"].ToString();
                    usm.UniqID = dr["UniqID"].ToString();
                    string expiryDate = "";
                    try { expiryDate = Convert.ToDateTime(dr["ExpiryDate"].ToString()).ToString("MM/dd/yyyy"); } catch { expiryDate = ""; }
                    usm.ExpiryDate = expiryDate;
                    usm.TenantName = dr["TenantName"].ToString();
                    usm.UnitNo = dr["UnitNo"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/pet/" + usm.PetVaccinationCert);
                    usm.FilePath = filePath;


                    lstData.Add(usm);
                }
                db.Dispose();
                return lstData.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

    }

    public class TenantPetListModel
    {
        public int PetID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public Nullable<int> PetType { get; set; }
        public string Breed { get; set; }
        public string Weight { get; set; }
        public string Age { get; set; }
        public string Photo { get; set; }
        public string PetVaccinationCert { get; set; }
        public string PetName { get; set; }
        public string VetsName { get; set; }
        public string OriginalPhoto { get; set; }
        public string OriginalVaccinationCert { get; set; }
        public string UniqID { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string UnitNo { get; set; }
        public string TenantName { get; set; }
        public long PropertyID { get; set; }
        public string ExpiryDateString { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        string serverURL = WebConfigurationManager.AppSettings["ServerURL"];
        string SendMessage = WebConfigurationManager.AppSettings["SendMessage"];
        public String SaveUpdateTenantPet(TenantPetListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PetID != 0)
            {
                var TenantPetInfo = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();
                if (TenantPetInfo != null)
                {
                    TenantPetInfo.Breed = model.Breed;
                    TenantPetInfo.Weight = model.Weight;
                    TenantPetInfo.Age = model.Age;
                    TenantPetInfo.PetName = model.PetName;
                    TenantPetInfo.UniqID = model.UniqID;
                    TenantPetInfo.ExpiryDate = model.ExpiryDate;
                    db.SaveChanges();
                    msg = "Pet information updated successfully.</br>";
                }
                else
                {
                    msg = "Pet information not updated successfully.</br>";
                }
            }
            db.Dispose();
            return msg;

        }

        public String SendRemainder(TenantPetListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PetID != 0)
            {
                var GetTenantPetData = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();

                if (GetTenantPetData != null)
                {
                    string expiryDate = "";
                    try { expiryDate = Convert.ToDateTime(GetTenantPetData.ExpiryDate).ToString("MM/dd/yyyy"); } catch { expiryDate = ""; }
                    ExpiryDateString = expiryDate;

                    var TenantInfo = db.tbl_TenantInfo.Where(p => p.ProspectID == GetTenantPetData.TenantID).FirstOrDefault();
                    if (TenantInfo != null)
                    {
                        TenantName = TenantInfo.FirstName + ' ' + TenantInfo.LastName;
                        Email = TenantInfo.Email;
                        PhoneNumber = TenantInfo.Mobile;
                    }

                    string reportHTML = "";
                    string filePath = HttpContext.Current.Server.MapPath("~/Content/Templates/");
                    reportHTML = System.IO.File.ReadAllText(filePath + "EmailTemplateProspect.html");
                    reportHTML = reportHTML.Replace("[%ServerURL%]", serverURL);
                    reportHTML = reportHTML.Replace("[%TodayDate%]", DateTime.Now.ToString("dddd,dd MMMM yyyy"));

                    if (model != null)
                    {
                        string emailBody = "";
                        emailBody += "<p style=\"margin-bottom: 0px;\">Dear " + TenantName + " your pet " + PetName + " vaccination is Expired / Expiring . Please submit new vaccination Certificate</p>";
                        reportHTML = reportHTML.Replace("[%EmailBody%]", emailBody);

                        //reportHTML = reportHTML.Replace("[%EmailHeader%]", "Pet Vaccination Certificate Expiry Remainder");
                        //reportHTML = reportHTML.Replace("[%EmailBody%]", " <p style='font-size: 14px; line-height: 21px; text-align: justify; margin: 0;'>&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Dear " + TenantName + " your pet " + PetName + " vaccination is Expired / Expiring . Please submit new vaccination Certificate</p>");
                        //reportHTML = reportHTML.Replace("[%TenantName%]", TenantName);
                        //reportHTML = reportHTML.Replace("[%TenantEmail%]", Email);
                    }
                    string body = reportHTML;
                    new EmailSendModel().SendEmail(Email, "Pet Vaccination Certificate Expiry Remainder", body);
                    string message = "Pet Vaccination Certificate Expiry Remainder. Please check the email for detail.";
                    if (SendMessage == "yes")
                    {
                        if (!string.IsNullOrWhiteSpace(PhoneNumber))
                        {
                            new TwilioService().SMS(PhoneNumber, message);
                        }
                    }

                }
                msg = "Vaccination Expiry Remainder Send Successfully";
            }
            return msg;
        }

        public String UpdateWeight(TenantPetListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PetID != 0)
            {
                var TenantPetInfo = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();
                if (TenantPetInfo != null)
                {
                    TenantPetInfo.Weight = model.Weight;
                    db.SaveChanges();
                    msg = "Pet information updated successfully.</br>";
                }
                else
                {
                    msg = "Pet information not updated successfully.</br>";
                }
            }
            db.Dispose();
            return msg;

        }
        public String UpdateUniqId(TenantPetListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PetID != 0)
            {
                var TenantPetInfo = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();
                if (TenantPetInfo != null)
                {

                    TenantPetInfo.UniqID = model.UniqID;
                    db.SaveChanges();
                    msg = "Pet information updated successfully.</br>";
                }
                else
                {
                    msg = "Pet information not updated successfully.</br>";
                }
            }
            db.Dispose();
            return msg;

        }
        public String UpdateExpiryDate(TenantPetListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();

            if (model.PetID != 0)
            {
                var TenantPetInfo = db.tbl_TenantPet.Where(p => p.PetID == model.PetID).FirstOrDefault();
                if (TenantPetInfo != null)
                {
                    TenantPetInfo.ExpiryDate = model.ExpiryDate;
                    db.SaveChanges();
                    msg = "Pet information updated successfully.</br>";
                }
                else
                {
                    msg = "Pet information not updated successfully.</br>";
                }
            }
            db.Dispose();
            return msg;

        }
    }
}