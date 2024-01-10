namespace Thread.Infrastructure.Specifications;
public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if(spec.Criteria != null)
            query = query.Where(spec.Criteria);

        if(spec.OrderBy != null)
            query = spec.OrderBy(query);

        if(spec.IsPagingEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        if(spec.DisableTracking)
            query = query.AsNoTracking();
        if(spec.Include != null)
        {
            query = spec.Include(query);
        }
        return query;
    }
}

