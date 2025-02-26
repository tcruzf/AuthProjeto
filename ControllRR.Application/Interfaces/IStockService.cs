using ControllRR.Application.Dto;

namespace ControllRR.Application.Interfaces;

public interface IStockService
{
    Task<List<StockDto>> FindAllAsync();
    Task<List<StockDto>> Search(string term);
    //  Task AddAsync(StockDto stock);
    Task<StockDto> CreateProductWithInitialMovementAsync(StockDto stockDto);

    Task<StockDto> GetProductWithMovementsAsync(int id);
    // Novo metodo para buscar uma lista de itens em estoque com base em um fornecedor. Por enquanto, funciona bem.
    Task<List<StockDto>> GetBySupplierIdAsync(int supplierId);
}