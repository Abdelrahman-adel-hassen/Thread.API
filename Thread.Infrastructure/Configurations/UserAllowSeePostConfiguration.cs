namespace Thread.Infrastructure.Configurations;
public class UserAllowSeePostConfiguration : IEntityTypeConfiguration<UserAllowSeePost>
{
    public void Configure(EntityTypeBuilder<UserAllowSeePost> builder)
    {
        builder.ToTable("UserAllowSeePost");

        builder.HasKey(UASP => new { UASP.PostId, UASP.UserId });

        builder.HasOne(UASP => UASP.User)
            .WithMany()
            .HasForeignKey(UASP => UASP.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

    }
}
