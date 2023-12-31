

using NLog.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.SetMinimumLevel(LogLevel.Error);
    builder.AddNLog(new NLogProviderOptions
    {
        LoggingConfigurationSectionName = "NLog"
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure Swagger to use Bearer token authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
});


builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddInfraStructureServices(builder.Configuration);

builder.Services.AddMappser();
builder.Services.AddScoped<LogUserActivity>();
builder.Services.AddSignalR();
builder.Host.UseNLog();

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


using IServiceScope scope = app.Services.CreateScope();

IServiceProvider services = scope.ServiceProvider;
ThreadContext context = services.GetRequiredService<ThreadContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
await context.Database.MigrateAsync();
await ThreadContextSeed.SeedAsync(context, loggerFactory);
await UserSeed.SeedUsers(userManager, roleManager);

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(x => x.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<ReactionPostHub>("hubs/reactionPost");
app.MapFallbackToController("Index", "Fallback");

app.Run();
