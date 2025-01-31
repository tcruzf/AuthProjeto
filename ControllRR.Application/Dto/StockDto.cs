using ControllRR.Domain.Entities;

namespace ControllRR.Application.Dto;


   public class StockDto
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public int ProductQuantity { get; set; }
    public string? ProductApplication { get; set; }
    public string? ProductReference { get; set; }
    public List<StockManagementDto> Movements { get; set; } = new(); // Sem Stock!
}

