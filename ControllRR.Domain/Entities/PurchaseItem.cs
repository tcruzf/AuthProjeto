namespace ControllRR.Domain.Entities;

 
public class PurchaseItem
{ 
    public int? Id { get; set; }
    public int? PurchaseOrderId { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TaxAmount { get; set; }
     public string? CFOPId { get; set; }
    public decimal? IcmsBase { get; set; }       // Base de cálculo do ICMS
    public decimal? IcmsAmount { get; set; }     // Valor do ICMS
    public decimal? PisBase { get; set; }        // Base de cálculo do PIS
    public decimal? PisAmount { get; set; }      // Valor do PIS
    public decimal? CofinsBase { get; set; }     // Base de cálculo do COFINS
    public decimal? CofinsAmount { get; set; }   // Valor do COFINS
    public PurchaseItem()
    {

    }

    public PurchaseItem(int quantity, decimal unitPrice, PurchaseOrder order, Stock stock)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        PurchaseOrder = order;
        Stock = stock;
        PurchaseOrderId = order.Id;
        StockId = stock.Id;
    }
}