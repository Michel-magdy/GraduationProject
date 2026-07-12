using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class BusinessController : Controller
{
    private readonly IBusiness businessService;

    public BusinessController(IBusiness business)
    {
        businessService = business;
    }

    public IActionResult Index()
    {
        return View(businessService.GetBusinessesForIndex());
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

    public IActionResult Edit(int id)
    {
        var business = businessService.GetBusinessDetails(id);

        if (business == null)
        {
            return NotFound();
        }

        return View(business);
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
            existingBusiness.BusinessType = business.BusinessType;
            existingBusiness.Address = business.Address;
            existingBusiness.Status = business.Status;
            existingBusiness.Description = business.Description;

            return View(existingBusiness);
        }

        businessService.UpdateBusiness(business);
        return RedirectToAction(nameof(Details), new { id = business.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        businessService.Delete(id);
        return RedirectToAction(nameof(Index));
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
