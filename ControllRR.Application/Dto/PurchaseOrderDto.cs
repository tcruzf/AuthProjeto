using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;


public class PurchaseOrderDto
{
    public int Id { get; set; }
    [Display(Name = "Data de compra")]
    public DateTime OrderDate { get; set; }
    [Display(Name = "Data de entrega")]
    public DateTime? DeliveryDate { get; set; }
    public int SupplierId { get; set; }
    [Display(Name = "Fornecedor")]
    public Supplier? Supplier { get; set; }
    [Display(Name = "Numero da Nota")]
    public string? InvoiceNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalTaxes { get; set; }
    public ICollection<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
    public string NFeAccessKey { get; set; }
    public DateTime NFeEmissionDate { get; set; }

}