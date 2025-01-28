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
}

