using ZumApi.Domain;

namespace ZumApi.BLL.Services.Interfaces;

public interface IFilterPostsService
{
    List<Post> SortPosts(List<Post> posts, string? sortBy, string? direction);
}
