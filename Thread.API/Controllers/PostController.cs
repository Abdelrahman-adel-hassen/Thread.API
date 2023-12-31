using Microsoft.AspNetCore.DataProtection;

namespace Thread.API.Controllers;

[Authorize]
public class PostController : BaseApiController
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostService _postService;
    private readonly ITrendService _trendService;
    private readonly IDataProtector _dataProtector;
    public PostController(ILogger<PostController> logger, IPostService PostService, ITrendService trendService, IDataProtectionProvider dataProtectionProvider)
    {
        _logger = logger;
        _postService = PostService;
        _trendService = trendService;
        _dataProtector = dataProtectionProvider.CreateProtector("SecureCoding");

    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PostToReturnDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllPosts([FromQuery] PostParams postParams)
    {
        var postToReturnDtoResult = await _postService.GetPostsAsync(postParams);

        if(postToReturnDtoResult.IsSuccess)
            Response.AddPaginationHeader(await _postService.GetPostsCountAsync(postParams), postParams.PageNumber, postParams.CurrentPageSize);

        return postToReturnDtoResult.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PostToReturnDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddPost([FromBody] PostDto postDto)
    {
        if(string.IsNullOrWhiteSpace(postDto.Body) && postDto.Photos.Count == 0)
            return BadRequest("content of post connot be empty");

        var result = await _postService.AddPostAsync(postDto);

        if(!result.IsSuccess)
            return BadRequest(result.Error);

        var postId = result.Value;

        await _trendService.AddTrend(postDto.Body, postId);

        var postToReturnDto = postDto.Adapt<PostToReturnDto>();
        postToReturnDto.Id = _dataProtector.Protect(postId.ToString());

        return CreatedAtRoute("GetPost", new { id = postId }, postToReturnDto);
    }

    [HttpGet("{id}", Name = "GetPost")]
    [ProducesResponseType(typeof(PostToReturnDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPost(string id)
    {
        var idProtect = int.Parse(_dataProtector.Unprotect(id));
        var postToReturnDtoResult = await _postService.GetPostAsync(idProtect);
        return postToReturnDtoResult.Match<IActionResult>(Ok, NotFound);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeletePost(string id)
    {
        var idProtect = int.Parse(_dataProtector.Unprotect(id));
        var result = await _postService.DeletePostAsync(idProtect);

        return result.Match<IActionResult>(isDeleted => Ok(), BadRequest);
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePost([FromQuery] string id, [FromBody] PostDto postDto)
    {

        var idProtect = int.Parse(_dataProtector.Unprotect(id));
        if(postDto.Body?.Trim()?.Length == 0 && postDto.Photos?.Count == 0)
            BadRequest("content of post connot be empty");


        var result = await _postService.UpdatePostAsync(idProtect, postDto);

        return result.Match<IActionResult>(isUpdated => NoContent(), BadRequest);
    }

    [HttpGet("Trend")]
    [ProducesResponseType(typeof(IReadOnlyList<PostToReturnDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllTrendPosts([FromQuery] TrendParams trendParams)
    {
        trendParams.Id = int.Parse(_dataProtector.Unprotect(trendParams.IdProtector));

        var postToReturnDtoResult = await _postService.GetAllTrendPosts(trendParams);

        if(postToReturnDtoResult.IsSuccess)
            Response.AddPaginationHeader(await _postService.GetPostsTrendCountAsync(trendParams.Id), trendParams.PageNumber, trendParams.CurrentPageSize);

        return postToReturnDtoResult.Match<IActionResult>(Ok, NotFound);
    }
}
