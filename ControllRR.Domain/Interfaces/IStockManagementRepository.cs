using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IStockManagementRepository
{
    Task<List<StockManagement>> FindAllAsync();
    //Task Search(string term);
}