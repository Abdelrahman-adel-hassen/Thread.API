using Microsoft.AspNetCore.DataProtection;

namespace Thread.Application;
public static class ServiceExtensions
{
    public static void AddMappser(this IServiceCollection service)
    {
        var dataProtectionProvider = service.BuildServiceProvider().GetRequiredService<IDataProtectionProvider>();

        var dataProtection = dataProtectionProvider.CreateProtector("SecureCoding");
        TypeAdapterConfig<Post, PostToReturnDto>
               .NewConfig()
               .Map(dest => dest.Photos, src => (src.PostPhotos ?? new List<PostPhoto>()).Select(photo => photo.Url).ToList())
               .Map(dest => dest.Id, src => dataProtection.Protect(src.Id.ToString()));

        TypeAdapterConfig<Comment, CommentToReturnDto>
              .NewConfig()
              .Map(dest => dest.Id, src => dataProtection.Protect(src.Id.ToString()));

        TypeAdapterConfig<PostDto, Post>
              .NewConfig()
              .Map(dest => dest.PostPhotos, src => src.Photos.Select(url => new PostPhoto { Url = url }).ToList());

        TypeAdapterConfig<AppUser, UserDto>
        .NewConfig()
        .Map(dest => dest.Name, src => src.UserName);

        TypeAdapterConfig<AppUser, UserToReturnDto>
            .NewConfig()
            .Map(dest => dest.Name, src => src.UserName);
    }
}
