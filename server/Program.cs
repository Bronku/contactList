using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using server.Data;
using server.Models;

namespace server;

public static class Program
{
    // quite a long function, but most of it is configuring the app
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // key for generating JWT
        builder.Services.AddSingleton(credentials);

        // hasher object,
        // could be created in the AuthController, or somewhere else, but it seems the most reasonable to use it here
        // it allows for creating new password hashes outside the AuthController object, 
        // which in turn allows for adding initial user in Main()
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        builder.Services.AddDbContext<ApplicationDbContext>();

        builder.Services.AddControllers();

        // for development purpose
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // JWT tokens for simple authentication,
        // should consider validating issuer, and audience in the future
        // right now it's not so crucial since there is only one possible issuer, and one audience
        builder
            .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = securityKey
                };
            });
        builder.Services.AddAuthorization();

        // for development to fix issues with cors, right now the frontend port is hardcoded
        // not a perfect solution
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });

        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // ensure database exists, and has a valid user saved before starting the application
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
            db.Database.EnsureCreated();

            if (!db.Users.Any())
            {
                var user = new User { Username = "admin" };
                user.PasswordHash = hasher.HashPassword(user, "secret");
                db.Users.Add(user);
                db.SaveChanges();
            }

            if (!db.BusinessCategories.Any())
            {
                var categories = new List<ContactCategoryEntity>
                {
                    new() { Name = "Business" },
                    new() { Name = "Personal" },
                    new() { Name = "Other" }
                };
                db.ContactCategoryEntities.AddRange(categories);
                db.SaveChanges();
            }

            if (!db.BusinessCategories.Any())
            {
                var categories = new List<BusinessCategoryEntity>
                {
                    new() { Name = "Boss" },
                    new() { Name = "Client" },
                    new() { Name = "Manager" },
                    new() { Name = "Developer" },
                    new() { Name = "Hr" }
                };
                db.BusinessCategories.AddRange(categories);
                db.SaveChanges();
            }
        }

        app.Run();
    }
}