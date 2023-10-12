// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
          new User
    {
        Id = 1,
        Name="Habeeb Ijaba",
        Email = "habeebav841@gmail.com",
        Password = "123",
        Image = "user1.jpg",
        DateOfBirth = new DateTime(1990, 1, 1), 
        Age = 33, 
        Place = "SomePlace" 
    },
    new User
    {
        Id = 2,
        Name="Safeer sfr",
        Email = "safeer@gmail.com",
        Password = "123",
        Image = "user2.jpg", 
        DateOfBirth = new DateTime(1991, 2, 2), 
        Age = 32,
        Place = "AnotherPlace" 
    },
    new User
    {
        Id = 3,
        Name="Saleel Hisan",
        Email = "saleel@gmail.com",
        Password = "123",
        Image = "user3.jpg", 
        DateOfBirth = new DateTime(1992, 3, 3), 
        Age = 31, 
        Place = "YetAnotherPlace" 
    }

        );

        modelBuilder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                Name = "habeebav841",
                Department = "nephrology",
                Charge = 200,
                Availability = "daily",
                ImagePath = ""
            },
            new Doctor
            {
                Id = 2,
                Name = "safeer",
                Department = "orthology",
                Charge = 200,
                Availability = "daily",
                ImagePath = ""
            },
            new Doctor
            {
                Id = 3,
                Name = "saleel",
                Department = "orthology",
                Charge = 200,
                Availability = "daily",
                ImagePath = ""
            }
        );
    }
}
