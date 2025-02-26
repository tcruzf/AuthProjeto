using ControllRR.Application.Dto;
using ControllRR.Application.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateNewSupplier(int? id)
    {
        var model = new SupplierDto();
        if (id.HasValue)
        {
            var result = await _supplierService.GetSupplierByIdAsync(id.Value);
            if (result != null)
                model = result;
            return View(model);

        }
        return View(model);
    }

    [Authorize(Roles = "Manager, Admin")]// Setadas as permissões para definir quem pode criar um novo fornecedor
    [ValidateAntiForgeryToken]
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
            TempData["SuccessMessage"] = "Produto inserido com sucesso!";
            return RedirectToAction("CreateNewSupplier", new { id = model.SupplierId });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Ocorreu um erro inesperado ao tentar incluir o produto! {ex.Message}";
            return RedirectToAction("CreateNewSupplier", new { id = model.SupplierId });
        }
    }
}