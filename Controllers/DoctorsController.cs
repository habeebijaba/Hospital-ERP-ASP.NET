using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunInVscode.Models;

namespace FunInVscode.Controllers;

public class DoctorsController : Controller
{
    private readonly ILogger<DoctorsController> _logger;
    public DoctorsController(ILogger<DoctorsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Doctors(int? id)
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
