using System.ComponentModel.DataAnnotations;

public record BookingDetailDto(int Id, int SeatNr,
    string Name, string RoomName, string Date);

