using FunInVscode.Models;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;

    public UserService(ApplicationDbContext context)
    {
        dbContext = context;

    }

    public User GetUserByEmail(string email)
    {

        return dbContext.Users.FirstOrDefault(u => u.Email == email);

    }
}
public interface IUserService
{
    User GetUserByEmail(string email);
}