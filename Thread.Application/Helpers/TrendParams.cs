namespace Thread.Application.Helpers;
public class TrendParams : PaginationParams
{
    public string IdProtector { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    public int Id { get; set; }
}
