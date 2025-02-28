using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class SupplierPurchaseOrdersViewComponent : ViewComponent
{
    private readonly IPurchaseOrderService _purchaseOrderService;

    public SupplierPurchaseOrdersViewComponent(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int supplierId)
    {
        var orders = await _purchaseOrderService.GetBySupplierAsync(supplierId);
        return View("Default", orders); // Renderiza a view "Default.cshtml"
    }
}