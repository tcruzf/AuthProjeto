using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Infrastructure.Repositories;

public class PurchaseItemRepository : BaseRepository<PurchaseItem>, IPurchaseItemRepository
{
    public PurchaseItemRepository(ControllRRContext context) : base(context)
    {

    }

    public Task<List<PurchaseItem>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PurchaseItem>> SearchAsync(string term)
    {
        throw new NotImplementedException();
    }
    
}