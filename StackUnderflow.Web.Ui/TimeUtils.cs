using System;

namespace StackUnderflow.Web.Ui
{
    public static class TimeUtils
    {
        public static string ToRelativeTime(DateTime absoluteTime)
        {
            var difference = DateTime.Now.Subtract(absoluteTime);
            if (difference.CompareTo(TimeSpan.FromSeconds(0)) < 0)
                return "0s ago";

            if (difference.TotalSeconds < 60)
                return ((int) difference.TotalSeconds) + "s ago";

            if (difference.TotalMinutes < 60)
                return ((int) difference.TotalMinutes) + "m ago";

            if (difference.TotalHours < 24)
                return ((int) difference.TotalHours) + "h ago";

            return ((int) difference.TotalDays) + "d ago";
        }

        public static string ToRelativeTimeDeatiled(DateTime absoluteTime)
        {
            var difference = DateTime.Now.Subtract(absoluteTime);
            if (difference.CompareTo(TimeSpan.FromSeconds(0)) < 0)
                return "0 seconds";

            if (difference.TotalSeconds < 60)
                return Pluralize(difference.TotalSeconds, "second");

            if (difference.TotalMinutes < 60)
                return Pluralize(difference.TotalMinutes, "minute");

            if (difference.TotalHours < 24)
                return Pluralize(difference.TotalHours, "hour");

            return Pluralize(difference.TotalDays, "day");
        }

        private static string Pluralize(double x, string timeunit)
        {
            int n = (int) x;
            if (n < 1)
                throw new Exception("Expected at least 1 time unit: " + x);

            if (n == 1)
                return n + " " + timeunit;

            return n + " " + timeunit + "s";
        }
    }
}