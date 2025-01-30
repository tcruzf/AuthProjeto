using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{

    private readonly ControllRRContext _controllRRContext;
    public StockRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    public async Task<List<Stock>> FindAllAsync()
    {
        var stock = await _controllRRContext.Stocks
        .ToListAsync();
        return stock;
    }

    public async Task<List<Stock>> SearchAsync(string term)
    {
        return await _controllRRContext.Stocks
        //.Include(x => x.Movements)
         .Where(d => d.ProductName.Contains(term) ||
                     d.ProductDescription.Contains(term) ||
                     d.ProductReference.Contains(term) ||
                     d.ProductApplication.Contains(term))
         .ToListAsync();
    }
}
