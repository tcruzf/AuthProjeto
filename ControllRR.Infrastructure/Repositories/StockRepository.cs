using ControllRR.Application.Dto;
using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class StockRepository : BaseRepository<Stock>, IStockRepository
{

    
    public StockRepository(ControllRRContext context) : base(context)
    {
      
    }

    public async Task<List<Stock>> FindAllAsync()
    {
        var stock = await _context.Stocks
        .ToListAsync();
        return stock;
    }

    public async Task<List<Stock>> SearchAsync(string term)
    {
        return await _context.Stocks
         .Where(s => s.ProductName.Contains(term) ||
                    s.ProductDescription.Contains(term) ||
                    s.ProductReference.Contains(term) ||
                    s.ProductApplication.Contains(term))
         .Include(s => s.Movements)
             .ThenInclude(m => m.Maintenance) // Carrega a manutenção relacionada
         .ToListAsync();
    }

  
    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks
            .Include(s => s.Movements)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

  
}