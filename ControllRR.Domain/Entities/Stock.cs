using System.ComponentModel.DataAnnotations;

namespace ControllRR.Domain.Entities;


public class Stock
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    // Quantidade atual no estoque
    public int ProductQuantity { get; set; }
    public string? ProductApplication { get; set; }
    public string? ProductReference { get; set; }

    // Relacionamento com as movimentações
    public ICollection<StockManagement> Movements { get; set; } = new List<StockManagement>();
    public ICollection<MaintenanceProduct> MaintenanceProducts { get; set; } = new List<MaintenanceProduct>();
    [Display(Name = "Preço de Compra")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [DataType(DataType.Currency)]
    public decimal PurchasePrice { get; set; }
    [Display(Name = "Preço de Venda")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [DataType(DataType.Currency)]
    public decimal SalePrice { get; set; }
    public int? SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    [Display(Name = "Imposto (%)")]
    [Required(ErrorMessage = "O campo {0} é obrigatorio ")]
    [Range(0, 100, ErrorMessage = "Taxa deve ser entre 0 e 100%")]
    public decimal TaxRate { get; set; }
      [Display(Name = "Lucro")]
    [DataType(DataType.Currency)]
    public decimal? Profit { get; set; }

    [Display(Name = "Sugestão de preço de venda")]
    [DataType(DataType.Currency)]
    public decimal? PriceSugested { get; set; }

    public Stock()
    {

    }

    public Stock(
        int id,
        string? productName,
        string? productDescription,
        int productQuantity,
        string? productApplication,
        string? productReference,
        decimal purchasePrice,
        decimal salePrice,
        Supplier supplier ,
        decimal taxRate,
        decimal? profit,
        decimal? priceSugested
        )
    {
        Id = id;
        ProductName = productName;
        ProductDescription = productDescription;
        ProductQuantity = productQuantity;
        ProductApplication = productApplication;
        ProductReference = productReference;
        PurchasePrice = purchasePrice;
        SalePrice = salePrice;
        Supplier = supplier;
        SupplierId = supplier?.Id;
        TaxRate = taxRate;
        Profit = profit;
        PriceSugested = priceSugested;

    }

}

