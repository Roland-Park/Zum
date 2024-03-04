using System.Text.Json;
using ZumApi.BLL.Dtos;
using ZumApi.BLL.Services.Interfaces;
using ZumApi.Domain;

namespace ZumApi.BLL.Services;

public class PostsRequestService : IPostsRequestService
{
    private readonly HttpClient client;

    public PostsRequestService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Post>> RequestPosts(List<string> tagsList)
    {
        List<Post> result = [];
        foreach(var tag in tagsList)
        {
            result.AddRange(await RequestPostForTag(tag));
        }

        return result.DistinctBy(x => x.id).ToList(); ;
    }

    private async Task<List<Post>> RequestPostForTag(string tag)
    {
        using HttpResponseMessage response = await client.GetAsync($"?tag={tag}");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        var postContainer = JsonSerializer.Deserialize<PostsContainer>(responseBody)!;

        return postContainer.posts;
    }
}
