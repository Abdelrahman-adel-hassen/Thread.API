using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Thread.Infrastructure.Context;
public class ThreadContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public ThreadContext(DbContextOptions<ThreadContext> options) : base(options)
    {
    }
    public ThreadContext()
    {

    }

    public DbSet<Comment> Comments { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<UserAllowSeePost> UserAllowSeePosts { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<Trend> Trends { get; set; }
    public DbSet<UserCommentLike> UserCommentLikes { get; set; }
    public DbSet<UserFollow> UserFollows { get; set; }
    public DbSet<UserPostLike> UserPostLikes { get; set; }
    public DbSet<UserPostRetweet> UserPostRetweets { get; set; }
    public DbSet<UserPostView> UserPostViews { get; set; }
    public DbSet<ViewedStory> UserStories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
