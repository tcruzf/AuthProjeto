using ControllRR.Application.Dto;
using ControllRR.Domain.Enums;

namespace ControllRR.Application.Interfaces;

public interface IStockManagementService
{
    Task<List<StockManagementDto>> FindAllAsync();

    Task AddMovementAsync(int stockId, StockMovementType type, int quantity);
    Task<int> GetCurrentStockAsync(int stockId);
    
}