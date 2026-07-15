using GraduationProject.Filters;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;

public class CarRentalBookingController : Controller
{
    private readonly ICarRentalBooking _bookingService;
    private readonly ICarRental _carRentalService;
    private readonly IUser _userService;

    public CarRentalBookingController(
        ICarRentalBooking bookingService,
        ICarRental carRentalService,
        IUser userService)
    {
        _bookingService = bookingService;
        _carRentalService = carRentalService;
        _userService = userService;
    }

    // GET: CarRentalBooking/Index?carId=1
    public IActionResult Index(int? carId)
    {
        List<CarRentalBooking> bookings;

        if (carId.HasValue)
        {
            var car = _carRentalService.GetById(carId.Value);

            if (car == null)
                return NotFound();

            ViewBag.CarId = car.Id;
            ViewBag.CarName = $"{car.Brand} {car.Model}";

            bookings = _bookingService
                .GetBookingsByCar(car.Id)
                .ToList();
        }
        else
        {
            ViewBag.CarName = "All Cars";
            bookings = _bookingService.GetBookings();
        }

        return View(bookings);
    }

    // GET: CarRentalBooking/Details/5
    public IActionResult Details(int id)
    {
        var booking = _bookingService.GetBookingWithDetails(id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }

    // GET: CarRentalBooking/Create?carId=1
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Create(int carId)
    {
        var car = _carRentalService.GetById(carId);

        if (car == null)
            return NotFound();

        ViewBag.CarId = car.Id;
        ViewBag.CarName = $"{car.Brand} {car.Model}";

        ViewBag.Users = new SelectList(
            _userService.GetAll(),
            "Id",
            "Name");

        return View(new CarRentalBooking
        {
            CarId = carId
        });
    }

    // POST: CarRentalBooking/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Create(CarRentalBooking booking)
    {
        if (!ModelState.IsValid)
        {
            var car = _carRentalService.GetById(booking.CarId);

            ViewBag.CarId = booking.CarId;
            ViewBag.CarName = $"{car?.Brand} {car?.Model}";
            ViewBag.Users = new SelectList(
                _userService.GetAll(),
                "Id",
                "Name",
                booking.UserId);

            return View(booking);
        }

        _bookingService.Add(booking);

        return RedirectToAction(nameof(Index),
            new { carId = booking.CarId });
    }

    // GET: CarRentalBooking/Edit/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Edit(int id)
    {
        var booking = _bookingService.GetBookingWithDetails(id);

        if (booking == null)
            return NotFound();

        ViewBag.CarName =
            $"{booking.Car?.Brand} {booking.Car?.Model}";

        ViewBag.Users = new SelectList(
            _userService.GetAll(),
            "Id",
            "Name",
            booking.UserId);

        return View(booking);
    }

    // POST: CarRentalBooking/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Edit(CarRentalBooking booking)
    {
        if (!ModelState.IsValid)
        {
            var car = _carRentalService.GetById(booking.CarId);

            ViewBag.CarName =
                $"{car?.Brand} {car?.Model}";

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

    // GET: CarRentalBooking/Delete/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult Delete(int id)
    {
        var booking = _bookingService.GetBookingWithDetails(id);

        if (booking == null)
            return NotFound();

        return View(booking);
    }

    // POST: CarRentalBooking/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new[] { "Admin", "Owner" } })]
    public IActionResult DeleteConfirmed(int id)
    {
        var booking = _bookingService.GetById(id);

        if (booking == null)
            return NotFound();

        int carId = booking.CarId;

        _bookingService.Delete(id);

        return RedirectToAction(nameof(Index),
            new { carId });
    }
}