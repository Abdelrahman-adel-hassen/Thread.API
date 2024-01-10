using Thread.Domain.DTOs;

namespace Thread.Infrastructure.Tests.CommentServiceTest;
public class CommentServiceTestParamters : ParentTestParameters
{
    public int PostId { get; set; }
    public int? InnertCommentId { get; set; }
    public List<CommentToReturnDto> CommentsExcepected { get; set; }

    public static IEnumerable<object[]> GettingCommentsIsDoneParameters()
    {
        var postIds = new List<int>() { 1, 2, 2, 3, 3, 3 };
        var innertCommentIds = new List<int?>() { null, 1, null, null, 1, 2 };
        List<CommentToReturnDto>[] commentsExcepected ={
            new(){
                new(){Id="1",Content="test1"},
            },
            new(){
                new(){Id="2",Content="test2"},
            },
            new(){
                new(){Id="2",Content="test2"},
            },
            new(){
                new(){Id="3",Content="test3"},
                new(){Id="4",Content="test4"},
                new(){Id="5",Content="test5"},
            },
            new(){
                new(){Id="4",Content="test4"},
            },
             new(){
                new(){Id="5",Content="test5"},
            },
        };
        for(int i = 0; i < postIds.Count; i++)
        {
            yield return new object[] { new CommentServiceTestParamters { PostId = postIds[i], InnertCommentId = innertCommentIds[i], CommentsExcepected = commentsExcepected[i] } };
        }
    }
}
