using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications.CoomentSpecifications;
public class CommentSpecification : BaseSpecification<Comment>
{
    private CommentSpecification(
        Expression<Func<Comment, bool>> criteria,
        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include,
        int skip,
        int take) : base(criteria)
    {
        Include = include;
        ApplyPaging(skip, take);
    }
    private CommentSpecification(
       Expression<Func<Comment, bool>> criteria,
       Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include = null) : base(criteria) => Include = include;

    public CommentSpecification(int id) : base(p => p.Id == id)
    {
    }

    public static CommentSpecification GetCommentWithoutIncludeSpecification(int id)
    {
        return new CommentSpecification(comment => comment.Id == id);
    }
    public static CommentSpecification GetAllCommentsSpecificationPagination(CommentParams commentParams)
    {
        Expression<Func<Comment, bool>> criteria = commentParams.InnertCommentId.HasValue ? comment => comment.InnerCommentId == commentParams.InnertCommentId : comment => comment.PostId == commentParams.PostId;

        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>> include = comment => comment.Include(c => c.User);

        return new CommentSpecification(criteria, include, (commentParams.PageNumber - 1) * commentParams.CurrentPageSize, commentParams.CurrentPageSize);
    }
    public static CommentSpecification GetAllCommentsSpecification(CommentParams commentParams)
    {
        Expression<Func<Comment, bool>> criteria = commentParams.InnertCommentId.HasValue ? comment => comment.InnerCommentId == commentParams.InnertCommentId : comment => comment.PostId == commentParams.PostId;

        return new CommentSpecification(criteria);
    }
    public static CommentSpecification GetCommentSpecification(int id)
    {
        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>> include = comment => comment.Include(c => c.User);

        return new CommentSpecification(post => post.Id == id, include);
    }
}
