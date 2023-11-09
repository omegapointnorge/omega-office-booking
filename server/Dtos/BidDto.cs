using System.ComponentModel.DataAnnotations;

public record BookingDto(int Id, int SeatId, 
    [property: Required]string Bookingder);