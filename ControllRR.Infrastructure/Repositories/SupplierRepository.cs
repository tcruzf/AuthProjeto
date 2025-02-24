using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ControllRRContext context) : base(context)
    {

    }

    public async Task<List<Supplier>> FindAllAsync()
    {
        return await _context.Suppliers.ToListAsync();
    }

    public Task<Supplier?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Supplier>> SearchAsync(string term)
    {
        throw new NotImplementedException();
    }
}