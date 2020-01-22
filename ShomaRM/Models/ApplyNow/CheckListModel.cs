using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Models
{
    public class CheckListModel
    {
        public long MIID { get; set; }
        public Nullable<long> ProspectID { get; set; }
        public Nullable<System.DateTime> MoveInDate { get; set; }
        public string MoveInTime { get; set; }
        public Nullable<decimal> MoveInCharges { get; set; }
        public string InsuranceDoc { get; set; }
        public string ElectricityDoc { get; set; }
        public Nullable<int> IsCheckPO { get; set; }
        public Nullable<int> IsCheckATT { get; set; }
        public Nullable<int> IsCheckWater { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string SaveMoveInCheckList(CheckListModel model)
        {
            string msg = "";
            ShomaRMEntities db = new ShomaRMEntities();
           
            if (model.ProspectID != null)
            {
                var loginDet = db.tbl_MoveInChecklist.Where(p => p.ProspectID == model.ProspectID).FirstOrDefault();
                if (loginDet == null)
                {
                    var saveMoveInCheckList = new tbl_MoveInChecklist()
                    {
                       
                        ProspectID = model.ProspectID,
                        MoveInDate = model.MoveInDate,
                        MoveInTime = model.MoveInTime,
                        MoveInCharges = model.MoveInCharges,
                        InsuranceDoc = model.InsuranceDoc,
                        ElectricityDoc = model.ElectricityDoc,
                        IsCheckPO = model.IsCheckPO,
                        IsCheckATT = model.IsCheckATT,
                        IsCheckWater = model.IsCheckWater,
                         CreatedDate = DateTime.Now,
                    };
                    db.tbl_MoveInChecklist.Add(saveMoveInCheckList);
                    db.SaveChanges();
                  

                }
               
            }
            msg = "Move In Check List Save Successfully";

           
            return msg;
        }
    }
}