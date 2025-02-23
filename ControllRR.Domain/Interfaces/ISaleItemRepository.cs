using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;


public interface IsaleItemRepository
{
    Task<List<SaleItem>> FindAllAsync();
}