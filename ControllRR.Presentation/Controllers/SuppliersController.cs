using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class SuppliersController : Controller
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateNewSupplier()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewSupplier(SupplierDto supplierDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Fornecedor não pode ser inserido!";
            return View(supplierDto);
        }
        var result = await _supplierService.InsertAsync(supplierDto);
        if (result.Success)
        {
            TempData["SuccessMessage"] = "Fornecedor cadastrado com sucesso!";
            return RedirectToAction(nameof(GetAllSuppliers));
        }
        TempData["ErrorMessage"] = result.Message;
        return View(supplierDto);

    }
}