
namespace Thread.Domain.Entities;
public class Post : BaseEntity
{
    public string? Body { get; set; }
    public PostPrivacy Privacy { get; set; }
    public int UserId { get; set; }
    public AppUser User { get; set; }
    public int NumberOfComments { get; set; } = 0;
    public int NumberOfLikes { get; set; } = 0;
    public int NumberOfRetweets { get; set; } = 0;
    public int NumberOfViews { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<PostPhoto>? PostPhotos { get; set; }
}
