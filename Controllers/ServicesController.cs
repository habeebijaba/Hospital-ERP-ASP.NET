using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunInVscode.Models;

namespace FunInVscode.Controllers;

public class ServicesController : Controller
{
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(ILogger<ServicesController> logger)
    {
        _logger = logger;
    }

     public IActionResult Services(int? id)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
