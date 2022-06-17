using SRM.Data.Models.Shuttles;
using SRM.Data.Models.Times;
using System;

namespace SRM.Domain
{
    public static class StaticExtensions
    {
        public static V ConvertValue<V>(this object x)
        {
            return (V)Convert.ChangeType(x, typeof(V));
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static string ToJSONNull(this string value)
        {
            return value.Replace(":\"\"", ":null");
        }

        public static bool IsJSONNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || value == "[]";
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool ToBoolean(this object value)
        {
            return Convert.ToBoolean(value);
        }

        public static int ToInteger(this object value)
        {
            return Convert.ToInt32(value);
        }

        public static DateTime? ToNullableDateTime(this DateTime value)
        {
            DateTime? nullValue = value;
            return nullValue;
        }

        public static DateTime ToDateTime(this DateTime? value)
        {
            return value.Value;
        }

        public static bool DateInCombination(this DateCombination combination, System.DateTime Date)
        {
            switch (Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return combination.Monday;
                case DayOfWeek.Tuesday:
                    return combination.Tuesday;
                case DayOfWeek.Wednesday:
                    return combination.Wednesday;
                case DayOfWeek.Thursday:
                    return combination.Thursday;
                case DayOfWeek.Friday:
                    return combination.Friday;
                case DayOfWeek.Saturday:
                    return combination.Saturday;
                case DayOfWeek.Sunday:
                    return combination.Sunday;
            }

            return true;
        }
        //Operasyon datasıni tarih kosuluna göre filtreleme
        public static bool AvaibleTimeConditionforShuttleOperation(this ShuttleOperation operation, AvailableTime availableTime)//
        {
            bool result = operation.DateTime.Date >= availableTime.StartDate.Date && operation.DateTime.Date <= availableTime.EndDate.Date;

            if (!availableTime.IsIntegrated && result)
            {
                result = operation.DateTime.TimeOfDay >= availableTime.StartTime.Value.TimeOfDay && operation.DateTime.TimeOfDay <= availableTime.EndTime.Value.TimeOfDay
                                      && availableTime.IncludedDate.DateInCombination(operation.DateTime);

            }
            return result;

        }

    }
}
