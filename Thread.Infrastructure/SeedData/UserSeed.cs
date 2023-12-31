namespace Thread.Infrastructure.SeedData;
public class UserSeed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        if(await userManager.Users.AnyAsync())
            return;
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        var userData = File.ReadAllText(path + @"/SeedData/Data/Users.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        if(users == null)
            return;

        var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

        foreach(var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        foreach(var user in users)
        {
            user.UserName = user.UserName.ToLower();
            await userManager.CreateAsync(user, "Pa$$w0rd1");
            await userManager.AddToRoleAsync(user, "Member");
        }

        var admin = new AppUser
        {
            UserName = "admin",
            KnownAs = "bido",
        };

        //await userManager.CreateAsync(admin, "Pa1A$$w0rd");
        // await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

    }
}

