namespace Thread.Application.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, int totalCount, int pageNumber,
        int currentPageSize)
    {
        var paginationHeader = new PaginationHeader(pageNumber, currentPageSize, totalCount);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}