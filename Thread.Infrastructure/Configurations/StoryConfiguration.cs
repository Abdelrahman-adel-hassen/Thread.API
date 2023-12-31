namespace Thread.Infrastructure.Configurations;
public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable("Story");

        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(c => c.Photo)
         .WithOne(c => c.Story)
         .HasForeignKey<StoryPhoto>(c => c.StoryId)
         .OnDelete(DeleteBehavior.Cascade)
         .IsRequired();

    }
}
