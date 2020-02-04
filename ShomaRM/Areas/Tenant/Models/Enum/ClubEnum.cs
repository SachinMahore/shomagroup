using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShomaRM.Areas.Tenant.Models.Enum
{
   public enum ActivityEnum
    {
        Yoga=1,
        Sports=2

    }

    public enum DayEnum
    {
        Sunday = 1,
        Monday = 2,
        Tuesday=3,
        Wednesday=4,
        Thursday=5,
        Friday=6,
        Saturday=7

    }
    public enum LevelEnum
    {
        Beginner = 1,
        Intermediate = 2,
        Advance = 3,
        AllLevel = 4

    }

    public enum SearchClubListEnum
    {
        [Display(Name = "A-Z")]
        A_Z = 1,
        [Display(Name = "Day of the Week")]
        Day_Of_Week = 2
    }
}