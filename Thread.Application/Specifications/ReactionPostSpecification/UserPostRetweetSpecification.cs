namespace Thread.Application.Specifications.ReactionPostSpecification;
public class ReactionPostRetweetSpecification : BaseSpecification<UserPostRetweet>
{
    private ReactionPostRetweetSpecification(Expression<Func<UserPostRetweet, bool>> criteria) : base(criteria)
    {
    }


    public static ReactionPostRetweetSpecification GetUserPostRetweetSpecification()
    {

        return new ReactionPostRetweetSpecification(up => up.UserId == UserIdShared.UserId);
    }
    public static ReactionPostRetweetSpecification GetUserPostRetweetByPostIdSpecification(int postId)
    {
        return new ReactionPostRetweetSpecification(up => up.UserId == UserIdShared.UserId && up.PostId == postId);
    }
}
