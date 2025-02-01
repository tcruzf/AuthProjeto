using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface IStockService
{
    Task<List<StockDto>> FindAllAsync();
    Task<List<StockDto>> Search(string term);
     Task AddAsync(StockDto stock);
    Task<StockDto> CreateProductWithInitialMovementAsync(StockDto stockDto);
}