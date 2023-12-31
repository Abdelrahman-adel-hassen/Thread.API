using System.ComponentModel.DataAnnotations;

namespace Thread.Domain.DTOs;

public record RegisterDto(
    [Required] string Name,
    [Required] string KnownAs,
    string Gender,
    DateOnly DateOfBirth,
    string City,
    string Country,
    [Required][EmailAddress] string Email,
    [Required][RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,}$", ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")] string Password
);
