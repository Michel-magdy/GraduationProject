using GraduationProject.Filters;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;

public class TourBookingController : Controller
{
    private readonly ITourBooking _bookingService;
    private readonly ITour _tourService;
    private readonly IUser _userService;

    public TourBookingController(
        ITourBooking bookingService,
        ITour tourService,
        IUser userService)
    {
        _bookingService = bookingService;
        _tourService = tourService;
        _userService = userService;
    }

    // GET: TourBooking/Index?tourId=1
    public IActionResult Index(int? tourId)
    {
        List<TourBooking> bookings;

        if (tourId.HasValue)
        {
            var tour = _tourService.GetById(tourId.Value);

            if (tour == null)
                return NotFound();

            ViewBag.TourId = tour.Id;
            ViewBag.TourName = tour.Name; // TODO: rename to the real display-name property on your Tour model (e.g. Name)

            bookings = _bookingService
                .GetBookingsByTour(tour.Id)
                .ToList();
        }
        else
        {
            ViewBag.TourName = "All Tours";
            bookings = _bookingService.GetBookings().ToList();
        }

        return View(bookings);
    }

    // GET: TourBooking/Details/5
    public IActionResult Details(int id)
    {
        var booking = _bookingService.GetBookingWithDetails(id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }

    // GET: TourBooking/Create?tourId=1
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Create(int tourId)
    {
        var tour = _tourService.GetById(tourId);

        if (tour == null)
            return NotFound();

        ViewBag.TourId = tour.Id;
        ViewBag.TourName = tour.Name;

        ViewBag.Users = new SelectList(
            _userService.GetAll(),
            "Id",
            "Name");

        return View(new TourBooking
        {
            TourId = tourId
        });
    }

    // POST: TourBooking/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Create(TourBooking booking)
    {
        if (!ModelState.IsValid)
        {
            var tour = _tourService.GetById(booking.TourId);

            ViewBag.TourId = booking.TourId;
            ViewBag.TourName = tour?.Name;
            ViewBag.Users = new SelectList(
                _userService.GetAll(),
                "Id",
                "Name",
                booking.UserId);

            return View(booking);
        }

        _bookingService.Add(booking);

        return RedirectToAction(nameof(Index),
            new { tourId = booking.TourId });
    }

    // GET: TourBooking/Edit/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Edit(int id)
    {
        var booking = _bookingService.GetBookingWithDetails(id);

        if (booking == null)
            return NotFound();

        ViewBag.TourName = booking.Tour?.Name;

        ViewBag.Users = new SelectList(
            _userService.GetAll(),
            "Id",
            "Name",
            booking.UserId);

        return View(booking);
    }

    // POST: TourBooking/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Edit(TourBooking booking)
    {
        if (!ModelState.IsValid)
        {
            var tour = _tourService.GetById(booking.TourId);

            ViewBag.TourName = tour?.Name;

            ViewBag.Users = new SelectList(
                _userService.GetAll(),
                "Id",
                "Name",
                booking.UserId);

            return View(booking);
        }

        _bookingService.Update(booking);

        return RedirectToAction(nameof(Details),
            new { id = booking.Id });
    }

    // GET: TourBooking/Delete/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Delete(int id)
    {
        var booking = _bookingService.GetBookingWithDetails(id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }

    // POST: TourBooking/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult DeleteConfirmed(int id)
    {
        var booking = _bookingService.GetById(id);

        if (booking == null)
            return NotFound();

        int tourId = booking.TourId;

        _bookingService.Delete(id);

        return RedirectToAction(nameof(Index),
            new { tourId });
    }
}