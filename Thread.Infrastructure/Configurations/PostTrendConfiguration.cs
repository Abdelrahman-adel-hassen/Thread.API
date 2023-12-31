namespace Thread.Infrastructure.Configurations;
public class PostTrendConfiguration : IEntityTypeConfiguration<PostTrend>
{
    public void Configure(EntityTypeBuilder<PostTrend> builder)
    {
        builder.ToTable("PostTrend");

        builder.Ignore(pt => pt.Id);

        builder.HasKey(pt => new { pt.PostId, pt.TrendId });

        builder.HasOne(pt => pt.Post)
               .WithMany()
               .HasForeignKey(pt => pt.PostId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.HasOne(pt => pt.Trend)
               .WithMany(t => t.Posts)
               .HasForeignKey(pt => pt.TrendId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

    }
}
