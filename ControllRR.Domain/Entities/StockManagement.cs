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
    public int StockId { get; set; }
    public Stock Stock { get; set; } = null!;

}