public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                }
            );
        });

        var app = builder.Build();
        app.UseRouting();
        app.UseCors("AllowSpecificOrigin");
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        app.Run();
    }
}
