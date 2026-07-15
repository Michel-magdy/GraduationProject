using GraduationProject.Filters;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class RoomController : Controller
{
    private readonly IRoom _roomService;
    private readonly IHotel _hotelService;

    public RoomController(IRoom roomService, IHotel hotelService)
    {
        _roomService = roomService;
        _hotelService = hotelService;
    }

    // GET: Room/Index?hotelId=1
    public IActionResult Index(int? hotelId)
    {
        List<Room> rooms;

        if (hotelId.HasValue)
        {
            var hotel = _hotelService.GetById(hotelId.Value);
            if (hotel == null)
                return NotFound();

            ViewBag.HotelId = hotel.Id;
            ViewBag.HotelName = hotel.Name;
            rooms = _roomService.GetRoomsByHotel(hotel.Id).ToList();
        }
        else
        {
            ViewBag.HotelName = "All Hotels";
            rooms = _roomService.GetRooms();
        }

        return View(rooms);
    }

    // GET: Room/Details/5
    public IActionResult Details(int id)
    {
        var room = _roomService.GetRoomWithDetails(id);
        if (room == null)
            return NotFound();

        return View(room);
    }

    // GET: Room/Create?hotelId=1
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Create(int hotelId)
    {
        var hotel = _hotelService.GetById(hotelId);
        if (hotel == null)
            return NotFound();

        ViewBag.HotelId = hotel.Id;
        ViewBag.HotelName = hotel.Name;

        return View(new Room { HotelId = hotel.Id, status = RoomStatus.Available });
    }

    // POST: Room/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Create(Room room)
    {
        if (!ModelState.IsValid)
        {
            var hotel = _hotelService.GetById(room.HotelId);
            ViewBag.HotelId = room.HotelId;
            ViewBag.HotelName = hotel?.Name;
            return View(room);
        }

        _roomService.Add(room);
        return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
    }

    // GET: Room/Edit/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Edit(int id)
    {
        var room = _roomService.GetRoomWithDetails(id);
        if (room == null)
            return NotFound();

        ViewBag.HotelName = room.Hotel?.Name;
        return View(room);
    }

    // POST: Room/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Edit(Room room)
    {
        if (!ModelState.IsValid)
        {
            var hotel = _hotelService.GetById(room.HotelId);
            ViewBag.HotelName = hotel?.Name;
            return View(room);
        }

        _roomService.Update(room);
        return RedirectToAction(nameof(Details), new { id = room.Id });
    }

    // GET: Room/Delete/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Delete(int id)
    {
        var room = _roomService.GetRoomWithDetails(id);
        if (room == null)
            return NotFound();

        return View(room);
    }

    // POST: Room/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult DeleteConfirmed(int id)
    {
        var room = _roomService.GetRoomWithDetails(id);
        if (room == null)
            return NotFound();

        if (room.HotelBookings.Any())
        {
            TempData["ErrorMessage"] = "Cannot delete this room because it has existing hotel bookings.";
            return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
        }

        var hotelId = room.HotelId;
        _roomService.Delete(id);
        TempData["SuccessMessage"] = "Room deleted successfully.";
        return RedirectToAction(nameof(Index), new { hotelId });
    }

    // POST: Room/ToggleStatus/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult ToggleStatus(int id)
    {
        var room = _roomService.GetById(id);
        if (room == null)
            return NotFound();

        room.status = room.status switch
        {
            RoomStatus.Available => RoomStatus.Occupied,
            RoomStatus.Occupied => RoomStatus.Available,
            RoomStatus.Maintenance => RoomStatus.Available,
            RoomStatus.Inactive => RoomStatus.Available,
            _ => RoomStatus.Available
        };

        _roomService.Update(room);
        return RedirectToAction(nameof(Index), new { hotelId = room.HotelId });
    }
}
