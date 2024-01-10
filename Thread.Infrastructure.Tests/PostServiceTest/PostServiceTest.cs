using Moq;
using Thread.Application.Helpers;
using Thread.Application.Interfaces;
using Thread.Domain.DTOs;
using Thread.Domain.Helpers;
using Thread.Infrastructure.Services;
using Thread.Infrastructure.UnitOfWorkPattern;

namespace Thread.Infrastructure.Tests.PostServiceTest;

public class PostServiceTest
{
    private readonly UnitOfWork _unitOfWork;
    public PostServiceTest()
    {
        _unitOfWork = PostServiceTestParameters.PrepareDataInMemoryDb("Post").Result;
    }




    [Theory]
    [MemberData(nameof(PostServiceTestParameters.GetDifferentUserIdWithPublicOrFollowingPostParameter), MemberType = typeof(PostServiceTestParameters))]
    public async void GetPostsAsync_GettingPostsIsCorrectwithDifferentUserIdWithPublicOrFollowingPost_ReturnExpectedParameter(PostServiceTestParameters parameter)
    {
        // Arrange
        var postService = new PostService(_unitOfWork, It.IsAny<ITrendService>());
        var postParams = new PostParams() { IsFolowing = parameter.IsFolowing };

        // Act
        var result = await postService.GetPostsAsync(postParams);
        // Assert

        Assert.True(result.IsSuccess);
        Assert.IsType<Result<IReadOnlyList<PostToReturnDto>, string>>(result);
        Assert.True(result.Value.Count == parameter.PostsExcepected.Count);
        for(int i = 0; i < result.Value.Count; i++)
        {
            Assert.True(result.Value[i].User != null);
            Assert.True(parameter.PostsExcepected[i].IsRetweetByCurrentUser == result.Value[i].IsRetweetByCurrentUser);
            Assert.True(parameter.PostsExcepected[i].IsLikedByCurrentUser == result.Value[i].IsLikedByCurrentUser);
            Assert.True(parameter.PostsExcepected[i].Privacy == result.Value[i].Privacy);
            Assert.True(parameter.PostsExcepected[i].Id == result.Value[i].Id);
        }


    }
    /// <summary>
    /// public postParams,post.UserId == SharedProperities.UserId restult [post]
    /// public postParams,post.UserId != SharedProperities.UserId restult [post]
    /// following postParams,post.UserId == SharedProperities.UserId restult [post]
    /// following postParam and post.UserId != SharedProperities.UserId and current follows the post  restult [post]
    /// following postParams and post.UserId != SharedProperities.UserId and current not following the post  restult post not in []
    /// </summary>  
    //[Fact]
    //public async void GetPostsAsync_DifferentUserIdWithPublicOrFollowingPost_ReturnExpectedParameter()
    //{
    //    // Arrange
    //    var prepareParameters = PostServiceTestParameters.GetDifferentUserIdWithPublicOrFollowingPostParameter();
    //    foreach(var parameter in prepareParameters)
    //    {
    //        var postParams = new PostParams() { IsFolowing = parameter.IsFolowing };
    //        var unitOfWorkMock = new Mock<IUnitOfWork>();
    //        var postRepositoryMock = new Mock<IGenericRepository<Post>>();
    //        var userFollowRepositoryMock = new Mock<IGenericRepository<UserFollow>>();
    //        var userPostLikeMock = new Mock<IGenericRepository<UserPostLike>>();
    //        var UserPostRetweetMock = new Mock<IGenericRepository<UserPostRetweet>>();


    //        postRepositoryMock.Setup(repo => repo.ListAsync(It.IsAny<PostSpecification>()))
    //                          .ReturnsAsync(parameter.PostsResult);
    //        unitOfWorkMock.Setup(uow => uow.Repository<Post>())
    //                      .Returns(postRepositoryMock.Object);

    //        userFollowRepositoryMock.Setup(repo => repo.IsEntityExistWithSpec(It.IsAny<UserFollowSpecification>())).ReturnsAsync(parameter.IsUserFolloPost);
    //        unitOfWorkMock.Setup(uow => uow.Repository<UserFollow>())
    //                      .Returns(userFollowRepositoryMock.Object);


    //        userPostLikeMock.Setup(repo => repo.IsEntityExistWithSpec(It.IsAny<UserPostLikeSpecification>()))
    //                         .ReturnsAsync(parameter.IsLiked);
    //        unitOfWorkMock.Setup(repo => repo.Repository<UserPostLike>())
    //                       .Returns(userPostLikeMock.Object);

    //        UserPostRetweetMock.Setup(repo => repo.IsEntityExistWithSpec(It.IsAny<ReactionPostRetweetSpecification>())).ReturnsAsync(parameter.IsRetweeted);
    //        unitOfWorkMock.Setup(repo => repo.Repository<UserPostRetweet>())
    //                       .Returns(UserPostRetweetMock.Object);

    //        var serviceUnderTest = new PostService(unitOfWorkMock.Object);

    //        // Act
    //        var result = await serviceUnderTest.GetPostsAsync(postParams);

    //        // Assert

    //        Assert.True(result.IsSuccess);
    //        Assert.IsType<Result<IReadOnlyList<PostToReturnDto>, string>>(result);
    //    }

    //}
}