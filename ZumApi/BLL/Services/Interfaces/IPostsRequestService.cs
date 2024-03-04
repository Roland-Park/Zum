using ZumApi.Domain;

namespace ZumApi.BLL.Services.Interfaces;

public interface IPostsRequestService
{
    Task<List<Post>> RequestPosts(List<string> tagList);
}
