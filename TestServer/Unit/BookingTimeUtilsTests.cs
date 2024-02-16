using server.Helpers;

namespace server.Services.Internal.Tests;

public class BookingTimeUtilsTests
{
    public BookingTimeUtilsTests()
    {
        BookingTimeUtils.SetOpeningTime(new TimeOnly(15, 00));
    }

    [Theory]
    [InlineData("2024-02-14 14:59:00", "2024-02-14")] // Wednesday, February 14th, 2024
    [InlineData("2024-02-14 15:00:00", "2024-02-15")] // Wednesday, February 14th, 2024
    [InlineData("2024-02-16 15:00:00", "2024-02-19")] // Friday, February 16th, 2024 (Edge case: Weekend)
    public void TestGetLatestBookingDate(string timeString, string expectedLatestBookingDateString)
    {
        // Arrange
        DateTime time = DateTime.Parse(timeString);
        DateOnly expectedLatestBookingDate = DateOnly.FromDateTime(DateTime.Parse(expectedLatestBookingDateString));

        // Act
        DateOnly latestBookingDate = BookingTimeUtils.CalculateLatestAllowedBookingDate(time);

        // Assert
        Assert.Equal(expectedLatestBookingDate, latestBookingDate);
    }

    [Theory]
    [InlineData("2024-02-15", "2024-02-14 15:00:00")] // Thursday, February 15th, 2024
    [InlineData("2024-02-19", "2024-02-16 15:00:00")] // Monday, February 19th, 2024 (Edge case: Weekend)
    public void TestGetBookingOpeningDateTime(string bookingDateString, string expectedOpeningDateTimeString)
    {
        // Arrange
        DateOnly bookingDate = DateOnly.FromDateTime(DateTime.Parse(bookingDateString));
        DateTime expectedOpeningDateTime = DateTime.Parse(expectedOpeningDateTimeString);

        // Act
        DateTime openingDateTime = BookingTimeUtils.GetBookingOpeningDateTime(bookingDate);

        // Assert
        Assert.Equal(expectedOpeningDateTime, openingDateTime);
    }

}