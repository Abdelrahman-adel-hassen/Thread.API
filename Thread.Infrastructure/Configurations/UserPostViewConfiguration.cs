namespace Thread.Infrastructure.Configuration;
public class UserPostViewConfiguration : IEntityTypeConfiguration<UserPostView>
{
    public void Configure(EntityTypeBuilder<UserPostView> builder)
    {
        builder.ToTable("UserPostView");

        builder.HasKey(UPV => new { UPV.UserId, UPV.PostId });

        builder.HasOne(UPV => UPV.User)
               .WithMany()
               .HasForeignKey(UPV => UPV.UserId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

        builder.HasOne(UPV => UPV.Post)
               .WithMany()
               .HasForeignKey(UPV => UPV.PostId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}
