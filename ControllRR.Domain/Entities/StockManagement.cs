using System.Text.Json.Serialization;
using ControllRR.Domain.Enums;

namespace ControllRR.Domain.Entities;

public class StockManagement
{
    public int Id { get; set; }
    public DateTime MovementDate { get; set; }
    public StockMovementType MovementType { get; set; }
    public int Quantity { get; set; }

    public int StockId { get; set; }
    [JsonIgnore]
    public Stock Stock { get; set; } = null!;

    public int? MaintenanceId { get; set; }
    public Maintenance? Maintenance { get; set; }

    // Propriedade derivada para exibição
    public string? MaintenanceNumber => Maintenance?.MaintenanceNumber?.ToString();
    public int? PurchaseOrderId { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
    
    public int? SaleId { get; set; }
    public Sale? Sale { get; set; }

    // Construtor vazio necessário para ORM ou deserialização
    public StockManagement() { }

    public StockManagement(int id,
     DateTime movementDate,
     StockMovementType movementType,
     int quantity,
     Stock stock,
     Maintenance? maintenance = null,
     PurchaseOrder? purchaseOrder = null ,
     Sale? sale = null 
     )
    {
        Id = id;
        MovementDate = movementDate;
        MovementType = movementType;
        Quantity = quantity;
        Stock = stock ?? throw new ArgumentNullException(nameof(stock));
        Maintenance = maintenance;
        PurchaseOrder = purchaseOrder;
        Sale = sale;
    }
}
