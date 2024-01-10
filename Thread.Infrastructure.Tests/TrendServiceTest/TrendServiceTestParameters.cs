using Thread.Domain.Entities;

namespace Thread.Infrastructure.Tests.TrendServiceTest;
public class TrendServiceTestParameters : ParentTestParameters
{
    public string Body { get; set; }
    public List<Trend> TrendExpected { get; set; }

    public static IEnumerable<object[]> GetAddingTrendsIsDoneParameters()
    {
        List<Trend> trendExpected =
        new(){
            new (){Id=1,Tag="#test@7",NumberOfInnerPosts=2},
            new (){Id=2,Tag="#test_6",NumberOfInnerPosts=2},
            new (){Id=3,Tag="#test1",NumberOfInnerPosts=1},
            new (){Id=4,Tag="#test3",NumberOfInnerPosts=1},
            new (){Id=5,Tag="#test4",NumberOfInnerPosts=1},
        };
        yield return new object[] { new TrendServiceTestParameters() { Body = "#test1 #Test1 #test3 #Test4 #test_6 #test@7 #TeSt_6", TrendExpected = trendExpected } };
    }
}
