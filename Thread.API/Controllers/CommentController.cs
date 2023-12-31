using Microsoft.AspNetCore.DataProtection;

namespace Thread.API.Controllers;
[Authorize]
public class CommentController : BaseApiController
{
    private readonly ILogger<CommentController> _logger;
    private readonly ICommentService _commentService;
    private readonly IDataProtector _dataProtector;

    public CommentController(ILogger<CommentController> logger, ICommentService commentService, IDataProtectionProvider dataProtectionProvider)
    {
        _logger = logger;
        _commentService = commentService;
        _dataProtector = dataProtectionProvider.CreateProtector("SecureCoding");
    }
    [HttpGet("{id}", Name = "GetComment")]
    [ProducesResponseType(typeof(CommentToReturnDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetComment(string id)
    {
        var idProtect = int.Parse(_dataProtector.Unprotect(id));

        var commentToReturnDtoResult = await _commentService.GetCommentAsync(idProtect);
        return commentToReturnDtoResult.Match<IActionResult>(Ok, NotFound);
    }
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CommentToReturnDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllComments([FromQuery] CommentParams CommentParams)
    {

        CommentParams.PostId = int.Parse(_dataProtector.Unprotect(CommentParams.PostIdProtector));

        var commentToReturnDtoResult = await _commentService.GetAllCommentsAsync(CommentParams);

        if(commentToReturnDtoResult.IsSuccess)
            Response.AddPaginationHeader(await _commentService.GetAllCommentsCountAsync(CommentParams), CommentParams.PageNumber, CommentParams.CurrentPageSize);

        return commentToReturnDtoResult.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost]
    [ProducesResponseType(typeof(CommentToReturnDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddComment([FromQuery] CommentDto commentDto)
    {
        commentDto.PostId = int.Parse(_dataProtector.Unprotect(commentDto.PostIdProtector));

        if(string.IsNullOrWhiteSpace(commentDto.Content))
            return BadRequest("content of comment connot be empty");


        var result = await _commentService.AddCommentAsync(commentDto);

        if(!result.IsSuccess)
            return BadRequest(result.Error);

        var commentToReturnDto = commentDto.Adapt<CommentToReturnDto>();
        commentToReturnDto.Id = _dataProtector.Protect(result.Value.ToString());

        return CreatedAtRoute("GetPost", new { id = result.Value }, commentToReturnDto);
    }
}