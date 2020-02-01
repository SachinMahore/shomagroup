using ShomaRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models
{
    public class CommunityClubModel
    {
        int communityClubID;
        string title;
        string activity;
        DateTime startDate;
        string venue;
        DateTime meetingDate;
        DateTime meetingTime;
        string contact;
        string email;
        string phone;
        string bestWayToContact;
        string performanceLevel;
        string comment;
    }
}