using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class ImageController : Controller
{
    IImage imageService;

    public ImageController(IImage imageService)
    {
        this.imageService = imageService;
    }

    public IActionResult Index()
    {
        return View(imageService.GetAll());
    }
}