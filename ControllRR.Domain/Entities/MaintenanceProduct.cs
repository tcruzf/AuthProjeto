namespace ControllRR.Domain.Entities;

public class MaintenanceProduct
{
    public int Id { get; set; }
    // Chave estrangeira para Maintenance
    public int MaintenanceId { get; set; }
    public Maintenance Maintenance { get; set; }

    // Chave estrangeira para Stock
    public int StockId { get; set; }
    public Stock Stock { get; set; }

    // Quantidade do produto utilizada na manutenção
    public int QuantityUsed { get; set; }
}