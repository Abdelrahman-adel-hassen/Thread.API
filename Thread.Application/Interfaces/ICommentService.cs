namespace Thread.Application.Interfaces;
public interface ICommentService
{
    Task<Result<IReadOnlyList<CommentToReturnDto>, string>> GetAllCommentsAsync(CommentParams CommentParams);
    Task<Result<CommentToReturnDto, string>> GetCommentAsync(int id);
    Task<Result<int, string>> AddCommentAsync(CommentDto CommentDto);
    Task<Result<int, string>> DeleteCommentAsync(int id);
    Task<int> GetAllCommentsCountAsync(CommentParams CommentParams);

}
