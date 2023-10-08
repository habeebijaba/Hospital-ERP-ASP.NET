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
        _logger.LogInformation("User: {userJson}...................", userJson);

        if (ModelState.IsValid)
        {


            // Hash the password before saving it to the database
            // obj.Password = HashPassword(obj.Password);
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

    [HttpPost]
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

    public async Task<IActionResult> Login(User model)
    {
        _logger.LogInformation("User {email} {password} logged in successfully.", model.Email, model.Password);

        var user = dbContext.Users.FirstOrDefault(u => u.Email == model.Email);

        if (user != null && VerifyPassword(model.Password, user.Password))
        {
            // bool isAdmin =true;
            // Create claims for the authenticated user (you can customize this as needed)
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
             new Claim("IsUser", "true")
            // Add other claims as needed, e.g., roles, user ID, etc.
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            // Sign in the user by creating and setting the authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Successful login - redirect to the "Index" action of the "Home" controller
            //  return isAdmin ? RedirectToAction("MyAction", "Custom") : RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Failed login - display an error message
            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }
    }

    // Simulate user authentication (replace with your actual authentication logic)

    private bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

    // Action to display the dashboard (replace with your actual dashboard logic)
    public IActionResult Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        // Sign out the user and delete the authentication cookie
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Redirect to the login page or any other desired page after logout
        return RedirectToAction("Login");
    }

}
