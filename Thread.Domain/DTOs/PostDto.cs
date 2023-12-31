namespace Thread.Domain.DTOs;
public class PostDto
{
    public string Body { get; set; } = string.Empty;
    public PostPrivacy Privacy { get; set; }
    public IReadOnlyList<string> Photos { get; set; } = new List<string>();

}
