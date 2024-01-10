using System.Text.RegularExpressions;
using Thread.Application.Specifications.TrendConfiguration;

namespace Thread.Infrastructure.Services;
public class TrendService : ITrendService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrendService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool, string>> AddTrend(string body, int postId)
    {
        try
        {
            if(!string.IsNullOrWhiteSpace(body))
            {

                var tags = GetUniqueHashWords(body);

                var trends = await _unitOfWork.Repository<Trend>().ListAsync(TrendSpecification.GetTrendSpecification(tags));
                foreach(var trend in trends)
                {
                    //todo Refactor use redis
                    trend.NumberOfInnerPosts++;
                    tags.Remove(trend.Tag);
                    var postTrend = new PostTrend() { PostId = postId, TrendId = trend.Id };
                    trend.Posts.Add(postTrend);
                    _unitOfWork.Repository<Trend>().Update(trend);
                }
                foreach(var tag in tags)
                {
                    var trend = new Trend() { NumberOfInnerPosts = 1, Tag = tag };
                    trend.Posts.Add(new PostTrend() { PostId = postId });
                    _unitOfWork.Repository<Trend>().Add(trend);
                }
                await _unitOfWork.CompleteAsync();
            }

        }
        catch(Exception ex)
        {
            return $"something happened-{ex.Message}";
        }

        return true;
    }
    private static HashSet<string> GetUniqueHashWords(string body)
    {
        HashSet<string> uniqueWords = new();

        string pattern = @"#\S*";
        MatchCollection tags = Regex.Matches(body, pattern);

        foreach(Match match in tags.Cast<Match>())
        {
            string word = match.Value.ToLower();
            uniqueWords.Add(word);
        }

        return uniqueWords;
    }


}
