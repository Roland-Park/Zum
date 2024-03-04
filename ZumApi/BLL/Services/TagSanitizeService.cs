using ZumApi.BLL.Exceptions;
using ZumApi.BLL.Services.Interfaces;

namespace ZumApi.BLL.Services;

public class TagSanitizeService : ITagSanitizeService
{
    public List<string> Sanitize(string tagsCsv)
    {
        if (string.IsNullOrWhiteSpace(tagsCsv)) throw new InvalidTagException("tags parameter is required");

        List<string> tagList = new();
        try
        {
            foreach (var tag in tagsCsv.Split(','))
            {
                 tagList.Add(tag.Trim().ToLower());
            }
        }
        catch (Exception ex)
        {
            throw new InvalidTagException($"tags parameter should be a csv list: {ex.Message}");
        }

        return tagList;
    }
}
