using lr4_1.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapLibraryRoutes();
app.MapBooksRoutes();
app.MapProfileRoutes();

app.Run();
