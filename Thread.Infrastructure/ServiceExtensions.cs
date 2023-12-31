namespace Thread.Infrastructure;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfraStructureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                 .AddScoped<IUnitOfWork, UnitOfWork>()
                 .AddScoped<IPostService, PostService>()
                 .AddScoped<IUserService, UserService>()
                 .AddScoped<ITokenService, TokenService>()
                 .AddScoped<ICommentService, CommentService>()
                 .AddScoped<ITrendService, TrendService>()
                 .AddHttpContextAccessor()
                 .AddDbContext<ThreadContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                           builder => { _ = builder.UseDateOnlyTimeOnly(); _ = builder.MigrationsAssembly(typeof(ThreadContext).Assembly.FullName); }
                   ));


        service.AddAuthorization(opt =>
                {
                    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole(nameof(UserRole.Admin)));
                    opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole(nameof(UserRole.Admin), nameof(UserRole.Moderator)));
                });

        return service;
    }
}
