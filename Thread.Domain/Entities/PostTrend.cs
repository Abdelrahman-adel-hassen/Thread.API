namespace Thread.Domain.Entities;
public class PostTrend : BaseEntity
{
    [NotMapped]
    public new int Id { get; set; }
    public int TrendId { get; set; }
    public Trend Trend { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
}
