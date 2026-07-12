using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class RoomController : Controller
{
    IRoom roomService;

    public RoomController(IRoom roomService)
    {
        this.roomService = roomService;
    }

    public IActionResult Index()
    {
        return View(roomService.GetRooms());
    }
}