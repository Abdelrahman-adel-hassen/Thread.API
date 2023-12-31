namespace Thread.Infrastructure.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUser");

        builder.Property(u => u.UserName)
            .IsRequired();

        builder.Property(u => u.KnownAs)
        .IsRequired();

        builder.HasMany(u => u.SourceBlockedUsers)
               .WithOne()
               .HasForeignKey(b => b.SourceUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.DestinationBlockedUsers)
              .WithOne()
              .HasForeignKey(b => b.DestinationUserId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Photos)
              .WithOne()
              .HasForeignKey(b => b.UserId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ur => ur.UserRoles)
           .WithOne(u => u.User)
           .HasForeignKey(ur => ur.UserId)
           .IsRequired();

    }
}
