using System;
using System.Globalization;

namespace Ineval.Extensions
{
    public static class Extensions
    {
        public static DateTime? ToDataTime(this string stringDate)
        {
            DateTime result;
            if (DateTime.TryParse(stringDate, out result))
                return result;
            return null;
        }
        public static DateTime? ToDataTimeExact(this string stringDate, string format)
        {
            try
            {
                return DateTime.ParseExact(stringDate, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static string GetStringDateTime(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString() : "";
        }
        public static string GetStringDateTime(this DateTime dateTime)
        {
            return dateTime.ToString();
        }
        public static string GetStringDateTimeExtact(this DateTime? dateTime, string format)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(format) : "";
        }

        public static string GetStringDateTimeExtact(this DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }
    }
}