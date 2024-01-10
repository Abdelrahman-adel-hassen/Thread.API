namespace Thread.Infrastructure.Services;
public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int, string>> AddCommentAsync(CommentDto commentDto)
    {
        var comment = commentDto.Adapt<Comment>();
        var comments = new List<Comment>() { comment };
        if(commentDto.InnerCommentId.HasValue)
        {
            var parentComment = await _unitOfWork.Repository<Comment>().GetByIdAsync(commentDto.InnerCommentId.Value);
            parentComment.NumberOfInnerComments++;
            comments.Add(parentComment);
        }

        await _unitOfWork.Repository<Comment>().AddRangeAsync(comments);

        await _unitOfWork.CompleteAsync();

        return comment.Id > 0 ? comment.Id : "Problem addding Comment";
    }

    public async Task<Result<int, string>> DeleteCommentAsync(int id)
    {
        var comment = await _unitOfWork.Repository<Comment>().GetEntityWithSpec(CommentSpecification.GetCommentWithoutIncludeSpecification(id));

        if(comment is null)
            return $"Comment with id:{id} does not exist";

        _unitOfWork.Repository<Comment>().Delete(comment);

        return comment.PostId;
    }

    public async Task<Result<IReadOnlyList<CommentToReturnDto>, string>> GetAllCommentsAsync(CommentParams CommentParams)
    {
        var specification = CommentSpecification.GetAllCommentsSpecificationPagination(CommentParams);
        var Comments = await _unitOfWork.Repository<Comment>().ListAsync(specification);

        return Comments.Adapt<List<CommentToReturnDto>>();
    }
    public async Task<int> GetAllCommentsCountAsync(CommentParams CommentParams)
    {
        var specification = CommentSpecification.GetAllCommentsSpecification(CommentParams);
        var commentsCount = await _unitOfWork.Repository<Comment>().CountAsync(specification);
        return commentsCount;
    }
    public async Task<Result<CommentToReturnDto, string>> GetCommentAsync(int id)
    {
        var specification = CommentSpecification.GetCommentSpecification(id);
        var Comment = await _unitOfWork.Repository<Comment>().GetEntityWithSpec(specification);

        return Comment is null ? $"Comment with id:{id} does not exist" : Comment.Adapt<CommentToReturnDto>();
    }

}
