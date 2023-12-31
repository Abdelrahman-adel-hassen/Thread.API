using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications;
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    Expression<Func<T, object>> selector { get; }
    Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }
    Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; }

    bool DisableTracking { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
