using ControllRR.Application.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class SuppliersController : Controller
{
    private readonly ISupplierService _service;

    public SuppliersController(ISupplierService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        
        var suppliers = await _service.FindAllAsync();
        return View(suppliers);
    }

}