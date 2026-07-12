using GraduationProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers;

public class ReviewController : Controller
{
    IReview reviewService;

    public ReviewController(IReview reviewService)
    {
        this.reviewService = reviewService;
    }

    public IActionResult Index()
    {
        return View(reviewService.GetReviews());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        reviewService.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
