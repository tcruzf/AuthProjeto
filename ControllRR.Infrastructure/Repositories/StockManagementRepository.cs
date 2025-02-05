using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControllRR.Infrastructure.Repositories;

public class StockManagementRepository : IStockManagementRepository
{

    private readonly ControllRRContext _controllRRContext;
    public StockManagementRepository(ControllRRContext controllRRContext)
    {
        _controllRRContext = controllRRContext;
    }

    public async Task AddAsync(StockManagement stock)
    {
        await _controllRRContext.AddAsync(stock);
        await SaveChangesAsync();
    }

    //Return all Stock Itens
    public async Task<List<StockManagement>> FindAllAsync()
    {
        var stockProductInfo = await _controllRRContext.StockManagements
        .Include(sm => sm.Maintenance)
        .Include(sm => sm.Stock)
        .ToListAsync();
        return stockProductInfo;
    }



    public async Task SaveChangesAsync()
    {
        await _controllRRContext.SaveChangesAsync();
    }



    public async Task<List<StockManagement>> GetByStockIdAsync(int stockId)
    {
        return await _controllRRContext.StockManagements
            .Where(m => m.StockId == stockId)
            .OrderByDescending(m => m.MovementDate)
            .ToListAsync();
    }


}