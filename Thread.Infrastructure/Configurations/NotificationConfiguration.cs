using Thread.Domain.Enums;

namespace Thread.Infrastructure.Configuration;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notification");

        builder.HasKey(c => c.Id);

        builder.HasOne(m => m.UserSender)
                 .WithMany()
                 .HasForeignKey(m => m.SenderId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

        builder.HasOne(m => m.UserRecipient)
               .WithMany()
               .HasForeignKey(m => m.RecipientId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.Property(m => m.NotificationType)
               .HasConversion(
                        o => o.ToString(),
                        o => (NotificationType)Enum.Parse(typeof(NotificationType), o)
                    );

        builder.Property(c => c.NotificationType)
            .IsRequired();

    }
}
