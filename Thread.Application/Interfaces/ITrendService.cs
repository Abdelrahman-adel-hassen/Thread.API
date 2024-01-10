namespace Thread.Application.Interfaces;
public interface ITrendService
{
    Task<Result<bool, string>> AddTrend(string tag, int postId);

}
