namespace ControllRR.Domain.Entities;


public class SaleItem
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; }
    public int StockId { get; set; }
    public Stock Stock { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxAmount { get; set; }

    // Iniciallização sem propriedades, necessário par usar o ORM( e peota que o pareo)
    public SaleItem()
    {

    }

    public SaleItem(Sale sale, Stock stock, int quantity, decimal unitPrice, decimal taxAmount)
    {
        // Validações
        if (sale == null)
            throw new ArgumentNullException(nameof(sale), "Venda não pode ser nula");
        
        if (stock == null)
            throw new ArgumentNullException(nameof(stock), "Item do estoque não pode ser nulo");
        
        if (quantity <= 0)
            throw new ArgumentException("Quantidade deve ser maior que zero", nameof(quantity));
        
        if (unitPrice < 0)
            throw new ArgumentException("Preço unitário não pode ser negativo", nameof(unitPrice));
        
        if (taxAmount < 0)
            throw new ArgumentException("Valor do imposto não pode ser negativo", nameof(taxAmount));

        // Inicialização das propriedades
        Sale = sale;
        SaleId = sale.Id;
        Stock = stock;
        StockId = stock.Id;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TaxAmount = taxAmount;
    }

}