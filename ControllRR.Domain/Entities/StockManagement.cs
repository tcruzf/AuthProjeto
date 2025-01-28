using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;

public class StockManagement
{

    public int Id { get; set; }

    // Data da movimentação
    public DateTime MovementDate { get; set; }

    // Enum para indicar se é entrada ou saída
    public StockMovementType MovementType { get; set; }

    // Quantidade movimentada
    public int Quantity { get; set; }

    // Relacionamento com o estoque
    // O LinQ é inteligente o suficiente para entender que é um vinculo com a class Stock
    // Referncia => https://learn.microsoft.com/en-us/ef/ef6/fundamentals/relationships
    public int StockId { get; set; }
    public Stock Stock { get; set; } = null!;

    // Construtor vazio - Quando usar?
    // Construtor vazio necessário para ORM ou deserialização
    public StockManagement()
    {

    }
    // No caso preciso impedir que seja repassado dados invalidos
    // Existe uma necessidade de semear alguns dados no ambiente de desenvolvimento.
    // Referencia =>
    public StockManagement(int id, DateTime movementDate, StockMovementType movementType, int quantity, Stock stock)
    {
        Id = id;
        MovementDate = movementDate;
        MovementType = movementType;
        Quantity = quantity;
        Stock = stock;
        
    }

}