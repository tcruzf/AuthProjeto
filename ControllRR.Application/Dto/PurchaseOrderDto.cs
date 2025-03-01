using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;


public class PurchaseOrderDto
{
    public int Id { get; set; }
    [Display(Name = "Data de compra")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public DateTime OrderDate { get; set; }
    [Display(Name = "Data de entrega")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public DateTime? DeliveryDate { get; set; }
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public int SupplierId { get; set; }
    
    [Required(ErrorMessage = "{0} é obrigatório.")]
    [Display(Name = "Numero da Nota")]
    public string? InvoiceNumber { get; set; }
    [Required(ErrorMessage = "{0} é obrigatório.")]

    public decimal TotalAmount { get; set; }
    [Required(ErrorMessage = "{0} é obrigatório.")]

    public decimal TotalTaxes { get; set; }

    public decimal UnitPrice {get; set;}
    
    public int StockId {get; set;}
    public List<PurchaseItemDto> Items { get; set; } = new List<PurchaseItemDto>();
    public string NFeAccessKey { get; set; }
    
    public DateTime NFeEmissionDate { get; set; }
   
    public ICollection<FinancialRecord> FinancialRecords { get; set; } = new List<FinancialRecord>();

}