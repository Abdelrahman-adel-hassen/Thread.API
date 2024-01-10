namespace Thread.API.Controllers;
public class TrendController : BaseApiController
{
    private readonly ILogger<TrendController> _logger;
    private readonly IUnitOfWork _iUnitOfWork;
    public TrendController(ILogger<TrendController> logger, IUnitOfWork iUnitOfWork)
    {
        _logger = logger;
        _iUnitOfWork = iUnitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrendDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IReadOnlyList<string>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllTrend(TrendParams trendParams)
    {
        var Trends = await _iUnitOfWork.Repository<Trend>().ListAllAsync();

        Response.AddPaginationHeader(Trends.Count, trendParams.PageNumber, trendParams.CurrentPageSize);

        return Ok(Trends.Adapt<TrendDto>());
    }
}
