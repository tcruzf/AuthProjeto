using ControllRR.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Presentation.Controllers;

public class PurchaseOrdersController : Controller
{
    private readonly IPurchaseOrderService _purchaseOrderService;
    public PurchaseOrdersController(IPurchaseOrderService purchaseOrderService)
    {
        _purchaseOrderService = purchaseOrderService;
    }

    [HttpGet]
    public async Task<IActionResult> SearchOrder(string filtro)
    {
        if (string.IsNullOrWhiteSpace(filtro))
        {
            return Json(new List<object>());
        }

        var purchaseSearch = await _purchaseOrderService.Search(filtro);

        return Json(purchaseSearch.Select(x => new
        {
            //Id = x.Id.ToString(),
            IssuerCNPJ = x.IssuerCNPJ,
            IssuerIE = x.IssuerIE,
            NFeAccessKey = x.NFeAccessKey,
            InvoiceNumber = x.InvoiceNumber
        }).ToList());

    }
}