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
                return "0 seconds ago";

            if (difference.TotalSeconds < 60)
                return ((int)difference.TotalSeconds) + " seconds ago";

            if (difference.TotalMinutes < 60)
                return ((int)difference.TotalMinutes) + " minutes ago";

            if (difference.TotalHours < 24)
                return ((int)difference.TotalHours) + " hours ago";

            return ((int)difference.TotalDays) + " days ago";
        }
    }
}