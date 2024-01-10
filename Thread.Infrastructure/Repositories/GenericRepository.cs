using Thread.Infrastructure.Specifications;

namespace Thread.Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ThreadContext _context;
    public GenericRepository(ThreadContext context)
    {
        _context = context;
    }
    public IQueryable<T> GetQurabale() => _context.Set<T>().AsQueryable();

    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> ListAllAsync() => await _context.Set<T>().ToListAsync();

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec) => await ApplySpecification(spec).ToListAsync();
    public async Task<bool> IsEntityExistWithSpec(ISpecification<T> spec) => await GetEntityWithSpec(spec) != null;

    public async Task<int> CountAsync(ISpecification<T> spec) => await ApplySpecification(spec).CountAsync();

    private IQueryable<T> ApplySpecification(ISpecification<T> spec) => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);

    public void Add(T entity) => _context.Set<T>().AddAsync(entity);
    public async Task AddRangeAsync(ICollection<T> entity) => await _context.Set<T>().AddRangeAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);


    public void Delete(T entity) => _context.Set<T>().Remove(entity);


}

