using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class UserController : Controller
{

    IUser userService;

    public UserController(IUser userService)
    {
        this.userService = userService;
    }

    public IActionResult Index()
    {
        return View(userService.GetUsers());
    }
}