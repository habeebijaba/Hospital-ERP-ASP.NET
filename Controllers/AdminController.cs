using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunInVscode.Models;
using Microsoft.AspNetCore.Authorization;

public class AdminController : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AdminController> _logger;

    public AdminController(ApplicationDbContext context, ILogger<AdminController> logger)
    {
        dbContext = context;
        _logger = logger;
    }

    public IActionResult Users()
    {
    var users = dbContext.Users.ToList(); // Retrieve all users

        return View(users); 
    }

    public IActionResult ManageDoctors()
    {
        return View("Doctors"); 
    }

}