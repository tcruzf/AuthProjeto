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
    [Display(Name = "Codigo Interno do Produto")]
    public string? ProductInternalCode { get; set; }
    [Display(Name = "Codigo de Barras do Produto")]
    public string? ProductBarCode { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? IcmsBase { get; set; }       // Base de cálculo do ICMS
    public decimal? IcmsAmount { get; set; }     // Valor do ICMS
    public decimal? PisBase { get; set; }        // Base de cálculo do PIS
    public decimal? PisAmount { get; set; }      // Valor do PIS
    public decimal? CofinsBase { get; set; }     // Base de cálculo do COFINS
    public decimal? CofinsAmount { get; set; }   // Valor do COFINS
   
    public int? PurchaseOrderId {get; set;} // Que bosta
    public int? InvoiceNumber {get; set;}
    public string? CFOPId {get; set;}


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
        Supplier supplier,
        decimal taxRate,
        decimal? profit,
        decimal? priceSugested,
        string? productInternalCode,
        string? productBarCode,
        int? quantity,
        decimal? unitprice,
        decimal? taxAmount,
        decimal? icmsBase,
        decimal? icmsAmount,
        decimal? pisBase,
        decimal? pisAmount,
        decimal? cofinsBase,
        decimal? cofinsAmount,
        int? invoiceNumber,
        string? cfopId,
        int? purchaseOrderId
       


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
        ProductInternalCode = productInternalCode;
        ProductBarCode = productBarCode;
        Quantity = quantity;
        UnitPrice = unitprice;
        TaxAmount = taxAmount;
        IcmsBase = icmsBase;
        IcmsAmount = icmsAmount;
        PisAmount = pisAmount;
        PisBase = pisBase;
        CofinsAmount = cofinsAmount;
        CofinsBase = cofinsBase;
        InvoiceNumber = invoiceNumber;
        CFOPId = cfopId;
        PurchaseOrderId = purchaseOrderId;
       
       

    }

}

