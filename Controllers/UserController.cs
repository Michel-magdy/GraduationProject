using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;

public class UserController : Controller
{

    IUser userService;

    public UserController(IUser userService)
    {
        this.userService = userService;
    }

    // public IActionResult Index()
    // {
    //     return View(userService.GetUsers());
    // }

    // public IActionResult Add()
    // {
    //     ViewBag.Roles = new SelectList(roleService.GetAll(), "Id", "Name");

    //     return View(new User());
    // }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Add(User user)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         ViewBag.Roles = new SelectList(roleService.GetAll(), "Id", "Name");
    //         return View(user);
    //     }

    //     userService.Add(user);
    //     return RedirectToAction("Index");
    // }


    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Delete(int id)
    // {
    //     userService.Delete(id);
    //     return RedirectToAction(nameof(Index));
    // }

}
