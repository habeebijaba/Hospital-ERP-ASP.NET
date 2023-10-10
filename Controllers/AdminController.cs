using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FunInVscode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

public class AdminController : Controller
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<AdminController> _logger;
    private readonly IWebHostEnvironment _environment; // Inject IWebHostEnvironment for file handling

    public AdminController(ApplicationDbContext context, ILogger<AdminController> logger, IWebHostEnvironment environment)
    {
        dbContext = context;
        _logger = logger;
        _environment = environment; // Injected IWebHostEnvironment
    }

    public IActionResult Users()
    {
        var users = dbContext.Users.ToList(); // Retrieve all users
        return View(users);
    }

    public IActionResult ManageDoctors()
    {
        var doctors = dbContext.Doctors.ToList(); // Retrieve all doctors

        return View("Doctors");
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManageDoctors(Doctor doctor, IFormFile? file) // Use IFormFile to handle file upload
    {
        if (ModelState.IsValid)
        {
            // Handle image upload   
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                doctor.ImagePath = fileName;
            }

            // Save doctor to the database
            dbContext.Add(doctor);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ManageDoctors");
        }
        return RedirectToAction("ManageDoctors");

    }
}
