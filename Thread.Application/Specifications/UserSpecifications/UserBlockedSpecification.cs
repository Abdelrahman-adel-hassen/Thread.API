using Microsoft.EntityFrameworkCore.Query;

namespace Thread.Application.Specifications.UserSpecifications;
public class UserBlockedSpecification : BaseSpecification<UserBlock>
{
    private UserBlockedSpecification(Expression<Func<UserBlock, bool>> criteria, Func<IQueryable<UserBlock>, IIncludableQueryable<UserBlock, object>> include = null) : base(criteria) => Include = include;

    public UserBlockedSpecification(int id) : base(p => p.Id == id)
    {
    }
    public static UserBlockedSpecification GetUsersBlockedSpecification()
    {
        return new UserBlockedSpecification(UB => UB.DestinationUserId == UserIdShared.UserId || UB.SourceUserId == UserIdShared.UserId);
    }
}
