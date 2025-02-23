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
    [DataType(DataType.Currency)]
    public decimal PurchasePrice { get; set; }
    [Display(Name = "Preço de Venda")]
    [DataType(DataType.Currency)]
    public decimal SalePrice { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    [Display(Name = "Imposto (%)")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal TaxRate { get; set; }
}

