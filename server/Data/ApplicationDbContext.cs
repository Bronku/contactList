using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class ApplicationDbContext : DbContext
{
    public required DbSet<Contact> Contacts { get; set; }
    public required DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlite($"Data Source=database.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().Property(u => u.Category).HasConversion<string>();
        modelBuilder.Entity<Contact>().Property(u => u.BusinessCategory).HasConversion<string>();
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}