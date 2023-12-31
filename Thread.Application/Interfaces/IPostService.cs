namespace Thread.Application.Interfaces;
public interface IPostService
{
    Task<Result<IReadOnlyList<PostToReturnDto>, string>> GetPostsAsync(PostParams postParams);
    Task<Result<PostToReturnDto, string>> GetPostAsync(int id);
    Task<Result<int, string>> AddPostAsync(PostDto postDto);
    Task<Result<bool, string>> DeletePostAsync(int id);
    Task<Result<bool, string>> UpdatePostAsync(int id, PostDto postDto);
    Task<Result<IReadOnlyList<PostToReturnDto>, string>> GetAllTrendPosts(TrendParams trendParams);
    Task<int> GetPostsCountAsync(PostParams postParams);
    Task<int> GetPostsTrendCountAsync(int id);

}
