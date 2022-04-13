using BoilerPlate.Utils;
using System;
using System.Globalization;

namespace BoilerPlate.Utils
{
    public static class DateHelper
    {
        public static string GetLocalDateFormatted(DateTime date, Countries country)
        {
            TimeZoneInfo timeZone = null;
            var pattern = "ddd dd MMM hh:mm ttt";
            var formattedDate = string.Empty;

            switch (country)
            {
                case Countries.Au:
                    {
                        timeZone = TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time");
                        var offset = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, TimeSpan.Zero);
                        var localDate = TimeZoneInfo.ConvertTimeFromUtc(offset.UtcDateTime, timeZone);
                        formattedDate = localDate.ToString(pattern);
                        break;
                    }
              
                default:
                    {
                        formattedDate = date.ToString(pattern);
                        break;
                    }
            }

            return formattedDate;
        }
    }
}
