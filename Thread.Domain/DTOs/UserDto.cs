namespace Thread.Domain.DTOs;
public class UserDto
{

    public DateOnly DateOfBirth { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    [Required]
    public string Name { get; set; }
    public string Bio { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string KnownAs { get; set; }
    [JsonIgnore]
    public int Id { get; set; }
    public override string? ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}