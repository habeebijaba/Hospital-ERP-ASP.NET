using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        var users = dbContext.Users.ToList();
        // ViewBag.users=users;
        ViewBag.UsersJson = JsonConvert.SerializeObject(users);



        return View(users);
    }

    public IActionResult ManageDoctors()
    {
        var doctors = dbContext.Doctors.ToList();
        ViewBag.doctors = doctors;
        ViewBag.DoctorsJson = JsonConvert.SerializeObject(ViewBag.doctors);


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

            dbContext.Add(doctor);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ManageDoctors");
        }
        return RedirectToAction("ManageDoctors");

    }

    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var doctor = await dbContext.Doctors.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }
        dbContext.Doctors.Remove(doctor);
        // Remove the corresponding image from the folder
        if (!string.IsNullOrEmpty(doctor.ImagePath))
        {
            var imagePath = Path.Combine(_environment.WebRootPath, "uploads", doctor.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
        await dbContext.SaveChangesAsync();
        return RedirectToAction("ManageDoctors");
    }

    //edit doctor

    [HttpPost]
    public async Task<IActionResult> EditDoctor(int id, Doctor doctor, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            var existingDoctor = await dbContext.Doctors.FindAsync(id);

            if (existingDoctor == null)
            {
                return NotFound();
            }

            if (file != null && file.Length > 0)
            {
                // Handle image upload
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                // Remove the existing image
                if (!string.IsNullOrEmpty(existingDoctor.ImagePath))
                {
                    var imagePath = Path.Combine(uploads, existingDoctor.ImagePath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Save the new image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                existingDoctor.ImagePath = fileName;
            }

            // Update doctor information
            existingDoctor.Name = doctor.Name;
            existingDoctor.Department = doctor.Department;
            existingDoctor.Charge = doctor.Charge;
            existingDoctor.Availability = doctor.Availability;

            dbContext.Update(existingDoctor);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("ManageDoctors");
        }

        return RedirectToAction("ManageDoctors");
    }

    //edit user


    [HttpPost]
    public async Task<IActionResult> EditUser(int id, User user, IFormFile? file)
    {
        var userJson = JsonConvert.SerializeObject(user);
        // if (ModelState.IsValid)
        // {

        var existingUser = await dbContext.Users.FindAsync(id);

        if (existingUser == null)
        {
            return NotFound();
        }

        if (file != null && file.Length > 0)
        {

            // Handle image upload
            var uploads = Path.Combine(_environment.WebRootPath, "users");

            // Remove the existing image
            if (!string.IsNullOrEmpty(existingUser.Image))
            {
                var imagePath = Path.Combine(uploads, existingUser.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // Save the new image
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            existingUser.Image = fileName;
        }

        // Update user information
        existingUser.Name = user.Name;
        existingUser.Age = user.Age;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.Place = user.Place;

        dbContext.Update(existingUser);
        await dbContext.SaveChangesAsync();

        return RedirectToAction("Users");
        // }
        //     _logger.LogInformation("model is not validddd");


        // return RedirectToAction("Users");
    }


    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await dbContext.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        dbContext.Users.Remove(user);
        // Remove the corresponding image from the folder
        if (!string.IsNullOrEmpty(user.Image))
        {
            var imagePath = Path.Combine(_environment.WebRootPath, "users", user.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
        await dbContext.SaveChangesAsync();
        return RedirectToAction("Users");
    }

}
