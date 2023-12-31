namespace Thread.Application.Helpers;
public class PaginationHeader
{
    public PaginationHeader(int pageNumber, int currentPageSize, int totalCount)
    {
        PageNumber = pageNumber;
        CurrentPageSize = currentPageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)currentPageSize);
    }

    public int PageNumber { get; set; }
    public int CurrentPageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
