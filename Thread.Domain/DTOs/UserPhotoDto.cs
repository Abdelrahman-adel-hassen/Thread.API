namespace Thread.Domain.DTOs;
public class UserPhotoDto
{
    public string Url { get; set; }
    public bool IsBackground { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }
}

