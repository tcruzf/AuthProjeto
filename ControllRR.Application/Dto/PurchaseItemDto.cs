using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;


public class PurchaseItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public decimal TaxAmount { get; set; }
    public string? CFOPId { get; set; }
    public decimal? IcmsBase { get; set; }       // Base de cálculo do ICMS
    public decimal? IcmsAmount { get; set; }     // Valor do ICMS
    public decimal? PisBase { get; set; }        // Base de cálculo do PIS
    public decimal? PisAmount { get; set; }      // Valor do PIS
    public decimal? CofinsBase { get; set; }     // Base de cálculo do COFINS
    public decimal? CofinsAmount { get; set; }   // Valor do COFINS
    [Display(Name = "% Imposto")]
    public decimal TaxRate { get; set; }  // Autopreenchido
    
    public string CFOP { get; set; }


}