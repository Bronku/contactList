using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public required DbSet<Contact> Contacts { get; set; }

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

        base.OnModelCreating(modelBuilder);
    }
}
