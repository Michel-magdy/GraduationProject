using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class RoleController : Controller
{
    IRole roleService;

    public RoleController(IRole roleService)
    {
        this.roleService = roleService;
    }

    public IActionResult Index()
    {
        return View(roleService.GetRoles());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        roleService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
