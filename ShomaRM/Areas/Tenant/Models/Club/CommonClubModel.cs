using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models.Club
{
    public class CommonClubModel
    {
        public CommonClubModel()
        {
            _ClubModel = new ClubModel();
            _clublist = new List<ClubModel>();
        }
       public ClubModel _ClubModel { get;set; }
       public List<ClubModel> _clublist { get; set; }
    }
}