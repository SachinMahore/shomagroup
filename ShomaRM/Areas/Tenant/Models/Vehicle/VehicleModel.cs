using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ShomaRM.Models;


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
        public string Tag { get; set; }
        public int ParkingID { get; set; }
        public string ParkingName { get; set; }

        public string SaveUpdateVehicle(VehicleModel model)
        {
            ShomaRMEntities db = new ShomaRMEntities();
            string msg = "";
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
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
                    Notes = model.Notes,
                    Tag=model.Tag,
                    ParkingID=model.ParkingID,
                    AddedBy = userid,
                };
                db.tbl_Vehicle.Add(saveVehicle);
                db.SaveChanges();

                var ParkingInfo = db.tbl_Parking.Where(p => p.ParkingID == model.ParkingID).FirstOrDefault();
                ParkingInfo.Status = 1;
                ParkingInfo.AddedBy = userid;
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
                   // getVehdata.Tag = model.Tag;
                    getVehdata.ParkingID = model.ParkingID;

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

            //var vehList = db.tbl_Vehicle.Where(p => p.TenantID == TenantID).ToList();
            long addedby = ShomaGroupWebSession.CurrentUser != null ? ShomaGroupWebSession.CurrentUser.UserID : 0;
            var vehList = db.tbl_Vehicle.Where(p => p.TenantID == TenantID && p.AddedBy == addedby).ToList();

            foreach (var pl in vehList)
            {
                //var vehState = db.tbl_State.Where(p => p.ID == (Int64)(pl.State)).FirstOrDefault();
                lstProp.Add(new VehicleModel
                {
                    Vehicle_ID = pl.Vehicle_ID,
                    License = !string.IsNullOrWhiteSpace(pl.License) ? pl.License : "",
                    Make = !string.IsNullOrWhiteSpace(pl.Make) ? pl.Make : "",
                    VModel = !string.IsNullOrWhiteSpace(pl.Model) ? pl.Model : "",
                    Year = !string.IsNullOrWhiteSpace(pl.Year) ? pl.Year : "",
                    Color = !string.IsNullOrWhiteSpace(pl.Color) ? pl.Color : "",
                    State = !string.IsNullOrWhiteSpace(State) ? State : "",
                    VehicleRegistration = !string.IsNullOrWhiteSpace(pl.VehicleRegistration) ? pl.VehicleRegistration : "",
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
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            if (VID != 0)
            {

                var vehData = db.tbl_Vehicle.Where(p => p.Vehicle_ID == VID).FirstOrDefault();
                if (vehData != null)
                {
                    var updateParking = db.tbl_Parking.Where(co => co.ParkingID == vehData.ParkingID && co.AddedBy == userid).FirstOrDefault();
                    if (updateParking != null)
                    {
                        updateParking.Status = 0;
                        updateParking.AddedBy = 0;
                        db.SaveChanges();
                    }

                    db.tbl_Vehicle.Remove(vehData);
                    db.SaveChanges();

                    var ParkingInfo = db.tbl_Parking.Where(p => p.ParkingID == vehData.ParkingID).FirstOrDefault();
                    ParkingInfo.Status = 0;
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
            if (vehicleInfo != null)
            {
                model.Vehicle_ID = vehicleInfo.Vehicle_ID;
                model.TenantID = vehicleInfo.TenantID;
                model.License = !string.IsNullOrWhiteSpace(vehicleInfo.License) ? vehicleInfo.License : "";
                model.State = !string.IsNullOrWhiteSpace(vehicleInfo.State) ? vehicleInfo.State : "";
                model.Make = !string.IsNullOrWhiteSpace(vehicleInfo.Make) ? vehicleInfo.Make : "";
                model.Color = !string.IsNullOrWhiteSpace(vehicleInfo.Color) ? vehicleInfo.Color : "";
                model.ProspectID = vehicleInfo.ProspectID;
                model.Year = !string.IsNullOrWhiteSpace(vehicleInfo.Year) ? vehicleInfo.Year : "";
                model.VehicleRegistration = !string.IsNullOrWhiteSpace(vehicleInfo.VehicleRegistration) ? vehicleInfo.VehicleRegistration : "";
                model.OwnerName = !string.IsNullOrWhiteSpace(vehicleInfo.OwnerName) ? vehicleInfo.OwnerName : "";
                model.Notes = !string.IsNullOrWhiteSpace(vehicleInfo.Notes) ? vehicleInfo.Notes : "";
                model.OriginalVehicleRegistation = !string.IsNullOrWhiteSpace(vehicleInfo.OriginalVehicleReg) ? vehicleInfo.OriginalVehicleReg : "";
                model.VModel = !string.IsNullOrWhiteSpace(vehicleInfo.Model) ? vehicleInfo.Model : "";
                model.Tag = !string.IsNullOrWhiteSpace(vehicleInfo.Tag) ? vehicleInfo.Tag : "";
                var getParkingName = db.tbl_Parking.Where(co => co.ParkingID == vehicleInfo.ParkingID).FirstOrDefault();
                model.ParkingName = getParkingName == null ? "" : !string.IsNullOrWhiteSpace(getParkingName.ParkingName) ? getParkingName.ParkingName : "";
                long stateS = Convert.ToInt64(model.State);
                var stateStr = db.tbl_State.Where(co => co.ID == stateS).FirstOrDefault();
                model.StateString = !string.IsNullOrWhiteSpace(stateStr.StateName) ? stateStr.StateName : "";
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
            int userid = ShomaRM.Models.ShomaGroupWebSession.CurrentUser != null ? ShomaRM.Models.ShomaGroupWebSession.CurrentUser.UserID : 0;
            var deleteVehicle = db.tbl_Vehicle.Where(co => co.TenantID == TenantId && co.AddedBy == userid).ToList();
            if (deleteVehicle != null)
            {
                foreach (var mod in deleteVehicle)
                {
                    var updateParking = db.tbl_Parking.Where(co => co.ParkingID == mod.ParkingID).FirstOrDefault();
                    if (updateParking != null)
                    {
                        updateParking.AddedBy = 0;
                        updateParking.Status = 0;
                        db.SaveChanges();
                    }
                }
                db.tbl_Vehicle.RemoveRange(deleteVehicle);
                db.SaveChanges();

                msg = "Vehicle Removed Successfully";
            }
            db.Dispose();
            return msg;
        }
    }
}