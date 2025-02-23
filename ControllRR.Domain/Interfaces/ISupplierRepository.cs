using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface ISupplierRepository
{
    Task<List<Supplier>> FindAllAsync();
    Task<List<Supplier>> SearchAsync(string term);
    Task<Supplier?> GetByIdAsync(int id);

}