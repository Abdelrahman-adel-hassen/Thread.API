namespace Thread.Domain.DTOs;
public record TrendDto(string Tag)
{
    public int NumberOfInnerPosts { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
