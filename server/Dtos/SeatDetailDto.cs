using System.ComponentModel.DataAnnotations;

public record SeatDetailDto(int Id, [property: Required]string? Room,
     int Price);