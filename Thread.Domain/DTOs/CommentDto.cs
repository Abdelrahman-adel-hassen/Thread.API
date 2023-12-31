
namespace Thread.Domain.DTOs;
public class CommentDto
{
    public string Content { get; set; }
    public int? InnerCommentId { get; set; }
    public string PostIdProtector { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    public int PostId { get; set; }
}