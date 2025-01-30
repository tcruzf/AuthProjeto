using ControllRR.Domain.Entities;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Dto;

public class StockManagementDto
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
    public StockDto Stock { get; set; } = null!;
}