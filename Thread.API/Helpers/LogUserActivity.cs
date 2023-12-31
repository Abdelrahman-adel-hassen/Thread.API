using Thread.Domain.Helpers;

namespace Thread.API.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    private readonly IUserService _userService;


    public LogUserActivity(IUserService userService)
    {
        _userService = userService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if(!resultContext.HttpContext.User.Identity.IsAuthenticated)
            return;

        var userId = resultContext.HttpContext.User.GetUserId();
        UserIdShared.UserId = userId;

        var user = await _userService.GetUserWithoutInclude(userId);

        if(user != null)
        {
            var userToReturn = user;

            userToReturn.LastActive = DateTime.UtcNow;

            var userDto = userToReturn.Adapt<UserDto>();

            await _userService.UpdateUserAsync(userDto);
        }
    }
}