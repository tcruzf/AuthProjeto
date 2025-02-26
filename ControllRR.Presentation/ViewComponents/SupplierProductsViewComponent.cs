using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class SupplierProductsViewComponent : ViewComponent
{
    private readonly IStockService _stockService;

    public SupplierProductsViewComponent(IStockService stockService)
    {
        _stockService = stockService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int supplierId)
    {
        // Busca produtos do estoque vinculados ao fornecedor fs
        var products = await _stockService.GetBySupplierIdAsync(supplierId);
        return View(products); // Passa a lista de StockDto para a view
    }
}