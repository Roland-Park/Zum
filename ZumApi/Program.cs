using Microsoft.Extensions.DependencyInjection;
using ZumApi.BLL.Services;
using ZumApi.BLL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ITagSanitizeService, TagSanitizeService>();
builder.Services.AddScoped<IFilterPostsService, FilterPostsService>();
builder.Services.AddScoped<IPostsRequestService, PostsRequestService>();
builder.Services.AddHttpClient<IPostsRequestService, PostsRequestService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ExternalPostsApi")); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
