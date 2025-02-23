namespace ControllRR.Domain.Entities;


public class PurchaseItem
{
    public int Id { get; set; }
    public int PurchaseOrderId { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxAmount { get; set; }

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