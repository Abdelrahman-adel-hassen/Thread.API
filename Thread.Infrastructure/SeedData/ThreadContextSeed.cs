namespace Thread.Infrastructure.SeedData;
public class ThreadContextSeed
{
    public static async Task SeedAsync(ThreadContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //if(!context.Users.Any())
            //{
            //    string usersData =
            //        File.ReadAllText(path + @"/SeedData/Data/Users.json");

            //    List<AppUser>? users = JsonSerializer.Deserialize<List<AppUser>>(usersData);

            //    foreach(AppUser item in users)
            //    {
            //        _ = context.Users.Add(item);
            //    }

            //    _ = await context.SaveChangesAsync();
            //}

            //if(!context.Posts.Any())
            //{
            //    string postsData =
            //        File.ReadAllText(path + @"/SeedData/Data/Posts.json");

            //    List<Post>? posts = JsonSerializer.Deserialize<List<Post>>(postsData);

            //    foreach(Post item in posts)
            //    {
            //        _ = context.Posts.Add(item);
            //    }

            //    _ = await context.SaveChangesAsync();
            //}

            if(!context.Comments.Any())
            {
                string commentsData =
                    File.ReadAllText(path + @"/SeedData/Data/Comments.json");

                List<Comment>? comments = JsonSerializer.Deserialize<List<Comment>>(commentsData);

                foreach(Comment item in comments)
                {
                    _ = context.Comments.Add(item);
                }

                _ = await context.SaveChangesAsync();
            }


            if(!context.UserAllowSeePosts.Any())
            {
                string userAllowSeePostData =
                    File.ReadAllText(path + @"/SeedData/Data/UserAllowSeePosts.json");

                List<UserAllowSeePost>? userAllowSeePosts = JsonSerializer.Deserialize<List<UserAllowSeePost>>(userAllowSeePostData);

                foreach(UserAllowSeePost item in userAllowSeePosts)
                {
                    _ = context.UserAllowSeePosts.Add(item);
                }

                _ = await context.SaveChangesAsync();
            }

        }
        catch(Exception ex)
        {
            ILogger<ThreadContextSeed> logger = loggerFactory.CreateLogger<ThreadContextSeed>();
            logger.LogError(ex.Message);
        }
    }

}
