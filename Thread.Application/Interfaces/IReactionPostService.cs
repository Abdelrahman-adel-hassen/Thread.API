namespace Thread.Application.Interfaces;
public interface IReactionPostService
{
    Task<Result<bool, string>> UpdateLikeAsync(int postId);
    Task<Result<bool, string>> UpdateRetweetAsync(int id);
    Task<Result<bool, string>> UpdateViewAsync(int id);
}
