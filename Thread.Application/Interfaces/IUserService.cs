namespace Thread.Application.Interfaces;
public interface IUserService
{
    Task<Result<IReadOnlyList<UserToReturnDto>, string>> GetUsersAsync(string userParams);
    Task<Result<UserToReturnDto, string>> GetUserAsync(int id);
    Task<Result<UserToReturnDto, string>> GetUserWithoutIncludeAsync(int id);
    Task<Result<int, string>> AddUserPhotoAsync(UserPhotoDto userPhotoDto);
    Task<Result<bool, string>> UpdateUserAsync(UserDto userDto);
    Task<Result<bool, string>> DeletePhotoAsync(int photoId);
    Task<bool> CheckEmailExistsAsync(string email);
    Task<bool> UserExists(string userName, string knownAs);
    Task<AppUser?> GetUserWithoutInclude(int id);
}
