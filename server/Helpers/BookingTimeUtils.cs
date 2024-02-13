namespace server.Helpers
{
    using System;

    public static class BookingTimeUtils
    {
        private static TimeOnly? _openingTime;

        public static DateTime ConvertToNorwegianTime(DateTime time)
        {
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            return TimeZoneInfo.ConvertTime(time, targetTimeZone);
        }

        public static DateOnly GetLatestAllowedBookingDate()
        {
            DateTime now = ConvertToNorwegianTime(DateTime.Now);
            DateOnly latestAllowedBookingDate = DateOnly.FromDateTime(now);
            TimeOnly openingTime = GetOpeningTime();

            if (IsWeekend(now) || TimeOnly.FromDateTime(now) >= openingTime)
            {
                latestAllowedBookingDate = GetNextWeekday(latestAllowedBookingDate);
            }
            return latestAllowedBookingDate;
        }

        // Check if it's Saturday, Sunday, or Friday after the same day cutoff hour
        private static bool IsWeekend(DateTime date)
        {
            TimeOnly openingTime = GetOpeningTime();
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
                   (date.DayOfWeek == DayOfWeek.Friday && TimeOnly.FromDateTime(date) >= openingTime);
        }

        private static DateOnly GetNextWeekday(DateOnly date)
        {
            DateOnly nextDay = date.AddDays(1);
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }
            return nextDay;
        }

        public static DateTime GetBookingOpeningDateTime(DateOnly bookingDate)
        {
            TimeOnly openingTime = GetOpeningTime();
            DateOnly openingDate = bookingDate.AddDays(-1);
            DateTime openingDateTime = new DateTime(openingDate.Year, openingDate.Month, openingDate.Day, openingTime.Hour, openingTime.Minute, openingTime.Second);

            while (IsWeekend(openingDateTime))
            {
                openingDateTime = openingDateTime.AddDays(-1);
            }
            return openingDateTime;
        }

        public static TimeOnly GetOpeningTime()
        {
            if (_openingTime == null)
            {
                throw new Exception("Opening time is not set.");
            }
            return (TimeOnly)_openingTime;
        }

        public static void SetOpeningTime(TimeOnly openingTime)
        {
            _openingTime = openingTime;
        }
    }
}