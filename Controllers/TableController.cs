using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class TableController : Controller
{
    ITable tableService;

    public TableController(ITable tableService)
    {
        this.tableService = tableService;
    }

    public IActionResult Index()
    {
        return View(tableService.GetTables());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        tableService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
