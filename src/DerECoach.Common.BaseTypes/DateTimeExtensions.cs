using System;

namespace DerECoach.Common.BaseTypes
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert datetime to a nullable DateTme
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime? ToNullable(this DateTime time)
        {
            if (time == default(DateTime)) return null;
            return time;
        }

        /// <summary>
        /// Convert datetime to an XML compatible string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToXmlDateTimeString(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        /// <summary>
        /// Convert nullable datetime to an XML compatible string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToXmlDateTimeString(this DateTime? value)
        {
            return value != null ? value.Value.ToXmlDateTimeString() : default(string);
        }

        /// <summary>
        /// Compare two dates. If the date part, hours, minutes and seconds are equal, they are considered equal
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EqualsUpToSeconds(this DateTime a, DateTime b)
        {
            return a.Date.Equals(b.Date) && a.Hour.Equals(b.Hour) && a.Minute.Equals(b.Minute) &&
                   a.Second.Equals(b.Second);
        }

        /// <summary>
        /// Convert datetime to a string that can be used in file names and which is sortable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dateSeparator"></param>
        /// <param name="timeSeparator"></param>
        /// <returns></returns>
        public static string ToSortableFileName(this DateTime value, string dateSeparator = "-",
            string timeSeparator = ".")
        {
            return value.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", dateSeparator, timeSeparator));
        }

        /// <summary>
        /// Returns true if the given date is on saturday on sunday
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsWeekendDay(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}