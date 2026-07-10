using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class BusinessController : Controller
{
    IBusiness business;

    public BusinessController(IBusiness business)
    {
        this.business = business;
    }

    public IActionResult Index()
    {
        return View(business.GetAll());
    }

}