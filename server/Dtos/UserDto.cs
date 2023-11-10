using System.ComponentModel.DataAnnotations;

public record UserDto(int Id, string? Name, string? Email, string? Phone);