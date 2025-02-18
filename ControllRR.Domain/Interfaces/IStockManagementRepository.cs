using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IStockManagementRepository
{
    Task<List<StockManagement>> FindAllAsync();
    Task AddAsync(StockManagement movement);
    //Task Search(string term);
  //  Task SaveChangesAsync();
    Task<List<StockManagement>> GetByStockIdAsync(int stockId);

}