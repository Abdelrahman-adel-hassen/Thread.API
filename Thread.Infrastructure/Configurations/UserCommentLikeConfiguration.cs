namespace Thread.Infrastructure.Configuration;
public class UserCommentLikeConfiguration : IEntityTypeConfiguration<UserCommentLike>
{
    public void Configure(EntityTypeBuilder<UserCommentLike> builder)
    {
        builder.ToTable("UserCommentLike");

        builder.HasKey(UCL => new { UCL.CommentId, UCL.UserId });

        builder.HasOne(UCL => UCL.User)
            .WithMany()
            .HasForeignKey(UCL => UCL.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(UCL => UCL.Comment)
            .WithMany()
            .HasForeignKey(UCL => UCL.CommentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

    }
}
