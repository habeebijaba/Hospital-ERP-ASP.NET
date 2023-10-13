using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunInVscode.Models;

namespace FunInVscode.Controllers;

public class DoctorsController : Controller
{
     private readonly ApplicationDbContext dbContext;

    public DoctorsController(ApplicationDbContext context)
    {
        dbContext = context;
    }

    public IActionResult Doctors(int? id)
    {
        var doctors = dbContext.Doctors.ToList(); // Retrieve all doctors
        ViewBag.doctors=doctors;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
