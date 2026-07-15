using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class TourController : Controller
{
    ITour TourService;
    IBusiness BusinessService;

    public TourController(ITour tourService, IBusiness businessService)
    {
        TourService = tourService;
        BusinessService = businessService;
    }

    public IActionResult Index(int? businessId)
    {
        if (businessId.HasValue)
        {
            var business = BusinessService.GetById(businessId.Value);

            if (business == null)
                return NotFound();

            ViewBag.BusinessId = business.Id;
            ViewBag.BusinessName = business.BusinessName;

            var businessTours = TourService.GetToursByBusiness(business.Id).ToList();
            return View(businessTours);
        }

        ViewBag.BusinessName = "All";
        var tours = TourService.GetTours();
        return View(tours);
    }

    public IActionResult Details(int id)
    {
        var tour = TourService.GetTourFullDetails(id);
        if (tour == null)
            return NotFound();

        return View(tour);
    }

    public IActionResult Create(int businessId)
    {
        var business = BusinessService.GetById(businessId);

        if (business == null)
            return NotFound();

        ViewBag.BusinessId = businessId;
        ViewBag.BusinessName = business.BusinessName;

        return View(new Tour { BusinessId = businessId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Tour tour)
    {
        if (!ModelState.IsValid)
        {
            var business = BusinessService.GetById(tour.BusinessId);
            ViewBag.BusinessId = tour.BusinessId;
            ViewBag.BusinessName = business?.BusinessName;
            return View(tour);
        }

        TourService.Add(tour);
        return RedirectToAction(nameof(Index), new { businessId = tour.BusinessId });
    }

    public IActionResult Edit(int id)
    {
        var tour = TourService.GetTourFullDetails(id);
        if (tour == null)
            return NotFound();

        return View(tour);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Tour tour)
    {
        if (!ModelState.IsValid)
        {
            return View(tour);
        }
        TourService.UpdateTour(tour);
        return RedirectToAction(nameof(Details), new { id = tour.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var tour = TourService.GetById(id);

        if (tour == null)
            return NotFound();

        TourService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddImage(int tourId, string imagePath, string returnAction = nameof(Details))
    {
        TourService.AddImage(tourId, imagePath);
        return RedirectToAction(returnAction, new { id = tourId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateImage(int tourId, int imageId, string imagePath)
    {
        TourService.UpdateImage(tourId, imageId, imagePath);
        return RedirectToAction(nameof(Edit), new { id = tourId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteImage(int tourId, int imageId, string returnAction = nameof(Details))
    {
        TourService.DeleteImage(tourId, imageId);
        return RedirectToAction(returnAction, new { id = tourId });
    }
}