
namespace lr4_1.Services
{
    public static class BooksService
    {
        public static void MapBooksRoutes(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/Library/Books", async context =>
            {
                var books = await File.ReadAllTextAsync("books.json");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(books);
            });
        }
    }
}
