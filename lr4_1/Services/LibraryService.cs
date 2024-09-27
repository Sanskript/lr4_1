
namespace lr4_1.Services
{
    public static class LibraryService
    {
        public static void MapLibraryRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Library", async context =>
            {
                await context.Response.WriteAsync("Welcome to the Library!");
            });
        }
    }
}
