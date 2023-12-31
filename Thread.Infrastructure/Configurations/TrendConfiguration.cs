namespace Thread.Infrastructure.Configurations;
public class TrendConfiguration : IEntityTypeConfiguration<Trend>
{
    public void Configure(EntityTypeBuilder<Trend> builder)
    {
        builder.ToTable("Trend");

        builder.HasKey(c => c.Id);
    }
}
