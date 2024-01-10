using Thread.Domain.DTOs;
using Thread.Domain.Enums;

namespace Thread.Infrastructure.Tests.PostServiceTest;
public class PostServiceTestParameters : ParentTestParameters
{
    public bool IsFolowing { get; }
    public List<PostToReturnDto> PostsExcepected { get; } = new List<PostToReturnDto>();
    private PostServiceTestParameters(bool isFolowing, List<PostToReturnDto> postsExcepected)
    {
        IsFolowing = isFolowing;
        PostsExcepected = postsExcepected;
    }

    public static IEnumerable<object[]> GetDifferentUserIdWithPublicOrFollowingPostParameter()
    {

        bool[] isFolowing = { false, true };

        List<PostToReturnDto>[] PostsExcepecteds =
           {
             new List<PostToReturnDto>() {
                  new() {Id="1" ,Privacy= PostPrivacy.Follower,IsLikedByCurrentUser=true,IsRetweetByCurrentUser=true },
                  new() {Id="2", Privacy= PostPrivacy.Public,IsLikedByCurrentUser=true,IsRetweetByCurrentUser=true },
                  new() {Id="4", Privacy= PostPrivacy.Public,IsLikedByCurrentUser=false,IsRetweetByCurrentUser=false },
              },
              new List<PostToReturnDto>() {
                  new() {Id="1" , Privacy= PostPrivacy.Follower,IsLikedByCurrentUser=true,IsRetweetByCurrentUser=true },
                  new() {Id="2", Privacy= PostPrivacy.Public,IsLikedByCurrentUser=true,IsRetweetByCurrentUser=true },
                  new() {Id="3", Privacy= PostPrivacy.Follower,IsLikedByCurrentUser=true,IsRetweetByCurrentUser=true },
              },
           };

        for(int i = 0; i < 2; i++)
            yield return new object[] { new PostServiceTestParameters(isFolowing[i], PostsExcepecteds[i]) };

    }
}