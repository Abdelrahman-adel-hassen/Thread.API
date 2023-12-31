namespace Thread.Infrastructure.Services;
internal class ReactionPostService : IReactionPostService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReactionPostService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int, string>> AddPostAsync(PostDto postDto)
    {
        var post = postDto.Adapt<Post>();

        _unitOfWork.Repository<Post>().Add(post);

        await _unitOfWork.CompleteAsync();

        return post.Id > 0 ? post.Id : "Problem addding Post";
    }

    public async Task<Result<bool, string>> UpdateLikeAsync(int postId)
    {
        var likePost = await _unitOfWork.Repository<UserPostLike>().GetEntityWithSpec(UserPostLikeSpecification.GetUserPostLikeByPostIdSpecification(postId));

        var userFackeId = 6;
        var isAdded = true;

        if(likePost is null)
        {
            likePost = new() { UserId = userFackeId, PostId = postId };
            _unitOfWork.Repository<UserPostLike>().Add(likePost);
        }
        else
        {
            isAdded = false;
            _unitOfWork.Repository<UserPostLike>().Delete(likePost);
        }

        return await _unitOfWork.CompleteAsync() > 0 ? isAdded : "Failed to update LikePost";
    }

    public async Task<Result<bool, string>> UpdateRetweetAsync(int postId)
    {
        var postRetweet = await _unitOfWork.Repository<UserPostRetweet>().GetEntityWithSpec(ReactionPostRetweetSpecification.GetUserPostRetweetByPostIdSpecification(postId));

        var userFackeId = 6;
        var isAdded = true;

        if(postRetweet is null)
        {
            postRetweet = new() { UserId = userFackeId, PostId = postId };
            _unitOfWork.Repository<UserPostRetweet>().Add(postRetweet);
        }
        else
        {
            isAdded = false;
            _unitOfWork.Repository<UserPostRetweet>().Delete(postRetweet);
        }

        return await _unitOfWork.CompleteAsync() > 0 ? isAdded : "Failed to update retweetPost";
    }
    public async Task<Result<bool, string>> UpdateViewAsync(int postId)
    {
        var postView = await _unitOfWork.Repository<UserPostView>().GetEntityWithSpec(UserPostViewSpecification.GetUserPostViewByPostIdSpecification(postId));

        var userFackeId = 6;
        var isAdded = true;
        if(postView is null)
        {
            postView = new() { UserId = userFackeId, PostId = postId };
            _unitOfWork.Repository<UserPostView>().Add(postView);
        }
        else
        {
            isAdded = false;
            _unitOfWork.Repository<UserPostView>().Delete(postView);
        }

        return await _unitOfWork.CompleteAsync() > 0 ? isAdded : "Failed to update retweetPost";
    }
}
