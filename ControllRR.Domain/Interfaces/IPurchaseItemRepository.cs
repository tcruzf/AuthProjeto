using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IPurchaseItemRepository
{
    Task<List<PurchaseItem>> FindAllAsync();
    Task<List<PurchaseItem>> SearchAsync(string term);
    Task<PurchaseItem?> GetByIdAsync(int id);
}