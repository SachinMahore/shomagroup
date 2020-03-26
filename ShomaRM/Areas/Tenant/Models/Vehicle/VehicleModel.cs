using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace ShomaRM.Areas.Tenant.Models
{
    public class VehicleModel
    {
        public long Vehicle_ID { get; set; }
        public long TenantID { get; set; }
        public string License { get; set; }
        public string State { get; set; }
        public string Make { get; set; }
        public string VModel { get; set; }
        public string Color { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public string Year { get; set; }
        public string VehicleRegistration { get; set; }

        public string OwnerName { get; set; }
        public string Notes { get; set; }
        public string TempVehicleRegistation { get; set; }
        public string OriginalVehicleRegistation { get; set; }
        public string StateString { get; set; }
        public string SaveUpdateVehicle(VehicleModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            if (model.Vehicle_ID == 0)
            {
                var saveVehicle = new tbl_Vehicle()
                {
                    VehicleRegistration = model.VehicleRegistration,
                    OriginalVehicleReg = model.OriginalVehicleRegistation,
                    Vehicle_ID = model.Vehicle_ID,
                    TenantID = model.TenantID,
                    Make = model.Make,
                    Model = model.VModel,
                    Year = model.Year,
                    Color = model.Color,
                    License = model.License,
                    State = model.State,
                    OwnerName = model.OwnerName,
                    Notes = model.Notes

                };
                db.tbl_Vehicle.Add(saveVehicle);
                db.SaveChanges();


                msg = "Vehicle Saved Successfully";
            }
            else
            {
                var getVehdata = db.tbl_Vehicle.Where(p => p.Vehicle_ID == model.Vehicle_ID).FirstOrDefault();
                if (getVehdata != null)
                {
                    VehicleRegistration = model.VehicleRegistration;
                    OriginalVehicleRegistation = model.OriginalVehicleRegistation;
                    getVehdata.Vehicle_ID = model.Vehicle_ID;
                    getVehdata.TenantID = model.TenantID;
                    getVehdata.Make = model.Make;
                    getVehdata.Model = model.VModel;
                    getVehdata.Year = model.Year;
                    getVehdata.Color = model.Color;
                    getVehdata.License = model.License;
                    getVehdata.State = model.State;
                    getVehdata.OwnerName = model.OwnerName;
                    getVehdata.Notes = model.Notes;

                }
                db.SaveChanges();
                msg = "Vehicle Updated Successfully";
            }

            db.Dispose();
            return msg;


        }
        public string SaveUpdateVehicleTenanat(VehicleModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            if (model.Vehicle_ID == 0)
            {
                var saveVehicle = new tbl_Vehicle()
                {
                    VehicleRegistration = model.VehicleRegistration,
                    OriginalVehicleReg = model.OriginalVehicleRegistation,
                    Vehicle_ID = model.Vehicle_ID,
                    TenantID = model.TenantID,
                    Make = model.Make,
                    Model = model.VModel,
                    Year = model.Year,
                    Color = model.Color,
                    License = model.License,
                    State = model.State,
                    OwnerName = model.OwnerName,
                    Notes = model.Notes

                };
                db.tbl_Vehicle.Add(saveVehicle);
                db.SaveChanges();


                msg = "Vehicle Saved Successfully";
            }
            else
            {
                var getVehdata = db.tbl_Vehicle.Where(p => p.Vehicle_ID == model.Vehicle_ID).FirstOrDefault();
                if (getVehdata != null)
                {
                    getVehdata.VehicleRegistration = model.VehicleRegistration;
                    getVehdata.OriginalVehicleReg = model.OriginalVehicleRegistation;
                    getVehdata.Vehicle_ID = model.Vehicle_ID;
                    getVehdata.TenantID = model.TenantID;
                    getVehdata.Make = model.Make;
                    getVehdata.Model = model.VModel;
                    getVehdata.Year = model.Year;
                    getVehdata.Color = model.Color;
                    getVehdata.License = model.License;
                    getVehdata.State = model.State;
                    getVehdata.OwnerName = model.OwnerName;
                    getVehdata.Notes = model.Notes;

                }
                db.SaveChanges();
                msg = "Vehicle Updated Successfully";
            }

            db.Dispose();
            return msg;


        }
        public List<VehicleModel> GetVehicleList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<VehicleModel> lstProp = new List<VehicleModel>();

            var vehList = db.tbl_Vehicle.Where(p => p.TenantID == TenantID).ToList();

            foreach (var pl in vehList)
            {
                //var vehState = db.tbl_State.Where(p => p.ID == (Int64)(pl.State)).FirstOrDefault();
                lstProp.Add(new VehicleModel
                {
                    Vehicle_ID = pl.Vehicle_ID,
                    License = pl.License,
                    Make = pl.Make,
                    VModel = pl.Model,
                    Year = pl.Year,
                    Color = pl.Color,
                    State = State,
                    VehicleRegistration = pl.VehicleRegistration,
                    OwnerName = !string.IsNullOrWhiteSpace(pl.OwnerName) ? pl.OwnerName : "",
                    Notes = !string.IsNullOrWhiteSpace(pl.Notes) ? pl.Notes : ""
                });

            }
            return lstProp;
        }

        public string DeleteTenantVehicle(long VID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";

            if (VID != 0)
            {

                var vehData = db.tbl_Vehicle.Where(p => p.Vehicle_ID == VID).FirstOrDefault();
                if(vehData!=null)
                {
                    db.tbl_Vehicle.Remove(vehData);
                    db.SaveChanges();
                    msg = "Vehicle Removed Successfully";

                }

               
            }

            db.Dispose();
            return msg;


        }
        public VehicleModel SaveUploadVehicleRegistation(HttpPostedFileBase fileBaseUploadVehicleRegistation, VehicleModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            VehicleModel vehicleModelVehicleReg = new VehicleModel();

            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            string Extension = "";

            if (fileBaseUploadVehicleRegistation != null && fileBaseUploadVehicleRegistation.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("~/Content/assets/img/VehicleRegistration/");
                DirectoryInfo di = new DirectoryInfo(filePath);
                FileInfo _FileInfo = new FileInfo(filePath);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName = fileBaseUploadVehicleRegistation.FileName;
                Extension = Path.GetExtension(fileBaseUploadVehicleRegistation.FileName);
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fileBaseUploadVehicleRegistation.FileName);
                fileBaseUploadVehicleRegistation.SaveAs(filePath + "//" + sysFileName);
                if (!string.IsNullOrWhiteSpace(fileBaseUploadVehicleRegistation.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("~/Content/assets/img/VehicleRegistration/") + "/" + sysFileName;

                }
                vehicleModelVehicleReg.TempVehicleRegistation = sysFileName.ToString();
                vehicleModelVehicleReg.OriginalVehicleRegistation = fileName;
            }

            return vehicleModelVehicleReg;
        }

        public VehicleModel GetVehicleInfo(long VehicleId)
        {
                ShomaRMEntities db = new ShomaRMEntities();
                var vehicleInfo = db.tbl_Vehicle.Where(co => co.Vehicle_ID == VehicleId).FirstOrDefault();
                VehicleModel model = new VehicleModel();
                if (vehicleInfo!=null)
                {
                    model.Vehicle_ID = vehicleInfo.Vehicle_ID;
                    model.TenantID = vehicleInfo.TenantID;
                    model.License = vehicleInfo.License;
                    model.State = vehicleInfo.State;
                    model.Make = vehicleInfo.Make;
                    model.Color = vehicleInfo.Color;
                    model.ProspectID = vehicleInfo.ProspectID;
                    model.Year = vehicleInfo.Year;
                    model.VehicleRegistration = vehicleInfo.VehicleRegistration;
                    model.OwnerName = vehicleInfo.OwnerName;
                    model.Notes = vehicleInfo.Notes;
                    model.OriginalVehicleRegistation = vehicleInfo.OriginalVehicleReg;
                    model.VModel = vehicleInfo.Model;
                long stateS = Convert.ToInt64(model.State);
                var stateStr = db.tbl_State.Where(co => co.ID == stateS).FirstOrDefault();
                model.StateString = stateStr.StateName;
            }

                return model;
        }

        public List<VehicleModel> GetProfileVehicleList(long TenantID)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            List<VehicleModel> lstProp = new List<VehicleModel>();

            var appVehicle = db.tbl_TenantInfo.Where(p => p.TenantID == TenantID).FirstOrDefault();
            if (appVehicle != null)
            {
                var vehList = db.tbl_Vehicle.Where(p => p.TenantID == appVehicle.ProspectID).ToList();
                if (vehList != null)
                {
                    foreach (var pl in vehList)
                    {
                        long StateId = Convert.ToInt64(pl.State);
                        var vehState = db.tbl_State.Where(p => p.ID == StateId).FirstOrDefault();

                        lstProp.Add(new VehicleModel
                        {
                            Vehicle_ID = pl.Vehicle_ID,
                            License = pl.License,
                            Make = pl.Make,
                            VModel = pl.Model,
                            Year = pl.Year,
                            Color = pl.Color,
                            State = vehState.StateName,
                            VehicleRegistration = pl.VehicleRegistration,
                            OwnerName = pl.OwnerName,
                            Notes = pl.Notes
                        });

                    }
                }
            }
            return lstProp;
        }


        public string SaveUpdateVehicleTenanat(VehicleModel model, long UserId)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            var applyNow = db.tbl_ApplyNow.Where(co => co.UserId == UserId).FirstOrDefault();
            if (applyNow != null)
            {
                if (model.Vehicle_ID == 0)
                {
                    var saveVehicle = new tbl_Vehicle()
                    {
                        VehicleRegistration = model.VehicleRegistration,
                        OriginalVehicleReg = model.OriginalVehicleRegistation,
                        Vehicle_ID = model.Vehicle_ID,
                        TenantID = applyNow.ID,
                        Make = model.Make,
                        Model = model.VModel,
                        Year = model.Year,
                        Color = model.Color,
                        License = model.License,
                        State = model.State,
                        OwnerName = model.OwnerName,
                        Notes = model.Notes

                    };
                    db.tbl_Vehicle.Add(saveVehicle);
                    db.SaveChanges();


                    msg = "Vehicle Saved Successfully";
                }
                else
                {
                    var getVehdata = db.tbl_Vehicle.Where(p => p.Vehicle_ID == model.Vehicle_ID).FirstOrDefault();
                    if (getVehdata != null)
                    {
                        getVehdata.VehicleRegistration = model.VehicleRegistration;
                        getVehdata.OriginalVehicleReg = model.OriginalVehicleRegistation;
                        getVehdata.Vehicle_ID = model.Vehicle_ID;
                        getVehdata.TenantID = applyNow.ID;
                        getVehdata.Make = model.Make;
                        getVehdata.Model = model.VModel;
                        getVehdata.Year = model.Year;
                        getVehdata.Color = model.Color;
                        getVehdata.License = model.License;
                        getVehdata.State = model.State;
                        getVehdata.OwnerName = model.OwnerName;
                        getVehdata.Notes = model.Notes;

                    }
                    db.SaveChanges();
                    msg = "Vehicle Updated Successfully";
                }
            }


            db.Dispose();
            return msg;


        }
        public string DeleteVehicleListOnCheck(long TenantId)
        {
            string msg = string.Empty;
            ShomaRMEntities db = new ShomaRMEntities();
            var deleteVehicle = db.tbl_Vehicle.Where(co => co.TenantID == TenantId).ToList();
            if (deleteVehicle != null)
            {
                db.tbl_Vehicle.RemoveRange(deleteVehicle);
                db.SaveChanges();
                msg = "Vehicle Removed Successfully";
            }
            db.Dispose();
            return msg;
        }
    }
}