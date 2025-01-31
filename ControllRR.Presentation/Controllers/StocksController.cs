using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class StocksController : Controller
{
    private readonly IStockService _stockService;
    
    public StocksController(IStockService stockService)
    {
        _stockService = stockService;
    }
    
    public async Task<IActionResult> NewProduct()
    {
        return View();
    }
    public async Task<IActionResult> GetProducts()
    {
        var stockProduct = await _stockService.FindAllAsync();
        return View(stockProduct);
    }

    public IActionResult SearchProduct() // Pode ser síncrono se não carregar dados
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
    return Json(products); // Agora serializa corretamente
}
}