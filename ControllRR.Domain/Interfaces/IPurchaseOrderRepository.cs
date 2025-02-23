using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IPurchaseOrderRepository
{
    Task<List<PurchaseOrder>> FindAllAsync();
    Task<List<PurchaseOrder>> SearchAsync(string term);
    Task<PurchaseOrder?> GetByIdAsync(int id);
}