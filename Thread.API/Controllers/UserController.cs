namespace Thread.API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    => _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string userName)
    {
        var reult = await _userService.GetUsersAsync(userName);

        return reult.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> GetUser(int id)
    {
        var reult = await _userService.GetUserAsync(id);

        return reult.Match<IActionResult>(Ok, NotFound);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserDto userDto)
    {
        if(string.IsNullOrWhiteSpace(userDto.Name))
            return BadRequest("Name is required");

        userDto.Id = User.GetUserId();
        var result = await _userService.UpdateUserAsync(userDto);
        return result.Match<IActionResult>(_ => Ok(), BadRequest);
    }

    [HttpPost("Photo")]
    public async Task<IActionResult> AddPhoto(UserPhotoDto userPhotoDto)
    {
        userPhotoDto.UserId = User.GetUserId();
        var result = await _userService.AddUserPhotoAsync(userPhotoDto);
        var id = User.GetUsername();
        return result.Match<IActionResult>(_ => CreatedAtRoute("GetUser", new { id = userPhotoDto.UserId }, userPhotoDto), BadRequest);


    }

    [HttpDelete("Photo/{photoId}")]
    public async Task<IActionResult> DeletePhoto(int photoId)
    {
        var result = await _userService.DeletePhotoAsync(photoId);

        return result.Match<IActionResult>(_ => Ok(), BadRequest);

    }
}