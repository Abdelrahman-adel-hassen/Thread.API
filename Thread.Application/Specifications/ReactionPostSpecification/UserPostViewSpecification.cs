namespace Thread.Application.Specifications.ReactionPostSpecification;
public class UserPostViewSpecification : BaseSpecification<UserPostView>
{
    private UserPostViewSpecification(Expression<Func<UserPostView, bool>> criteria) : base(criteria)
    {
    }


    public static UserPostViewSpecification GetUserPostViewSpecification()
    {
        return new UserPostViewSpecification(up => up.UserId == SharedProperities.UserId);
    }
    public static UserPostViewSpecification GetUserPostViewByPostIdSpecification(int postId)
    {
        return new UserPostViewSpecification(up => up.UserId == SharedProperities.UserId && up.PostId == postId);
    }
}
