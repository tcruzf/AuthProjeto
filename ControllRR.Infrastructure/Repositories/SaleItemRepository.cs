using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace ControllRR.Infrastructure.Repositories;


public class SaleItemRepository : BaseRepository<SaleItem>, IsaleItemRepository
{
    public SaleItemRepository(ControllRRContext context) : base(context)
    {

    }

    public Task<List<SaleItem>> FindAllAsync()
    {
        throw new NotImplementedException();
    }
}