namespace Thread.Infrastructure.Configurations;
public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.ToTable("UserFollow");

        builder.Ignore(UF => UF.Id);

        builder.HasKey(UF => new { UF.SourceUserId, UF.DestinationUserId });

        builder.HasOne(UF => UF.SourceUser)
            .WithMany()
            .HasForeignKey(UF => UF.SourceUserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(UF => UF.DestinationUser)
            .WithMany()
            .HasForeignKey(UF => UF.DestinationUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

    }
}
