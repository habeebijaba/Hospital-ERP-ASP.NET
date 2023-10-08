// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Email = "habeebav841@gmail.com", Password = "123" },
            new User { Id = 2, Email = "safeer@gmail.com", Password = "123" },
            new User { Id = 3, Email = "saleel@gmail.com", Password = "123" }

        );
    }
}
