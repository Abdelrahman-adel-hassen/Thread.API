namespace Thread.Infrastructure.Configurations;
public class UserPostLikeConfiguration : IEntityTypeConfiguration<UserPostLike>
{
    public void Configure(EntityTypeBuilder<UserPostLike> builder)
    {
        builder.ToTable("UserPostLike");

        builder.Ignore(UPL => UPL.Id);

        builder.HasKey(UPL => new { UPL.UserId, UPL.PostId });

        builder.HasOne(UPL => UPL.User)
           .WithMany()
           .HasForeignKey(UPL => UPL.UserId)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired();

        builder.HasOne(UPL => UPL.Post)
            .WithMany()
            .HasForeignKey(UPL => UPL.PostId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
