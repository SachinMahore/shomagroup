using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ShomaRM.Data;
using System.Data.Common;
using System.IO;
using System.Globalization;

namespace ShomaRM.Areas.Admin.Models
{
    public class EventModel
    {
        public long EventID { get; set; }
        public string EventName { get; set; }
        public long PropertyID { get; set; }
        public string PropertyName { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public string EventDateString { get; set; }
        public string EventDateText { get; set; }
        public string Photo { get; set; }
        public string OriginalPhoto { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<int> Type { get; set; }
        public string TypeText { get; set; }
        public Nullable<System.TimeSpan> EventTime { get; set; }
        public Nullable<decimal> Fees { get; set; }
        public string EventTimeString { get; set; }
        public bool IsFree { get; set; }
        public string FessString { get; set; }

        public string SaveUpdateEvent(EventModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
            DateTime dt = DateTime.Parse(model.EventTimeString != null ? model.EventTimeString : "00:00");
            
            TimeSpan time = dt.TimeOfDay;
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (model.EventID == 0)
            {
                var saveEvent = new tbl_Event()
                {
                    EventName = model.EventName,
                    PropertyID = model.PropertyID,
                    EventDate = model.EventDate,
                    Photo = model.Photo,
                    Description = model.Description,
                    CreatedByID = userid,
                    CreatedByDate = DateTime.Now.Date,
                    Type = model.Type,

                    EventTime = time,
                    Fees = model.Fees
                };
                db.tbl_Event.Add(saveEvent);
                db.SaveChanges();
                msg = "Event Save Successfully";
            }
            else
            {
                string PhotoName = "";
                var GetEventData = db.tbl_Event.Where(p => p.EventID == model.EventID).FirstOrDefault();
                PhotoName = GetEventData.Photo;
                if (PhotoName != model.Photo && model.Photo != "")
                {
                    PhotoName = model.Photo;
                }
                if (GetEventData != null)
                {
                    GetEventData.EventName = model.EventName;
                    GetEventData.PropertyID = model.PropertyID;
                    GetEventData.EventDate = model.EventDate;
                    GetEventData.Photo = PhotoName;
                    GetEventData.Description = model.Description;
                    GetEventData.Type = model.Type;
                    GetEventData.EventTime = time;
                    GetEventData.Fees = model.Fees;
                    //GetEventData.CreatedByID = ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID;
                    //GetEventData.CreatedByDate = DateTime.Now.Date;
                    db.SaveChanges();
                    msg = "Event Updated Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

        public EventModel GetEventData(int Id)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EventModel model = new EventModel();

            var GetEventData = db.tbl_Event.Where(p => p.EventID == Id).FirstOrDefault();
            if (GetEventData != null)
            {
                model.EventName = GetEventData.EventName;
                model.PropertyID = GetEventData.PropertyID.HasValue? GetEventData.PropertyID.Value:0;
                model.EventDate = GetEventData.EventDate;
                model.Photo = GetEventData.Photo;
                model.Description = GetEventData.Description;
                model.Type = GetEventData.Type;
                model.EventTime = GetEventData.EventTime;
                model.Fees = GetEventData.Fees;
                model.FessString = GetEventData.Fees != null ? GetEventData.Fees.Value.ToString("0.00") : "0.00";
                model.EventDateString = GetEventData.EventDate != null ? GetEventData.EventDate.Value.ToString("MM/dd/yyyy") : "";
                //model.EventTimeString = GetEventData.EventTime != null ? GetEventData.EventTime.ToString() : "";
                if (GetEventData.EventTime != null)
                {
                    TimeSpan tt = new TimeSpan();
                    tt = GetEventData.EventTime.Value;
                    DateTime dt = DateTime.Today.Add(tt);
                    string displayTime = dt.ToString("hh:mm tt");
                    model.EventTimeString = displayTime;
                }
                else
                {
                    model.EventTimeString = "00:00";
                }
                model.IsFree = GetEventData.Fees != null || GetEventData.Fees <= 0 ? true : false;
            }
            model.EventID = Id;
            return model;
        }
        public List<EventModel> GetEventListData(DateTime FromDate, DateTime ToDate)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<EventModel> lstpr = new List<EventModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetEventList";
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
                    EventModel pr = new EventModel();
                    DateTime? eventDate = null;
                    try
                    {

                        eventDate = Convert.ToDateTime(dr["EventDate"].ToString());
                    }
                    catch
                    {

                    }

                    pr.EventID = Convert.ToInt64(dr["EventID"].ToString());
                    pr.EventName = dr["EventName"].ToString();
                    pr.PropertyName = dr["PropertyName"].ToString();
                    pr.EventDateString = eventDate == null ? "" : eventDate.Value.ToString("MM/dd/yyy");
                    pr.Photo = dr["Photo"].ToString();
                    pr.Description = dr["Description"].ToString();
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
        public List<EventModel> EventList()
        {
            List<EventModel> list = new List<EventModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var EventList = db.tbl_Event.ToList();
            foreach (var item in EventList)
            {
                list.Add(new EventModel()
                {
                    EventID = item.EventID,
                    EventName = item.EventName
                });
            }
            return list;
        }
        public class EventSearchModel
        {
            public long EventID { get; set; }
            public string EventName { get; set; }
            public string PropertyName { get; set; }
            public string Photo { get; set; }
            public string EventDate { get; set; }
            public string Description { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageNumber { get; set; }
            public int NumberOfRows { get; set; }
            public int Type { get; set; }
            public string SortBy { get; set; }
            public string OrderBy { get; set; }
        }
        public int BuildPaganationEventList(EventSearchModel model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetEventPaginationAndSearchData";
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
        public List<EventSearchModel> FillEventSearchGrid(EventSearchModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<EventSearchModel> lstEvent = new List<EventSearchModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetEventPaginationAndSearchData";
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
                    EventSearchModel searchmodel = new EventSearchModel();
                    searchmodel.EventID = Convert.ToInt64(dr["EventID"].ToString());
                    searchmodel.EventName = dr["EventName"].ToString();
                    searchmodel.PropertyName = dr["PropertyName"].ToString();
                    searchmodel.Photo = dr["Photo"].ToString();
                    searchmodel.EventDate = dr["EventDate"].ToString();
                    searchmodel.Description = dr["Description"].ToString();
                    searchmodel.Type = Convert.ToInt32(dr["Type"].ToString());
                    lstEvent.Add(searchmodel);
                }
                db.Dispose();
                return lstEvent.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public List<EventModel> GetEventList()
        {
            List<EventModel> listEvent = new List<EventModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var eventList = db.tbl_Event.Where(co => co.EventDate == DateTime.Now || co.EventDate >= DateTime.Now).ToList();
            if (eventList != null)
            {
                foreach (var item in eventList)
                {
                    listEvent.Add(new EventModel()
                    {
                        EventID = item.EventID,
                        EventDate = item.EventDate,
                        EventName = item.EventName,
                        EventDateString = item.EventDate.Value.ToString("dd"),
                        Type = item.Type
                    });
                }
            }
            db.Dispose();
            return listEvent;
        }
        public List<EventModel> GetCalEventList(DateTime dt)
        {
            List<EventModel> listEvent = new List<EventModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var eventList = db.tbl_Event.Where(co => co.EventDate == dt ).ToList();
            if (eventList != null)
            {
                foreach (var item in eventList)
                {
                    listEvent.Add(new EventModel()
                    {
                        EventID = item.EventID,
                        EventDate = item.EventDate,
                        EventName = item.EventName,
                        EventDateString = item.EventDate.Value.ToString("dd"),
                        Type = item.Type
                    });
                }
            }
            db.Dispose();
            return listEvent;
        }


        public List<EventModel> GetDateEventList()
        {
            List<EventModel> listEvent = new List<EventModel>();
            ShomaRMEntities db = new ShomaRMEntities();
            var eventList = db.tbl_Event.Where(co => co.EventDate >= DateTime.Now).ToList();
            if (eventList != null)
            {
                foreach (var item in eventList)
                {
                    DateTime? eventDate = null;
                    try
                    {

                        eventDate = Convert.ToDateTime(item.EventDate.Value);
                    }
                    catch
                    {

                    }

                    listEvent.Add(new EventModel()
                    {
                        
                        EventID = item.EventID,
                        EventDate = item.EventDate,
                        EventDateText = eventDate == null ? "" : eventDate.Value.ToString("yyy-MM-dd"),
                        EventName = item.EventName,
                        EventDateString = item.EventDate.Value.ToString("dd"),
                        Type = item.Type
                    });
                }
            }
            db.Dispose();
            return listEvent;
        }


        public List<EventModel> GetNewDateEventList()
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<EventModel> lstDateEvent = new List<EventModel>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetCalendarEvent";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbDataAdapter da = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dtTable);
                    db.Database.Connection.Close();
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    EventModel DateEventmodel = new EventModel();

                    DateEventmodel.EventName = dr["EventName"].ToString();
                    DateEventmodel.EventDateText = dr["EventDate"].ToString();
                    DateEventmodel.TypeText = dr["Type"].ToString();
                    lstDateEvent.Add(DateEventmodel);
                }
                db.Dispose();
                return lstDateEvent.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }

        public EventModel GetEventDetail(long EventID)
        {
            EventModel _eventModel = new EventModel();
            ShomaRMEntities db = new ShomaRMEntities();
            var getEventDetail = db.tbl_Event.Where(co => co.EventID == EventID).FirstOrDefault();
            if (getEventDetail!=null)
            {
                _eventModel.EventID = getEventDetail.EventID;
                _eventModel.EventName = getEventDetail.EventName;
                _eventModel.Description = getEventDetail.Description;
                _eventModel.EventDate = getEventDetail.EventDate;
                _eventModel.EventTime = getEventDetail.EventTime;
                _eventModel.EventDateString = getEventDetail.EventDate.Value.ToString("MM/dd/yyyy");
                TimeSpan tt = new TimeSpan();
                tt = getEventDetail.EventTime.Value;
                DateTime dt = DateTime.Today.Add(tt);
                string displayTime = dt.ToString("hh:mm tt");
                _eventModel.EventTimeString = displayTime;
            }

            return _eventModel;
        }

        public EventModel EventFileUpload(HttpPostedFileBase fileBaseUpload1, EventModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            EventModel eventModel = new EventModel();
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUpload1 != null && fileBaseUpload1.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/Event/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUpload1.FileName;
                Extension = Path.GetExtension(fileBaseUpload1.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUpload1.FileName);
                fileBaseUpload1.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUpload1.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/Event/") + "/" + sysFileName;

                }
                eventModel.Photo = sysFileName;
                eventModel.OriginalPhoto = fileName;
            }
            return eventModel;
        }
    }
}