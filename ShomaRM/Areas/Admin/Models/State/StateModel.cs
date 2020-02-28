using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ShomaRM.Data;
using System.Net;
using System.Data;
using System.Data.Common;
using System.IO;

namespace ShomaRM.Areas.Admin.Models
{
    public class StateModel
    {
        public long ID { get; set; }
        public string StateName { get; set; }
        public string Abbreviation { get; set; }
        public int BuildPaganationStateList(StateList model)
        {
            int NOP = 0;
            ShomaRMEntities db = new ShomaRMEntities();
            List<StateList> lstState = new List<StateList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetStatePaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

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
        public List<StateList> GetStateList(StateList model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<StateList> lstState = new List<StateList>();
            try
            {
                DataTable dtTable = new DataTable();
                using (var cmd = db.Database.Connection.CreateCommand())
                {
                    db.Database.Connection.Open();
                    cmd.CommandText = "usp_GetStatePaginationAndSearchData";
                    cmd.CommandType = CommandType.StoredProcedure;

                    DbParameter paramC = cmd.CreateParameter();
                    paramC.ParameterName = "Criteria";
                    paramC.Value = model.Criteria;
                    cmd.Parameters.Add(paramC);

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
                    StateList usm = new StateList();
                    usm.ID = int.Parse(dr["ID"].ToString());
                    usm.StateName = dr["StateName"].ToString();
                    usm.Abbreviation = dr["Abbreviation"].ToString();
                    usm.NumberOfPages = int.Parse(dr["NumberOfPages"].ToString());
                    lstState.Add(usm);
                }
                db.Dispose();
                return lstState.ToList();
            }
            catch (Exception ex)
            {
                db.Database.Connection.Close();
                throw ex;
            }
        }
        public StateModel GetStateInfo(int ID = 0)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            StateModel model = new StateModel();
            model.ID = 0;
            model.StateName = "";
            model.Abbreviation = "";

            var stateInfo = db.tbl_State.Where(p => p.ID == ID).FirstOrDefault();
            if (stateInfo != null)
            {
                model.ID = stateInfo.ID;
                model.StateName = stateInfo.StateName;
                model.Abbreviation = stateInfo.Abbreviation;
            }
            return model;
        }
        public long SaveUpdateState(StateModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            var userNameExists = db.tbl_State.Where(p => p.ID != model.ID && p.StateName == model.StateName).FirstOrDefault();
            if (userNameExists == null)
            {
                if (model.ID == 0)
                {
                    var userData = new tbl_State()
                    {
                        StateName = model.StateName,
                        Abbreviation = model.Abbreviation,
                    };
                    db.tbl_State.Add(userData);
                    db.SaveChanges();
                    model.ID = userData.ID;
                }
                else
                {
                    var stateInfo = db.tbl_State.Where(p => p.ID == model.ID).FirstOrDefault();
                    if (stateInfo != null)
                    {
                        stateInfo.StateName = model.StateName;
                        stateInfo.Abbreviation = model.Abbreviation;
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception(model.StateName + " not exists in the system.");
                    }
                }
                return model.ID;
            }
            else
            {
                throw new Exception(model.StateName + " already exists in the system.");
            }
        }


        public string DeleteState(long StateID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (StateID != 0)
            {

                var stateData = db.tbl_State.Where(p => p.ID == StateID).FirstOrDefault();
                if (stateData != null)
                {
                    db.tbl_State.Remove(stateData);
                    db.SaveChanges();
                    msg = "State Removed Successfully";
                }
            }
            db.Dispose();
            return msg;
        }

    }
    public class StateList
    {
        public long ID { get; set; }
        public string StateName { get; set; }
        public string Abbreviation { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfRows { get; set; }
        public string Criteria { get; set; }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}