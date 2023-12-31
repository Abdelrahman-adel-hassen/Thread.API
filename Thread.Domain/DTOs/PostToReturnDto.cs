namespace Thread.Domain.DTOs;
public class PostToReturnDto
{
    public string Id { get; set; }
    public string? Body { get; set; }
    public PostPrivacy Privacy { get; set; }
    public bool IsLikedByCurrentUser { get; set; }
    public bool IsRetweetByCurrentUser { get; set; }
    public UserDto User { get; set; }
    public IReadOnlyList<string> Photos { get; set; }
    public IReadOnlyList<CommentDto> Comments { get; set; }
    public int NumberOfComments { get; set; }
    public int NumberOfLikes { get; set; }
    public int NumberOfRetweet { get; set; }
    public int NumberOfViews { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
