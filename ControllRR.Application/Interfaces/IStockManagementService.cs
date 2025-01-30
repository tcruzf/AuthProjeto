using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface IStockManagementService
{
    Task<List<StockManagementDto>> FindAllAsync();
}