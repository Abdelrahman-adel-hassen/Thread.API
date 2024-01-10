namespace Thread.Application.Specifications.UserSpecifications;
public class UserFollowSpecification : BaseSpecification<UserFollow>
{
    private UserFollowSpecification(Expression<Func<UserFollow, bool>> criteria) : base(criteria) { }

    public static UserFollowSpecification GetUsersFollowingSpecification()
    {
        return new(ub => ub.SourceUserId == SharedProperities.UserId);
    }
    public static UserFollowSpecification GetUsersFollowingByUserIdSpecification(int UserId)
    {
        return new(ub => ub.SourceUserId == SharedProperities.UserId && ub.DestinationUserId == UserId);
    }
}
