var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet(
    "/",
    () =>
    {
        return "Hello root\n";
    }
);

app.Run();
