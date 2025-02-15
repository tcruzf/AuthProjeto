using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ControllRR.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [Authorize(Roles = "Manager, Admin")]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    public IActionResult DashBoard()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
