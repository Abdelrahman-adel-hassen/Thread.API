namespace Thread.Infrastructure.Configurations;
public class UserStoryConfiguration : IEntityTypeConfiguration<ViewedStory>
{
    public void Configure(EntityTypeBuilder<ViewedStory> builder)
    {
        builder.ToTable("ViewedStory");

        builder.HasKey(us => new { us.UserId, us.StoryId });

        builder.HasOne(us => us.User)
            .WithMany()
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(us => us.Story)
            .WithMany(s => s.UserViewrs)
            .HasForeignKey(us => us.StoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
