using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunInVscode.Models;

namespace FunInVscode.Controllers;

public class CustomController : Controller
{
    private readonly ILogger<CustomController> _logger;

    public CustomController(ILogger<CustomController> logger)
    {
        _logger = logger;
    }

     public IActionResult MyAction(int? id)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
