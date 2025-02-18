using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ControllRR.Application.Interfaces;
using System.Threading.Tasks;

namespace ControllRR.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISystemRoutines _systemRoutines;
    

    public HomeController(
        ILogger<HomeController> logger,
        ISystemRoutines systemRoutines
        )
    {
        _logger = logger;
        _systemRoutines = systemRoutines;
        

    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Index()
    {
        var device = await _systemRoutines.CountDevices();
        var maintenance = await _systemRoutines.CountMaintenance();
        var model = new DashboardViewModel
        {
            DeviceCount = device,
            MaintenanceCount = maintenance
        };

        return View(model);
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
