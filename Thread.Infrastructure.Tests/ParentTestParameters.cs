using Thread.Domain.Entities;
using Thread.Domain.Enums;
using Thread.Domain.Helpers;
using Thread.Infrastructure.UnitOfWorkPattern;

namespace Thread.Infrastructure.Tests;
public class ParentTestParameters
{
    private static Dictionary<string, UnitOfWork?> _unitOfWork { get; set; } = new();
    private static readonly object _lockObject = new();
    public async static Task<UnitOfWork> PrepareDataInMemoryDb(string testName)
    {
        lock(_lockObject)
        {
            if(_unitOfWork.ContainsKey(testName))
                return _unitOfWork[testName]!;

            var dbContext = new InMemoryDbContext();
            _unitOfWork[testName] = new UnitOfWork(dbContext);

            List<Post> posts =
             new()
             {
              new(){Privacy = PostPrivacy.Follower, UserId = 1 },
              new(){Privacy = PostPrivacy.Public, UserId = 1 },
              new(){Privacy = PostPrivacy.Follower, UserId = 2 },
              new(){Privacy = PostPrivacy.Public, UserId = 2 },
              new(){Privacy = PostPrivacy.Follower, UserId = 3 },
             };
            List<UserPostLike> userPostsLike =
             new()
             {
              new(){PostId=1, UserId = 1 },
              new(){PostId=1, UserId = 2 },
              new(){PostId=2, UserId = 1 },
              new(){PostId=2, UserId = 2 },
              new(){PostId=3, UserId= 1 },
              new(){PostId=3, UserId= 2 },
              new(){PostId=4, UserId = 2 }
             };
            List<UserPostRetweet> userPostsRetweet =
             new()
             {
              new(){PostId=1, UserId = 1 },
              new(){PostId=1, UserId = 2 },
              new(){PostId=2, UserId = 1 },
              new(){PostId=2, UserId = 2 },
              new(){PostId=3, UserId= 1 },
              new(){PostId=3, UserId= 2 },
              new(){PostId=4, UserId = 2 }
             };
            List<UserFollow> usersFollow =
            new()
            {
              new(){SourceUserId=2, DestinationUserId = 1 },
              new(){SourceUserId=3, DestinationUserId = 2 },
              new(){SourceUserId=1, DestinationUserId = 2 },
              new(){SourceUserId=3, DestinationUserId = 1 },
            };
            List<Trend> trends =
            new()
             {
              new(){Tag="#test@7",NumberOfInnerPosts=1},
              new(){Tag="#test_6",NumberOfInnerPosts=1},
             };
            List<AppUser> users =
            new()
             {
                new(){KnownAs="Bido",UserName="Abdeou",City="abc",Country="Cairo"},
                new(){KnownAs="Bido",UserName="adel",City="abc",Country="Cairo"},
                new(){KnownAs="Bido",UserName="adel",City="abc",Country="Cairo"},
             };
            List<Comment> comments =
            new()
            {
                new(){PostId=1,Content="test1" ,UserId=1},
                new(){PostId=2,Content="test2",InnerCommentId=1,UserId=1},
                new(){PostId=3,Content="test3",UserId=1},
                new(){PostId=3,Content="test4",InnerCommentId=1,UserId=1},
                new(){PostId=3,Content="test5",InnerCommentId=2,UserId=1},
            };
            SharedProperities.UserId = 1;
            dbContext.AddRange(users);
            //dbContext.SaveChangesAsync();
            _unitOfWork[testName]!.Repository<UserFollow>()!.AddRangeAsync(usersFollow).Wait();
            _unitOfWork[testName]!.Repository<Post>()!.AddRangeAsync(posts).Wait();
            _unitOfWork[testName]!.Repository<UserPostLike>()!.AddRangeAsync(userPostsLike).Wait();
            _unitOfWork[testName]!.Repository<UserPostRetweet>()!.AddRangeAsync(userPostsRetweet).Wait();
            _unitOfWork[testName]!.Repository<Trend>()!.AddRangeAsync(trends).Wait();
            _unitOfWork[testName]!.Repository<Comment>()!.AddRangeAsync(comments).Wait();
            _unitOfWork[testName]!.CompleteAsync().Wait();
        }
        return _unitOfWork[testName]!;
    }

}
