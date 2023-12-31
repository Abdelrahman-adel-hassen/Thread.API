using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications.PostSpecifications;
public class PostSpecification : BaseSpecification<Post>
{
    private PostSpecification(
        Expression<Func<Post, bool>> criteria,
        Func<IQueryable<Post>, IIncludableQueryable<Post, object>> include,
        int skip,
        int take) : base(criteria)
    {
        Include = include;
        ApplyPaging(skip, take);
    }
    private PostSpecification(
      Expression<Func<Post, bool>> criteria,
      Func<IQueryable<Post>, IIncludableQueryable<Post, object>> include = null) : base(criteria)
    {
        Include = include;

    }
    public static PostSpecification GetAllPostsWithPaginationSpecification(PostParams postParams)
    {

        Expression<Func<Post, bool>> criteria = postParams.ByUserId ? post => post.UserId == UserIdShared.UserId : post => postParams.IsFolowing ? post.Privacy == PostPrivacy.Follower : post.Privacy == PostPrivacy.Public;

        Func<IQueryable<Post>, IIncludableQueryable<Post, object>> include =
           post => post.Include(p => p.User)
                       .Include(p => p.PostPhotos);

        return new PostSpecification(criteria, include, (postParams.PageNumber - 1) * postParams.CurrentPageSize, postParams.CurrentPageSize);
    }
    public static PostSpecification GetAllPostsSpecification(PostParams postParams)
    {

        Expression<Func<Post, bool>> criteria = postParams.ByUserId ? post => post.UserId == UserIdShared.UserId : post => postParams.IsFolowing ? post.Privacy == PostPrivacy.Follower : post.Privacy == PostPrivacy.Public;

        return new PostSpecification(criteria);
    }
    public static PostSpecification GetPostSpecification(int id)
    {

        Func<IQueryable<Post>, IIncludableQueryable<Post, object>> include =
            post => post.Include(p => p.User)
                        .Include(p => p.PostPhotos);

        return new PostSpecification(post => post.Id == id, include);
    }
    public static PostSpecification GetPostWithoutIncludeSpecification(int id)
    {
        return new PostSpecification(post => post.Id == id);
    }
}
