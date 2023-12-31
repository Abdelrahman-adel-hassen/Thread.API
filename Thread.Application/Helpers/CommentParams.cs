namespace Thread.Application.Helpers;
public class CommentParams : PaginationParams
{
    public string PostIdProtector { get; set; }
    [JsonIgnore]
    public int PostId { get; set; }
    public int? InnertCommentId { get; set; }
}
