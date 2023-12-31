namespace Thread.Infrastructure.Configuration;
public class UserPostRetweetConfiguration : IEntityTypeConfiguration<UserPostRetweet>
{
    public void Configure(EntityTypeBuilder<UserPostRetweet> builder)
    {
        builder.ToTable("UserPostRetweet");

        builder.HasKey(UPR => new { UPR.UserId, UPR.PostId });

        builder.HasOne(UPR => UPR.User)
             .WithMany()
             .HasForeignKey(UPR => UPR.UserId)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired();

        builder.HasOne(UPR => UPR.Post)
               .WithMany()
               .HasForeignKey(UPR => UPR.PostId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}
