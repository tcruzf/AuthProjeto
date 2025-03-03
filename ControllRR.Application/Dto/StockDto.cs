using System.ComponentModel.DataAnnotations;
using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;


public class StockDto
{
    public int Id { get; set; }
    [Display(Name = "Nome do Produto")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? ProductName { get; set; }
    [Display(Name = "Descrição Simples")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "{0} minimo {2} e no maximo {1} caracteres")]
    public string? ProductDescription { get; set; }
    [Display(Name = "Quantidade")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public int ProductQuantity { get; set; }
    [Display(Name = "Aplicação do Produto")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public string? ProductApplication { get; set; }
    [Display(Name = "Referencia")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public string? ProductReference { get; set; }

    public List<StockManagementDto> Movements { get; set; } = new();

    [Display(Name = "Preço de Compra")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [DataType(DataType.Currency)]
    public decimal PurchasePrice { get; set; }
    [Display(Name = "Preço de Venda")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [DataType(DataType.Currency)]
    public decimal SalePrice { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    [Display(Name = "Imposto (%)")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public decimal TaxRate { get; set; }
    [Display(Name = "Lucro")]
    [DataType(DataType.Currency)]
    public decimal? Profit { get; set; }

    [Display(Name = "Sugestão de preço de venda")]
    [DataType(DataType.Currency)]
    public decimal? PriceSugested { get; set; }
    [Display(Name = "Codigo Interno do Produto")]
    public string? ProductInternalCode { get; set; }
    [Display(Name = "Codigo de Barras do Produto")]
    public string? ProductBarCode { get; set; }
    [Display(Name = "Quantidade")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    public int? Quantity { get; set; }
    [Display(Name = "Preço Unitario")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [DataType(DataType.Currency)]
    public decimal? UnitPrice { get; set; }
    [Display(Name = "Total Impostos")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? TaxAmount { get; set; }
    [Display(Name = "ICMS Base")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? IcmsBase { get; set; }       // Base de cálculo do ICMS
    [Display(Name = "Total ICMS")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? IcmsAmount { get; set; }     // Valor do ICMS
    [Display(Name = "PIS Base")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? PisBase { get; set; }        // Base de cálculo do PIS
    [Display(Name = "Total PIS")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? PisAmount { get; set; }      // Valor do PIS
    [Display(Name = "COFINS Base")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? CofinsBase { get; set; }     // Base de cálculo do COFINS
    [Display(Name = "Total COFINS")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal? CofinsAmount { get; set; }   // Valor do COFINS
    public int? PurchaseOrderId {get; set;}
    [Display(Name = "Nº da NFe ")]
    public int? InvoiceNumber {get; set;}
    [Display(Name = "Nº da NFe ")]
    public string? CFOPId {get; set;}



}

