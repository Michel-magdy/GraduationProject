using GraduationProject.Interfaces;
using GraduationProject.Models;
using GraduationProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;

public class BusinessController : Controller
{
    private readonly IBusiness businessService;
    private readonly IUser UserService;

    public BusinessController(IBusiness _business, IUser _userservice)
    {
        businessService = _business;
        UserService = _userservice;
    }

    public IActionResult Index()
    {
        return View(businessService.GetBusinessData());
    }

    public IActionResult Details(int id)
    {
        var business = businessService.GetBusinessDetails(id);

        if (business == null)
        {
            return NotFound();
        }

        return View(business);
    }

    public IActionResult Create()
    {
        ViewBag.Owners = new SelectList(UserService.GetOwners(), "Id", "FullName");
        return View(new Business());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Business business)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Owners = new SelectList(UserService.GetOwners(), "Id", "FullName");
            return View(business);
        }

        businessService.Add(business);
        return RedirectToAction("Index");

    }

    public IActionResult Edit(int id)
    {
        var business = businessService.GetBusinessDetails(id);

        if (business == null)
        {
            return NotFound();
        }

        return View(business);
    }


    public IActionResult Approve(int id)
    {
        var business = businessService.GetById(id);

        business.Status = BusinessStatus.Approved;

        businessService.Update(business);
        return RedirectToAction("Index");
    }

    public IActionResult Reject(int id)
    {
        var business = businessService.GetById(id);

        business.Status = BusinessStatus.Rejected;

        businessService.Update(business);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Business business)
    {
        if (id != business.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            var existingBusiness = businessService.GetBusinessDetails(id);

            if (existingBusiness == null)
            {
                return NotFound();
            }

            existingBusiness.BusinessName = business.BusinessName;
            existingBusiness.Address = business.Address;
            existingBusiness.Status = business.Status;
            existingBusiness.Description = business.Description;

            return View(existingBusiness);
        }

        businessService.Update(business);
        return RedirectToAction("Details", new { id = business.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        businessService.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddImage(int businessId, string imagePath, string returnAction = nameof(Details))
    {
        businessService.AddImage(businessId, imagePath);
        return RedirectToAction(returnAction, new { id = businessId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateImage(int businessId, int imageId, string imagePath)
    {
        businessService.UpdateImage(businessId, imageId, imagePath);
        return RedirectToAction(nameof(Edit), new { id = businessId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteImage(int businessId, int imageId, string returnAction = nameof(Details))
    {
        businessService.DeleteImage(businessId, imageId);
        return RedirectToAction(returnAction, new { id = businessId });
    }
}
