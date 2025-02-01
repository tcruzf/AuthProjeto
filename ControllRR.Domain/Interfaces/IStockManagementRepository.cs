using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IStockManagementRepository
{
    Task<List<StockManagement>> FindAllAsync();
    Task AddAsync(StockManagement stock);
    //Task Search(string term);
    Task SaveChangesAsync();

}