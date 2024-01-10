using Thread.Domain.Entities;
using Thread.Domain.Helpers;
using Thread.Infrastructure.Services;
using Thread.Infrastructure.UnitOfWorkPattern;
namespace Thread.Infrastructure.Tests.TrendServiceTest;
public class TrendServiceTest
{
    private readonly UnitOfWork _unitOfWork;

    public TrendServiceTest()
    {
        _unitOfWork = TrendServiceTestParameters.PrepareDataInMemoryDb("Trend").Result;
    }
    [Theory]
    [MemberData(nameof(TrendServiceTestParameters.GetAddingTrendsIsDoneParameters), MemberType = typeof(TrendServiceTestParameters))]
    public async void AddTrend_AddingTrendsIsDone_ReturnExpectedParameter(TrendServiceTestParameters parameter)
    {
        //Arrange
        var parameters = TrendServiceTestParameters.GetAddingTrendsIsDoneParameters();
        var trendService = new TrendService(_unitOfWork);
        //Act

        var result = await trendService.AddTrend(parameter.Body, 1);
        var trends = await _unitOfWork.Repository<Trend>()!.ListAllAsync();
        trends = trends.OrderBy(t => t.Id).ToList();
        //Assert
        Assert.IsType<Result<bool, string>>(result);
        Assert.True(result.IsSuccess);
        Assert.True(trends.Count == parameter.TrendExpected.Count);

        parameter.TrendExpected = parameter.TrendExpected.OrderBy(t => t.Id).ToList();

        for(int i = 0; i < parameter.TrendExpected.Count; i++)
        {
            Assert.True(trends[i].Id == parameter.TrendExpected[i].Id);
            Assert.True(trends[i].Tag == parameter.TrendExpected[i].Tag);
            Assert.True(trends[i].NumberOfInnerPosts == parameter.TrendExpected[i].NumberOfInnerPosts);
        }



    }
}
