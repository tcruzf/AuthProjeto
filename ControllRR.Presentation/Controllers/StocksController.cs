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
    public async Task<IActionResult> NewProduct()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewProduct(StockViewModel model)
    {
        System.Console.WriteLine(model.StockDto.ProductName);
        System.Console.WriteLine(model.StockDto.ProductQuantity);
        System.Console.WriteLine(model.StockDto.ProductApplication);
        System.Console.WriteLine(model.StockDto.ProductDescription);
        System.Console.WriteLine(model.StockDto.ProductReference);

        if (!ModelState.IsValid)
            return View(model);


        try
        {
            TempData["SuccessMessage"] = "Produto inserido com sucesso!";
            // Usa o serviço para toda a lógica
            var createdStock = await _stockService.CreateProductWithInitialMovementAsync(model.StockDto);
            return RedirectToAction("GetProducts");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO: {ex.ToString()}");

            // Log para o usuário
            ModelState.AddModelError("", $"Erro interno: {ex.Message}");
            return View(model);
        }
    }

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
        // return Json(products); // Agora serializa corretamente
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
                movementType = m.MovementType == m.MovementType ? "Entrada" : "Saída",
                quantity = m.Quantity,
                movementDate = m.MovementDate  // para uso no reloadProductData() - Testado e funcional
            }).ToList()
        }));
    }

    [HttpGet("GetProduct/{id}")]
    public async Task<IActionResult> GetProduct(int id)
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

    [HttpPost]
    public async Task<IActionResult> AddMovement(int stockId, StockMovementType type, int quantity, DateTime movementDate)
    {
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