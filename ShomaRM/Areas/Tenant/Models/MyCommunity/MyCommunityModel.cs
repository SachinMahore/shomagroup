using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models
{
    public class MyCommunityModel
    {
      //  [ID]
      //,[Amenity]
      //,[AmenityDetails]


        public long AmenityID { get; set; }
        public string Amenity { get; set; }
        public string AmenityDetails { get; set; }

        public List<MyCommunityModel> GetAmenitiesList()
        {
            List<MyCommunityModel> listAmenities = new List<MyCommunityModel>();
            ShomaRMEntities db = new ShomaRMEntities();

            var amenitiesList = db.tbl_Amenities.ToList();

            if (amenitiesList != null)
            {
                foreach (var item in amenitiesList)
                {
                    listAmenities.Add(new MyCommunityModel()
                    {
                        AmenityID = item.ID,
                        Amenity = item.Amenity,
                        AmenityDetails = item.AmenityDetails
                    });
                }
            }
            db.Dispose();
            return listAmenities;
        }
    }
}