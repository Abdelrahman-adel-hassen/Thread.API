using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications.UserSpecifications;
public class UserSpecification : BaseSpecification<AppUser>
{
    private UserSpecification(Expression<Func<AppUser, bool>> criteria, Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include = null) : base(criteria) => Include = include;

    public UserSpecification(int id) : base(p => p.Id == id)
    {
    }
    public static UserSpecification GetUserSpecification(string userName)
    {
        Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include =
           trendPost => trendPost.Include(tp => tp.Photos);

        return new(U => U.UserName == userName.ToLower(), include);
    }
    public static UserSpecification GetUsersSpecification(string userName)
    {
        Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include =
           trendPost => trendPost.Include(au => au.Photos);

        return new(U => U.UserName.Contains(userName.ToLower()), include);
    }
    public static UserSpecification GetUsersWithoutIncludesSpecification(string userName) => new(U => U.UserName.Contains(userName.ToLower()));
}
