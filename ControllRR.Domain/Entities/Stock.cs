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

    public Stock(){

    }

    public Stock(int id, string? productName, string? productDescription, int productQuantity, string? productApplication, string? productReference) 
    {
        Id = id;
        ProductName = productName;
        ProductDescription = productDescription;
        ProductQuantity = productQuantity;
        ProductApplication = productApplication;
        ProductReference = productReference;

    }

}

