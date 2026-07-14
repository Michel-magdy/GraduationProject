using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;


public class HotelController : Controller
{
    IHotel HotelService;
    IBusiness BusinessService;

    IRoom RoomService;

    public HotelController(IHotel hotelService, IBusiness businessService, IRoom roomService)
    {
        HotelService = hotelService;
        BusinessService = businessService;
        RoomService = roomService;
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

            var businessHotels = HotelService.GetHotelsByBusiness(business.Id).ToList();
            return View(businessHotels);
        }

        ViewBag.BusinessName = "All";
        var hotels = HotelService.GetHotels();
        return View(hotels);
    }

    public IActionResult Details(int id)
    {
        var hotel = HotelService.GetHotelFullDetails(id);

        if (hotel == null)
            return NotFound();

        return View(hotel);
    }

    public IActionResult Create(int businessId)
    {
        var business = BusinessService.GetById(businessId);

        if (business == null)
            return NotFound();

        ViewBag.BusinessId = businessId;
        ViewBag.BusinessName = business.BusinessName;

        return View(new Hotel { BusinessId = businessId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Hotel hotel, List<string> ImageUrls)
    {
        if (!ModelState.IsValid)
        {
            var business = BusinessService.GetById(hotel.BusinessId);
            ViewBag.BusinessId = hotel.BusinessId;
            ViewBag.BusinessName = business?.BusinessName;
            return View(hotel);
        }

        HotelService.Add(hotel);

        if (ImageUrls != null)
        {
            foreach (var url in ImageUrls)
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    HotelService.AddImage(hotel.Id, url);
                }
            }
        }

        return RedirectToAction(nameof(Index), new { businessId = hotel.BusinessId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var hotel = HotelService.GetById(id);

        if (hotel == null)
            return NotFound();

        HotelService.Delete(id);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var hotel = HotelService.GetHotelFullDetails(id);

        if (hotel == null)
            return NotFound();

        ViewBag.Rooms = new SelectList(RoomService.GetAll(), "Id", "RoomNumber");
        return View(hotel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Hotel hotel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Rooms = new SelectList(RoomService.GetAll(), "Id", "RoomNumber");
            return View(hotel);
        }
        HotelService.UpdateHotel(hotel);
        return RedirectToAction(nameof(Details), new { id = hotel.Id });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddImage(int hotelId, string imagePath, string returnAction = nameof(Details))
    {
        HotelService.AddImage(hotelId, imagePath);
        return RedirectToAction(returnAction, new { id = hotelId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateImage(int hotelId, int imageId, string imagePath)
    {
        HotelService.UpdateImage(hotelId, imageId, imagePath);
        return RedirectToAction(nameof(Edit), new { id = hotelId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteImage(int hotelId, int imageId, string returnAction = nameof(Details))
    {
        HotelService.DeleteImage(hotelId, imageId);
        return RedirectToAction(returnAction, new { id = hotelId });
    }
}
