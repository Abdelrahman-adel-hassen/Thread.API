namespace Thread.API.SignalR;

public class ReactionPostHub : Hub
{
    private readonly ICommentService _commentService;
    private readonly IReactionPostService _reactionPostService;
    private readonly IUnitOfWork _unitOfWork;

    public ReactionPostHub(ICommentService commentService, IUnitOfWork unitOfWork, IReactionPostService reactionPostService)
    {
        _commentService = commentService;
        _unitOfWork = unitOfWork;
        _reactionPostService = reactionPostService;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var postId = httpContext.Request.Query["postId"].ToString();
        await Groups.AddToGroupAsync(Context.ConnectionId, $"post-{postId}");
    }

    public async Task<int> AddComment(CommentDto commentDto)
    {
        if(string.IsNullOrWhiteSpace(commentDto.Content))
            throw new HubException("Comment must have content");

        var post = await _unitOfWork.Repository<Post>().GetByIdAsync(commentDto.PostId);
        post.NumberOfComments++;

        var result = await _commentService.AddCommentAsync(commentDto);

        var id = result.Match(id => id, error => throw new HubException(error));

        await Clients.Group($"post-{commentDto.PostId}").SendAsync("UpdatedPost", post.Adapt<PostToReturnDto>());

        return await _unitOfWork.CompleteAsync() > 0 ? id : throw new HubException("Failed To adding Comment");
    }
    public async Task<int> DeleteComment(int id)
    {

        var result = await _commentService.DeleteCommentAsync(id);

        var postId = result.Match(postId => postId, error => throw new HubException(error));

        var post = await _unitOfWork.Repository<Post>().GetByIdAsync(postId);
        post.NumberOfComments--;

        var idResult = result.Match(id => id, error => throw new HubException(error));

        await Clients.Group($"post-{postId}").SendAsync("UpdatedPost", post.Adapt<PostToReturnDto>());

        return await _unitOfWork.CompleteAsync() > 0 ? id : throw new HubException("Failed To deleting comment");
    }
    public async Task UpdateReactionPost(int postId, ReactionPost reactionPost)
    {
        var result = reactionPost switch
        {
            ReactionPost.LikePost => await _reactionPostService.UpdateLikeAsync(postId),
            ReactionPost.RetweetPost => await _reactionPostService.UpdateRetweetAsync(postId),
            _ => await _reactionPostService.UpdateViewAsync(postId)
        };

        var isAdded = result.Match(isAdded => isAdded, error => throw new HubException(error));

        var post = await _unitOfWork.Repository<Post>().GetByIdAsync(postId);

        _ = reactionPost switch
        {
            ReactionPost.LikePost => post.NumberOfLikes += isAdded ? 1 : -1,
            ReactionPost.RetweetPost => post.NumberOfRetweets += isAdded ? 1 : -1,
            _ => post.NumberOfViews += isAdded ? 1 : -1
        };

        if(await _unitOfWork.CompleteAsync() == 0)
            throw new HubException("Failed To reaction post");

        await Clients.Group($"post-{postId}").SendAsync("UpdatedPost", post.Adapt<PostToReturnDto>());

    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var httpContext = Context.GetHttpContext();
        var postId = httpContext?.Request.Query["postId"].ToString();
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"post-{postId}");
    }
}
