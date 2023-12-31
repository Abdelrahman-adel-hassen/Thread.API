using Thread.Infrastructure.Repositories;

namespace Thread.Infrastructure.UnitOfWorkPattern;
public class UnitOfWork : IUnitOfWork
{
    private readonly ThreadContext _context;
    private Hashtable _repositories;
    public UnitOfWork(ThreadContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if(!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return _repositories[type] as IGenericRepository<TEntity>;
    }
}

