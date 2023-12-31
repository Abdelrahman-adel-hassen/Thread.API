namespace Thread.Domain.DTOs;

public class CommentToReturnDto
{
    public string Id { get; set; }
    public int NumberOfInnerComments { get; set; }
    public UserToReturnDto User { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
