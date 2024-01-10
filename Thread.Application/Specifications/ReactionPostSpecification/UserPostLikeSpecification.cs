namespace Thread.Application.Specifications.ReactionPostSpecification;
public class UserPostLikeSpecification : BaseSpecification<UserPostLike>
{
    private UserPostLikeSpecification(Expression<Func<UserPostLike, bool>> criteria) : base(criteria)
    {
    }


    public static UserPostLikeSpecification GetUserPostLikeSpecification()
    {
        return new UserPostLikeSpecification(up => up.UserId == SharedProperities.UserId);
    }
    public static UserPostLikeSpecification GetUserPostLikeByPostIdSpecification(int postId)
    {
        return new UserPostLikeSpecification(up => up.UserId == SharedProperities.UserId && up.PostId == postId);
    }

}
