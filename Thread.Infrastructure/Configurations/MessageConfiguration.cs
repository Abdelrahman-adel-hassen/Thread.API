namespace Thread.Infrastructure.Configuration;
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Message");

        builder.HasKey(c => c.Id);

        builder.HasOne(m => m.UserSender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        builder.HasOne(m => m.UserRecipient)
               .WithMany(u => u.MessagesReceived)
               .HasForeignKey(m => m.RecipientId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
    }
}
