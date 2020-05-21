using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Data;
using System.Data.Common;
using ShomaRM.Models;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class DocumentModel
    {
        public long DocID { get; set; }
        public Nullable<long> TenantID { get; set; }
        public string TenantName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public Nullable<long> UploadBy { get; set; }

        public string SaveUpdateDocument(HttpPostedFileBase fb, DocumentModel model)
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

            int userid = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.DocID == 0)
            {
                var saveDocument = new tbl_Document()
                {
                    TenantID = model.TenantID,
                    DocumentName = sysFileName,
                    DocumentType = model.DocumentType,
                    DocumentNumber = model.DocumentNumber,
                    UploadBy = userid,
                    UploadDate = DateTime.Now.Date
                };
                db.tbl_Document.Add(saveDocument);
                db.SaveChanges();
                msg = "Document Save Successfully";
            }
            else
            {
                var GetDocumentData = db.tbl_Document.Where(p => p.DocID == model.DocID).FirstOrDefault();
                if (GetDocumentData != null)
                {
                    GetDocumentData.TenantID = model.TenantID;
                    GetDocumentData.DocumentName = sysFileName;
                    GetDocumentData.DocumentType = model.DocumentType;
                    GetDocumentData.DocumentNumber = model.DocumentNumber;
                    GetDocumentData.UploadBy = userid;
                    GetDocumentData.UploadDate = DateTime.Now.Date;
                    db.SaveChanges();
                    msg = "Document Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public DocumentModel GetDocumentData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            DocumentModel model = new DocumentModel();

            var GetDocumentData = db.tbl_Document.Where(p => p.DocID == Id).FirstOrDefault();

            if (GetDocumentData != null)
            {
                model.TenantID = GetDocumentData.TenantID;
                model.DocumentName = GetDocumentData.DocumentName;
                model.DocumentType = GetDocumentData.DocumentType;
                model.DocumentNumber = GetDocumentData.DocumentNumber;
            }
            model.DocID = Id;
            return model;
        }

        public List<DocumentModel> GetDocumentListData(long ProspectID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<DocumentModel> lstpr = new List<DocumentModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetProspectDocumentList";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramTID = cmd.CreateParameter();
                    paramTID.ParameterName = "ProspectID";
                    paramTID.Value = ProspectID;
                    cmd.Parameters.Add(paramTID);

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    DocumentModel pr = new DocumentModel();

                    pr.DocID = Convert.ToInt64(dr["DocID"].ToString());
                   
                    pr.DocumentName = dr["DocumentName"].ToString();
                    pr.DocumentType = dr["DocumentType"].ToString();
                    pr.DocumentNumber = dr["DocumentNumber"].ToString();
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

        public List<DocumentModel> DocumentList()
        {
            List<DocumentModel> list = new List<DocumentModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var DocumentList = db.tbl_Document.ToList();
            foreach (var item in DocumentList)
            {
                list.Add(new DocumentModel()
                {
                    DocID = item.DocID,
                    DocumentName = item.DocumentName
                });
            }
            return list;
        }
    }
}