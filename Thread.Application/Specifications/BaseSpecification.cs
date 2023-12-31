using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{

    public BaseSpecification(Expression<Func<T, bool>> criteria) => Criteria = criteria;

    public Expression<Func<T, bool>> Criteria { get; set; }

    public Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public Expression<Func<T, object>> selector => null;

    public bool DisableTracking => false;

    Func<IQueryable<T>, IOrderedQueryable<T>> ISpecification<T>.OrderBy => null;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}

