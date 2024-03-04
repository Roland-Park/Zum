namespace ZumApi.BLL.Services.Interfaces;

public interface ITagSanitizeService
{
    List<string> Sanitize(string tagCsv);
}
