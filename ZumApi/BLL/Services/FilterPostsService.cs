using ZumApi.BLL.Exceptions;
using ZumApi.BLL.Services.Interfaces;
using ZumApi.Domain;

namespace ZumApi.BLL.Services;

public class FilterPostsService : IFilterPostsService
{
    public List<Post> SortPosts(List<Post> posts, string? sortBy, string? direction)
    {
        if (string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrEmpty(direction)) return posts;

        bool isAsc = true;
        switch (direction.Trim().ToLower())
        {
            case "asc":
                break;
            case "desc":
                isAsc = false;
                break;
            default:
                throw new InvalidSortDirectionException("direction parameter is invalid");
        }

        var sanitizedSortBy = sortBy.ToLower().Trim();
        switch (sanitizedSortBy)
        {
            case nameof(Post.id):
                posts = isAsc ? posts.OrderBy(x => x.id).ToList() : posts.OrderByDescending(x => x.id).ToList();
                break;
            case nameof(Post.reads):
                posts = isAsc ? posts.OrderBy(x => x.reads).ToList() : posts.OrderByDescending(x => x.reads).ToList();
                break;
            case nameof(Post.likes):
                posts = isAsc ? posts.OrderBy(x => x.likes).ToList() : posts.OrderByDescending(x => x.likes).ToList();
                break;
            case nameof(Post.popularity):
                posts = isAsc ? posts.OrderBy(x => x.popularity).ToList() : posts.OrderByDescending(x => x.popularity).ToList();
                break;
            default:
                throw new InvalidSortByException("sortBy parameter is invalid");
        }

        return posts;
    }
}
