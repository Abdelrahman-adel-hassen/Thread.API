namespace Thread.API.Controllers;
public class AccountController : BaseApiController
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IUserService _userService;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IUserService userService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if(await _userService.UserExists(registerDto.Name, registerDto.KnownAs))
            return BadRequest("Username is taken or KnownAs is taken");

        if(await _userService.CheckEmailExistsAsync(registerDto.Email))
            return BadRequest("Email is taken");
        var user = registerDto.Adapt<AppUser>();

        user.UserName = registerDto.Name.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if(!result.Succeeded)
            return BadRequest(result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, nameof(UserRole.Member));

        if(!roleResult.Succeeded)
            BadRequest(result.Errors);

        var userToReturnDto = registerDto.Adapt<UserToReturnDto>();

        userToReturnDto.Token = await _tokenService.CreateToken(user);
        return Ok(userToReturnDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserToReturnDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Name.ToLower());

        if(user == null)
            return Unauthorized("Invalid username");

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if(!result.Succeeded)
            return Unauthorized();

        var userToReturnDto = user.Adapt<UserToReturnDto>();
        userToReturnDto.Token = await _tokenService.CreateToken(user);

        return userToReturnDto;
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

}
