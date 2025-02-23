using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Infrastructure.Repositories;

public class PurchaseOrderRepository : BaseRepository<PurchaseOrder>, IPurchaseOrderRepository
{
    public PurchaseOrderRepository(ControllRRContext context) : base(context)
    {

    }

    public Task<List<PurchaseOrder>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PurchaseOrder?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PurchaseOrder>> SearchAsync(string term)
    {
        throw new NotImplementedException();
    }
}