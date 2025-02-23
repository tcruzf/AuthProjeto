namespace ControllRR.Domain.Entities;


public class Sale
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string InvoiceNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalTaxes { get; set; }
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    public Sale()
    {

    }

     public Sale(DateTime saleDate, Customer customer)
    {
        SaleDate = saleDate;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        CustomerId = customer.Id;
        Items = new List<SaleItem>();
    }
}