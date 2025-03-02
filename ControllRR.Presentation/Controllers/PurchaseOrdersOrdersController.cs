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
    public async Task<IActionResult> Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<object>());
        }

        var purchaseSearch = await _purchaseOrderService.Search(term);

        return Json(purchaseSearch.Select(x => new
        {
            IssuerCNPJ = x.IssuerCNPJ,
            IssuerIE = x.IssuerIE,
            NFeAccessKey = x.NFeAccessKey,
            InvoiceNumber = x.InvoiceNumber
        }).ToList());

    }
}