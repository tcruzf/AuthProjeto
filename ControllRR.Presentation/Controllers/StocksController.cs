//Toda honra e gloria ao meu Deus
// O senhor me acompanhou até aqui, te amo meu Pai
using AutoMapper;
using ControllRR.Application.Interfaces;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;
using ControllRR.Domain.Interfaces;
using ControllRR.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class StocksController : Controller
{
    private readonly IStockService _stockService;
    private readonly IStockManagementService _stockManagementService;
    public readonly IMapper _mapper;
    public readonly IStockRepository _stockRepository;

    public StocksController(IStockService stockService, IStockManagementService stockManagementService, IMapper mapper, IStockRepository stockRepository)
    {
        _stockService = stockService;
        _stockManagementService = stockManagementService;
        _mapper = mapper;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> NewProduct()
    {

        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<IActionResult> NewProduct(StockViewModel model)
    {
        if (!ModelState.IsValid)
            throw new Exception();

        try
        {
            TempData["SuccessMessage"] = "Produto inserido com sucesso!";
            // Usa o serviço para toda a lógica
            System.Console.WriteLine("Salvando produto");
            await _stockService.CreateProductWithInitialMovementAsync(model.StockDto);
            System.Console.WriteLine("Agora redirecionando");
            return RedirectToAction("SearchProduct");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.ToString()}");

            // Log para o usuário
            ModelState.AddModelError("", $"Erro interno: {ex.Message}");
            TempData["ErrorMessage"] = "Ocorreu um erro inesperado ao tentar incluir o produto!";
            return View(model);
        }
    }

    [Authorize(Roles = "Manager, Admin")]
    public async Task<IActionResult> GetProducts()
    {
        var stockProduct = await _stockService.FindAllAsync();
        return View(stockProduct);
    }

    // Pode ser síncrono se não carregar dados
    public IActionResult SearchProduct()
    {
        return View();
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet]
    public async Task<IActionResult> Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<object>());
        }

        var products = await _stockService.Search(term);

        return Json(products.Select(p => new
        {
            id = p.Id,
            productName = p.ProductName,
            productDescription = p.ProductDescription,
            productApplication = p.ProductApplication,
            productReference = p.ProductReference,
            productQuantity = p.ProductQuantity,
            movements = p.Movements.Select(m => new
            {
                formattedMovementDate = m.MovementDate.ToString("yyyy-MM-dd"),
                movementType = m.MovementType == (int)StockMovementType.Entrada ? "Entrada" : "Saída", // Retorna tipo de movimentaçaão a ser exibida na pagina de 
                quantity = m.Quantity,                                                                 // de produtos 
                movementDate = m.MovementDate,
                maintenanceId = m.MaintenanceId,
                maintenanceNumber = m.MaintenanceNumber
            }).ToList()
        }));
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet("DDGetProduct/{id}")]
    public async Task<IActionResult> Desativado(int id)
    {
        var product = await _stockRepository.GetByIdAsync(id);
        return Json(new
        {
            productQuantity = product.ProductQuantity,
            movements = product.Movements.Select(m => new
            {
                movementDate = m.MovementDate,
                movementType = m.MovementType,
                quantity = m.Quantity
            })
        });
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpGet("Stocks/GetProduct/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _stockService.GetProductWithMovementsAsync(id);
        return Ok(product);
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpGet("Help/Documentation/Movements")]
    public async Task<IActionResult> Documentation()
    {
        var temp = "Temp temp";
        return Json(temp

        );
    }

    //
    [Authorize(Roles = "Manager, Admin")]
    [HttpPost]
    public async Task<IActionResult> AddMovement(int stockId, StockMovementType type, int quantity, DateTime movementDate)
    {
        if (stockId == null)
            return Content("<script type='text/javascript'>alert('Erro ao Cadastrar Plano: Rx005');</script>");
        System.Console.WriteLine(type.ToString());
        try
        {
            await _stockManagementService.AddMovementAsync(stockId, type, quantity, movementDate);
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }



}