using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class StockManagementRepository : BaseRepository<StockManagement>, IStockManagementRepository
{

   
    public StockManagementRepository(ControllRRContext context) : base(context)
    {
        
    }

    public async Task AddAsync(StockManagement stock)
    {
        await _context.StockManagements.AddAsync(stock);
        
    }

    //Return all Stock Itens
    public async Task<List<StockManagement>> FindAllAsync()
    {
        var stockProductInfo = await _context.StockManagements
        .Include(sm => sm.Maintenance)
        .Include(sm => sm.Stock)
        .ToListAsync();
        return stockProductInfo;
    }


    public async Task<List<StockManagement>> GetByStockIdAsync(int stockId)
    {
        return await _context.StockManagements
            .Where(m => m.StockId == stockId)
            .OrderByDescending(m => m.MovementDate)
            .ToListAsync();
    }


}