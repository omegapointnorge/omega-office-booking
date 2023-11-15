using System.ComponentModel.DataAnnotations;

public record SeatDetailDto(int Id, [property: Required]string RoomName,
     int RoomId);