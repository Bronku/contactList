var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new UserStore());
builder.Services.AddControllers();
var app = builder.Build();
app.UseRouting();
app.MapControllers();

app.Run();
