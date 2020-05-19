using System;
using System.Globalization;

namespace AppProj.Web.Utility
{
    public class StringGenerator
    {
        public static string DateTimeToString(DateTime? dateTime, bool isIncludeTime = true)
        {
            return dateTime == null ? string.Empty : dateTime.Value.ToString(isIncludeTime ? "dd/MM/yyyy hh:mm:ss tt" : "dd/MM/yyyy");
        }
    }
}