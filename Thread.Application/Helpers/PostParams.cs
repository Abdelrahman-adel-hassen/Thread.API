
namespace Thread.Application.Helpers;
public class PostParams : PaginationParams
{
    public bool IsFolowing { get; set; } = false;
    public bool ByUserId { get; set; } = false;

}
