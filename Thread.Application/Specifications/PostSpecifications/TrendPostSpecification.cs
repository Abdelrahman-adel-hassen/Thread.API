using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications.PostSpecifications;
public class TrendPostSpecification : BaseSpecification<PostTrend>
{
    private TrendPostSpecification(Expression<Func<PostTrend, bool>> criteria) : base(criteria)
    {

    }
    private TrendPostSpecification(
        Expression<Func<PostTrend, bool>> criteria,
        Func<IQueryable<PostTrend>, IIncludableQueryable<PostTrend, object>> include,
        int skip,
        int take) : base(criteria)
    {
        Include = include;
        ApplyPaging(skip, take);
    }

    public static TrendPostSpecification GetAllTrendPostsSpecification(TrendParams trendParams)
    {

        Func<IQueryable<PostTrend>, IIncludableQueryable<PostTrend, object>> include =
            trendPost => trendPost.Include(tp => tp.Post)
                                  .ThenInclude(p => p.User)
                                  .Include(tp => tp.Post)
                                  .ThenInclude(p => p.PostPhotos);

        return new TrendPostSpecification(tp => tp.TrendId == trendParams.Id, include, (trendParams.PageNumber - 1) * trendParams.CurrentPageSize, trendParams.CurrentPageSize);
    }
    public static TrendPostSpecification GetAllTrendPostsByIdSpecification(int id)
    {
        return new TrendPostSpecification(tp => tp.TrendId == id);
    }
}
