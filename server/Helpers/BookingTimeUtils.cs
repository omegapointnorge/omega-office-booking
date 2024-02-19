namespace server.Helpers;

using System;


public static class BookingTimeUtils
{
    private static TimeOnly? _openingTime;
    private static IDateTimeProvider _dateTimeProvider = new SystemDateTimeProvider(); // Default provider

    public static void SetDateTimeProvider(IDateTimeProvider provider)
    {
        _dateTimeProvider = provider;
    }

    public static DateTime GetBookingOpeningDateTime(DateOnly bookingDate)
    {
        DateOnly openingDateTime = bookingDate.AddDays(-1);

        while (IsWeekend(openingDateTime))
        {
            openingDateTime = openingDateTime.AddDays(-1);
        }
        return openingDateTime.ToDateTime(GetOpeningTime());
    }


    public static DateOnly GetLatestAllowedBookingDate()
    {
        return CalculateLatestAllowedBookingDate(_dateTimeProvider.GetCurrentDateTime());
    }

    public static DateOnly CalculateLatestAllowedBookingDate(DateTime time)
    {
        DateOnly latestAllowedBookingDate = DateOnly.FromDateTime(time);

        if (TimeOnly.FromDateTime(time) >= GetOpeningTime())
        {
            latestAllowedBookingDate = GetNextWeekday(latestAllowedBookingDate);
        }

        return latestAllowedBookingDate;
    }

    public static DateTime ConvertToNorwegianTime(DateTime time)
    {
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        return TimeZoneInfo.ConvertTime(time, targetTimeZone);
    }

    private static bool IsWeekend(DateOnly date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    private static DateOnly GetNextWeekday(DateOnly date)
    {
        DateOnly nextDay = date.AddDays(1);
        while (IsWeekend(nextDay))
        {
            nextDay = nextDay.AddDays(1);
        }
        return nextDay;
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

public interface IDateTimeProvider
{
    DateTime GetCurrentDateTime();
}

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTime() => BookingTimeUtils.ConvertToNorwegianTime(DateTime.Now);
}