using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BoilerPlate.Utils
{
    public static class StringHelper
    {
        public static DateTime? TryConvertToDateTime(this string text, string format)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            DateTime dt;
            DateTime.TryParseExact(text,
                                  format,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out dt);
            return dt;
        }

        public static DateTime? TryConvertToDateTime(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            DateTime dt;
            DateTime.TryParse(text, out dt);

            if (dt == DateTime.MinValue)
                return null;

            return dt;
        }

        public static DateTime? TryConvertToExactDateTime(this string text, string format)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            DateTime dt;
            DateTime.TryParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            if (dt == DateTime.MinValue)
                return null;

            return dt;
        }

        public static int? TryToConvertToInt(this string text)
        {
            int? result = null;

            if (!string.IsNullOrEmpty(text))
            {
                int value;
                int.TryParse(text, out value);

                if (value != 0)
                    result = value;
            }

            return result;
        }

        public static bool? TryToConvertToBool(this string text)
        {

            if (string.IsNullOrEmpty(text))
                return null;
            try
            {
                return Convert.ToBoolean(text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static double? TryToConvertToDouble(this string text)
        {

            if (string.IsNullOrEmpty(text))
                return null;
            try
            {
                return Convert.ToDouble(text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string DisplayNumeric(this long number)
        {
            try
            {
                return String.Format("{0:n0}", number);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static DateTime? TryConvertFromEpochSecondToDatetime(this long? epochSeconds, bool miliSecond = false)
        {
            if (!epochSeconds.HasValue || epochSeconds.Value == 0)
                return null;

            DateTimeOffset dateTimeOffset;

            if (!miliSecond)
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds.Value);
            else
                dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(epochSeconds.Value);

            DateTime dt = dateTimeOffset.DateTime;

            if (dt == default(DateTime))
                return null;

            return dt;
        }

        public static string Hash(this int number)
        {
            try
            {
                return number.ToString().GetHashCode().ToString("X");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static int GetHashValue(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            using (var md5Hasher = MD5.Create())
            {
                var hashed = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(text));
                return BitConverter.ToInt32(hashed, 0);
            }
        }

        public static string GetPostCode(this string text)
        {
            return Regex.Replace(text, @"[^0-9a-zA-Z:,]", "").ToLower();
        }

        public static string HtmlEncode(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return HttpUtility.HtmlEncode(text);
        }

    }
}
