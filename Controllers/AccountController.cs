using GraduationProject.Interfaces;
using GraduationProject.Models;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class AccountController : Controller
{
    private readonly IUser _userService;
    private readonly Context _context;

    public AccountController(IUser userService, Context context)
    {
        _userService = userService;
        _context = context;
    }

    // GET: Account/Login
    public IActionResult Login(string? returnUrl = null)
    {
        // If already logged in, redirect to home
        if (HttpContext.Session.GetInt32("UserId") != null)
            return RedirectToAction("Index", "Home");

        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _userService.Login(model.Email, model.Password);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        // Set session values
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserRole", user.Role?.Name ?? "Customer");
        HttpContext.Session.SetInt32("RoleId", user.RoleId);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    // GET: Account/Register
    public IActionResult Register()
    {
        // If already logged in, redirect to home
        if (HttpContext.Session.GetInt32("UserId") != null)
            return RedirectToAction("Index", "Home");

        return View();
    }

    // POST: Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Check if email already exists
        if (_userService.EmailExists(model.Email))
        {
            ModelState.AddModelError("Email", "This email is already registered.");
            return View(model);
        }

        // Get the "Customer" role (default for self-registration)
        var customerRole = _context.Roles.FirstOrDefault(r => r.Name == "Customer");
        if (customerRole == null)
        {
            // Create the Customer role if it doesn't exist
            customerRole = new Role { Name = "Customer" };
            _context.Roles.Add(customerRole);
            _context.SaveChanges();
        }

        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            PasswordHash = model.Password, // Simple storage for this project
            Phone = model.Phone ?? string.Empty,
            RoleId = customerRole.Id,
            CreatedAt = DateTime.UtcNow
        };

        _userService.Add(user);

        // Auto-login after registration
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.FullName);
        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserRole", "Customer");
        HttpContext.Session.SetInt32("RoleId", customerRole.Id);

        return RedirectToAction("Index", "Home");
    }

    // POST: Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    // GET: Account/AccessDenied
    public IActionResult AccessDenied()
    {
        return View();
    }

    // GET: Account/Profile
    public IActionResult Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction(nameof(Login));

        var user = _userService.GetUserWithDetails(userId.Value);
        if (user == null)
            return NotFound();

        return View(user);
    }
}
