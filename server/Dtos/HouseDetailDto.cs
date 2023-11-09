using System.ComponentModel.DataAnnotations;

public record SeatDetailDto(int Id, [property: Required]string? Room, [property: Required]string? Country,
    string? Description, int Price, string? Photo);