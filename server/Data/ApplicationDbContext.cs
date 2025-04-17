using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    public required DbSet<Contact> Contacts { get; set; }
    public required DbSet<User> Users { get; set; }
    public required DbSet<ContactCategoryEntity> ContactCategoryEntities { get; set; }
    public required DbSet<BusinessCategoryEntity> BusinessCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured) options.UseSqlite($"Data Source={configuration["Sqlite:file"]}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<Contact>().HasOne<ContactCategoryEntity>().WithMany()
            .HasForeignKey(c => c.ContactCategoryId);
        modelBuilder.Entity<Contact>().HasOne<BusinessCategoryEntity>().WithMany()
            .HasForeignKey(c => c.BusinessCategoryId);

        base.OnModelCreating(modelBuilder);
    }
}