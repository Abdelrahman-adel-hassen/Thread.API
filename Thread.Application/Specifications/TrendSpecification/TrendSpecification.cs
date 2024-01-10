namespace Thread.Application.Specifications.TrendConfiguration;
public class TrendSpecification : BaseSpecification<Trend>
{
    private TrendSpecification(Expression<Func<Trend, bool>> criteria) : base(criteria)
    {
    }


    public static TrendSpecification GetTrendSpecification(string tag)
    {
        return new TrendSpecification(trend => trend.Tag == tag);
    }
    public static TrendSpecification GetTrendSpecification(HashSet<string> tags)
    {
        return new TrendSpecification(trend => tags.Contains(trend.Tag));
    }

}
