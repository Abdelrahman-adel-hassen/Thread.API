using Thread.Application.Helpers;
using Thread.Domain.DTOs;
using Thread.Domain.Helpers;
using Thread.Infrastructure.Services;
using Thread.Infrastructure.UnitOfWorkPattern;

namespace Thread.Infrastructure.Tests.CommentServiceTest;
public class CommentServiceTest
{
    private readonly UnitOfWork _unitOfWork;

    public CommentServiceTest()
    {
        _unitOfWork = CommentServiceTestParamters.PrepareDataInMemoryDb("Comment").Result;
    }

    [Theory]
    [MemberData(nameof(CommentServiceTestParamters.GettingCommentsIsDoneParameters), MemberType = typeof(CommentServiceTestParamters))]
    public async void GetAllCommentsAsync_GettingCommentsIsDone_ReturnExpectedParameter(CommentServiceTestParamters parameter)
    {
        //Arrange
        var commentService = new CommentService(_unitOfWork);

        //Act
        var commentParams = new CommentParams() { PostId = parameter.PostId, InnertCommentId = parameter.InnertCommentId };
        var result = await commentService.GetAllCommentsAsync(commentParams);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<Result<IReadOnlyList<CommentToReturnDto>, string>>(result);
        Assert.True(result.Value.Count == parameter.CommentsExcepected.Count);
        for(int i = 0; i < result.Value.Count; i++)
        {
            Assert.True(result.Value[i].User != null);
            Assert.True(result.Value[i].Id == parameter.CommentsExcepected[i].Id);
            Assert.True(result.Value[i].Content == parameter.CommentsExcepected[i].Content);
            Assert.True(result.Value[i].NumberOfInnerComments == parameter.CommentsExcepected[i].NumberOfInnerComments);
        }
    }
}
