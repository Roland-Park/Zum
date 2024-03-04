using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ZumApi.BLL.Exceptions;
using ZumApi.BLL.Services.Interfaces;
using ZumApi.Domain;

namespace ZumApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostsRequestService postsRequestService;
    private readonly ITagSanitizeService tagsSanitizer;
    private readonly IFilterPostsService filterPostsService;
    private readonly ILogger<PostController> logger;

    public PostController(IPostsRequestService postsRequestService, ITagSanitizeService tagsSanitizer, IFilterPostsService sortPostsService, ILogger<PostController> logger)
    {
        this.postsRequestService = postsRequestService;
        this.tagsSanitizer = tagsSanitizer;
        this.filterPostsService = sortPostsService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetPosts([FromQuery] string tags, [FromQuery] string? sortBy, [FromQuery] string? direction)
    {
        List<Post> result = [];

        try
        {
            List<string> tagList = this.tagsSanitizer.Sanitize(tags);
            List<Post> postsResponse = await postsRequestService.RequestPosts(tagList);
            result = filterPostsService.SortPosts(postsResponse, sortBy, direction);

        }
        catch(InvalidTagException ex)
        {
            logger.LogError("invalid tags blah blah etc");
            return BadRequest(ex.Message);
        }
        catch(InvalidSortByException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidSortDirectionException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(result.Distinct());
    }
}
