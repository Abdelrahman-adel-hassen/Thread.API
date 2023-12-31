namespace Thread.Application.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
    public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, ClaimsPrincipal user)
    {
        var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
    }
}