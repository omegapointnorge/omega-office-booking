namespace server.Helpers;

using System;

public static class BookingTimeUtils
{
    private static IConfiguration? _configuration;
    public static void SetConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static DateTime ConvertToNorwegianTime(DateTime time)
    {
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        return TimeZoneInfo.ConvertTime(time, targetTimeZone);
    }

    public static DateOnly GetLatestAllowedBookingDate()
    {
        DateTime now = ConvertToNorwegianTime(DateTime.Now);
        DateOnly latestAllowedBookingDate = DateOnly.FromDateTime(now);
        TimeOnly openingTime = getOpeningTime();

        if (IsWeekend(now) || TimeOnly.FromDateTime(now) >= openingTime)
        {
            latestAllowedBookingDate = GetNextWeekday(latestAllowedBookingDate);
        }
        return latestAllowedBookingDate;
    }

    // Check if it's Saturday, Sunday, or Friday after the same day cutoff hour
    private static bool IsWeekend(DateTime date)
    {
        TimeOnly openingTime = getOpeningTime();
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
        TimeOnly openingTime = getOpeningTime();
        DateOnly openingDate = bookingDate.AddDays(-1);
        DateTime openingDateTime = new DateTime(openingDate.Year, openingDate.Month, openingDate.Day, openingTime.Hour, openingTime.Minute, openingTime.Second);

        while (IsWeekend(openingDateTime))
        {
            openingDateTime = openingDateTime.AddDays(-1);
        }
        return openingDateTime;
    }

    private static TimeOnly getOpeningTime()
    {
        string openingTime = _configuration["OpeningTime"];
        return TimeOnly.Parse(openingTime);
    }
}
