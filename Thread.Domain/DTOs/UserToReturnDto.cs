namespace Thread.Domain.DTOs;
public class UserToReturnDto
{
    public DateOnly DateOfBirth { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public required string Name { get; set; }
    public string Bio { get; set; }
    public required string KnownAs { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastActive { get; set; }
    public ICollection<UserPhotoDto> Photos { get; set; }
    public string Token { get; set; }

    public override string? ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
