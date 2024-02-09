namespace server.Helpers;

using System;

public static class BookingTimeUtils
{
    private const int SameDayCutoffHour = 16;

    public static DateTime ConvertToNorwegianTime(DateTime time)
    {
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        return TimeZoneInfo.ConvertTime(time, targetTimeZone);
    }

    public static DateOnly GetLatestAllowedBookingDate()
    {
        DateTime now = ConvertToNorwegianTime(DateTime.Now);
        DateOnly latestAllowedBookingDate = DateOnly.FromDateTime(now);
        TimeSpan sameDayCutoff = new TimeSpan(SameDayCutoffHour, 0, 0);

        if (IsWeekend(now) || now.TimeOfDay > sameDayCutoff)
        {
            latestAllowedBookingDate = GetNextWeekday(latestAllowedBookingDate);
        }
        return latestAllowedBookingDate;
    }

    // Check if it's Saturday, Sunday, or Friday after the same day cutoff hour
    private static bool IsWeekend(DateTime date)
    {
        TimeSpan sameDayCutoff = new TimeSpan(SameDayCutoffHour, 0, 0);
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
               date.DayOfWeek == DayOfWeek.Friday && date.TimeOfDay > sameDayCutoff;
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

    public static DateTime GetBookingOpeningTime(DateOnly bookingDate)
    {
        TimeOnly openingTime = new TimeOnly(SameDayCutoffHour, 0);
        DateOnly openingDate = bookingDate.AddDays(-1);
        DateTime openingDateTime = new DateTime(openingDate.Year, openingDate.Month, openingDate.Day, openingTime.Hour, openingTime.Minute, openingTime.Second);

        while (IsWeekend(openingDateTime))
        {
            openingDateTime = openingDateTime.AddDays(-1);
        }
        return openingDateTime;
    }




}
