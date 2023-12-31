namespace Thread.Infrastructure.Services;
internal class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;

    public PostService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int, string>> AddPostAsync(PostDto postDto)
    {
        var post = postDto.Adapt<Post>();
        post.UserId = UserIdShared.UserId;

        _unitOfWork.Repository<Post>().Add(post);

        await _unitOfWork.CompleteAsync();

        return post.Id > 0 ? post.Id : "Problem addding post";
    }
    public async Task<Result<bool, string>> UpdatePostAsync(int id, PostDto postDto)
    {
        var post = postDto.Adapt<Post>();
        post.Id = id;

        _unitOfWork.Repository<Post>().Update(post);

        return await _unitOfWork.CompleteAsync() > 0 ? true : "Failed to update post";
    }
    public async Task<Result<bool, string>> DeletePostAsync(int id)
    {
        var post = await _unitOfWork.Repository<Post>().GetEntityWithSpec(PostSpecification.GetPostWithoutIncludeSpecification(id));

        if(post is null)
            return $"post with id:{id} does not exist";

        _unitOfWork.Repository<Post>().Delete(post);

        return await _unitOfWork.CompleteAsync() > 0 ? true : "Failed to delete post";
    }

    public async Task<Result<IReadOnlyList<PostToReturnDto>, string>> GetPostsAsync(PostParams postParams)
    {
        var specification = PostSpecification.GetAllPostsWithPaginationSpecification(postParams);
        var posts = await _unitOfWork.Repository<Post>().ListAsync(specification);

        List<PostToReturnDto> postToReturnDtos = new();
        foreach(var post in posts)
        {
            var IscurrentUserFollowPostOwnerOrIsPublicPost = !postParams.IsFolowing;

            var currentUserIsPostOwner = post.UserId == UserIdShared.UserId;

            if(postParams.IsFolowing && !currentUserIsPostOwner)
                IscurrentUserFollowPostOwnerOrIsPublicPost = await _unitOfWork.Repository<UserFollow>().IsEntityExistWithSpec(UserFollowSpecification.GetUsersFollowingByUserIdSpecification(post.UserId));


            if(IscurrentUserFollowPostOwnerOrIsPublicPost || currentUserIsPostOwner)
            {
                var postToReturnDto = await GetpostToReturnDto(post);

                postToReturnDtos.Add(postToReturnDto);
            }
        }

        return postToReturnDtos;
    }
    public async Task<int> GetPostsCountAsync(PostParams postParams)
    {
        var specification = PostSpecification.GetAllPostsSpecification(postParams);
        var postsCount = await _unitOfWork.Repository<Post>().CountAsync(specification);

        return postsCount;
    }
    public async Task<Result<PostToReturnDto, string>> GetPostAsync(int id)
    {
        var post = await _unitOfWork.Repository<Post>().GetEntityWithSpec(PostSpecification.GetPostSpecification(id));

        PostToReturnDto postToReturnDto = post is null ? new PostToReturnDto() : await GetpostToReturnDto(post);

        return post is null ? $"Post not found with id {id}" : postToReturnDto;
    }

    public async Task<Result<IReadOnlyList<PostToReturnDto>, string>> GetAllTrendPosts(TrendParams trendParams)
    {

        var trendPosts = await _unitOfWork.Repository<PostTrend>().ListAsync(TrendPostSpecification.GetAllTrendPostsSpecification(trendParams));
        if(trendPosts is null)
            return $"Posts not found with trendId {trendParams.Id}";


        var postToReturnDto = new List<PostToReturnDto>();

        foreach(var trendPost in trendPosts)
        {
            postToReturnDto.Add(await GetpostToReturnDto(trendPost.Post));
        }
        return postToReturnDto;

    }

    private async Task<PostToReturnDto> GetpostToReturnDto(Post post)
    {
        var postToReturnDto = post.Adapt<PostToReturnDto>();

        postToReturnDto.IsLikedByCurrentUser = await _unitOfWork.Repository<UserPostLike>().IsEntityExistWithSpec(UserPostLikeSpecification.GetUserPostLikeByPostIdSpecification(post.Id));

        postToReturnDto.IsRetweetByCurrentUser = await _unitOfWork.Repository<UserPostRetweet>().IsEntityExistWithSpec(ReactionPostRetweetSpecification.GetUserPostRetweetByPostIdSpecification(post.Id));

        return postToReturnDto;
    }

    public async Task<int> GetPostsTrendCountAsync(int id)
    {
        var specification = TrendPostSpecification.GetAllTrendPostsByIdSpecification(id);
        var postsCount = await _unitOfWork.Repository<PostTrend>().CountAsync(specification);

        return postsCount;
    }


}
