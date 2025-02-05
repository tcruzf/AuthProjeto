using ControllRR.Application.Dto;
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
         .Where(s => s.ProductName.Contains(term) ||
                    s.ProductDescription.Contains(term) ||
                    s.ProductReference.Contains(term) ||
                    s.ProductApplication.Contains(term))
         .Include(s => s.Movements)
             .ThenInclude(m => m.Maintenance) // Carrega a manutenção relacionada
         .ToListAsync();
    }

    public async Task InsertAsync(Stock stock)
    {

        await _controllRRContext.Stocks.AddAsync(stock);
        await _controllRRContext.SaveChangesAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _controllRRContext.Stocks
            .Include(s => s.Movements)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateAsync(Stock stock)
    {
        //_controllRRContext.Stocks.Update(stock);
        //await _controllRRContext.SaveChangesAsync();
        _controllRRContext.Entry(stock).State = EntityState.Modified;
    }
}
