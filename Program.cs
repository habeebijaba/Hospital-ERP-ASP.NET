using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//dbconnection establishhhhh

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

//registering the interface
builder.Services.AddScoped<IUserService, UserService>();


// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Specify the login page URL
        options.AccessDeniedPath = "/"; // Specify the access denied page URL
    });

//for specifying authorization
//    builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy => policy.RequireClaim("IsAdmin", "true"));
//     options.AddPolicy("User", policy => policy.RequireClaim("IsUser", "true"));
// });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User")); // Use ClaimTypes.Role
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//     endpoints.MapControllerRoute(
//         name: "dashboard",
//         pattern: "Account/Dashboard",
//         defaults: new { controller = "Account", action = "Dashboard" });

//     // Additional routes can be defined here
// });

app.MapControllerRoute(
    name: "signup",
    pattern: "/signup",
    defaults: new { controller = "Account", action = "Signup" });

app.MapControllerRoute(
    name: "login",
    pattern: "/Login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "dashboard",
    pattern: "/Dashboard",
    defaults: new { controller = "Account", action = "Dashboard" });

app.MapControllerRoute(
    name: "services",
    // pattern: "services/{action}/{id?}",
    pattern: "/Services",
    defaults: new { controller = "Services", action = "Services" });

app.MapControllerRoute(
    name: "doctors",
    pattern: "/Doctors",
    defaults: new { controller = "Doctors", action = "Doctors" });

app.MapControllerRoute(
    name: "users",
    pattern: "Dashboard/users",
    defaults: new { controller = "Admin", action = "Users" });

app.MapControllerRoute(
    name: "ManageDoctors",
    pattern: "Dashboard/managedoctors",
    defaults: new { controller = "Admin", action = "Doctors" });

    
//  app.MapControllerRoute(
//     name: "deleteDoctor",
//     pattern: "Admin/ManageDoctors/{id:int}",
//     defaults: new { controller = "Admin", action = "DeleteDoctor" });

// app.MapControllerRoute(
//     name: "deleteUser",
//     pattern: "Admin/ManageUsers/{id:int}",
//     defaults: new { controller = "Admin", action = "DeleteUser" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
