using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Dto;


public class PurchaseOrderDto
{
    public int Id { get; set; }
    [Display(Name = "Data de compra")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [Display(Name = "Data de entrega")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public DateTime? DeliveryDate { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório.")]
    [Display(Name = "Numero da Nota")]
    public string? InvoiceNumber { get; set; }
    [Display(Name = "Total da Nota R$")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public decimal TotalAmount { get; set; }
    [Display(Name = "Total de Impostos %")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public decimal TotalTaxes { get; set; }
    [Display(Name = "Preço por unidade R$")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public decimal? UnitPrice { get; set; }
    public int StockId { get; set; }
    [Display(Name = "Preço por unidade R$")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public List<PurchaseItemDto?> Items { get; set; } = new List<PurchaseItemDto>();
    [Display(Name = "Chave da NFe")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public string NFeAccessKey { get; set; }
    [Display(Name = "Data de Emissão da NFe")]
    [Required(ErrorMessage = "{0} é obrigatório.")]

    public DateTime NFeEmissionDate { get; set; } = DateTime.Now;

    public ICollection<FinancialRecord?> FinancialRecords { get; set; } = new List<FinancialRecord>();
    [Display(Name = "Tipo de frete")]
    public FreightMode? FreightMode { get; set; }
    [Display(Name = "Status NFe")]
    public NFeStatus? NFeStatus { get; set; }

    [Display(Name = "Origem/Destino")]
    public NFeSource? NFeSource { get; set; }
    [Display(Name = "Tipo de Pagamento")]
    public PaymentMethod? PaymentMethod { get; set; }
    [Display(Name = "CNPJ do Emissor")]
    public string? IssuerCNPJ { get; set; }   // CNPJ da empresa que emite a nota
    [Display(Name = "Inscrição Estadual Emissor")]
    public string? IssuerIE { get; set; }
    [Display(Name = "Chave Refência NFe")]
    public string? ReferenceNFeKey { get; set; }
    [Display(Name = "Chave Refência NFe")]
    [Required(ErrorMessage = "{0} é obrigatório.")]
    public NFeOperationType? OperationType { get; set; }

}