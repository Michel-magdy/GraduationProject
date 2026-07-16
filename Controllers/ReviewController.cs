using GraduationProject.Filters;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

[TypeFilter(typeof(AuthFilter))]
public class ReviewController : Controller
{
    private readonly IHotelReview _hotelReviewService;
    private readonly IRestaurantReview _restaurantReviewService;
    private readonly ITourReview _tourReviewService;
    private readonly ICarRentalReview _carRentalReviewService;

    public ReviewController(
        IHotelReview hotelReviewService,
        IRestaurantReview restaurantReviewService,
        ITourReview tourReviewService,
        ICarRentalReview carRentalReviewService)
    {
        _hotelReviewService = hotelReviewService;
        _restaurantReviewService = restaurantReviewService;
        _tourReviewService = tourReviewService;
        _carRentalReviewService = carRentalReviewService;
    }

    public IActionResult MyReviews()
    {
        var userId = HttpContext.Session.GetInt32("UserId").Value;

        ViewBag.HotelReviews = _hotelReviewService.GetAll().Where(r => r.UserId == userId).ToList();
        ViewBag.RestaurantReviews = _restaurantReviewService.GetAll().Where(r => r.UserId == userId).ToList();
        ViewBag.TourReviews = _tourReviewService.GetAll().Where(r => r.UserId == userId).ToList();
        ViewBag.CarRentalReviews = _carRentalReviewService.GetAll().Where(r => r.UserId == userId).ToList();

        return View();
    }

    // --- Add Reviews ---
    public IActionResult AddHotelReview(int hotelId)
    {
        return View("AddReview", new ReviewViewModel { EntityId = hotelId, EntityType = "Hotel" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddHotelReview(ReviewViewModel model)
    {
        if (ModelState.IsValid)
        {
            var review = new HotelReview
            {
                HotelId = model.EntityId,
                Rate = model.Rate,
                Comment = model.Comment,
                UserId = HttpContext.Session.GetInt32("UserId").Value
            };
            _hotelReviewService.Add(review);
            return RedirectToAction("Details", "Hotel", new { id = model.EntityId });
        }
        return View("AddReview", model);
    }

    public IActionResult AddRestaurantReview(int restaurantId)
    {
        return View("AddReview", new ReviewViewModel { EntityId = restaurantId, EntityType = "Restaurant" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddRestaurantReview(ReviewViewModel model)
    {
        if (ModelState.IsValid)
        {
            var review = new RestaurantReview
            {
                RestaurantId = model.EntityId,
                Rate = model.Rate,
                Comment = model.Comment,
                UserId = HttpContext.Session.GetInt32("UserId").Value
            };
            _restaurantReviewService.Add(review);
            return RedirectToAction("Details", "Restaurant", new { id = model.EntityId });
        }
        return View("AddReview", model);
    }

    public IActionResult AddTourReview(int tourId)
    {
        return View("AddReview", new ReviewViewModel { EntityId = tourId, EntityType = "Tour" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddTourReview(ReviewViewModel model)
    {
        if (ModelState.IsValid)
        {
            var review = new TourReview
            {
                TourId = model.EntityId,
                Rate = model.Rate,
                Comment = model.Comment,
                UserId = HttpContext.Session.GetInt32("UserId").Value
            };
            _tourReviewService.Add(review);
            return RedirectToAction("Details", "Tour", new { id = model.EntityId });
        }
        return View("AddReview", model);
    }

    public IActionResult AddCarRentalReview(int carId)
    {
        return View("AddReview", new ReviewViewModel { EntityId = carId, EntityType = "CarRental" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddCarRentalReview(ReviewViewModel model)
    {
        if (ModelState.IsValid)
        {
            var review = new CarRentalReview
            {
                CarRentalId = model.EntityId,
                Rate = model.Rate,
                Comment = model.Comment,
                UserId = HttpContext.Session.GetInt32("UserId").Value
            };
            _carRentalReviewService.Add(review);
            return RedirectToAction("Details", "CarRental", new { id = model.EntityId });
        }
        return View("AddReview", model);
    }
}
