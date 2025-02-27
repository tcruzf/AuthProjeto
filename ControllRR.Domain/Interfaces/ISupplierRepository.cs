using System.Linq.Expressions;
using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<List<Supplier>> FindAllAsync();
    Task<List<Supplier>> SearchAsync(string term);
    Task<Supplier?> GetByIdAsync(int id);
    Task<bool> AnyAsync(Expression<Func<Supplier, bool>> predicate);
}