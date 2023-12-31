namespace Thread.Infrastructure.Configurations;
public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");

        builder.HasKey(c => c.Id);

        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasMany(c => c.PostPhotos)
            .WithOne()
            .HasForeignKey(c => c.PostId)
            .IsRequired();

        builder.Property(m => m.Privacy)
            .HasConversion(
                     o => o.ToString(),
                     o => (PostPrivacy)Enum.Parse(typeof(PostPrivacy), o)
                 );

    }
}
