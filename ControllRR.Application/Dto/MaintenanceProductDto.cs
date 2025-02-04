namespace ControllRR.Application.Dto;
public class MaintenanceProductDto
{
    public int Id { get; set; }
    public int StockId { get; set; }
    public int QuantityUsed { get; set; }
    public StockDto Stock { get; set; } 
    public bool IsDeleted { get; set; } 
}