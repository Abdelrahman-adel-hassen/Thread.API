namespace Thread.Infrastructure.Services;
public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ILogger<UserService> logger)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
    }


    public async Task<Result<IReadOnlyList<UserToReturnDto>, string>> GetUsersAsync(string userName)
    {
        _logger.LogInformation("start getting users with userName contains {username}", userName);

        var users = await _userManager.Users.Include(u => u.Photos)
                                            .Where(u => u.UserName.Contains(userName.ToLower()))
                                            .ToListAsync();
        var userToReturnDto = users.Adapt<List<UserToReturnDto>>();

        _logger.LogInformation("the result of Getting users with userName contains {username} is {users}", userName, string.Join(',', userToReturnDto));

        return userToReturnDto;
    }
    public async Task<Result<UserToReturnDto, string>> GetUserAsync(int id)
    {
        _logger.LogInformation("start getting user with id : {id}", id);

        var user = await GetUser(id);

        var userToReturnDto = user.Adapt<UserToReturnDto>();

        _logger.LogInformation("the result of Getting user with id : {id} is {user}", id, userToReturnDto);
        return userToReturnDto;
    }
    public async Task<Result<UserToReturnDto, string>> GetUserWithoutIncludeAsync(int id)
    {
        _logger.LogInformation("start getting user without Include with id : {id}", id);

        var user = await GetUserWithoutInclude(id);
        var userToReturnDto = user.Adapt<UserToReturnDto>();

        _logger.LogInformation("the result of getting user without Include with id : {id} is {user}", id, userToReturnDto);

        return userToReturnDto;
    }
    public async Task<Result<bool, string>> UpdateUserAsync(UserDto userDto)
    {
        _logger.LogInformation("start updating user with data : {userDto}", userDto);

        var user = await GetUser(userDto.Id);
        user = userDto.Adapt(user);

        _logger.LogInformation("the result of getting user with data to begin updating : {userDto} is {user}", userDto, user);

        var result = user is null ? new() : await _userManager.UpdateAsync(user);

        _logger.LogInformation("the result of updating user with data : {userDto} is {result}", userDto, result.Succeeded);


        return result.Succeeded ? true : "Failed to update user";
    }
    public async Task<Result<int, string>> AddUserPhotoAsync(UserPhotoDto userPhotoDto)
    {

        var userPhoto = userPhotoDto.Adapt<UserPhoto>();

        _unitOfWork.Repository<UserPhoto>().Add(userPhoto);

        return await _unitOfWork.CompleteAsync() > 0
            ? userPhoto.Id
            : "failed adding photo";
    }

    public async Task<Result<bool, string>> DeletePhotoAsync(int photoId)
    {
        AppUser? user = await GetUser(3);

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if(photo == null)
            return "Photo Not Found";

        user.Photos.Remove(photo);

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded ? true : "Failed to delete photo";
    }

    private async Task<AppUser?> GetUser(int id)
    {
        return await _userManager.Users.Where(u => u.Id == id)
                                       .Include(u => u.Photos)
                                       .FirstOrDefaultAsync();
    }
    public async Task<AppUser?> GetUserWithoutInclude(int id)
    {
        return await _userManager.Users.Where(u => u.Id == id)
                                       .FirstOrDefaultAsync();
    }
    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> UserExists(string userName, string knownAs)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == userName.ToLower() || x.KnownAs == knownAs.ToLower());
    }
}
