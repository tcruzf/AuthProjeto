using System.ComponentModel;
using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControllRR.Presentation.Controllers;

public class SuppliersController : Controller
{
    private readonly ISupplierService _supplierService;
    private readonly IStockService _stockService;

    public SuppliersController(ISupplierService supplierService, IStockService stockService)
    {
        _supplierService = supplierService;
        _stockService = stockService;
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> CreateNewSupplier(int id)
    {
       SupplierDto supplierDto;
    if (id == 0)
    {
        supplierDto = new SupplierDto();
    }
    else
    {
        supplierDto = await _supplierService.GetSupplierByIdAsync(id);
        // Carrega produtos ou inicializa lista vazia
        ViewBag.SupplierProducts = await _stockService.GetBySupplierIdAsync(id) ?? new List<StockDto>();
    }
    return View(supplierDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetSupplierProducts(int supplierId)
    {
        var products = await _stockService.GetBySupplierIdAsync(supplierId);
        return ViewComponent("SupplierProducts", new { supplierId });
    }

    [Authorize(Roles = "Manager, Admin")]// Setadas as permissões para definir quem pode criar um novo fornecedor
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> CreateNewSupplier(SupplierDto supplierDto)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Fornecedor não pode ser inserido!";
            System.Console.WriteLine(supplierDto.Name);
            System.Console.WriteLine(supplierDto.CNPJ);
            return View(supplierDto);
        }
        var result = await _supplierService.InsertAsync(supplierDto);
        if (result.Success)
        {
            //TempData["SuccessMessage"] = "Fornecedor cadastrado com sucesso!";
            return RedirectToAction(nameof(CreateNewSupplier), new { id = result.Id });
        }
        //TempData["ErrorMessage"] = result.Message;
        ModelState.AddModelError(string.Empty, result.Message ?? "Erro desconhecido!");
        return View(supplierDto);

    }


    // Metodo criado apenas para fornecedores
    // Quando for cadastrar um fornecedor, então terei a opção de cadastrar também um produto.
    // Todas as vezes que enviar o form, a pagina será recarregada com o id do fornecedor como parametro
    // Ex: https://one.tva.one/Suppliers/CreateNewSupplier/16 -> cadastro, gera o id 16 e recarrega a pagina com o id.
    // ai já posso fazer a inserção do produtl ;)
    [Authorize(Roles = "Manager, Admin")]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> SupplierNewProduct(StockDto model)//
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Dados inválidos! Verifique os campos e tente novamente.";
            return RedirectToAction("CreateNewSupplier", new { id = model.SupplierId });
        }

        try
        {
            await _stockService.CreateProductWithInitialMovementAsync(model);
            TempData["ProductSuccessMessage"] = "Produto cadastrado com sucesso!";
            return RedirectToAction("CreateNewSupplier", new { id = model.SupplierId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Ocorreu um erro inesperado ao tentar incluir o produto! {ex.Message}";
            return RedirectToAction("CreateNewSupplier", new { id = model.SupplierId });
        }
    }

    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateNewPurcharseOrder(PurchaseOrderDto dto)
    {
        // Lógica de criação da ordem de compra
        return RedirectToAction("CreateNewSupplier", new { id = dto.SupplierId });
    }



    [HttpGet]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> Search(string term)
    {


        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<object>());
        }
        var result = await _supplierService.Search(term);

        return Json(result.Select(p => new
        {
            Id = p.Id,
            Name = p.Name,
            CNPJ = p.CNPJ,
            ContactEmail = p.ContactEmail,
            PhoneNumber = p.PhoneNumber,
            Address = p.Address
        }));

    }

    [HttpGet]
    public async Task<IActionResult> ValidateCnpj(string cnpj)
    {
        try
        {
            var exists = await _supplierService.CnpjExists(cnpj);
            return Json(new { valid = !exists, message = exists ? "CNPJ já cadastrado" : "CNPJ válido" });
        }
        catch (ArgumentException ex)
        {
            return Json(new { valid = false, message = ex.Message });
        }
    }
}