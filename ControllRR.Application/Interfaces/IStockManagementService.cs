using ControllRR.Application.Dto;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Interfaces;

public interface IStockManagementService
{
    Task<List<StockManagementDto>> FindAllAsync();

    Task AddMovementAsync(int stockId, StockMovementType type, int quantity, DateTime movementDate, int? maintenanceId=null );
    Task<int> GetCurrentStockAsync(int stockId);
    
}