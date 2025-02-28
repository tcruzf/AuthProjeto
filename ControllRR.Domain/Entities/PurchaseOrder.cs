namespace ControllRR.Domain.Entities;


public class PurchaseOrder
{
    public int Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string? InvoiceNumber { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? TotalTaxes { get; set; }
    public ICollection<PurchaseItem?> Items { get; set; } = new List<PurchaseItem>();
    public ICollection<FinancialRecord?> FinancialRecords { get; set; } = new List<FinancialRecord>();
    public string? NFeAccessKey { get; set; }
    public DateTime? NFeEmissionDate { get; set; }
    public int StockId { get; set; }

    public PurchaseOrder()
    {

    }

    public PurchaseOrder(DateTime orderDate, Supplier supplier)
    {
        OrderDate = orderDate;
        Supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
        SupplierId = supplier.Id;
        Items = new List<PurchaseItem>();
    }
}