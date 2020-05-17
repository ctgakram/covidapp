using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ToLocalDateTime(this DateTime utcTime)
        {
            //return TimeZoneInfo.ConvertTimeFromUtc(utcTime, SessionHelper.TimeZone);
            return utcTime;
        }

        public static string ToDateString(this DateTime? dt)
        {
            return dt == null ? "" : dt.Value.ToString("dd MMM, yyyy");
        }

        public static string ToDateTimeString(this DateTime? dt)
        {
            //return TimeZoneInfo.ConvertTimeFromUtc(utcTime, SessionHelper.TimeZone);
            return dt == null ? "" : dt.Value.ToString("dd MMM, yyyy hh:mm");
        }
    }
}