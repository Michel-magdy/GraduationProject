using GraduationProject.Filters;
using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GraduationProject.Controllers;

public class TableController : Controller
{
    private readonly ITable _tableService;
    private readonly IRestaurant _restaurantService;
    private readonly IUser _userService;

    public TableController(ITable tableService, IRestaurant restaurantService, IUser userService)
    {
        _tableService = tableService;
        _restaurantService = restaurantService;
        _userService = userService;
    }

    // GET: Table/Index?restaurantId=1
    public IActionResult Index(int? restaurantId)
    {
        List<Table> tables;

        if (restaurantId.HasValue)
        {
            var restaurant = _restaurantService.GetById(restaurantId.Value);
            if (restaurant == null)
                return NotFound();

            ViewBag.RestaurantId = restaurant.Id;
            ViewBag.RestaurantName = restaurant.Name;
            tables = _tableService.GetTablesByRestaurant(restaurant.Id).ToList();
        }
        else
        {
            ViewBag.RestaurantName = "All Restaurants";
            tables = _tableService.GetTables();
        }

        return View(tables);
    }

    // GET: Table/Details/5
    public IActionResult Details(int id)
    {
        var table = _tableService.GetTableWithDetails(id);
        if (table == null)
            return NotFound();

        return View(table);
    }

    // GET: Table/Create?restaurantId=1
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Create(int restaurantId)
    {
        var restaurant = _restaurantService.GetById(restaurantId);
        if (restaurant == null)
            return NotFound();

        ViewBag.RestaurantId = restaurantId;
        ViewBag.RestaurantName = restaurant.Name;

        return View(new Table { RestaurantId = restaurantId });
    }

    // POST: Table/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Create(Table table)
    {
        if (!ModelState.IsValid)
        {
            var restaurant = _restaurantService.GetById(table.RestaurantId);
            ViewBag.RestaurantId = table.RestaurantId;
            ViewBag.RestaurantName = restaurant?.Name;
            return View(table);
        }

        _tableService.Add(table);
        return RedirectToAction(nameof(Index), new { restaurantId = table.RestaurantId });
    }

    // GET: Table/Edit/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Edit(int id)
    {
        var table = _tableService.GetTableWithDetails(id);
        if (table == null)
            return NotFound();

        ViewBag.RestaurantName = table.Restaurant?.Name;
        return View(table);
    }

    // POST: Table/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Edit(Table table)
    {
        if (!ModelState.IsValid)
        {
            var restaurant = _restaurantService.GetById(table.RestaurantId);
            ViewBag.RestaurantName = restaurant?.Name;
            return View(table);
        }

        _tableService.Update(table);
        return RedirectToAction(nameof(Details), new { id = table.Id });
    }

    // GET: Table/Delete/5
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult Delete(int id)
    {
        var table = _tableService.GetTableWithDetails(id);
        if (table == null)
            return NotFound();

        return View(table);
    }

    // POST: Table/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult DeleteConfirmed(int id)
    {
        var table = _tableService.GetById(id);
        if (table == null)
            return NotFound();

        var restaurantId = table.RestaurantId;
        _tableService.Delete(id);
        return RedirectToAction(nameof(Index), new { restaurantId });
    }

    // POST: Table/ToggleStatus/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(RoleFilter), Arguments = new object[] { new string[] { "Admin", "Owner" } })]
    public IActionResult ToggleStatus(int id)
    {
        var table = _tableService.GetById(id);
        if (table == null)
            return NotFound();

        table.Status = table.Status switch
        {
            TableStatus.Available => TableStatus.Occupied,
            TableStatus.Occupied => TableStatus.Available,
            TableStatus.Reserved => TableStatus.Available,
            _ => TableStatus.Available
        };

        _tableService.Update(table);
        return RedirectToAction(nameof(Index), new { restaurantId = table.RestaurantId });
    }
}
