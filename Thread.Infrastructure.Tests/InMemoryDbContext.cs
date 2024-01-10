using Microsoft.EntityFrameworkCore;
using Thread.Infrastructure.Context;

namespace Thread.Infrastructure.Tests;
internal class InMemoryDbContext : ThreadContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
    public override void Dispose()
    {
        Database.EnsureDeleted();
        base.Dispose();
    }
}
