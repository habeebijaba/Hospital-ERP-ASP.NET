using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;  // For List<T>
using System.Security.Claims;      // For Claim and ClaimTypes
using System.Threading.Tasks;     // For Task


public class AccountController : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
    {
        dbContext = context;
        _logger = logger;
    }

    //  private readonly ApplicationDbContext dbContext;

    // public AccountController(ApplicationDbContext context)
    // {
    //     dbContext = context;
    // }

    // private readonly ILogger<AccountController> _logger;

    // public AccountController(ILogger<AccountController> logger)
    // {
    //     _logger = logger;
    // }

    // Action to display the Signup form
    public IActionResult Signup()
    {
        return View();
    }

    // Action to process the signup request

    [HttpPost]
    public IActionResult Signup(User obj)
    {
        var userJson = JsonConvert.SerializeObject(obj);
        _logger.LogInformation("User: {userJson}", userJson);

        if (ModelState.IsValid)
        {
            obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            dbContext.Users.Add(obj);
            dbContext.SaveChanges();
            return RedirectToAction("Login");
        }
        return View();
    }

    // Action to display the login form
    public IActionResult Login()
    {
        return View();
    }

    // Action to process the login request

    // public IActionResult Login(User model)
    // {
    //     _logger.LogInformation("User {email} {password}logged in successfully.", model.Email, model.Password);

    //     // Check if email and password match (replace with your authentication logic)
    //     // if (IsValidUser(model.Email, model.Password))
    //     // {
    //     //     var users = dbContext.Users.ToList(); // Retrieve all users
    //     //     _logger.LogInformation("Users: {@users[0]}.............................................", users);
    //     //     foreach (var user in users)
    //     //     {
    //     //         var userJson = JsonConvert.SerializeObject(user);
    //     //         _logger.LogInformation("User: {userJson}", userJson);
    //     //     }
    //     //     // Successful login - redirect to the next page
    //     //     return RedirectToAction("Dashboard");
    //     // }
    //     var user = dbContext.Users.FirstOrDefault(u => u.Email == model.Email);

    //     if (user != null && VerifyPassword(model.Password, user.Password))
    //     {
    //         // Successful login - redirect to the next page
    //         return RedirectToAction("Index", "Home");
    //     }
    //     else
    //     {
    //         // Failed login - display an error message
    //         // ViewData["ErrorMessage"] = "Invalid email or password.";
    //          ModelState.AddModelError("", "Invalid email or password.");

    //         return View();
    //     }
    // }

    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {
        _logger.LogInformation("User {email} {password} logged in successfully.", model.Email, model.Password);

        if (IsAnAdmin(model.Email, model.Password))
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign in the admin user by creating and setting the authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Dashboard");
        }
        else
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                var claims = new List<Claim>
                 {
                  new Claim(ClaimTypes.Name, user.Email),
                 new Claim(ClaimTypes.Role, "User")
                  };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                // Sign in the user by creating and setting the authentication cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Failed login, display error message
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }
        }
    }


    private bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

    // Action to display the dashboard 
    public IActionResult Dashboard()
    {
        return View();
    }

    private bool IsAnAdmin(string email, string password)
    {
        if (email == "admin@gmail.com" && password == "Admin@123")
        {
            return true;
        }
        return false;
    }

    public async Task<IActionResult> Logout()
    {
        // Sign out the user and delete the authentication cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

}
