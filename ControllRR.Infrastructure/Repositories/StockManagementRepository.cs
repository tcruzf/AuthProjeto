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

    //Return all Stock Itens
    public async Task<List<StockManagement>> FindAllAsync()
    {
        var stockProductInfo = await _controllRRContext.StockManagements
        .ToListAsync();
        return stockProductInfo;
    }

    

}