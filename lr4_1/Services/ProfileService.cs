using System.Text.Json;

namespace lr4_1.Services
{
    public static class ProfileService
    {
        public static void MapProfileRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Library/Profile/{id?}", async context =>
            {
                var id = context.Request.RouteValues["id"]?.ToString() ?? "default";

                var profiles = await File.ReadAllTextAsync("profiles.json");
                var profileData = GetProfileById(profiles, id);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(profileData);
            });
        }

        private static string GetProfileById(string profilesJson, string id)
        {
            var profiles = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(profilesJson);

            if (profiles != null && profiles.TryGetValue(id, out var profile))
            {
                return JsonSerializer.Serialize(profile);
            }

            return profiles != null && profiles.ContainsKey("0") ? JsonSerializer.Serialize(profiles["0"]) : "No default profile available.";
        }
    }
}
