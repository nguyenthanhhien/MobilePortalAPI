using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities
{
    public static class StringExtension
    {
        public static bool StringEqual(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }        
            return str1 == str2.ToLower();
        }
        public static bool StringNotEqual(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return str1 != str2.ToLower();
        }
        public static bool StringIsNull(this string str1)
        {
            return str1 == null;
        }
        public static bool StringIsNotNull(this string str1)
        {
            return str1 != null;
        }
        public static bool StringLessThan(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return string.Compare(str1, str2.ToLower()) < 0;
        }
        public static bool StringLessThanOrEqual(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return string.Compare(str1, str2.ToLower()) <= 0;
        }
        public static bool StringGreaterThan(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return string.Compare(str1, str2) > 0;
        }

        public static bool StringGreaterThanOrEqual(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return string.Compare(str1, str2.ToLower()) >= 0;
        }
        public static bool StringContains(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return str1 != null && str1.Contains(str2.ToLower());
        }
        public static bool StringNotContains(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return str1 == null || (str1 != null && !str1.Contains(str2.ToLower()));
        }
        public static bool StringEmpty(this string str1)
        {
            return str1 == string.Empty;
        }
        public static bool StringNotEmpty(this string str1)
        {
            return str1 != string.Empty;
        }
        public static bool StringStartsWith(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return str1 != null && str1.StartsWith(str2.ToLower());
        }
        public static bool StringEndsWith(this string str1, string str2)
        {
            if (!string.IsNullOrWhiteSpace(str1))
            {
                str1 = str1.ToLower();
            }
            return str1 != null && str1.EndsWith(str2.ToLower());
        }

        public static bool DateTimeEqual(this DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue && dt1.Value.Date == dt2.Date;
        }
        public static bool DateTimeNotEqual(this DateTime? dt1, DateTime dt2)
        {
            return !dt1.HasValue || (dt1.HasValue && dt1.Value.Date == dt2.Date);
        }
        public static bool DateTimeIsNull(this DateTime? dt1)
        {
            return !dt1.HasValue;
        }
        public static bool DateTimeIsNotNull(this DateTime? dt1)
        {
            return dt1.HasValue;
        }
        public static bool DateTimeLessThan(this DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue && dt1.Value.Date < dt2.Date;
        }
        public static bool DateTimeLessThanOrEqual(this DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue && dt1.Value.Date <= dt2.Date;
        }
        public static bool DateTimeGreaterThan(this DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue && dt1.Value.Date > dt2.Date;
        }
        public static bool DateTimeGreaterThanOrEqual(this DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue && dt1.Value.Date >= dt2.Date;
        }
    }
}
