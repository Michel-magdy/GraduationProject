using GraduationProject.Filters;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

[TypeFilter(typeof(AuthFilter))]
public class UserBookingController : Controller
{
    private readonly IHotelBooking _hotelBookingService;
    private readonly IRestaurantBooking _restaurantBookingService;
    private readonly ITourBooking _tourBookingService;
    private readonly ICarRentalBooking _carRentalBookingService;
    
    private readonly IRoom _roomService;
    private readonly ITable _tableService;
    private readonly ITour _tourService;
    private readonly ICarRental _carRentalService;

    public UserBookingController(
        IHotelBooking hotelBookingService,
        IRestaurantBooking restaurantBookingService,
        ITourBooking tourBookingService,
        ICarRentalBooking carRentalBookingService,
        IRoom roomService,
        ITable tableService,
        ITour tourService,
        ICarRental carRentalService)
    {
        _hotelBookingService = hotelBookingService;
        _restaurantBookingService = restaurantBookingService;
        _tourBookingService = tourBookingService;
        _carRentalBookingService = carRentalBookingService;
        _roomService = roomService;
        _tableService = tableService;
        _tourService = tourService;
        _carRentalService = carRentalService;
    }

    public IActionResult MyBookings()
    {
        var userId = HttpContext.Session.GetInt32("UserId").Value;

        ViewBag.HotelBookings = _hotelBookingService.GetBookingsByUser(userId);
        ViewBag.RestaurantBookings = _restaurantBookingService.GetBookingsByUser(userId);
        ViewBag.TourBookings = _tourBookingService.GetBookingsByUser(userId);
        ViewBag.CarRentalBookings = _carRentalBookingService.GetBookingsByUser(userId);

        return View();
    }

    // --- Hotel Room Booking ---
    public IActionResult BookRoom(int roomId)
    {
        var room = _roomService.GetById(roomId);
        if (room == null || room.status != RoomStatus.Available)
            return NotFound();

        ViewBag.RoomNumber = room.RoomNumber;
        ViewBag.Price = room.Price;
        
        return View(new HotelBooking { RoomId = roomId, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookRoom(HotelBooking booking)
    {
        booking.UserId = HttpContext.Session.GetInt32("UserId").Value;
        booking.Status = BookingStatus.Pending;
        
        var room = _roomService.GetById(booking.RoomId);
        if (room != null)
        {
            var days = (booking.CheckOut.Date - booking.CheckIn.Date).TotalDays;
            booking.TotalPrice = room.Price * (decimal)(days > 0 ? days : 1);
        }

        ModelState.Remove("User");
        ModelState.Remove("Room");
        
        if (ModelState.IsValid)
        {
            _hotelBookingService.Add(booking);
            return RedirectToAction(nameof(MyBookings));
        }
        
        ViewBag.RoomNumber = room?.RoomNumber;
        ViewBag.Price = room?.Price;
        return View(booking);
    }

    // --- Restaurant Table Booking ---
    public IActionResult BookTable(int tableId)
    {
        var table = _tableService.GetById(tableId);
        if (table == null || table.Status != TableStatus.Available)
            return NotFound();

        ViewBag.TableNumber = table.TableNumber;
        
        return View(new RestaurantBooking { TableId = tableId, ReservationTime = DateTime.Now.AddHours(2) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookTable(RestaurantBooking booking)
    {
        booking.UserId = HttpContext.Session.GetInt32("UserId").Value;
        booking.Status = BookingStatus.Pending;
        
        ModelState.Remove("User");
        ModelState.Remove("Table");

        if (ModelState.IsValid)
        {
            _restaurantBookingService.Add(booking);
            return RedirectToAction(nameof(MyBookings));
        }

        var table = _tableService.GetById(booking.TableId);
        ViewBag.TableNumber = table?.TableNumber;
        return View(booking);
    }

    // --- Tour Booking ---
    public IActionResult BookTour(int tourId)
    {
        var tour = _tourService.GetById(tourId);
        if (tour == null)
            return NotFound();

        ViewBag.TourName = tour.Name;
        ViewBag.Price = tour.Price;
        
        return View(new TourBooking { TourId = tourId, Persons = 1 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookTour(TourBooking booking)
    {
        booking.UserId = HttpContext.Session.GetInt32("UserId").Value;
        
        var tour = _tourService.GetById(booking.TourId);
        if (tour != null)
        {
            booking.TotalPrice = tour.Price * booking.Persons;
        }

        ModelState.Remove("User");
        ModelState.Remove("Tour");

        if (ModelState.IsValid)
        {
            _tourBookingService.Add(booking);
            return RedirectToAction(nameof(MyBookings));
        }

        ViewBag.TourName = tour?.Name;
        ViewBag.Price = tour?.Price;
        return View(booking);
    }

    // --- Car Rental Booking ---
    public IActionResult BookCarRental(int carId)
    {
        var car = _carRentalService.GetById(carId);
        if (car == null || !car.Available)
            return NotFound();

        ViewBag.CarName = $"{car.Brand} {car.Model}";
        ViewBag.Price = car.PricePerDay;
        
        return View(new CarRentalBooking { CarId = carId, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookCarRental(CarRentalBooking booking)
    {
        booking.UserId = HttpContext.Session.GetInt32("UserId").Value;
        
        var car = _carRentalService.GetById(booking.CarId);
        if (car != null)
        {
            var days = (booking.EndDate.Date - booking.StartDate.Date).TotalDays;
            booking.TotalPrice = car.PricePerDay * (decimal)(days > 0 ? days : 1);
        }

        ModelState.Remove("User");
        ModelState.Remove("Car");

        if (ModelState.IsValid)
        {
            _carRentalBookingService.Add(booking);
            return RedirectToAction(nameof(MyBookings));
        }

        ViewBag.CarName = $"{car?.Brand} {car?.Model}";
        ViewBag.Price = car?.PricePerDay;
        return View(booking);
    }

    [HttpPost]
    public IActionResult CancelBooking(string type, int id)
    {
        switch (type)
        {
            case "Hotel":
                _hotelBookingService.CancelBooking(id);
                break;
            case "Restaurant":
                _restaurantBookingService.CancelBooking(id);
                break;
            case "Tour":
                _tourBookingService.CancelBooking(id);
                break;
            case "CarRental":
                _carRentalBookingService.CancelBooking(id);
                break;
        }
        return RedirectToAction(nameof(MyBookings));
    }
}
