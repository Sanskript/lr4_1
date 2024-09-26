using System.Text.Json;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Library", async context =>
{
    await context.Response.WriteAsync("Welcome to the Library!");
});

app.MapGet("/Library/Books", async context =>
{
    try
    {
        var books = await File.ReadAllTextAsync("books.json");
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(books);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync($"Error reading books data: {ex.Message}");
    }
});

app.MapGet("/Library/Profile/{id?}", async context =>
{
    var id = context.Request.RouteValues["id"]?.ToString() ?? "default";
    try
    {
        var profiles = await File.ReadAllTextAsync("profiles.json");
        var profileData = GetProfileById(profiles, id);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(profileData);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync($"Error reading profiles data: {ex.Message}");
    }
});

app.Run();

string GetProfileById(string profilesJson, string id)
{
    var profiles = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(profilesJson);

    if (profiles != null && profiles.TryGetValue(id, out var profile))
    {
        return JsonSerializer.Serialize(profile);
    }

    return profiles != null && profiles.ContainsKey("0") ? JsonSerializer.Serialize(profiles["0"]) : "No default profile available.";
}
